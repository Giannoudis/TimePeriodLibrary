// -- FILE ------------------------------------------------------------------
// name       : MonthDemo.cs
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
	public class MonthDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int year, YearMonth yearMonth )
		{
			WriteLine( "Input: count={0}, year={1}, month={2}", periodCount, year, yearMonth );
			WriteLine();

			MonthTimeRange monthTimeRange;
			if ( periodCount == 1 )
			{
				Month month = new Month( year, yearMonth );
				monthTimeRange = month;

				Month previousMonth = month.GetPreviousMonth();
				Month nextMonth = month.GetNextMonth();

				ShowMonth( month );
				ShowCompactMonth( previousMonth, "Previous Month" );
				ShowCompactMonth( nextMonth, "Next Month" );
				WriteLine();
			}
			else
			{
				Months months = new Months( year, yearMonth, periodCount );
				monthTimeRange = months;

				ShowMonths( months );
				WriteLine();

				foreach ( Month month in months.GetMonths() )
				{
					ShowCompactMonth( month );
				}
				WriteLine();
			}

			foreach ( Day day in monthTimeRange.GetDays() )
			{
				DayDemo.ShowCompactDay( day );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactMonth( Month month, string caption = "Month" )
		{
			WriteLine( "{0}: {1}", caption, month );
		} // ShowCompactMonth

		// ----------------------------------------------------------------------
		public static void ShowMonth( Month month, string caption = "Month" )
		{
			WriteLine( "{0}: {1}", caption, month );
			WriteIndentLine( "YearMonth: {0}", month.YearMonth );
			WriteIndentLine( "Year: {0}", month.Year );
			WriteIndentLine( "YearMonth: {0}", month.YearMonth );
			WriteIndentLine( "DaysInMonth: {0}", month.DaysInMonth );
			WriteIndentLine( "MonthName: {0}", month.MonthName );
			WriteIndentLine( "MonthOfYearName: {0}", month.MonthOfYearName );
			WriteIndentLine( "FirstDayStart: {0}", Format( month.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( month.LastDayStart ) );
			WriteLine();
		} // ShowMonth

		// ----------------------------------------------------------------------
		public static void ShowCompactMonths( Months months, string caption = "Months" )
		{
			WriteLine( "{0}: {1}", caption, months );
		} // ShowCompactMonths

		// ----------------------------------------------------------------------
		public static void ShowMonths( Months months, string caption = "Months" )
		{
			WriteLine( "{0}: {1}", caption, months );
			WriteIndentLine( "StartYear: {0}", months.StartYear );
			WriteIndentLine( "StartMonth: {0}", months.StartMonth );
			WriteIndentLine( "EndYear: {0}", months.EndYear );
			WriteIndentLine( "EndMonth: {0}", months.EndMonth );
			WriteIndentLine( "FirstDayStart: {0}", Format( months.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( months.LastDayStart ) );
			WriteLine();
		} // ShowMonths

	} // class MonthDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
