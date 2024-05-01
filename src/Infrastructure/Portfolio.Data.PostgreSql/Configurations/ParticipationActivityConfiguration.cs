using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="ParticipationActivity"/>
	/// </summary>
	internal class ParticipationActivityConfiguration : EntityBaseConfiguration<ParticipationActivity>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<ParticipationActivity> builder)
		{
			builder.ToTable("participation_activity", "public")
				.HasComment("Участие в мероприятии");

			builder.Property(p => p.Status)
				.HasComment("Статус")
				.IsRequired();

			builder.Property(p => p.Result)
				.HasComment("Результат");

			builder.Property(p => p.Result)
				.HasComment("Результат");

			builder.Property(p => p.Date)
				.HasComment("Дата участия");

			builder.Property(p => p.Description)
				.HasComment("Описание участия");

			builder.Property(p => p.Comment)
				.HasComment("Комментарий от администратора");

			builder.Property(p => p.ActivityId)
				.HasComment("Идентификатор мероприятия");

			builder.Property(p => p.PortfolioId)
				.HasComment("Идентификатор портфолио")
				.IsRequired();

			builder.HasOne(x => x.Activity)
				.WithMany(y => y!.Participations)
				.HasForeignKey(x => x.ActivityId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(x => x.Portfolio)
				.WithMany(y => y!.Participations)
				.HasForeignKey(x => x.PortfolioId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasOne(x => x.ParticipationActivityDocument)
				.WithOne(y => y.Participation)
				.HasForeignKey<ParticipationActivityDocument>(x => x.ParticipationId)
				.HasPrincipalKey<ParticipationActivity>(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasOne(x => x.CreatedByUser)
				.WithMany(y => y!.CreatedParticipationActivites)
				.HasForeignKey(x => x.CreatedByUserId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasOne(x => x.ModifiedByUser)
				.WithMany(y => y!.ModifiedParticipationActivites)
				.HasForeignKey(x => x.ModifiedByUserId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasOne(x => x.ManagerUser)
				.WithMany(y => y!.CheckParticipationActivites)
				.HasForeignKey(x => x.ManagerUserId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.Activity, ParticipationActivity.ActivityField);
			builder.SetPropertyAccessModeField(x => x.Portfolio, ParticipationActivity.PortfolioField);
			builder.SetPropertyAccessModeField(x => x.CreatedByUser, ParticipationActivity.CreatedByUserField);
			builder.SetPropertyAccessModeField(x => x.ModifiedByUser, ParticipationActivity.ModifiedByUserField);
			builder.SetPropertyAccessModeField(x => x.ManagerUser, ParticipationActivity.ManagerUserField);
		}
	}
}
