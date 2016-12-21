// -- FILE ------------------------------------------------------------------
// name       : Duration.cs
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
	public static class Duration
	{

		#region Year

		// ----------------------------------------------------------------------
		public static TimeSpan Year( int year )
		{
			return DateTimeFormatInfo.CurrentInfo == null ? TimeSpan.Zero : Year( DateTimeFormatInfo.CurrentInfo.Calendar, year );
		} // Year

		// ----------------------------------------------------------------------
		public static TimeSpan Year( Calendar calendar, int year )
		{
			return Days( calendar.GetDaysInYear( year ) );
		} // Year

		#endregion

		#region Halfyear

		// ----------------------------------------------------------------------
		public static TimeSpan Halfyear( int year, YearHalfyear yearHalfyear )
		{
			return DateTimeFormatInfo.CurrentInfo == null ? TimeSpan.Zero : Halfyear( DateTimeFormatInfo.CurrentInfo.Calendar, year, yearHalfyear );
		} // Halfyear

		// ----------------------------------------------------------------------
		public static TimeSpan Halfyear( Calendar calendar, int year, YearHalfyear yearHalfyear )
		{
			YearMonth[] halfyearMonths = TimeTool.GetMonthsOfHalfyear( yearHalfyear );
			TimeSpan duration = TimeSpec.NoDuration;
			foreach ( YearMonth halfyearMonth in halfyearMonths )
			{
				duration = duration.Add( Month( calendar, year, halfyearMonth ) );
			}
			return duration;
		} // Halfyear

		#endregion

		#region Quarter

		// ----------------------------------------------------------------------
		public static TimeSpan Quarter( int year, YearQuarter yearQuarter )
		{
			return DateTimeFormatInfo.CurrentInfo == null ? TimeSpan.Zero : Quarter( DateTimeFormatInfo.CurrentInfo.Calendar, year, yearQuarter );
		} // Quarter

		// ----------------------------------------------------------------------
		public static TimeSpan Quarter( Calendar calendar, int year, YearQuarter yearQuarter )
		{
			YearMonth[] quarterMonths = TimeTool.GetMonthsOfQuarter( yearQuarter );
			TimeSpan duration = TimeSpec.NoDuration;
			foreach ( YearMonth quarterMonth in quarterMonths )
			{
				duration = duration.Add( Month( calendar, year, quarterMonth ) );
			}
			return duration;
		} // Quarter

		#endregion

		#region Month

		// ----------------------------------------------------------------------
		public static TimeSpan Month( int year, YearMonth yearMonth )
		{
			return DateTimeFormatInfo.CurrentInfo == null ? TimeSpan.Zero : Month( DateTimeFormatInfo.CurrentInfo.Calendar, year, yearMonth );
		} // Month

		// ----------------------------------------------------------------------
		public static TimeSpan Month( Calendar calendar, int year, YearMonth yearMonth )
		{
			return Days( calendar.GetDaysInMonth( year, (int)yearMonth ) );
		} // Month

		#endregion

		#region Week

		// ----------------------------------------------------------------------
		public static TimeSpan Week = Weeks( 1 );

		// ----------------------------------------------------------------------
		public static TimeSpan Weeks( int weeks )
		{
			return Days( weeks * TimeSpec.DaysPerWeek );
		} // Weeks

		#endregion

		#region Day

		// ----------------------------------------------------------------------
		public static TimeSpan Day = Days( 1 );

		// ----------------------------------------------------------------------
		public static TimeSpan Days( int days, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0 )
		{
			return new TimeSpan( days, hours, minutes, seconds, milliseconds );
		} // Days

		#endregion

		#region Hour

		// ----------------------------------------------------------------------
		public static TimeSpan Hour = Hours( 1 );

		// ----------------------------------------------------------------------
		public static TimeSpan Hours( int hours, int minutes = 0, int seconds = 0, int milliseconds = 0 )
		{
			return new TimeSpan( 0, hours, minutes, seconds, milliseconds );
		} // Hours

		#endregion

		#region Minute

		// ----------------------------------------------------------------------
		public static TimeSpan Minute = Minutes( 1 );

		// ----------------------------------------------------------------------
		public static TimeSpan Minutes( int minutes, int seconds = 0, int milliseconds = 0 )
		{
			return new TimeSpan( 0, 0, minutes, seconds, milliseconds );
		} // Minutes

		#endregion

		#region Second

		// ----------------------------------------------------------------------
		public static TimeSpan Second = Seconds( 1 );

		// ----------------------------------------------------------------------
		public static TimeSpan Seconds( int seconds, int milliseconds = 0 )
		{
			return new TimeSpan( 0, 0, 0, seconds, milliseconds );
		} // Seconds

		#endregion

		#region Millisecond

		// ----------------------------------------------------------------------
		public static TimeSpan Millisecond = Milliseconds( 1 );

		// ----------------------------------------------------------------------
		public static TimeSpan Milliseconds( int milliseconds )
		{
			return new TimeSpan( 0, 0, 0, 0, milliseconds );
		} // Milliseconds

		#endregion

	} // class Duration

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
