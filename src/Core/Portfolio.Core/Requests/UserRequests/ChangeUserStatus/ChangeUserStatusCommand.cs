using MediatR;
using Portfolio.Contracts.Requests.UserRequests.ChangeUserStatus;

namespace Portfolio.Core.Requests.UserRequests.ChangeUserStatus
{
	/// <summary>
	/// Комманда на изменение статуса пользователя
	/// </summary>
	public class ChangeUserStatusCommand : ChangeUserStatusRequest, IRequest
	{
	}
}
