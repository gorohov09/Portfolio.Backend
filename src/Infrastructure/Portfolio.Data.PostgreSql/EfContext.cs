using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Exceptions;
using File = Portfolio.Domain.Entities.File;

namespace Portfolio.Data.PostgreSql
{
	/// <summary>
	/// Контекст EF Core для приложения
	/// </summary>
	public class EfContext : DbContext, IDbContext
	{
		private const string DefaultSchema = "public";
		private readonly IUserContext _userContext;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IMediator _domainEventsDispatcher;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="options">Параметры подключения к БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="dateTimeProvider">Провайдер даты и времени</param>
		/// <param name="domainEventsDispatcher">Медиатор для доменных событий</param>
		public EfContext(
			DbContextOptions<EfContext> options,
			IUserContext userContext,
			IDateTimeProvider dateTimeProvider,
			IMediator domainEventsDispatcher)
			: base(options)
		{
			_userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
			_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
			_domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
		}

		public DbSet<MyPortfolio> Portfolios { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<Institute> Institutes { get; set; }

		public DbSet<Faculty> Faculties { get; set; }

		public DbSet<ParticipationActivity> Participations { get; set; }

		public DbSet<Activity> Activities { get; set; }

		public DbSet<File> Files { get; set; }

		public DbSet<EmailMessage> EmailMessages { get; set; }

		/// <inheritdoc/>
		public bool IsInMemory => Database.IsInMemory();

		/// <inheritdoc cref="IDbContext.SaveChangesAsync(bool, CancellationToken)" />
		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			var entityEntries = ChangeTracker.Entries().ToArray();
			if (entityEntries.Length > 10)
				entityEntries.AsParallel().ForAll(OnSave);
			else
				foreach (var entityEntry in entityEntries)
					OnSave(entityEntry);

			// перед применением событий получаем их все из доменных сущностей во избежание дубликации в рекурсии
			var domainEvents = entityEntries
				.Select(po => po.Entity)
				.OfType<EntityBase>()
				.SelectMany(x => x.RetrieveDomainEvents())
				.ToArray();

			try
			{
				var isNewTransaction = Database.CurrentTransaction is null;

				if (isNewTransaction)
					await Database.BeginTransactionAsync(cancellationToken);

				await PublishEventsAsync(domainEvents.Where(x => x.IsInTransaction), cancellationToken);
				var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

				if (isNewTransaction)
				{
					await Database.CommitTransactionAsync(cancellationToken);
					await PublishEventsAsync(domainEvents.Where(x => !x.IsInTransaction), cancellationToken);
				}

				return result;
			}
			catch (DbUpdateException ex)
			{
				if (Database.CurrentTransaction is not null)
					await Database.RollbackTransactionAsync(cancellationToken);

				return HandleDbUpdateException(ex, cancellationToken);
			}
		}

		/// <inheritdoc cref="IDbContext.SaveChangesAsync(CancellationToken)" />
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
			await SaveChangesAsync(true, cancellationToken);

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema(DefaultSchema);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);
		}

		protected virtual int HandleDbUpdateException(DbUpdateException ex, CancellationToken cancellationToken = default)
		{
			if (ex?.InnerException is PostgresException postgresEx)
				throw postgresEx.SqlState switch
				{
					PostgresErrorCodes.ForeignKeyViolation => new ApplicationExceptionBase(
						$"Заданы некорректные идентификаторы для внешних ключей сущности: {postgresEx.Detail}", ex),
					PostgresErrorCodes.UniqueViolation => new DuplicateUniqueKeyException(ex),
					_ => ex,
				};
			throw ex ?? throw new ArgumentNullException(nameof(ex));
		}

		private static void SoftDeleted(EntityEntry entityEntry)
		{
			if (entityEntry?.Entity is not null
				&& entityEntry.Entity is ISoftDeletable softDeletable)
			{
				if (softDeletable.IsDeleted)
				{
					entityEntry.State = EntityState.Deleted;
					return;
				}

				softDeletable.IsDeleted = true;
				entityEntry.State = EntityState.Modified;
			}
		}

		private void OnSave(EntityEntry entityEntry)
		{
			// TODO: вынести в домен
			if (entityEntry.State != EntityState.Unchanged)
			{
				UpdateTimestamp(entityEntry);
				SetModifiedUser(entityEntry);
			}

			if (entityEntry.State == EntityState.Deleted)
				SoftDeleted(entityEntry);
		}

		private void UpdateTimestamp(EntityEntry entityEntry)
		{
			var entity = entityEntry.Entity;
			if (entity is null)
				return;

			if (entity is EntityBase table)
			{
				table.ModifiedOn = _dateTimeProvider.UtcNow;

				if (entityEntry.State == EntityState.Added && table.CreatedOn == DateTime.MinValue)
					table.CreatedOn = _dateTimeProvider.UtcNow;
			}
		}

		private async Task PublishEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
		{
			foreach (var @event in domainEvents)
				await _domainEventsDispatcher.Publish(@event, cancellationToken);
		}

		private void SetModifiedUser(EntityEntry entityEntry)
		{
			if (entityEntry?.Entity != null
				&& entityEntry.State != EntityState.Unchanged
				&& entityEntry.Entity is IUserTrackable userTrackable)
			{
				userTrackable.ModifiedByUserId = _userContext.CurrentUserId;

				if (entityEntry.State == EntityState.Added)
				{
					if (IsInMemory && userTrackable.CreatedByUserId != default)
						return;
					userTrackable.CreatedByUserId = _userContext.CurrentUserId;
				}
			}
		}
	}
}
