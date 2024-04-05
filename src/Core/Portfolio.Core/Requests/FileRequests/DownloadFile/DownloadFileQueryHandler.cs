using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.FileRequests.DownloadFile;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.FileRequests.DownloadFile
{
	/// <summary>
	/// Обработчик запроса <see cref="DownloadFileQuery"/>
	/// </summary>
	public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileResponse>
	{
		private readonly IS3Service _s3Service;
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="s3Service">Хранилище файлов</param>
		/// <param name="dbContext">БД</param>
		public DownloadFileQueryHandler(IS3Service s3Service, IDbContext dbContext)
		{
			_s3Service = s3Service;
			_dbContext = dbContext;
		}

		/// <inheritdoc/>
		public async Task<DownloadFileResponse> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
		{
			if (request?.Id is null)
				throw new ArgumentNullException(nameof(request));

			var file = await _dbContext.Files
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
				?? throw new NotFoundException(nameof(request.Id));

			var fileContents = await _s3Service.GetAsync(
				key: file.Address,
				customFileName: file.FileName,
				bucket: file.Bucket,
				cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Файл с идентификатором {file.Id} не найден в хранилище файлов");

			return new DownloadFileResponse(fileContents.Content, fileContents.ContentType, file.FileName);
		}
	}
}
