using Portfolio.Core.Models;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис SignalR для уведомлений
	/// </summary>
	public interface IHubNotificationService
	{
		/// <summary>
		/// Отправить новое уведомление в сигналер
		/// </summary>
		/// <param name="notification">Сообщение</param>
		/// <param name="usersId">Идентификатор получателя</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		Task SendNewNotificationAsync(
			NotificationModel notification,
			Guid userId,
			CancellationToken cancellationToken);
	}
}
