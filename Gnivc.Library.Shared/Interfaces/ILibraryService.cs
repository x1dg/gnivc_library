using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gnivc.Test.Service.Interfaces;

/// <summary>
/// Сервис библиотеки
/// </summary>
public interface ILibraryService
{
    /// <summary>
    /// Сохранить книгу
    /// </summary>
    /// <param name="query"></param>
    ValueTask<int> AddBook(BookQuery query, CancellationToken ct);

    /// <summary>
    /// Получить книгу
    /// </summary>
    /// <param name="oId"></param>
    ValueTask<BookDto> GetBookAsync(int oId, CancellationToken ct);
    
    /// <summary>
    /// Изменить книгу
    /// </summary>
    /// <param name="orCreateQuery"></param>
    ValueTask<bool> UpdateOrCreateBookAsync(BookUpdateOrCreateQuery orCreateQuery, CancellationToken ct);
    
    /// <summary>
    /// Удалить книгу
    /// </summary>
    /// <param name="id"></param>
    ValueTask DeleteBookAsync(int id, CancellationToken ct);
    
    /// <summary>
    /// Получить все книги
    /// </summary>
    ValueTask<IEnumerable<BookDto>> GetAllBooks(CancellationToken ct);
    
    /// <summary>
    /// Получить все книги по совпадению заголовка
    /// </summary>
    /// <param name="title"></param>
    ValueTask<IEnumerable<BookDto>> GetBooksByTitle(string title, CancellationToken ct);
}