using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio;
using Portfolio.Core.Requests.PortfolioRequests.GetMyPortfolio;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для портфолио
	/// </summary>
	public class PortfolioController : ApiControllerBase
	{
		/// <summary>
		/// Получить свое портфолио
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект портфолио</returns>
		[HttpGet("MyPortfolio")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetMyPortfolioResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<GetMyPortfolioResponse> GetMyPortfolioAsync(
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new GetMyPortfolioQuery(),
				cancellationToken);
	}
}
