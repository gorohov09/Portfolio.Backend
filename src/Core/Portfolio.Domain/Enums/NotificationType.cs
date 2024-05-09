namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Типы уведомлений
	/// </summary>
	public enum NotificationType
	{
		/// <summary>
		/// Участие в мероприятии подано
		/// </summary>
		ParticipationActivitySubmitted = 1,

		/// <summary>
		/// Участие в мероприятии одобрено
		/// </summary>
		ParticipationActivityConfirmed = 2,

		/// <summary>
		/// Участие в мероприятии отправлено на доработку
		/// </summary>
		ParticipationActivitySendRevision = 3,
	}
}
