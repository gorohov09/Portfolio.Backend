namespace Portfolio.Contracts.Requests.Activities.GetActivitiesNames
{
	/// <summary>
	/// Элемент списка для <see cref="GetActivitiesNamesResponse"/>
	/// </summary>
	public class GetActivitiesNamesResponseItem
	{
		/// <summary>
		/// Идентификатор мероприятия
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Название мероприятия
		/// </summary>
		public string Name { get; set; } = default!;
	}
}
