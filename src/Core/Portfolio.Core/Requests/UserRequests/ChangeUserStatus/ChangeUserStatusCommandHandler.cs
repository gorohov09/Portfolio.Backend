using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Requests.UserRequests.ChangeUserStatus
{
	public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand>
	{
		private readonly IDbContext _dbContext;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="dbContext">Контекст БД</param>
		public ChangeUserStatusCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;

		}

		public async Task<Unit> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			if (request.UserId == null)
			{
				//throw
			}

			var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
				?? throw new NotFoundException();
			if (user.Id == request.CurrentId)
			{
				throw new Exception("Нельзя заблокировать себя");
			}
			user.IsBlocked = !user.IsBlocked;
			await _dbContext.SaveChangesAsync(cancellationToken);

			return default;

		}

	}
}
