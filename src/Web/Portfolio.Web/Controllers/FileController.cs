using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.FileRequests.UploadFile;
using Portfolio.Core.Requests.FileRequests.UploadFile;
using Portfolio.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Контроллер для работы с файлами
	/// </summary>
	public class FileController : ApiControllerBase
	{
		/// <summary>
		/// Лимит - 512 MB
		/// </summary>
		private const int Limit = 536870912;

		/// <summary>
		/// Загрузить файлы в хранилище
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос для загрузки файла</param>
		/// <param name="cancellationToken">Токен отмены запроса</param>
		/// <returns>Список ключей загруженных файлов</returns>
		[HttpPost("{bucket}")]
		//[RequestSizeLimit(Limit)]
		//[RequestFormLimits(MultipartBodyLengthLimit = Limit)]
		//[SwaggerResponse(StatusCodes.Status200OK, type: typeof(UploadFileResponse))]
		//[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<UploadFileResponse> UploadAsync(
			Buckets bucket,
			[FromServices] IMediator mediator,
			[FromForm] UploadFileRequest request,
			CancellationToken cancellationToken)
			=> await mediator.Send(
				new UploadFileCommand
				{
					File = request.File,
					Bucket = bucket,
				},
				cancellationToken: cancellationToken);
	}
}
