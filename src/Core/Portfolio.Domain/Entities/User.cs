using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	public class User : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_role"/>
		/// </summary>
		public const string RoleField = nameof(_role);

		private Role? _role;

		/// <summary>
		/// Конструктор
		/// </summary>
		protected User()
		{
		}

		/// <summary>
		/// Логин
		/// </summary>
		public string Login { get; protected set; } = default!;

		/// <summary>
		/// Хеш пароля
		/// </summary>
		public string? PasswordHash { get; protected set; }

		/// <summary>
		/// Электронная почта
		/// </summary>
		public string Email { get; protected set; } = default!;

		/// <summary>
		/// Телефон
		/// </summary>
		public string? Phone { get; protected set; }

		/// <summary>
		/// Идентификатор роли
		/// </summary>
		public Guid RoleId { get; protected set; }

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
