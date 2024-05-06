using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Сервис фильтрации
	/// </summary>
	public static class FilterService
	{
		/// <summary>
		/// Создать фильтр для участий в мероприятии
		/// </summary>
		/// <param name="query">Запрос</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <returns>Запрос с фильтром</returns>
		public static IQueryable<ParticipationActivity> Filter(this IQueryable<ParticipationActivity> query, IUserContext userContext)
		{
			ArgumentNullException.ThrowIfNull(query);

			if (userContext.CurrentUserRoleName == DefaultRoles.StudentName)
				query = query
					.Where(x => x.CreatedByUserId == userContext.CurrentUserId);
			else if (userContext.CurrentUserRoleName == DefaultRoles.ManagerName)
				query = query
					.Where(x => x.ManagerUserId == userContext.CurrentUserId);

			return query;
		}

		/// <summary>
		/// Создать фильтр для портфолио
		/// </summary>
		/// <param name="query">Запрос</param>
		/// <param name="filter">Фильтр</param>
		/// <returns>Запрос с фильтром</returns>
		public static IQueryable<MyPortfolio> Filter(
			this IQueryable<MyPortfolio> query,
			IFilterPortfolio filter)
		{
			ArgumentNullException.ThrowIfNull(query);
			ArgumentNullException.ThrowIfNull(filter);

			query = query
				.Where(x => string.IsNullOrWhiteSpace(filter.LastName)
					|| x.LastName!.ToLower().Contains(filter.LastName.ToLower()))
				.Where(x => string.IsNullOrWhiteSpace(filter.FirstName)
					|| x.FirstName!.ToLower().Contains(filter.FirstName.ToLower()))
				.Where(x => string.IsNullOrWhiteSpace(filter.Surname)
					|| x.Surname!.ToLower().Contains(filter.Surname.ToLower()))
				.Where(x => string.IsNullOrWhiteSpace(filter.Faculty)
					|| x.Faculty.ShortName!.ToLower().Contains(filter.Faculty.ToLower()))
				.Where(x => string.IsNullOrWhiteSpace(filter.Institute)
					|| x.Faculty.Institute.ShortName!.ToLower().Contains(filter.Institute.ToLower()));

			return query;
		}
	}
}
