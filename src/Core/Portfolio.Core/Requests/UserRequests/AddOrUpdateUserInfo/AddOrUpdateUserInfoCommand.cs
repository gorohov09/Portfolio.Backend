using MediatR;
using Portfolio.Contracts.Requests.UserRequests.AddOrUpdateUserInfo;

namespace Portfolio.Core.Requests.UserRequests.AddOrUpdateUserInfo
{
	/// <summary>
	/// Команда на добавление/обновление контактных данных пользователя
	/// </summary>
	public class AddOrUpdateUserInfoCommand : AddOrUpdateUserInfoRequest, IRequest
	{
	}
}
