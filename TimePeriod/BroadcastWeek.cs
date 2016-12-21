// -- FILE ------------------------------------------------------------------
// name       : BroadcastWeek.cs
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
	public sealed class BroadcastWeek : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		public BroadcastWeek() :
			this( new TimeCalendar() )
		{
		} // BroadcastWeek

		// ----------------------------------------------------------------------
		public BroadcastWeek( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // BroadcastWeek

		// ----------------------------------------------------------------------
		public BroadcastWeek( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // BroadcastWeek

		// ----------------------------------------------------------------------
		public BroadcastWeek( DateTime moment, ITimeCalendar calendar ) :
			this( GetYearOf( moment ), GetWeekOf( moment ), calendar )
		{
		} // BroadcastWeek

		// ----------------------------------------------------------------------
		public BroadcastWeek( int year, int week ) :
			this( year, week, new TimeCalendar() )
		{
		} // BroadcastWeek

		// ----------------------------------------------------------------------
		public BroadcastWeek( int year, int week, ITimeCalendar calendar ) :
			base( GetPeriodOf( year, week ), calendar )
		{
			this.year = year;
			this.week = week;
		} // BroadcastWeek

		// ----------------------------------------------------------------------
		public int Week
		{
			get { return week; }
		} // Week

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
		public BroadcastWeek GetPreviousWeek()
		{
			return AddWeeks( -1 );
		} // GetPreviousWeek

		// ----------------------------------------------------------------------
		public BroadcastWeek GetNextWeek()
		{
			return AddWeeks( 1 );
		} // GetNextWeek

		// ----------------------------------------------------------------------
		public BroadcastWeek AddWeeks( int weeks )
		{
			return new BroadcastWeek( Start.AddDays( weeks * TimeSpec.DaysPerWeek ), Calendar );
		} // AddWeeks

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( string.Format( "{0}/{1}", Year, Week ),
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

		// ----------------------------------------------------------------------
		private static int GetYearOf( DateTime moment )
		{
			int year;
			BroadcastCalendarTool.GetYearOf( moment, out year );
			return year;
		} // GetYearOf

		// ----------------------------------------------------------------------
		private static int GetWeekOf( DateTime moment )
		{
			int year;
			int week;
			BroadcastCalendarTool.GetWeekOf( moment, out year, out week );
			return week;
		} // GetWeekOf

		// ----------------------------------------------------------------------
		private static ITimeRange GetPeriodOf( int year, int week )
		{
			DateTime start = BroadcastCalendarTool.GetStartOfWeek( year, week );
			return new CalendarTimeRange( start, start.AddDays( TimeSpec.DaysPerWeek ) );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int year;
		private readonly int week;

	} // class BroadcastWeek

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
