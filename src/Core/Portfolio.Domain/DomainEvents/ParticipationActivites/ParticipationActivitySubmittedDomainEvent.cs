using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.DomainEvents.ParticipationActivites
{
	/// <summary>
	/// Событие подачи участия в мероприятии на рассмотрение
	/// </summary>
	public class ParticipationActivitySubmittedDomainEvent : IDomainEvent
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="participation">Участие в мероприятии</param>
		public ParticipationActivitySubmittedDomainEvent(ParticipationActivity participation)
			=> Participation = participation ?? throw new ArgumentNullException(nameof(participation));

		/// <summary>
		/// Участие в мероприятии
		/// </summary>
		public ParticipationActivity Participation { get; private set; } = default!;

		public bool IsInTransaction => true;
	}
}
