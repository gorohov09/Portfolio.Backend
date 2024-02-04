using System.Security.Claims;
using Portfolio.Domain.Entities;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Фабрика ClaimsPrincipal для пользователей.
	/// </summary>
	public interface IClaimsIdentityFactory
	{
		/// <summary>
		/// Создать ClaimsIdentity по данным пользователя.
		/// </summary>
		/// <param name="user">Данные пользователя.</param>
		/// <returns>ClaimsIdentity.</returns>
		ClaimsIdentity CreateClaimsIdentity(User user);
	}
}
