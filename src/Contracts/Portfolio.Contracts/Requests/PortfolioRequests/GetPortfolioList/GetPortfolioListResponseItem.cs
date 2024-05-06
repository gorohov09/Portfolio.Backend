namespace Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolioList
{
	/// <summary>
	/// Элемент списка для <see cref="GetPortfolioListResponse"/>
	/// </summary>
	public class GetPortfolioListResponseItem
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// ФИО
		/// </summary>
		public string FullName { get; set; } = default!;

		/// <summary>
		/// Название института
		/// </summary>
		public string? InstituteName { get; set; }

		/// <summary>
		/// Название кафедры
		/// </summary>
		public string? FacultyName { get; set; }

		/// <summary>
		/// Номер группы
		/// </summary>
		public string? GroupNumber { get; set; }

		/// <summary>
		/// Название специальности
		/// </summary>
		public string? SpecialityName { get; set; }
	}
}
