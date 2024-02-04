using Portfolio.Core.Abstractions;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Провайдер даты
	/// </summary>
	public class DateTimeProvider : IDateTimeProvider
	{
		/// <inheritdoc/>
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
