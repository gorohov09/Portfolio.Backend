using System.ComponentModel;

namespace Portfolio.Core.Extensions
{
	/// <summary>
	/// Расширения для <see cref="Enum"/>
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Получить описание в атрибуте <see cref="DescriptionAttribute"/>. Если его нет, то просто перечисление
		/// </summary>
		/// <param name="value">Значение перечисления</param>
		/// <returns>Описание</returns>
		public static string GetDescription(this Enum? value)
		{
			if (value == null)
				return string.Empty;

			var attribute = GetAttribute<DescriptionAttribute>(value);
			return attribute == null ? value.ToString() : attribute.Description;
		}

		/// <summary>
		/// Получить список значений Enum
		/// </summary>
		/// <typeparam name="T">Тип нужного Enum</typeparam>
		/// <returns>Cписок значений Enum</returns>
		public static List<T> GetEnumList<T>()
		{
			var array = (T[])Enum.GetValues(typeof(T));
			var list = new List<T>(array);
			return list;
		}

		private static T? GetAttribute<T>(Enum value)
			where T : Attribute
		{
			var type = value.GetType();
			var memberInfo = type.GetMember(value.ToString());
			var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
			return attributes.Length > 0
				? (T)attributes[0]
				: null;
		}
	}
}
