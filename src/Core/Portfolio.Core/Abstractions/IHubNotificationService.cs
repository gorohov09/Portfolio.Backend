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
		/// <param name="userId">Идентификатор получателя</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		Task SendNewNotificationAsync(
			NotificationModel notification,
			Guid userId,
			CancellationToken cancellationToken);

		/// <summary>
		/// Отправить сообщение о том, что сообщение прочитали
		/// </summary>
		/// <param name="countReadNotifications">Количество прочитанных сообщений</param>
		/// <param name="userId">Идентификатор получателя</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		Task SendReadNotificationAsync(
			int countReadNotifications,
			Guid userId,
			CancellationToken cancellationToken);
	}
}
