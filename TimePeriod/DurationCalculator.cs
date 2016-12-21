// -- FILE ------------------------------------------------------------------
// name       : DurationCalculator.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class DurationCalculator
	{

		// ----------------------------------------------------------------------
		public DurationCalculator() :
			this( new DurationProvider() )
		{
		} // DurationCalculator

		// ----------------------------------------------------------------------
		public DurationCalculator( IDurationProvider durationProvider )
		{
			if ( durationProvider == null )
			{
				throw new ArgumentNullException( "durationProvider" );
			}

			this.durationProvider = durationProvider;
		} // DurationCalculator

		// ----------------------------------------------------------------------
		public IDurationProvider DurationProvider
		{
			get { return durationProvider; }
		} // DurationProvider

		// ----------------------------------------------------------------------
		public ITimePeriodCollection IncludePeriods
		{
			get { return includePeriods; }
		} // IncludePeriods

		// ----------------------------------------------------------------------
		public ITimePeriodCollection ExcludePeriods
		{
			get { return excludePeriods; }
		} // ExcludePeriods

		// ----------------------------------------------------------------------
		public void Hours( Time start, Time end )
		{
			Hours( new HourRange( start, end ) );
		} // Hours

		// ----------------------------------------------------------------------
		public void Hours( params HourRange[] hours )
		{
			filter.CollectingHours.Clear();
			foreach ( HourRange hour in hours )
			{
				filter.CollectingHours.Add( hour );
			}
		} // Hours

		// ----------------------------------------------------------------------
		public void DayHours( DayOfWeek dayOfWeek, Time start, Time end )
		{
			DayHours( new DayHourRange( dayOfWeek, start, end ) );
		} // DayHours

		// ----------------------------------------------------------------------
		public void DayHours( params DayHourRange[] dayHours )
		{
			filter.CollectingDayHours.Clear();
			foreach ( DayHourRange dayHour in dayHours )
			{
				filter.CollectingDayHours.Add( dayHour );
			}
		} // DayHours

		// ----------------------------------------------------------------------
		public void WeekDays( params DayOfWeek[] weekDays )
		{
			filter.WeekDays.Clear();
			foreach ( DayOfWeek weekDay in weekDays )
			{
				filter.WeekDays.Add( weekDay );
			}
		} // WeekDays

		// ----------------------------------------------------------------------
		public void WorkingWeekDays()
		{
			filter.WeekDays.Clear();
			filter.AddWorkingWeekDays();
		} // WorkingWeekDays

		// ----------------------------------------------------------------------
		public void WeekendWeekDays()
		{
			filter.WeekDays.Clear();
			filter.AddWeekendWeekDays();
		} // WeekendWeekDays

		// ----------------------------------------------------------------------
		public TimeSpan CalcDuration( ITimeRange period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			return DoCalcDuration( period.Start, period.End );
		} // CalcDuration

		// ----------------------------------------------------------------------
		public TimeSpan CalcDuration( DateTime start, DateTime end )
		{
			return DoCalcDuration( start, end );
		} // CalcDuration

		// ----------------------------------------------------------------------
		public TimeSpan CalcDayllightDuration( ITimeRange period, TimeZoneInfo timeZone )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			if ( timeZone == null )
			{
				timeZone = TimeZoneInfo.Local;
			}
			return DoCalcDuration( period.Start, period.End, timeZone );
		} // CalcDayllightDuration

		// ----------------------------------------------------------------------
		public TimeSpan CalcDayllightDuration( DateTime start, DateTime end, TimeZoneInfo timeZone )
		{
			if ( timeZone == null )
			{
				timeZone = TimeZoneInfo.Local;
			}
			return DoCalcDuration( start, end, timeZone );
		} // CalcDayllightDuration

		// ----------------------------------------------------------------------
		protected virtual TimeSpan DoCalcDuration( DateTime start, DateTime end, TimeZoneInfo timeZone = null )
		{
			if ( start.Equals( end ) )
			{
				return TimeSpan.Zero;
			}

			// test range
			TimeRange testRange = new TimeRange( start, end );

			// search range
			DateTime searchStart = new Day( testRange.Start ).Start;
			DateTime serachEnd = new Day( testRange.End ).GetNextDay().Start;
			TimeRange searchPeriod = new TimeRange( searchStart, serachEnd );

			// exclude periods
			filter.ExcludePeriods.Clear();
			filter.ExcludePeriods.AddAll( excludePeriods );

			// collect hours
			TimeCalendar calendar = new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } );
			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, searchPeriod, SeekDirection.Forward, calendar );
			collector.CollectHours();

			TimeSpan duration = TimeSpan.Zero;
			collector.Periods.AddAll( includePeriods );
			foreach ( ICalendarTimeRange period in collector.Periods )
			{
				// get the intersection of the test-range and the day hours
				ITimePeriod intersection = testRange.GetIntersection( period );
				if ( intersection == null )
				{
					continue;
				}
				duration = duration.Add( durationProvider.GetDuration( intersection.Start, intersection.End ) );
			}

			return start < end ? duration : duration.Negate();
		} // DoCalcDuration

		// ----------------------------------------------------------------------
		// members
		private readonly CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
		private readonly TimePeriodCollection includePeriods = new TimePeriodCollection();
		private readonly TimePeriodCollection excludePeriods = new TimePeriodCollection();
		private readonly IDurationProvider durationProvider;

	} // class DurationCalculator

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
