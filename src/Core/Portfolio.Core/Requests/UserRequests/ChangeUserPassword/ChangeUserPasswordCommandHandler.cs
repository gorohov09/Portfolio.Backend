
using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.UserRequests.ChangeUserPassword
{
	/// <summary>
	/// Обработчик запроса <see cref="ChangeUserPasswordCommand"/>
	/// </summary>
	public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;
		private readonly IPasswordEncryptionService _passwordEncryptionService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">User контекст</param>
		/// <param name="passwordEncryptionService">Сервис хэширования паролей</param>
		public ChangeUserPasswordCommandHandler(IDbContext dbContext, IUserContext userContext, IPasswordEncryptionService passwordEncryptionService)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_passwordEncryptionService = passwordEncryptionService;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			if (string.IsNullOrEmpty(request.OldPassword)
				|| string.IsNullOrEmpty(request.NewPassword))
				throw new RequiredFieldNotSpecifiedException();

			var user = await _dbContext.Users
				.FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException();

			var isValidOldPassword = _passwordEncryptionService.ValidatePassword(
				password: request.OldPassword,
				encodedPassword: user.PasswordHash);

			if (!isValidOldPassword)
				throw new ApplicationExceptionBase("Пароль неккоректный");

			var newPasswordHash = _passwordEncryptionService.EncodePassword(request.NewPassword);

			user.PasswordHash = newPasswordHash;

			await _dbContext.SaveChangesAsync(cancellationToken);

			return default;
		}
	}
}
