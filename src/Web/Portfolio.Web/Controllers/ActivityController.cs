using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.Activities.GetActivitiesNames;
using Portfolio.Contracts.Requests.Activities.GetActivityById;
using Portfolio.Contracts.Requests.Activities.PostActivity;
using Portfolio.Core.Requests.Activities.GetActivitiesNames;
using Portfolio.Core.Requests.Activities.GetActivityById;
using Portfolio.Core.Requests.Activities.PostActivity;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для мероприятий
	/// </summary>
	public class ActivityController : ApiControllerBase
	{
		/// <summary>
		/// Получить мероприятие
		/// </summary>
		/// <param name="id">Идентификатор мероприятия</param>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект мероприятия</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetActivityByIdResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<GetActivityByIdResponse> GetParticipationActivityByIdAsync(
			[FromRoute] Guid id,
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new GetActivityByIdQuery
				{
					Id = id,
				},
				cancellationToken);

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

		/// <summary>
		/// Создать мероприятие
		/// </summary>
		/// <param name="request">Запрос</param>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPost]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostActivityResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<PostActivityResponse> CreateParticipationActivityAsync(
			[FromBody] PostActivityRequest request,
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new PostActivityCommand
				{
					Name = request.Name,
					Section = request.Section,
					Type = request.Type,
					Level = request.Level,
					Description = request.Description,
					Location = request.Location,
					Link = request.Link,
					StartDate = request.StartDate,
					EndDate = request.EndDate,
				},
				cancellationToken);
	}
}
