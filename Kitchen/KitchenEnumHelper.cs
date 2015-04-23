using System;

namespace Kitchen
{
	/// <summary>
	/// This static class converts the inputted dish type to the DishType enum.
	/// </summary>
	public static class KitchenEnumHelper
	{
		/// <summary>
		/// Gets corresponding DishType enum from input string.
		/// </summary>
		/// <param name="dishType">
		/// The dishType string to convert to the enum
		/// </param>
		/// <returns>
		/// The corresponding DishType enum
		/// </returns>
		public static DishType GetDishType(string dishType)
		{
			int d;
			// Throws an exception if the input is not an integer.
			bool isInt = Int32.TryParse (dishType, out d);
			if (!isInt) 
			{
				throw new InvalidDishTypeException ("Invalid dish type.");
			}
			// The switch statement tests for all four types of dishes. If it doesn't match,
			// an exception is thrown.
			switch (d) 
			{
				case 1:
					return DishType.Entree;
				case 2:
					return DishType.Side;
				case 3:
					return DishType.Drink;
				case 4:
					return DishType.Desert;
				default:
					throw new InvalidDishTypeException ("Invalid dish type.");
			}
		}
	}
}

