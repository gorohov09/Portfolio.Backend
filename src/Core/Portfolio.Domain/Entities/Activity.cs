using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

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
		private DateTime _startDate;
		private DateTime _endDate;
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
			StartDate = startDate;
			EndDate = endDate;
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
			set => _name = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Имя")
				: value;
		}

		/// <summary>
		/// Вид
		/// </summary>
		public ActivitySections Section
		{
			get => _section;
			set => _section = value == default
				? throw new RequiredFieldNotSpecifiedException("Вид")
				: value;
		}

		/// <summary>
		/// Тип
		/// </summary>
		public ActivityTypes Type
		{
			get => _type;
			set => _type = value == default
				? throw new RequiredFieldNotSpecifiedException("Тип")
				: value;
		}

		/// <summary>
		/// Уровень
		/// </summary>
		public ActivityLevel Level
		{
			get => _level;
			set => _level = value == default
				? throw new RequiredFieldNotSpecifiedException("Уровень")
				: value;
		}

		/// <summary>
		/// Дата начала
		/// </summary>
		public DateTime StartDate
		{
			get => _startDate;
			set => _startDate = value == default
				? throw new RequiredFieldNotSpecifiedException("Дата начала")
				: value;
		}

		/// <summary>
		/// Дата окончания
		/// </summary>
		public DateTime EndDate
		{
			get => _endDate;
			set => _endDate = value == default
				? throw new RequiredFieldNotSpecifiedException("Дата окончания")
				: value;
		}

		/// <summary>
		/// Место
		/// </summary>
		public string? Location { get; set; }

		/// <summary>
		/// Ссылка на официальную информацию
		/// </summary>
		public string? Link { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string? Description { get; set; }

		#region Navigation properties

		/// <summary>
		/// Список участий в мероприятии
		/// </summary>
		public IReadOnlyList<ParticipationActivity>? Participations => _participations;

		#endregion
	}
}
