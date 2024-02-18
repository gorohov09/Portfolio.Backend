using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.UserRequest.GetMyUserInfo;
using Portfolio.Core.Requests.UserRequests.GetMyUserInfo;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для работы с пользователем
	/// </summary>
	public class UserController : ApiControllerBase
	{
		/// <summary>
		/// Получить свою контактную информацию
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект портфолио</returns>
		[HttpGet("MyUserInfo")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetMyUserInfoResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<GetMyUserInfoResponse> GetMyUserInfoAsync(
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new GetMyUserInfoQuery(),
				cancellationToken);
	}
}
