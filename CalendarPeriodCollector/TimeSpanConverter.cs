// -- FILE ------------------------------------------------------------------
// name       : TimeSpanConverter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows.Data;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class TimeSpanConverter : IValueConverter
	{

		// ----------------------------------------------------------------------
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( value == null )
			{
				return null;
			}
			if ( value.GetType() != typeof( TimeSpan ) )
			{
				return null;
			}

			return ( (TimeSpan)value ).ToString( parameter as string );
		} // Convert

		// ----------------------------------------------------------------------
		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new Exception( "Not implemented" );
		} // ConvertBack

	} // class TimeSpanConverter

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
