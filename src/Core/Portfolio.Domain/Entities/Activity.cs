using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Мероприятие
	/// </summary>
	public class Activity : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_participations"/>
		/// </summary>
		public const string ParticipationsField = nameof(_participations);

		private string _name;
		private ActivitySections _section;
		private ActivityTypes _type;
		private ActivityLevel _level;
		private List<ParticipationActivity>? _participations;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="section">Вид</param>
		/// <param name="type">Тип</param>
		/// <param name="level">Уровень</param>
		/// <param name="startDate">Дата начала</param>
		/// <param name="endDate">Дата окончания</param>
		/// <param name="location">Место</param>
		/// <param name="link">Ссылка на официальную информацию</param>
		/// <param name="description">Описание</param>
		public Activity(
			string name,
			ActivitySections section,
			ActivityTypes type,
			ActivityLevel level,
			DateTime startDate,
			DateTime endDate,
			string? location = default,
			string? link = default,
			string? description = default)
		{
			Name = name;
			Section = section;
			Type = type;
			Level = level;
			Period = new Period(startDate, endDate);
			Location = location;
			Link = link;
			Description = description;

			_participations = new List<ParticipationActivity>();
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected Activity()
		{
		}

		/// <summary>
		/// Название
		/// </summary>
		public string Name
		{
			get => _name;
			private set => _name = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Имя")
				: value;
		}

		/// <summary>
		/// Вид
		/// </summary>
		public ActivitySections Section
		{
			get => _section;
			private set => _section = value == default
				? throw new RequiredFieldNotSpecifiedException("Вид")
				: value;
		}

		/// <summary>
		/// Тип
		/// </summary>
		public ActivityTypes Type
		{
			get => _type;
			private set => _type = value == default
				? throw new RequiredFieldNotSpecifiedException("Тип")
				: value;
		}

		/// <summary>
		/// Уровень
		/// </summary>
		public ActivityLevel Level
		{
			get => _level;
			private set => _level = value == default
				? throw new RequiredFieldNotSpecifiedException("Уровень")
				: value;
		}

		/// <summary>
		/// Период
		/// </summary>
		public Period Period { get; private set; }

		/// <summary>
		/// Место
		/// </summary>
		public string? Location { get; private set; }

		/// <summary>
		/// Ссылка на официальную информацию
		/// </summary>
		public string? Link { get; private set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string? Description { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Список участий в мероприятии
		/// </summary>
		public IReadOnlyList<ParticipationActivity>? Participations => _participations;

		#endregion

		/// <summary>
		/// Добавить/Обновить информацию об мероприятии
		/// </summary>
		/// <param name="name">Имя</param>
		/// <param name="section">Вид</param>
		/// <param name="type">Тип</param>
		/// <param name="level">Уровень</param>
		/// <param name="startDate">Дата начала</param>
		/// <param name="endDate">Дата окончания</param>
		/// <param name="location">Место</param>
		/// <param name="link">Ссылка на официальную информацию</param>
		/// <param name="description">Описание</param>
		public void UpsertInformation(
			string? name = default,
			ActivitySections? section = default,
			ActivityTypes? type = default,
			ActivityLevel? level = default,
			DateTime? startDate = default,
			DateTime? endDate = default,
			string? location = default,
			string? link = default,
			string? description = default)
		{
			if (name != null && Name != name)
				Name = name;
			if (section != null && Section != section)
				Section = section.Value;
			if (type != null && Type != type)
				Type = type.Value;
			if (level != null && Level != level)
				Level = level.Value;
			if (startDate != null && Period.StartDate != startDate.Value.ToUniversalTime())
				Period.StartDate = startDate.Value.ToUniversalTime();
			if (endDate != null && Period.EndDate != endDate.Value.ToUniversalTime())
				Period.StartDate = endDate.Value.ToUniversalTime();
			if (location != null && Location != location)
				Location = location;
			if (link != null && Link != link)
				Link = link;
			if (description != null && Description != description)
				Description = description;
		}
	}
}
