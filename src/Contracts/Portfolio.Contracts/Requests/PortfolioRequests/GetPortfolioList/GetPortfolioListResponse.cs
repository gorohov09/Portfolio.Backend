namespace Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolioList
{
	/// <summary>
	/// Ответ на получение списка портфолио
	/// </summary>
	public class GetPortfolioListResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="entities">Список сущностей</param>
		/// <param name="totalCount">Колличество сущностей</param>
		public GetPortfolioListResponse(
			List<GetPortfolioListResponseItem> entities,
			int totalCount)
		{
			Entities = entities;
			TotalCount = totalCount;
		}

		/// <summary>
		/// Список сущностей
		/// </summary>
		public List<GetPortfolioListResponseItem> Entities { get; }

		/// <summary>
		/// Колличество сущностей
		/// </summary>
		public int TotalCount { get; set; }
	}
}
