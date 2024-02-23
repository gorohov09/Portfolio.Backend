using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Подтверждающий документ участия в мероприятии
	/// </summary>
	public class ParticipationActivityDocument : BaseDocument
	{
		/// <summary>
		/// Поле для <see cref="_participation"/>
		/// </summary>
		public const string ParticipationField = nameof(_participation);

		private ParticipationActivity _participation;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="participation">Участие в мероприятии</param>
		/// <param name="file">Файл</param>
		public ParticipationActivityDocument(
			ParticipationActivity participation,
			File file)
			: base(file)
		{
			Participation = participation;
			File = file;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected ParticipationActivityDocument()
		{
		}

		/// <summary>
		/// Тип подтверждающего документа участия
		/// </summary>
		public ParticipationActivityDocumentType Type { get; set; }

		/// <summary>
		/// Идентификатор участия в мероприятии
		/// </summary>
		public Guid ParticipationId { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Участие в мероприятии
		/// </summary>
		public ParticipationActivity? Participation
		{
			get => _participation;
			set
			{
				_participation = value
					?? throw new RequiredFieldNotSpecifiedException("Участие в мероприятии");
				ParticipationId = value.Id;
			}
		}

		#endregion
	}
}
