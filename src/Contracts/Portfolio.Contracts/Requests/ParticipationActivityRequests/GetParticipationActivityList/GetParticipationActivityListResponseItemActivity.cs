namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityList
{
	/// <summary>
	/// Модель мероприятия для <see cref="GetParticipationActivityListResponseItem"/>
	/// </summary>
	public class GetParticipationActivityListResponseItemActivity
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; } = default!;
	}
}
