// -- FILE ------------------------------------------------------------------
// name       : Now.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public static class Now
	{

		#region Year

		// ----------------------------------------------------------------------
		public static DateTime CalendarYear
		{
			get { return Year( TimeSpec.CalendarYearStartMonth ); }
		} // CalendarYear

		// ----------------------------------------------------------------------
		public static DateTime Year( YearMonth yearStartMonth )
		{
			DateTime now = ClockProxy.Clock.Now;
			int startMonth = (int)yearStartMonth;
			int monthOffset = now.Month - startMonth;
			int year = monthOffset < 0 ? now.Year - 1 : now.Year;
			return new DateTime( year, startMonth, 1 );
		} // Year

		#endregion

		#region Halfyear

		// ----------------------------------------------------------------------
		public static DateTime CalendarHalfyear
		{
			get { return Halfyear( TimeSpec.CalendarYearStartMonth ); }
		} // CalendarHalfyear

		// ----------------------------------------------------------------------
		public static DateTime Halfyear( YearMonth yearStartMonth )
		{
			DateTime now = ClockProxy.Clock.Now;
			int year = now.Year;
			if ( now.Month - (int)yearStartMonth < 0 )
			{
				year--;
			}
			YearHalfyear halfyear = TimeTool.GetHalfyearOfMonth( yearStartMonth, (YearMonth)now.Month );
			int months = ( (int)halfyear - 1 ) * TimeSpec.MonthsPerHalfyear;
			return new DateTime( year, (int)yearStartMonth, 1 ).AddMonths( months );
		} // Halfyear

		#endregion

		#region Quarter

		// ----------------------------------------------------------------------
		public static DateTime CalendarQuarter
		{
			get { return Quarter( TimeSpec.CalendarYearStartMonth ); }
		} // CalendarQuarter

		// ----------------------------------------------------------------------
		public static DateTime Quarter( YearMonth yearStartMonth )
		{
			DateTime now = ClockProxy.Clock.Now;
			int year = now.Year;
			if ( now.Month - (int)yearStartMonth < 0 )
			{
				year--;
			}
			YearQuarter quarter = TimeTool.GetQuarterOfMonth( yearStartMonth, (YearMonth)now.Month );
			int months = ( (int)quarter - 1 ) * TimeSpec.MonthsPerQuarter;
			return new DateTime( year, (int)yearStartMonth, 1 ).AddMonths( months );
		} // Quarter

		#endregion

		#region Month

		// ----------------------------------------------------------------------
		public static DateTime Month
		{
			get { return TimeTrim.Day( ClockProxy.Clock.Now ); }
		} // Month

		// ----------------------------------------------------------------------
		public static YearMonth YearMonth
		{
			get { return (YearMonth)ClockProxy.Clock.Now.Month; }
		} // YearMonth

		#endregion

		#region Week

		// ----------------------------------------------------------------------
		public static DateTime Week()
		{
			return DateTimeFormatInfo.CurrentInfo == null ? DateTime.Now : Week( DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek );
		} // Week

		// ----------------------------------------------------------------------
		public static DateTime Week( DayOfWeek firstDayOfWeek )
		{
			DateTime currentDay = ClockProxy.Clock.Now;
			while ( currentDay.DayOfWeek != firstDayOfWeek )
			{
				currentDay = currentDay.AddDays( -1 );
			}
			return new DateTime( currentDay.Year, currentDay.Month, currentDay.Day );
		} // Week

		#endregion

		#region Day

		// ----------------------------------------------------------------------
		public static DateTime Today
		{
			get { return ClockProxy.Clock.Now.Date; }
		} // Today

		#endregion

		#region Hour

		// ----------------------------------------------------------------------
		public static DateTime Hour
		{
			get { return TimeTrim.Minute( ClockProxy.Clock.Now ); }
		} // Hour

		#endregion

		#region Minute

		// ----------------------------------------------------------------------
		public static DateTime Minute
		{
			get { return TimeTrim.Second( ClockProxy.Clock.Now ); }
		} // Minute

		#endregion

		#region Second

		// ----------------------------------------------------------------------
		public static DateTime Second
		{
			get { return TimeTrim.Millisecond( ClockProxy.Clock.Now ); }
		} // Second

		#endregion

	} // class Now

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
