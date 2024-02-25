using Newtonsoft.Json;
using Portfolio.Core.Abstractions;
using Portfolio.Core.Models;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Extensions;

namespace Portfolio.Core.Services
{
	/// <summary>
	/// Сервис по работе со специальностями
	/// </summary>
	public class SpecialityService : ISpecialityService
	{
		private readonly List<SpecialityModel> _specialities;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <exception cref="ApplicationExceptionBase"></exception>
		public SpecialityService()
		{
			var json = File.ReadAllText("specialties_data.json");

			_specialities = JsonConvert.DeserializeObject<List<SpecialityModel>>(json)
				?? throw new ApplicationExceptionBase("Не удалось распарсить файл с данными про специальноси");
		}

		/// <inheritdoc/>
		public string? GetNameByNumber(string number)
		{
			if (IsExist(number))
				return _specialities.FirstOrDefault(x => x.Code == number)!.Name;

			return default;
		}

		/// <inheritdoc/>
		public bool IsExist(string number)
			=> _specialities.Any(x => x.Code == number);

		/// <inheritdoc/>
		public bool SatisfySpecialityLevel(string number, EducationLevels educationLevel)
		{
			var speciality = _specialities.FirstOrDefault(x => x.Code == number)
				?? throw new NotFoundException($"Не удалось найти специальность по номеру: {number}");

			return educationLevel.GetDescription() == speciality.Level;
		}
	}
}
