using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.Activities.GetActivitiesNames;
using Portfolio.Core.Requests.Activities.GetActivitiesNames;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для мероприятий
	/// </summary>
	public class ActivityController : ApiControllerBase
	{
		/// <summary>
		/// Получить список названий мероприятий
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект участия в мероприятии</returns>
		[HttpGet("list/names")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetActivitiesNamesResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<GetActivitiesNamesResponse> GetActivitiesNamesAsync(
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new GetActivitiesNamesQuery(),
				cancellationToken);
	}
}
