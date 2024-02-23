using Portfolio.Domain.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Domain.Entities
{
	/// <summary>
	/// Базовый документ
	/// </summary>
	public class BaseDocument : EntityBase, ISoftDeletable
	{
		/// <summary>
		/// Поле для <see cref="_file"/>
		/// </summary>
		public const string FileField = nameof(_file);

		private File _file;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="file">Файл</param>
		public BaseDocument(
			File file)
			=> File = file;

		/// <summary>
		/// Конструктор
		/// </summary>
		protected BaseDocument()
		{
		}

		/// <inheritdoc/>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Идентификатор файла
		/// </summary>
		public Guid FileId { get; private set; }

		#region Navigation properties

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
