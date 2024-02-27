using Portfolio.Core.Abstractions;

namespace Portfolio.Web.WebSocketServices
{
	/// <summary>
	/// Точка входа сигналера для уведомлений
	/// </summary>
	public static class Entry
	{
		/// <summary>
		/// Добавить службы SignalR
		/// </summary>
		/// <param name="services">Коллекция сервисов</param>
		/// <returns>Дополненная коллекция</returns>
		public static IServiceCollection AddSignaler(this IServiceCollection services)
			=> services
				.AddSignalR()
				.Services
				.AddSingleton<IHubNotificationService, HubNotificationService>();

		/// <summary>
		/// Добавить в промежуточное ПО парсинг токена для SignalR
		/// </summary>
		/// <param name="builder">Билдер</param>
		public static IApplicationBuilder UseSignalRQueryStringAuth(this IApplicationBuilder builder)
			=> builder.UseMiddleware<SignalRQueryStringAuthMiddleware>();
	}
}
