// -- FILE ------------------------------------------------------------------
// name       : TimeLine.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimeLine<T> : ITimeLine where T : ITimePeriod, new()
	{

		// ----------------------------------------------------------------------
		public TimeLine( ITimePeriodContainer periods, ITimePeriodMapper periodMapper = null ) :
			this( periods, null, periodMapper )
		{
		} // TimeLine

		// ----------------------------------------------------------------------
		public TimeLine( ITimePeriodContainer periods, ITimePeriod limits = null, ITimePeriodMapper periodMapper = null )
		{
			if ( periods == null )
			{
				throw new ArgumentNullException( "periods" );
			}

			this.limits = limits != null ? new TimeRange( limits ) : new TimeRange( periods );
			this.periods = periods;
			this.periodMapper = periodMapper;
		} // TimeLine

		// ----------------------------------------------------------------------
		public ITimePeriodContainer Periods
		{
			get { return periods; }
		} // Periods

		// ----------------------------------------------------------------------
		public ITimePeriod Limits
		{
			get { return limits; }
		} // Limits

		// ----------------------------------------------------------------------
		public ITimePeriodMapper PeriodMapper
		{
			get { return periodMapper; }
		} // PeriodMapper

		// ----------------------------------------------------------------------
		public bool HasOverlaps()
		{
			return GetTimeLineMoments().HasOverlaps();
		} // HasOverlaps

		// ----------------------------------------------------------------------
		public bool HasGaps()
		{
			return GetTimeLineMoments().HasGaps();
		} // HasGaps

		// ----------------------------------------------------------------------
		public ITimePeriodCollection CombinePeriods()
		{
			if ( periods.Count == 0 )
			{
				return new TimePeriodCollection();
			}

			ITimeLineMomentCollection timeLineMoments = GetTimeLineMoments();
			return timeLineMoments.Count == 0 ? new TimePeriodCollection { new TimeRange( periods ) } : CombinePeriods( timeLineMoments );
		} // CombinePeriods

		// ----------------------------------------------------------------------
		public ITimePeriodCollection IntersectPeriods( bool combinePeriods = true )
		{
			if ( periods.Count == 0 )
			{
				return new TimePeriodCollection();
			}

			ITimeLineMomentCollection timeLineMoments = GetTimeLineMoments();
			if ( timeLineMoments.Count == 0 )
			{
				return new TimePeriodCollection();
			}

			return combinePeriods ? IntersectCombinedPeriods( timeLineMoments ) : IntersectPeriods( timeLineMoments );
		} // IntersectPeriods

		// ----------------------------------------------------------------------
		public ITimePeriodCollection CalculateGaps()
		{
			// exclude periods
			TimePeriodCollection gapPeriods = new TimePeriodCollection();
			foreach ( ITimePeriod period in periods )
			{
				if ( limits.IntersectsWith( period ) )
				{
					gapPeriods.Add( new TimeRange( period ) );
				}
			}

			ITimeLineMomentCollection timeLineMoments = GetTimeLineMoments( gapPeriods );
			if ( timeLineMoments.Count == 0 )
			{
				return new TimePeriodCollection { limits };
			}

			T range = new T();
			range.Setup( MapPeriodStart( limits.Start ), MapPeriodEnd( limits.End ) );
			return CalculateGaps( range, timeLineMoments );
		} // CalculateGaps

		// ----------------------------------------------------------------------
		private ITimeLineMomentCollection GetTimeLineMoments()
		{
			return GetTimeLineMoments( periods );
		} // GetTimeLineMoments

		// ----------------------------------------------------------------------
		private ITimeLineMomentCollection GetTimeLineMoments( ICollection<ITimePeriod> momentPeriods )
		{
			TimeLineMomentCollection timeLineMoments = new TimeLineMomentCollection();

			if ( momentPeriods.Count == 0 )
			{
				return timeLineMoments;
			}

			// setup gap set with all start/end points
			ITimePeriodCollection intersections = new TimePeriodCollection();
			foreach ( ITimePeriod momentPeriod in momentPeriods )
			{
				if ( momentPeriod.IsMoment )
				{
					continue;
				}

				// calculate the intersection between the periods
				ITimeRange intersection = limits.GetIntersection( momentPeriod );
				if ( intersection == null || intersection.IsMoment )
				{
					continue;
				}

				if ( periodMapper != null )
				{
					intersection = new TimeRange( MapPeriodStart( intersection.Start ), MapPeriodEnd( intersection.End ) );
				}

				intersections.Add( intersection );
			}

			timeLineMoments.AddAll( intersections );
			return timeLineMoments;
		} // GetTimeLineMoments

		// ----------------------------------------------------------------------
		private static ITimePeriodCollection CombinePeriods( ITimeLineMomentCollection timeLineMoments )
		{
			TimePeriodCollection periods = new TimePeriodCollection();
			if ( timeLineMoments.IsEmpty )
			{
				return periods;
			}

			// search for periods
			int itemIndex = 0;
			while ( itemIndex < timeLineMoments.Count )
			{
				ITimeLineMoment periodStart = timeLineMoments[ itemIndex ];
				int startCount = periodStart.StartCount;
				if ( startCount == 0 )
				{
					throw new InvalidOperationException();
				}

				// search next period end
				// use balancing to handle overlapping periods
				int balance = startCount;
				ITimeLineMoment periodEnd = null;
				while ( itemIndex < timeLineMoments.Count - 1 && balance > 0 )
				{
					itemIndex++;
					periodEnd = timeLineMoments[ itemIndex ];
					balance += periodEnd.BalanceCount;
				}

				if ( periodEnd == null )
				{
					throw new InvalidOperationException();
				}

				if ( periodEnd.StartCount > 0 ) // touching
				{
					itemIndex++;
					continue;
				}

				// found a period
				if ( itemIndex < timeLineMoments.Count )
				{
					T period = new T();
					period.Setup( periodStart.Moment, periodEnd.Moment );
					periods.Add( period );
				}

				itemIndex++;
			}

			return periods;
		} // CombinePeriods

		// ----------------------------------------------------------------------
		private static ITimePeriodCollection IntersectCombinedPeriods( ITimeLineMomentCollection timeLineMoments )
		{
			TimePeriodCollection periods = new TimePeriodCollection();
			if ( timeLineMoments.IsEmpty )
			{
				return periods;
			}

			// search for periods
			int intersectionStart = -1;
			int balance = 0;
			for ( int i = 0; i < timeLineMoments.Count; i++ )
			{
				ITimeLineMoment moment = timeLineMoments[ i ];

				int startCount = moment.StartCount;
				int endCount = moment.EndCount;
				balance += startCount;
				balance -= endCount;

				// intersection is starting by a period start
				if ( startCount > 0 && balance > 1 && intersectionStart < 0 )
				{
					intersectionStart = i;
					continue;
				}

				// intersection is starting by a period end
				if ( endCount <= 0 || balance > 1 || intersectionStart < 0 )
				{
					continue;
				}

				T period = new T();
				period.Setup( timeLineMoments[ intersectionStart ].Moment, moment.Moment );
				periods.Add( period );
				intersectionStart = -1;
			}

			return periods;
		} // IntersectCombinedPeriods

		// ----------------------------------------------------------------------
		private static ITimePeriodCollection IntersectPeriods( ITimeLineMomentCollection timeLineMoments )
		{
			TimePeriodCollection periods = new TimePeriodCollection();
			if ( timeLineMoments.IsEmpty )
			{
				return periods;
			}

			// search for periods
			int intersectionStart = -1;
			int balance = 0;
			for ( int i = 0; i < timeLineMoments.Count; i++ )
			{
				ITimeLineMoment moment = timeLineMoments[ i ];

				balance += moment.BalanceCount;

				// intersection is starting by a period start
				if ( balance > 1 && intersectionStart < 0 )
				{
					intersectionStart = i;
					continue;
				}

				// intersection is starting by a period end
				if ( intersectionStart < 0 )
				{
					continue;
				}

				T period = new T();
				period.Setup( timeLineMoments[ intersectionStart ].Moment, moment.Moment );
				periods.Add( period );
				intersectionStart = balance > 1 ? i : -1;
			}

			return periods;
		} // IntersectCombinedPeriods

		// ----------------------------------------------------------------------
		private static ITimePeriodCollection CalculateGaps( ITimePeriod range, ITimeLineMomentCollection timeLineMoments )
		{
			TimePeriodCollection gaps = new TimePeriodCollection();
			if ( timeLineMoments.IsEmpty )
			{
				return gaps;
			}

			// range leading gap
			ITimeLineMoment periodStart = timeLineMoments.Min;
			if ( periodStart != null && range.Start < periodStart.Moment )
			{
				T startingGap = new T();
				startingGap.Setup( range.Start, periodStart.Moment );
				gaps.Add( startingGap );
			}

			// search for gaps
			int itemIndex = 0;
			while ( itemIndex < timeLineMoments.Count )
			{
				ITimeLineMoment moment = timeLineMoments[ itemIndex ];
				int startCount = moment.StartCount;
				if ( startCount == 0 )
				{
					throw new InvalidOperationException();
				}

				// search next gap start
				// use balancing to handle overlapping periods
				int balance = startCount;
				ITimeLineMoment gapStart = null;
				while ( itemIndex < timeLineMoments.Count - 1 && balance > 0 )
				{
					itemIndex++;
					gapStart = timeLineMoments[ itemIndex ];
					balance += gapStart.BalanceCount;
				}

				if ( gapStart == null )
				{
					throw new InvalidOperationException();
				}

				if ( gapStart.StartCount > 0 ) // touching
				{
					itemIndex++;
					continue;
				}

				// found a gap
				if ( itemIndex < timeLineMoments.Count - 1 )
				{
					T gap = new T();
					gap.Setup( gapStart.Moment, timeLineMoments[ itemIndex + 1 ].Moment );
					gaps.Add( gap );
				}

				itemIndex++;
			}

			// range closing gap
			ITimeLineMoment periodEnd = timeLineMoments.Max;
			if ( periodEnd != null && range.End > periodEnd.Moment )
			{
				T endingGap = new T();
				endingGap.Setup( periodEnd.Moment, range.End );
				gaps.Add( endingGap );
			}

			return gaps;
		} // CalculateGaps

		// ----------------------------------------------------------------------
		private DateTime MapPeriodStart( DateTime start )
		{
			return periodMapper != null ? periodMapper.UnmapStart( start ) : start;
		} // MapPeriodStart

		// ----------------------------------------------------------------------
		private DateTime MapPeriodEnd( DateTime end )
		{
			return periodMapper != null ? periodMapper.UnmapEnd( end ) : end;
		} // MapPeriodEnd

		// ----------------------------------------------------------------------
		// members
		private readonly ITimeRange limits;
		private readonly ITimePeriodContainer periods;
		private readonly ITimePeriodMapper periodMapper;

	} // class TimeLine

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
