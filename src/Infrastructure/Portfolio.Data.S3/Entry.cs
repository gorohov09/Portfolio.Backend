using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Abstractions;

namespace Portfolio.Data.S3
{
	public static class Entry
	{
		/// <summary>
		/// Добавить хранилище файлов на протоколе S3
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <param name="options">Параметры подключения к хранилищу</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddS3Storage(this IServiceCollection services, S3Options options)
		{
			ArgumentNullException.ThrowIfNull(options);

			if (string.IsNullOrWhiteSpace(options.AccessKey))
				throw new ArgumentException(nameof(options.AccessKey));
			if (string.IsNullOrWhiteSpace(options.DefaultBucketName))
				throw new ArgumentException(nameof(options.DefaultBucketName));
			if (string.IsNullOrWhiteSpace(options.SecretKey))
				throw new ArgumentException(nameof(options.SecretKey));
			if (string.IsNullOrWhiteSpace(options.ServiceUrl))
				throw new ArgumentException(nameof(options.ServiceUrl));

			services.AddSingleton(options);
			services.AddSingleton<IS3Service, S3Service>();

			services.AddSingleton<IAmazonS3, AmazonS3Client>(_ => new AmazonS3Client(
				options.AccessKey,
				options.SecretKey,
				new AmazonS3Config
				{
					ServiceURL = options.ServiceUrl,
					ForcePathStyle = true,
					UseHttp = false,
				})
			);

			return services;
		}

		/// <summary>
		/// Добавить хранилище файлов на протоколе S3
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <param name="options">Параметры подключения к хранилищу</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddS3Storage(this IServiceCollection services, Action<S3Options> options)
		{
			var storageOptions = new S3Options();
			options?.Invoke(storageOptions);

			return AddS3Storage(services, storageOptions);
		}
	}
}
