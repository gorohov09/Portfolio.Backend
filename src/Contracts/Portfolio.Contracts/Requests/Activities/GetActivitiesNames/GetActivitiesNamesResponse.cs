using Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityList;

namespace Portfolio.Contracts.Requests.Activities.GetActivitiesNames
{
	/// <summary>
	/// Ответ на получение списка названий мероприятий
	/// </summary>
	public class GetActivitiesNamesResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="entities">Список сущностей</param>
		/// <param name="totalCount">Колличество сущностей</param>
		public GetActivitiesNamesResponse(
			List<GetActivitiesNamesResponseItem> entities,
			int totalCount)
		{
			Entities = entities;
			TotalCount = totalCount;
		}

		/// <summary>
		/// Список сущностей
		/// </summary>
		public List<GetActivitiesNamesResponseItem> Entities { get; }

		/// <summary>
		/// Колличество сущностей
		/// </summary>
		public int TotalCount { get; set; }
	}
}
