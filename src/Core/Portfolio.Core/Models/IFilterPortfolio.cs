namespace Portfolio.Core.Models
{
	public interface IFilterPortfolio
	{
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
		List<string> Institutes { get; set; }

		/// <summary>
		/// Кафедры
		/// </summary>
		List<string> Faculties { get; set; }
	}
}
