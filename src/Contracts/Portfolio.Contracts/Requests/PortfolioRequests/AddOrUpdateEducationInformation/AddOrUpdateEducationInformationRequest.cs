using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.PortfolioRequests.AddOrUpdateEducationInformation
{
	/// <summary>
	/// Запрос на добавление/обновление к портфолио информации о получении образования
	/// </summary>
	public class AddOrUpdateEducationInformationRequest
	{
		/// <summary>
		/// Уровень образования
		/// </summary>
		public EducationLevels? EducationLevel { get; set; }

		/// <summary>
		/// Номер группы
		/// </summary>
		public string? GroupNumber { get; set; }

		/// <summary>
		/// Номер специальности
		/// </summary>
		public string? SpecialityNumber { get; set; }

		/// <summary>
		/// Идентификатор кафедры
		/// </summary>
		public Guid? FacultyId { get; set; }
	}
}
