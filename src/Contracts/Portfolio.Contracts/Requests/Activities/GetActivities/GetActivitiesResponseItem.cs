using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.Activities.GetActivities
{
	/// <summary>
	/// Элемент списка для <see cref="GetActivitiesResponse"/>
	/// </summary>
	public class GetActivitiesResponseItem
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
	}
}
