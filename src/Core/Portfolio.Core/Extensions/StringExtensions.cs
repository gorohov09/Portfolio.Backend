using System.Text.RegularExpressions;

namespace Portfolio.Core.Extensions
{
	/// <summary>
	/// Расширения для <see cref="string"/>
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Валидация российского номера телефона. Пример: +7 (843) 213-04-40
		/// </summary>
		/// <param name="phoneNumber">Значение номера телефона</param>
		public static bool IsValidRussianPhoneNumber(this string phoneNumber)
		{
			if (phoneNumber == null)
				throw new ArgumentNullException(nameof(phoneNumber));

			var regexPattern = @"^\+7\s\(\d{3}\)\s\d{3}-\d{2}-\d{2}$";
			var regex = new Regex(regexPattern);
			return regex.IsMatch(phoneNumber);
		}

		/// <summary>
		/// Валидация почтового адреса
		/// </summary>
		/// <param name="email">Значение почтового адреса</param>
		public static bool IsValidEmailAddress(this string email)
		{
			if (email == null)
				throw new ArgumentNullException(nameof(email));

			var regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
			var regex = new Regex(regexPattern);
			return regex.IsMatch(email);
		}
	}
}
