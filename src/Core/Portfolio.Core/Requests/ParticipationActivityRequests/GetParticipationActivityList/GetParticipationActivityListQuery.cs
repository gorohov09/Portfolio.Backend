using MediatR;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityList;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.GetParticipationActivityList
{
	/// <summary>
	/// Запрос на получения участий в мероприятии, предназначенных каждому пользователю
	/// </summary>
	public class GetParticipationActivityListQuery : IRequest<GetParticipationActivityListResponse>
	{
	}
}
