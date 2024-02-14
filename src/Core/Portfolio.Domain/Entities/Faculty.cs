using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Кафедра
	/// </summary>
	public class Faculty : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_institute"/>
		/// </summary>
		public const string InstituteField = nameof(_institute);

		/// <summary>
		/// Поле для <see cref="_institute"/>
		/// </summary>
		public const string PortfoliosField = nameof(_portfolios);

		private string _fullName;
		private string _shortName;

		private Institute _institute;
		private List<MyPortfolio>? _portfolios;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="fullName">Полное имя</param>
		/// <param name="shortName">Сокращенное имя</param>
		public Faculty(
			string fullName,
			string shortName,
			Institute institute)
		{
			FullName = fullName;
			ShortName = shortName;
			Institute = institute;

			_portfolios = new List<MyPortfolio>();
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected Faculty()
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

		/// <summary>
		/// Идентификатор института
		/// </summary>
		public Guid InstituteId { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Институт
		/// </summary>
		public Institute? Institute
		{
			get => _institute;
			private set
			{
				_institute = value
					?? throw new RequiredFieldNotSpecifiedException("Институт");
				InstituteId = value.Id;
			}
		}

		public IReadOnlyList<MyPortfolio>? Portfolios => _portfolios;

		#endregion

		/// <summary>
		/// Получить количество портфолио Кафедры
		/// </summary>
		/// <returns>Число портфолио</returns>
		/// <exception cref="NotIncludedException"></exception>
		public int GetCountPortfolios()
		{
			if (_portfolios == null)
				throw new NotIncludedException("Портфолио");

			return _portfolios.Count;
		}

		/// <summary>
		/// Добавить портфолио к кафедре
		/// </summary>
		/// <param name="portfolio">Портфолио</param>
		/// <exception cref="NotIncludedException"></exception>
		public void AddPortfolio(MyPortfolio portfolio)
		{
			ArgumentNullException.ThrowIfNull(portfolio);

			if (_portfolios == null)
				throw new NotIncludedException("Портфолио");

			if (_portfolios.Any(x => x.Id == portfolio.Id))
				throw new ApplicationExceptionBase("Такое портфолио уже существует на кафедре");

			_portfolios.Add(portfolio);
		}
	}
}
