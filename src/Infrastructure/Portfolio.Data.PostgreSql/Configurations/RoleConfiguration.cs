using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="Role"/>
	/// </summary>
	internal class RoleConfiguration : EntityBaseConfiguration<Role>
	{
		public override void ConfigureChild(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("role", "public")
				.HasComment("Роль");

			builder.Property(p => p.Name)
				.IsRequired()
				.HasComment("Наименование")
				.UsePropertyAccessMode(PropertyAccessMode.Field);

			builder.HasMany(x => x.Users)
				.WithOne(y => y.Role!)
				.HasForeignKey(y => y.RoleId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasMany(x => x.Privileges)
				.WithOne(y => y.Role!)
				.HasForeignKey(y => y.RoleId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.Users, Role.UsersField);
			builder.SetPropertyAccessModeField(x => x.Privileges, Role.PrivilegesField);
		}
	}
}
