// -- FILE ------------------------------------------------------------------
// name       : App.xaml.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Windows;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public partial class App
	{

		// ----------------------------------------------------------------------
		public App()
		{
			Startup += Application_Startup;
			Exit += Application_Exit;
			UnhandledException += Application_UnhandledException;

			InitializeComponent();
		} // App

		// ----------------------------------------------------------------------
		private void Application_Startup( object sender, StartupEventArgs e )
		{
			RootVisual = new MainPage();
		} // Application_Startup

		// ----------------------------------------------------------------------
		private static void Application_Exit( object sender, EventArgs e )
		{
		} // Application_Exit

		// ----------------------------------------------------------------------
		private static void Application_UnhandledException( object sender, ApplicationUnhandledExceptionEventArgs e )
		{
			// If the app is running outside of the debugger then report the exception using
			// the browser's exception mechanism. On IE this will display it a yellow alert 
			// icon in the status bar and Firefox will display a script error.
			if ( !System.Diagnostics.Debugger.IsAttached )
			{

				// NOTE: This will allow the application to continue running after an exception has been thrown
				// but not handled. 
				// For production applications this error handling should be replaced with something that will 
				// report the error to the website and stop the application.
				e.Handled = true;
				// ReSharper disable ConvertToLambdaExpression
				Deployment.Current.Dispatcher.BeginInvoke( delegate { ReportErrorToDOM( e ); } );
				// ReSharper restore ConvertToLambdaExpression
			}
		} // Application_UnhandledException

		// ----------------------------------------------------------------------
		private static void ReportErrorToDOM( ApplicationUnhandledExceptionEventArgs e )
		{
			try
			{
				string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
				errorMsg = errorMsg.Replace( '"', '\'' ).Replace( "\r\n", @"\n" );

				System.Windows.Browser.HtmlPage.Window.Eval( "throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");" );
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch ( Exception )
			// ReSharper restore EmptyGeneralCatchClause
			{
			}
		} // ReportErrorToDOM

	} // class App

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
