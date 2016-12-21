// -- FILE ------------------------------------------------------------------
// name       : CalendarTimeRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class CalendarTimeRange : TimeRange, ICalendarTimeRange
	{

		// ----------------------------------------------------------------------
		public CalendarTimeRange( DateTime start, DateTime end ) :
			this( start, end, new TimeCalendar() )
		{
		} // CalendarTimeRange

		// ----------------------------------------------------------------------
		public CalendarTimeRange( DateTime start, DateTime end, ITimeCalendar calendar ) :
			this( new TimeRange( start, end ), calendar )
		{
		} // CalendarTimeRange

		// ----------------------------------------------------------------------
		public CalendarTimeRange( DateTime start, TimeSpan duration ) :
			this( start, duration, new TimeCalendar() )
		{
		} // CalendarTimeRange

		// ----------------------------------------------------------------------
		public CalendarTimeRange( DateTime start, TimeSpan duration, ITimeCalendar calendar ) :
			this( new TimeRange( start, duration ), calendar )
		{
		} // CalendarTimeRange

		// ----------------------------------------------------------------------
		public CalendarTimeRange( ITimePeriod period ) :
			this( period, new TimeCalendar() )
		{
		} // CalendarTimeRange

		// ----------------------------------------------------------------------
		public CalendarTimeRange( ITimePeriod period, ITimeCalendar calendar ) :
			base( ToCalendarTimeRange( period, calendar ), true )
		{
			this.calendar = calendar;
		} // CalendarTimeRange

		// ----------------------------------------------------------------------
		public ITimeCalendar Calendar
		{
			get { return calendar; }
		} // Calendar

		// ----------------------------------------------------------------------
		public YearMonth YearBaseMonth
		{
			get { return Calendar.YearBaseMonth; }
		} // YearBaseMonth

		// ----------------------------------------------------------------------
		public virtual int BaseYear
		{
			get { return Start.Year; }
		}

		// ----------------------------------------------------------------------
		public DateTime FirstMonthStart
		{
			get { return new DateTime( Start.Year, Start.Month, 1 ); }
		} // FirstMonthStart

		// ----------------------------------------------------------------------
		public DateTime LastMonthStart
		{
			get { return new DateTime( End.Year, End.Month, 1 ); }
		} // LastMonthStart

		// ----------------------------------------------------------------------
		public DateTime FirstDayStart
		{
			get { return new DateTime( Start.Year, Start.Month, Start.Day ); }
		} // FirstDayStart

		// ----------------------------------------------------------------------
		public DateTime LastDayStart
		{
			get { return new DateTime( End.Year, End.Month, End.Day ); }
		} // LastDayStart

		// ----------------------------------------------------------------------
		public DateTime FirstHourStart
		{
			get { return new DateTime( Start.Year, Start.Month, Start.Day, Start.Hour, 0, 0 ); }
		} // FirstHourStart

		// ----------------------------------------------------------------------
		public DateTime LastHourStart
		{
			get { return new DateTime( End.Year, End.Month, End.Day, End.Hour, 0, 0 ); }
		} // LastHourStart

		// ----------------------------------------------------------------------
		public DateTime FirstMinuteStart
		{
			get { return new DateTime( Start.Year, Start.Month, Start.Day, Start.Hour, Start.Minute, 0, 0 ); }
		} // FirstMinuteStart

		// ----------------------------------------------------------------------
		public DateTime LastMinuteStart
		{
			get { return new DateTime( End.Year, End.Month, End.Day, End.Hour, End.Minute, 0, 0 ); }
		} // LastMinuteStart

		// ----------------------------------------------------------------------
		public override ITimeRange Copy( TimeSpan offset )
		{
			throw new NotSupportedException();
		} // Copy

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( formatter.GetDateTime( Start ), formatter.GetDateTime( End ), Duration );
		} // Format

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as CalendarTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), calendar );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private bool HasSameData( CalendarTimeRange comp )
		{
			return calendar.Equals( comp.calendar );
		} // HasSameData

		// ----------------------------------------------------------------------
		private static TimeRange ToCalendarTimeRange( ITimePeriod period, ITimePeriodMapper mapper )
		{
			DateTime mappedStart = mapper.MapStart( period.Start );
			DateTime mappedEnd = mapper.MapEnd( period.End );
			if ( mappedEnd <= mappedStart )
			{
				throw new NotSupportedException();
			}
			return new TimeRange( mappedStart, mappedEnd );
		} // ToCalendarTimeRange

		// ----------------------------------------------------------------------
		// members
		private readonly ITimeCalendar calendar;

	} // class CalendarTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
