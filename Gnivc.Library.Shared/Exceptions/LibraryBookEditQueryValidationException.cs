using System;

namespace Gnivc.Test.Exceptions
{
	public class LibraryBookUpdateOrCreateQueryValidationException : Exception
	{
		public string ShortMessage { get; set; }
		public LibraryBookUpdateOrCreateQueryValidationException(string details, string shortMessage = $"BookUpdateOrCreateQuery is invalid!") : base($"{shortMessage}: {details}")
		{
			ShortMessage = shortMessage;
		}
	}
}
