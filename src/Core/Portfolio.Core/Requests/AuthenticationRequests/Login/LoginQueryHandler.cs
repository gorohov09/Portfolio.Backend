using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.AuthenticationRequests.Login;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.AuthenticationRequests.Login
{
	/// <summary>
	/// Обработчик команды <see cref="LoginQuery"/>
	/// </summary>
	public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
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
		public LoginQueryHandler(
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
		public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			if (string.IsNullOrEmpty(request.Login)
				|| string.IsNullOrEmpty(request.Password))
				throw new RequiredFieldNotSpecifiedException();

			var user = await _dbContext.Users
				.Include(x => x.Role)
				.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken)
				?? throw new NotFoundException("Не найден пользователь по логину");

			if (user.Role == null)
				throw new ApplicationExceptionBase($"Пользователь с Id: {user.Id} не имеет роли");

			var isValidPassword = _passwordEncryptionService.ValidatePassword(
				password: request.Password,
				encodedPassword: user.PasswordHash);

			if (!isValidPassword)
				throw new ApplicationExceptionBase("Пароль неккоректный");

			var claims = _claimsIdentityFactory.CreateClaimsIdentity(user);
			var token = _tokenAuthenticationService.CreateToken(claims, TokenTypes.Auth);

			return new LoginResponse(
				userId: user.Id,
				role: user.Role!.ToRoleName(),
				token: token);
		}
	}
}
