namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.PostParticipationActivity
{
	/// <summary>
	/// Ответ на запрос создания участия в мероприятии
	/// </summary>
	public class PostParticipationActivityResponse
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid ParticipationActivityId { get; set; }
	}
}
