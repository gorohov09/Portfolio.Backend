using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.AddGeneralInformation;

namespace Portfolio.Core.Requests.PortfolioRequests.AddGeneralInformation
{
	/// <summary>
	/// Команда на добавление к портфолио общей информации
	/// </summary>
	public class AddGeneralInformationCommand : AddGeneralInformationRequest, IRequest
	{
	}
}
