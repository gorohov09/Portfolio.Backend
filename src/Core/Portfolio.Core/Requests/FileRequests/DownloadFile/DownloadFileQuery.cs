using MediatR;
using Portfolio.Contracts.Requests.FileRequests.DownloadFile;

namespace Portfolio.Core.Requests.FileRequests.DownloadFile
{
	/// <summary>
	/// Запрос на получение файла
	/// </summary>
	public class DownloadFileQuery : DownloadFileRequest, IRequest<DownloadFileResponse>
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="id">ИД файла</param>
		public DownloadFileQuery(Guid id)
			: base(id)
		{
		}
	}
}
