using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.Activities.GetActivities
{
	/// <summary>
	/// Запрос на получение мероприятий
	/// </summary>
	public class GetActivitiesRequest
	{
		private int _pageNumber;
		private int _pageSize;

		/// <summary>
		/// Конструктор
		/// </summary>
		public GetActivitiesRequest()
		{
			_pageNumber = PaginationDefaults.PageNumber;
			_pageSize = PaginationDefaults.PageSize;
		}

		/// <summary>
		/// Номер страницы, начиная с 1
		/// </summary>
		public int PageNumber { get => _pageNumber; set => _pageNumber = value > 0 ? value : PaginationDefaults.PageNumber; }

		/// <summary>
		/// Размер страницы
		/// </summary>
		public int PageSize { get => _pageSize; set => _pageSize = value > 0 ? value : PaginationDefaults.PageSize; }

		/// <summary>
		/// Тип
		/// </summary>
		public ActivityTypes? Type { get; set; }

		/// <summary>
		/// Секция
		/// </summary>
		public ActivitySections? Section { get; set; }

		/// <summary>
		/// Уровень
		/// </summary>
		public ActivityLevel? Level { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string? Name { get; set; }
	}
}
