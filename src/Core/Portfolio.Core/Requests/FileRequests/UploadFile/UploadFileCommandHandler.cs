using MediatR;
using Portfolio.Contracts.Requests.FileRequests.UploadFile;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.Exceptions;
using File = Portfolio.Domain.Entities.File;

namespace Portfolio.Core.Requests.FileRequests.UploadFile
{
	/// <summary>
	/// Обработчик запроса <see cref="UploadFileCommand"/>
	/// </summary>
	public class UploadFileCommandHandler
		: IRequestHandler<UploadFileCommand, UploadFileResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IS3Service _s3Service;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="s3Service">Сервис S3-хранилища</param>
		public UploadFileCommandHandler(
			IDbContext dbContext,
			IS3Service s3Service)
		{
			_dbContext = dbContext;
			_s3Service = s3Service;
		}

		/// <inheritdoc/>
		public async Task<UploadFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			if (string.IsNullOrEmpty(request.File.FileName))
				throw new ValidateException("Файл должен иметь название");

			if (request.Bucket == default)
				throw new ValidateException("Не задан бакет, куда загружать файл");

			using var stream = request.File.OpenReadStream();

			if (stream.Length <= 0)
				throw new ValidateException("Некорректный размер файла");

			var fileId = await _s3Service.UploadAsync(
				file: new FileContent(
					content: stream,
					fileName: request.File.FileName,
					contentType: request.File.ContentType,
					bucket: request.Bucket),
				cancellationToken: cancellationToken);

			var file = new File(
				address: fileId,
				name: request.File.FileName,
				size: stream.Length,
				bucket: request.Bucket,
				mimeType: request.File.ContentType);

			_dbContext.Files.Add(file);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return new UploadFileResponse
			{
				Id = file.Id,
				Name = file.FileName,
			};
		}
	}
}
