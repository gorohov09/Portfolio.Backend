using MediatR;
using Portfolio.Contracts.Requests.AuthenticationRequests.Login;

namespace Portfolio.Core.Requests.AuthenticationRequests.Login
{
	/// <summary>
	/// Запрос на логин
	/// </summary>
	public class LoginQuery : LoginRequest, IRequest<LoginResponse>
	{
	}
}
