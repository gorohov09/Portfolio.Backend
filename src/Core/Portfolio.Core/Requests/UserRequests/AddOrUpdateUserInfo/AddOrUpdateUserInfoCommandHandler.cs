using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Extensions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.UserRequests.AddOrUpdateUserInfo
{
	public class AddOrUpdateUserInfoCommandHandler : IRequestHandler<AddOrUpdateUserInfoCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public AddOrUpdateUserInfoCommandHandler(
			IDbContext dbContext,
			IUserContext userContext)
		{
			_dbContext = dbContext;
			_userContext = userContext;
		}

		public async Task<Unit> Handle(
			AddOrUpdateUserInfoCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			if (request.Email != null && !request.Email.IsValidEmailAddress())
				throw new ValidateException("Введен некорректный Email");

			if (request.Phone != null && !request.Phone.IsValidRussianPhoneNumber())
				throw new ValidateException("Введен некорректный номер телефона");

			var user = await _dbContext.Users
				.FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"Пользователь с Id: {_userContext.CurrentUserId} не найден");

			user.UpsertContactInformation(
				login: request.Login,
				email: request.Email,
				phone: request.Phone);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
