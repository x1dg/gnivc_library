using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gnivc.Library.Data.Shared;
using Gnivc.Library.Data.Shared.Interfaces;
using Gnivc.Test.Exceptions;
using Newtonsoft.Json;

namespace Gnivc.Test.Data
{
	public class LibraryContext : ILibraryContext
	{
		private const string _path = "library.json";
		private static int _lastId = 1;
		
		private static List<Book> Books { get; }

		static LibraryContext()
		{
			Books = new List<Book>();
			
			if (!File.Exists(_path))
			{
				File.Create(_path);
				return;
			}
			
			using (StreamReader r = new StreamReader(_path))
			{
				string json = r.ReadToEnd();
				if (!string.IsNullOrWhiteSpace(json))
				{
					Books = JsonConvert.DeserializeObject<List<Book>>(json);
					_lastId = Books.Last().Id + 1;
				}
			}
		}

		public async ValueTask<int> AddBook(BookQuery query, CancellationToken ct)
		{
			var book = new Book
			{
				Id = _lastId++,
				Title = query.Title,
				Author = query.Author,
				Year = query.Year,
				Genre = query.Genre
			};
			Books.Add(book);
			Save();
			return book.Id;
		}
		
		public async ValueTask<Book> GetBook(int oId, CancellationToken ct)
		{
			var book = await GetExistingBookInternal(oId, ct);

			return book;
		}
		
		public async ValueTask<bool> UpdateOrCreateBook(BookUpdateOrCreateQuery orCreateQuery, CancellationToken ct)
		{
			var bookToUpdate = await GetBook(orCreateQuery.oId, ct);
			var isNew = false;
			
			if (bookToUpdate == null)
			{
				bookToUpdate = new Book();
				bookToUpdate.Id = orCreateQuery.oId;
				isNew = true;
			}
			
			bookToUpdate.Title = orCreateQuery.Title;
			bookToUpdate.Author = orCreateQuery.Author;
			bookToUpdate.Year = orCreateQuery.Year;
			bookToUpdate.Genre = orCreateQuery.Genre;
			
			Save();
			
			return isNew;
		}
		
		public async ValueTask DeleteBook(int oId, CancellationToken ct)
		{
			var bookToDelete = await GetExistingBookInternal(oId, ct);

			Books.Remove(bookToDelete);
			
			Save();
		}

		public async ValueTask<IEnumerable<Book>> GetAllBooks(CancellationToken ct)
		{
			return Books;
		}

		public async ValueTask<IEnumerable<Book>> GetBooksByName(string title, CancellationToken ct)
		{
			return Books.Where(x => x.Title.Contains(title));
		}

		private async ValueTask<Book> GetExistingBookInternal(int oId, CancellationToken ct)
		{
			var book = Books.FirstOrDefault(x => x.Id == oId);
			
			if (book == null)
			{
				throw new LibraryBookNotFoundException(oId);
			}

			return book;
		}
		
		private async ValueTask<Book> GetBookInternal(int oId, CancellationToken ct)
		{
			return Books.FirstOrDefault(x => x.Id == oId);
		}
		
		private void Save()
		{
			File.WriteAllBytes(_path, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Books)));
		}
	}
}