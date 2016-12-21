	// -- FILE ------------------------------------------------------------------
// name       : HalfyearDemo.cs
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
	public class HalfyearDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int startYear, YearHalfyear yearHalfyear, TimeCalendarConfig calendarConfig )
		{
			WriteLine( "Input: count={0}, year={1}, halfyear={2}", periodCount, startYear, yearHalfyear );
			WriteLine();

			HalfyearTimeRange halfyearTimeRange;
			if ( periodCount == 1 )
			{
				Halfyear halfyear = new Halfyear( startYear, yearHalfyear, new TimeCalendar( calendarConfig ) );
				halfyearTimeRange = halfyear;

				Halfyear previousHalfyear = halfyear.GetPreviousHalfyear();
				Halfyear nextHalfyear = halfyear.GetNextHalfyear();

				ShowHalfyear( halfyear );
				ShowCompactHalfyear( previousHalfyear, "Previous Halfyear" );
				ShowCompactHalfyear( nextHalfyear, "Next Halfyear" );
				WriteLine();
			}
			else
			{
				Halfyears halfyears = new Halfyears( startYear, yearHalfyear, periodCount, new TimeCalendar( calendarConfig ) );
				halfyearTimeRange = halfyears;

				ShowHalfyears( halfyears );
				WriteLine();
				
				foreach ( Halfyear halfyear in halfyears.GetHalfyears() )
				{
					ShowCompactHalfyear( halfyear );
				}
				WriteLine();
			}

			foreach ( Quarter quarter in halfyearTimeRange.GetQuarters() )
			{
				QuarterDemo.ShowCompactQuarter( quarter );
			}
			WriteLine();
			foreach ( Month month in halfyearTimeRange.GetMonths() )
			{
				MonthDemo.ShowCompactMonth( month );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactHalfyear( Halfyear halfyear, string caption = "Halfyear" )
		{
			WriteLine( "{0}: {1}", caption, halfyear );
		} // ShowCompactHalfyear

		// ----------------------------------------------------------------------
		public static void ShowHalfyear( Halfyear halfyear, string caption = "Halfyear" )
		{
			WriteLine( "{0}: {1}", caption, halfyear );
			WriteIndentLine( "YearBaseMonth: {0}", halfyear.YearBaseMonth );
			WriteIndentLine( "StartMonth: {0}", halfyear.StartMonth );
			WriteIndentLine( "Year: {0}", halfyear.Year );
			WriteIndentLine( "YearHalfyear: {0}", halfyear.YearHalfyear );
			WriteIndentLine( "IsCalendarHalfyear: {0}", halfyear.IsCalendarHalfyear );
			WriteIndentLine( "MultipleCalendarYears: {0}", halfyear.MultipleCalendarYears );
			WriteIndentLine( "HalfyearName: {0}", halfyear.HalfyearName );
			WriteIndentLine( "HalfyearOfYearName: {0}", halfyear.HalfyearOfYearName );
			WriteIndentLine( "FirstDayStart: {0}", Format( halfyear.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( halfyear.LastDayStart ) );
			WriteLine();
		} // ShowHalfyear

		// ----------------------------------------------------------------------
		public static void ShowCompactHalfyears( Halfyears halfyears, string caption = "Halfyears" )
		{
			WriteLine( "{0}: {1}", caption, halfyears );
		} // ShowCompactHalfyears

		// ----------------------------------------------------------------------
		public static void ShowHalfyears( Halfyears halfyears, string caption = "Halfyears" )
		{
			WriteLine( "{0}: {1}", caption, halfyears );
			WriteIndentLine( "YearBaseMonth: {0}", halfyears.YearBaseMonth );
			WriteIndentLine( "StartYear: {0}", halfyears.StartYear );
			WriteIndentLine( "StartHalfyear: {0}", halfyears.StartHalfyear );
			WriteIndentLine( "StartHalfyearName: {0}", halfyears.StartHalfyearName );
			WriteIndentLine( "StartHalfyearOfYearName: {0}", halfyears.StartHalfyearOfYearName );
			WriteIndentLine( "EndYear: {0}", halfyears.EndYear );
			WriteIndentLine( "EndHalfyear: {0}", halfyears.EndHalfyear );
			WriteIndentLine( "EndHalfyearName: {0}", halfyears.EndHalfyearName );
			WriteIndentLine( "EndHalfyearOfYearName: {0}", halfyears.EndHalfyearOfYearName );
			WriteLine();
		} // ShowHalfyears

	} // class HalfyearDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
