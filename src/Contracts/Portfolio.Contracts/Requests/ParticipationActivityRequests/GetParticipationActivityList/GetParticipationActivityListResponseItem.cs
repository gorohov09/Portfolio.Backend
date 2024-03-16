using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityList
{
	/// <summary>
	/// Элемент списка для <see cref="GetParticipationActivityListResponse"/>
	/// </summary>
	public class GetParticipationActivityListResponseItem
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Статус
		/// </summary>
		public ParticipationActivityStatus Status { get; set; }

		/// <summary>
		/// Результат
		/// </summary>
		public ParticipationActivityResult? Result { get; set; }

		/// <summary>
		/// Дата участия
		/// </summary>
		public DateTime? Date { get; set; }

		/// <summary>
		/// Мероприятие
		/// </summary>
		public GetParticipationActivityListResponseItemActivity? Activity { get; set; }

		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Дата обновления
		/// </summary>
		public DateTime UpdateDate { get; set; }
	}
}
