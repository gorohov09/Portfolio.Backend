namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Сообщения для уведомлений
	/// </summary>
	public static class NotificationMessages
	{
		/// <summary>
		/// Заголовок для события - Подача участия в мероприятии
		/// </summary>
		public static readonly string ParticipationActivitySubmittedTitle = "На Вас назначена проверка";

		/// <summary>
		/// Текст для события - Подача участия в мероприятии
		/// </summary>
		public static readonly string ParticipationActivitySubmittedDescription = "На Вас назначена проверка. Проверьте раздел - Проверки";

		/// <summary>
		/// Текст для события - Одобрение участия в мероприятии
		/// </summary>
		public static readonly string ParticipationActivityConfirmedTitle = "Уведомление об участии в мероприятии";

		/// <summary>
		/// Текст для события - Одобрение участия в мероприятии
		/// </summary>
		public static readonly string ParticipationActivityConfirmedDescription = "Ваше участие в мероприятии одобрено!";
	}
}
