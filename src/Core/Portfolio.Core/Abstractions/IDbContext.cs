using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Контекст EF Core для приложения
	/// </summary>
	public interface IDbContext
	{
		/// <summary>
		/// Портфолио
		/// </summary>
		DbSet<MyPortfolio> Portfolios { get; }

		/// <summary>
		/// Пользователи
		/// </summary>
		DbSet<User> Users { get; }

		/// <summary>
		/// Роли
		/// </summary>
		DbSet<Role> Roles { get; }

		/// <summary>
		/// БД в памяти
		/// </summary>
		bool IsInMemory { get; }

		/// <summary>
		/// Сохранить изменения в БД
		/// </summary>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Количество обновленных записей</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
