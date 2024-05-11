using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.AdminRequests.PostManager;
using Portfolio.Core.Requests.AdminRequests.PostManager;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для администратора
	/// </summary>
	public class AdminController : ApiControllerBase
	{
		/// <summary>
		/// Создать администратора
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект администратора</returns>
		[HttpPost("createManager")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostManagerResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<PostManagerResponse> GetActivitiesAsync(
			[FromBody] PostManagerRequest request,
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new PostManagerCommand
				{
					LastName = request.LastName,
					FirstName = request.FirstName,
					Email = request.Email,
					Login = request.Login,
					Password = request.Password,
				},
				cancellationToken);
	}
}
