using Portfolio.Domain.Entities;

namespace Portfolio.Core.Abstractions
{
	/// <summary>
	/// Сервис отправки email-сообщений
	/// </summary>
	public interface IEmailService
	{
		/// <summary>
		/// Метод отправки email-сообщений
		/// </summary>
		/// <param name="message">Объект электронно-почтового сообщения</param>
		/// <returns>Успех/неуспех отправки</returns>
		Task<bool> SendEmailMessageAsync(EmailMessage message);
	}
}
