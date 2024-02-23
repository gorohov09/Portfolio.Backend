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
		/// <inheritdoc/>
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
				.HasComment("Хеш пароля")
				.IsRequired();

			builder.Property(p => p.Phone)
				.HasComment("Телефон");

			builder.Property(p => p.RoleId)
				.HasComment("Идентификатор роли");

			builder.HasOne(x => x.Role)
				.WithMany(y => y!.Users)
				.HasForeignKey(x => x.RoleId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasMany(x => x.CreatedParticipationActivites)
				.WithOne(y => y.CreatedByUser)
				.HasForeignKey(y => y.CreatedByUserId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasMany(x => x.ModifiedParticipationActivites)
				.WithOne(y => y.ModifiedByUser)
				.HasForeignKey(y => y.ModifiedByUserId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasMany(x => x.CheckParticipationActivites)
				.WithOne(y => y.ManagerUser)
				.HasForeignKey(y => y.ManagerUserId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.Role, User.RoleField);
			builder.SetPropertyAccessModeField(x => x.CreatedParticipationActivites, User.CreatedParticipationActivitesField);
			builder.SetPropertyAccessModeField(x => x.ModifiedParticipationActivites, User.ModifiedParticipationActivitesField);
			builder.SetPropertyAccessModeField(x => x.CheckParticipationActivites, User.CheckParticipationActivitesField);
		}
	}
}
