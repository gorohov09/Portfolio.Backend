using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Contracts.Requests.ParticipationActivityRequests.GetParticipationActivityById;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.ParticipationActivityRequests.GetParticipationActivityById
{
	/// <summary>
	/// Обработчик запроса <see cref="GetParticipationActivityByIdQuery"/>
	/// </summary>
	public class GetParticipationActivityByIdQueryHandler
		: IRequestHandler<GetParticipationActivityByIdQuery, GetParticipationActivityByIdResponse>
	{
		private readonly IDbContext _dbContext;
		private readonly IUserContext _userContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		/// <param name="userContext">Контекст текущего пользователя</param>
		public GetParticipationActivityByIdQueryHandler(
			IDbContext dbContext,
			IUserContext userContext)
		{
			_dbContext = dbContext;
			_userContext = userContext;
		}

		/// <inheritdoc/>
		public async Task<GetParticipationActivityByIdResponse> Handle(
			GetParticipationActivityByIdQuery request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var participationActivity = await _dbContext.Participations
				.Include(x => x.ParticipationActivityDocument)
					.ThenInclude(x => x!.File)
				.Include(x => x.Activity)
				.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Участие в мероприятии с Id: {request.Id} не найдено");

			if (_userContext.CurrentUserId != participationActivity.CreatedByUserId
				&& _userContext.CurrentUserId != participationActivity.ManagerUserId)
				throw new ApplicationExceptionBase("Просматривать участие в мероприятии может пользователь, создавший его и проектный менеджер");

			return new GetParticipationActivityByIdResponse
			{
				Id = participationActivity.Id,
				Status = participationActivity.Status,
				Result = participationActivity.Result,
				Date = participationActivity.Date,
				Description = participationActivity.Description,
				Document = participationActivity.ParticipationActivityDocument != null
				? new GetParticipationActivityByIdResponseDocument
				{
					Id = participationActivity.ParticipationActivityDocument.File!.Id,
					Name = participationActivity.ParticipationActivityDocument.File!.FileName,
					Address = participationActivity.ParticipationActivityDocument.File.Address,
				}
				: default,
				Comment = participationActivity.Comment,
				CanEdit = GetCanEdit(participationActivity, _userContext.CurrentUserId),
			};
		}

		private static bool GetCanEdit(ParticipationActivity participation, Guid userId)
		{
			if (userId == participation.CreatedByUserId)
				return participation.Status is ParticipationActivityStatus.Draft
					or ParticipationActivityStatus.SentRevision;

			if (userId == participation.ManagerUserId)
				return participation.Status == ParticipationActivityStatus.Submitted;

			return false;
		}
	}
}
