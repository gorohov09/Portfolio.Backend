using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Data.PostgreSql.Extensions;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Конфигурация для <see cref="PhotoPortfolio"/>
	/// </summary>
	internal class PhotoConfiguration : EntityBaseConfiguration<PhotoPortfolio>
	{
		public override void ConfigureChild(EntityTypeBuilder<PhotoPortfolio> builder)
		{
			builder.ToTable("photo_portfolio", "public")
				.HasComment("Фотография портфолио");

			builder.Property(p => p.IsAvatar)
				.HasComment("Является ли фотография аватаркой")
				.IsRequired();

			builder.Property(p => p.PortfolioId)
				.HasComment("Идентификатор портфолио")
				.IsRequired();

			builder.Property(p => p.FileId)
				.HasComment("Идентификатор файла")
				.IsRequired();

			builder.HasOne(x => x.Portfolio)
				.WithMany(y => y!.Photos)
				.HasForeignKey(x => x.PortfolioId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasOne(x => x.File)
				.WithMany(y => y!.Photos)
				.HasForeignKey(x => x.FileId)
				.HasPrincipalKey(y => y!.Id)
				.OnDelete(DeleteBehavior.SetNull);

			builder.SetPropertyAccessModeField(x => x.Portfolio, PhotoPortfolio.PortfolioField);
			builder.SetPropertyAccessModeField(x => x.File, PhotoPortfolio.FileField);
		}
	}
}
