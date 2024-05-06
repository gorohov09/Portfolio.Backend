using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolio
{
	/// <summary>
	/// Информация о мероприятии для <see cref="ParticipationActivityPortfolioResponse"/>
	/// </summary>
	public class ActivityPortfolioResponse
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
