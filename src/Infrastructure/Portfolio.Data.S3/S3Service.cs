using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Extensions;
using Portfolio.Core.Models;
using Portfolio.Domain.Enums;

namespace Portfolio.Data.S3
{
	/// <summary>
	/// Сервис для взаимодействия с S3-хранилищем
	/// </summary>
	public class S3Service : IS3Service
	{
		/// <summary>
		/// Тип данных файла по умолчанию
		/// </summary>
		private const string DefaultContentType = "application/octet-stream";

		private readonly IAmazonS3 _amazonS3;
		private readonly S3Options _s3Options;
		private readonly ILogger<S3Service> _logger;

		public S3Service(
			IAmazonS3 amazonS3,
			S3Options s3Options,
			ILogger<S3Service> logger)
		{
			_amazonS3 = amazonS3;
			_s3Options = s3Options;
			_logger = logger;
		}

		/// <inheritdoc/>
		public async Task<FileContent?> GetAsync(
			string key,
			string customFileName,
			Buckets? bucket = null,
			string? filePath = null,
			CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));

			var request = new GetObjectRequest
			{
				BucketName = bucket == null ? _s3Options.DefaultBucketName : bucket.GetDescription(),
				Key = key,
			};

			try
			{
				var response = await _amazonS3.GetObjectAsync(request, cancellationToken);

				if (response?.ResponseStream == null)
					return null;

				return new FileContent(
					response.ResponseStream,
					customFileName,
					response.Headers?.ContentType ?? DefaultContentType,
					BuildBucket(response.BucketName));
			}
			catch (AmazonServiceException ex)
			{
				if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					_logger.LogWarning("Не найден файл {key} в бакете {bucket}", key, bucket.GetDescription());
					return null;
				}

				throw;
			}
		}

		/// <inheritdoc/>
		public async Task<Stream?> DownloadAsync(
			string key,
			Buckets? bucket = null,
			CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));

			var bucketName = bucket == null ? _s3Options.DefaultBucketName : bucket.GetDescription();

			try
			{
				return await _amazonS3.GetObjectStreamAsync(bucketName, key, null, cancellationToken);
			}
			catch (AmazonServiceException ex)
			{
				if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					_logger.LogWarning("Не найден файл {key} в бакете {bucket}", key, bucket.GetDescription());
					return null;
				}

				throw;
			}
		}

		/// <inheritdoc/>
		public async Task<string> UploadAsync(
			FileContent file,
			bool needAutoCloseStream = true,
			CancellationToken cancellationToken = default)
		{
			if (file?.Content == null)
				throw new ArgumentNullException(nameof(file));
			if (file?.FileName == null)
				throw new ArgumentException(nameof(file.FileName));

			var putRequest = new PutObjectRequest
			{
				BucketName = file.Bucket == null ? _s3Options.DefaultBucketName : file.Bucket.GetDescription(),
				Key = ContentKey(file.FileName),
				InputStream = file.Content,
				ContentType = string.IsNullOrWhiteSpace(file.ContentType) ? DefaultContentType : file.ContentType,
				AutoCloseStream = needAutoCloseStream,
			};

			await _amazonS3.PutObjectAsync(putRequest, cancellationToken);
			return putRequest.Key;
		}

		/// <inheritdoc/>
		public async Task DeleteAsync(
			string key,
			Buckets? bucket = null,
			CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));

			var bucketName = bucket == null ? _s3Options.DefaultBucketName : bucket.GetDescription();

			var request = new DeleteObjectRequest
			{
				BucketName = bucketName,
				Key = key,
			};

			await _amazonS3.DeleteObjectAsync(request, cancellationToken);
		}

		private static string ContentKey(string? fileName)
			=> $"{DateTime.UtcNow:yyyy-MM-dd}/{Guid.NewGuid()}{Path.GetExtension(fileName)}";

		private static Buckets BuildBucket(string bucketName) => bucketName switch
		{
			"Avatars" => Buckets.Avatars,
			_ => throw new NotImplementedException(),
		};
	}
}
