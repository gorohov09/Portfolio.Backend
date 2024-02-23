using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Core.Services;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using File = Portfolio.Domain.Entities.File;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.PutParticipationActivity
{
	/// <summary>
	/// Обработчик запроса <see cref="PutParticipationActivityCommand"/>
	/// </summary>
	public class PutParticipationActivityCommandHandler : IRequestHandler<PutParticipationActivityCommand>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;
		private readonly IAuthorizationService _authorizationService;
		private readonly IS3Service _s3Service;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="authorizationService">Сервис проверки прав доступа</param>
		/// <param name="s3Service">Сервис S3-хранилища</param>
		public PutParticipationActivityCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			IAuthorizationService authorizationService,
			IS3Service s3Service)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_authorizationService = authorizationService;
			_s3Service = s3Service;
		}

		/// <inheritdoc/>
		public async Task<Unit> Handle(
			PutParticipationActivityCommand request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			await _authorizationService.CheckPrivilegeAsync(Privileges.ParticipationActivityUpdate, cancellationToken);

			var participationActivity = await _dbContext.Participations
				.Include(x => x.Activity)
				.Include(x => x.ParticipationActivityDocument)
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Не найдено участие в мероприятии с Id: {request.Id}");

			if (participationActivity.CreatedByUserId != _userContext.CurrentUserId
				&& participationActivity.ManagerUserId != _userContext.CurrentUserId)
				throw new ApplicationExceptionBase("Обновлять инофрмацию может только пользователь, создавший - Участие в мероприятие " +
					"или проектный менеджер");

			if (participationActivity.Status is not ParticipationActivityStatus.Draft
				and not ParticipationActivityStatus.SentRevision)
				throw new ApplicationExceptionBase("Добавление и обновление информации возможно только из статусов: " +
					$"{nameof(ParticipationActivityStatus.Draft)} или {nameof(ParticipationActivityStatus.SentRevision)}");

			var activity = request.ActivityId.HasValue && request.ActivityId.Value != participationActivity.ActivityId
				? await _dbContext.Activities.FirstOrDefaultAsync(x => x.Id == request.ActivityId.Value, cancellationToken)
					?? throw new NotFoundException($"Не найдено мероприятие с Id: {request.ActivityId}")
				: null;

			if (request.File != null)
			{
				await using var stream = request.File.OpenReadStream();

				var mimeType = MimeTypeMap.GetMimeType(request.File.FileName);

				if (mimeType != DefaultFileExtensions.Pdf)
					throw new ApplicationExceptionBase("Документ должен быть pdf формата");

				var fileId = await _s3Service.UploadAsync(
					file: new FileContent(
						content: stream,
						fileName: request.File.FileName,
						contentType: mimeType,
						bucket: Buckets.ParticipationActivityDocuments),
					needAutoCloseStream: false,
					cancellationToken: cancellationToken);

				var file = new File(
					address: fileId,
					name: request.File.FileName,
					size: stream.Length,
					mimeType: mimeType);

				participationActivity.UpsertInformation(
					result: request.Result,
					date: request.Date,
					description: request.Description,
					activity: activity,
					file: file);
			}
			else
				participationActivity.UpsertInformation(
					result: request.Result,
					date: request.Date,
					description: request.Description,
					activity: activity);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
