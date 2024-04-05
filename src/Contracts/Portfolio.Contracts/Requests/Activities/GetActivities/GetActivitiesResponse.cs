namespace Portfolio.Contracts.Requests.Activities.GetActivities
{
	/// <summary>
	/// Ответ на получение списка мероприятий
	/// </summary>
	public class GetActivitiesResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="entities">Список сущностей</param>
		/// <param name="totalCount">Колличество сущностей</param>
		public GetActivitiesResponse(
			List<GetActivitiesResponseItem> entities,
			int totalCount)
		{
			Entities = entities;
			TotalCount = totalCount;
		}

		/// <summary>
		/// Список сущностей
		/// </summary>
		public List<GetActivitiesResponseItem> Entities { get; }

		/// <summary>
		/// Колличество сущностей
		/// </summary>
		public int TotalCount { get; set; }
	}
}
