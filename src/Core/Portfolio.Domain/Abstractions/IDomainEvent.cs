using MediatR;

namespace Portfolio.Domain.Abstractions
{
	/// <summary>
	/// Доменное событие
	/// </summary>
	public interface IDomainEvent : INotification
	{
		/// <summary>
		/// Обрабатывать событие в транзакции
		/// </summary>
		public bool IsInTransaction { get; }
	}
}
