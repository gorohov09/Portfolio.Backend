using System.ComponentModel;

namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Бакеты, в которые сохраняются файлы в S3-хранилище
	/// </summary>
	public enum Buckets
	{
		/// <summary>
		/// Фотографии пользователей
		/// </summary>
		[Description("Photos")]
		Photos = 1,
	}
}
