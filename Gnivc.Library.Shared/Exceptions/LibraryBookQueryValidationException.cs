using System;

namespace Gnivc.Test.Exceptions
{
	public class LibraryBookQueryValidationException : Exception
	{
		public string ShortMessage { get; set; }
		public LibraryBookQueryValidationException(string details, string shortMessage = $"BookQuery is invalid!") : base($"{shortMessage}: {details}")
		{
			ShortMessage = shortMessage;
		}
	}
}
