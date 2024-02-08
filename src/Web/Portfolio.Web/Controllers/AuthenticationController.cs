using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.AuthenticationRequests.Login;
using Portfolio.Contracts.Requests.AuthenticationRequests.RegisterStudent;
using Portfolio.Core.Requests.AuthenticationRequests.Login;
using Portfolio.Core.Requests.AuthenticationRequests.RegisterStudent;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для аутентификации
	/// </summary>
	[AllowAnonymous]
	public class AuthenticationController : ApiControllerBase
	{
		/// <summary>
		/// Логин
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект логина</returns>
		[HttpPost("Login")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(LoginResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<LoginResponse> LoginAsync(
			[FromServices] IMediator mediator,
			[FromBody] LoginRequest request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			return await mediator.Send(
				new LoginQuery
				{
					Login = request.Login,
					Password = request.Password,
				},
				cancellationToken);
		}

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
		public async Task<RegisterStudentResponse> PostRegisterAsync(
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
