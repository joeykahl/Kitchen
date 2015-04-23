using System;

namespace Kitchen
{
	/// <summary>
	/// This class represents an exception thrown upon receiving an invalid order (i.e. ordering
	/// desert in the morning, ordering multiple entrees, etc.)
	/// </summary>
	public class InvalidOrderException : Exception
	{
		// This property represents the order output rather than the exception message.
		public string OrderOutput { get; private set;}

		public InvalidOrderException (string message, string orderOutput) : base(message)
		{
			OrderOutput = orderOutput;
		}

		public InvalidOrderException(string message) : base(message) {}


	}
}

