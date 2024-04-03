using MediatR;
using Portfolio.Contracts.Requests.Activities.PostActivity;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Requests.Activities.PostActivity
{
	/// <summary>
	/// Обработчик запроса <see cref="PostActivityCommand"/>
	/// </summary>
	public class PostActivityCommandHandler
		: IRequestHandler<PostActivityCommand, PostActivityResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;
		private readonly IAuthorizationService _authorizationService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="authorizationService">Сервис проверки прав доступа</param>
		public PostActivityCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			IAuthorizationService authorizationService)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_authorizationService = authorizationService;
		}

		/// <inheritdoc/>
		public async Task<PostActivityResponse> Handle(
			PostActivityCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _authorizationService.CheckPrivilegeAsync(Privileges.ActivityCreated, cancellationToken);

			var activity = new Activity(
				name: request.Name,
				section: request.Section,
				type: request.Type,
				level: request.Level,
				startDate: request.StartDate,
				endDate: request.EndDate,
				location: request.Location,
				link: request.Link,
				description: request.Description);

			_dbContext.Activities.Add(activity);
			await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

			return new PostActivityResponse { Id = activity.Id };
		}
	}
}
