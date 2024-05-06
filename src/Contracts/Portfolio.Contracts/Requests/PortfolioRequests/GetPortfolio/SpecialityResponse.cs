namespace Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolio
{
	/// <summary>
	/// Информация о специальности для <see cref="GetPortfolioResponse"/>
	/// </summary>
	public class SpecialityResponse
	{
		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Номер
		/// </summary>
		public string Number { get; set; } = default!;
	}
}
