namespace Portfolio.Contracts.Requests.UserRequests.AddOrUpdateUserInfo
{
	/// <summary>
	/// Запрос на добавление/обновление контактных данных пользователя
	/// </summary>
	public class AddOrUpdateUserInfoRequest
	{
		/// <summary>
		/// Логин пользователя
		/// </summary>
		public string? Login { get; set; }

		/// <summary>
		/// Почтовый адрес
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Номер телефона
		/// </summary>
		public string? Phone { get; set; }
	}
}
