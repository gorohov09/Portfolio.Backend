using MediatR;
using Microsoft.AspNetCore.Http;
using Portfolio.Contracts.Requests.FileRequests.UploadFile;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Requests.FileRequests.UploadFile
{
	/// <summary>
	/// Команда на сохранения файла
	/// </summary>
	public class UploadFileCommand : IRequest<UploadFileResponse>
	{
		/// <summary>
		/// Прикрепленный файл
		/// </summary>
		public IFormFile File { get; set; } = default!;

		/// <summary>
		/// Бакет, куда сохраняется файл
		/// </summary>
		public Buckets Bucket { get; set; }
	}
}
