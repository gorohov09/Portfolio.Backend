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

		private string _lastName = default!;
		private string _firstName = default!;
		private DateTime _birthday;

		private User? _user;
		private Faculty? _faculty;

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
	}
}
