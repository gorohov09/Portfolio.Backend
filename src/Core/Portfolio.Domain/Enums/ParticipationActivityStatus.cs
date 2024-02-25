using System.ComponentModel;

namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Статус участия в мероприятии
	/// </summary>
	public enum ParticipationActivityStatus
	{
		/// <summary>
		/// Черновик
		/// </summary>
		[Description("Черновик")]
		Draft = 1,

		/// <summary>
		/// Подан
		/// </summary>
		[Description("Подан")]
		Submitted = 2,

		/// <summary>
		/// Отправлен на доработку
		/// </summary>
		[Description("Отправлен на доработку")]
		SentRevision = 3,

		/// <summary>
		/// Одобрен
		/// </summary>
		[Description("Одобрен")]
		Approved = 4,
	}
}
