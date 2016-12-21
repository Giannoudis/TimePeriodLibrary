// -- FILE ------------------------------------------------------------------
// name       : App.xaml.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public partial class App
	{

		// ----------------------------------------------------------------------
		/// <summary>
		/// Constructor for the Application object.
		/// </summary>
		public App()
		{
			// Global handler for uncaught exceptions. 
			UnhandledException += Application_UnhandledException;

			// Show graphics profiling information while debugging.
			if ( System.Diagnostics.Debugger.IsAttached )
			{
				// Display the current frame rate counters.
				Current.Host.Settings.EnableFrameRateCounter = true;

				// Show the areas of the app that are being redrawn in each frame.
				//Application.Current.Host.Settings.EnableRedrawRegions = true;

				// Enable non-production analysis visualization mode, 
				// which shows areas of a page that are being GPU accelerated with a colored overlay.
				//Application.Current.Host.Settings.EnableCacheVisualization = true;
			}

			SetupCulture();

			// Standard Silverlight initialization
			InitializeComponent();

			// Phone-specific initialization
			InitializePhoneApplication();
		} // App

		// ----------------------------------------------------------------------
		private static void SetupCulture()
		{
			//System.Threading.Thread.CurrentThread.CurrentCulture = new  System.Globalization.CultureInfo( "ar-DZ" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "ca-ES" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "zh-HK" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "cs-CZ" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "da-DK" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "de-DE" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "de-CH" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "el-GR" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "en-US" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "es-ES" );
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "fi-FI" );
		} // SetupCulture

		// ----------------------------------------------------------------------
		/// <summary>
		/// Provides easy access to the root frame of the Phone Application.
		/// </summary>
		/// <returns>The root frame of the Phone Application.</returns>
		public PhoneApplicationFrame RootFrame { get; private set; }

		// ----------------------------------------------------------------------
		// Code to execute when the application is launching (eg, from Start)
		// This code will not execute when the application is reactivated
		private void Application_Launching( object sender, LaunchingEventArgs e )
		{
		} // Application_Launching

		// ----------------------------------------------------------------------
		// Code to execute when the application is activated (brought to foreground)
		// This code will not execute when the application is first launched
		private void Application_Activated( object sender, ActivatedEventArgs e )
		{
		} // Application_Activated

		// ----------------------------------------------------------------------
		// Code to execute when the application is deactivated (sent to background)
		// This code will not execute when the application is closing
		private void Application_Deactivated( object sender, DeactivatedEventArgs e )
		{
		} // Application_Deactivated

		// ----------------------------------------------------------------------
		// Code to execute when the application is closing (eg, user hit Back)
		// This code will not execute when the application is deactivated
		private void Application_Closing( object sender, ClosingEventArgs e )
		{
		} // Application_Closing

		// ----------------------------------------------------------------------
		// Code to execute if a navigation fails
		private static void RootFrame_NavigationFailed( object sender, NavigationFailedEventArgs e )
		{
			if ( System.Diagnostics.Debugger.IsAttached )
			{
				// A navigation has failed; break into the debugger
				System.Diagnostics.Debugger.Break();
			}
		} // RootFrame_NavigationFailed

		// ----------------------------------------------------------------------
		// Code to execute on Unhandled Exceptions
// ReSharper disable MemberCanBeMadeStatic.Local
		private void Application_UnhandledException( object sender, ApplicationUnhandledExceptionEventArgs e )
// ReSharper restore MemberCanBeMadeStatic.Local
		{
			if ( System.Diagnostics.Debugger.IsAttached )
			{
				// An unhandled exception has occurred; break into the debugger
				System.Diagnostics.Debugger.Break();
			}
		} // Application_UnhandledException

		#region Phone application initialization

		// ----------------------------------------------------------------------
		// Avoid double-initialization
		private bool phoneApplicationInitialized;

		// ----------------------------------------------------------------------
		// Do not add any additional code to this method
		private void InitializePhoneApplication()
		{
			if ( phoneApplicationInitialized )
				return;

			// Create the frame but don't set it as RootVisual yet; this allows the splash
			// screen to remain active until the application is ready to render.
			RootFrame = new PhoneApplicationFrame();
			RootFrame.Navigated += CompleteInitializePhoneApplication;

			// Handle navigation failures
			RootFrame.NavigationFailed += RootFrame_NavigationFailed;

			// Ensure we don't initialize again
			phoneApplicationInitialized = true;
		} // InitializePhoneApplication

		// ----------------------------------------------------------------------
		// Do not add any additional code to this method
		private void CompleteInitializePhoneApplication( object sender, NavigationEventArgs e )
		{
			// Set the root visual to allow the application to render
// ReSharper disable RedundantCheckBeforeAssignment
			if ( RootVisual != RootFrame )
// ReSharper restore RedundantCheckBeforeAssignment
				RootVisual = RootFrame;

			// Remove this handler since it is no longer needed
			RootFrame.Navigated -= CompleteInitializePhoneApplication;
		} // CompleteInitializePhoneApplication

		#endregion

	} // class App

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
