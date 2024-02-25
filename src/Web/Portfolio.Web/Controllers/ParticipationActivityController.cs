using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.PostParticipationActivity;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.PutParticipationActivity;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.SendRevisionParticipationActivity;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.SubmitParticipationActivity;
using Portfolio.Core.Requests.ParticipationActivityRequests.PostParticipationActivity;
using Portfolio.Core.Requests.ParticipationActivityRequests.PutParticipationActivity;
using Portfolio.Core.Requests.ParticipationActivityRequests.SendRevisionParticipationActivity;
using Portfolio.Core.Requests.ParticipationActivityRequests.SubmitParticipationActivity;
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
		[HttpPost]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostParticipationActivityResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<PostParticipationActivityResponse> CreateParticipationActivityAsync(
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new PostParticipationActivityCommand(),
				cancellationToken);

		/// <summary>
		/// Подать участие в мероприятии на рассмотрение
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPost("Submit")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task SubmitParticipationActivityAsync(
			[FromServices] IMediator mediator,
			[FromBody] SubmitParticipationActivityRequest request,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new SubmitParticipationActivityCommand
				{
					Id = request.Id,
				},
				cancellationToken);

		/// <summary>
		/// Подать участие в мероприятии на рассмотрение
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPost("SendRevision")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task SendRevisionParticipationActivityAsync(
			[FromServices] IMediator mediator,
			[FromBody] SendRevisionParticipationActivityRequest request,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new SendRevisionParticipationActivityCommand
				{
					Id = request.Id,
					Comment = request.Comment,
				},
				cancellationToken);

		/// <summary>
		/// Обновить участие в мероприятии
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPut]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task UpdateParticipationActivityAsync(
			[FromServices] IMediator mediator,
			[FromForm] PutParticipationActivityRequest request,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new PutParticipationActivityCommand
				{
					Id = request.Id,
					Date = request.Date,
					Description = request.Description,
					ActivityId = request.ActivityId,
					File = request.File,
					Result = request.Result,
				},
				cancellationToken);
	}
}
