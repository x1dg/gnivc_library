using Asp.Versioning;
using Gnivc.Test.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Gnivc.Test.Helper;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Gnivc.Test.Service
{
	/// <summary>
	/// Контроллер для рассчетов
	/// </summary>
	[ApiVersion("1.0")]
	[Route("v{v:apiVersion}/library")]
	[ApiController]
	public class LibraryController : ControllerBase
	{
		private readonly ILogger _logger;
		private readonly ILibraryService _libraryService;

		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="libraryService"></param>
		public LibraryController(
			ILogger logger,
			ILibraryService libraryService)
		{
			_logger = logger;
			_libraryService = libraryService;
		}

		/// <summary>
		/// Добавление книги в библиотеку
		/// </summary>
		/// <response code="201">При успешном добавлении книги</response>
		/// <returns>Id книги</returns>
		[Route("book")]
		[HttpPost]
		[ProducesResponseType(typeof(string), 201)]
		public async Task<IActionResult> AddBook(BookQuery query, CancellationToken ct)
		{
			query.Validate();

			var id = await _libraryService.AddBook(query, ct);
			if (id == 0)
			{
				return BadRequest();
			}

			return Created($"v1.0/library/book/{id}", id.ToString());
		}
		
		/// <summary>
		/// Получение информации о книге по Id
		/// </summary>
		/// <response code="200"></response>
		/// <returns>Книга</returns>
		[Route("book/{Id}")]
		[HttpGet]
		[ProducesResponseType(typeof(string), 200)]
		public async Task<IActionResult> GetBook([FromRoute] int oId, CancellationToken ct)
		{
			var book = await _libraryService.GetBookAsync(oId, ct);
			return Ok(book);
		}
		
		/// <summary>
		/// Обновление информации о книге по orCreateQuery или создание
		/// Согласно https://developer.mozilla.org/ru/docs/Web/HTTP/Methods/PUT обновляет или создает запись
		/// </summary>
		/// <response code="201">Книга создана</response>
		/// <response code="204">Книга обновлена</response>
		/// <returns>Книга</returns>
		[Route("book")]
		[HttpPut]
		[ProducesResponseType(typeof(string), 200)]
		public async Task<IActionResult> UpdateOrCreateBook([FromBody] BookUpdateOrCreateQuery orCreateQuery, CancellationToken ct)
		{
			orCreateQuery.Validate();
			var created = await _libraryService.UpdateOrCreateBookAsync(orCreateQuery, ct);
			if (created)
			{
				return Created();
			}
			
			return NoContent();
		}
		
		/// <summary>
		/// Удаление информации о книге по id
		/// </summary>
		/// <response code="200"></response>
		/// <returns>Книга</returns>
		[Route("book/{Id}")]
		[HttpDelete]
		[ProducesResponseType(typeof(string), 204)]
		public async Task<IActionResult> DeleteBook([FromRoute] int id, CancellationToken ct)
		{
			await _libraryService.DeleteBookAsync(id, ct);
			return NoContent();
		}
		
		/// <summary>
		/// Получение информации о всех книгах
		/// </summary>
		/// <response code="200"></response>
		/// <returns>Книга</returns>
		[Route("books")]
		[HttpGet]
		[ProducesResponseType(typeof(string), 200)]
		public async Task<IActionResult> GetBooks(CancellationToken ct)
		{
			var books = await _libraryService.GetAllBooks(ct);
			return Ok(books);
		}
		
		/// <summary>
		/// Получение информации о всех книгах
		/// </summary>
		/// <response code="200"></response>
		/// <returns>Книга</returns>
		[Route("books/{title}")]
		[HttpGet]
		[ProducesResponseType(typeof(string), 200)]
		public async Task<IActionResult> GetBooksByTitle([FromRoute] string title, CancellationToken ct)
		{
			var books = await _libraryService.GetBooksByTitle(title, ct);
			return Ok(books);
		}
	}
}