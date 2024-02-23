using MediatR;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.PutParticipationActivity;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.PutParticipationActivity
{
	/// <summary>
	/// Команда на обновление участия в мероприятии
	/// </summary>
	public class PutParticipationActivityCommand : PutParticipationActivityRequest, IRequest
	{
	}
}
