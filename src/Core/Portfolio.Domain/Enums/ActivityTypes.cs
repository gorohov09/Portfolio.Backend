using System.ComponentModel;
using Portfolio.Domain.Attributes;

namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Тип мероприятия
	/// </summary>
	public enum ActivityTypes
	{
		/// <summary>
		/// Олимпиада
		/// </summary>
		[ActivitySection(ActivitySections.ScientificAndEducational)]
		[Description("Олимпиада")]
		Olympiad = 1,

		/// <summary>
		/// Конференция
		/// </summary>
		[ActivitySection(ActivitySections.ScientificAndEducational)]
		[Description("Конференция")]
		Сonference = 2,
	}
}
