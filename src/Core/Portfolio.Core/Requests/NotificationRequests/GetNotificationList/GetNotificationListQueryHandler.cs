using MediatR;
using Portfolio.Contracts.Requests.NotificationRequests.GetNotificationList;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;

namespace Portfolio.Core.Requests.NotificationRequests.GetNotificationList
{
	/// <summary>
	/// Обработчик запроса <see cref="GetNotificationListQuery"/>
	/// </summary>
	public class GetNotificationListQueryHandler
		: IRequestHandler<GetNotificationListQuery, GetNotificationListResponse>
	{
		private readonly IUserContext _userContext;
		private readonly INotificationService _notificationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="notificationService">Сервис рассылки уведомлений</param>
		public GetNotificationListQueryHandler(
			IUserContext userContext,
			INotificationService notificationService)
		{
			_userContext = userContext;
			_notificationService = notificationService;
		}

		/// <inheritdoc/>
		public async Task<GetNotificationListResponse> Handle(
			GetNotificationListQuery request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var notifications = await _notificationService.GetNotificationsAsync(
				request.IsRead.HasValue ? new NotificationFilterModel { IsRead = request.IsRead } : null,
				_userContext.CurrentUserId,
				cancellationToken);

			var notificationsModel = notifications.Select(x => new GetNotificationListResponseItem
			{
				Id = x.Id,
				ToUserId = x.UserId,
				Type = x.Type,
				Title = x.Title,
				Description = x.Description,
				IsRead = x.IsRead,
				CreationDate = x.CreatedOn,
				UpdateDate = x.ModifiedOn,
			})
			.ToList();

			return new GetNotificationListResponse(
				entities: notificationsModel,
				totalCount: notificationsModel.Count);
		}
	}
}
