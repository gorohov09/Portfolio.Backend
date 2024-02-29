using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Settings;
using Portfolio.Domain.Entities;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Сервис отправки email-сообщений
	/// </summary>
	public class EmailService : IEmailService
	{
		private readonly SmtpClient _smtpClient;
		private readonly EmailSettings _emailSettings;
		private readonly ILogger<EmailService> _logger;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="emailSettings">Настройки для отправки email-сообщений</param>
		/// <param name="logger">Логгер</param>
		public EmailService(
			EmailSettings emailSettings,
			ILogger<EmailService> logger)
		{
			_emailSettings = emailSettings;
			_logger = logger;
			_smtpClient = InitializationSmptClient();
		}

		/// <inheritdoc/>
		public async Task<bool> SendEmailMessageAsync(EmailMessage message)
		{
			var mailMessage = new MailMessage
			{
				From = new MailAddress(_emailSettings.Address),
				Subject = message.Subject,
				Body = message.Body,
			};

			mailMessage.To.Add(message.AddressTo);

			if (_emailSettings.IsSentInLog)
			{
				_logger.LogInformation($"Получатель: {message.AddressTo} Сообщение: {message.Body}.");
				return true;
			}

			try
			{
				await _smtpClient.SendMailAsync(mailMessage);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Ошибка при отправке сообщения: {ex.Message}");
				return false;
			}
		}

		private SmtpClient InitializationSmptClient() => new(_emailSettings.SmtpClient)
		{
			Credentials = new NetworkCredential(_emailSettings.Address, _emailSettings.AppPassword),
			EnableSsl = _emailSettings.EnableSsl,
			Port = _emailSettings.Port,
		};
	}
}
