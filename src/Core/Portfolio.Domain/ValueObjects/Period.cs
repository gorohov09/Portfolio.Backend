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
			set
			{
				if (value == default)
					throw new RequiredFieldNotSpecifiedException("Дата окончания");

				if (EndDate != default && value.ToUniversalTime() > EndDate)
					throw new ApplicationExceptionBase("Дата начала не может быть раньше даты окончания");

				_startDate = value.ToUniversalTime();
			}
		}

		/// <summary>
		/// Дата окончания
		/// </summary>
		public DateTime EndDate
		{
			get => _endDate;
			set
			{
				if (value == default)
					throw new RequiredFieldNotSpecifiedException("Дата окончания");

				if (StartDate != default && value.ToUniversalTime() < StartDate)
					throw new ApplicationExceptionBase("Дата окончания не может быть раньше даты начала");

				_endDate = value.ToUniversalTime();
			}
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
