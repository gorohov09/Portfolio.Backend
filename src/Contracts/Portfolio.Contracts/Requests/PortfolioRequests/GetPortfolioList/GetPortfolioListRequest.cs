using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolioList
{
	/// <summary>
	/// Запрос получения списка портфолио
	/// </summary>
	public class GetPortfolioListRequest
	{
		private int _pageNumber;
		private int _pageSize;

		/// <summary>
		/// Конструктор
		/// </summary>
		public GetPortfolioListRequest()
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
		/// Фамилия
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		public string Surname { get; set; }

		/// <summary>
		/// Институты
		/// </summary>
		public List<string> Institutes { get; set; }

		/// <summary>
		/// Кафедры
		/// </summary>
		public List<string> Faculties { get; set; }
	}
}
