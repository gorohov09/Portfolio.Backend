using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Portfolio.Contracts.Requests;

namespace Portfolio.Web.Controllers
{
	/// <summary>
	/// Базовый API-контроллер
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ApiControllerBase : ControllerBase
	{
		private const string DefaultContentType = "application/octet-stream";
		private const string DefaultContentDisposition = "attachment";

		/// <summary>
		/// Получить поток файл для ответа
		/// </summary>
		/// <param name="file">Базовый ответ файла</param>
		/// <param name="headers">Заголовки</param>
		/// <param name="inline">Если TRUE, то заголово ответа Content-Disposition=inline, иначе attachment</param>
		/// <returns>Поток файла для ответа</returns>
		protected static FileStreamResult GetFileStreamResult(
			BaseFileStreamResponse file,
			IHeaderDictionary headers,
			bool inline = false)
		{
			ArgumentNullException.ThrowIfNull(file);

			var cd = new ContentDispositionHeaderValue(inline ? "inline" : DefaultContentDisposition);
			cd.SetHttpFileName(file.FileName);
			headers[HeaderNames.ContentDisposition] = cd.ToString();

			if (file.Content.CanSeek)
				file.Content.Seek(0, SeekOrigin.Begin);

			return new FileStreamResult(file.Content, file.ContentType ?? DefaultContentType);
		}
	}
}
