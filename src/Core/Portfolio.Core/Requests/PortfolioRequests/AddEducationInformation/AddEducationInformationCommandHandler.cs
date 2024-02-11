using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Core.Requests.PortfolioRequests.AddEducationInformation
{
	/// <summary>
	/// Обработчик запроса <see cref="AddEducationInformationCommand"/>
	/// </summary>
	public class AddEducationInformationCommandHandler : IRequestHandler<AddEducationInformationCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;
		private readonly Speciality.GetSpecialityNameByNumber _specialityNameByNumber;
		private readonly Speciality.SatisfySpecialityLevel _satisfySpecialityLevel;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="specialityNameByNumber">Делегат получения названия специальности по коду</param>
		/// /// <param name="specialityNameByNumber">Делегат соответствия номера уровню образования</param>
		public AddEducationInformationCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			Speciality.GetSpecialityNameByNumber specialityNameByNumber,
			Speciality.SatisfySpecialityLevel satisfySpecialityLevel)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_specialityNameByNumber = specialityNameByNumber;
			_satisfySpecialityLevel = satisfySpecialityLevel;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(AddEducationInformationCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var portfolio = await _dbContext.Portfolios
				.Include(x => x.Faculty)
				.FirstOrDefaultAsync(x => x.UserId == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"У пользователя с Id: {_userContext.CurrentUserId} не найдено портфолио");

			var faculty = request.FacultyId.HasValue
				? await _dbContext.Faculties
					  .Include(x => x.Portfolios)
					  .FirstOrDefaultAsync(x => x.Id == request.FacultyId, cancellationToken)
					   ?? throw new NotFoundException($"По Id: {request.FacultyId} не найдена кафедра")
				: default;

			var speciality = request.SpecialityNumber != null
				? new Speciality(
					number: request.SpecialityNumber,
					getSpecialityNameByNumber: _specialityNameByNumber)
				: default;

			portfolio.UpsertEducationInformation(
				educationLevel: request.EducationLevel,
				groupNumber: request.GroupNumber,
				speciality: speciality,
				faculty: faculty,
				satisfySpecialityLevel: _satisfySpecialityLevel);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
