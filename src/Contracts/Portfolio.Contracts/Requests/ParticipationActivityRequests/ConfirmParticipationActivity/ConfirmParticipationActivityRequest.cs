namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.ConfirmParticipationActivity
{
	/// <summary>
	/// Запрос на подтвержение участия в мероприятии
	/// </summary>
	public class ConfirmParticipationActivityRequest
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid Id { get; set; }
	}
}
