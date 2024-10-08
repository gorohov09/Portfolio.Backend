using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.UserRequests.GetMyUserInfo;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.UserRequests.GetMyUserInfo
{
	/// <summary>
	/// Обработчик запроса <see cref="GetMyUserInfoQuery"/>
	/// </summary>
	public class GetMyUserInfoQueryHandler : IRequestHandler<GetMyUserInfoQuery, GetMyUserInfoResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public GetMyUserInfoQueryHandler(
			IDbContext dbContext,
			IUserContext userContext)
		{
			_dbContext = dbContext;
			_userContext = userContext;
		}

		/// <inheritdoc/>
		public async Task<GetMyUserInfoResponse> Handle(
			GetMyUserInfoQuery request,
			CancellationToken cancellationToken)
		{
			var user = await _dbContext.Users
				.Include(x => x.Role)
				.Include(x => x.Notifications!.Where(x => !x.IsRead))
				.FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"Пользователь с Id: {_userContext.CurrentUserId} не найден");

			return new GetMyUserInfoResponse
			{
				Login = user.Login,
				FullName = user.FullName,
				Email = user.Email,
				RoleName = user.Role!.Name,
				Phone = user.Phone,
				CreatedOn = user.CreatedOn,
				NotificationCount = user.GetUnreadNotificationCount(),
			};
		}
	}
}
