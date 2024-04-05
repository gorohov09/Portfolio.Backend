namespace Portfolio.Contracts.Requests.NotificationRequests.MarkAsRead
{
	/// <summary>
	/// Запрос на перевод уведомлений в статус прочитанных
	/// </summary>
	public class MarkAsReadRequest
	{
		/// <summary>
		/// Идентификаторы уведомлений
		/// </summary>
		public Guid[] Ids { get; set; }
	}
}
