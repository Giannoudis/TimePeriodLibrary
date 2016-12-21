// -- FILE ------------------------------------------------------------------
// name       : TimeRangePeriodRelationTestData.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	public class TimeRangePeriodRelationTestData
	{

		// ----------------------------------------------------------------------
		public TimeRangePeriodRelationTestData( DateTime start, DateTime end, TimeSpan offset )
		{
			if ( offset <= TimeSpan.Zero )
			{
				throw new ArgumentOutOfRangeException( "offset" );
			}
			Reference = new TimeRange( start, end, true );

			DateTime beforeEnd = start.Subtract( offset );
			DateTime beforeStart = beforeEnd.Subtract( Reference.Duration );
			DateTime insideStart = start.Add( offset );
			DateTime insideEnd = end.Subtract( offset );
			DateTime afterStart = end.Add( offset );
			DateTime afterEnd = afterStart.Add( Reference.Duration );

			After = new TimeRange( beforeStart, beforeEnd, true );
			StartTouching = new TimeRange( beforeStart, start, true );
			StartInside = new TimeRange( beforeStart, insideStart, true );
			InsideStartTouching = new TimeRange( start, afterStart, true );
			EnclosingStartTouching = new TimeRange( start, insideEnd, true );
			Enclosing = new TimeRange( insideStart, insideEnd, true );
			EnclosingEndTouching = new TimeRange( insideStart, end, true );
			ExactMatch = new TimeRange( start, end, true );
			Inside = new TimeRange( beforeStart, afterEnd, true );
			InsideEndTouching = new TimeRange( beforeStart, end, true );
			EndInside = new TimeRange( insideEnd, afterEnd, true );
			EndTouching = new TimeRange( end, afterEnd, true );
			Before = new TimeRange( afterStart, afterEnd, true );

			allPeriods.Add( Reference );
			allPeriods.Add( After );
			allPeriods.Add( StartTouching );
			allPeriods.Add( StartInside );
			allPeriods.Add( InsideStartTouching );
			allPeriods.Add( EnclosingStartTouching );
			allPeriods.Add( Enclosing );
			allPeriods.Add( EnclosingEndTouching );
			allPeriods.Add( ExactMatch );
			allPeriods.Add( Inside );
			allPeriods.Add( InsideEndTouching );
			allPeriods.Add( EndInside );
			allPeriods.Add( EndTouching );
			allPeriods.Add( Before );
		} // TimeRangePeriodRelationTestData

		// ----------------------------------------------------------------------
		public ICollection<ITimePeriod> AllPeriods
		{
			get { return allPeriods; }
		} // AllPeriods

		// ----------------------------------------------------------------------
		public ITimeRange Reference { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange Before { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange StartTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange StartInside { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange InsideStartTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange EnclosingStartTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange Inside { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange EnclosingEndTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange ExactMatch { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange Enclosing { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange InsideEndTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange EndInside { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange EndTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeRange After { get; private set; }

		// ----------------------------------------------------------------------
		// members
		private readonly List<ITimePeriod> allPeriods = new List<ITimePeriod>();

	} // class TimeRangePeriodRelationTestData

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
