using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="Notification"/>
	/// </summary>
	internal class NotificationConfiguration : EntityBaseConfiguration<Notification>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<Notification> builder)
		{
			builder.ToTable("notification", "public")
				.HasComment("Уведомление");

			builder.Property(p => p.Type)
				.HasComment("Тип уведомления")
				.IsRequired();

			builder.Property(p => p.Title)
				.HasComment("Заголовок")
				.IsRequired();

			builder.Property(p => p.Description)
				.HasComment("Описание");

			builder.Property(p => p.IsRead)
				.HasComment("Является ли уведомление прочитанным")
				.IsRequired();

			builder.Property(p => p.UserId)
				.HasComment("Идентификатор пользователя-получателя")
				.IsRequired();

			builder.HasOne(x => x.User)
				.WithMany(y => y.Notifications)
				.HasForeignKey(x => x.UserId)
				.HasPrincipalKey(y => y.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.User, Notification.UserField);
		}
	}
}
