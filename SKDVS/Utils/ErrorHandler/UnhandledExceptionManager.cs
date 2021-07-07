using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Runtime.InteropServices;
namespace SKDVS.Utils.ErrorHandler
{

	//--
	//-- Generic UNHANDLED error handling class
	//--
	//-- Intended as a last resort for errors which crash our application, so we can get feedback on what
	//-- caused the error.
	//-- 
	//-- To use: UnhandledExceptionManager.AddHandler() in the STARTUP of your application
	//--
	//-- more background information on Exceptions at:
	//--   http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
	//--
	//--
	//-- Jeff Atwood
	//-- http://www.codinghorror.com
	//--
	public class UnhandledExceptionManager
	{

		private UnhandledExceptionManager()
		{
			// to keep this class from being creatable as an instance.
		}

		private static bool _blnLogToEventLog;
		private static bool _blnLogToFile;
		private static bool _blnLogToEmail;
		private static bool _blnLogToScreenshot;

		private static bool _blnLogToUI;
		private static bool _blnLogToFileOK;
		private static bool _blnLogToEmailOK;
		private static bool _blnLogToScreenshotOK;

		private static bool _blnLogToEventLogOK;
		private static bool _blnEmailIncludeScreenshot;
		private static System.Drawing.Imaging.ImageFormat _ScreenshotImageFormat = System.Drawing.Imaging.ImageFormat.Png;
		private static string _strScreenshotFullPath;

		private static string _strLogFullPath;
		private static bool _blnConsoleApp;
		private static System.Reflection.Assembly _objParentAssembly = null;
		private static string _strException;

		private static string _strExceptionType;
		private static bool _blnIgnoreDebugErrors;

		private static bool _blnKillAppOnException;
		private const string _strLogName = "UnhandledExceptionLog.txt";
		private const string _strScreenshotName = "UnhandledException";

		private const string _strClassName = "UnhandledExceptionManager";
		#region "Properties"

		public static bool IgnoreDebugErrors {
			get { return _blnIgnoreDebugErrors; }
			set { _blnIgnoreDebugErrors = value; }
		}

		public static bool DisplayDialog {
			get { return _blnLogToUI; }
			set { _blnLogToUI = value; }
		}

		public static bool EmailScreenshot {
			get { return _blnEmailIncludeScreenshot; }
			set { _blnEmailIncludeScreenshot = value; }
		}

		public static bool KillAppOnException {
			get { return _blnKillAppOnException; }
			set { _blnKillAppOnException = value; }
		}

		public static System.Drawing.Imaging.ImageFormat ScreenshotImageFormat {
			get { return _ScreenshotImageFormat; }
			set { _ScreenshotImageFormat = value; }
		}

		public static bool LogToFile {
			get { return _blnLogToFile; }
			set { _blnLogToFile = value; }
		}

		public static bool LogToEventLog {
			get { return _blnLogToEventLog; }
			set { _blnLogToEventLog = value; }
		}

		public static bool SendEmail {
			get { return _blnLogToEmail; }
			set { _blnLogToEmail = value; }
		}

