using MediatR;
using Portfolio.Contracts.Requests.Activities.GetActivitiesNames;

namespace Portfolio.Core.Requests.Activities.GetActivitiesNames
{
	/// <summary>
	/// Запрос на получение списка названий мероприятий
	/// </summary>
	public class GetActivitiesNamesQuery : IRequest<GetActivitiesNamesResponse>
	{
	}
}
