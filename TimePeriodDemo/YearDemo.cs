// -- FILE ------------------------------------------------------------------
// name       : YearDemo.cs
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
	public class YearDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int startYear, TimeCalendarConfig calendarConfig )
		{
			WriteLine( "Input: count={0}, year={1}", periodCount, startYear );
			WriteLine( "Calendar: base-month={0}, week-type={1}, culture={2}", calendarConfig.YearBaseMonth,  calendarConfig.YearWeekType, calendarConfig.Culture.Name );

			WriteLine();

			YearTimeRange yearTimeRange;
			if ( periodCount == 1 )
			{
				Year year = new Year( startYear, new TimeCalendar( calendarConfig ) );
				yearTimeRange = year;

				Year previousYear = year.GetPreviousYear();
				Year nextYears = year.GetNextYear();

				ShowYear( year );
				ShowCompactYear( previousYear, "Previous Year" );
				ShowCompactYear( nextYears, "Next Year" );
				WriteLine();
			}
			else
			{
				Years years = new Years( startYear, periodCount, new TimeCalendar( calendarConfig ) );
				yearTimeRange = years;

				ShowYears( years );
				WriteLine();

				foreach ( Year year in years.GetYears() )
				{
					ShowCompactYear( year );
				}
				WriteLine();
			}

			foreach ( Halfyear halfyear in yearTimeRange.GetHalfyears() )
			{
				HalfyearDemo.ShowCompactHalfyear( halfyear );
			}
			WriteLine();
			foreach ( Quarter quarter in yearTimeRange.GetQuarters() )
			{
				QuarterDemo.ShowCompactQuarter( quarter );
			}
			WriteLine();
			foreach ( Month month in yearTimeRange.GetMonths() )
			{
				MonthDemo.ShowCompactMonth( month );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactYear( Year year, string caption = "Year" )
		{
			WriteLine( "{0}: {1}", caption, year );
		} // ShowCompactYear

		// ----------------------------------------------------------------------
		public static void ShowYear( Year year, string caption = "Year" )
		{
			WriteLine( "{0}: {1}", caption, year );
			WriteIndentLine( "YearBaseMonth: {0}", year.YearBaseMonth );
			WriteIndentLine( "IsCalendarYear: {0}", year.IsCalendarYear );
			WriteIndentLine( "StartYear: {0}", year.StartYear );
			WriteIndentLine( "FirstDayStart: {0}", Format( year.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( year.LastDayStart ) );
			WriteIndentLine( "LastMonthStart: {0}", Format( year.LastMonthStart ) );
			WriteIndentLine( "YearName: {0}", year.YearName );
			WriteLine();
		} // ShowYear

		// ----------------------------------------------------------------------
		public static void ShowCompactYears( Years years, string caption = "Years" )
		{
			WriteLine( "{0}: {1}", caption, years );
		} // ShowCompactYears

		// ----------------------------------------------------------------------
		public static void ShowYears( Years years, string caption = "Years" )
		{
			WriteLine( "{0}: {1}", caption, years );
			WriteIndentLine( "YearBaseMonth: {0}", years.YearBaseMonth );
			WriteIndentLine( "StartYear: {0}", years.StartYear );
			WriteIndentLine( "StartYearName: {0}", years.StartYearName );
			WriteIndentLine( "EndYear: {0}", years.EndYear );
			WriteIndentLine( "EndYearName: {0}", years.EndYearName );
			WriteIndentLine( "FirstDayStart: {0}", Format( years.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( years.LastDayStart ) );
			WriteIndentLine( "LastMonthStart: {0}", Format( years.LastMonthStart ) );
			WriteLine();
		} // ShowYears

	} // class YearDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
