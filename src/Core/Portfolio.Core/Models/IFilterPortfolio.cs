namespace Portfolio.Core.Models
{
	public interface IFilterPortfolio
	{
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
