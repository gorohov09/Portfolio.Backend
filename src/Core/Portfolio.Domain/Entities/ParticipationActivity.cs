using Portfolio.Domain.Abstractions;
using Portfolio.Domain.DomainEvents.ParticipationActivityEvents;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Extensions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Участие в мероприятии
	/// </summary>
	public class ParticipationActivity : EntityBase, IUserTrackable
	{
		/// <summary>
		/// Поле для <see cref="_activity"/>
		/// </summary>
		public const string ActivityField = nameof(_activity);

		/// <summary>
		/// Поле для <see cref="_portfolio"/>
		/// </summary>
		public const string PortfolioField = nameof(_portfolio);

		/// <summary>
		/// Поле для <see cref="_createdByUser"/>
		/// </summary>
		public const string CreatedByUserField = nameof(_createdByUser);

		/// <summary>
		/// Поле для <see cref="_modifiedByUser"/>
		/// </summary>
		public const string ModifiedByUserField = nameof(_modifiedByUser);

		/// <summary>
		/// Поле для <see cref="_managerUser"/>
		/// </summary>
		public const string ManagerUserField = nameof(_managerUser);

		private ParticipationActivityStatus _status;
		private Guid _createdByUserId;
		private Guid _modifiedUserId;
		private Activity? _activity;
		private MyPortfolio _portfolio;
		private User? _createdByUser;
		private User? _modifiedByUser;
		private User? _managerUser;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="portfolio">Портфолио</param>
		/// <param name="activity">Мероприятие</param>
		public ParticipationActivity(
			Activity? activity = null)
		{
			Activity = activity;
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
		/// Идентификатор пользователя, создавшего сущность
		/// </summary>
		public Guid CreatedByUserId
		{
			get => _createdByUserId;
			set
			{
				if (_createdByUserId != default)
					throw new ApplicationExceptionBase("Нельзя сменить пользователя, создавшего сущность");

				_createdByUserId = value;
			}
		}

		/// <summary>
		/// Идентификатор пользователя, изменившего сущность
		/// </summary>
		public Guid ModifiedByUserId
		{
			get => _modifiedUserId;
			set
			{
				if (value != CreatedByUserId
					&& value != ManagerUserId)
					throw new ApplicationExceptionBase("Новый идентификатор не соответствует создателю и менеджеру");

				_modifiedUserId = value;
			}
		}

		/// <summary>
		/// Идентификатор пользователя, на проверку которому назначена сущность
		/// </summary>
		public Guid? ManagerUserId { get; private set; }

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
		public ParticipationActivityDocument? ParticipationActivityDocument { get; private set; }

		/// <summary>
		/// Пользователь, создавший сущность
		/// </summary>
		public User? CreatedByUser
		{
			get => _createdByUser;
			set
			{
				_createdByUser = value
					?? throw new RequiredFieldNotSpecifiedException("Пользователь, создавший сущность");
				CreatedByUserId = value.Id;
			}
		}

		/// <summary>
		/// Пользователь, изменивший сущность
		/// </summary>
		public User? ModifiedByUser
		{
			get => _modifiedByUser;
			set
			{
				_modifiedByUser = value
					?? throw new RequiredFieldNotSpecifiedException("Пользователь, изменивший сущность");
				ModifiedByUserId = value.Id;
			}
		}

		/// <summary>
		/// Пользователь менеджер, на проверку которому назначена сущность
		/// </summary>
		public User? ManagerUser
		{
			get => _modifiedByUser;
			set
			{
				if (value == null)
					throw new RequiredFieldNotSpecifiedException("Пользователь менеджер, на проверку которому назначена сущность");

				if (value.RoleId != DefaultRoles.ManagerId)
					throw new ApplicationExceptionBase("На проверку можно назначить только пользователя с ролью - Менеджер");

				_managerUser = value;
				ManagerUserId = value.Id;
			}
		}

		#endregion

		/// <summary>
		/// Подать участие в мероприятии на рассмотрение
		/// </summary>
		/// <param name="currentDate">Текущая дата</param
		public void Submit(DateTime currentDate)
		{
			if (Status is not ParticipationActivityStatus.Draft
				and not ParticipationActivityStatus.SentRevision)
				throw new ApplicationExceptionBase($"Перевести в статус {ParticipationActivityStatus.Submitted.GetDescription()} возможно только из статуса: " +
					$"{ParticipationActivityStatus.Draft.GetDescription()} или {ParticipationActivityStatus.SentRevision.GetDescription()}");

			if (Result == null
				|| Date == null
				|| Description == null
				|| Activity == null)
				throw new ApplicationExceptionBase("Необходимо заполнить все обязательные поля");

			if (ParticipationActivityDocument == null || ParticipationActivityDocument.IsDeleted)
				throw new ApplicationExceptionBase("Необходимо прикрепить подтверждающий документ");

			if (Activity.Period.EndDate > currentDate)
				throw new ApplicationExceptionBase("Невозможно подать заявку на мероприятие, которое еще не закончилось");

			if (Date < Activity.Period.StartDate || Date > Activity.Period.EndDate)
				throw new ApplicationExceptionBase("Дата участия не совпадает с датами мероприятия");

			var isRepeatSubmit = Status == ParticipationActivityStatus.SentRevision;

			Status = ParticipationActivityStatus.Submitted;

			AddDomainEvent(new ParticipationActivitySubmittedDomainEvent(this, isRepeatSubmit: isRepeatSubmit));
		}

		/// <summary>
		/// Отправить участие в мероприятии на доработку
		/// </summary>
		/// <param name="comment">Комментарий</param>
		public void SendRevision(string comment)
		{
			if (Status is not ParticipationActivityStatus.Submitted)
				throw new ApplicationExceptionBase($"Перевести в статус {ParticipationActivityStatus.SentRevision.GetDescription()} возможно только из статуса: " +
					$"{ParticipationActivityStatus.Submitted.GetDescription()}");

			if (string.IsNullOrEmpty(comment))
				throw new ApplicationExceptionBase("Необходимо описать причину отправления на доработку");

			Comment = comment;
			Status = ParticipationActivityStatus.SentRevision;
			AddDomainEvent(new ParticipationActivitySendRevisionDomainEvent(this));
		}

		/// <summary>
		/// Добавить/Обновить информацию об участии в мероприятии
		/// </summary>
		/// <param name="result">Результат</param>
		/// <param name="date">Дата участия</param>
		/// <param name="description">Описание участия</param>
		/// <param name="activity">Мероприятие</param>
		/// <param name="file">Файл подтверждающего документа участия в мероприятии</param>
		public void UpsertInformation(
			ParticipationActivityResult? result = default,
			DateTime? date = default,
			string? description = default,
			Activity? activity = default,
			File? file = default)
		{
			if (_portfolio == null)
				throw new NotIncludedException(nameof(_portfolio));

			if (result != null && Result != result)
				Result = result;
			if (date != null && Date != date.Value.ToUniversalTime())
				Date = date.Value.ToUniversalTime();
			if (description != null && Description != description)
				Description = description;
			if (activity != null
				&& ActivityId != activity.Id)
			{
				if (_portfolio.IsExistParticipationActivity(activity))
					throw new ApplicationExceptionBase("Для данного мероприятия уже существует заявка");

				Activity = activity;
			}

			if (file != null && file.Id != ParticipationActivityDocument?.FileId)
			{
				if (file.ContentType != DefaultFileExtensions.Pdf)
					throw new ApplicationExceptionBase("Файл должен быть в Pdf формате");

				if (ParticipationActivityDocument != null)
					ParticipationActivityDocument.IsDeleted = true;

				ParticipationActivityDocument = new ParticipationActivityDocument(
					participation: this,
					file: file);
			}
		}

		/// <summary>
		/// Одобрить участие в мероприятии
		/// </summary>
		/// <exception cref="ApplicationExceptionBase"></exception>
		public void Confirm()
		{
			if (Status is not ParticipationActivityStatus.Submitted)
				throw new ApplicationExceptionBase($"Перевести в статус {ParticipationActivityStatus.Approved.GetDescription()} возможно только из статуса: " +
					$"{ParticipationActivityStatus.Submitted.GetDescription()}");

			Comment = default;
			Status = ParticipationActivityStatus.Approved;
			AddDomainEvent(new ParticipationActivityConfirmedDomainEvent(this));
		}
	}
}
