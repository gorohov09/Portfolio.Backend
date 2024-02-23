using MediatR;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.SubmitParticipationActivity;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.SubmitParticipationActivity
{
	/// <summary>
	/// Команда на подачу участия в мероприятии
	/// </summary>
	public class SubmitParticipationActivityCommand : SubmitParticipationActivityRequest, IRequest
	{
	}
}
