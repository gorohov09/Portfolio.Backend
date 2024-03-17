namespace Portfolio.Core.Models
{
	/// <summary>
	/// Фильтр для запроса получения уведомлений
	/// </summary>
	public class NotificationFilterModel
	{
		/// <summary>
		/// Является ли уведомление прочитанным
		/// </summary>
		public bool? IsRead { get; set; }
	}
}
