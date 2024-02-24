using MediatR;

namespace Portfolio.Domain.Abstractions
{
	/// <summary>
	/// Доменное событие
	/// </summary>
	public interface IDomainEvent : MediatR.INotification
	{
		/// <summary>
		/// Обрабатывать событие в транзакции
		/// </summary>
		public bool IsInTransaction { get; }
	}
}
