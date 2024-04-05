namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.PostParticipationActivity
{
	/// <summary>
	/// Запрос создания участия в мероприятии
	/// </summary>
	public class PostParticipationActivityRequest
	{
		/// <summary>
		/// Идентификатор мероприятия, для которого создается заявка
		/// </summary>
		public Guid? ActivityId { get; set; }
	}
}
