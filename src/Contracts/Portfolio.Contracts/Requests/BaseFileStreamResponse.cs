namespace Portfolio.Contracts.Requests
{
	/// <summary>
	/// Базовый ответ файла c потоком
	/// </summary>
	public class BaseFileStreamResponse
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="content">Данные файла</param>
		/// <param name="contentType">Тип файла</param>
		/// <param name="fileName">Название</param>
		public BaseFileStreamResponse(
			Stream content,
			string? contentType,
			string? fileName)
		{
			Content = content ?? throw new ArgumentNullException(nameof(content));
			FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
			ContentType = contentType;
		}

		/// <summary>
		/// Данные файла
		/// </summary>
		public Stream Content { get; }

		/// <summary>
		/// Тип файла
		/// </summary>
		public string? ContentType { get; }

		/// <summary>
		/// Название
		/// </summary>
		public string FileName { get; }
	}
}
