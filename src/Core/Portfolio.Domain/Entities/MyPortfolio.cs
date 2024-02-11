using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Портфолио студента
	/// </summary>
	public class MyPortfolio : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_users"/>
		/// </summary>
		public const string UserField = nameof(_user);

		/// <summary>
		/// Поле для <see cref="_faculty"/>
		/// </summary>
		public const string FacultyField = nameof(_faculty);

		/// <summary>
		/// Поле для <see cref="_courseProjects"/>
		/// </summary>
		public const string CourseProjectsField = nameof(_courseProjects);

		private string _lastName = default!;
		private string _firstName = default!;
		private DateTime _birthday;

		private User? _user;
		private Faculty? _faculty;
		private List<CourseProject> _courseProjects;

		public MyPortfolio(
			string lastName,
			string firstName,
			string? surname = default,
			DateTime birthday = default,
			User user = default)
		{
			LastName = lastName;
			FirstName = firstName;
			Surname = surname;
			Birthday = birthday;
			User = user;

			_courseProjects = new List<CourseProject>();
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected MyPortfolio()
		{
		}

		#region Общая информация о студенте

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName
		{
			get => _lastName;
			private set => _lastName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Фамилия")
				: value;
		}

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName
		{
			get => _firstName;
			private set => _firstName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Имя")
				: value;
		}

		/// <summary>
		/// Отчество
		/// </summary>
		public string? Surname { get; private set; }

		/// <summary>
		/// Дата рождения
		/// </summary>
		public DateTime Birthday
		{
			get => _birthday;
			private set => _birthday = value == default
				? throw new RequiredFieldNotSpecifiedException("Дата рождения")
				: value.ToUniversalTime();
		}

		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		public Guid UserId { get; private set; }

		#endregion

		#region Информация о получении образования

		/// <summary>
		/// Уровень образования
		/// </summary>
		public EducationLevels? EducationLevel { get; private set; }

		/// <summary>
		/// Номер группы
		/// </summary>
		public string? GroupNumber { get; private set; }

		/// <summary>
		/// Специальность
		/// </summary>
		public Speciality? Speciality { get; private set; }

		/// <summary>
		/// Идентификатор кафедры
		/// </summary>
		public Guid? FacultyId { get; private set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Пользователь
		/// </summary>
		public User? User
		{
			get => _user;
			private set
			{
				ArgumentNullException.ThrowIfNull(value);
				ArgumentNullException.ThrowIfNull(value.Role);

				if (value.Role.Id != DefaultRoles.StudentId)
					throw new ApplicationExceptionBase("Портфолио может быть только у студента");

				_user = value;
				UserId = value.Id;
			}
		}

		/// <summary>
		/// Кафедра
		/// </summary>
		public Faculty? Faculty
		{
			get => _faculty;
			private set
			{
				_faculty = value
					?? throw new RequiredFieldNotSpecifiedException("Институт");
				FacultyId = value.Id;
			}
		}

		/// <summary>
		/// Курсовые проекты
		/// </summary>
		public IReadOnlyList<CourseProject>? CourseProjects => _courseProjects;

		#endregion

		/// <summary>
		/// Добавить/Обновить общую информацию в портфолио
		/// </summary>
		/// <param name="lastName">Фамилия</param>
		/// <param name="firstName">Имя</param>
		/// <param name="birthday">Дата рождения</param>
		/// <param name="surname">Отчество</param>
		public void UpsertGeneralInformation(
			string? lastName = default,
			string? firstName = default,
			DateTime birthday = default,
			string? surname = default)
		{
			LastName = lastName ?? LastName;
			FirstName = firstName ?? FirstName;
			Surname = surname ?? Surname;
			Birthday = birthday == default
				? Birthday
				: birthday;
		}

		/// <summary>
		/// Добавить курсовой проект
		/// </summary>
		/// <param name="subjectName">Наименование дисциплины</param>
		/// <param name="topicName">Наименование темы</param>
		/// <param name="semesterNumber">Номер семестра</param>
		/// <param name="scoreNumber">Оценка</param>
		/// <param name="pointNumber">Количество баллов</param>
		/// <param name="completionDate">Дата сдачи</param>
		public void AddCourseProject(
			string subjectName,
			string topicName,
			int semesterNumber,
			int scoreNumber,
			int pointNumber,
			DateTime completionDate)
		{
			if (_courseProjects == null)
				throw new NotIncludedException("Курсовые проекты");

			var isExist = _courseProjects.Any(x => x.SubjectName == subjectName
				&& x.TopicName == topicName);

			if (isExist)
				throw new ApplicationExceptionBase("Данный курсовой проект по этой дисциплине уже существует");

			_courseProjects.Add(new CourseProject(
				portfolio: this,
				subjectName: subjectName,
				topicName: topicName,
				semesterNumber: semesterNumber,
				scoreNumber: scoreNumber,
				pointNumber: pointNumber,
				completionDate: completionDate));
		}
	}
}
