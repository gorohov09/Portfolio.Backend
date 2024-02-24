using Microsoft.AspNetCore.SignalR;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Web.Hubs;

namespace Portfolio.Web.WebSocketServices
{
	/// <summary>
	/// Сервис SignalR для уведомлений
	/// </summary>
	public class HubNotificationService : IHubNotificationService
	{
		private readonly IHubContext<NotificationsHub> _hubContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="hubContext">Контекст хаба</param>
		public HubNotificationService(IHubContext<NotificationsHub> hubContext)
			=> _hubContext = hubContext;

		/// <summary>
		/// Отправить новое уведомление в SignalR
		/// </summary>
		/// <param name="notification">Сообщение</param>
		/// <param name="usersId">Идентификатор получателя</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		public async Task SendNewNotificationAsync(
			NotificationModel notification,
			Guid userId,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(notification);

			await _hubContext.Clients.User(userId.ToString())
				.SendAsync(
					"Notifications",
					new
					{
						notification.Type,
						notification.Title,
						notification.Description,
					},
					cancellationToken);
		}
	}
}
