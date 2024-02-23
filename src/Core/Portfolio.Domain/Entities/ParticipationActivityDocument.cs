using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Подтверждающий документ участия в мероприятии
	/// </summary>
	public class ParticipationActivityDocument : EntityBase
	{

		/// <summary>
		/// Поле для <see cref="_participation"/>
		/// </summary>
		public const string ParticipationField = nameof(_participation);

		/// <summary>
		/// Поле для <see cref="_file"/>
		/// </summary>
		public const string FileField = nameof(_file);

		private ParticipationActivity _participation;
		private File _file;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="participation">Участие в мероприятии</param>
		/// <param name="file">Файл</param>
		public ParticipationActivityDocument(
			ParticipationActivity participation,
			File file)
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

		/// <summary>
		/// Идентификатор файла
		/// </summary>
		public Guid FileId { get; private set; }

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

		/// <summary>
		/// Файл
		/// </summary>
		public File? File
		{
			get => _file;
			set
			{
				_file = value
					?? throw new RequiredFieldNotSpecifiedException("Файл");
				FileId = value.Id;
			}
		}

		#endregion
	}
}
