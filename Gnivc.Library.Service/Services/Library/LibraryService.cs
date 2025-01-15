using Gnivc.Library.Data.Shared.Interfaces;
using Gnivc.Test.Service.Interfaces;
namespace Gnivc.Test.Service.Services.Mark
{
	public class LibraryService(ILibraryContext libraryContext) : ILibraryService
	{
		private readonly ILibraryContext _libraryContext = libraryContext;

		public async ValueTask<int> AddBook(BookQuery query, CancellationToken ct)
		{
			return await _libraryContext.AddBook(query, ct);
		}

		public async ValueTask<BookDto> GetBookAsync(int oId, CancellationToken ct)
		{
			var book = await _libraryContext.GetBook(oId, ct);
			return new BookDto
			{
				Title = book.Title,
				Author = book.Author,
				Year = book.Year,
				Genre = book.Genre
			};
		}
		
		public async ValueTask<bool> UpdateOrCreateBookAsync(BookUpdateOrCreateQuery orCreateQuery, CancellationToken ct)
		{
			return await _libraryContext.UpdateOrCreateBook(orCreateQuery, ct);
		}
		
		public async ValueTask DeleteBookAsync(int id, CancellationToken ct)
		{
			await _libraryContext.DeleteBook(id, ct);
		}

		public async ValueTask<IEnumerable<BookDto>> GetAllBooks(CancellationToken ct)
		{
			var books = await _libraryContext.GetAllBooks(ct);
			return books.Select(x => new BookDto
			{
				Title = x.Title,
				Author = x.Author,
				Year = x.Year,
				Genre = x.Genre
			});
		}

		public async ValueTask<IEnumerable<BookDto>> GetBooksByTitle(string title, CancellationToken ct)
		{
			var books = await _libraryContext.GetBooksByName(title, ct);
			return books.Select(x => new BookDto
			{
				Title = x.Title,
				Author = x.Author,
				Year = x.Year,
				Genre = x.Genre
			});
		}
	}
}