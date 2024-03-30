using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.Activities.GetActivityById;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.Activities.GetActivityById
{
	/// <summary>
	/// Обработчик запроса <see cref="GetActivityByIdQuery"/>
	/// </summary>
	public class GetActivityByIdQueryHandler
		: IRequestHandler<GetActivityByIdQuery, GetActivityByIdResponse>
	{
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public GetActivityByIdQueryHandler(
			IDbContext dbContext)
			=> _dbContext = dbContext;

		/// <inheritdoc/>
		public async Task<GetActivityByIdResponse> Handle(
			GetActivityByIdQuery request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var response = await _dbContext.Activities
							   .Select(x => new GetActivityByIdResponse
							   {
								   Id = x.Id,
								   Name = x.Name,
								   Type = x.Type,
								   Section = x.Section,
								   Level = x.Level,
								   Location = x.Location,
								   Description = x.Description,
								   Link = x.Link,
								   Period = new GetActivityByIdResponsePeriod
								   {
									   StartDate = x.Period.StartDate,
									   EndDate = x.Period.EndDate,
									   IsOneDay = x.Period.IsOneDay,
								   },
							   })
							   .AsNoTracking()
							   .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
						   ?? throw new NotFoundException($"Мероприятие с Id: {request.Id} не найдено");

			return response;
		}
	}
}
