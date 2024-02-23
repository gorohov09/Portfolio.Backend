using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="ParticipationActivityDocument"/>
	/// </summary>
	internal class ParticipationActivityDocumentConfiguration : EntityBaseConfiguration<ParticipationActivityDocument>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<ParticipationActivityDocument> builder)
		{
			builder.ToTable("participation_activity_document", "public")
				.HasComment("Подтверждающий документ участия в мероприятии");

			builder.Property(p => p.Type)
				.HasComment("Тип подтверждающего документа участия")
				.IsRequired();

			builder.Property(p => p.FileId)
				.HasComment("Идентификатор файла")
				.IsRequired();

			builder.HasOne(x => x.Participation)
				.WithOne(y => y.ParticipationActivityDocument)
				.HasForeignKey<ParticipationActivity>(x => x.ParticipationActivityDocumentId)
				.HasPrincipalKey<ParticipationActivityDocument>(x => x.Id)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(x => x.File)
				.WithMany(y => y!.ParticipationActivityDocuments)
				.HasForeignKey(x => x.FileId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.SetNull);

			builder.SetPropertyAccessModeField(x => x.Participation, ParticipationActivityDocument.ParticipationField);
			builder.SetPropertyAccessModeField(x => x.File, ParticipationActivityDocument.FileField);
		}
	}
}
