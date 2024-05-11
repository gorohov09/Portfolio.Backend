using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.AdminRequests.PostManager;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.AdminRequests.PostManager
{
	/// <summary>
	/// Обработчик запроса <see cref="PostActivityCommand"/>
	/// </summary>
	public class PostManagerCommandHandler
		: IRequestHandler<PostManagerCommand, PostManagerResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IAuthorizationService _authorizationService;
		private readonly IPasswordEncryptionService _passwordEncryptionService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="authorizationService">Сервис проверки прав доступа</param>
		/// <param name="passwordEncryptionService">Сервис хэширования паролей</param>
		public PostManagerCommandHandler(
			IDbContext dbContext,
			IAuthorizationService authorizationService,
			IPasswordEncryptionService passwordEncryptionService)
		{
			_dbContext = dbContext;
			_authorizationService = authorizationService;
			_passwordEncryptionService = passwordEncryptionService;
		}

		/// <inheritdoc/>
		public async Task<PostManagerResponse> Handle(
			PostManagerCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _authorizationService.CheckPrivilegeAsync(Privileges.ManagerCreated, cancellationToken);

			var isExist = await _dbContext.Users
				.AnyAsync(x => x.Login == request.Login || x.Email == request.Email, cancellationToken: cancellationToken);

			if (isExist)
				throw new ApplicationExceptionBase($"Пользователь с таким логином или email уже существует");

			var roleManager = await _dbContext.Roles
				.FirstOrDefaultAsync(x => x.Id == DefaultRoles.ManagerId, cancellationToken: cancellationToken)
				?? throw new NotFoundException("Не найдена роль - Менеджер");

			var passwordHash = _passwordEncryptionService.EncodePassword(request.Password);

			var user = new User(
				lastName: request.LastName,
				firstName: request.FirstName,
				login: request.Login,
				passwordHash: passwordHash,
				email: request.Email,
				role: roleManager);

			await _dbContext.Users.AddAsync(user, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return new PostManagerResponse
			{
				Id = user.Id,
			};
		}
	}
}
