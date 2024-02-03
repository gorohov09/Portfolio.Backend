using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Роль пользователя
	/// </summary>
	public class Role : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_users"/>
		/// </summary>
		public const string UsersField = nameof(_users);

		/// <summary>
		/// Поле для <see cref="_privileges"/>
		/// </summary>
		public const string PrivilegesField = nameof(_privileges);

		private string _name = default!;

		private List<User>? _users;
		private List<RolePrivilege>? _privileges;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="name">Наименование</param>
		public Role(string name)
		{
			_name = name;

			_users = new List<User>();
			_privileges = new List<RolePrivilege>();
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected Role()
		{
		}

		/// <summary>
		/// Наименование
		/// </summary>
		public string Name
		{
			get => _name;
			set => _name = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Наименование")
				: value;
		}

		#region Navigation properties

		/// <summary>
		/// Пользователи
		/// </summary>
		public IReadOnlyList<User>? Users => _users;

		/// <summary>
		/// Права доступа
		/// </summary>
		public IReadOnlyList<RolePrivilege>? Privileges => _privileges;

		#endregion
	}
}
