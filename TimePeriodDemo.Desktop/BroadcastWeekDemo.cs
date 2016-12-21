// -- FILE ------------------------------------------------------------------
// name       : BroadcastWeekDemo.cs
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
	public class BroadcastWeekDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int year, int weekOfYear )
		{
			WriteLine( "Input: year={0}, week={1}", year, weekOfYear );
			WriteLine();

			BroadcastWeek week = new BroadcastWeek( year, weekOfYear );

			BroadcastWeek previousWeek = week.GetPreviousWeek();
			BroadcastWeek nextWeek = week.GetNextWeek();

			ShowWeek( week );
			ShowCompactWeek( previousWeek, "Previous BroadcastWeek" );
			ShowCompactWeek( nextWeek, "Next BroadcastWeek" );
			WriteLine();

			foreach ( Day day in week.GetDays() )
			{
				DayDemo.ShowCompactDay( day );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactWeek( BroadcastWeek week, string caption = "BroadcastWeek" )
		{
			WriteLine( "{0}: {1}", caption, week );
		} // ShowCompactWeek

		// ----------------------------------------------------------------------
		public static void ShowWeek( BroadcastWeek week, string caption = "BroadcastWeek" )
		{
			WriteLine( "{0}: {1}", caption, week );
			WriteIndentLine( "Year: {0}", week.Year );
			WriteIndentLine( "Week: {0}", week.Week );
			WriteIndentLine( "FirstDayStart: {0}", Format( week.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( week.LastDayStart ) );
			WriteLine();
		} // ShowWeek

	} // class BroadcastWeekDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
