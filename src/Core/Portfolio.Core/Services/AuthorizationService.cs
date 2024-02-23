using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Extensions;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Сервис проверки прав доступа
	/// </summary>
	public class AuthorizationService : IAuthorizationService
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public AuthorizationService(IDbContext dbContext, IUserContext userContext)
		{
			_dbContext = dbContext;
			_userContext = userContext;
		}

		/// <inheritdoc/>
		public async Task CheckPrivilegeAsync(Privileges privilege, CancellationToken cancellationToken = default)
		{
			var userHasPrivilege = await _dbContext.Users
				.Where(x => x.Id == _userContext.CurrentUserId)
				.AnyAsync(
					x => x.Role!.Privileges!.Any(y => y.Privilege == privilege),
					cancellationToken);

			if (!userHasPrivilege)
				throw new UnauthorizedAccessException($"Пользователь не обладает правом {privilege.GetDescription()}");
		}
	}
}
