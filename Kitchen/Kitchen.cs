using System;

namespace Kitchen
{
	/// <summary>
	/// Kitchen is the main program. It takes the user input and transfers to the Kitchen service
	/// </summary>
	public class MyKitchen
	{
		/// <summary>
		/// Main method. Takes user input and starts the program.
		/// </summary>
		/// <param name="args">
		/// Will be "Kitchen.exe".
		/// </param>
		/// <returns>
		/// Returns an exit code. Does not exit until exit code is 0.
		/// </returns>
		public static int Main (string[] args)
		{
			Console.WriteLine ("Welcome to Joey's Kitchen.");
			int exitCode = 0;

			// Prompts user for order. Will repeat until 'exit' is entered
			do 
			{
				Console.WriteLine("Please enter your order, or enter 'exit' to exit.");
				// reads user's order (i.e. "morning, 1, 2, 3"
				string order = Console.ReadLine();
				// Begins processing the order
				exitCode = RunKitchen(order);
			} while (exitCode != 0);
			Console.WriteLine ("Thank you for visiting Joey's Kitchen!");

			return exitCode;
		}

		/// <summary>
		/// Run kitchen takes the initial user input and creates the KitchenService to process
		/// the order.
		/// </summary>
		/// <param name="order">
		/// The user input of the order
		/// </param>
		/// <returns>
		/// Returns 0 if the input is 'exit', returns 1 otherwise.
		/// </returns>
		public static int RunKitchen (string order)
		{
			try
			{
				// return 0 when user is ready to exit
				if (order.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
				{
					return 0;
				}

				// otherwise, the KitchenService is created, and the order is processed.
				var service = new KitchenService(order);
				var output = service.ProcessOrder();
				Console.WriteLine(output);
			}
			catch (InvalidDishTypeException idtex) 
			{
				Console.WriteLine (idtex.OrderOutput);
				Console.WriteLine (idtex.Message);
			}
			catch (InvalidOrderException ioex) 
			{
				Console.WriteLine (ioex.OrderOutput);
				Console.WriteLine (ioex.Message);
			}

			Console.WriteLine ();
			return 1;
		}
	}
}
