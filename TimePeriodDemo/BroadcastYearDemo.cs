// -- FILE ------------------------------------------------------------------
// name       : BroadcastYearDemo.cs
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
	public class BroadcastYearDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int year )
		{
			WriteLine( "Input: year={0}", year );

			WriteLine();

			BroadcastYear broadcastYear = new BroadcastYear( year );

			BroadcastYear previousYear = broadcastYear.GetPreviousYear();
			BroadcastYear nextYears = broadcastYear.GetNextYear();

			ShowYear( broadcastYear );
			ShowCompactYear( previousYear, "Previous BroadcastYear" );
			ShowCompactYear( nextYears, "Next BroadcastYear" );
			WriteLine();

			foreach ( BroadcastMonth month in broadcastYear.GetMonths() )
			{
				BroadcastMonthDemo.ShowCompactMonth( month );
			}
			WriteLine();
			foreach ( BroadcastWeek week in broadcastYear.GetWeeks() )
			{
				BroadcastWeekDemo.ShowCompactWeek( week );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactYear( BroadcastYear year, string caption = "BroadcastYear" )
		{
			WriteLine( "{0}: {1}", caption, year );
		} // ShowCompactYear

		// ----------------------------------------------------------------------
		public static void ShowYear( BroadcastYear year, string caption = "BroadcastYear" )
		{
			WriteLine( "{0}: {1}", caption, year );
			WriteIndentLine( "Year: {0}", year.Year );
			WriteIndentLine( "FirstDayStart: {0}", Format( year.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( year.LastDayStart ) );
			WriteIndentLine( "LastMonthStart: {0}", Format( year.LastMonthStart ) );
			WriteLine();
		} // ShowYear

	} // class BroadcastYearDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
