using Gnivc.Test;

namespace Gnivc.Library.Data.Shared
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int Year { get; set; }
		public BookGenre Genre { get; set; }
	}
}