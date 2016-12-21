// -- FILE ------------------------------------------------------------------
// name       : BroadcastMonth.cs
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
	public sealed class BroadcastMonth : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		public BroadcastMonth() :
			this( new TimeCalendar() )
		{
		} // BroadcastMonth

		// ----------------------------------------------------------------------
		public BroadcastMonth( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // BroadcastMonth

		// ----------------------------------------------------------------------
		public BroadcastMonth( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // BroadcastMonth

		// ----------------------------------------------------------------------
		public BroadcastMonth( DateTime moment, ITimeCalendar calendar ) :
			this( GetYearOf( moment ), GetMonthOf( moment ), calendar )
		{
		} // BroadcastMonth

		// ----------------------------------------------------------------------
		public BroadcastMonth( int year, YearMonth month ) :
			this( year, month, new TimeCalendar() )
		{
		} // BroadcastMonth

		// ----------------------------------------------------------------------
		public BroadcastMonth( int year, YearMonth month, ITimeCalendar calendar ) :
			base( GetPeriodOf( year, month ), calendar )
		{
			this.year = year;
			this.month = month;
		} // BroadcastMonth

		// ----------------------------------------------------------------------
		public YearMonth Month
		{
			get { return month; }
		} // Month

		// ----------------------------------------------------------------------
		public int Year
		{
			get { return year; }
		} // Year

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetDays()
		{
			TimePeriodCollection days = new TimePeriodCollection();
			DateTime moment = Start.Date;
			while ( moment <= End.Date )
			{
				days.Add( new Day( moment.Date, Calendar ) );
				moment = moment.AddDays( 1 );
			}
			return days;
		} // GetDays

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetWeeks()
		{
			TimePeriodCollection weeks = new TimePeriodCollection();
			DateTime moment = Start.Date;
			while ( moment < End.Date )
			{
				weeks.Add( new BroadcastWeek( moment.Date, Calendar ) );
				moment = moment.AddDays( TimeSpec.DaysPerWeek );
			}
			return weeks;
		} // GetWeeks

		// ----------------------------------------------------------------------
		public BroadcastMonth GetPreviousMonth()
		{
			return AddMonths( -1 );
		} // GetPreviousMonth

		// ----------------------------------------------------------------------
		public BroadcastMonth GetNextMonth()
		{
			return AddMonths( 1 );
		} // GetNextMonth

		// ----------------------------------------------------------------------
		public BroadcastMonth AddMonths( int months )
		{
			int targetYear;
			YearMonth targetMonth;
			BroadcastCalendarTool.AddMonths( Year, Month, months, out targetYear, out targetMonth );
			return new BroadcastMonth( targetYear, targetMonth, Calendar );
		} // AddMonths

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( string.Format( "{0}/{1}", Year, Month ),
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

		// ----------------------------------------------------------------------
		private static int GetYearOf( DateTime moment )
		{
			int year;
			BroadcastCalendarTool.GetYearOf( moment, out year );
			return year;
		} // GetYaarOf

		// ----------------------------------------------------------------------
		private static YearMonth GetMonthOf( DateTime moment )
		{
			int year;
			YearMonth month;
			BroadcastCalendarTool.GetMonthOf( moment, out year, out month );
			return month;
		} // GetMonthOf

		// ----------------------------------------------------------------------
		private static ITimeRange GetPeriodOf( int year, YearMonth month )
		{
			DateTime start = BroadcastCalendarTool.GetStartOfMonth( year, month );
			if ( month == YearMonth.December )
			{
				year++;
				month = YearMonth.January;
			}
			else
			{
				month++;
			}
			DateTime end = BroadcastCalendarTool.GetStartOfMonth( year, month );
			return new CalendarTimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int year;
		private readonly YearMonth month;

	} // class BroadcastMonth

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
