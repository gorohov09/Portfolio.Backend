using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityList;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Services;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.GetParticipationActivityList
{
	/// <summary>
	/// Обработчик запроса <see cref="GetParticipationActivityListQuery"/>
	/// </summary>
	public class GetParticipationActivityListQueryHandler
		: IRequestHandler<GetParticipationActivityListQuery, GetParticipationActivityListResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public GetParticipationActivityListQueryHandler(
			IDbContext dbContext,
			IUserContext userContext)
		{
			_dbContext = dbContext;
			_userContext = userContext;
		}

		/// <inheritdoc/>
		public async Task<GetParticipationActivityListResponse> Handle(
			GetParticipationActivityListQuery request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var query = _dbContext.Participations
				.Filter(_userContext);

			var count = await query.CountAsync(cancellationToken: cancellationToken);

			var participationActivities = await query
				.Select(x => new GetParticipationActivityListResponseItem
				{
					Id = x.Id,
					Result = x.Result,
					Status = x.Status,
					Date = x.Date,
					Activity = x.Activity != null
						? new GetParticipationActivityListResponseItemActivity
						{
							Id = x.Activity.Id,
							Name = x.Activity.Name,
						}
						: null,
					UpdateDate = x.ModifiedOn,
					CreationDate = x.CreatedOn,
				})
				.ToListAsync(cancellationToken: cancellationToken);

			return new GetParticipationActivityListResponse(
				entities: participationActivities,
				totalCount: count);
		}
	}
}
