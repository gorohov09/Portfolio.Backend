using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolioList;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Extensions;
using Portfolio.Core.Services;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Requests.PortfolioRequests.GetPortfolioList
{
	/// <summary>
	/// Обработчик запроса <see cref="GetPortfolioListQuery"/>
	/// </summary>
	public class GetPortfolioListQueryHandler
		: IRequestHandler<GetPortfolioListQuery, GetPortfolioListResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IAuthorizationService _authorizationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public GetPortfolioListQueryHandler(
			IDbContext dbContext,
			IAuthorizationService authorizationService)
		{
			_dbContext = dbContext;
			_authorizationService = authorizationService;
		}

		/// <inheritdoc/>
		public async Task<GetPortfolioListResponse> Handle(
			GetPortfolioListQuery request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _authorizationService.CheckPrivilegeAsync(
				Privileges.PortfolioListView,
				cancellationToken: cancellationToken);

			var portfolioQueryable = _dbContext.Portfolios.Filter(request);

			var count = await portfolioQueryable.CountAsync(cancellationToken: cancellationToken);

			var entities = portfolioQueryable
				.Select(x => new GetPortfolioListResponseItem
				{
					Id = x.Id,
					FullName = $"{x.LastName} {x.FirstName} {x.Surname}".Trim(),
					FacultyName = x.Faculty != null ? x.Faculty.FullName : null,
					GroupNumber = x.GroupNumber,
					InstituteName = x.Faculty != null ? x.Faculty.Institute!.FullName : null,
					SpecialityName = x.Speciality != null ? x.Speciality.Name : null,
				})
				.SkipTake(request)
				.OrderBy(x => x.FullName)
				.ToList();

			return new GetPortfolioListResponse(entities, count);
		}
	}
}
