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
	}
}
