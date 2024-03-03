using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityById
{
	/// <summary>
	/// Ответ на запрос получения участия в мероприятии по Id
	/// </summary>
	public class GetParticipationActivityByIdResponse
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Статус участия в мероприятии
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
		/// Описание участия
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Документ, подтверждающий участие в мероприятии
		/// </summary>
		public GetParticipationActivityByIdResponseDocument? Document { get; set; }

		/// <summary>
		/// Флаг обозначающий, может ли пользователь редактировать сущность
		/// </summary>
		public bool CanEdit { get; set; }
	}
}
