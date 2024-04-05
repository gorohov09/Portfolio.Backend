using Portfolio.Domain.Enums;

namespace Portfolio.Core.Models
{
	/// <summary>
	/// Модель уведомления для SignalR
	/// </summary>
	public class NotificationModel
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="type">Тип уведомления</param>
		/// <param name="title">Заголовок</param>
		/// <param name="description">Описание</param>
		public NotificationModel(
			NotificationType type,
			string title,
			string description)
		{
			Type = type;
			Title = title;
			Description = description;
		}

		/// <summary>
		/// Тип уведомления
		/// </summary>
		public NotificationType Type { get; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; }
	}
}
