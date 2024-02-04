using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="MyPortfolio"/>
	/// </summary>
	internal class UserConfiguration : EntityBaseConfiguration<User>
	{
		public override void ConfigureChild(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("user", "public")
				.HasComment("Пользователь");

			builder.Property(p => p.Login)
				.HasComment("Логин")
				.IsRequired();

			builder.Property(p => p.Email)
				.HasComment("Электронная почта")
				.IsRequired();

			builder.Property(p => p.PasswordHash)
				.HasComment("Хеш пароля");

			builder.Property(p => p.Phone)
				.HasComment("Телефон");

			builder.Property(p => p.RoleId)
				.HasComment("Идентификатор роли");

			builder.HasOne(x => x.Role)
				.WithMany(y => y!.Users)
				.HasForeignKey(x => x.RoleId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.Role, User.RoleField);
		}
	}
}
