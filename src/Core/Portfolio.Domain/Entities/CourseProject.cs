using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Курсовой проект
	/// </summary>
	public class CourseProject : EntityBase
	{
		/// <summary>
		/// Поле для <see cref="_portfolio"/>
		/// </summary>
		public const string PortfolioField = nameof(_portfolio);

		private MyPortfolio _portfolio;
		private string _subjectName = default!;
		private string _topicName = default!;
		private int _semesterNumber;
		private int _scoreNumber;
		private int _pointNumber;
		private DateTime _completionDate;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="subjectName">Наименование дисциплины</param>
		/// <param name="topicName">Наименование темы</param>
		/// <param name="semesterNumber">Номер семестра</param>
		/// <param name="scoreNumber">Оценка</param>
		/// <param name="pointNumber">Количество баллов</param>
		/// <param name="completionDate">Дата сдачи</param>
		public CourseProject(
			MyPortfolio portfolio,
			string subjectName,
			string topicName,
			int semesterNumber,
			int scoreNumber,
			int pointNumber,
			DateTime completionDate)
		{
			Portfolio = portfolio;
			SubjectName = subjectName;
			TopicName = topicName;
			SemesterNumber = semesterNumber;
			ScoreNumber = scoreNumber;
			PointNumber = pointNumber;
			СompletionDate = completionDate;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		protected CourseProject()
		{
		}

		/// <summary>
		/// Наименование дисциплины
		/// </summary>
		public string SubjectName
		{
			get => _subjectName;
			private set => _subjectName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Наименование дисциплины")
				: value;
		}

		/// <summary>
		/// Наименование темы
		/// </summary>
		public string TopicName
		{
			get => _topicName;
			private set => _topicName = string.IsNullOrWhiteSpace(value)
				? throw new RequiredFieldNotSpecifiedException("Наименование темы")
				: value;
		}

		/// <summary>
		/// Номер семестра
		/// </summary>
		public int SemesterNumber
		{
			get => _semesterNumber;
			private set => _semesterNumber = value is >= 1 and <= 12
				? value
				: throw new ApplicationExceptionBase("Номер семестра некорректый");
		}

		/// <summary>
		/// Оценка(2-5)
		/// </summary>
		public int ScoreNumber
		{
			get => _scoreNumber;
			private set => _scoreNumber = value is >= 2 and <= 5
				? value
				: throw new ApplicationExceptionBase("Оценка некорректа");
		}

		/// <summary>
		/// Количество баллов(0-100)
		/// </summary>
		public int PointNumber
		{
			get => _pointNumber;
			private set => _pointNumber = value is >= 0 and <= 100
				? value
				: throw new ApplicationExceptionBase("Количество баллов некорректно");
		}

		/// <summary>
		/// Дата сдачи
		/// </summary>
		public DateTime СompletionDate
		{
			get => _completionDate;
			private set => _completionDate = value == default
				? throw new RequiredFieldNotSpecifiedException("Дата сдачи")
				: value;
		}

		/// <summary>
		/// Идентификатор портфолио
		/// </summary>
		public Guid PortfolioId { get; private set; }

		#region Navigation properties

		/// <summary>
		/// Портфолио
		/// </summary>
		public MyPortfolio? Portfolio
		{
			get => _portfolio;
			private set
			{
				_portfolio = value
					?? throw new RequiredFieldNotSpecifiedException("Портфолио");
				PortfolioId = value.Id;
			}
		}

		#endregion
	}
}
