using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Contracts.Requests.UserRequests.ChangeUserStatus
{
	/// <summary>
	/// Запрос на изменения статуса пользователя
	/// </summary>
	public class ChangeUserStatusRequest
	{
		public Guid UserId {  get; set; }
		public Guid CurrentId {  get; set; }
	}
}
