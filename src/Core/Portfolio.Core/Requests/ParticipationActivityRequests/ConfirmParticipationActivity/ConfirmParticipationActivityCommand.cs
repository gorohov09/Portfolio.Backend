using MediatR;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.ConfirmParticipationActivity;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.ConfirmParticipationActivity
{
	/// <summary>
	/// Команда на одобрение участия в мероприятии
	/// </summary>
	public class ConfirmParticipationActivityCommand : ConfirmParticipationActivityRequest, IRequest
	{
	}
}
