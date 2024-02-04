using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="MyPortfolio"/>
	/// </summary>
	internal class PortfolioConfiguration : EntityBaseConfiguration<MyPortfolio>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<MyPortfolio> builder)
		{
			builder.ToTable("portfolio", "public")
				.HasComment("Портфолио");

			builder.Property(p => p.LastName)
				.HasComment("Фамилия")
				.IsRequired();

			builder.Property(p => p.FirstName)
				.HasComment("Имя")
				.IsRequired();

			builder.Property(p => p.Surname)
				.HasComment("Отчество");

			builder.Property(p => p.Birthday)
				.HasComment("Дата рождения")
				.IsRequired();

			builder.Property(p => p.EducationLevel)
				.HasComment("Уровень образования");

			builder.OwnsOne(p => p.Institute, a =>
			{
				a.Property(u => u!.FullName)
					.HasComment("Полное имя");
				a.Property(u => u!.ShortName)
					.HasComment("Сокращенное имя");
			});

			builder.OwnsOne(p => p.Speciality, a =>
			{
				a.Property(u => u!.Name)
					.HasComment("Название");
				a.Property(u => u!.Number)
					.HasComment("Номер");
			});

			builder.Property(p => p.UserId)
				.HasComment("Идентификатор пользователя")
				.IsRequired();

			builder.HasOne(x => x.User)
				.WithOne()
				.HasForeignKey<MyPortfolio>(x => x.UserId)
				.HasPrincipalKey<User>(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.User, MyPortfolio.UserField);
		}
	}
}
