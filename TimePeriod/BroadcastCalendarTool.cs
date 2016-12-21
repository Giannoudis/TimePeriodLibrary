// -- FILE ------------------------------------------------------------------
// name       : BroadcastCalendarTool.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.09.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public static class BroadcastCalendarTool
	{

		#region Week

		// ----------------------------------------------------------------------
		public static int GetWeeksOfYear( int year )
		{
			DateTime yearStart = GetStartOfYear( year );
			DateTime nextYearStart = GetStartOfYear( year + 1 );
			return (int)nextYearStart.Subtract( yearStart ).TotalDays / TimeSpec.DaysPerWeek;
		} // GetWeeksOfYear

		// ----------------------------------------------------------------------
		public static void GetWeekOf( DateTime moment, out int year, out int week )
		{
			GetYearOf( moment, out year );
			DateTime firstWeekStart = GetStartOfWeek( year, 1 );
			week = 1 + (int)( moment.Subtract( firstWeekStart ).TotalDays / TimeSpec.DaysPerWeek );
		} // GetWeekOf

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfWeek( int year, int week )
		{
			DateTime yearStart = GetStartOfYear( year );
			return yearStart.AddDays( ( week - 1 ) * TimeSpec.DaysPerWeek );
		} // GetStartOfWeek

		#endregion

		#region Month

		// ----------------------------------------------------------------------
		public static void AddMonths( int startYear, YearMonth startMonth, int addMonths, out int targetYear, out YearMonth targetMonth )
		{
			int totalMonths = ( startYear * TimeSpec.MonthsPerYear ) + ( (int)startMonth - 1 ) + addMonths;
			targetYear = totalMonths / TimeSpec.MonthsPerYear;
			targetMonth = (YearMonth)( ( totalMonths % TimeSpec.MonthsPerYear ) + 1 );
		} // AddMonths

		// ----------------------------------------------------------------------
		public static void GetMonthOf( DateTime moment, out int year, out YearMonth month )
		{
			int currentYear = moment.Year;
			YearMonth currentMonth = YearMonth.January;
			do
			{
				AddMonths( currentYear, currentMonth, 1, out currentYear, out currentMonth );
				if ( GetStartOfMonth( currentYear, currentMonth ) > moment )
				{
					AddMonths( currentYear, currentMonth, -1, out year, out month );
					break;
				}
			} while ( true );
		} // GetMonthOf

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfMonth( int year, YearMonth month )
		{
			DateTime start = new DateTime( year, (int)month, 1 );
			start = start.DayOfWeek == DayOfWeek.Sunday ?
				start.AddDays( -6 ) :
				start.AddDays( -( (int)start.DayOfWeek - 1 ) );

			return start;
		} // GetStartOfMonth

		#endregion

		#region Year

		// ----------------------------------------------------------------------
		public static void GetYearOf( DateTime moment, out int year )
		{
			year = moment.Year;

			while ( GetStartOfYear( year + 1 ) <= moment )
			{
				year++;
			}
		} // GetYearOf

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfYear( int year )
		{
			return GetStartOfMonth( year, YearMonth.January );
		} // GetStartOfYear

		#endregion

	} // class BroadcastCalendarTool

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
