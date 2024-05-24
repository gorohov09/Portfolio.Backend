using Microsoft.Extensions.DependencyInjection;
using Portfolio.Contracts.Messages.Email;

namespace Portfolio.Data.RabbitMq
{
	/// <summary>
	/// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
	/// </summary>
	public static class Entry
	{
		/// <summary>
		/// Добавить службы проекта с очередью
		/// </summary>
		/// <param name="services">Коллекция служб</param>
		/// <returns>Обновленная коллекция служб</returns>
		public static IServiceCollection AddRabbitMq(this IServiceCollection services, RabbitOptions rabbitOptions)
		{
			ArgumentNullException.ThrowIfNull(rabbitOptions);

			if (string.IsNullOrWhiteSpace(rabbitOptions.Url))
				throw new ArgumentException(nameof(rabbitOptions.Url));

			services.AddMassTransit(rabbitOptions.Url, options => options
				.AddProducer<EmailMessage>(exchangeName: "portfolio-email-exchange"));

			return services;
		}
	}
}
