using MediatR;
using Portfolio.Contracts.Requests.Activities.GetActivities;
using Portfolio.Core.Models;

namespace Portfolio.Core.Requests.Activities.GetActivities
{
	/// <summary>
	/// Запрос на получение списка мероприятий
	/// </summary>
	public class GetActivitiesQuery : GetActivitiesRequest, IRequest<GetActivitiesResponse>, IFilterActivity
	{
	}
}
