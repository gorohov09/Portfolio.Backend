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
		private readonly INotificationService _notificationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="hubNotificationService">Сервис SignalR для уведомлений</param>
		public ParticipationActivitySubmittedDomainEventHandler(
			IDbContext dbContext,
			INotificationService notificationService)
		{
			_dbContext = dbContext;
			_notificationService = notificationService;
		}

		/// <inheritdoc />
		public async Task Handle(
			ParticipationActivitySubmittedDomainEvent notification,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(notification);

			var participation = notification.Participation
				?? throw new ArgumentNullException(nameof(notification));

			var mostFreeManager = notification.IsRepeatSubmit && participation.ManagerUserId.HasValue
				? await _dbContext.Users
					.FirstOrDefaultAsync(x => x.Id == participation.ManagerUserId, cancellationToken: cancellationToken)
					?? throw new NotFoundException($"Менеджер по Id: {participation.ManagerUserId} не найден")
				: await _dbContext.Users
					.Where(x => x.RoleId == DefaultRoles.ManagerId)
					.OrderBy(x => x.CheckParticipationActivites!.Count(x => x.Status == ParticipationActivityStatus.Submitted))
					.FirstOrDefaultAsync(cancellationToken: cancellationToken)
					?? throw new NotFoundException($"Самый свободный менеджер не найден");

			participation.ManagerUser = mostFreeManager;

			await _notificationService.PushAsync(
				notificationModel: new NotificationModel(
					type: NotificationType.ParticipationActivitySubmitted,
					title: NotificationMessages.ParticipationActivitySubmittedTitle,
					description: NotificationMessages.ParticipationActivitySubmittedDescription),
				user: mostFreeManager,
				cancellationToken: cancellationToken,
				isSendingEmail: false);

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
