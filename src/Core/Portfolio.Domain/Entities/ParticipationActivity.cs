using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Участие в мероприятии
	/// </summary>
	public class ParticipationActivity : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_activity"/>
		/// </summary>
		public const string ActivityField = nameof(_activity);

		/// <summary>
		/// Поле для <see cref="_participationActivityDocument"/>
		/// </summary>
		public const string ParticipationActivityDocumentField = nameof(_participationActivityDocument);

		/// <summary>
		/// Поле для <see cref="_portfolio"/>
		/// </summary>
		public const string PortfolioField = nameof(_portfolio);

		private ParticipationActivityStatus _status;
		private Activity? _activity;
		private MyPortfolio _portfolio;
		private ParticipationActivityDocument? _participationActivityDocument;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="portfolio">Портфолио</param>
		public ParticipationActivity(MyPortfolio portfolio)
		{
			Portfolio = portfolio;
			Status = ParticipationActivityStatus.Draft;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected ParticipationActivity()
		{
		}

		/// <summary>
		/// Статус
		/// </summary>
		public ParticipationActivityStatus Status
		{
			get => _status;
			private set => _status = value == default
				? throw new RequiredFieldNotSpecifiedException("Статус")
				: value;
		}

		/// <summary>
		/// Результат
		/// </summary>
		public ParticipationActivityResult? Result { get; private set; }

		/// <summary>
		/// Дата участия
		/// </summary>
		public DateTime? Date { get; private set; }

		/// <summary>
		/// Описание участия
		/// </summary>
		public string? Description { get; private set; }

		/// <summary>
		/// Комментарий от администратора
		/// </summary>
		public string? Comment { get; private set; }

		/// <summary>
		/// Идентификатор мероприятия
		/// </summary>
		public Guid? ActivityId { get; private set; }

		/// <summary>
		/// Идентификатор портфолио
		/// </summary>
		public Guid PortfolioId { get; private set; }

		/// <summary>
		/// Идентификатор подтверждающего документа участия в мероприятии
		/// </summary>
		public Guid? ParticipationActivityDocumentId { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Мероприятие
		/// </summary>
		public Activity? Activity
		{
			get => _activity;
			private set
			{
				_activity = value;
				ActivityId = value?.Id;
			}
		}

		/// <summary>
		/// Портфолио
		/// </summary>
		public MyPortfolio Portfolio
		{
			get => _portfolio;
			private set
			{
				_portfolio = value
					?? throw new RequiredFieldNotSpecifiedException("Портфолио");
				PortfolioId = value.Id;
			}
		}

		/// <summary>
		/// Подтверждающий документ участия в мероприятии
		/// </summary>
		public ParticipationActivityDocument? ParticipationActivityDocument
		{
			get => _participationActivityDocument;
			private set
			{
				_participationActivityDocument = value;
				ParticipationActivityDocumentId = value?.Id;
			}
		}

		#endregion

		/// <summary>
		/// Добавить/Обновить информацию об участии в мероприятии
		/// </summary>
		/// <param name="result">Результат</param>
		/// <param name="date">Дата участия</param>
		/// <param name="description">Описание участия</param>
		/// <param name="activity">Мероприятие</param>
		/// <param name="participationActivityDocument">Подтверждающий документ участия в мероприятии</param>
		public void UpsertInformation(
			ParticipationActivityResult? result = default,
			DateTime? date = default,
			string? description = default,
			Activity? activity = default,
			ParticipationActivityDocument? participationActivityDocument = default)
		{
			if (Status is not ParticipationActivityStatus.Draft
				and not ParticipationActivityStatus.SentRevision)
				throw new ApplicationExceptionBase("Добавление и обновление информации возможно только из статусов: " +
					$"{nameof(ParticipationActivityStatus.Draft)} или {nameof(ParticipationActivityStatus.SentRevision)}");

			if (result != null && Result != result)
				Result = result;
			if (date != null && Date != date)
				Date = date;
			if (description != null && Description != description)
				Description = description;
			if (activity != null && ActivityId != activity.Id)
				Activity = activity;
			if (participationActivityDocument != null && ParticipationActivityDocumentId != participationActivityDocument.Id)
				ParticipationActivityDocument = participationActivityDocument;
		}
	}
}
