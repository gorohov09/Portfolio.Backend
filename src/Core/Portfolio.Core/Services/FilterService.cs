using Portfolio.Core.Abstractions;
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
	}
}
