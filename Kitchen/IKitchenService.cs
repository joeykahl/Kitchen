using System.Collections.Generic;

namespace Kitchen
{
	/// <summary>
	/// Provides access to the KitchenService.
	/// </summary>
	public interface IKitchenService
	{
		// The menu associated with the kitchen.
		IDictionary<DishType, MenuItem> KitchenMenu { get;}

		/// <summary>
		/// Sets the menu of the kitchen
		/// </summary>
		/// <param name="mealTime">
		/// Represents the meal time to be associated with the menu
		/// </param>
		void SetMenu (string mealTime);

		/// <summary>
		/// Process the order from the user input
		/// </summary>
		/// <returns>
		/// The final output of the order
		/// </returns>
		string ProcessOrder();
	}
}

