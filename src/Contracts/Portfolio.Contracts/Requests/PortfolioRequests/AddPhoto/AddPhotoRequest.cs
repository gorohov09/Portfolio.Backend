using Microsoft.AspNetCore.Http;

namespace Portfolio.Contracts.Requests.PortfolioRequests.AddPhoto
{
	/// <summary>
	/// Запрос на добавление к портфолио фотографии
	/// </summary>
	public class AddPhotoRequest
	{
		/// <summary>
		/// Файл
		/// </summary>
		public IFormFile File { get; set; } = default!;
	}
}
