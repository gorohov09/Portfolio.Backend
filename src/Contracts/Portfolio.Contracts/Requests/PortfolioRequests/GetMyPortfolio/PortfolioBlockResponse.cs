using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Информация о блоке в портфолио,
	/// в котором содержатся участия в мероприятии <see cref="GetMyPortfolioResponse"/>
	/// </summary>
	public class PortfolioBlockResponse
	{
		/// <summary>
		/// Название блока
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Секция, за которую отвечает блок
		/// </summary>
		public ActivitySections Section { get; set; }

		/// <summary>
		/// Список участий в мероприятиях
		/// </summary>
		public List<ParticipationActivityPortfolioResponse> ParticipationActivities { get; set; }
	}
}
