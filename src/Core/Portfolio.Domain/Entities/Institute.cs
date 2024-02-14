using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Институт
	/// </summary>
	public class Institute : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_faculties"/>
		/// </summary>
		public const string FacultiesField = nameof(_faculties);

		private string _fullName;
		private string _shortName;
		private List<Faculty>? _faculties;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="fullName">Полное имя</param>
		/// <param name="shortName">Сокращенное имя</param>
		public Institute(
			string fullName,
			string shortName)
		{
			FullName = fullName;
			ShortName = shortName;

			_faculties = new List<Faculty>();
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected Institute()
		{
		}

		/// <summary>
		/// Полное имя
		/// </summary>
		public string FullName
		{
			get => _fullName;
			private set => _fullName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Полное имя")
				: value;
		}

		/// <summary>
		/// Сокращенное имя
		/// </summary>
		public string ShortName
		{
			get => _shortName;
			private set => _shortName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Сокращенное имя")
				: value;
		}

		#region Navigation properties

		public IReadOnlyList<Faculty>? Faculties => _faculties;

		#endregion

		/// <summary>
		/// Получить количество портфолио Института
		/// </summary>
		/// <returns>Число портфолио</returns>
		/// <exception cref="NotIncludedException"></exception>
		public int GetCountPortfolios()
		{
			if (_faculties == null)
				throw new NotIncludedException("Факультеты");

			return _faculties
				.Select(x => x.GetCountPortfolios())
				.Sum();
		}
	}
}
