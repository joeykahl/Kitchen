using System;
using System.Collections.Generic;
using System.Linq;

namespace Kitchen
{
	/// <summary>
	/// The KitchenService class is the crux of this practicum. It takes the initial input
	/// and creates an order
	/// </summary>
	public class KitchenService : IKitchenService
	{
		// The menu associated with the Kitchen.
		public IDictionary<DishType, MenuItem> KitchenMenu { get; private set; }

		// represents the user-inputted order
		private readonly IList<string> _order;

		// represents the Order object to be created from _order
		private Order _orderOutput;

		/// <summary>
		/// Constructor; constructs an KitchenService object
		/// </summary>
		/// <param name="order">
		/// The user-inputted order (i.e. "morning, 1, 2, 3")
		/// </param>
		/// <returns>
		/// Returns new KitchenService object
		/// </returns>
		public KitchenService (string order)
		{
			// Split the order into its keywords
			IList<string> args = order.Split (',').ToList();
			_order = new List<string> ();

			// remove spaces from order
			foreach (string arg in args) 
			{
				_order.Add(arg.Trim());
			}

			// set the menu based on the mealtime
			SetMenu (_order.First());

			// remove mealtime from order
			_order.RemoveAt (0);
		}

		/// <summary>
		/// Implementation of SetMenu from IKitchenService interface. Sets the menu according to
		/// the meal time, and creates an Order object accordingly
		/// </summary>
		/// <param name="mealTime">
		/// Represents the meal time for the menu
		/// </param>
		/// <exception cref="InvalidOrderException">
		/// Throws InvalidOrderException if anything other than "morning" or "night" is used
		/// as the meal time
		/// </exception>
		public void SetMenu(string mealTime)
		{
			// Set morning menu if applicable
			if (mealTime.Equals ("morning", StringComparison.InvariantCultureIgnoreCase)) 
			{
				KitchenMenu = Menu.MorningMenu;
				_orderOutput = new Order (mealTime, this, false);
			} 

			// Set night menu if applicable
			else if (mealTime.Equals ("night", StringComparison.InvariantCultureIgnoreCase)) 
			{
				KitchenMenu = Menu.NightMenu;
				_orderOutput = new Order (mealTime, this, true);
			} 
			else 
			{
				throw new InvalidOrderException ("Invalid meal time. Please input 'morning' or 'night'.");
			}
		}

		/// <summary>
		/// Implementation of ProcessOrder from IKitchenService interface. Processes the order
		/// from the user input
		/// </summary>
		/// <exception cref="InvalidDishTypeException">
		/// If an invalid dish type is used (i.e. any input that isn't a number from 1-4,
		/// this method catches an InvalidDishTypeExcept, then rethrows it so that it includes
		/// the order output.
		/// </exception>
		/// <exception cref="InvalidOrderException">
		/// An InvalidOrderException is thrown if no food items are listed in the order.
		/// </exception> 
		/// <returns>
		/// The final output of the order.
		/// </returns>
		public string ProcessOrder()
		{
			try
			{
				// throws exception if no food items are included in the order
				if (!_order.Any())
				{
					throw new InvalidOrderException ("Please select items from the menu.");
				}
				// iterate through each order, retrieves the DishType enum, and adds dish to
				// order. If unsuccessful, .AddDishToOrder throws an InvalidOrderExcepton.
				foreach (string dishType in _order)
				{
					DishType thisDishType = KitchenEnumHelper.GetDishType(dishType);
					_orderOutput.AddDishToOrder(thisDishType);
				}
			}
			catch (InvalidDishTypeException idtex) 
			{
				throw new InvalidOrderException (idtex.Message, _orderOutput.GetOutput (true));
			}
			catch (InvalidOrderException ioex) 
			{
				throw new InvalidOrderException (ioex.Message, _orderOutput.GetOutput (true));
			}

			// returns the final output.
			return _orderOutput.GetOutput ();
		}
	}
}

