// -- FILE ------------------------------------------------------------------
// name       : CalendarDateAdd.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.04
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class CalendarDateAdd : DateAdd
	{

		// ----------------------------------------------------------------------
		public CalendarDateAdd() :
			this( new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } ) )
		{
		} // CalendarDateAdd

		// ----------------------------------------------------------------------
		public CalendarDateAdd( ITimeCalendar calendar )
		{
			if ( calendar == null )
			{
				throw new ArgumentNullException( "calendar" );
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
		} // CalendarDateAdd

		// ----------------------------------------------------------------------
		public IList<DayOfWeek> WeekDays
		{
			get { return weekDays; }
		} // WeekDays

		// ----------------------------------------------------------------------
		public IList<HourRange> WorkingHours
		{
			get { return workingHours; }
		} // WorkingHours

		// ----------------------------------------------------------------------
		public IList<DayHourRange> WorkingDayHours
		{
			get { return workingDayHours; }
		} // WorkingDayHours

		// ----------------------------------------------------------------------
		public ITimeCalendar Calendar
		{
			get { return calendar; }
		} // Calendar

		// ----------------------------------------------------------------------
		public new ITimePeriodCollection IncludePeriods
		{
			get { throw new NotSupportedException(); }
		} // IncludePeriods

		// ----------------------------------------------------------------------
		public void AddWorkingWeekDays()
		{
			weekDays.Add( DayOfWeek.Monday );
			weekDays.Add( DayOfWeek.Tuesday );
			weekDays.Add( DayOfWeek.Wednesday );
			weekDays.Add( DayOfWeek.Thursday );
			weekDays.Add( DayOfWeek.Friday );
		} // AddWorkingWeekDays

		// ----------------------------------------------------------------------
		public void AddWeekendWeekDays()
		{
			weekDays.Add( DayOfWeek.Saturday );
			weekDays.Add( DayOfWeek.Sunday );
		} // AddWeekendWeekDays

		// ----------------------------------------------------------------------
		public override DateTime? Subtract( DateTime start, TimeSpan offset, SeekBoundaryMode seekBoundaryMode = SeekBoundaryMode.Next )
		{
			if ( weekDays.Count == 0 && ExcludePeriods.Count == 0 && workingHours.Count == 0 )
			{
				return start.Subtract( offset );
			}

			return offset < TimeSpan.Zero ?
				CalculateEnd( start, offset.Negate(), SeekDirection.Forward, seekBoundaryMode ) :
				CalculateEnd( start, offset, SeekDirection.Backward, seekBoundaryMode );
		} // Subtract

		// ----------------------------------------------------------------------
		public override DateTime? Add( DateTime start, TimeSpan offset, SeekBoundaryMode seekBoundaryMode = SeekBoundaryMode.Next )
		{
			if ( weekDays.Count == 0 && ExcludePeriods.Count == 0 && workingHours.Count == 0 )
			{
				return start.Add( offset );
			}

			return offset < TimeSpan.Zero ?
				CalculateEnd( start, offset.Negate(), SeekDirection.Backward, seekBoundaryMode ) :
				CalculateEnd( start, offset, SeekDirection.Forward, seekBoundaryMode );
		} // Add

		// ----------------------------------------------------------------------
		protected DateTime? CalculateEnd( DateTime start, TimeSpan offset,
			SeekDirection seekDirection, SeekBoundaryMode seekBoundaryMode )
		{
			if ( offset < TimeSpan.Zero )
			{
				throw new InvalidOperationException( "time span must be positive" );
			}

			DateTime? endDate = null;
			DateTime moment = start;
			TimeSpan? remaining = offset;
			Week week = new Week( start, calendar );

			// search end date, iteraring week by week
			while ( week != null )
			{
				base.IncludePeriods.Clear();
				base.IncludePeriods.AddAll( GetAvailableWeekPeriods( week ) );

				endDate = CalculateEnd( moment, remaining.Value, seekDirection, seekBoundaryMode, out remaining );
				if ( endDate != null || !remaining.HasValue )
				{
					break;
				}

				switch ( seekDirection )
				{
					case SeekDirection.Forward:
						week = FindNextWeek( week );
						if ( week != null )
						{
							moment = week.Start;
						}
						break;
					case SeekDirection.Backward:
						week = FindPreviousWeek( week );
						if ( week != null )
						{
							moment = week.End;
						}
						break;
				}
			}

			return endDate;
		} // CalculateEnd

		// ----------------------------------------------------------------------
		private Week FindNextWeek( Week current )
		{
			if ( ExcludePeriods.Count == 0 )
			{
				return current.GetNextWeek();
			}

			TimeRange limits = new TimeRange( current.End.AddTicks( 1 ), DateTime.MaxValue );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>( calendar );
			ITimePeriodCollection remainingPeriods = gapCalculator.GetGaps( ExcludePeriods, limits );
			return remainingPeriods.Count > 0 ? new Week( remainingPeriods[ 0 ].Start ) : null;
		} // FindNextWeek

		// ----------------------------------------------------------------------
		private Week FindPreviousWeek( Week current )
		{
			if ( ExcludePeriods.Count == 0 )
			{
				return current.GetPreviousWeek();
			}

			TimeRange limits = new TimeRange( DateTime.MinValue, current.Start.AddTicks( -1 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>( calendar );
			ITimePeriodCollection remainingPeriods = gapCalculator.GetGaps( ExcludePeriods, limits );
			return remainingPeriods.Count > 0 ? new Week( remainingPeriods[ remainingPeriods.Count - 1 ].End ) : null;
		} // FindPreviousWeek

		// ----------------------------------------------------------------------
		private IEnumerable<ITimePeriod> GetAvailableWeekPeriods( ITimePeriod period )
		{
			if ( weekDays.Count == 0 && workingHours.Count == 0 && WorkingDayHours.Count == 0 )
			{
				return new TimePeriodCollection { period };
			}

			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();

			// days
			foreach ( DayOfWeek weekDay in weekDays )
			{
				filter.WeekDays.Add( weekDay );
			}

			// hours
			foreach ( HourRange workingHour in workingHours )
			{
				filter.CollectingHours.Add( workingHour );
			}

			// day hours
			foreach ( DayHourRange workingDayHour in workingDayHours )
			{
				filter.CollectingDayHours.Add( workingDayHour );
			}

			CalendarPeriodCollector weekCollector =
				new CalendarPeriodCollector( filter, period, SeekDirection.Forward, calendar );
			weekCollector.CollectHours();
			return weekCollector.Periods;
		} // GetAvailableWeekPeriods

		// ----------------------------------------------------------------------
		// members
		private readonly List<DayOfWeek> weekDays = new List<DayOfWeek>();
		private readonly List<HourRange> workingHours = new List<HourRange>();
		private readonly List<DayHourRange> workingDayHours = new List<DayHourRange>();
		private readonly ITimeCalendar calendar;

	} // class CalendarDateAdd

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
