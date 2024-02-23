using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="Activity"/>
	/// </summary>
	internal class ActivityConfiguration : EntityBaseConfiguration<Activity>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<Activity> builder)
		{
			builder.ToTable("activity", "public")
				.HasComment("Мероприятие");

			builder.Property(p => p.Name)
				.HasComment("Название")
				.IsRequired();

			builder.Property(p => p.Section)
				.HasComment("Вид")
				.IsRequired();

			builder.Property(p => p.Type)
				.HasComment("Тип")
				.IsRequired();

			builder.Property(p => p.Level)
				.HasComment("Уровень")
				.IsRequired();

			builder.Property(p => p.StartDate)
				.HasComment("Дата начала")
				.IsRequired();

			builder.Property(p => p.EndDate)
				.HasComment("Дата окончания")
				.IsRequired();

			builder.Property(p => p.Location)
				.HasComment("Место");

			builder.Property(p => p.Link)
				.HasComment("Ссылка на официальную информацию");

			builder.Property(p => p.Description)
				.HasComment("Описание");

			builder.HasMany(x => x.Participations)
				.WithOne(y => y!.Activity)
				.HasForeignKey(x => x.ActivityId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.SetNull);

			builder.SetPropertyAccessModeField(x => x.Participations, Activity.ParticipationsField);
		}
	}
}
