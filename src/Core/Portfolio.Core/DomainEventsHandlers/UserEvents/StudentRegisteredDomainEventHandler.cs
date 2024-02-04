using MediatR;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.DomainEvents.UserEvents;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.DomainEventsHandlers.UserEvents
{
	/// <summary>
	/// Обработчик события <see cref="StudentRegisteredDomainEvent"/>
	/// </summary>
	public class StudentRegisteredDomainEventHandler : INotificationHandler<StudentRegisteredDomainEvent>
	{
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		public StudentRegisteredDomainEventHandler(IDbContext dbContext) => _dbContext = dbContext;

		/// <inheritdoc />
		public async Task Handle(
			StudentRegisteredDomainEvent notification,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(notification);

			if (notification.User is null)
				throw new ArgumentNullException(nameof(notification));

			if (notification.User.Role?.Id != DefaultRoles.StudentId)
				throw new ApplicationExceptionBase("Роль пользователя не является студентом");

			var portfolio = new MyPortfolio(
				lastName: notification.LastName,
				firstName: notification.FirstName,
				birthday: notification.Birthday,
				user: notification.User);

			await _dbContext.Portfolios.AddAsync(portfolio, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
