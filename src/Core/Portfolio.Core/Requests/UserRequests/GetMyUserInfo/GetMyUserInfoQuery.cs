using MediatR;
using Portfolio.Contracts.Requests.UserRequest.GetMyUserInfo;

namespace Portfolio.Core.Requests.UserRequests.GetMyUserInfo
{
	/// <summary>
	/// Запрос на получение информации о пользователе
	/// </summary>
	public class GetMyUserInfoQuery : IRequest<GetMyUserInfoResponse>
	{
	}
}
