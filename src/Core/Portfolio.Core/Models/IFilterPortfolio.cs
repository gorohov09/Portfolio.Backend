namespace Portfolio.Core.Models
{
	public interface IFilterPortfolio
	{
		/// <summary>
		/// Фамилия
		/// </summary>
		string? LastName { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		string? FirstName { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		string? Surname { get; set; }

		/// <summary>
		/// Институт
		/// </summary>
		string Institute { get; set; }

		/// <summary>
		/// Кафедра
		/// </summary>
		string Faculty { get; set; }
	}
}
