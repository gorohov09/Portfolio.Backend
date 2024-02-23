using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.SubmitParticipationActivity
{
	/// <summary>
	/// Обработчик запроса <see cref="SubmitParticipationActivityCommand"/>
	/// </summary>
	public class SubmitParticipationActivityCommandHandler : IRequestHandler<SubmitParticipationActivityCommand>
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
		public SubmitParticipationActivityCommandHandler(
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
			SubmitParticipationActivityCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _authorizationService.CheckPrivilegeAsync(Privileges.ParticipationActivitySubmit, cancellationToken);

			var participationActivity = await _dbContext.Participations
				.Include(x => x.Activity)
				.Include(x => x.ParticipationActivityDocument)
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Не найдено участие в мероприятии с Id: {request.Id}");

			if (participationActivity.CreatedByUserId != _userContext.CurrentUserId)
				throw new ApplicationExceptionBase("Подать может только пользователь, создавший - Участие в мероприятие ");

			participationActivity.Submit();

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
