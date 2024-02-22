namespace Portfolio.Contracts.Requests.PortfolioRequests.AddGeneralInformation
{
	/// <summary>
	/// Запрос на добавление к портфолио общей информации
	/// </summary>
	public class AddOrUpdateGeneralInformationRequest
	{
		/// <summary>
		/// Фамилия
		/// </summary>
		public string? LastName { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public string? FirstName { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		public string? Surname { get; set; }

		/// <summary>
		/// Дата рождения
		/// </summary>
		public DateTime? Birthday { get; set; }
	}
}
