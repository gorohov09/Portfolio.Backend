namespace Portfolio.Contracts.Requests.AdminRequests.PostManager
{
	/// <summary>
	/// Запрос на создание менеджера
	/// </summary>
	public class PostManagerRequest
	{
		/// <summary>
		/// Логин
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; } = default!;

		/// <summary>
		/// Электронная почта
		/// </summary>
		public string Email { get; set; } = default!;

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName { get; set; } = default!;

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName { get; set; } = default!;
	}
}
