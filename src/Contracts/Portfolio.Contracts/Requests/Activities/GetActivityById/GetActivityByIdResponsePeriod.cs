namespace Portfolio.Contracts.Requests.Activities.GetActivityById
{
	/// <summary>
	/// Период мероприятия для <see cref="GetActivityByIdResponse"/>
	/// </summary>
	public class GetActivityByIdResponsePeriod
	{
		/// <summary>
		/// Дата начала
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Дата окончания
		/// </summary>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Является ли дата начала и дата окончания одним днем
		/// </summary>
		public bool IsOneDay { get; set; }
	}
}
