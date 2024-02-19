using Portfolio.Core.Models;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис для взаимодействия с S3-хранилищем
	/// </summary>
	public interface IS3Service
	{
		/// <summary>
		/// Загрузить файл в хранилище
		/// </summary>
		/// <param name="file">Файл для сохранения в S3</param>
		/// <param name="needAutoCloseStream">Нужно автоматическое закрытие потока после загрузки</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>ИД файла в хранилище</returns>
		Task<string> UploadAsync(
			FileContent file,
			bool needAutoCloseStream = true,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Получить файл с его метаданными по ключу
		/// </summary>
		/// <param name="key">Ключ файла</param>
		/// <param name="bucket">Бакет, если отличается от умолчания</param>
		/// <param name="customFileName">Кастомное наименование файла</param>
		/// <param name="filePath">Папка файла</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Данные о файле. Если запрашиваемый файл не найден, то NULL</returns>
		Task<FileContent?> GetAsync(
			string key,
			string customFileName,
			Buckets? bucket = null,
			string? filePath = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Получить файл из хранилища в виде потока
		/// </summary>
		/// <param name="key">Ключ, под которым файл был сохранен в хранилище</param>
		/// <param name="bucket">Название бакета</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Поток с файлом. Если запрашиваемый файл не найден, то NULL</returns>
		Task<Stream?> DownloadAsync(
			string key,
			Buckets? bucket = null,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Удалить файл из хранилища
		/// </summary>
		/// <param name="key">Ключ, под которым файл был сохранен в хранилище</param>
		/// <param name="bucket">Название бакета</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		Task DeleteAsync(
			string key,
			Buckets? bucket = null,
			CancellationToken cancellationToken = default);
	}
}
