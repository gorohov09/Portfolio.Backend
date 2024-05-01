using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="ParticipationActivityDocument"/>
	/// </summary>
	internal class ParticipationActivityDocumentConfiguration : HierarchyBaseConfiguration<ParticipationActivityDocument>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<ParticipationActivityDocument> builder)
		{
			builder.ToTable("participation_activity_document", "public")
				.HasComment("Подтверждающий документ участия в мероприятии");

			builder.Property(p => p.Type)
				.HasComment("Тип подтверждающего документа участия")
				.IsRequired();

			builder.Property(p => p.ParticipationId)
				.HasComment("Идентификатор участия в мероприятии")
				.IsRequired();

			builder.HasOne(x => x.Participation)
				.WithOne(y => y.ParticipationActivityDocument)
				.HasForeignKey<ParticipationActivityDocument>(x => x.ParticipationId)
				.HasPrincipalKey<ParticipationActivity>(x => x.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.Participation, ParticipationActivityDocument.ParticipationField);
		}
	}
}
