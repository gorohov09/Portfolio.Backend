using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Contracts.Requests.FileRequests.UploadFile;
using Portfolio.Core.Requests.FileRequests.DownloadFile;
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
		[RequestSizeLimit(Limit)]
		[RequestFormLimits(MultipartBodyLengthLimit = Limit)]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(UploadFileResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
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

		/// <summary>
		/// Скачать файл из хранилища
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="id">ИД файла для скачивания</param>
		/// <param name="cancellationToken">Токен отмены запроса</param>
		/// <param name="inline">Если TRUE, то заголово ответа Content-Disposition=inline, иначе attachment</param>
		/// <returns>Файл</returns>
		[HttpGet("{id}/Download")]
		[SwaggerResponse(StatusCodes.Status200OK, type: typeof(FileStreamResult))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
		public async Task<FileStreamResult> DownloadAsync(
			[FromServices] IMediator mediator,
			[FromRoute] Guid id,
			CancellationToken cancellationToken,
			[FromQuery] bool inline = false)
		{
			var file = await mediator.Send(new DownloadFileQuery(id), cancellationToken);

			return GetFileStreamResult(file, Response.Headers, inline);
		}
	}
}
