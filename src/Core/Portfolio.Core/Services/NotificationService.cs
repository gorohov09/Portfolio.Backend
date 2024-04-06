using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Реализация сервиса рассылки уведомлений
	/// </summary>
	public class NotificationService : INotificationService
	{
		private readonly IDbContext _dbContext;
		private readonly IHubNotificationService _hubNotificationService;
		private readonly ILogger<NotificationService> _logger;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контект БД</param>
		/// <param name="hubNotificationService">Сервис SignalR для уведомлений</param>
		/// <param name="logger">Логгер NotificationService</param>
		public NotificationService(
			IDbContext dbContext,
			IHubNotificationService hubNotificationService,
			ILogger<NotificationService> logger)
		{
			_dbContext = dbContext;
			_hubNotificationService = hubNotificationService;
			_logger = logger;
		}

		/// <summary>
		/// Получить список уведомлений пользователя по заданным параметрам фильтрации
		/// </summary>
		/// <param name="filter">Модель фильтра</param>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <param name="cancellationToken">Токен отмены операции</param>
		/// <returns>-</returns>
		public async Task<List<Notification>> GetNotificationsAsync(
			NotificationFilterModel? filter,
			Guid userId,
			CancellationToken cancellationToken)
		{
			var usersNotificationsQuery = _dbContext.Notifications.Where(notification => notification.UserId == userId);

			if (filter != null && filter.IsRead.HasValue)
			{
				return await usersNotificationsQuery
					.Where(notification => notification.IsRead == filter.IsRead)
					.ToListAsync(cancellationToken);
			}

			return await usersNotificationsQuery.ToListAsync(cancellationToken);
		}

		/// <summary>
		/// Отметить уведомления прочитанными
		/// </summary>
		/// <param name="notificationIds">Идентификаторы уведомлений</param>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <param name="cancellationToken">Токен отмены операции</param>
		/// <returns>-</returns>
		public async Task MarkAsReadAsync(Guid[] notificationIds, Guid userId, CancellationToken cancellationToken)
		{
			if (notificationIds == null || notificationIds.Length == 0)
				throw new ArgumentNullException(nameof(notificationIds));

			var notificationsToMarkAsRead = await _dbContext.Notifications
				.Where(notification => notificationIds.Contains(notification.Id) && notification.UserId == userId)
				.ToListAsync(cancellationToken);

			if (notificationsToMarkAsRead.Any(x => x.IsRead))
				throw new ApplicationExceptionBase("Нельзя прочитать прочитанное сообщение");

			foreach (var notification in notificationsToMarkAsRead)
				notification.IsRead = true;

			await _hubNotificationService.SendReadNotificationAsync(
					notificationsToMarkAsRead.Count,
					userId: userId,
					cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Отправка уведомления
		/// </summary>
		/// <param name="notificationModel">Модель уведомления</param>
		/// <param name="user">Объект пользователя</param>
		/// <param name="cancellationToken">Токен отмены операции</param>
		/// <param name="isSendingEmail">Флаг отправки уведомления на электронную почту</param>
		/// <returns>-</returns>
		public async Task PushAsync(
			NotificationModel notificationModel,
			User user,
			CancellationToken cancellationToken,
			bool isSendingEmail = false)
		{
			if (isSendingEmail)
				CreateEmailMessage(notificationModel, user);

			try
			{
				await _hubNotificationService.SendNewNotificationAsync(
					notification: notificationModel,
					userId: user.Id,
					cancellationToken: cancellationToken);

				CreateNotification(notificationModel, user);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Ошибка при отправке уведомления пользователю с Id - {user.Id}. {ex}");
			}
		}

		/// <summary>
		/// Создание уведомления
		/// </summary>
		/// <param name="notificationModel">Модель уведомления</param>
		/// <param name="user">Объект пользователя</param>
		private void CreateNotification(
			NotificationModel notificationModel,
			User user)
		{
			var notification = new Notification(
				notificationType: notificationModel.Type,
				user: user,
				title: notificationModel.Title,
				description: notificationModel.Description);

			_dbContext.Notifications.Add(notification);
		}

		/// <summary>
		/// Создание электронно-почтового сообщения
		/// </summary>
		/// <param name="notificationModel">Модель уведомления</param>
		/// <param name="user">Объект пользователя</param>
		private void CreateEmailMessage(
			NotificationModel notificationModel,
			User user)
		{
			var message = new EmailMessage(
				   addressTo: user.Email,
				   subject: notificationModel.Title,
				   body: notificationModel.Description,
				   toUserId: user.Id);

			_dbContext.EmailMessages.Add(message);
		}
	}
}
