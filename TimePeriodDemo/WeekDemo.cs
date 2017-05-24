// -- FILE ------------------------------------------------------------------
// name       : WeekDemo.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Globalization;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class WeekDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int year, int weekOfYear, TimeCalendarConfig calendarConfig )
		{
			WriteLine( "Input: year={0}, week={1}", year, weekOfYear );
			WriteLine();

			WeekTimeRange weekTimeRange;
			if ( periodCount == 1 )
			{
				Week week = new Week( year, weekOfYear, new TimeCalendar( calendarConfig ) );
				weekTimeRange = week;

				Week previousWeek = week.GetPreviousWeek();
				Week nextWeek = week.GetNextWeek();

				ShowWeek( week );
				ShowCompactWeek( previousWeek, "Previous Week" );
				ShowCompactWeek( nextWeek, "Next Week" );
				WriteLine();
			}
			else
			{
				Weeks weeks = new Weeks( year, weekOfYear, periodCount, new TimeCalendar( calendarConfig ) );
				weekTimeRange = weeks;

				ShowWeeks( weeks );
				WriteLine();

				foreach ( Week week in weeks.GetWeeks() )
				{
					ShowCompactWeek( week );
				}
				WriteLine();
			}

			foreach ( Day day in weekTimeRange.GetDays() )
			{
				DayDemo.ShowCompactDay( day );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactWeek( Week week, string caption = "Week" )
		{
			WriteLine( "{0}: {1}", caption, week );
		} // ShowCompactWeek

		// ----------------------------------------------------------------------
		public static void ShowWeek( Week week, string caption = "Week" )
		{
			WriteLine( "{0}: {1}", caption, week );
			WriteIndentLine( "MultipleCalendarYears: {0}", week.MultipleCalendarYears );
			WriteIndentLine( "FirstDayStart: {0}", Format( week.FirstDayStart ) );
			WriteIndentLine( "FirstDayOfWeek: {0}", week.FirstDayOfWeek );
			WriteIndentLine( "LastDayStart: {0}", Format( week.LastDayStart ) );
			WriteIndentLine( "LastDayOfWeek: {0}", Format( week.LastDayOfWeek ) );
			WriteIndentLine( "LastDayOfWeek: {0}", week.LastDayOfWeek );
			WriteIndentLine( "WeekOfYear: {0}", week.WeekOfYear );
			WriteIndentLine( "WeekOfYearName: {0}", week.WeekOfYearName );
			WriteLine();
		} // ShowWeek

		// ----------------------------------------------------------------------
		public static void ShowCompactWeeks( Weeks weeks, string caption = "Weeks" )
		{
			WriteLine( "{0}: {1}", caption, weeks );
		} // ShowCompactWeeks

		// ----------------------------------------------------------------------
		public static void ShowWeeks( Weeks weeks, string caption = "Weeks" )
		{
			WriteLine( "{0}: {1}", caption, weeks );
			WriteIndentLine( "YearWeek: {0}", Format( weeks.Start ) );
			WriteIndentLine( "FirstDayStart: {0}", Format( weeks.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( weeks.LastDayStart ) );
			WriteIndentLine( "StartWeek: {0}", weeks.StartWeek );
			WriteIndentLine( "StartWeekOfYearName: {0}", weeks.StartWeekOfYearName );
			WriteIndentLine( "EndWeek: {0}", weeks.EndWeek );
			WriteIndentLine( "EndWeekOfYearName: {0}", weeks.EndWeekOfYearName );
			WriteLine();
		} // ShowWeeks

	} // class WeekDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
