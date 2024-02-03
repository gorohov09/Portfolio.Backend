namespace Portfolio.Domain.ValueObjects
{
	/// <summary>
	/// Институт
	/// </summary>
	public class Institute
	{
		/// <summary>
		/// Полное имя
		/// </summary>
		public string FullName { get; set; } = default!;

		/// <summary>
		/// Сокращенное имя
		/// </summary>
		public string ShortName { get; set; } = default!;
	}
}
