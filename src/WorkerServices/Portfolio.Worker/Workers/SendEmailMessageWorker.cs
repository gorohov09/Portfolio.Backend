using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;

namespace Portfolio.Worker.Workers
{
	/// <summary>
	/// Служба для отправки email-сообщений
	/// </summary>
	public class SendEmailMessageWorker : IWorker
	{
		private readonly IEmailService _emailService;
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="emailService">Сервис для работы с Email</param>
		/// <param name="dbContext">Контекст БД</param>
		public SendEmailMessageWorker(
			IEmailService emailService,
			IDbContext dbContext)
		{
			_dbContext = dbContext;
			_emailService = emailService;
		}

		/// <inheritdoc/>
		public async Task RunAsync()
		{
			var unsentMessages = await GetUnsentMessagesAsync();

			foreach (var message in unsentMessages)
			{
				if (await _emailService.SendEmailMessageAsync(message))
					message.IsSent = true;
			}

			await _dbContext.SaveChangesAsync();
		}

		private async Task<List<EmailMessage>> GetUnsentMessagesAsync() =>
			await _dbContext.EmailMessages
					.Where(msg => !msg.IsSent)
					.OrderByDescending(msg => msg.CreatedOn)
					.ToListAsync();
	}
}
