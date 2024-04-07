using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.DomainEvents.PortfolioEvents;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.DomainEventsHandlers.PortfolioEvents
{
	/// <summary>
	/// Обработчик события <see cref="PortfolioChangedGeneralInformationDomainEvent"/>
	/// </summary>
	public class PortfolioChangedGeneralInformationDomainEventHandler
		: INotificationHandler<PortfolioChangedGeneralInformationDomainEvent>
	{
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		public PortfolioChangedGeneralInformationDomainEventHandler(IDbContext dbContext)
			=> _dbContext = dbContext;

		/// <inheritdoc />
		public async Task Handle(
			PortfolioChangedGeneralInformationDomainEvent notification,
			CancellationToken cancellationToken)
		{
			var portfolio = notification?.Portfolio
				?? throw new ArgumentNullException(nameof(notification));

			var user = await _dbContext.Users
				.FirstOrDefaultAsync(x => x.Id == portfolio.UserId, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Пользователь по Id: {portfolio.UserId} не найден");

			user.UpsertFullNameInformation(
				lastName: portfolio.LastName,
				firstName: portfolio.FirstName,
				surname: portfolio.Surname);

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
