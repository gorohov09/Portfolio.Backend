using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для портфолио
	/// </summary>
	[Authorize]
	public class PortfolioController : ControllerBase
	{
		/// <summary>
		/// Получить свое портфолио
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект портфолио</returns>
		[HttpGet("Portfolio")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<string> GetMyPortfolioAsync(
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
		{
			return "Port";
		}
	}
}
