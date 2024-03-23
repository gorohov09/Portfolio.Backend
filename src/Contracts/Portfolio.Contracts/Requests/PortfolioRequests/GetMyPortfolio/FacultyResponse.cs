namespace Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Информация о кафедре для <see cref="GetMyPortfolioResponse"/>
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
