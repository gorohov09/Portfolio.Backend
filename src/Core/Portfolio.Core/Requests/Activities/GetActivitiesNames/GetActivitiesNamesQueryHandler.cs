using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.Activities.GetActivitiesNames;
using Portfolio.Core.Abstractions;

namespace Portfolio.Core.Requests.Activities.GetActivitiesNames
{
	/// <summary>
	/// Обработчик запроса <see cref="GetActivitiesNamesQuery"/>
	/// </summary>
	public class GetActivitiesNamesQueryHandler
		: IRequestHandler<GetActivitiesNamesQuery, GetActivitiesNamesResponse>
	{
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		public GetActivitiesNamesQueryHandler(
			IDbContext dbContext)
			=> _dbContext = dbContext;

		/// <inheritdoc/>
		public async Task<GetActivitiesNamesResponse> Handle(
			GetActivitiesNamesQuery request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var entities = await _dbContext.Activities
				.Select(x => new GetActivitiesNamesResponseItem
				{
					Id = x.Id,
					Name = x.Name,
				})
				.ToListAsync(cancellationToken: cancellationToken);

			return new GetActivitiesNamesResponse(
				entities: entities,
				totalCount: entities.Count);
		}
	}
}
