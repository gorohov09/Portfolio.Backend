using System.ComponentModel.DataAnnotations;
using Portfolio.Domain.Abstractions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Файл
	/// </summary>
	public class File : EntityBase, ISoftDeletable
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="address">Адрес в S3</param>
		/// <param name="name">Название файла</param>
		/// <param name="size">Размер файла в байтах</param>
		/// <param name="mimeType">Тип файла</param>
		public File(
			string address,
			string name,
			long size,
			string? mimeType = null)
		{
			if (string.IsNullOrWhiteSpace(address))
				throw new ValidationException("Не задан адрес файла в S3-хранилище");

			if (string.IsNullOrWhiteSpace(name))
				throw new ValidationException("Не задано название файла");

			if (size <= 0)
				throw new ValidationException($"Некорректный размер файла в байтах: {size}");

			Address = address;
			FileName = name;
			Size = size;
			ContentType = mimeType;
		}

		protected File()
		{
		}

		/// <summary>
		/// Идентификатор для S3-хранилища
		/// </summary>
		public string Address { get; private set; } = default!;

		/// <summary>
		/// Название файла
		/// </summary>
		public string FileName { get; set; } = default!;

		/// <summary>
		/// Размер файла в байтах
		/// </summary>
		public long Size { get; private set; }

		/// <summary>
		/// Mime-тип
		/// </summary>
		public string? ContentType { get; private set; }

		/// <inheritdoc/>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Расширение файла
		/// </summary>
		/// <example>Extension("SomeFileName.pdf") возвратит "pdf"</example>
		public string? Extension => Path.GetExtension(FileName)?.Trim('.').ToLowerInvariant();

		#region Navigation properties

		/// <summary>
		/// Фотографии пользователей
		/// </summary>
		public List<Photo>? Photos { get; protected set; }

		#endregion
	}
}
