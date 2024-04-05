using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.PortfolioRequests.GetMyPortfolio
{
	/// <summary>
	/// Обработчик запроса <see cref="GetMyPortfolioQuery"/>
	/// </summary>
	public class GetMyPortfolioQueryHandler : IRequestHandler<GetMyPortfolioQuery, GetMyPortfolioResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public GetMyPortfolioQueryHandler(
			IDbContext dbContext,
			IUserContext userContext)
		{
			_dbContext = dbContext;
			_userContext = userContext;
		}

		/// <inheritdoc/>
		public async Task<GetMyPortfolioResponse> Handle(
			GetMyPortfolioQuery request,
			CancellationToken cancellationToken)
		{
			var portfolio = await _dbContext.Portfolios
				.Include(x => x.Faculty)
					.ThenInclude(x => x!.Institute)
				.FirstOrDefaultAsync(x => x.UserId == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"У пользователя с Id: {_userContext.CurrentUserId} не найдено портфолио");

			return new GetMyPortfolioResponse
			{
				LastName = portfolio.LastName,
				FirstName = portfolio.FirstName,
				Surname = portfolio.Surname,
				Birthday = portfolio.Birthday,
				Institute = portfolio.Faculty?.Institute != null
					? new InstituteResponse
					{
						FullName = portfolio.Faculty.Institute.FullName,
						ShortName = portfolio.Faculty.Institute.ShortName,
					}
					: null,
				Faculty = portfolio.Faculty != null
					? new FacultyResponse
					{
						Id = portfolio.Faculty.Id,
						FullName = portfolio.Faculty.FullName,
						ShortName = portfolio.Faculty.ShortName,
					}
					: null,
				Speciality = portfolio.Speciality != null
					? new SpecialityResponse
					{
						Name = portfolio.Speciality.Name,
						Number = portfolio.Speciality.Number,
					}
					: null,
				EducationLevel = portfolio.EducationLevel,
				GroupNumber = portfolio.GroupNumber,
			};
		}
	}
}
