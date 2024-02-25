namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.SendRevisionParticipationActivity
{
	/// <summary>
	/// Запрос на отправление участия в мероприятии на доработку
	/// </summary>
	public class SendRevisionParticipationActivityRequest
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Причина отправление участия в мероприятии на доработку
		/// </summary>
		public string Comment { get; set; } = default!;
	}
}
