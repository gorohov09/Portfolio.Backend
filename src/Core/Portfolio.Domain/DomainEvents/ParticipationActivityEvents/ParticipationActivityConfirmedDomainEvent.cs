using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.DomainEvents.ParticipationActivityEvents
{
	/// <summary>
	/// Событие одобрения участия в мероприятии
	/// </summary>
	public class ParticipationActivityConfirmedDomainEvent : IDomainEvent
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="participation">Участие в мероприятии</param>
		public ParticipationActivityConfirmedDomainEvent(
			ParticipationActivity participation) =>
			Participation = participation ?? throw new ArgumentNullException(nameof(participation));

		/// <summary>
		/// Участие в мероприятии
		/// </summary>
		public ParticipationActivity Participation { get; private set; } = default!;

		/// <inheritdoc />
		public bool IsInTransaction => true;
	}
}
