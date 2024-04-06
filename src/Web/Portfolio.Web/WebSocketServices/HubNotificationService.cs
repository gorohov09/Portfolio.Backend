using Microsoft.AspNetCore.SignalR;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.Exceptions;
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

		/// <inheritdoc/>
		public async Task SendNewNotificationAsync(
			NotificationModel notification,
			Guid userId,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(notification);

			await _hubContext.Clients.User(userId.ToString())
				.SendAsync(
					"Notifications",
					notification,
					cancellationToken);
		}

		/// <inheritdoc/>
		public async Task SendReadNotificationAsync(
			int countReadNotifications,
			Guid userId,
			CancellationToken cancellationToken)
		{
			if (countReadNotifications < 0)
				throw new ApplicationExceptionBase("Нельзя прочитать отрицательное число сообщений");

			await _hubContext.Clients.User(userId.ToString())
				.SendAsync(
					"NotificationsRead",
					countReadNotifications,
					cancellationToken);
		}
	}
}