		public static bool TakeScreenshot {
			get { return _blnLogToScreenshot; }
			set { _blnLogToScreenshot = value; }
		}
		[DllImport("gdi32", EntryPoint = "BitBlt", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

		#endregion

		#region "win32api screenshot calls"

		//--
		//-- Windows API calls necessary to support screen capture
		//--
		private static extern int BitBlt(int hDestDC, int x, int y, int nWidth, int nHeight, int hSrcDC, int xSrc, int ySrc, int dwRop);
		[DllImport("user32", EntryPoint = "GetDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

		private static extern int GetDC(int hwnd);
		[DllImport("user32", EntryPoint = "ReleaseDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

		private static extern int ReleaseDC(int hwnd, int hdc);
		#endregion

		private static System.Reflection.Assembly ParentAssembly()
		{
			if (_objParentAssembly == null) {
				if (System.Reflection.Assembly.GetEntryAssembly() == null) {
					_objParentAssembly = System.Reflection.Assembly.GetCallingAssembly();
				} else {
					_objParentAssembly = System.Reflection.Assembly.GetEntryAssembly();
				}
			}
			return _objParentAssembly;
		}

		//--
		//-- load some settings that may optionally be present in our .config file
		//-- if they aren't present, we get the defaults as defined here
		//--
		private static void LoadConfigSettings()
		{
			SendEmail = GetConfigBoolean("SendEmail", true);
			TakeScreenshot = GetConfigBoolean("TakeScreenshot", true);
			EmailScreenshot = GetConfigBoolean("EmailScreenshot", true);
			LogToEventLog = GetConfigBoolean("LogToEventLog", false);
			LogToFile = GetConfigBoolean("LogToFile", true);
			DisplayDialog = GetConfigBoolean("DisplayDialog", true);
			IgnoreDebugErrors = GetConfigBoolean("IgnoreDebug", true);
			KillAppOnException = GetConfigBoolean("KillAppOnException", true);
		}

		//--
		//-- This *MUST* be called early in your application to set up global error handling
		//--
		public static void AddHandler(bool blnConsoleApp = false)
		{
			//-- attempt to load optional settings from .config file
			LoadConfigSettings();

			//-- we don't need an unhandled exception handler if we are running inside
			//-- the vs.net IDE; it is our "unhandled exception handler" in that case
			if (_blnIgnoreDebugErrors) {
				if (Debugger.IsAttached)
					return;
			}

			//-- track the parent assembly that set up error handling
			//-- need to call this NOW so we set it appropriately; otherwise
			//-- we may get the wrong assembly at exception time!
			ParentAssembly();

			//-- for winforms applications
			Application.ThreadException -= ThreadExceptionHandler;
			Application.ThreadException += ThreadExceptionHandler;

			//-- for console applications
			System.AppDomain.CurrentDomain.UnhandledException -= UnhandledExceptionHandler;
			System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

			//-- I cannot find a good way to programatically detect a console app, so that must be specified.
			_blnConsoleApp = blnConsoleApp;

		}

		//--
		//-- handles Application.ThreadException event
		//--
		private static void ThreadExceptionHandler(System.Object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			GenericExceptionHandler(e.Exception);
		}


		//--
		//-- handles AppDomain.CurrentDoamin.UnhandledException event
		//--
		private static void UnhandledExceptionHandler(System.Object sender, UnhandledExceptionEventArgs args)
		{
			Exception objException = (Exception)args.ExceptionObject;
			GenericExceptionHandler(objException);
		}

		//--
		//-- exception-safe file attrib retrieval; we don't care if this fails
		//--
		private static DateTime AssemblyFileTime(System.Reflection.Assembly objAssembly)
		{
			try {
				return System.IO.File.GetLastWriteTime(objAssembly.Location);
			} catch (Exception ex) {
				return DateTime.MaxValue;
			}
		}

		//--
		//-- returns build datetime of assembly
		//-- assumes default assembly value in AssemblyInfo:
		//-- <Assembly: AssemblyVersion("1.0.*")> 
		//--
		//-- filesystem create time is used, if revision and build were overridden by user
		//--
		private static DateTime AssemblyBuildDate(System.Reflection.Assembly objAssembly, bool blnForceFileDate = false)
		{
			System.Version objVersion = objAssembly.GetName().Version;
			DateTime dtBuild = default(DateTime);

			if (blnForceFileDate) {
				dtBuild = AssemblyFileTime(objAssembly);
			} else {
				dtBuild = (Convert.ToDateTime("01/01/2000").AddDays(objVersion.Build).AddSeconds(objVersion.Revision * 2));
				if (TimeZone.IsDaylightSavingTime(DateTime.Now, TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year))) {
					dtBuild = dtBuild.AddHours(1);
				}
				if (dtBuild > DateTime.Now | objVersion.Build < 730 | objVersion.Revision == 0) {
					dtBuild = AssemblyFileTime(objAssembly);
				}
			}

			return dtBuild;
		}

		//--
		//-- turns a single stack frame object into an informative string
		//--
		private static string StackFrameToString(StackFrame sf)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			int intParam = 0;
			MemberInfo mi = sf.GetMethod();

			var _with1 = sb;
			//-- build method name
			_with1.Append("   ");
			_with1.Append(mi.DeclaringType.Namespace);
			_with1.Append(".");
			_with1.Append(mi.DeclaringType.Name);
			_with1.Append(".");
			_with1.Append(mi.Name);

			//-- build method params
			ParameterInfo[] objParameters = sf.GetMethod().GetParameters();
			ParameterInfo objParameter = null;
			_with1.Append("(");
			intParam = 0;
			foreach (ParameterInfo objParameter_loopVariable in objParameters) {
				objParameter = objParameter_loopVariable;
				intParam += 1;
				if (intParam > 1)
					_with1.Append(", ");
				_with1.Append(objParameter.Name);
				_with1.Append(" As ");
				_with1.Append(objParameter.ParameterType.Name);
			}
			_with1.Append(")");
			_with1.Append(Environment.NewLine);

			//-- if source code is available, append location info
			_with1.Append("       ");
			if (sf.GetFileName() == null || sf.GetFileName().Length == 0) {
				_with1.Append(System.IO.Path.GetFileName(ParentAssembly().CodeBase));
				//-- native code offset is always available
				_with1.Append(": N ");
				_with1.Append(string.Format("{0:#00000}", sf.GetNativeOffset()));

			} else {
				_with1.Append(System.IO.Path.GetFileName(sf.GetFileName()));
				_with1.Append(": line ");
				_with1.Append(string.Format("{0:#0000}", sf.GetFileLineNumber()));
				_with1.Append(", col ");
				_with1.Append(string.Format("{0:#00}", sf.GetFileColumnNumber()));
				//-- if IL is available, append IL location info
				if (sf.GetILOffset() != StackFrame.OFFSET_UNKNOWN) {
					_with1.Append(", IL ");
					_with1.Append(string.Format("{0:#0000}", sf.GetILOffset()));
				}
			}
			_with1.Append(Environment.NewLine);
			return sb.ToString();
		}

		//--
		//-- enhanced stack trace generator
		//--
		private static string EnhancedStackTrace(StackTrace objStackTrace, string strSkipClassName = "")
		{
			int intFrame = 0;

			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			sb.Append(Environment.NewLine);
			sb.Append("---- Stack Trace ----");
			sb.Append(Environment.NewLine);

			for (intFrame = 0; intFrame <= objStackTrace.FrameCount - 1; intFrame++) {
				StackFrame sf = objStackTrace.GetFrame(intFrame);
				MemberInfo mi = sf.GetMethod();

				if (!string.IsNullOrEmpty(strSkipClassName) && mi.DeclaringType.Name.IndexOf(strSkipClassName) > -1) {
					//-- don't include frames with this name
				} else {
					sb.Append(StackFrameToString(sf));
				}
			}
			sb.Append(Environment.NewLine);

			return sb.ToString();
		}

		//--
		//-- enhanced stack trace generator (exception)
		//--
		private static string EnhancedStackTrace(Exception objException)
		{
			StackTrace objStackTrace = new StackTrace(objException, true);
			return EnhancedStackTrace(objStackTrace);
		}

		//--
		//-- enhanced stack trace generator (no params)
		//--
		private static string EnhancedStackTrace()
		{
			StackTrace objStackTrace = new StackTrace(true);
			return EnhancedStackTrace(objStackTrace, "ExceptionManager");
		}

		//--
		//-- generic exception handler; the various specific handlers all call into this sub
		//--

		private static void GenericExceptionHandler(Exception objException)
		{
			//-- turn the exception into an informative string
			try {
				_strException = ExceptionToString(objException);
				_strExceptionType = objException.GetType().FullName;
			} catch (Exception ex) {
				_strException = "Error '" + ex.Message + "' while generating exception string";
				_strExceptionType = "";
			}

			if (!_blnConsoleApp) {
				Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			}

			//-- log this error to various locations
			try {
				//-- screenshot takes around 1 second
				if (_blnLogToScreenshot)
					ExceptionToScreenshot();
				//-- event logging takes < 100ms
				if (_blnLogToEventLog)
					ExceptionToEventLog();
				//-- textfile logging takes < 50ms
				if (_blnLogToFile)
					ExceptionToFile();
				//-- email takes under 1 second
				if (_blnLogToEmail)
					ExceptionToEmail();
			} catch (Exception ex) {
				//-- generic catch because any exceptions inside the UEH
				//-- will cause the code to terminate immediately
			}

			if (!_blnConsoleApp) {
				Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
			//-- display message to the user
			if (_blnLogToUI)
				ExceptionToUI();

			if (_blnKillAppOnException) {
				KillApp();
				Application.Exit();
			}

		}

		//--
		//-- This is in a private routine for .NET security reasons
		//-- if this line of code is in a sub, the entire sub is tagged as full trust
		//--
		private static void KillApp()
		{
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

		//--
		//-- turns exception into something an average user can hopefully
		//-- understand; still very technical
		//--
		private static string FormatExceptionForUser(bool blnConsoleApp)
		{
			System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
			string strBullet = null;
			if (blnConsoleApp) {
				strBullet = "-";
			} else {
				strBullet = "•";
			}

			var _with2 = objStringBuilder;
			if (!blnConsoleApp) {
				_with2.Append("The development team was automatically notified of this problem. ");
				_with2.Append("If you need immediate assistance, contact (contact).");
			}
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			_with2.Append("The following information about the error was automatically captured: ");
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			if (_blnLogToScreenshot) {
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				if (_blnLogToScreenshotOK) {
					_with2.Append("a screenshot was taken of the desktop at:");
					_with2.Append(Environment.NewLine);
					_with2.Append("   ");
					_with2.Append(_strScreenshotFullPath);
				} else {
					_with2.Append("a screenshot could NOT be taken of the desktop.");
				}
				_with2.Append(Environment.NewLine);
			}
			if (_blnLogToEventLog) {
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				if (_blnLogToEventLogOK) {
					_with2.Append("an event was written to the application log");
				} else {
					_with2.Append("an event could NOT be written to the application log");
				}
				_with2.Append(Environment.NewLine);
			}
			if (_blnLogToFile) {
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				if (_blnLogToFileOK) {
					_with2.Append("details were written to a text log at:");
				} else {
					_with2.Append("details could NOT be written to the text log at:");
				}
				_with2.Append(Environment.NewLine);
				_with2.Append("   ");
				_with2.Append(_strLogFullPath);
				_with2.Append(Environment.NewLine);
			}
			if (_blnLogToEmail) {
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				_with2.Append("attempted to send an email to: ");
				_with2.Append(Environment.NewLine);
				_with2.Append("   ");
				_with2.Append(GetConfigString("EmailTo"));
				_with2.Append(Environment.NewLine);
			}
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			_with2.Append("Detailed error information follows:");
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			_with2.Append(_strException);
			return objStringBuilder.ToString();
		}

		//--
		//-- display a dialog to the user; otherwise we just terminate with no alert at all!
		//--

		private static void ExceptionToUI()
		{
			const string _strWhatHappened = "There was an unexpected error in (app). This may be due to a programming bug.";
			string _strHowUserAffected = null;
			const string _strWhatUserCanDo = "Restart (app), and try repeating your last action. Try alternative methods of performing the same action.";

			if (UnhandledExceptionManager.KillAppOnException) {
				_strHowUserAffected = "When you click OK, (app) will close.";
			} else {
				_strHowUserAffected = "The action you requested was not performed.";
			}

			if (!_blnConsoleApp) {
				//-- don't send ANOTHER email if we are already doing so!
				HandledExceptionManager.EmailError = !SendEmail;
				//-- pop the dialog
				HandledExceptionManager.ShowDialog(_strWhatHappened, _strHowUserAffected, _strWhatUserCanDo, FormatExceptionForUser(false), MessageBoxButtons.OK, MessageBoxIcon.Stop);
			} else {
				//-- note that writing to console pauses for ENTER
				//-- otherwise console window just terminates immediately
				ExceptionToConsole();
			}
		}
		//--
		//-- for non-web hosted apps, returns:
		//--   "[path]\bin\YourAssemblyName."
		//-- for web hosted apps, returns URL with non-filesystem chars removed:
		//--   "c:\http___domain\path\YourAssemblyName."
		private static string GetApplicationPath()
		{
			if (ParentAssembly().CodeBase.StartsWith("http://")) {
				return "c:\\" + Regex.Replace(ParentAssembly().CodeBase, "[\\/\\\\\\:\\*\\?\\\"\\<\\>\\|]", "_") + ".";
			} else {
				return System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName + ".";
			}
		}


		//--
		//-- take a desktop screenshot of our exception
		//-- note that this fires BEFORE the user clicks on the OK dismissing the crash dialog
		//-- so the crash dialog itself will not be displayed
		//--
		private static void ExceptionToScreenshot()
		{
			//-- note that screenshotname does NOT include the file type extension
			try {
				TakeScreenshotPrivate(GetApplicationPath() + _strScreenshotName);
				_blnLogToScreenshotOK = true;
			} catch (Exception ex) {
				_blnLogToScreenshotOK = false;
			}
		}

		//-- 
		//-- write an exception to the Windows NT event log
		//--
		private static void ExceptionToEventLog()
		{
			try {
				System.Diagnostics.EventLog.WriteEntry(System.AppDomain.CurrentDomain.FriendlyName, Environment.NewLine + _strException, EventLogEntryType.Error);
				_blnLogToEventLogOK = true;
			} catch (Exception ex) {
				_blnLogToEventLogOK = false;
			}
		}

		//-- 
		//-- write an exception to the console
		//--
		private static void ExceptionToConsole()
		{
			Console.WriteLine("This application encountered an unexpected problem.");
			Console.WriteLine(FormatExceptionForUser(true));
			Console.WriteLine("The application must now terminate. Press ENTER to continue...");
			Console.ReadLine();
		}
		//--
		//-- write an exception to a text file
		//--
		private static void ExceptionToFile()
		{
			_strLogFullPath = GetApplicationPath() + _strLogName;
			try {
				System.IO.StreamWriter objStreamWriter = new System.IO.StreamWriter(_strLogFullPath, true);
				objStreamWriter.Write(_strException);
				objStreamWriter.WriteLine();
				objStreamWriter.Close();
				_blnLogToFileOK = true;
			} catch (Exception ex) {
				_blnLogToFileOK = false;
			}
		}


		//--
		//-- this is the code that executes in the spawned thread
		//--
		private static void ThreadHandler()
		{
			SimpleMail.SMTPClient objMail = new SimpleMail.SMTPClient();
			SimpleMail.SMTPMailMessage objMailMessage = new SimpleMail.SMTPMailMessage();
			var _with3 = objMailMessage;
			_with3.To = GetConfigString("EmailTo", "");
			_with3.Subject = "Unhandled Exception notification - " + _strExceptionType;
			_with3.Body = _strException;
			if (_blnLogToScreenshot & _blnEmailIncludeScreenshot) {
				_with3.AttachmentPath = _strScreenshotFullPath;
			}
			try {
				objMail.SendMail(objMailMessage);
				_blnLogToEmailOK = true;
			} catch (Exception e) {
				_blnLogToEmailOK = false;
				//-- don't do anything; sometimes SMTP isn't available, which generates an exception
				//-- and an exception in the unhandled exception manager.. is bad news.
				//--MsgBox("exception email failed to send:" + Environment.Newline + Environment.Newline + e.Message)
			}
		}

		//--
		//-- send an exception via email
		//--
		private static void ExceptionToEmail()
		{
			//-- spawn off the email send attempt as a thread for improved throughput
			Thread objThread = new Thread(new ThreadStart(ThreadHandler));
			objThread.Name = "SendExceptionEmail";
			objThread.Start();
		}

		//--
		//-- exception-safe WindowsIdentity.GetCurrent retrieval returns "domain\username"
		//-- per MS, this sometimes randomly fails with "Access Denied" particularly on NT4
		//--
		private static string CurrentWindowsIdentity()
		{
			try {
				return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			} catch (Exception ex) {
				return "";
			}
		}
		//--
		//-- exception-safe "domain\username" retrieval from Environment
		//--
		private static string CurrentEnvironmentIdentity()
		{
			try {
				return System.Environment.UserDomainName + "\\" + System.Environment.UserName;
			} catch (Exception ex) {
				return "";
			}
		}
		//--
		//-- retrieve identity with fallback on error to safer method
		//--
		private static string UserIdentity()
		{
			string strTemp = null;
			strTemp = CurrentWindowsIdentity();
			if (string.IsNullOrEmpty(strTemp)) {
				strTemp = CurrentEnvironmentIdentity();
			}
			return strTemp;
		}


		//--
		//-- gather some system information that is helpful to diagnosing
		//-- exception
		//--
		static internal string SysInfoToString(bool blnIncludeStackTrace = false)
		{
			System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();

			var _with4 = objStringBuilder;

			_with4.Append("Date and Time:         ");
			_with4.Append(DateTime.Now);
			_with4.Append(Environment.NewLine);

			_with4.Append("Machine Name:          ");
			try {
				_with4.Append(Environment.MachineName);
			} catch (Exception e) {
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);

			_with4.Append("IP Address:            ");
			_with4.Append(GetCurrentIP());
			_with4.Append(Environment.NewLine);

			_with4.Append("Current User:          ");
			_with4.Append(UserIdentity());
			_with4.Append(Environment.NewLine);
			_with4.Append(Environment.NewLine);

			_with4.Append("Application Domain:    ");
			try {
				_with4.Append(System.AppDomain.CurrentDomain.FriendlyName);
			} catch (Exception e) {
				_with4.Append(e.Message);
			}


			_with4.Append(Environment.NewLine);
			_with4.Append("Assembly Codebase:     ");
			try {
				_with4.Append(ParentAssembly().CodeBase);
			} catch (Exception e) {
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);

			_with4.Append("Assembly Full Name:    ");
			try {
				_with4.Append(ParentAssembly().FullName);
			} catch (Exception e) {
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);

			_with4.Append("Assembly Version:      ");
			try {
                _with4.Append(ParentAssembly().GetName().Version.ToString());
			} catch (Exception e) {
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);

			_with4.Append("Assembly Build Date:   ");
			try {
				_with4.Append(AssemblyBuildDate(ParentAssembly()).ToString());
			} catch (Exception e) {
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);
			_with4.Append(Environment.NewLine);

			if (blnIncludeStackTrace) {
				_with4.Append(EnhancedStackTrace());
			}


			return objStringBuilder.ToString();
		}


		//--
		//-- translate exception object to string, with additional system info
		//--
		static internal string ExceptionToString(Exception objException)
		{
			System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();

			if ((objException.InnerException != null)) {
				//-- sometimes the original exception is wrapped in a more relevant outer exception
				//-- the detail exception is the "inner" exception
				//-- see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
				var _with5 = objStringBuilder;
				_with5.Append("(Inner Exception)");
				_with5.Append(Environment.NewLine);
				_with5.Append(ExceptionToString(objException.InnerException));
				_with5.Append(Environment.NewLine);
				_with5.Append("(Outer Exception)");
				_with5.Append(Environment.NewLine);
			}
			var _with6 = objStringBuilder;
			//-- get general system and app information
			_with6.Append(SysInfoToString());

			//-- get exception-specific information
			_with6.Append("Exception Source:      ");
			try {
				_with6.Append(objException.Source);
			} catch (Exception e) {
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);

			_with6.Append("Exception Type:        ");
			try {
				_with6.Append(objException.GetType().FullName);
			} catch (Exception e) {
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);

			_with6.Append("Exception Message:     ");
			try {
				_with6.Append(objException.Message);
			} catch (Exception e) {
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);

			_with6.Append("Exception Target Site: ");
			try {
				_with6.Append(objException.TargetSite.Name);
			} catch (Exception e) {
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);

			try {
				string x = EnhancedStackTrace(objException);
				_with6.Append(x);
			} catch (Exception e) {
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);

			return objStringBuilder.ToString();
		}


		//--
		//-- returns ImageCodecInfo for the specified MIME type
		//--
		private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string strMimeType)
		{
			int j = 0;
			System.Drawing.Imaging.ImageCodecInfo[] objImageCodecInfo = null;
			objImageCodecInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();

			j = 0;
			while (j < objImageCodecInfo.Length) {
				if (objImageCodecInfo[j].MimeType == strMimeType) {
					return objImageCodecInfo[j];
				}
				j += 1;
			}

			return null;
		}


		//--
		//-- save bitmap object to JPEG of specified quality level
		//--
		private static void BitmapToJPEG(Bitmap objBitmap, string strFilename, long lngCompression = 75)
		{
			System.Drawing.Imaging.EncoderParameters objEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
            System.Drawing.Imaging.ImageCodecInfo objImageCodecInfo =  GetEncoderInfo("image/jpeg");

			objEncoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, lngCompression);
			objBitmap.Save(strFilename, objImageCodecInfo, objEncoderParameters);
		}


		//--
		//-- takes a screenshot of the desktop and saves to filename and format specified
		//--
		private static void TakeScreenshotPrivate(string strFilename)
		{
			Rectangle objRectangle = Screen.PrimaryScreen.Bounds;
			Bitmap objBitmap = new Bitmap(objRectangle.Right, objRectangle.Bottom);
			Graphics objGraphics = null;
			IntPtr hdcDest = default(IntPtr);
			int hdcSrc = 0;
			const int SRCCOPY = 0xcc0020;
			string strFormatExtension = null;
            objGraphics = Graphics.FromImage(objBitmap);

			//-- get a device context to the windows desktop and our destination  bitmaps
			hdcSrc = GetDC(0);
			hdcDest = objGraphics.GetHdc();
			//-- copy what is on the desktop to the bitmap
			BitBlt(hdcDest.ToInt32(), 0, 0, objRectangle.Right, objRectangle.Bottom, hdcSrc, 0, 0, SRCCOPY);
			//-- release device contexts
			objGraphics.ReleaseHdc(hdcDest);
			ReleaseDC(0, hdcSrc);

			strFormatExtension = _ScreenshotImageFormat.ToString().ToLower();
			if (System.IO.Path.GetExtension(strFilename) != "." + strFormatExtension) {
				strFilename += "." + strFormatExtension;
			}
			switch (strFormatExtension) {
				case "jpeg":
					BitmapToJPEG(objBitmap, strFilename, 80);
					break;
				default:
					objBitmap.Save(strFilename, _ScreenshotImageFormat);
					break;
			}

			//-- save the complete path/filename of the screenshot for possible later use
			_strScreenshotFullPath = strFilename;
		}

		//--
		//-- get IP address of this machine
		//-- not an ideal method for a number of reasons (guess why!)
		//-- but the alternatives are very ugly
		//--
		private static string GetCurrentIP()
		{
			try {
				string strIP = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
				return strIP;
			} catch (Exception ex) {
				return "127.0.0.1";
			}
		}

		const string _strKeyNotPresent = "The key <{0}> is not present in the <appSettings> section of .config file";

		const string _strKeyError = "Error {0} retrieving key <{1}> from <appSettings> section of .config file";
		//--
		//-- Returns the specified String value from the application .config file,
		//-- with many fail-safe checks (exceptions, key not present, etc)
		//--
		//-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
		//-- 
		private static string GetConfigString(string strKey, string strDefault = null)
		{
			try {
				string strTemp = Convert.ToString(ConfigurationSettings.AppSettings.Get(_strClassName + "/" + strKey));
				if (strTemp == null) {
					if (strDefault == null) {
						return string.Format(_strKeyNotPresent, _strClassName + "/" + strKey);
					} else {
						return strDefault;
					}
				} else {
					return strTemp;
				}
			} catch (Exception ex) {
				if (strDefault == null) {
					return string.Format(_strKeyError, ex.Message, _strClassName + "/" + strKey);
				} else {
					return strDefault;
				}
			}
		}

		//--
		//-- Returns the specified boolean value from the application .config file,
		//-- with many fail-safe checks (exceptions, key not present, etc)
		//--
		//-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
		//-- 
		private static bool GetConfigBoolean(string strKey, bool blnDefault = false)
		{
			string strTemp = null;
			try {
				strTemp = ConfigurationSettings.AppSettings.Get(_strClassName + "/" + strKey);
			} catch (Exception ex) {
				if (blnDefault == null) {
					return false;
				} else {
					return blnDefault;
				}
			}

			if (strTemp == null) {
				if (blnDefault == null) {
					return false;
				} else {
					return blnDefault;
				}
			} else {
				switch (strTemp.ToLower()) {
					case "1":
					case "true":
						return true;
					default:
						return false;
				}
			}
		}

	}
}
