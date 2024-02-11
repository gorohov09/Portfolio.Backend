using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.PortfolioRequests.AddEducationInformation;
using Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio;
using Portfolio.Core.Requests.PortfolioRequests.AddEducationInformation;
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

		/// <summary>
		/// Добавить в портфолио информацию о получении образования
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPut("Add/Education")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task AddEducationInformationAsync(
			[FromServices] IMediator mediator,
			[FromBody] AddEducationInformationRequest request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await mediator.Send(
				new AddEducationInformationCommand
				{
					EducationLevel = request.EducationLevel,
					FacultyId = request.FacultyId,
					GroupNumber = request.GroupNumber,
					SpecialityNumber = request.SpecialityNumber,
				},
				cancellationToken);
		}
	}
}
