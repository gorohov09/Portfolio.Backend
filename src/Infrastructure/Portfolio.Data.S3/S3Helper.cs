using Amazon.S3;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Extensions;

namespace Portfolio.Data.S3
{
	/// <summary>
	/// Хелпер для S3-хранилища
	/// </summary>
	public class S3Helper
	{
		private readonly IAmazonS3 _amazonS3;
		private readonly S3Options _options;
		private readonly ILogger<S3Helper> _logger;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="amazonS3">Клиент S3-хранилища</param>
		/// <param name="options">Настройки S3-хранилища</param>
		/// <param name="logger">Логгер</param>
		public S3Helper(IAmazonS3 amazonS3, S3Options options, ILogger<S3Helper> logger)
		{
			_amazonS3 = amazonS3;
			_options = options;
			_logger = logger;
		}

		/// <summary>
		/// Подготовить S3-хранилище
		/// </summary>
		/// <returns>Ничего</returns>
		public async Task PrepareAsync()
		{
			var operationId = Guid.NewGuid().ToString().Substring(0, 4);
			_logger.LogInformation($"Prepare S3-service:{operationId}: starting...");
			try
			{
				await CreateBucketsAsync();
				_logger.LogInformation($"Prepare S3-service:{operationId}: successfully...");
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Prepare S3-service:{operationId}: failed");
				throw;
			}
		}

		private async Task CreateBucketsAsync(CancellationToken cancellationToken = default)
		{
#pragma warning disable CA1304 // Укажите CultureInfo
#pragma warning disable CA1311 // Укажите язык и региональные параметры или используйте инвариантную версию
			var bucketsEnum = EnumExtensions.GetEnumList<Buckets>()
				.Select(x => x.GetDescription().ToLower())
				.ToList();

			bucketsEnum.Add(_options.DefaultBucketName.ToLower());
#pragma warning restore CA1311 // Укажите язык и региональные параметры или используйте инвариантную версию
#pragma warning restore CA1304 // Укажите CultureInfo

			var bucketsS3 = (await _amazonS3.ListBucketsAsync(cancellationToken)).Buckets
				.Select(x => x.BucketName)
				.ToList();

			var bucketsResult = new List<string>();

			foreach (var bucket in bucketsEnum)
			{
				if (!bucketsS3.Contains(bucket))
					bucketsResult.Add(bucket);
			}

			foreach (var bucketName in bucketsResult)
				await _amazonS3.PutBucketAsync(bucketName, cancellationToken);
		}
	}
}
