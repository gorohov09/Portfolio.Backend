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
				|| string.IsNullOrEmpty(request.Email)
				|| string.IsNullOrEmpty(request.Password)
				|| string.IsNullOrEmpty(request.Login))
				throw new RequiredFieldNotSpecifiedException();

			var role = await _dbContext.Roles
				.FirstOrDefaultAsync(x => x.Id == DefaultRoles.StudentId, cancellationToken)
				?? throw new NotFoundException("Роль не найдена");

			var isExist = await _dbContext.Users.AnyAsync(
				x => x.Login == request.Login
				|| x.Email == request.Email, cancellationToken);

			if (isExist)
				throw new ApplicationExceptionBase("Пользователь с таким логином или e-mail уже существует");

			var passwordHash = _passwordEncryptionService.EncodePassword(request.Password);

			var user = new User(
				lastName: request.LastName,
				firstName: request.FirstName,
				login: request.Login,
				passwordHash: passwordHash,
				email: request.Email,
				role: role);

			await _dbContext.Users.AddAsync(user, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			var claims = _claimsIdentityFactory.CreateClaimsIdentity(user);
			var token = _tokenAuthenticationService.CreateToken(claims, TokenTypes.Auth);

			return new RegisterStudentResponse(studentId: user.Id, token: token);
		}
	}
}
