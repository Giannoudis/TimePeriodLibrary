// -- FILE ------------------------------------------------------------------
// name       : DayDemo.cs
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
	public class DayDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int year, YearMonth yearMonth, int dayValue )
		{
			WriteLine( "Input: count={0}, year={1}, month={2}, day={3}", periodCount, year, yearMonth, dayValue );
			WriteLine();

			DayTimeRange dayTimeRange;
			if ( periodCount == 1 )
			{
				Day day = new Day( year, (int)yearMonth, dayValue );
				dayTimeRange = day;

				Day previousDay = day.GetPreviousDay();
				Day nextDay = day.GetNextDay();

				ShowDay( day );
				ShowCompactDay( previousDay, "Previous Day" );
				ShowCompactDay( nextDay, "Next Day" );
				WriteLine();
			}
			else
			{
				Days days = new Days( year, (int)yearMonth, dayValue, periodCount );
				dayTimeRange = days;

				ShowDays( days );
				WriteLine();

				foreach ( Day day in days.GetDays() )
				{
					ShowCompactDay( day );
				}
				WriteLine();
			}

			foreach ( Hour hour in dayTimeRange.GetHours() )
			{
				HourDemo.ShowCompactHour( hour );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactDay( Day day, string caption = "Day" )
		{
			WriteLine( "{0}: {1}", caption, day );
		} // ShowCompactDay

		// ----------------------------------------------------------------------
		public static void ShowDay( Day day, string caption = "Day" )
		{
			WriteLine( "{0}: {1}", caption, day );
			WriteIndentLine( "Year: {0}", day.Year );
			WriteIndentLine( "Month: {0}", day.Month );
			WriteIndentLine( "DayValue: {0}", day.DayValue );
			WriteIndentLine( "DayOfWeek: {0}", day.DayOfWeek );
			WriteIndentLine( "DayName: {0}", day.DayName );
			WriteIndentLine( "FirstHourStart: {0}", day.FirstHourStart );
			WriteIndentLine( "LastHourStart: {0}", day.LastHourStart );
			WriteLine();
		} // ShowDay

		// ----------------------------------------------------------------------
		public static void ShowCompactDays( Days days, string caption = "Days" )
		{
			WriteLine( "{0}: {1}", caption, days );
		} // ShowCompactDays

		// ----------------------------------------------------------------------
		public static void ShowDays( Days days, string caption = "Days" )
		{
			WriteLine( "{0}: {1}", caption, days );
			WriteIndentLine( "StartYear: {0}", days.StartYear );
			WriteIndentLine( "StartMonth: {0}", days.StartMonth );
			WriteIndentLine( "StartDay: {0}", days.StartDay );
			WriteIndentLine( "EndYear: {0}", days.EndYear );
			WriteIndentLine( "EndMonth: {0}", days.EndMonth );
			WriteIndentLine( "EndDay: {0}", days.EndDay );
			WriteIndentLine( "FirstDayStart: {0}", Format( days.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( days.LastDayStart ) );
			WriteIndentLine( "FirstHourStart: {0}", days.FirstHourStart );
			WriteIndentLine( "LastHourStart: {0}", days.LastHourStart );
			WriteLine();
		} // ShowDays

	} // class DayDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
