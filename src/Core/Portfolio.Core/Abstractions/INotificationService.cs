using Portfolio.Core.Models;
using Portfolio.Domain.Entities;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис рассылки уведомлений
	/// </summary>
	public interface INotificationService
	{
		/// <summary>
		/// Получить список уведомлений пользователя по заданным параметрам фильтрации
		/// </summary>
		/// <param name="filter">Модель фильтра</param>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <param name="cancellationToken">Токен отмены операции</param>
		/// <returns>-</returns>
		Task<List<Notification>> GetNotificationsAsync(NotificationFilterModel? filter, Guid userId, CancellationToken cancellationToken);

		/// <summary>
		/// Отметить уведомления прочитанными
		/// </summary>
		/// <param name="notificationIds">Идентификаторы уведомлений</param>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <param name="cancellationToken">Токен отмены операции</param>
		/// <returns>-</returns>
		Task MarkAsReadAsync(Guid[] notificationIds, Guid userId, CancellationToken cancellationToken);

		/// <summary>
		/// Отправка уведомления
		/// </summary>
		/// <param name="notificationModel">Модель уведомления</param>
		/// <param name="user">Объект пользователя</param>
		/// <param name="cancellationToken">Токен отмены операции</param>
		/// <param name="isSendingEmail">Флаг отправки уведомления на электронную почту</param>
		/// <returns>-</returns>
		Task PushAsync(NotificationModel notificationModel, User user, CancellationToken cancellationToken, bool isSendingEmail = false);
	}
}
