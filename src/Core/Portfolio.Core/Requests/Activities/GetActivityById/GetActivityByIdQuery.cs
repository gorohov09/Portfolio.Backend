using MediatR;
using Portfolio.Contracts.Requests.Activities.GetActivityById;

namespace Portfolio.Core.Requests.Activities.GetActivityById
{
	/// <summary>
	/// Запрос на получение мероприятия по идентификатору
	/// </summary>
	public class GetActivityByIdQuery : IRequest<GetActivityByIdResponse>
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public Guid Id { get; set; }
	}
}
