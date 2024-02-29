using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using File = Portfolio.Domain.Entities.File;

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
		/// Институты
		/// </summary>
		DbSet<Institute> Institutes { get; }

		/// <summary>
		/// Кафедры
		/// </summary>
		DbSet<Faculty> Faculties { get; }

		/// <summary>
		/// Участия в мероприятиях
		/// </summary>
		DbSet<ParticipationActivity> Participations { get; }

		/// <summary>
		/// Мероприятия
		/// </summary>
		DbSet<Activity> Activities { get; }

		/// <summary>
		/// Файлы
		/// </summary>
		DbSet<File> Files { get; }

		/// <summary>
		/// Электронно-почтовые сообщения
		/// </summary>
		DbSet<EmailMessage> EmailMessages { get; }

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
