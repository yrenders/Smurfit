using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SKDVS.Utils.ErrorHandler;
namespace WindowsApplication1
{

	//--
	//-- Jeff Atwood
	//-- http://www.codinghorror.com
	//--
	static class Module1
	{

		public static void Main()
		{
			UnhandledExceptionManager.AddHandler();
			Form1 frm = new Form1();
			Application.Run(frm);
		}

	}
}
