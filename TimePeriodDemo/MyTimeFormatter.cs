// -- FILE ------------------------------------------------------------------
// name       : MyTimeFormatter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.12.22
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Text;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class MyTimeFormatter : TimeFormatter
	{

		// ----------------------------------------------------------------------
		public override string GetDuration( int years, int months, int days, int hours, int minutes, int seconds )
		{
			StringBuilder sb = new StringBuilder();

			// years(s)
			if ( years != 0 )
			{
				sb.Append( years );
				sb.Append( " " );
				sb.Append( years == 1 ? "Year" : "Years" );
			}

			// month(s)
			if ( months != 0 )
			{
				if ( sb.Length > 0 )
				{
					sb.Append( ", " );
				}
				sb.Append( months );
				sb.Append( " " );
				sb.Append( months == 1 ? "Month" : "Months" );
			}

			// day(s)
			if ( days != 0 )
			{
				if ( sb.Length > 0 )
				{
					sb.Append( ", " );
				}
				sb.Append( days );
				sb.Append( " " );
				sb.Append( days == 1 ? "Day" : "Days" );
			}

			return sb.ToString();
		} // GetDuration

	} // class MyTimeFormatter

} // namespace Itenso.TimePeriodDemo.Player
// -- EOF -------------------------------------------------------------------
