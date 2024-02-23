namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.PostParticipationActivity
{
	/// <summary>
	/// Ответ на команду <see cref="PostParticipationActivityRequest"/>
	/// </summary>
	public class PostParticipationActivityResponse
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid ParticipationActivityId { get; set; }
	}
}
