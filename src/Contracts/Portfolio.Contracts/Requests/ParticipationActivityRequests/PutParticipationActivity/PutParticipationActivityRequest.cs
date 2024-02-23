using Microsoft.AspNetCore.Http;
using Portfolio.Domain.Enums;

namespace Portfolio.Contracts.Requests.ParticipationActivityRequests.PutParticipationActivity
{
	/// <summary>
	/// Запрос на обновление участия в мероприятии
	/// </summary>
	public class PutParticipationActivityRequest
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid Id { get; set; }

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
		/// Идентификатор мероприятия
		/// </summary>
		public Guid? ActivityId { get; set; }

		/// <summary>
		/// Документ, подтверждающий участие в мероприятии
		/// </summary>
		public IFormFile? File { get; set; }
	}
}
