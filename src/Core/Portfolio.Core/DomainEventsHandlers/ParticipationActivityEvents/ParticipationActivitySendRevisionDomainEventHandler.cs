using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.DomainEvents.ParticipationActivityEvents;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.DomainEventsHandlers.ParticipationActivityEvents
{
	/// <summary>
	/// Обработчик события <see cref="ParticipationActivitySendRevisionDomainEvent"/>
	/// </summary>
	public class ParticipationActivitySendRevisionDomainEventHandler
		: INotificationHandler<ParticipationActivitySendRevisionDomainEvent>
	{
		private readonly IDbContext _dbContext;
		private readonly INotificationService _notificationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="hubNotificationService">Сервис SignalR для уведомлений</param>
		public ParticipationActivitySendRevisionDomainEventHandler(
			IDbContext dbContext,
			INotificationService notificationService)
		{
			_dbContext = dbContext;
			_notificationService = notificationService;
		}

		/// <inheritdoc />
		public async Task Handle(
			ParticipationActivitySendRevisionDomainEvent notification,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(notification);

			var participation = notification.Participation
				?? throw new ArgumentNullException(nameof(notification));

			var user = await _dbContext.Users
				.FirstOrDefaultAsync(x => x.Id == participation.CreatedByUserId, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Пользователь по Id: {participation.ManagerUserId} не найден");

			var activity = await _dbContext.Activities
				.FirstOrDefaultAsync(x => x.Id == participation.ActivityId, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Участие в мероприятии по Id: {participation.ManagerUserId} не найдено");

			await _notificationService.PushAsync(
				notificationModel: new NotificationModel(
					type: NotificationType.ParticipationActivitySendRevision,
					title: NotificationMessages.ParticipationActivitySendRevisionTitle,
					description: string.Format(NotificationMessages.ParticipationActivitySendRevisionDescription, activity.Name)),
				user: user,
				cancellationToken: cancellationToken,
				isSendingEmail: true);
		}
	}
}
