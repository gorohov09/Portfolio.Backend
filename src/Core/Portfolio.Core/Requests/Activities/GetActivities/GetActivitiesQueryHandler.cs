using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.Activities.GetActivities;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Services;

namespace Portfolio.Core.Requests.Activities.GetActivities
{
	/// <summary>
	/// Обработчик запроса <see cref="GetActivitiesQuery"/>
	/// </summary>
	public class GetActivitiesQueryHandler
		: IRequestHandler<GetActivitiesQuery, GetActivitiesResponse>
	{
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		public GetActivitiesQueryHandler(
			IDbContext dbContext)
			=> _dbContext = dbContext;

		/// <inheritdoc/>
		public async Task<GetActivitiesResponse> Handle(
			GetActivitiesQuery request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var activities = _dbContext.Activities.Filter(request);
			var count = await activities.CountAsync(cancellationToken: cancellationToken);

			var entities = await activities
				.Select(x => new GetActivitiesResponseItem
				{
					Id = x.Id,
					Name = x.Name,
					Section = x.Section,
					Type = x.Type,
					Level = x.Level,
				})
				.ToListAsync(cancellationToken: cancellationToken);

			return new GetActivitiesResponse(
				entities: entities,
				totalCount: count);
		}
	}
}
