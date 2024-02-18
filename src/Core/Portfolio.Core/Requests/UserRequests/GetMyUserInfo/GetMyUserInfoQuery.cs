using MediatR;
using Portfolio.Contracts.Requests.UserRequests.GetMyUserInfo;

namespace Portfolio.Core.Requests.UserRequests.GetMyUserInfo
{
	/// <summary>
	/// Запрос на получение информации о пользователе
	/// </summary>
	public class GetMyUserInfoQuery : IRequest<GetMyUserInfoResponse>
	{
	}
}
