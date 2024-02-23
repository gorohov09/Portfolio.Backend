using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.DomainEvents.ParticipationActivites;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.DomainEventsHandlers.ParticipationActivites
{
	/// <summary>
	/// Обработчик события <see cref="ParticipationActivitySubmittedDomainEvent"/>
	/// </summary>
	public class ParticipationActivitySubmittedDomainEventHandler
		: INotificationHandler<ParticipationActivitySubmittedDomainEvent>
	{
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		public ParticipationActivitySubmittedDomainEventHandler(IDbContext dbContext)
			=> _dbContext = dbContext;

		/// <inheritdoc />
		public async Task Handle(
			ParticipationActivitySubmittedDomainEvent notification,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(notification);

			var participation = notification.Participation
				?? throw new ArgumentNullException(nameof(notification));

			var mostFreeManager = await _dbContext.Users
				.Where(x => x.RoleId == DefaultRoles.ManagerId)
				.OrderBy(x => x.CheckParticipationActivites!.Count())
				.FirstOrDefaultAsync(cancellationToken: cancellationToken);

			// TODO: Оповестить менеджера по почте
			participation.ManagerUser = mostFreeManager;

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
