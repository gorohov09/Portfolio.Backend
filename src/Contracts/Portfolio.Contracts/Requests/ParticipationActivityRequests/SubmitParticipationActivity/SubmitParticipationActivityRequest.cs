namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.SubmitParticipationActivity
{
	/// <summary>
	/// Запрос на подачу участия в мероприятии
	/// </summary>
	public class SubmitParticipationActivityRequest
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid Id { get; set; }
	}
}
