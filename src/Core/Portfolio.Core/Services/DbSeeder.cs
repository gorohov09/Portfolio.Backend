using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Сервис добавления данных в БД
	/// </summary>
	public class DbSeeder : IDbSeeder
	{
		private static readonly Type RolesEnumType = typeof(DefaultRoles);

		private readonly IReadOnlyDictionary<Guid, string> _roles = new Dictionary<Guid, string>
		{
			[DefaultRoles.StudentId] = GetDefaultValueDescription(nameof(DefaultRoles.StudentId), RolesEnumType),
		};

		/// <inheritdoc/>
		public async Task SeedAsync(IDbContext dbContext, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(dbContext);

			await SeedRolesAsync(dbContext, cancellationToken);
			await SeedRolesPrivilegesAsync(dbContext, cancellationToken);
			await SeedInstitutesAndFacultiesAsync(dbContext, cancellationToken);

			await dbContext.SaveChangesAsync(cancellationToken);
		}

		private static async Task SeedRolesPrivilegesAsync(IDbContext dbContext, CancellationToken cancellationToken)
		{
			var existRolesInDB = await dbContext.Roles
				.Include(x => x.Privileges)
				.Where(x => DefaultRoles.RolesIdsToPrivileges.Keys.Contains(x.Id))
				.ToListAsync(cancellationToken);

			existRolesInDB.ForEach(x =>
			{
				if (!DefaultRoles.RolesIdsToPrivileges.TryGetValue(x.Id, out var privileges))
					throw new ApplicationExceptionBase($"Не удалось получить список привилегий для роли {x.Id}");

				var currentPrivileges = x.Privileges!.Select(y => y.Privilege).ToList();
				currentPrivileges.AddRange(privileges);
				currentPrivileges = currentPrivileges.Distinct().ToList();

				x.UpdatePrivileges(currentPrivileges);
			});
		}

		private async Task SeedRolesAsync(IDbContext dbContext, CancellationToken cancellationToken)
		{
			var existRolesIdsInDB = await dbContext.Roles
				.Where(x => _roles.Keys.Contains(x.Id))
				.Select(x => x.Id)
				.ToListAsync(cancellationToken);

			var rolesToSeed = _roles
				.Where(x => !existRolesIdsInDB.Contains(x.Key))
				.Select(x => new Role(x.Value) { Id = x.Key })
				.ToList();

			rolesToSeed.ForEach(x =>
			{
				if (!DefaultRoles.RolesIdsToPrivileges.TryGetValue(x.Id, out var privileges))
					throw new ApplicationExceptionBase($"Не удалось получить список привилегий для роли {x.Id}");

				x.UpdatePrivileges(privileges);
			});

			await dbContext.Roles.AddRangeAsync(rolesToSeed, cancellationToken);
		}

		private async Task SeedInstitutesAndFacultiesAsync(IDbContext dbContext, CancellationToken cancellationToken)
		{
			var isExist = await dbContext.Institutes.AnyAsync(cancellationToken);

			if (!isExist)
			{
				var instituteIKTZI = new Institute("Институт компьютерных технологий и защиты информации", "ИКТЗИ");

				var facultyPMI = new Faculty("Кафедра прикладной математики и информатики", "ПМИ", instituteIKTZI);
				var facultySIB = new Faculty("Кафедра систем информационной безопасности", "СИБ", instituteIKTZI);

				await dbContext.Institutes.AddRangeAsync(instituteIKTZI);
				await dbContext.Faculties.AddRangeAsync(facultyPMI, facultySIB);
			}
		}

		private static string GetDefaultValueDescription(string fieldName, Type enumWithDefaultValue)
		{
			var memberInfo = enumWithDefaultValue.GetMember(fieldName)
				?? throw new ApplicationExceptionBase("Не удалось получить свойство");

			var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes.Length < 1 ? fieldName : ((DescriptionAttribute)attributes[0]).Description;
		}
	}
}
