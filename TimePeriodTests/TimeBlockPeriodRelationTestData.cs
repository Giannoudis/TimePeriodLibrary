// -- FILE ------------------------------------------------------------------
// name       : TimeBlockPeriodRelationTestData.cs
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
	public class TimeBlockPeriodRelationTestData
	{

		// ----------------------------------------------------------------------
		public TimeBlockPeriodRelationTestData( DateTime start, TimeSpan duration, TimeSpan offset )
		{
			if ( offset <= TimeSpan.Zero )
			{
				throw new ArgumentOutOfRangeException( "offset" );
			}
			DateTime end = start.Add( duration );
			Reference = new TimeBlock( start, duration, true );

			DateTime beforeEnd = start.Subtract( offset );
			DateTime beforeStart = beforeEnd.Subtract( Reference.Duration );
			DateTime insideStart = start.Add( offset );
			DateTime insideEnd = end.Subtract( offset );
			DateTime afterStart = end.Add( offset );
			DateTime afterEnd = afterStart.Add( Reference.Duration );

			After = new TimeBlock( beforeStart, beforeEnd, true );
			StartTouching = new TimeBlock( beforeStart, start, true );
			StartInside = new TimeBlock( beforeStart, insideStart, true );
			InsideStartTouching = new TimeBlock( start, afterStart, true );
			EnclosingStartTouching = new TimeBlock( start, insideEnd, true );
			Enclosing = new TimeBlock( insideStart, insideEnd, true );
			EnclosingEndTouching = new TimeBlock( insideStart, end, true );
			ExactMatch = new TimeBlock( start, end, true );
			Inside = new TimeBlock( beforeStart, afterEnd, true );
			InsideEndTouching = new TimeBlock( beforeStart, end, true );
			EndInside = new TimeBlock( insideEnd, afterEnd, true );
			EndTouching = new TimeBlock( end, afterEnd, true );
			Before = new TimeBlock( afterStart, afterEnd, true );

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
		} // TimeBlockPeriodRelationTestData

		// ----------------------------------------------------------------------
		public ICollection<ITimePeriod> AllPeriods
		{
			get { return allPeriods; }
		} // AllPeriods

		// ----------------------------------------------------------------------
		public ITimeBlock Reference { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock Before { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock StartTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock StartInside { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock InsideStartTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock EnclosingStartTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock Inside { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock EnclosingEndTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock ExactMatch { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock Enclosing { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock InsideEndTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock EndInside { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock EndTouching { get; private set; }

		// ----------------------------------------------------------------------
		public ITimeBlock After { get; private set; }

		// ----------------------------------------------------------------------
		// members
		private readonly List<ITimePeriod> allPeriods = new List<ITimePeriod>();

	} // class TimeBlockPeriodRelationTestData

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
