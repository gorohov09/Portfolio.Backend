using Portfolio.Domain.Enums;

namespace Portfolio.Core.Models
{
	public interface IFilterActivity
	{
		/// <summary>
		/// Тип
		/// </summary>
		public ActivityTypes? Type { get; set; }

		/// <summary>
		/// Секция
		/// </summary>
		public ActivitySections? Section { get; set; }

		/// <summary>
		/// Уровень
		/// </summary>
		public ActivityLevel? Level { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string? Name { get; set; }
	}
}
