using System;
using Gtk;
using CaroPoPman.Tests;

namespace CaroPoPman
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Testt();
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}

		public static void Testt() {
			var t = new TestClientData();
			t.TestManyOperations();
		}
	}
}
