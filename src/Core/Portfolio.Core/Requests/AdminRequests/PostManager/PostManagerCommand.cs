using MediatR;
using Portfolio.Contracts.Requests.AdminRequests.PostManager;

namespace Portfolio.Core.Requests.AdminRequests.PostManager
{
	/// <summary>
	/// Запрос на создание менеджера
	/// </summary>
	public class PostManagerCommand : PostManagerRequest, IRequest<PostManagerResponse>
	{
	}
}
