namespace Portfolio.Contracts.Requests.AdminRequests.PostManager
{
	/// <summary>
	/// Ответ на запрос <see cref="PostManagerRequest"/>
	/// </summary>
	public class PostManagerResponse
	{
		/// <summary>
		/// Идентификатор менеджера
		/// </summary>
		public Guid Id { get; set; }
	}
}
