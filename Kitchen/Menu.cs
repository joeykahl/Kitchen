using System;
using System.Collections.Generic;

namespace Kitchen
{
	/// <summary>
	/// This static class provides two menus, one for morning, one for night.
	/// </summary>
	/// <remarks>
	/// These menues are contained in two dictionaries, with the key being the
	/// DishType enum, and the value being the associated MenuItem object.
	/// </remarks>
	public static class Menu
	{
		public static IDictionary<DishType, MenuItem> MorningMenu = new Dictionary<DishType, MenuItem> 
		{
			{ DishType.Entree, new MenuItem ("eggs", DishType.Entree) },
			{ DishType.Side, new MenuItem ("toast", DishType.Side) },
			{ DishType.Drink, new MenuItem ("coffee", DishType.Drink, true) },
		};

		public static IDictionary<DishType, MenuItem> NightMenu = new Dictionary<DishType, MenuItem> 
		{
			{ DishType.Entree, new MenuItem ("steak", DishType.Entree) },
			{ DishType.Side, new MenuItem ("potato", DishType.Side, true) },
			{ DishType.Drink, new MenuItem ("wine", DishType.Drink) },
			{ DishType.Desert, new MenuItem ("cake", DishType.Desert) }
		};
	}
}