using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.DomainEvents.ParticipationActivityEvents
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
		/// <param name="isRepeatSubmit">Осуществляется ли повторная подача</param>
		public ParticipationActivitySubmittedDomainEvent(
			ParticipationActivity participation,
			bool isRepeatSubmit)
		{
			Participation = participation ?? throw new ArgumentNullException(nameof(participation));
			IsRepeatSubmit = isRepeatSubmit;
		}

		/// <summary>
		/// Участие в мероприятии
		/// </summary>
		public ParticipationActivity Participation { get; private set; } = default!;

		/// <summary>
		/// Осуществляется ли повторная подача
		/// </summary>
		public bool IsRepeatSubmit { get; private set; }

		/// <inheritdoc />
		public bool IsInTransaction => true;
	}
}
