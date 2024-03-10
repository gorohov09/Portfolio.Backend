using System.Security.Claims;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

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

			if (user.Role == null)
				throw new NotIncludedException(nameof(user.Role));

			var claims = new List<Claim>
			{
				new(ClaimNames.UserId, user.Id.ToString(), ClaimValueTypes.String),
				new(ClaimNames.Login, user.Login, ClaimValueTypes.String),
				new(ClaimNames.Role, user.Role.ToRoleName(), ClaimValueTypes.String),
			};

			return new ClaimsIdentity(claims);
		}
	}
}
