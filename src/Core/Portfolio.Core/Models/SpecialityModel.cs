namespace Portfolio.Core.Models
{
	/// <summary>
	/// Модель специальности
	/// </summary>
	public class SpecialityModel
	{
		/// <summary>
		/// Код
		/// </summary>
		public string Code { get; set; } = default!;

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Уровень
		/// </summary>
		public string Level { get; set; } = default!;
	}
}
