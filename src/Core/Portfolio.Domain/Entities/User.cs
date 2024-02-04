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

		private string _login;
		private string _email;
		private string _passwordHash;
		private Role? _role;

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
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected User()
		{
		}

		/// <summary>
		/// Логин
		/// </summary>
		public string Login
		{
			get => _login;
			set => _login = value
					?? throw new RequiredFieldNotSpecifiedException("Логин");
		}

		/// <summary>
		/// Хеш пароля
		/// </summary>
		public string PasswordHash
		{
			get => _passwordHash;
			private set => _passwordHash = value
					?? throw new RequiredFieldNotSpecifiedException("Хеш пароля");
		}

		/// <summary>
		/// Электронная почта
		/// </summary>
		public string Email
		{
			get => _email;
			set => _email = value
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

		#endregion
	}
}
