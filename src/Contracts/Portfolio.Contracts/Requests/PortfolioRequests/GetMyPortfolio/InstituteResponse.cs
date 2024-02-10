namespace Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Информация о институте для <see cref="GetMyPortfolioResponse"/>
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
