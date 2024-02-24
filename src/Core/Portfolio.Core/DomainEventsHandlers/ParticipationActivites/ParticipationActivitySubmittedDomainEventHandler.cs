using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.DomainEvents.ParticipationActivites;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.DomainEventsHandlers.ParticipationActivites
{
	/// <summary>
	/// Обработчик события <see cref="ParticipationActivitySubmittedDomainEvent"/>
	/// </summary>
	public class ParticipationActivitySubmittedDomainEventHandler
		: INotificationHandler<ParticipationActivitySubmittedDomainEvent>
	{
		private readonly IDbContext _dbContext;
		private readonly IHubNotificationService _hubNotificationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="hubNotificationService">Сервис SignalR для уведомлений</param>
		public ParticipationActivitySubmittedDomainEventHandler(
			IDbContext dbContext,
			IHubNotificationService hubNotificationService)
		{
			_dbContext = dbContext;
			_hubNotificationService = hubNotificationService;
		}

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
				.FirstOrDefaultAsync(cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Самый свободный менеджер ен найден");

			// TODO: Оповестить менеджера по почте
			participation.ManagerUser = mostFreeManager;

			await _hubNotificationService.SendNewNotificationAsync(
				notification: new NotificationModel(
					type: NotificationType.ParticipationActivitySubmitted,
					title: NotificationMessages.ParticipationActivitySubmittedTitle,
					description: NotificationMessages.ParticipationActivitySubmittedDescription),
				userId: mostFreeManager.Id,
				cancellationToken: cancellationToken);

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
