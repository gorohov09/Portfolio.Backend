using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.ValueObjects
{
	/// <summary>
	/// Специальность
	/// </summary>
	public class Speciality : ValueObject
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="number">Номер</param>
		public Speciality(
			string number,
			GetSpecialityNameByNumber getSpecialityNameByNumber)
		{
			ArgumentNullException.ThrowIfNull(getSpecialityNameByNumber);
			ArgumentNullException.ThrowIfNull(number);

			Number = number;
			Name = getSpecialityNameByNumber(Number)
				?? throw new ApplicationExceptionBase("Не найдена специальность по номеру");
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		private Speciality()
		{
		}

		/// <summary>
		/// Делегат получения названия специальности по коду
		/// </summary>
		/// <param name="number">Номер специальности</param>
		/// <returns>Название специальности</returns>
		public delegate string? GetSpecialityNameByNumber(string number);

		/// <summary>
		/// Делегат соответствия номера уровню образования
		/// </summary>
		/// <param name="number">Номер специальности</param>
		/// <param name="educationLevel">Уровень образования</param>
		/// <returns>True/False</returns>
		public delegate bool SatisfySpecialityLevel(string number, EducationLevels educationLevel);

		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; private set; } = default!;

		/// <summary>
		/// Номер
		/// </summary>
		public string Number { get; private set; } = default!;

		/// <inheritdoc/>
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Name;
			yield return Number;
		}
	}
}
