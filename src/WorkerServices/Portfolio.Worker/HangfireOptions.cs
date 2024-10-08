namespace Portfolio.Worker
{
	/// <summary>
	/// Конфигурация для hangfire
	/// </summary>
	public class HangfireOptions
	{
		/// <summary>
		/// Cron для частоты тестовой службы
		/// </summary>
		public string TestCron { get; set; } = default!;

		/// <summary>
		/// Cron для частоты электронно-почтовой службы
		/// </summary>
		public string EmailMessageCron { get; set; } = default!;

		/// <summary>
		/// Показывать dashboard
		/// </summary>
		public bool DisplayDashBoard { get; set; }
	}
}
