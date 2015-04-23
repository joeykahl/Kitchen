using NUnit.Framework;
using System;
using Kitchen;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace KitchenTests
{
	/// <summary>
	/// Tests the Kitchen and KitchenService clases
	/// </summary>
	[TestFixture]
	public class KitchenTests
	{	
		/// <summary>
		/// Test to make sure that inputing 'exit' exits the program.
		/// </summary>
		[Test]
		public void TestExit ()
		{
			int exitCode = MyKitchen.RunKitchen ("exit");
			Assert.AreEqual (0, exitCode);
		}

		/// <summary>
		/// Tests whether a correct morning order works as designed
		/// </summary>
		[Test]
		public void TestCorrectInputMorning()
		{
			var service = new KitchenService ("morning, 1, 2, 3");
			var output = service.ProcessOrder ();
			Assert.AreEqual ("eggs, toast, coffee", output);
		}

		/// <summary>
		/// Tests whether a correct night order works as designed
		/// </summary>
		[Test]
		public void TestCorrectInputNight()
		{
			var service = new KitchenService ("night, 1, 2, 3, 4");
			var output = service.ProcessOrder ();
			Assert.AreEqual ("steak, potato, wine, cake", output);
		}

		/// <summary>
		/// Tests whether a morning order with an allowed duplicate item works correctly
		/// </summary>
		[Test]
		public void TestCorrectUseOfDuplicateMorningItem()
		{
			var service = new KitchenService ("morning, 1, 2, 3, 3, 3");
			var output = service.ProcessOrder ();
			Assert.AreEqual ("eggs, toast, coffee(3x)", output);
		}

		/// <summary>
		/// Tests whether a night order with an allowed duplicate item works correctly
		/// </summary>
		[Test]
		public void TestCorrectUseOfDuplicateNightItem()
		{
			var service = new KitchenService ("night, 1, 2, 2, 4");
			var output = service.ProcessOrder ();
			Assert.AreEqual ("steak, potato(2x), cake", output);
		}

		/// <summary>
		/// Test whether the order output is printed in the correct order
		/// </summary>
		[Test]
		public void TestOutputIsInCorrectOrder()
		{
			var service = new KitchenService ("morning, 3, 2, 1");
			var output = service.ProcessOrder ();
			Assert.AreEqual ("eggs, toast, coffee", output);
		}

		/// <summary>
		/// Tests whether an InvalidOrderException is thrown when desert is ordered at breakfast
		/// </summary>
		[Test]
		public void TestErrorWhenDesertAtBreakfast()
		{
			try 
			{
				var service = new KitchenService("morning, 1, 2, 3, 4");
				service.ProcessOrder();
				Assert.Fail ("Exception was not thrown.");
			}
			catch (InvalidOrderException ex) 
			{
				Assert.AreEqual ("eggs, toast, coffee, error", ex.OrderOutput);
				Assert.AreEqual ("The dish type 'Desert' specified does not apply to 'morning' meals.", ex.Message);
			}
		}

		/// <summary>
		/// Tests whether an InvalidOrderException is thrown when an invalid dish type
		/// (i.e. anything other than a number from 1-4) is used
		/// </summary>
		[Test]
		public void TestErrorWhenInvalidDishTypeIsUsed()
		{
			try
			{
				var service = new KitchenService("night, 1, 2, 3, 5");
				service.ProcessOrder();
				Assert.Fail("Exception was not thrown");
			}
			catch (InvalidOrderException ex)
			{
				Assert.AreEqual ("steak, potato, wine, error", ex.OrderOutput);
				Assert.AreEqual ("Invalid dish type.", ex.Message);
			}
		}

		/// <summary>
		/// Tests whether an InvalidOrderException is thrown when a non-allowed multiple order
		/// of a dish is used.
		/// </summary>
		[Test]
		public void TestErrorWhenAskingForMultipleDishesWhenNotAllowed()
		{
			try
			{
				var service = new KitchenService("night, 1, 1, 2, 3, 5");
				service.ProcessOrder();
				Assert.Fail("Exception was not thrown");
			}
			catch (InvalidOrderException ex) 
			{
				Assert.AreEqual ("steak, error", ex.OrderOutput);
				Assert.AreEqual ("Multiple orders of 'steak' are not allowed", ex.Message);
			}
		}
	}
}

