using System.Security.Claims;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Фабрика ClaimsPrincipal для пользователей.
	/// </summary>
	public class ClaimsIdentityFactory : IClaimsIdentityFactory
	{
		/// <inheritdoc/>
		public ClaimsIdentity CreateClaimsIdentity(User user)
		{
			ArgumentNullException.ThrowIfNull(user);

			var claims = new List<Claim>
			{
				new(ClaimNames.UserId, user.Id.ToString(), ClaimValueTypes.String),
				new(ClaimNames.Login, user.Login, ClaimValueTypes.String),
			};

			return new ClaimsIdentity(claims);
		}
	}
}
