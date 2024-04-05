namespace Portfolio.Web.Logging
{
	/// <summary>
	/// Точка входа для обработки исключений
	/// </summary>
	public static class Entry
	{
		/// <summary>
		/// Использовать обработчик исключений
		/// </summary>
		/// <param name="builder">Билдер</param>
		/// <returns>Билдер</returns>
		public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
			=> builder.UseMiddleware<ExceptionHandlingMiddleware>();
	}
}
