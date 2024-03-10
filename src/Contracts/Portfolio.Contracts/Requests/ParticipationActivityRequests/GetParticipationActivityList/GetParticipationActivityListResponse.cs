namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityList
{
	/// <summary>
	/// Ответ на получение списка участий в мероприятии
	/// </summary>
	public class GetParticipationActivityListResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="entities">Список сущностей</param>
		/// <param name="totalCount">Колличество сущностей</param>
		public GetParticipationActivityListResponse(
			List<GetParticipationActivityListResponseItem> entities,
			int totalCount)
		{
			Entities = entities;
			TotalCount = totalCount;
		}

		/// <summary>
		/// Список сущностей
		/// </summary>
		public List<GetParticipationActivityListResponseItem> Entities { get; }

		/// <summary>
		/// Колличество сущностей
		/// </summary>
		public int TotalCount { get; set; }
	}
}
