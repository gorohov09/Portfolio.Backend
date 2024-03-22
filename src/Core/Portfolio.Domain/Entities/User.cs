using Portfolio.Domain.DomainEvents.UserEvents;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	public class User : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_role"/>
		/// </summary>
		public const string RoleField = nameof(_role);

		/// <summary>
		/// Поле для <see cref="_createdParticipationActivites"/>
		/// </summary>
		public const string CreatedParticipationActivitesField = nameof(_createdParticipationActivites);

		/// <summary>
		/// Поле для <see cref="_modifiedParticipationActivites"/>
		/// </summary>
		public const string ModifiedParticipationActivitesField = nameof(_modifiedParticipationActivites);

		/// <summary>
		/// Поле для <see cref="_checkParticipationActivites"/>
		/// </summary>
		public const string CheckParticipationActivitesField = nameof(_checkParticipationActivites);

		private string _lastName;
		private string _firstName;
		private string _surname;
		private string _login;
		private string _email;
		private string _passwordHash;
		private Role? _role;
		private List<ParticipationActivity>? _createdParticipationActivites;
		private List<ParticipationActivity>? _modifiedParticipationActivites;
		private List<ParticipationActivity>? _checkParticipationActivites;

		/// <summary>
		/// Конструткор
		/// </summary>
		/// <param name="login">Логин</param>
		/// <param name="passwordHash">Хеш пароля</param>
		/// <param name="email">Электронная почта</param>
		/// <param name="phone">Телефон</param>
		/// <param name="role">Роль</param>
		public User(
			string lastName,
			string firstName,
			DateTime birthday,
			string login,
			string passwordHash,
			string email,
			string? phone = default,
			Role? role = default)
		{
			LastName = lastName;
			FirstName = firstName;
			Login = login;
			PasswordHash = passwordHash;
			Email = email;
			Phone = phone;
			Role = role;

			if (Role!.Id == DefaultRoles.StudentId)
				AddDomainEvent(new StudentRegisteredDomainEvent(
					user: this,
					lastName: lastName,
					firstName: firstName,
					birthday: birthday));

			_createdParticipationActivites = new List<ParticipationActivity>();
			_modifiedParticipationActivites = new List<ParticipationActivity>();
			_checkParticipationActivites = new List<ParticipationActivity>();
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected User()
		{
		}

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName
		{
			get => _lastName;
			private set => _lastName = value
					?? throw new RequiredFieldNotSpecifiedException("Фамилия");
		}

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName
		{
			get => _firstName;
			private set => _firstName = value
					?? throw new RequiredFieldNotSpecifiedException("Имя");
		}

		/// <summary>
		/// Отчество
		/// </summary>
		public string? Surname { get; private set; }

		/// <summary>
		/// Логин
		/// </summary>
		public string Login
		{
			get => _login;
			private set => _login = value
					?? throw new RequiredFieldNotSpecifiedException("Логин");
		}

		/// <summary>
		/// Хеш пароля
		/// </summary>
		public string PasswordHash
		{
			get => _passwordHash;
			set => _passwordHash = value
					?? throw new RequiredFieldNotSpecifiedException("Хеш пароля");
		}

		/// <summary>
		/// Электронная почта
		/// </summary>
		public string Email
		{
			get => _email;
			private set => _email = value
					?? throw new RequiredFieldNotSpecifiedException("Электронная почта");
		}

		/// <summary>
		/// Телефон
		/// </summary>
		public string? Phone { get; set; }

		/// <summary>
		/// Идентификатор роли
		/// </summary>
		public Guid RoleId { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Роль
		/// </summary>
		public Role? Role
		{
			get => _role;
			set
			{
				_role = value
					?? throw new RequiredFieldNotSpecifiedException("Роль");
				RoleId = value.Id;
			}
		}

		/// <summary>
		/// Созданные участия в мероприятиях
		/// </summary>
		public IReadOnlyList<ParticipationActivity>? CreatedParticipationActivites => _createdParticipationActivites;

		/// <summary>
		/// Измененные участия в мероприятиях
		/// </summary>
		public IReadOnlyList<ParticipationActivity>? ModifiedParticipationActivites => _modifiedParticipationActivites;

		/// <summary>
		/// Участия в мероприятиях, которые нужно проверить данному пользователю
		/// </summary>
		public IReadOnlyList<ParticipationActivity>? CheckParticipationActivites => _checkParticipationActivites;

		#endregion

		/// <summary>
		/// ФИО
		/// </summary>
		public string FullName
			=> $"{LastName} {FirstName} {Surname}".Trim();

		/// <summary>
		/// Обновить контактную информацию пользователя
		/// </summary>
		/// <param name="login">Логин</param>
		/// <param name="email">E-mail</param>
		/// <param name="phone">Телефон</param>
		public void UpsertContactInformation(
			string? login = default,
			string? email = default,
			string? phone = default)
		{
			if (login != null && Login != login)
				Login = login;
			if (email != null && Email != email)
				Email = email;
			if (phone != null && Phone != phone)
				Phone = phone;
		}
	}
}
