namespace Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Информация о документе, подтверждающего участие в мероприятии
	/// для <see cref="ParticipationActivityPortfolioResponse"/>
	/// </summary>
	public class ParticipationActivityDocumentResponse
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Идентификатор файла
		/// </summary>
		public Guid FileId { get; set; }
	}
}
