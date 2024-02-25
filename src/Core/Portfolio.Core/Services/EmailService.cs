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
		private readonly IDbContext _dbContext;
		private readonly EmailSettings _emailSettings;
		private readonly ILogger<EmailService> _logger;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="emailSettings">Настройки для отправки email-сообщений</param>
		/// <param name="logger">Логгер</param>
		public EmailService(
			IDbContext dbContext,
			EmailSettings emailSettings,
			ILogger<EmailService> logger)
		{
			_emailSettings = emailSettings;
			_dbContext = dbContext;
			_logger = logger;
			_smtpClient = InitializationSmptClient();
		}

		/// <inheritdoc/>
		public async Task SendEmailAsync(EmailMessage message)
		{
			var mailMessage = new MailMessage
			{
				From = new MailAddress(_emailSettings.Address),
				Subject = message.Subject,
				Body = message.Body,
			};

			mailMessage.To.Add(message.AddressTo);

			if (_emailSettings.IsSentInLog)
				_logger.LogInformation($"Получатель: {message.AddressTo} Сообщение: {message.Body}.");
			else
				await _smtpClient.SendMailAsync(mailMessage);

			message.IsSent = true;
			await _dbContext.SaveChangesAsync();
		}

		private SmtpClient InitializationSmptClient()
		{
			var smtpClient = new SmtpClient(_emailSettings.SmtpClient)
			{
				Credentials = new NetworkCredential(_emailSettings.Address, _emailSettings.AppPassword),
				EnableSsl = _emailSettings.EnableSsl,
				Port = _emailSettings.Port,
			};

			return smtpClient;
		}
	}
}
