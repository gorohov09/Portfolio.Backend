using Portfolio.Core.Models;

namespace Portfolio.Core.Extensions
{
	/// <summary>
	/// Расширения <see cref="IQueryable"/>
	/// </summary>
	public static class IQueryableExtensions
	{
		/// <summary>
		/// Применить пагинацию
		/// <see cref="IPaginationQuery"/>
		/// </summary>
		/// <typeparam name="T">Тип IQueryable</typeparam>
		/// <param name="queryable">IQueryable</param>
		/// <param name="pagination">Пагинация</param>
		/// <returns>IQueryable с пагинацией</returns>
		public static IQueryable<T> SkipTake<T>(this IQueryable<T> queryable, IPaginationQuery pagination)
		{
			ArgumentNullException.ThrowIfNull(queryable);

			if (pagination == null || pagination.PageNumber <= 0 || pagination.PageSize <= 0)
				return queryable;

			return queryable
				.Skip((pagination.PageNumber - 1) * pagination.PageSize)
				.Take(pagination.PageSize);
		}
	}
}
