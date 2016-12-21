// -- FILE ------------------------------------------------------------------
// name       : MinuteDemo.cs
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
	public class MinuteDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int year, YearMonth month, int day, int hour, int minuteValue )
		{
			WriteLine( "Input: count={0}, year={1}, month={2}, day={3}, hour={4}, minute={5}", periodCount, year, month, day, hour, minuteValue );
			WriteLine();

			if ( periodCount == 1 )
			{
				Minute minute = new Minute( year, (int)month, day, hour, minuteValue );
				Minute previousMinute = minute.GetPreviousMinute();
				Minute nextMinute = minute.GetNextMinute();

				ShowMinute( minute );
				ShowCompactMinute( previousMinute, "Previous Minute" );
				ShowCompactMinute( nextMinute, "Next Minute" );
				WriteLine();
			}
			else
			{
				Minutes minutes = new Minutes( year, (int)month, day, hour, minuteValue, periodCount );

				ShowMinutes( minutes );
				WriteLine();

				foreach ( Minute minute in minutes.GetMinutes() )
				{
					ShowCompactMinute( minute );
				}
				WriteLine();
			}
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactMinute( Minute minute, string caption = "Minute" )
		{
			WriteLine( "{0}: {1}", caption, minute );
		} // ShowCompactMinute

		// ----------------------------------------------------------------------
		public static void ShowMinute( Minute minute, string caption = "Minute" )
		{
			WriteLine( "{0}: {1}", caption, minute );
			WriteIndentLine( "Year: {0}", minute.Year );
			WriteIndentLine( "Month: {0}", minute.Month );
			WriteIndentLine( "Day: {0}", minute.Day );
			WriteIndentLine( "Hour: {0}", minute.Hour );
			WriteIndentLine( "MinuteValue: {0}", minute.MinuteValue );
			WriteLine();
		} // ShowMinute

		// ----------------------------------------------------------------------
		public static void ShowCompactMinutes( Minutes minutes, string caption = "Minutes" )
		{
			WriteLine( "{0}: {1}", caption, minutes );
		} // ShowCompactMinutes

		// ----------------------------------------------------------------------
		public static void ShowMinutes( Minutes minutes, string caption = "Minutes" )
		{
			WriteLine( "{0}: {1}", caption, minutes );
			WriteIndentLine( "StartYear: {0}", minutes.StartYear );
			WriteIndentLine( "StartMonth: {0}", minutes.StartMonth );
			WriteIndentLine( "StartDay: {0}", minutes.StartDay );
			WriteIndentLine( "StartHour: {0}", minutes.StartHour );
			WriteIndentLine( "StartMinute: {0}", minutes.StartMinute );
			WriteIndentLine( "EndYear: {0}", minutes.EndYear );
			WriteIndentLine( "EndMonth: {0}", minutes.EndMonth );
			WriteIndentLine( "EndDay: {0}", minutes.EndDay );
			WriteIndentLine( "EndHour: {0}", minutes.EndHour );
			WriteIndentLine( "EndMinute: {0}", minutes.EndMinute );
			WriteIndentLine( "FirstMinuteStart: {0}", minutes.FirstMinuteStart );
			WriteIndentLine( "LastMinuteStart: {0}", minutes.LastMinuteStart );
			WriteLine();
		} // ShowMinutes

	} // class MinuteDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
