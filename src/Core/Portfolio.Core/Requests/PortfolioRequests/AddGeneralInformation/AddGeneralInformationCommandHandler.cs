using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.PortfolioRequests.AddGeneralInformation
{
	public class AddGeneralInformationCommandHandler
		: IRequestHandler<AddGeneralInformationCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public AddGeneralInformationCommandHandler(
			IDbContext dbContext,
			IUserContext userContext)
		{
			_dbContext = dbContext;
			_userContext = userContext;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(
			AddGeneralInformationCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var portfolio = await _dbContext.Portfolios
				.Include(x => x.Faculty)
				.FirstOrDefaultAsync(x => x.UserId == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"У пользователя с Id: {_userContext.CurrentUserId} не найдено портфолио");

			portfolio.UpsertGeneralInformation(
				lastName: request.LastName,
				firstName: request.FirstName,
				birthday: request.Birthday,
				surname: request.Surname);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
