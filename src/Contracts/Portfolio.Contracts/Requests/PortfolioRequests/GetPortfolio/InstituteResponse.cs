namespace Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolio
{
	/// <summary>
	/// Информация о институте для <see cref="GetPortfolioResponse"/>
	/// </summary>
	public class InstituteResponse
	{
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
