using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Web.Logging
{
	/// <summary>
	/// Обработчик ошибок API
	/// </summary>
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		/// <summary>
		/// Обработчик ошибок API
		/// </summary>
		/// <param name="next">Делегат следующего обработчика в пайплайне ASP.NET Core</param>
		/// <param name="logger">Логгер</param>
		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		/// <summary>
		/// Действия по обработке запроса ASP.NET
		/// </summary>
		/// <param name="context">Контекст запроса ASP.NET</param>
		/// <returns>Задача на обработку запроса ASP.NET</returns>
#pragma warning disable VSTHRD200
		public async Task Invoke(HttpContext context)
#pragma warning restore VSTHRD200
		{
			try
			{
				await _next(context);
			}
			catch (NotFoundException ex)
			{
				await HandleNotFoundExceptionAsync(context, ex);
			}
			catch (ApplicationExceptionBase ex)
			{
				await HandleApplicationExceptionBaseAsync(context, ex);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		/// <summary>
		/// Обработка исключения <see cref="NotFoundException"/>
		/// </summary>
		/// <param name="context">Контекст запроса ASP.NET</param>
		/// <param name="exception">Исключение</param>
		/// <returns>Задача на обработку запроса ASP.NET</returns>
		private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
		{
			var errorText = exception.Message;
			var logLevel = LogLevel.Warning;
			var responseCode = HttpStatusCode.NotFound;
			await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
		}

		/// <summary>
		/// Обработка исключения <see cref="ApplicationExceptionBase"/>
		/// </summary>
		/// <param name="context">Контекст запроса ASP.NET</param>
		/// <param name="exception">Исключение</param>
		/// <returns>Задача на обработку запроса ASP.NET</returns>
		private async Task HandleApplicationExceptionBaseAsync(HttpContext context, ApplicationExceptionBase exception)
		{
			var errorText = exception.Message;
			var logLevel = LogLevel.Warning;
			var responseCode = HttpStatusCode.BadRequest;
			await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
		}

		/// <summary>
		/// Обработка исключения <see cref="Exception"/>
		/// </summary>
		/// <param name="context">Контекст запроса ASP.NET</param>
		/// <param name="exception">Исключение</param>
		/// <returns>Задача на обработку запроса ASP.NET</returns>
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var errorText = exception.Message;
			var logLevel = LogLevel.Error;
			var responseCode = HttpStatusCode.InternalServerError;
			await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
		}

		private async Task LogAndReturnAsync(
			HttpContext context,
			Exception exception,
			string errorText,
			HttpStatusCode responseCode,
			LogLevel logLevel,
			Dictionary<string, object>? details = null)
		{
			var errorId = Guid.NewGuid().ToString();
			details ??= new Dictionary<string, object>();
			details.Add("errorId", errorId);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)responseCode;

#pragma warning disable CA1848
			_logger.Log(logLevel, exception, $"Error #{errorId}: {exception}");
#pragma warning restore CA1848

			var response = new ProblemDetails
			{
				Title = errorText,
				Instance = null,
				Status = (int)responseCode,
				Type = null,
				Detail = errorText,
			};
			response.Extensions.Add("ErrorId", errorId);

			foreach (var detail in details)
				response.Extensions.Add(detail.Key, detail.Value);

			var jsonOptions = context.RequestServices.GetRequiredService<IOptions<JsonOptions>>();
			await context.Response.WriteAsync(
				JsonSerializer.Serialize(response, jsonOptions.Value.JsonSerializerOptions));
		}
	}
}
