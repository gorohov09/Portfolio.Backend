using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Portfolio.Web.Hubs
{
	/// <summary>
	/// Хаб для уведомлений
	/// </summary>
	[Authorize]
	public class NotificationsHub : Hub
	{
	}
}
