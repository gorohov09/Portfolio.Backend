using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.AddEducationInformation;

namespace Portfolio.Core.Requests.PortfolioRequests.AddEducationInformation
{
	/// <summary>
	/// Команда на добавление к портфолио информации о получении образования
	/// </summary>
	public class AddEducationInformationCommand : AddEducationInformationRequest, IRequest
	{
	}
}
