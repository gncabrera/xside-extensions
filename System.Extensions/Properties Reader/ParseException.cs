using System;

namespace System.Extensions
{
	/// <summary>
	/// An exception thrown by <see cref="JavaPropertyReader"/> when parsing
	/// a properties stream.
	/// </summary>
	public class ParseException : System.Exception
	{
		/// <summary>
		/// Construct an exception with an error message.
		/// </summary>
		/// <param name="message">A descriptive message for the exception</param>
		public ParseException( string message ): base( message )
		{
		}
	}
}
