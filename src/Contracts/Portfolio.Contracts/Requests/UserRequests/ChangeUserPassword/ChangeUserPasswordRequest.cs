namespace Portfolio.Contracts.Requests.UserRequests.ChangeUserPassword
{
	/// <summary>
	/// Запрос на изменение пароля пользователя
	/// </summary>
	public class ChangeUserPasswordRequest
	{
		/// <summary>
		/// Старый паароль
		/// </summary>
		public string OldPassword { get; set; } = default!;

		/// <summary>
		/// Новый пароль
		/// </summary>
		public string NewPassword { get; set; } = default!;
	}
}
