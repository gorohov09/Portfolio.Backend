using Portfolio.Domain.Enums;

namespace Portfolio.Domain.ValueObjects
{
	/// <summary>
	/// Специальность
	/// </summary>
	public class Speciality
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
			Name = getSpecialityNameByNumber(Number);
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
		public delegate string GetSpecialityNameByNumber(string number);

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

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			var otherSpeciality = (Speciality)obj;
			return Number == otherSpeciality.Number;
		}

		public override int GetHashCode() => HashCode.Combine(Number);

		public static bool operator ==(Speciality speciality1, Speciality speciality2)
			=> speciality1.Equals(speciality2);

		public static bool operator !=(Speciality speciality1, Speciality speciality2)
			=> !(speciality1 == speciality2);
	}
}
