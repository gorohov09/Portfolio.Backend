using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.UserRequests.AddOrUpdateUserInfo;
using Portfolio.Contracts.Requests.UserRequests.GetMyUserInfo;
using Portfolio.Core.Requests.UserRequests.AddOrUpdateUserInfo;
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

		/// <summary>
		/// Добавить/обновить контактную информацию о пользователе
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPut("AddOrUpdate/UserInfo")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task AddOrUpdateUserInfoAsync(
			[FromServices] IMediator mediator,
			[FromBody] AddOrUpdateUserInfoRequest request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await mediator.Send(
				new AddOrUpdateUserInfoCommand
				{
					Login = request.Login,
					Email = request.Email,
					Phone = request.Phone,
				},
				cancellationToken);
		}
	}
}
