namespace Portfolio.Contracts.Requests.UserRequests.ChangeUserPassword
{
	/// <summary>
	/// Запрос на изменение пароля пользователя
	/// </summary>
	public class ChangeUserPasswordRequest
	{
		public string OldPassword { get; set; } = default!;

		public string NewPassword { get; set; } = default!;
	}
}
