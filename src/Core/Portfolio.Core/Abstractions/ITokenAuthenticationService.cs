using System.Security.Claims;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис работы с токенами.
	/// </summary>
	public interface ITokenAuthenticationService
	{
		/// <summary>
		/// Создать токен для пользователя с клаймами.
		/// </summary>
		/// <param name="identity">Пользователь с клаймами.</param>
		/// <param name="tokenType">Тип токена.</param>
		/// <returns>Токен для пользователя с клаймами.</returns>
		string CreateToken(ClaimsIdentity identity, TokenTypes tokenType);
	}
}
