// -- FILE ------------------------------------------------------------------
// name       : MainPage.xaml.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public partial class MainPage
	{

		// ----------------------------------------------------------------------
		public MainPage()
		{
			InitializeComponent();
			DataContext = new CollectorViewModel();
		} // MainPage

	} // class MainPage

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
