using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
	/// <summary>
	/// This class represents a single food order.
	/// </summary>
	public class Order
	{
		// Morning or night
		public string MealTime { get; private set;}

		// The Kitchen service that will be injected. This will provide the menu.
		private readonly IKitchenService _service;
		// This dictionary keeps track of the food ordered so far, by DishType and quantity
		private IDictionary<DishType, int> _orders;

		/// <summary>
		/// Constructor; constructs an Order object
		/// </summary>
		/// <param name="mealTime">
		/// Represents the meal time. Will be either "morning" or "night".
		/// </param>
		/// <param name="service">
		/// Represents the Kitchen service, which will be used to obtain the menu
		/// </param>
		/// <param name="desertIsServed">
		/// represents whether desert will be served.
		/// </param>
		/// <returns>
		/// Returns new Order object
		/// </returns>
		public Order (string mealTime, IKitchenService service, bool desertIsServed)
		{
			MealTime = mealTime;
			_service = service;
			SetupOrdersDictionary (desertIsServed);
		}

		/// <summary>
		/// Sets up the orders dictionary, which will keep track of the food ordered so far.
		/// </summary>
		/// <param name="desertIsServed">
		/// Represents whether desert will be served
		/// </param>
		public void SetupOrdersDictionary(bool desertIsServed)
		{
			_orders = new Dictionary<DishType, int> ();
			_orders.Add (DishType.Entree, 0);
			_orders.Add (DishType.Side, 0);
			_orders.Add (DishType.Drink, 0);
			if (desertIsServed) 
			{
				_orders.Add (DishType.Desert, 0);
			}
		}

		/// <summary>
		/// Adds dish to order.
		/// </summary>
		/// <param name="dishType">
		/// The type of dish being added to the order.
		/// </param>
		/// <exception cref="InvalidOrderException">
		/// An exception is thrown if desert is ordered in the morning, or if a food album is
		/// ordered multiple times when it is not allowed
		/// </exception>
		public void AddDishToOrder(DishType dishType)
		{
			// This checks to see whether desert is being ordered during the morning.
			if (!_orders.ContainsKey(dishType))
			{
				throw new InvalidOrderException (string.Format ("The dish type '{0}' specified does not apply to '{1}' meals.", dishType, MealTime));
			}

			// if the dish has not been ordered yet, it changes the quantity ordered to 1.
			if (_orders [dishType] == 0) 
			{
				_orders [dishType] = 1;
			}
			// if the dish has already been ordered, it checks to see if you are allowed
			// to order the dish more than once. If so, it adds 1 to the quantity.
			// Otherwise, it throws an InvalidOrderException
			else 
			{
				var menuItem = _service.KitchenMenu [dishType];
				if (!menuItem.MultipleAllowed) 
				{
					throw new InvalidOrderException (string.Format ("Multiple orders of '{0}' are not allowed", _service.KitchenMenu [dishType].Name));
				}
				_orders [dishType]++;
			}
		}

		/// <summary>
		/// Returns the correct output that represents the order. This will include an
		/// appended ", error" if specified
		/// </summary>
		/// <param name="error">
		/// If error is true, ", error" is appended to the output. Otherwise, it is not.
		/// </param>
		/// <returns>
		/// The final output of the order.
		/// </returns>
		public string GetOutput(bool error = false)
		{
			// Per the requirements of the practicum, the output must be listed in order
			// of dish type (entree, then side, then drink, then desert). Each output is
			// stored in this list, which will be joined at the end.
			var outputList = new List<string> ();

			// Iterate through each DishType enum, adds to the output list if
			// a dish of the respective DishType was ordered.
			foreach (DishType dishType in Enum.GetValues(typeof(DishType))) {
				if (_orders.ContainsKey (dishType)) 
				{
					if (_orders [dishType] != 0) 
					{
						var menuItem = _service.KitchenMenu [dishType];
						var sb = new StringBuilder ();
						sb.Append (menuItem.Name);

						// Adds a multiplier to the output if multiple dishes of the
						// same time are ordered in the same order.
						if (_orders [dishType] > 1) 
						{
							sb.AppendFormat ("({0}x)", _orders [dishType]);
						}
						outputList.Add (sb.ToString ());
					}
				}
			}

			// Appends ("error") if the order ended in an error.
			if (error) 
			{
				outputList.Add ("error");
			}

			return string.Join (", ", outputList);
		}
	}
}

