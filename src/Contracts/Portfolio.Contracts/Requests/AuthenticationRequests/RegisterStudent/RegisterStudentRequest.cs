namespace Portfolio.Contracts.Requests.AuthenticationRequests.RegisterStudent
{
	/// <summary>
	/// Запрос на регистрацию студента
	/// </summary>
	public class RegisterStudentRequest
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

		/// <summary>
		/// Номер зачетной книжки
		/// </summary>
		public string CardNumber { get; set; } = default!;
	}
}
