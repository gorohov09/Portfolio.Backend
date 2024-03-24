namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityById
{
	/// <summary>
	/// Ответ для мероприятия, в котором принималось участие для <see cref="GetParticipationActivityByIdResponse"/>
	/// </summary>
	public class GetParticipationActivityByIdResponseActivity
	{
		/// <summary>
		/// Идентификатор мероприятия
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; } = default!;
	}
}
