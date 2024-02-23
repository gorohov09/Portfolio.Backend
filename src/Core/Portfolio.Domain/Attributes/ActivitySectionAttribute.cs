using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Attributes
{
	/// <summary>
	/// Атрибут, определяющий раздел для типов мероприятий
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class ActivitySectionAttribute : Attribute
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="activitySection">Раздел</param>
		public ActivitySectionAttribute(ActivitySections activitySection)
			=> ActivitySection = activitySection;

		/// <summary>
		/// Раздел
		/// </summary>
		public ActivitySections ActivitySection { get; }
	}
}
