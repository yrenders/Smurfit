using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

using SKDVS.Utils.ErrorHandler;
namespace ConsoleApplication1
{

	//--
	//-- Jeff Atwood
	//-- http://www.codinghorror.com
	//--
	static class Module1
	{

		public static void Main()
		{
			UnhandledExceptionManager.AddHandler(true);
			Console.WriteLine("Hello World!");
			GenerateException();
			Console.WriteLine("End of Sub Main(). Press ENTER to continue...");
			Console.ReadLine();
		}

		public static void GenerateException()
		{
			//-- just a simple "object not initialized" exception (should read "as new")
			System.Collections.Specialized.NameValueCollection x = null;
			Console.WriteLine(x.Count);
		}

	}
}
