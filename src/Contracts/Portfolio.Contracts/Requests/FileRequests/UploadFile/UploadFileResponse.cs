namespace Portfolio.Contracts.Requests.FileRequests.UploadFile
{
	/// <summary>
	/// Ответ на запрос <see cref="UploadFileRequest"/>
	/// </summary>
	public class UploadFileResponse
	{
		/// <summary>
		/// Id файла
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Имя файла
		/// </summary>
		public string Name { get; set; } = default!;
	}
}
