using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Core.DelegateMethods
{
	/// <summary>
	/// Методы делегатов класса специальности
	/// </summary>
	public static class SpecialityDelegateMethods
	{
		/// <summary>
		/// Добавить методы для делегатов специальности
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddSpecialityMethods(this IServiceCollection services)
		{
			services.AddScoped<Speciality.GetSpecialityNameByNumber>(
				sp =>
					(number) =>
						sp.GetRequiredService<ISpecialityService>()
							.GetNameByNumber(number));

			services.AddScoped<Speciality.SatisfySpecialityLevel>(
				sp =>
					(number, level) =>
						sp.GetRequiredService<ISpecialityService>()
							.SatisfySpecialityLevel(number, level));

			return services;
		}
	}
}
