// -- FILE ------------------------------------------------------------------
// name       : HourDemo.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class HourDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int year, YearMonth month, int day, int hourValue )
		{
			WriteLine( "Input: count={0}, year={1}, month={2}, day={3}, hour={4}", periodCount, year, month, day, hourValue );
			WriteLine();

			HourTimeRange hourTimeRange;
			if ( periodCount == 1 )
			{
				Hour hour = new Hour( year, (int)month, day, hourValue );
				hourTimeRange = hour;

				Hour previousHour = hour.GetPreviousHour();
				Hour nextHour = hour.GetNextHour();

				ShowHour( hour );
				ShowCompactHour( previousHour, "Previous Hour" );
				ShowCompactHour( nextHour, "Next Hour" );
				WriteLine();
			}
			else
			{
				Hours hours = new Hours( year, (int)month, day, hourValue, periodCount );
				hourTimeRange = hours;

				ShowHours( hours );
				WriteLine();

				foreach ( Hour hour in hours.GetHours() )
				{
					ShowCompactHour( hour );
				}
				WriteLine();
			}

			foreach ( Minute minute in hourTimeRange.GetMinutes() )
			{
				MinuteDemo.ShowCompactMinute( minute );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactHour( Hour hour, string caption = "Hour" )
		{
			WriteLine( "{0}: {1}", caption, hour );
		} // ShowCompactHour

		// ----------------------------------------------------------------------
		public static void ShowHour( Hour hour, string caption = "Hour" )
		{
			WriteLine( "{0}: {1}", caption, hour );
			WriteIndentLine( "Year: {0}", hour.Year );
			WriteIndentLine( "Month: {0}", hour.Month );
			WriteIndentLine( "Day: {0}", hour.Day );
			WriteIndentLine( "HourValue: {0}", hour.HourValue );
			WriteIndentLine( "FirstMinuteStart: {0}", hour.FirstMinuteStart );
			WriteIndentLine( "LastMinuteStart: {0}", hour.LastMinuteStart );
			WriteLine();
		} // ShowHour

		// ----------------------------------------------------------------------
		public static void ShowCompactHours( Hours hours, string caption = "Hours" )
		{
			WriteLine( "{0}: {1}", caption, hours );
		} // ShowCompactHours

		// ----------------------------------------------------------------------
		public static void ShowHours( Hours hours, string caption = "Hours" )
		{
			WriteLine( "{0}: {1}", caption, hours );
			WriteIndentLine( "StartYear: {0}", hours.StartYear );
			WriteIndentLine( "StartMonth: {0}", hours.StartMonth );
			WriteIndentLine( "StartDay: {0}", hours.StartDay );
			WriteIndentLine( "StartHour: {0}", hours.StartHour );
			WriteIndentLine( "EndYear: {0}", hours.EndYear );
			WriteIndentLine( "EndMonth: {0}", hours.EndMonth );
			WriteIndentLine( "EndDay: {0}", hours.EndDay );
			WriteIndentLine( "EndHour: {0}", hours.EndHour );
			WriteIndentLine( "FirstHourStart: {0}", hours.FirstHourStart );
			WriteIndentLine( "LastHourStart: {0}", hours.LastHourStart );
			WriteLine();
		} // ShowHours

	} // class HourDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
