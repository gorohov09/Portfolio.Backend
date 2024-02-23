using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.PostParticipationActivity;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.PostParticipationActivity
{
	/// <summary>
	/// Обработчик запроса <see cref="PostParticipationActivityCommand"/>
	/// </summary>
	public class PostParticipationActivityCommandHandler
		: IRequestHandler<PostParticipationActivityCommand, PostParticipationActivityResponse>
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
		public PostParticipationActivityCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			IAuthorizationService authorizationService)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_authorizationService = authorizationService;
		}

		/// <inheritdoc/>
		public async Task<PostParticipationActivityResponse> Handle(
			PostParticipationActivityCommand request,
			CancellationToken cancellationToken)
		{
			await _authorizationService.CheckPrivilegeAsync(Privileges.ParticipationActivityCreated, cancellationToken);

			var portfolio = await _dbContext.Portfolios
				.Include(x => x.Participations)
				.FirstOrDefaultAsync(x => x.UserId == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"У пользователя с Id: {_userContext.CurrentUserId} не найдено портфолио");

			var participationActivity = new ParticipationActivity(portfolio);
			portfolio.AddParticipationActivity(participationActivity);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return new PostParticipationActivityResponse
			{
				ParticipationActivityId = participationActivity.Id,
			};
		}
	}
}
