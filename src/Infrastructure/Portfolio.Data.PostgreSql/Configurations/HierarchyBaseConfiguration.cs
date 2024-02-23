using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Data.PostgreSql.Configurations
{
	/// <summary>
	/// Базовая конфигурация для иерархичной сущности
	/// </summary>
	/// <typeparam name="TEntity">Тип сущности</typeparam>
	internal abstract class HierarchyBaseConfiguration<TEntity> : EntityBaseConfiguration<TEntity>
		where TEntity : EntityBase
	{
		/// <inheritdoc />
		protected override void ConfigureId(EntityTypeBuilder<TEntity> builder)
		{
			// У наследуемой таблицы не должно быть автоинкрементируемого идентификатора
		}
	}
}
