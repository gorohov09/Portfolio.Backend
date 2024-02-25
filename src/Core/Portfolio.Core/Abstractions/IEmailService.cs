using Portfolio.Domain.Entities;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис отправки email-сообщений
	/// </summary>
	public interface IEmailService
	{
		Task SendEmailAsync(EmailMessage message);
	}
}
