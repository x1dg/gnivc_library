using System;

namespace Gnivc.Test.Exceptions
{
	public class LibraryBookNotFoundException : Exception
	{
		public int BookId { get; set; }
		public LibraryBookNotFoundException(int bookId, string message = $"Book is not found") : base($"{message} Id = {bookId}")
		{
			BookId = bookId;
		}
	}
}
