using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Электронно-почтовое сообщение
	/// </summary>
	public class EmailMessage : EntityBase
	{
		private string _addressFrom = default!;
		private string _addressTo = default!;
		private string _subject = default!;
		private string _body = default!;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="addressFrom">Адрес отправителя</param>
		/// <param name="addressTo">Адрес получателя</param>
		/// <param name="subject">Заголовок письма</param>
		/// <param name="body">Сокращенное имя</param>
		/// <param name="isSent">Является ли сообщение отправленным</param>
		public EmailMessage(
			string addressFrom,
			string addressTo,
			string subject,
			string body,
			bool isSent = false)
		{
			AddressFrom = addressFrom;
			AddressTo = addressTo;
			Subject = subject;
			Body = body;
			IsSent = isSent;
		}

		/// <summary>
		/// Адрес отправителя
		/// </summary>
		public string AddressFrom
		{
			get => _addressFrom;
			private set => _addressFrom = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Адрес отправителя")
				: value;
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
		/// Является ли сообщение отправленным
		/// </summary>
		public bool IsSent { get; set; }
	}
}
