using Microsoft.AspNetCore.Http;

namespace Portfolio.Contracts.Requests.FileRequests.UploadFile
{
	/// <summary>
	/// Запрос на сохранения файла
	/// </summary>
	public class UploadFileRequest
	{
		/// <summary>
		/// Прикрепленный файл
		/// </summary>
		public IFormFile File { get; set; } = default!;
	}
}
