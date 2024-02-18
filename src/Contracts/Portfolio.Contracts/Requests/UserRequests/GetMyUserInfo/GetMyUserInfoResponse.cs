namespace Portfolio.Contracts.Requests.UserRequests.GetMyUserInfo
{
	/// <summary>
	/// Ответ на запрос получения информации о пользователе
	/// </summary>
	public class GetMyUserInfoResponse
	{
		/// <summary>
		/// Логин
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// Почтовый адрес
		/// </summary>
		public string Email { get; set; } = default!;

		/// <summary>
		/// Наименование роли
		/// </summary>
		public string RoleName { get; set; } = default!;

		/// <summary>
		/// Номер телефона
		/// </summary>
		public string? Phone { get; set; }

		/// <summary>
		/// Дата регистрации
		/// </summary>
		public DateTime CreatedOn { get; set; }
	}
}
