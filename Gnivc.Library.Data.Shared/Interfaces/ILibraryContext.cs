using Gnivc.Test;

namespace Gnivc.Library.Data.Shared.Interfaces;

public interface ILibraryContext
{
    ValueTask<int> AddBook(BookQuery book, CancellationToken ct);

    ValueTask<Book> GetBook(int oId, CancellationToken ct);

    ValueTask<bool> UpdateOrCreateBook(BookUpdateOrCreateQuery orCreateQuery, CancellationToken ct);

    ValueTask DeleteBook(int oId, CancellationToken ct);
    
    ValueTask<IEnumerable<Book>> GetAllBooks(CancellationToken ct);
    
    ValueTask<IEnumerable<Book>> GetBooksByName(string title, CancellationToken ct);
}