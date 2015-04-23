using System;

namespace Kitchen
{
	/// <summary>
	/// This class represents an exception thrown when an invalid dish type (i.e. a number that is not
	/// between 1 and 4) is inputted.
	/// </summary>
	public class InvalidDishTypeException : Exception
	{
		// This property represents the order output rather than the exception message.
		public string OrderOutput { get; private set;}

		public InvalidDishTypeException(string message, string orderOutput) : base(message)
		{
			OrderOutput = OrderOutput;
		}

		public InvalidDishTypeException(string message) : base(message) {}
	}
}

