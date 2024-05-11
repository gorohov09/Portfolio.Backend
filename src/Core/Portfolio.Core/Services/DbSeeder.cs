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
			[DefaultRoles.ManagerId] = GetDefaultValueDescription(nameof(DefaultRoles.ManagerId), RolesEnumType),
		};

		/// <inheritdoc/>
		public async Task SeedAsync(IDbContext dbContext, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(dbContext);

			await SeedRolesAsync(dbContext, cancellationToken);
			await SeedRolesPrivilegesAsync(dbContext, cancellationToken);
			await SeedInstitutesAndFacultiesAsync(dbContext, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);
			await SeedTestUsersAsync(dbContext, cancellationToken);
			await SeedActivitiesAsync(dbContext, cancellationToken);
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
				.Select(x => new Role(x.Key, x.Value))
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

		private async Task SeedTestUsersAsync(IDbContext dbContext, CancellationToken cancellationToken)
		{
			var roleManager = await dbContext.Roles
				.FirstOrDefaultAsync(x => x.Id == DefaultRoles.ManagerId, cancellationToken);

			var roleStudent = await dbContext.Roles
				.FirstOrDefaultAsync(x => x.Id == DefaultRoles.StudentId, cancellationToken);

			if (roleManager == null || roleStudent == null)
				return;

			var passwordHashService = new PasswordEncryptionService();

			var passwordHash = passwordHashService.EncodePassword("123456");

			var user = new User(
				lastName: "Менеджеров",
				firstName: "Менеджер",
				login: "manager",
				passwordHash: passwordHash,
				email: "manager@mail.ru",
				role: roleManager);

			var user2 = new User(
				lastName: "Менеджеров2",
				firstName: "Менеджер2",
				login: "manager2",
				passwordHash: passwordHash,
				email: "manager2@mail.ru",
				role: roleManager);

			var user3 = new User(
				lastName: "Тестов",
				firstName: "Тест",
				login: "test",
				passwordHash: passwordHash,
				email: "test@mail.ru",
				role: roleStudent);

			if (await dbContext.Users.AnyAsync(
				x => x.Login == user.Login || x.Login == user2.Login || x.Login == user3.Login,
				cancellationToken: cancellationToken))
				return;

			await dbContext.Users.AddRangeAsync(user, user2, user3);
		}

		private async Task SeedActivitiesAsync(IDbContext dbContext, CancellationToken cancellationToken)
		{
			var activity = new Activity(
				name: "Олимпиада - Я профессионал",
				section: ActivitySections.ScientificAndEducational,
				type: ActivityTypes.Olympiad,
				level: ActivityLevel.Country,
				startDate: new DateTime(2023, 9, 1).ToUniversalTime(),
				endDate: new DateTime(2024, 5, 30).ToUniversalTime());

			var activity2 = new Activity(
				name: "Крылья России",
				section: ActivitySections.ScientificAndEducational,
				type: ActivityTypes.Сonference,
				level: ActivityLevel.Country,
				startDate: new DateTime(2023, 9, 1).ToUniversalTime(),
				endDate: new DateTime(2023, 9, 3).ToUniversalTime());

			if (!await dbContext.Activities.AnyAsync(cancellationToken: cancellationToken))
			{
				await dbContext.Activities.AddRangeAsync(activity, activity2);
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
