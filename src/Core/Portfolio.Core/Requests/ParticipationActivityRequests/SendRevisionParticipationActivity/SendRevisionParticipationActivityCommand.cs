using MediatR;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.SendRevisionParticipationActivity;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.SendRevisionParticipationActivity
{
	/// <summary>
	/// Команда на отправление участия в мероприятии на доработку
	/// </summary>
	public class SendRevisionParticipationActivityCommand : SendRevisionParticipationActivityRequest, IRequest
	{
	}
}
