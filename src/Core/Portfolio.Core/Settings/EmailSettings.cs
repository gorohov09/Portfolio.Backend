using Microsoft.Extensions.Configuration;

namespace Portfolio.Core.Settings
{
	/// <summary>
	/// Настройки для отправки email-сообщений
	/// </summary>
	public class EmailSettings
	{
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="configuration">Конфигурации приложения</param>
		public EmailSettings(IConfiguration configuration)
			=> _configuration = configuration;

		/// <summary>
		/// Клиент для работы с Smtp
		/// </summary>
		public string SmtpClient => _configuration["EmailSettings:SmptClient"];

		/// <summary>
		/// Адрес отправителя
		/// </summary>
		public string Address => _configuration["EmailSettings:Address"];

		/// <summary>
		/// Пароль приложения
		/// </summary>
		public string AppPassword => _configuration["EmailSettings:AppPassword"];

		/// <summary>
		/// Используется ли SSL
		/// </summary>
		public bool EnableSsl => bool.Parse(_configuration["EmailSettings:EnableSsl"]);

		/// <summary>
		/// Порт
		/// </summary>
		public int Port => int.Parse(_configuration["EmailSettings:Port"]);

		/// <summary>
		/// Является ли отправка сообщений заглушкой в лог
		/// </summary>
		public bool IsSentInLog => bool.Parse(_configuration["EmailSettings:IsSentInLog"]);
	}
}
