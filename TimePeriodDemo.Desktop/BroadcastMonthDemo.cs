// -- FILE ------------------------------------------------------------------
// name       : BroadcastMonthDemo.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.09.30
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class BroadcastMonthDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int year, YearMonth yearMonth )
		{
			WriteLine( "Input: year={0}, month={1}", year, yearMonth );
			WriteLine();

			BroadcastMonth month = new BroadcastMonth( year, yearMonth );

			BroadcastMonth previousMonth = month.GetPreviousMonth();
			BroadcastMonth nextMonth = month.GetNextMonth();

			ShowMonth( month );
			ShowCompactMonth( previousMonth, "Previous BroadcastMonth" );
			ShowCompactMonth( nextMonth, "Next BroadcastMonth" );
			WriteLine();

			foreach ( BroadcastWeek week in month.GetWeeks() )
			{
				BroadcastWeekDemo.ShowCompactWeek( week );
			}
			WriteLine();
			foreach ( Day day in month.GetDays() )
			{
				DayDemo.ShowCompactDay( day );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactMonth( BroadcastMonth month, string caption = "BroadcastMonth" )
		{
			WriteLine( "{0}: {1}", caption, month );
		} // ShowCompactMonth

		// ----------------------------------------------------------------------
		public static void ShowMonth( BroadcastMonth month, string caption = "BroadcastMonth" )
		{
			WriteLine( "{0}: {1}", caption, month );
			WriteIndentLine( "Year: {0}", month.Year );
			WriteIndentLine( "Month: {0}", month.Month );
			WriteIndentLine( "FirstDayStart: {0}", Format( month.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( month.LastDayStart ) );
			WriteLine();
		} // ShowMonth

	} // class BroadcastMonthDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
