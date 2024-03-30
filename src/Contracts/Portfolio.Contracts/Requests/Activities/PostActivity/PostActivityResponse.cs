namespace Portfolio.Contracts.Requests.Activities.PostActivity
{
	/// <summary>
	/// Ответ на запрос <see cref="PostActivityRequest"/>
	/// </summary>
	public class PostActivityResponse
	{
		/// <summary>
		/// Идентификатор мероприятия
		/// </summary>
		public Guid Id { get; set; }
	}
}
