using Portfolio.Domain.Enums;

namespace Portfolio.Core.Models
{
	/// <summary>
	/// Данные о файле
	/// </summary>
	public class FileContent
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="content">Бинарные данные файла</param>
		/// <param name="fileName">Название файла</param>
		/// <param name="contentType">Тип данных файла</param>
		/// <param name="bucket">Бакет, в который сохраняется файл</param>
		/// <param name="metadata">Метаданные файла</param>
		public FileContent(
			Stream content,
			string fileName,
			string? contentType = null,
			Buckets? bucket = null)
		{
			FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
			Content = content ?? throw new ArgumentNullException(nameof(content));
			ContentType = contentType;
			Bucket = bucket;
		}

		/// <summary>
		/// Бинарные данные файла
		/// </summary>
		public Stream Content { get; private set; } = default!;

		/// <summary>
		/// Название файла
		/// </summary>
		public string FileName { get; private set; } = default!;

		/// <summary>
		/// Тип данных файла
		/// </summary>
		public string? ContentType { get; private set; }

		/// <summary>
		/// Бакет, в который сохраняется файл
		/// </summary>
		public Buckets? Bucket { get; private set; }
	}
}
