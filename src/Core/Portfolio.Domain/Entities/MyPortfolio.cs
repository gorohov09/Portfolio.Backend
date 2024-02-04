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

		private string _lastName = default!;
		private string _firstName = default!;
		private DateTime _birthday;
		private User? _user;

		public MyPortfolio(
			string lastName,
			string firstName,
			string? surname = default,
			DateTime birthday = default,
			Institute? institute = default,
			Speciality? speciality = default,
			EducationLevels educationLevel = default,
			User user = default)
		{
			LastName = lastName;
			FirstName = firstName;
			Surname = surname;
			Birthday = birthday;
			Institute = institute;
			Speciality = speciality;
			EducationLevel = educationLevel;
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
			set => _lastName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Фамилия")
				: value;
		}

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName
		{
			get => _firstName;
			set => _firstName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Имя")
				: value;
		}

		/// <summary>
		/// Отчество
		/// </summary>
		public string? Surname { get; set; }

		/// <summary>
		/// Дата рождения
		/// </summary>
		public DateTime Birthday
		{
			get => _birthday;
			set => _birthday = value == default
				? throw new RequiredFieldNotSpecifiedException("Дата рождения")
				: value.ToUniversalTime();
		}

		#endregion

		#region Информация о получении образования

		/// <summary>
		/// Институт
		/// </summary>
		public Institute? Institute { get; set; }

		/// <summary>
		/// Специальность
		/// </summary>
		public Speciality? Speciality { get; set; }

		/// <summary>
		/// Уровень образования
		/// </summary>
		public EducationLevels? EducationLevel { get; set; }

		#endregion

		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		public Guid UserId { get; protected set; }

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

		#endregion
	}
}
