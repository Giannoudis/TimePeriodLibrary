// -- FILE ------------------------------------------------------------------
// name       : ViewModel.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.ComponentModel;
using System.Windows;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class ViewModel : DependencyObject
	{

		// ----------------------------------------------------------------------
		public event PropertyChangedEventHandler PropertyChanged;

		// ----------------------------------------------------------------------
		protected void NotifyPropertyChanged( string propertyName )
		{
			PropertyChangedEventHandler propertyChanged = PropertyChanged;
			if ( propertyChanged != null )
			{
				propertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
			}
		} // NotifyPropertyChanged

	} // class ViewModel

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
