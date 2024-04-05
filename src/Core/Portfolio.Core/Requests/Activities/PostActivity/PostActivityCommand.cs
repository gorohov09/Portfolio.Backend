using MediatR;
using Portfolio.Contracts.Requests.Activities.PostActivity;

namespace Portfolio.Core.Requests.Activities.PostActivity
{
	/// <summary>
	/// Запрос на создание мероприятия
	/// </summary>
	public class PostActivityCommand : PostActivityRequest, IRequest<PostActivityResponse>
	{
	}
}
