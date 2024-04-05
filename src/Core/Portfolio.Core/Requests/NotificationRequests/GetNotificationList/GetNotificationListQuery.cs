using MediatR;
using Portfolio.Contracts.Requests.NotificationRequests.GetNotificationList;

namespace Portfolio.Core.Requests.NotificationRequests.GetNotificationList
{
	/// <summary>
	/// Запрос на получение уведомлений конкретного пользователя
	/// </summary>
	public class GetNotificationListQuery : IRequest<GetNotificationListResponse>
	{
		/// <summary>
		/// Флаг для фильтра получаемых уведомлений
		/// </summary>
		public bool? IsRead { get; set; }
	}
}
