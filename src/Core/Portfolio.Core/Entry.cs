using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Services;

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
			services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

			return services;
		}
	}
}
