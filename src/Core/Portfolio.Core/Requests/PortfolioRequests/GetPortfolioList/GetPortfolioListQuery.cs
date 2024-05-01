using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolioList;
using Portfolio.Core.Models;

namespace Portfolio.Core.Requests.PortfolioRequests.GetPortfolioList
{
	/// <summary>
	/// Запрос на получение списка портфолио
	/// </summary>
	public class GetPortfolioListQuery
		: GetPortfolioListRequest, IRequest<GetPortfolioListResponse>, IPaginationQuery, IFilterPortfolio
	{
	}
}
