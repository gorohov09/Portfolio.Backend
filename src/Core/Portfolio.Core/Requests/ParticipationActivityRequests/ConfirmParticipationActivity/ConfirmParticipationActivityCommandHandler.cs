using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.ConfirmParticipationActivity
{
	/// <summary>
	/// Обработчик запроса <see cref="ConfirmParticipationActivityCommand"/>
	/// </summary>
	public class ConfirmParticipationActivityCommandHandler : IRequestHandler<ConfirmParticipationActivityCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;
		private readonly IAuthorizationService _authorizationService;
		private readonly IHubNotificationService _hubNotificationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public ConfirmParticipationActivityCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			IAuthorizationService authorizationService,
			IHubNotificationService hubNotificationService)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_authorizationService = authorizationService;
			_hubNotificationService = hubNotificationService;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(
			ConfirmParticipationActivityCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _authorizationService.CheckPrivilegeAsync(Privileges.ParticipationActivityConfirm, cancellationToken);

			var participationActivity = await _dbContext.Participations
				.Include(x => x.Activity)
				.Include(x => x.ParticipationActivityDocument)
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Не найдено участие в мероприятии с Id: {request.Id}");

			if (participationActivity.ManagerUserId != _userContext.CurrentUserId)
				throw new ApplicationExceptionBase("Одобрить участие может только пользователь, являющийся назначенным менеджером для проверки");

			participationActivity.Confirm();

			await _hubNotificationService.SendNewNotificationAsync(
				notification: new NotificationModel(
					type: NotificationType.ParticipationActivityConfirmed,
					title: NotificationMessages.ParticipationActivityConfirmedTitle,
					description: NotificationMessages.ParticipationActivityConfirmedDescription),
				userId: participationActivity.CreatedByUserId,
				cancellationToken: cancellationToken);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
