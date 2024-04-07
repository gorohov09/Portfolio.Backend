using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.DomainEvents.PortfolioEvents
{
	/// <summary>
	/// Событие изменения основной информации в портфолио
	/// </summary>
	public class PortfolioChangedGeneralInformationDomainEvent : IDomainEvent
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="lastName">Фамилия</param>
		/// <param name="fistName">Имя</param>
		/// <param name="surname">Отчество</param>
		public PortfolioChangedGeneralInformationDomainEvent(MyPortfolio portfolio)
			=> Portfolio = portfolio ?? throw new ArgumentNullException(nameof(portfolio));

		/// <summary>
		/// Имя
		/// </summary>
		public MyPortfolio Portfolio { get; } = default!;

		/// <inheritdoc />
		public bool IsInTransaction => true;
	}
}
