namespace Portfolio.Contracts.Requests.UserRequests.ChangeUserStatus
{
	/// <summary>
	/// Запрос на изменения статуса пользователя
	/// </summary>
	public class ChangeUserStatusRequest
	{
		/// <summary>
		/// Идентификатор пользователя, у которого требуется изменить статус
		/// </summary>
		public Guid UserId { get; set; }
	}
}
