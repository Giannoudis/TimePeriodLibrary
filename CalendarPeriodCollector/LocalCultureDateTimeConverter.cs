// -- FILE ------------------------------------------------------------------
// name       : LocalCultureDateTimeConverter.cs
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
	public class LocalCultureDateTimeConverter : IValueConverter
	{

		// ----------------------------------------------------------------------
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( value == null )
			{
				return null;
			}
			if ( value.GetType() != typeof( DateTime ) )
			{
				return null;
			}
			return ( (DateTime)value ).ToString(); // do not use the parameter culture
		} // Convert

		// ----------------------------------------------------------------------
		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new Exception( "Not implemented" );
		} // ConvertBack

	} // class LocalCultureDateTimeConverter

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
