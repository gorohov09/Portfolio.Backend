using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.AddOrUpdateEducationInformation;

namespace Portfolio.Core.Requests.PortfolioRequests.AddOrUpdateEducationInformation
{
	/// <summary>
	/// Команда на добавление/обновление к портфолио информации о получении образования
	/// </summary>
	public class AddOrUpdateEducationInformationCommand : AddOrUpdateEducationInformationRequest, IRequest
	{
	}
}
