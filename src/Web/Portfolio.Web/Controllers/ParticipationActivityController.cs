using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.PostParticipationActivity;
using Portfolio.Core.Requests.ParticipationActivityRequests.PostParticipationActivity;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для работы с участими в мероприятиях
	/// </summary>
	public class ParticipationActivityController : ApiControllerBase
	{
		/// <summary>
		/// Создать участие в мероприятии
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPost("Create")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostParticipationActivityResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task CreateParticipationActivityAsync(
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new PostParticipationActivityCommand(),
				cancellationToken);
	}
}
