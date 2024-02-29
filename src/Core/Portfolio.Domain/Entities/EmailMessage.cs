using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Электронно-почтовое сообщение
	/// </summary>
	public class EmailMessage : EntityBase
	{
		private string _addressTo = default!;
		private string _subject = default!;
		private string _body = default!;
		private Guid _toUserId = default!;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="addressTo">Адрес получателя</param>
		/// <param name="subject">Заголовок письма</param>
		/// <param name="body">Сокращенное имя</param>
		/// <param name="isSent">Является ли сообщение отправленным</param>
		public EmailMessage(
			string addressTo,
			string subject,
			string body,
			Guid toUserId,
			bool isSent = false)
		{
			AddressTo = addressTo;
			Subject = subject;
			Body = body;
			ToUserId = toUserId;
			IsSent = isSent;
		}

		/// <summary>
		/// Адрес получателя
		/// </summary>
		public string AddressTo
		{
			get => _addressTo;
			private set => _addressTo = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Адрес получателя")
				: value;
		}

		/// <summary>
		/// Заголовок письма
		/// </summary>
		public string Subject
		{
			get => _subject;
			private set => _subject = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Заголовок письма")
				: value;
		}

		/// <summary>
		/// Тело письма
		/// </summary>
		public string Body
		{
			get => _body;
			private set => _body = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Тело письма")
				: value;
		}

		/// <summary>
		/// Id пользователя-получателя в системе
		/// </summary>
		public Guid ToUserId
		{
			get => _toUserId;
			private set => _toUserId = value == default
				? throw new RequiredFieldNotSpecifiedException("Id пользователя-получателя в системе")
				: value;
		}

		/// <summary>
		/// Является ли сообщение отправленным
		/// </summary>
		public bool IsSent { get; set; }
	}
}
