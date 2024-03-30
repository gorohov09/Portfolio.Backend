namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Права доступа
	/// </summary>
	public enum Privileges
	{
		/// <summary>
		/// Портфолио - просмотр
		/// </summary>
		PortfolioView = 1,

		/// <summary>
		/// Участие в мероприятии - создание
		/// </summary>
		ParticipationActivityCreated = 101,

		/// <summary>
		/// Участие в мероприятии - обновление
		/// </summary>
		ParticipationActivityUpdate = 102,

		/// <summary>
		/// Участие в мероприятии - подача
		/// </summary>
		ParticipationActivitySubmit = 103,

		/// <summary>
		/// Участие в мероприятии - отправление на доработку
		/// </summary>
		ParticipationActivitySendRevision = 104,

		/// <summary>
		/// Участие в мероприятии - одобрение
		/// </summary>
		ParticipationActivityConfirm = 105,

		/// <summary>
		/// Мероприятие - создание
		/// </summary>
		ActivityCreated = 201,

		/// <summary>
		/// Мероприятие - обновление
		/// </summary>
		ActivityUpdated = 202,
	}
}
