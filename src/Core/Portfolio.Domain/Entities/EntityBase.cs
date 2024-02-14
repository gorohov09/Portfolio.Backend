using System.Collections.Concurrent;
using Portfolio.Domain.Abstractions;

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
		public Guid Id { get; set; }

		/// <summary>
		/// Дата создания сущности
		/// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// Дата последнего изменения сущности
		/// </summary>
		public DateTime ModifiedOn { get; set; }

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
	}
}
