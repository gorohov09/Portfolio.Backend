using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.DomainEvents.UserEvents
{
	/// <summary>
	/// Событие регистрации студента
	/// </summary>
	public class StudentRegisteredDomainEvent : IDomainEvent
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="user">Пользователь</param>
		/// <exception cref="ArgumentNullException"></exception>
		public StudentRegisteredDomainEvent(
			User user,
			string lastName,
			string firstName)
		{
			User = user ?? throw new ArgumentNullException(nameof(user));
			LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
			FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
		}

		/// <summary>
		/// Пользователь
		/// </summary>
		public User User { get; private set; } = default!;

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName { get; private set; } = default!;

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName { get; private set; } = default!;

		/// <summary>
		/// Дата рождения
		/// </summary>
		public DateTime Birthday { get; private set; }

		/// <inheritdoc />
		public bool IsInTransaction => true;
	}
}
