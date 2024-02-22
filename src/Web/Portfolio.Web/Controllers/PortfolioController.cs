using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.PortfolioRequests.AddGeneralInformation;
using Portfolio.Contracts.Requests.PortfolioRequests.AddOrUpdateEducationInformation;
using Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio;
using Portfolio.Core.Requests.PortfolioRequests.AddOrUpdateEducationInformation;
using Portfolio.Core.Requests.PortfolioRequests.AddOrUpdateGeneralInformation;
using Portfolio.Core.Requests.PortfolioRequests.AddPhoto;
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
		/// Добавить/обновить информацию в портфолио о получении образования
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPut("Add/Education")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task AddOrUpdateEducationInformationAsync(
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
		/// Добавить/обновить общую информацию в портфолио
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		[HttpPut("Add/General")]
		[SwaggerResponse(StatusCodes.Status200OK)]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task AddOrUpdateGeneralEducationInformationAsync(
			[FromServices] IMediator mediator,
			[FromBody] AddGeneralInformationRequest request,
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
