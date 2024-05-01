namespace Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Информация о участии в мероприятии для <see cref="GetMyPortfolioResponse"/>
	/// </summary>
	public class ParticipationActivityPortfolioResponse
	{
		/// <summary>
		/// Идентификтор участия у мероприятии
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Дата участия
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Мероприятие
		/// </summary>
		public ActivityPortfolioResponse Activity { get; set; } = default!;

		/// <summary>
		/// Документ, подтверждающий участие в мероприятии
		/// </summary>
		public ParticipationActivityDocumentResponse Document { get; set; } = default!;
	}
}
