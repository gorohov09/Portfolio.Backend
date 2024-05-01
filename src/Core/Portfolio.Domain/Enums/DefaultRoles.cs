using System.ComponentModel;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Enums
{
	/// <summary>
	/// Идентификаторы ролей пол умолчанию
	/// </summary>
	public static class DefaultRoles
	{
		/// <summary>
		/// Идентификатор роли "Студент"
		/// </summary>
		[Description("Студент")]
		public static readonly Guid StudentId = new("e15a85fd-4736-4b05-b215-576ce2386f27");

		/// <summary>
		/// Название роли "Студент"
		/// </summary>
		public static readonly string StudentName = "Student";

		/// <summary>
		/// Идентификатор роли "Менеджер"
		/// </summary>
		[Description("Менеджер")]
		public static readonly Guid ManagerId = new("8a3ee818-0de0-4269-952a-2478cf8c76ce");

		/// <summary>
		/// Название роли "Менеджер"
		/// </summary>
		public static readonly string ManagerName = "Manager";

		/// <summary>
		/// Идентификатор ролей к списку привилегий
		/// </summary>
		public static readonly IReadOnlyDictionary<Guid, List<Privileges>> RolesIdsToPrivileges =
			new Dictionary<Guid, List<Privileges>>
			{
				[StudentId] = new()
				{
					Privileges.PortfolioView,
					Privileges.ParticipationActivityCreated,
					Privileges.ParticipationActivityUpdate,
					Privileges.ParticipationActivitySubmit,
				},

				[ManagerId] = new()
				{
					Privileges.PortfolioListView,
					Privileges.ParticipationActivityUpdate,
					Privileges.ParticipationActivitySendRevision,
					Privileges.ParticipationActivityConfirm,
					Privileges.ActivityCreated,
					Privileges.ActivityUpdated,
				},
			};

		public static string ToRoleName(this Role role)
		{
			ArgumentNullException.ThrowIfNull(role);

			if (role.Id == StudentId)
				return StudentName;
			if (role.Id == ManagerId)
				return ManagerName;

			return string.Empty;
		}
	}
}
