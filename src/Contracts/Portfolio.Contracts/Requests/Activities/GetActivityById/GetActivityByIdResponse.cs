using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.Activities.GetActivityById
{
	/// <summary>
	/// Ответ на получение мероприятия
	/// </summary>
	public class GetActivityByIdResponse
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }

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
		/// Период
		/// </summary>
		public GetActivityByIdResponsePeriod Period { get; set; } = default!;

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
