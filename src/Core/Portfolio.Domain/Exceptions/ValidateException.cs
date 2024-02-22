namespace Portfolio.Domain.Exceptions
{
	public class ValidateException : ApplicationExceptionBase
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		public ValidateException()
		{
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="message">Сообщение об ошибке</param>
		public ValidateException(string message)
			: base(message)
		{
		}
	}
}
