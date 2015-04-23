using System;

namespace Kitchen
{
	public enum DishType
	{
		Entree = '1',
		Side = '2',
		Drink = '3',
		Desert = '4'
	}

	/// <summary>
	/// This class represents a food item in the menu, i.e. steak, toast, etc.
	/// Each MenuItem has a name (i.e. steak), a DishType (i.e. entree), and
	/// a boolean that represents whether or not you are allowed to order the
	/// menu item more than once.
	/// </summary>
	public class MenuItem
	{
		public string Name { get; private set;}
		public DishType Type { get; private set;}
		public bool MultipleAllowed { get; private set;}

		/// <summary>
		/// Constructor; constructs a MenuItem object
		/// </summary>
		/// <param name="name">
		/// The name of the menu item
		/// </param>
		/// <param name="type">
		/// the type of dish
		/// </param>
		/// <param name="multipleAllowed">
		/// represents whether or not you are allowed to order the menu item more
		/// than once. Defaults to false.
		/// </param> 
		/// <returns>
		/// Returns new MenuItem object
		/// </returns>
		public MenuItem (string name, DishType type, bool multipleAllowed = false)
		{
			Name = name;
			Type = type;
			MultipleAllowed = multipleAllowed;
		}
	}
}