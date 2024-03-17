namespace Portfolio.Contracts.Requests.NotificationRequests.GetNotificationList
{
	/// <summary>
	/// Ответ на получение списка уведомлений пользователя
	/// </summary>
	public class GetNotificationListResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="entities">Список сущностей</param>
		/// <param name="totalCount">Количество сущностей</param>
		public GetNotificationListResponse(
			List<GetNotificationListResponseItem> entities,
			int totalCount)
		{
			Entities = entities;
			TotalCount = totalCount;
		}

		/// <summary>
		/// Список сущностей
		/// </summary>
		public List<GetNotificationListResponseItem> Entities { get; }

		/// <summary>
		/// Количество сущностей
		/// </summary>
		public int TotalCount { get; set; }
	}
}
