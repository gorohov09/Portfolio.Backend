using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.AuthenticationRequests.RegisterStudent;
using Portfolio.Core.Requests.AuthenticationRequests.RegisterStudent;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для аутентификации
	/// </summary>
	public class AuthenticationController : ControllerBase
	{
		/// <summary>
		/// Зарегистрироваться студентом
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Идентификатор созданного студента</returns>
		[HttpPost("Register/Student")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(RegisterStudentResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<RegisterStudentResponse> PostFaqAsync(
			[FromServices] IMediator mediator,
			[FromBody] RegisterStudentRequest request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			return await mediator.Send(
				new RegisterStudentCommand
				{
					LastName = request.LastName,
					FirstName = request.FirstName,
					Birthday = request.Birthday,
					Login = request.Login,
					Email = request.Email,
					Password = request.Password,
					Phone = request.Phone,
				},
				cancellationToken);
		}
	}
}
