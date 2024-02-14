using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Abstractions;
using Portfolio.Core.DelegateMethods;
using Portfolio.Core.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Core
{
	/// <summary>
	/// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
	/// </summary>
	public static class Entry
	{
		/// <summary>
		/// Добавить службы проекта с логикой
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddCore(this IServiceCollection services)
		{
			services.AddMediatR(typeof(Entry));
			services.AddMediatR(typeof(EntityBase));

			services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
			services.AddScoped<IDbSeeder, DbSeeder>();
			services.AddSingleton<IPasswordEncryptionService, PasswordEncryptionService>();
			services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();
			services.AddScoped<IClaimsIdentityFactory, ClaimsIdentityFactory>();
			services.AddSingleton<ISpecialityService, SpecialityService>();

			services.AddSpecialityMethods();

			return services;
		}
	}
}
