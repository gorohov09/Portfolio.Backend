using MediatR;
using Portfolio.Contracts.Requests.UserRequests.ChangeUserPassword;

namespace Portfolio.Core.Requests.UserRequests.ChangeUserPassword
{
	/// <summary>
	/// Команда на изменение пароля
	/// </summary>
	public class ChangeUserPasswordCommand : ChangeUserPasswordRequest, IRequest
	{
	}
}
