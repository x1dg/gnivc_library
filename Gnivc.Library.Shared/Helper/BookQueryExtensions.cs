using System;
using System.Text;
using Gnivc.Test.Exceptions;

namespace Gnivc.Test.Helper
{
	/// <summary>
	/// Методы расширения для BookQuery
	/// </summary>
	public static class BookQueryExtensions
	{
		public static void Validate(this BookQuery query)
		{
			
			var builder = new StringBuilder();
			
			if (query != null)
			{
				if (string.IsNullOrWhiteSpace(query.Title))
				{
					builder.Append($"{nameof(BookQuery)}_{nameof(BookQuery.Title)} is empty!;");
				}

				if (string.IsNullOrWhiteSpace(query.Author))
				{
					builder.Append($"{nameof(BookQuery)}_{nameof(BookQuery.Author)} is empty!;");
				}
            
				if (query.Year > DateTime.Now.Year)
				{
					builder.Append($"{nameof(BookQuery)}_{nameof(BookQuery.Year)} is greater that current year!;");
				}
			}
			
            if (query == null)
            {
	            builder.Append($"{nameof(BookQuery)} is null!;");
            }
            
            var error = builder.ToString();
            if (!string.IsNullOrWhiteSpace(error))
            {
	            throw new LibraryBookQueryValidationException(error);
            }
		}
		
		public static void Validate(this BookUpdateOrCreateQuery orCreateQuery)
		{
			var builder = new StringBuilder();
			
			if (orCreateQuery != null)
			{
				if (orCreateQuery.oId <= 0)
				{
					builder.Append($"{nameof(BookUpdateOrCreateQuery)}_{nameof(BookUpdateOrCreateQuery.oId)} <= 0;");
				}
				
				if (string.IsNullOrWhiteSpace(orCreateQuery.Title))
				{
					builder.Append($"{nameof(BookUpdateOrCreateQuery)}_{nameof(BookUpdateOrCreateQuery.Title)} is empty!;");
				}

				if (string.IsNullOrWhiteSpace(orCreateQuery.Author))
				{
					builder.Append($"{nameof(BookUpdateOrCreateQuery)}_{nameof(BookUpdateOrCreateQuery.Author)} is empty!;");
				}

				if (orCreateQuery.Year > DateTime.Now.Year)
				{
					builder.Append($"{nameof(BookUpdateOrCreateQuery)}_{nameof(BookUpdateOrCreateQuery.Year)} is greater that current year!;");
				}
			}

			if (orCreateQuery == null)
			{
				builder.Append($"{nameof(BookQuery)} is null!;");
			}
			
			var error = builder.ToString();
			if (!string.IsNullOrWhiteSpace(error))
			{ 
				throw new LibraryBookUpdateOrCreateQueryValidationException(error);
			}

		}
	}
}