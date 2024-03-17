using MediatR;
using Portfolio.Contracts.Requests.NotificationRequests.MarkAsRead;

namespace Portfolio.Core.Requests.NotificationRequests.MarkAsRead
{
	/// <summary>
	/// Команда для перевода уведомлений в статус прочитанных
	/// </summary>
	public class MarkAsReadCommand : MarkAsReadRequest, IRequest
	{
	}
}
