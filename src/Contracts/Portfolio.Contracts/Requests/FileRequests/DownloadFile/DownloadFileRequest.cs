namespace Portfolio.Contracts.Requests.FileRequests.DownloadFile
{
	/// <summary>
	/// Запрос на получение файла
	/// </summary>
	public class DownloadFileRequest
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="id">Идентификатор файла</param>
		public DownloadFileRequest(Guid id) => Id = id;

		/// <summary>
		/// Идентификатор файла
		/// </summary>
		public Guid Id { get; set; } = default!;
	}
}
