using MediatR;
using Portfolio.Contracts.Requests.Activities.GetActivities;

namespace Portfolio.Core.Requests.Activities.GetActivities
{
	/// <summary>
	/// Запрос на получение списка мероприятий
	/// </summary>
	public class GetActivitiesQuery : IRequest<GetActivitiesResponse>
	{
	}
}
