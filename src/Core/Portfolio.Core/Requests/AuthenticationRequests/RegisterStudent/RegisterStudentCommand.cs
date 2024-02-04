using MediatR;
using Portfolio.Contracts.Requests.AuthenticationRequests.RegisterStudent;

namespace Portfolio.Core.Requests.AuthenticationRequests.RegisterStudent
{
	/// <summary>
	/// Команда на регистрацию студента
	/// </summary>
	public class RegisterStudentCommand : RegisterStudentRequest, IRequest<RegisterStudentResponse>
	{
	}
}
