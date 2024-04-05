using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Уведомление пользователя
	/// </summary>
	public class Notification : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_user"/>
		/// </summary>
		public const string UserField = nameof(_user);

		private User? _user;
		private NotificationType _type;
		private string _title = default!;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="notificationType">Тип уведомления</param>
		/// <param name="user">Пользователь-получатель</param>
		public Notification(
			NotificationType notificationType,
			string title,
			string description,
			User user)
		{
			Type = notificationType;
			Title = title;
			Description = description;
			User = user;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		public Notification()
		{
		}

		/// <summary>
		/// Тип уведомления
		/// </summary>
		public NotificationType Type
		{
			get => _type;
			private set => _type = value == default
				? throw new RequiredFieldNotSpecifiedException("Тип уведомления")
				: value;
		}

		/// <summary>
		/// Идентификатор пользователя-получателя
		/// </summary>
		public Guid UserId { get; private set; }

		/// <summary>
		/// Уведомление является прочтенным
		/// </summary>
		public bool IsRead { get; set; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string Title
		{
			get => _title;
			private set => _title = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Заголовок")
				: value;
		}

		/// <summary>
		/// Описание
		/// </summary>
		public string? Description { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Пользователь-получатель
		/// </summary>
		public User? User
		{
			get => _user;
			set
			{
				if (value == null)
					throw new RequiredFieldNotSpecifiedException("Пользователь, которому отправится уведомление");

				_user = value;
				UserId = value.Id;
			}
		}

		#endregion
	}
}
