using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Право доступа для роли
	/// </summary>
	public class RolePrivilege : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_role"/>
		/// </summary>
		public const string RoleField = nameof(_role);

		private Role? _role;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="role">Роль</param>
		/// <param name="privilege">Право доступа</param>
		public RolePrivilege(Role role, Privileges privilege)
		{
			Role = role;
			Privilege = privilege;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected RolePrivilege()
		{
		}

		/// <summary>
		/// Идентификатор роли
		/// </summary>
		public Guid RoleId { get; protected set; }

		/// <summary>
		/// Право доступа
		/// </summary>
		public Privileges Privilege { get; protected init; }

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
