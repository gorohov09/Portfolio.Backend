using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.NotificationRequests.GetNotificationList;
using Portfolio.Contracts.Requests.NotificationRequests.MarkAsRead;
using Portfolio.Core.Requests.NotificationRequests.GetNotificationList;
using Portfolio.Core.Requests.NotificationRequests.MarkAsRead;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для работы с уведомлениями
	/// </summary>
	public class NotificationController : ApiControllerBase
	{
		/// <summary>
		/// Получить список уведомлений
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Список уведомлений пользователя</returns>
		[HttpGet("list")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetNotificationListResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<GetNotificationListResponse> GetNotificationsAsync(
			bool? isRead,
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new GetNotificationListQuery()
				{
					IsRead = isRead,
				},
				cancellationToken);

		/// <summary>
		/// Пометить сообщения прочитанными
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPost("MarkAsRead")]
		[SwaggerResponse(StatusCodes.Status204NoContent)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<NoContentResult> MarkAsReadAsync(
			[FromServices] IMediator mediator,
			[FromBody] MarkAsReadRequest request,
			CancellationToken cancellationToken)
		{
			await mediator.Send(
				new MarkAsReadCommand
				{
					Ids = request.Ids,
				},
				cancellationToken);

			return NoContent();
		}
	}
}
