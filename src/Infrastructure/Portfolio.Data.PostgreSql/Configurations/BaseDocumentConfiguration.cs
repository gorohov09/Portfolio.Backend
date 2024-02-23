using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="BaseDocument"/>
	/// </summary>
	internal class BaseDocumentConfiguration : EntityBaseConfiguration<BaseDocument>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<BaseDocument> builder)
		{
			builder.ToTable("base_document", "public")
				.HasComment("Базовый документ");

			builder.Property(p => p.IsDeleted)
				.HasComment("Признак удаленности")
				.IsRequired();

			builder.Property(p => p.FileId)
				.HasComment("Идентификатор файла")
				.IsRequired();

			builder.HasOne(x => x.File)
				.WithMany(y => y!.BaseDocuments)
				.HasForeignKey(x => x.FileId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.SetPropertyAccessModeField(x => x.File, BaseDocument.FileField);
		}
	}
}
