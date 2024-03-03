using MediatR;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityById;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.GetParticipationActivityById
{
	/// <summary>
	/// Запрос на получения участия в мероприятии по идентификатору
	/// </summary>
	public class GetParticipationActivityByIdQuery : IRequest<GetParticipationActivityByIdResponse>
	{
		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid Id { get; set; }
	}
}
