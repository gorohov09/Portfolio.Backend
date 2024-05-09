namespace Portfolio.Contracts.Messages.Email
{
	/// <summary>
	/// Сообщение об уведомлении пользователя по Email
	/// </summary>
	public class EmailMessage
	{
		public string Email { get; set; } = default!;

		public string Title { get; set; } = default!;

		public string? Description { get; set; }
	}
}
