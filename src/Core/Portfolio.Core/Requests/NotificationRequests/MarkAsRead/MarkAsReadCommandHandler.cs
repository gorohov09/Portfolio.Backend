using MediatR;
using Portfolio.Core.Abstractions;

namespace Portfolio.Core.Requests.NotificationRequests.MarkAsRead
{
	/// <summary>
	/// Обработчик запроса <see cref="MarkAsReadCommand"/>
	/// </summary>
	public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly INotificationService _notificationService;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="notificationService">Сервис для рассылки уведомлений</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public MarkAsReadCommandHandler(
			IDbContext dbContext,
			INotificationService notificationService,
			IUserContext userContext)
		{
			_dbContext = dbContext;
			_notificationService = notificationService;
			_userContext = userContext;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(
			MarkAsReadCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _notificationService.MarkAsReadAsync(
				notificationIds: request.Ids,
				userId: _userContext.CurrentUserId,
				cancellationToken: cancellationToken);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
