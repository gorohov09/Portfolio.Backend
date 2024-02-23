using Portfolio.Domain.Enums;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис проверки прав доступа
	/// </summary>
	public interface IAuthorizationService
	{
		/// <summary>
		/// Проверить право доступа текущего пользователя
		/// </summary>
		/// <param name="privilege">Право доступа</param>
		/// <param name="cancellationToken">Токен отмены операции</param>
		/// <returns>-</returns>
		Task CheckPrivilegeAsync(Privileges privilege, CancellationToken cancellationToken = default);
	}
}
