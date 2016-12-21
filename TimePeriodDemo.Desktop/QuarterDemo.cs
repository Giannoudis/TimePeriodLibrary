// -- FILE ------------------------------------------------------------------
// name       : QuarterDemo.cs
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
	public class QuarterDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount,  int startYear, YearQuarter yearQuarter, TimeCalendarConfig calendarConfig )
		{
			WriteLine( "Input: count={0}, year={1}, quarter={2}", periodCount, startYear, yearQuarter );
			WriteLine();

			QuarterTimeRange quarterTimeRange;
			if ( periodCount == 1 )
			{
				Quarter quarter = new Quarter( startYear, yearQuarter, new TimeCalendar( calendarConfig ) );
				quarterTimeRange = quarter;

				Quarter previousQuarter = quarter.GetPreviousQuarter();
				Quarter nextQuarter = quarter.GetNextQuarter();

				ShowQuarter( quarter );
				ShowCompactQuarter( previousQuarter, "Previous Quarter" );
				ShowCompactQuarter( nextQuarter, "Next Quarter" );
				WriteLine();
			}
			else
			{
				Quarters quarters = new Quarters( startYear, yearQuarter, periodCount, new TimeCalendar( calendarConfig ) );
				quarterTimeRange = quarters;

				ShowQuarters( quarters );
				WriteLine();
				
				foreach ( Quarter quarter in quarters.GetQuarters() )
				{
					ShowCompactQuarter( quarter );
				}
				WriteLine();
			}

			foreach ( Month month in quarterTimeRange.GetMonths() )
			{
				MonthDemo.ShowCompactMonth( month );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactQuarter( Quarter quarter, string caption = "Quarter" )
		{
			WriteLine( "{0}: {1}", caption, quarter );
		} // ShowCompactQuarter

		// ----------------------------------------------------------------------
		public static void ShowQuarter( Quarter quarter, string caption = "Quarter" )
		{
			WriteLine( "{0}: {1}", caption, quarter );
			WriteIndentLine( "YearBaseMonth: {0}", quarter.YearBaseMonth );
			WriteIndentLine( "StartMonth: {0}", quarter.StartMonth );
			WriteIndentLine( "Year: {0}", quarter.Year );
			WriteIndentLine( "YearQuarter: {0}", quarter.YearQuarter );
			WriteIndentLine( "IsCalendarQuarter: {0}", quarter.IsCalendarQuarter );
			WriteIndentLine( "MultipleCalendarYears: {0}", quarter.MultipleCalendarYears );
			WriteIndentLine( "QuarterName: {0}", quarter.QuarterName );
			WriteIndentLine( "QuarterOfYearName: {0}", quarter.QuarterOfYearName );
			WriteIndentLine( "FirstDayStart: {0}", Format( quarter.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( quarter.LastDayStart ) );
			WriteLine();
		} // ShowQuarter

		// ----------------------------------------------------------------------
		public static void ShowCompactQuarters( Quarters quarters, string caption = "Quarters" )
		{
			WriteLine( "{0}: {1}", caption, quarters );
		} // ShowCompactQuarters

		// ----------------------------------------------------------------------
		public static void ShowQuarters( Quarters quarters, string caption = "Quarters" )
		{
			WriteLine( "{0}: {1}", caption, quarters );
			WriteIndentLine( "YearBaseMonth: {0}", quarters.YearBaseMonth );
			WriteIndentLine( "StartYear: {0}", quarters.StartYear );
			WriteIndentLine( "StartQuarter: {0}", quarters.StartQuarter );
			WriteIndentLine( "StartQuarterName: {0}", quarters.StartQuarterName );
			WriteIndentLine( "StartQuarterOfYearName: {0}", quarters.StartQuarterOfYearName );
			WriteIndentLine( "EndYear: {0}", quarters.EndYear );
			WriteIndentLine( "EndQuarter: {0}", quarters.EndQuarter );
			WriteIndentLine( "EndQuarterName: {0}", quarters.EndQuarterName );
			WriteIndentLine( "EndQuarterOfYearName: {0}", quarters.EndQuarterOfYearName );
			WriteIndentLine( "FirstDayStart: {0}", Format( quarters.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( quarters.LastDayStart ) );
			WriteLine();
		} // ShowQuarters

	} // class QuarterDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
