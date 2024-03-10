using System.Security.Claims;

namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Названия клеймов
	/// </summary>
	public static class ClaimNames
	{
		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		public const string UserId = ClaimTypes.NameIdentifier;

		/// <summary>
		/// Логин
		/// </summary>
		public const string Login = "Login";

		/// <summary>
		/// Роль
		/// </summary>
		public const string Role = ClaimTypes.Role;
	}
}
