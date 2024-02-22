using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.AddGeneralInformation;

namespace Portfolio.Core.Requests.PortfolioRequests.AddOrUpdateGeneralInformation
{
	/// <summary>
	/// Команда на добавление/обновление общей информации в портфолио
	/// </summary>
	public class AddOrUpdateGeneralInformationCommand : AddOrUpdateGeneralInformationRequest, IRequest
	{
	}
}
