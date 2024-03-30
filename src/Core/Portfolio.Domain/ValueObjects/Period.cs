using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.ValueObjects
{
	/// <summary>
	/// Период
	/// </summary>
	public class Period : ValueObject
	{
		private DateTime _startDate;
		private DateTime _endDate;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="startDate">Дата начала</param>
		/// <param name="endDate">Дата окончания</param>
		public Period(
			DateTime startDate,
			DateTime endDate = default)
		{
			StartDate = startDate;
			EndDate = endDate == default ? startDate : endDate;
		}

		/// <summary>
		/// Дата начала
		/// </summary>
		public DateTime StartDate
		{
			get => _startDate;
			set => _startDate = value == default
				? throw new RequiredFieldNotSpecifiedException("Дата начала")
				: value;
		}

		/// <summary>
		/// Дата окончания
		/// </summary>
		public DateTime EndDate
		{
			get => _endDate;
			set => _endDate = value == default
				? throw new RequiredFieldNotSpecifiedException("Дата окончания")
				: value;
		}

		/// <summary>
		/// Является ли дата начала и дата окончания одним днем
		/// </summary>
		public bool IsOneDay => StartDate.Date == EndDate.Date;

		/// <inheritdoc/>
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return StartDate;
			yield return EndDate;
		}
	}
}
