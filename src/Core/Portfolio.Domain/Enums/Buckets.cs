using System.ComponentModel;

namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Бакеты, в которе сохраняются файлы в S3-хранилище
	/// </summary>
	public enum Buckets
	{
		/// <summary>
		/// Аватарки пользователей
		/// </summary>
		[Description("Avatars")]
		Avatars = 1,
	}
}
