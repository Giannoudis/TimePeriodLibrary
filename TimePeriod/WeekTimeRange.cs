// -- FILE ------------------------------------------------------------------
// name       : WeekTimeRange.cs
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
	public abstract class WeekTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected WeekTimeRange( int year, int startWeek, int weekCount ) :
			this( year, startWeek, weekCount, new TimeCalendar() )
		{
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		protected WeekTimeRange( int year, int startWeek, int weekCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( year, startWeek, weekCount, calendar ), calendar )
		{
			this.year = year;
			this.startWeek = startWeek;
			this.weekCount = weekCount;
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		protected WeekTimeRange( DateTime moment, int weekCount ) :
			this( moment, weekCount, new TimeCalendar() )
		{
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		protected WeekTimeRange( DateTime moment, int weekCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( moment, weekCount, calendar ), calendar )
		{
			TimeTool.GetWeekOfYear( moment, calendar.Culture, calendar.YearWeekType, out year, out startWeek );
			this.weekCount = weekCount;
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		public int Year
		{
			get { return year; }
		} // Year

		// ----------------------------------------------------------------------
		public int WeekCount
		{
			get { return weekCount; }
		} // WeekCount

		// ----------------------------------------------------------------------
		public int StartWeek
		{
			get { return startWeek; }
		} // StartWeek

		// ----------------------------------------------------------------------
		public int EndWeek
		{
			get { return startWeek + weekCount - 1; }
		} // EndWeek

		// ----------------------------------------------------------------------
		public string StartWeekOfYearName
		{
			get { return Calendar.GetWeekOfYearName( Year, StartWeek ); }
		} // StartWeekOfYearName

		// ----------------------------------------------------------------------
		public string EndWeekOfYearName
		{
			get { return Calendar.GetWeekOfYearName( Year, EndWeek ); }
		} // EndWeekOfYearName

		// ----------------------------------------------------------------------
		public DateTime GetStartOfWeek( int weekIndex )
		{
			if ( weekIndex < 0 || weekIndex >= weekCount )
			{
				throw new ArgumentOutOfRangeException( "weekIndex" );
			}

			DateTime startDate = TimeTool.GetStartOfYearWeek( year, startWeek, Calendar.Culture, Calendar.YearWeekType );
			return startDate.AddDays( weekIndex * TimeSpec.DaysPerWeek );
		} // GetStartOfWeek

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetDays()
		{
			TimePeriodCollection days = new TimePeriodCollection();
			DateTime startDate = TimeTool.GetStartOfYearWeek( year, startWeek, Calendar.Culture, Calendar.YearWeekType );
			int dayCount = weekCount * TimeSpec.DaysPerWeek;
			for ( int i = 0; i < dayCount; i++ )
			{
				days.Add( new Day( startDate.AddDays( i ), Calendar ) );
			}
			return days;
		} // GetDays

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as WeekTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( WeekTimeRange comp )
		{
			return year == comp.year && startWeek == comp.startWeek && weekCount == comp.weekCount;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), year, startWeek, weekCount );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( DateTime moment, int weekCount, ITimeCalendar calendar )
		{
			int year;
			int weekOfYear;
			TimeTool.GetWeekOfYear( moment, calendar.Culture, calendar.YearWeekType, out year, out weekOfYear );
			return GetPeriodOf( year, weekOfYear, weekCount, calendar );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, int weekOfYear, int weekCount, ITimeCalendar calendar )
		{
			if ( weekCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "weekCount" );
			}

			DateTime start = TimeTool.GetStartOfYearWeek( year, weekOfYear, calendar.Culture, calendar.YearWeekType );
			DateTime end = start.AddDays( weekCount * TimeSpec.DaysPerWeek );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int year;
		private readonly int startWeek;
		private readonly int weekCount;

	} // class WeekTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
