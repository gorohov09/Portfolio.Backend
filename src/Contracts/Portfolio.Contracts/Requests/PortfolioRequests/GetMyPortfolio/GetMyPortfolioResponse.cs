using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Ответ на запрос получения своего портфолио
	/// </summary>
	public class GetMyPortfolioResponse
	{
		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName { get; set; } = default!;

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName { get; set; } = default!;

		/// <summary>
		/// Отчество
		/// </summary>
		public string? Surname { get; set; }

		/// <summary>
		/// Дата рождения
		/// </summary>
		public DateTime Birthday { get; set; }

		/// <summary>
		/// Институт
		/// </summary>
		public InstituteResponse? Institute { get; set; }

		/// <summary>
		/// Кафедра
		/// </summary>
		public FacultyResponse? Faculty { get; set; }

		/// <summary>
		/// Специальность
		/// </summary>
		public SpecialityResponse? Speciality { get; set; }

		/// <summary>
		/// Уровень образования
		/// </summary>
		public EducationLevels? EducationLevel { get; set; }

		/// <summary>
		/// Номер группы
		/// </summary>
		public string? GroupNumber { get; set; }

		/// <summary>
		/// Блоки портфолио
		/// </summary>
		public List<PortfolioBlockResponse> Blocks { get; set; }
	}
}
