using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.GetPortfolio;

namespace Portfolio.Core.Requests.PortfolioRequests.GetPortfolio
{
	/// <summary>
	/// Запрос на получение своего портфолио
	/// </summary>
	public class GetPortfolioQuery : IRequest<GetPortfolioResponse>
	{
		/// <summary>
		/// Идентификатор портфолио
		/// </summary>
		public Guid? Id { get; set; }
	}
}
