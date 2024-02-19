using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Portfolio.Domain.Entities.File;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="File"/>
	/// </summary>
	internal class FileConfiguration : EntityBaseConfiguration<File>
	{
		public override void ConfigureChild(EntityTypeBuilder<File> builder)
		{
			builder.ToTable("file", "public")
				.HasComment("Файл");

			builder.Property(x => x.Address).IsRequired();
			builder.Property(x => x.FileName).IsRequired();
			builder.Property(x => x.Size).IsRequired();
			builder.Property(x => x.IsDeleted).IsRequired();
			builder.Property(x => x.ContentType);
			builder.Ignore(x => x.Extension);

			builder.HasMany(x => x.Photos)
				.WithOne(y => y!.File)
				.HasForeignKey(x => x.FileId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
