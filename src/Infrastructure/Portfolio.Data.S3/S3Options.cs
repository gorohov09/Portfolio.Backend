namespace Portfolio.Data.S3
{
	/// <summary>
	/// Конфигурация хранилища
	/// </summary>
	public class S3Options
	{
		/// <summary>
		/// Логин
		/// </summary>
		public string AccessKey { get; set; } = default!;

		/// <summary>
		/// Секрет
		/// </summary>
		public string SecretKey { get; set; } = default!;

		/// <summary>
		/// УРЛ хранилища
		/// </summary>
		public string ServiceUrl { get; set; } = default!;

		/// <summary>
		/// Название бакета по умолчанию
		/// </summary>
		public string DefaultBucketName { get; set; } = default!;
	}
}
