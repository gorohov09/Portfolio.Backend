using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.PortfolioRequests.GetMyPortfolio;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Extensions;

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
					.ThenInclude(y => y.Institute)
				.Include(x => x.Participations!.Where(x => x.Status == ParticipationActivityStatus.Approved))
					.ThenInclude(y => y.Activity)
				.Include(x => x.Participations!.Where(x => x.Status == ParticipationActivityStatus.Approved))
					.ThenInclude(y => y.ParticipationActivityDocument)
				.FirstOrDefaultAsync(x => x.UserId == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"У пользователя с Id: {_userContext.CurrentUserId} не найдено портфолио");

			var b = GetPortfolioBlocks(portfolio.Participations);

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
				Blocks = GetPortfolioBlocks(portfolio.Participations),
			};
		}

		private List<PortfolioBlockResponse> GetPortfolioBlocks(
			IReadOnlyList<ParticipationActivity> participationActivities)
		{
			var result = new List<PortfolioBlockResponse>();

			var dictionary = participationActivities.GroupBy(x => x.Activity!.Section)
								 .ToDictionary(y => y.Key, y => y.ToList());

			foreach (var item in dictionary)
			{
				result.Add(new PortfolioBlockResponse
				{
					Name = item.Key.GetDescription(),
					Section = item.Key,
					ParticipationActivities = item.Value
						.Select(x => new ParticipationActivityPortfolioResponse
						{
							Id = x.Id,
							Date = x.Date!.Value,
							Activity = new ActivityPortfolioResponse
							{
								Id = x.Activity!.Id,
								Name = x.Activity.Name,
								Level = x.Activity.Level,
								Section = x.Activity.Section,
								Type = x.Activity.Type,
							},
							Document = new ParticipationActivityDocumentResponse
							{
								Id = x.ParticipationActivityDocument!.Id,
								FileId = x.ParticipationActivityDocument!.FileId,
							},
						})
						.ToList(),
				});
			}

			return result;
		}
	}
}
