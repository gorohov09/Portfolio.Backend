namespace Portfolio.Contracts.Requests.FileRequests.DownloadFile
{
	/// <summary>
	/// Ответ на запрос <see cref="DownloadFileRequest"/>
	/// </summary>
	public class DownloadFileResponse : BaseFileStreamResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="content">Данные файла</param>
		/// <param name="contentType">Тип файла</param>
		/// <param name="fileName">Название</param>
		public DownloadFileResponse(
			Stream content,
			string? contentType,
			string? fileName)
			: base(content, contentType, fileName)
		{
		}
	}
}
