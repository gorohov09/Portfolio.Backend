using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.AuthenticationRequests.RegisterStudent;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.AuthenticationRequests.RegisterStudent
{
	/// <summary>
	/// Обработчик команды <see cref="RegisterStudentCommand"/>
	/// </summary>
	public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, RegisterStudentResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IClaimsIdentityFactory _claimsIdentityFactory;
		private readonly IPasswordEncryptionService _passwordEncryptionService;
		private readonly ITokenAuthenticationService _tokenAuthenticationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="claimsIdentityFactory">Фабрика ClaimsPrincipal для пользователей</param>
		/// <param name="passwordEncryptionService">Сервис хэширования паролей</param>
		/// <param name="tokenAuthenticationService">Сервис работы с токенами</param>
		public RegisterStudentCommandHandler(
			IDbContext dbContext,
			IClaimsIdentityFactory claimsIdentityFactory,
			IPasswordEncryptionService passwordEncryptionService,
			ITokenAuthenticationService tokenAuthenticationService)
		{
			_dbContext = dbContext;
			_claimsIdentityFactory = claimsIdentityFactory;
			_passwordEncryptionService = passwordEncryptionService;
			_tokenAuthenticationService = tokenAuthenticationService;
		}

		/// <inheritdoc/>
		public async Task<RegisterStudentResponse> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			if (string.IsNullOrEmpty(request.LastName)
				|| string.IsNullOrEmpty(request.FirstName)
				|| string.IsNullOrEmpty(request.Phone)
				|| string.IsNullOrEmpty(request.Email)
				|| string.IsNullOrEmpty(request.Password))
				throw new RequiredFieldNotSpecifiedException();

			var role = await _dbContext.Roles
				.FirstOrDefaultAsync(x => x.Id == DefaultRoles.StudentId, cancellationToken)
				?? throw new NotFoundException("Роль не найдена");

			var isExist = await _dbContext.Users.AnyAsync(
				x => x.Login == request.Login
				|| x.Phone == request.Phone
				|| x.Email == request.Email, cancellationToken);

			if (isExist)
				throw new ApplicationExceptionBase("Укажите уникальный логин, e-mail и номер телефона");

			var passwordHash = _passwordEncryptionService.EncodePassword(request.Password);

			var user = new User(
				lastName: request.LastName,
				firstName: request.FirstName,
				birthday: request.Birthday,
				login: request.Login,
				passwordHash: passwordHash,
				email: request.Email,
				phone: request.Phone,
				role: role);

			await _dbContext.Users.AddAsync(user, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			var claims = _claimsIdentityFactory.CreateClaimsIdentity(user);
			var token = _tokenAuthenticationService.CreateToken(claims, TokenTypes.Auth);

			return new RegisterStudentResponse(studentId: user.Id, token: token);
		}
	}
}
