using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Core.Services;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using File = Portfolio.Domain.Entities.File;

namespace Portfolio.Core.Requests.PortfolioRequests.AddPhoto
{
	/// <summary>
	/// Обработчик запроса <see cref="AddPhotoCommand"/>
	/// </summary>
	public class AddPhotoCommandHandler
		: IRequestHandler<AddPhotoCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;
		private readonly IS3Service _s3Service;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="s3Service">Сервис S3-хранилища</param>
		public AddPhotoCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			IS3Service s3Service)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_s3Service = s3Service;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(
			AddPhotoCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);
			ArgumentNullException.ThrowIfNull(request.File);

			var portfolio = await _dbContext.Portfolios
				.Include(x => x.Photos)
				.FirstOrDefaultAsync(x => x.UserId == _userContext.CurrentUserId, cancellationToken)
				?? throw new NotFoundException($"У пользователя с Id: {_userContext.CurrentUserId} не найдено портфолио");

			await using var stream = request.File.OpenReadStream();

			var mimeType = MimeTypeMap.GetMimeType(request.File.FileName);

			var fileId = await _s3Service.UploadAsync(
				file: new FileContent(
					content: stream,
					fileName: request.File.FileName,
					contentType: mimeType,
					bucket: Buckets.Photos),
				needAutoCloseStream: false,
				cancellationToken: cancellationToken);

			var file = new File(
				address: fileId,
				name: request.File.FileName,
				size: stream.Length,
				bucket: Buckets.Photos,
				mimeType: mimeType);

			portfolio.AddPhoto(file: file, isAvatar: true);

			await _dbContext.Files.AddAsync(file, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
