using Microsoft.Extensions.Logging;

namespace Portfolio.Worker.Workers
{
	/// <summary>
	/// Тестовая служба
	/// </summary>
	public class TestWorker : IWorker
	{
		private readonly ILogger<TestWorker> _logger;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="logger">Логгер</param>
		public TestWorker(ILogger<TestWorker> logger)
			=> _logger = logger;

		/// <inheritdoc/>
		public async Task RunAsync()
		{
			_logger.LogInformation("Тестовая служба работает");
		}
	}
}
