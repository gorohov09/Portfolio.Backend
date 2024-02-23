using MediatR;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.PostParticipationActivity;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.PostParticipationActivity
{
	/// <summary>
	/// Команда на создание участия в мероприятии
	/// </summary>
	public class PostParticipationActivityCommand : IRequest<PostParticipationActivityResponse>
	{
	}
}
