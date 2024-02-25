using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="EmailMessage"/>
	/// </summary>
	internal class EmailMessageConfiguration : EntityBaseConfiguration<EmailMessage>
	{
		public override void ConfigureChild(EntityTypeBuilder<EmailMessage> builder)
		{
			builder.ToTable("email_message", "public")
				.HasComment("Электронно-почтовое сообщение");

			builder.Property(p => p.AddressTo)
				.HasComment("Адрес получателя")
				.IsRequired();

			builder.Property(p => p.Subject)
				.HasComment("Заголовок сообщения")
				.IsRequired();

			builder.Property(p => p.Body)
				.HasComment("Тело сообщения")
				.IsRequired();

			builder.Property(p => p.ToUserId)
				.HasComment("Id пользователя-получателя")
				.IsRequired();
		}
	}
}
