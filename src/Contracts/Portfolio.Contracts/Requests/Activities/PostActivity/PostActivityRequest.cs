using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.Activities.PostActivity
{
	/// <summary>
	/// Запрос на создание мероприятия
	/// </summary>
	public class PostActivityRequest
	{
		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Вид
		/// </summary>
		public ActivitySections Section { get; set; } = default!;

		/// <summary>
		/// Тип
		/// </summary>
		public ActivityTypes Type { get; set; } = default!;

		/// <summary>
		/// Уровень
		/// </summary>
		public ActivityLevel Level { get; set; } = default!;

		/// <summary>
		/// Дата начала
		/// </summary>
		public DateTime StartDate { get; set; } = default!;

		/// <summary>
		/// Дата окончания
		/// </summary>
		public DateTime EndDate { get; set; } = default!;

		/// <summary>
		/// Место
		/// </summary>
		public string? Location { get; set; }

		/// <summary>
		/// Ссылка на официальную информацию
		/// </summary>
		public string? Link { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string? Description { get; set; }
	}
}
