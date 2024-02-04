using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Abstractions;

namespace Portfolio.Data.PostgreSql
{
	/// <summary>
	/// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
	/// </summary>
	public static class Entry
	{
		/// <summary>
		/// Добавить службы проекта с Postgresql
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <param name="optionsAction">Параметры подключения к Postgresql</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddPostgreSql(
			this IServiceCollection services,
			Action<PostgresDbOptions>? optionsAction)
		{
			var options = new PostgresDbOptions();
			optionsAction?.Invoke(options);

			return services.AddPostgreSql(options);
		}

		/// <summary>
		/// Добавить службы проекта с Postgresql
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <param name="options">Конфиг подключения к Postgresql</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddPostgreSql(
			this IServiceCollection services,
			PostgresDbOptions options)
		{
			ArgumentNullException.ThrowIfNull(options);

			if (string.IsNullOrWhiteSpace(options.ConnectionString))
				throw new ArgumentException(nameof(options.ConnectionString));

			services.AddDbContext<EfContext>(opt =>
			{
				opt.UseSnakeCaseNamingConvention();
				opt.UseNpgsql(options!.ConnectionString);
				opt.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
			});

			services.AddScoped<IDbContext, EfContext>();

			return services;
		}
	}
}
