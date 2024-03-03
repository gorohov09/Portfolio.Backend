namespace Portfolio.Web.WebSocketServices
{
	/// <summary>
	/// Middleware для парсинга токена для SignalR
	/// </summary>
	public class SignalRQueryStringAuthMiddleware
	{
		private readonly RequestDelegate _next;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="next">Следующий вызов</param>
		public SignalRQueryStringAuthMiddleware(RequestDelegate next) => _next = next;

		/// <summary>
		/// Вызов Middleware
		/// </summary>
		/// <param name="context">Http контекст</param>
		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Query.TryGetValue("authToken", out var token))
				context.Request.Headers.Add("Authorization", "Bearer " + token.First());

			await _next.Invoke(context);
		}
	}
}
