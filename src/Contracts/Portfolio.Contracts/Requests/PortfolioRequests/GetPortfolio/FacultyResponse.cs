namespace Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolio
{
	/// <summary>
	/// Информация о кафедре для <see cref="GetPortfolioResponse"/>
	/// </summary>
	public class FacultyResponse
	{
		/// <summary>
		/// Идентификатор кафедры
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Полное имя
		/// </summary>
		public string FullName { get; set; } = default!;

		/// <summary>
		/// Сокращенное имя
		/// </summary>
		public string ShortName { get; set; } = default!;
	}
}
