using System.ComponentModel;

namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Уровень образования
	/// </summary>
	public enum EducationLevels
	{
		/// <summary>
		/// Бакалавриат
		/// </summary>
		[Description("Бакалавриат")]
		Bachelor = 1,

		/// <summary>
		/// Магистратура
		/// </summary>
		[Description("Магистратура")]
		Magistracy = 2,

		/// <summary>
		/// Специалитет
		/// </summary>
		[Description("Специалитет")]
		Specialty = 3,

		/// <summary>
		/// Аспирантура
		/// </summary>
		[Description("Аспирантура")]
		Postgraduate = 4,
	}
}
