using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Worker.Workers;

namespace Portfolio.Worker
{
	/// <summary>
	/// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
	/// </summary>
	public static class Entry
	{
		/// <summary>
		/// Добавить службы проекта с тасками по расписанию
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddHangfireWorker(this IServiceCollection services)
			=> services.AddHangfire(x => x.UseMemoryStorage());

		/// <summary>
		/// Использование Hangfire в pipeline
		/// </summary>
		/// <param name="app">app</param>
		/// <param name="hangfireOptions">Настройки hangfire</param>
		/// <returns>IApplicationBuilder</returns>
		public static IApplicationBuilder UseHangfireWorker(
			this IApplicationBuilder app,
			HangfireOptions hangfireOptions)
		{
			ArgumentNullException.ThrowIfNull(hangfireOptions);

			if (hangfireOptions.DisplayDashBoard)
				app.UseHangfireDashboard("/worker", new DashboardOptions
				{
					Authorization = new[] { new DashboardAuthorizationFilter() },
				});

			app.UseHangfireServer();

			AddJob<TestWorker>(hangfireOptions.TestCron);

			return app;
		}

		/// <summary>
		/// Добавить работу по расписанию
		/// </summary>
		/// <typeparam name="T">Тип работы</typeparam>
		/// <param name="cron">CRON-расписание</param>
		private static void AddJob<T>(string cron)
			where T : IWorker
			=> RecurringJob.AddOrUpdate<T>(
				typeof(T).FullName,
				(x) => x.RunAsync(),
				cron,
				TimeZoneInfo.Local);
	}
}
