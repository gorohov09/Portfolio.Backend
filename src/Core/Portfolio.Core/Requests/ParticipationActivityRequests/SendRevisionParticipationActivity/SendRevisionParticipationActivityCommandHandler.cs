using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.SendRevisionParticipationActivity
{
	/// <summary>
	/// Обработчик запроса <see cref="SendRevisionParticipationActivityCommand"/>
	/// </summary>
	public class SendRevisionParticipationActivityCommandHandler : IRequestHandler<SendRevisionParticipationActivityCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;
		private readonly IAuthorizationService _authorizationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="authorizationService">Сервис проверки прав доступа</param>
		public SendRevisionParticipationActivityCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			IAuthorizationService authorizationService)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_authorizationService = authorizationService;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(
			SendRevisionParticipationActivityCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _authorizationService.CheckPrivilegeAsync(Privileges.ParticipationActivitySendRevision, cancellationToken);

			var participationActivity = await _dbContext.Participations
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Не найдено участие в мероприятии с Id: {request.Id}");

			if (participationActivity.ManagerUserId != _userContext.CurrentUserId)
				throw new ApplicationExceptionBase("Отправить на доработку может только пользователь, являющийся назначенным менеджером для проверки");

			participationActivity.SendRevision(comment: request.Comment);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
