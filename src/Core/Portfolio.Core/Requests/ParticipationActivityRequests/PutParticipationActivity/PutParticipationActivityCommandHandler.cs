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

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		/// <param name="authorizationService">Сервис проверки прав доступа</param>
		public PutParticipationActivityCommandHandler(
			IDbContext dbContext,
			IUserContext userContext,
			IAuthorizationService authorizationService)
		{
			_dbContext = dbContext;
			_userContext = userContext;
			_authorizationService = authorizationService;
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
				and not ParticipationActivityStatus.SentRevision
				&& participationActivity.CreatedByUserId == _userContext.CurrentUserId)
				throw new ApplicationExceptionBase("Добавление и обновление информации возможно только из статусов: " +
					$"{nameof(ParticipationActivityStatus.Draft)} или {nameof(ParticipationActivityStatus.SentRevision)}");

			if (participationActivity.Status is not ParticipationActivityStatus.Submitted
				&& participationActivity.ManagerUserId == _userContext.CurrentUserId)
				throw new ApplicationExceptionBase("Добавление и обновление информации для менеджера возможно только из статусов: " +
					$"{nameof(ParticipationActivityStatus.Submitted)}");

			if (participationActivity.ManagerUserId == _userContext.CurrentUserId
				&& request.Description != participationActivity.Description
				&& request.FileId != participationActivity.ParticipationActivityDocument?.FileId)
				throw new ApplicationExceptionBase("Менеджер не может менять описание и загружать файл для участия в мероприятии");

			var activity = request.ActivityId.HasValue && request.ActivityId.Value != participationActivity.ActivityId
				? await _dbContext.Activities.FirstOrDefaultAsync(x => x.Id == request.ActivityId.Value, cancellationToken)
					?? throw new NotFoundException($"Не найдено мероприятие с Id: {request.ActivityId}")
				: null;

			var file = request.FileId != null && request.FileId != participationActivity.ParticipationActivityDocument?.FileId
				? await _dbContext.Files
					.FirstOrDefaultAsync(x => x.Id == request.FileId, cancellationToken: cancellationToken)
				: default;

			participationActivity.UpsertInformation(
				result: request.Result,
				date: request.Date,
				description: request.Description,
				activity: activity,
				file: file);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return default;
		}
	}
}
