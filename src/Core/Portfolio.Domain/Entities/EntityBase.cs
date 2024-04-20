using System.Collections.Concurrent;
using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Базовая сущность
	/// </summary>
	public abstract class EntityBase
	{
		/// <summary>
		/// Доменные события
		/// </summary>
		private ConcurrentQueue<IDomainEvent> _domainEvents = default!;

		/// <summary>
		/// ИД сущности
		/// </summary>
		public Guid Id { get; protected set; }

		/// <summary>
		/// Дата создания сущности
		/// </summary>
		public DateTime CreatedOn { get; protected set; }

		/// <summary>
		/// Дата последнего изменения сущности
		/// </summary>
		public DateTime ModifiedOn { get; protected set; }

		/// <summary>
		/// Новая сущность
		/// </summary>
		public bool IsNew => Id == default;

		/// <summary>
		/// Добавить доменное событие
		/// </summary>
		/// <param name="eventItem">Событие</param>
		public void AddDomainEvent(IDomainEvent eventItem)
		{
			if (eventItem is null)
				return;

			_domainEvents ??= new ConcurrentQueue<IDomainEvent>();
			_domainEvents.Enqueue(eventItem);
		}

		/// <summary>
		/// Получить доменные события и очистить их список
		/// </summary>
		/// <returns>Доменные события</returns>
		public IEnumerable<IDomainEvent> RetrieveDomainEvents()
		{
			if (_domainEvents is null)
				return Enumerable.Empty<IDomainEvent>();

			var events = _domainEvents.ToList();
			_domainEvents.Clear();
			return events;
		}

		/// <summary>
		/// Установить дату создания
		/// </summary>
		/// <param name="createdOn">Дата создания</param>
		public void SetCreatedDate(DateTime createdOn)
		{
			if (createdOn == default)
				throw new ApplicationExceptionBase("Некоректная дата создания");

			if (!IsNew)
				throw new ApplicationExceptionBase("Дата создания устанавливается только новой сущности");

			CreatedOn = createdOn;
		}

		/// <summary>
		/// Установить дату обновления
		/// </summary>
		/// <param name="modifiedOn">Дата обновления</param>
		public void SetUpdatedDate(DateTime modifiedOn)
		{
			if (modifiedOn == default)
				throw new ApplicationExceptionBase("Некоректная дата обновления");

			if (modifiedOn < CreatedOn)
				throw new ApplicationExceptionBase("Дата обновления не может быть раньше даты создания");

			ModifiedOn = modifiedOn;
		}
	}
}
