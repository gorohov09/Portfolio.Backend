using MediatR;
using Portfolio.Contracts.Requests.PortfolioRequests.AddPhoto;

namespace Portfolio.Core.Requests.PortfolioRequests.AddPhoto
{
	/// <summary>
	/// Команда на добавление к портфолио фотографии
	/// </summary>
	public class AddPhotoCommand : AddPhotoRequest, IRequest
	{
	}
}
