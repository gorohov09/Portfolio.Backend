using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="CourseProject"/>
	/// </summary>
	internal class CourseProjectConfiguration : EntityBaseConfiguration<CourseProject>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<CourseProject> builder)
		{
			builder.ToTable("course_project", "public")
				.HasComment("Курсовой проект");

			builder.Property(p => p.SubjectName)
				.HasComment("Наименование дисциплины")
				.IsRequired();

			builder.Property(p => p.TopicName)
				.HasComment("Наименование темы")
				.IsRequired();

			builder.Property(p => p.SemesterNumber)
				.HasComment("Номер семестра")
				.IsRequired();

			builder.Property(p => p.PointNumber)
				.HasComment("Количество баллов")
				.IsRequired();

			builder.Property(p => p.ScoreNumber)
				.HasComment("Оценка")
				.IsRequired();

			builder.Property(p => p.СompletionDate)
				.HasComment("Дата сдачи")
				.IsRequired();

			builder.HasOne(x => x.Portfolio)
				.WithMany(y => y!.CourseProjects)
				.HasForeignKey(x => x.PortfolioId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.Portfolio, CourseProject.PortfolioField);
		}
	}
}
