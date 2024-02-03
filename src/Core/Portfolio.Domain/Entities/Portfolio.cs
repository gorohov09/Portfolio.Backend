using System.Data;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Портфолио студента
	/// </summary>
	public class Portfolio : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_users"/>
		/// </summary>
		public const string UserField = nameof(_user);

		private string _lastName = default!;
		private string _firstName = default!;
		private User? _user;

		public Portfolio(
			string lastName,
			string firstName,
			string? surname = default,
			DateTime birthday = default,
			Institute? institute = default,
			Speciality? speciality = default,
			EducationLevels educationLevel = default)
		{
			LastName = lastName;
			FirstName = firstName;
			Surname = surname;
			Birthday = birthday;
			Institute = institute;
			Speciality = speciality;
			EducationLevel = educationLevel;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected Portfolio()
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
		public DateTime Birthday { get; set; }

		#endregion

		#region Информация о получении образования

		/// <summary>
		/// Институт
		/// </summary>
		public Institute? Institute { get; set; }

		/// <summary>
		/// Специальность
		/// </summary>
		public Speciality? Speciality { get; set; } = default!;

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
			set
			{
				_user = value
					?? throw new RequiredFieldNotSpecifiedException("Пользователь");
				UserId = value.Id;
			}
		}

		#endregion
	}
}
