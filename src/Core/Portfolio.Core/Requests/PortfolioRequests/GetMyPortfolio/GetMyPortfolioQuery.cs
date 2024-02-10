using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio;

namespace Portfolio.Core.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Запрос на получение своего портфолио
	/// </summary>
	public class GetMyPortfolioQuery : IRequest<GetMyPortfolioResponse>
	{
	}
}
