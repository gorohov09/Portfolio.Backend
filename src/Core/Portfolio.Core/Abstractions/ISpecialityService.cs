using Portfolio.Domain.Enums;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис по работе со специальностями
	/// </summary>
	public interface ISpecialityService
	{
		/// <summary>
		/// Проверяет, существует ли специальность по номеру
		/// </summary>
		/// <param name="number">Номер специальности</param>
		/// <returns>True/False</returns>
		bool IsExist(string number);

		/// <summary>
		/// Проверяет, соответствует ли номер уровню образования
		/// </summary>
		/// <param name="number">Номер специальности</param>
		/// <param name="educationLevel">Уровень образования</param>
		/// <returns>True/False</returns>
		bool SatisfySpecialityLevel(string number, EducationLevels educationLevel);

		/// <summary>
		/// Получить название специальности по номеру
		/// </summary>
		/// <param name="number">Номер специальности</param>
		/// <returns>Название специальности</returns>
		string? GetNameByNumber(string number);
	}
}
