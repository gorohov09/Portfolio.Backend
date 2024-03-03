namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityById
{
	/// <summary>
	/// Ответ для документа, подтверждающего участие в мероприятии для <see cref="GetParticipationActivityByIdResponse"/>
	/// </summary>
	public class GetParticipationActivityByIdResponseDocument
	{
		/// <summary>
		/// Идентификатор документа
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Адресс в файловом хранилище
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Название документа
		/// </summary>
		public string Name { get; set; }
	}
}
