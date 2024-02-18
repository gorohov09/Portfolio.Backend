using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	public class Photo : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_portfolio"/>
		/// </summary>
		public const string PortfolioField = nameof(_portfolio);

		/// <summary>
		/// Поле для <see cref="_file"/>
		/// </summary>
		public const string FileField = nameof(_file);

		private MyPortfolio _portfolio;
		private File _file;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="portfolio">Портфолио</param>
		/// <param name="file">Файл</param>
		public Photo(
			MyPortfolio portfolio,
			File file,
			bool isAvatar = false)
		{
			Portfolio = portfolio;
			File = file;
			IsAvatar = isAvatar;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected Photo()
		{
		}

		/// <summary>
		/// Является ли фотография аватаркой
		/// </summary>
		public bool IsAvatar { get; private set; }

		/// <summary>
		/// Идентификатор портфолио
		/// </summary>
		public Guid PortfolioId { get; private set; }

		/// <summary>
		/// Идентификатор файла
		/// </summary>
		public Guid FileId { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Портфолио
		/// </summary>
		public MyPortfolio? Portfolio
		{
			get => _portfolio;
			set
			{
				_portfolio = value
					?? throw new RequiredFieldNotSpecifiedException("Портфолио");
				PortfolioId = value.Id;
			}
		}

		/// <summary>
		/// Файл
		/// </summary>
		public File? File
		{
			get => _file;
			set
			{
				_file = value
					?? throw new RequiredFieldNotSpecifiedException("Файл");
				FileId = value.Id;
			}
		}

		#endregion
	}
}
