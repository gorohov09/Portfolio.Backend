using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.NotificationRequests.GetNotificationList
{
	/// <summary>
	/// Элемент списка для <see cref="GetNotificationListResponse"/>
	/// </summary>
	public class GetNotificationListResponseItem
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Идентификатор пользователя-получателя
		/// </summary>
		public Guid ToUserId { get; set; }

		/// <summary>
		/// Тип уведомления
		/// </summary>
		public NotificationType Type { get; set; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string? Title { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Является ли уведомление прочитанным
		/// </summary>
		public bool IsRead { get; set; }

		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Дата обновления
		/// </summary>
		public DateTime UpdateDate { get; set; }
	}
}
