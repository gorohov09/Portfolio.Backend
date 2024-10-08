namespace Portfolio.Contracts.Requests.AuthenticationRequests.Login
{
	/// <summary>
	/// Ответ на запрос <see cref="LoginRequest"/>
	/// </summary>
	public class LoginResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="userId">Id пользователя</param>
		/// <param name="token">Токен авторизации</param>
		public LoginResponse(
			Guid userId,
			string role,
			string token)
		{
			UserId = userId;
			Role = role;
			Token = token;
		}

		/// <summary>
		/// Id пользователя
		/// </summary>
		public Guid UserId { get; }

		/// <summary>
		/// ФИО
		/// </summary>
		public string FullName { get; }

		/// <summary>
		/// Роль пользователя
		/// </summary>
		public string Role { get; } = default!;

		/// <summary>
		/// Токен авторизации
		/// </summary>
		public string Token { get; } = default!;
	}
}
