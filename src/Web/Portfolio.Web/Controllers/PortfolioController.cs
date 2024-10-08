using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.PortfolioRequests.AddGeneralInformation;
using Portfolio.Contracts.Requests.PortfolioRequests.AddOrUpdateEducationInformation;
using Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolio;
using Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolioList;
using Portfolio.Core.Requests.PortfolioRequests.AddOrUpdateEducationInformation;
using Portfolio.Core.Requests.PortfolioRequests.AddOrUpdateGeneralInformation;
using Portfolio.Core.Requests.PortfolioRequests.AddPhoto;
using Portfolio.Core.Requests.PortfolioRequests.GetPortfolio;
using Portfolio.Core.Requests.PortfolioRequests.GetPortfolioList;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для портфолио
	/// </summary>
	public class PortfolioController : ApiControllerBase
	{
		/// <summary>
		/// Получить список портфолио
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Список портфолио</returns>
		[HttpPost("list")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetPortfolioListResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<GetPortfolioListResponse> GetPortfolioListAsync(
			[FromBody] GetPortfolioListRequest request,
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new GetPortfolioListQuery
				{
					PageNumber = request.PageNumber,
					PageSize = request.PageSize,
					LastName = request.LastName,
					FirstName = request.FirstName,
					Surname = request.Surname,
					Faculty = request.Faculty,
					Institute = request.Institute,
				},
				cancellationToken);

		/// <summary>
		/// Получить свое портфолио
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект портфолио</returns>
		[HttpGet("MyPortfolio/{portfolioId?}")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetPortfolioResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<GetPortfolioResponse> GetMyPortfolioAsync(
			[FromRoute] Guid? portfolioId,
			[FromServices] IMediator mediator,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new GetPortfolioQuery()
				{
					Id = portfolioId,
				},
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
			[FromBody] AddOrUpdateEducationInformationRequest request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await mediator.Send(
				new AddOrUpdateEducationInformationCommand
				{
					EducationLevel = request.EducationLevel,
					FacultyId = request.FacultyId,
					GroupNumber = request.GroupNumber,
					SpecialityNumber = request.SpecialityNumber,
				},
				cancellationToken);
		}

		/// <summary>
		/// Добавить в портфолио общую информацию
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPut("Add/General")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task AddGeneralEducationInformationAsync(
			[FromServices] IMediator mediator,
			[FromBody] AddOrUpdateGeneralInformationRequest request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await mediator.Send(
				new AddOrUpdateGeneralInformationCommand
				{
					LastName = request.LastName,
					FirstName = request.FirstName,
					Surname = request.Surname,
					Birthday = request.Birthday,
				},
				cancellationToken);
		}

		/// <summary>
		/// Добавить в портфолио фотографию
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="file">Файл</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPut("Add/Photo")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task AddPhotoAsync(
			[FromServices] IMediator mediator,
			IFormFile file,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new AddPhotoCommand
				{
					File = file,
				},
				cancellationToken);
	}
}
