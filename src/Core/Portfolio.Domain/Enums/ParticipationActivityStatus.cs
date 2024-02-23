namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Статус участия в мероприятии
	/// </summary>
	public enum ParticipationActivityStatus
	{
		/// <summary>
		/// Черновик
		/// </summary>
		Draft = 1,

		/// <summary>
		/// Подан
		/// </summary>
		Submitted = 2,

		/// <summary>
		/// Отправлен на доработку
		/// </summary>
		SentRevision = 3,

		/// <summary>
		/// Одобрен
		/// </summary>
		Approved = 4,
	}
}
