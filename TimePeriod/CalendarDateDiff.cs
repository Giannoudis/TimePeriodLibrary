// -- FILE ------------------------------------------------------------------
// name       : CalendarDateDiff.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.09.15
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class CalendarDateDiff
	{

		// ----------------------------------------------------------------------
		public CalendarDateDiff() :
			this( new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } ), new DurationProvider() )
		{
		} // CalendarDateDiff

		// ----------------------------------------------------------------------
		public CalendarDateDiff( ITimeCalendar calendar, IDurationProvider durationProvider )
		{
			if ( calendar == null )
			{
				throw new ArgumentNullException( "calendar" );
			}
			if ( durationProvider == null )
			{
				throw new ArgumentNullException( "durationProvider" );
			}
			if ( calendar.StartOffset != TimeSpan.Zero )
			{
				throw new ArgumentOutOfRangeException( "calendar", "start offset" );
			}
			if ( calendar.EndOffset != TimeSpan.Zero )
			{
				throw new ArgumentOutOfRangeException( "calendar", "end offset" );
			}

			this.calendar = calendar;
			this.durationProvider = durationProvider;
		} // CalendarDateDiff

		// ----------------------------------------------------------------------
		public IList<DayOfWeek> WeekDays
		{
			get { return collectorFilter.WeekDays; }
		} // WeekDays

		// ----------------------------------------------------------------------
		public IList<HourRange> WorkingHours
		{
			get { return collectorFilter.CollectingHours; }
		} // WorkingHours

		// ----------------------------------------------------------------------
		public IList<DayHourRange> WorkingDayHours
		{
			get { return collectorFilter.CollectingDayHours; }
		} // WorkingDayHours

		// ----------------------------------------------------------------------
		public ITimeCalendar Calendar
		{
			get { return calendar; }
		} // Calendar

		// ----------------------------------------------------------------------
		public IDurationProvider DurationProvider
		{
			get { return durationProvider; }
		} // DurationProvider

		// ----------------------------------------------------------------------
		public void AddWorkingWeekDays()
		{
			WeekDays.Add( DayOfWeek.Monday );
			WeekDays.Add( DayOfWeek.Tuesday );
			WeekDays.Add( DayOfWeek.Wednesday );
			WeekDays.Add( DayOfWeek.Thursday );
			WeekDays.Add( DayOfWeek.Friday );
		} // AddWorkingWeekDays

		// ----------------------------------------------------------------------
		public void AddWeekendWeekDays()
		{
			WeekDays.Add( DayOfWeek.Saturday );
			WeekDays.Add( DayOfWeek.Sunday );
		} // AddWeekendWeekDays

		// ----------------------------------------------------------------------
		public TimeSpan Difference( DateTime date )
		{
			return Difference( date, ClockProxy.Clock.Now );
		} // Difference

		// ----------------------------------------------------------------------
		public TimeSpan Difference( DateTime date1, DateTime date2 )
		{
			if ( date1.Equals( date2 ) )
			{
				return TimeSpan.Zero;
			}
			if ( collectorFilter.WeekDays.Count == 0 && collectorFilter.CollectingHours.Count == 0 && collectorFilter.CollectingDayHours.Count == 0 )
			{
				return new DateDiff( date1, date2, calendar.Culture.Calendar, calendar.FirstDayOfWeek, calendar.YearBaseMonth ).Difference;
			}

			TimeRange differenceRange = new TimeRange( date1, date2 );

			CalendarPeriodCollector collector = new CalendarPeriodCollector(
				collectorFilter, new TimeRange( differenceRange.Start.Date, differenceRange.End.Date.AddDays( 1 ) ), SeekDirection.Forward, calendar );
			collector.CollectHours();

			// calculate gaps
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>( calendar );
			ITimePeriodCollection gaps = gapCalculator.GetGaps( collector.Periods, differenceRange );

			// calculate difference (sum gap durations)
			TimeSpan difference = gaps.GetTotalDuration( durationProvider );
			return date1 < date2 ? difference : difference.Negate();
		} // Difference

		// ----------------------------------------------------------------------
		// members
		private readonly CalendarPeriodCollectorFilter collectorFilter = new CalendarPeriodCollectorFilter();
		private readonly ITimeCalendar calendar;
		private readonly IDurationProvider durationProvider;

	} // class CalendarDateDiff

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
