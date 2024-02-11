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

			builder.OwnsOne(p => p.Speciality, a =>
			{
				a.Property(u => u!.Name)
					.HasComment("Название");
				a.Property(u => u!.Number)
					.HasComment("Номер");
			});

			builder.Property(p => p.GroupNumber)
				.HasComment("Номер группы");

			builder.Property(p => p.UserId)
				.HasComment("Идентификатор пользователя")
				.IsRequired();

			builder.Property(p => p.FacultyId)
				.HasComment("Идентификатор кафедры");

			builder.HasOne(x => x.User)
				.WithOne()
				.HasForeignKey<MyPortfolio>(x => x.UserId)
				.HasPrincipalKey<User>(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasOne(x => x.Faculty)
				.WithMany(y => y.Portfolios)
				.HasForeignKey(x => x.FacultyId)
				.HasPrincipalKey(x => x.Id)
				.OnDelete(DeleteBehavior.SetNull);

			builder.SetPropertyAccessModeField(x => x.User, MyPortfolio.UserField);
			builder.SetPropertyAccessModeField(x => x.Faculty, MyPortfolio.FacultyField);
		}
	}
}
