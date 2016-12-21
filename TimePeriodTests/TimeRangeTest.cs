// -- FILE ------------------------------------------------------------------
// name       : TimeRangeTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class TimeRangeTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		public TimeRangeTest()
		{
			start = ClockProxy.Clock.Now;
			end = start.Add( duration );
			testData = new TimeRangePeriodRelationTestData( start, end, offset );
		} // TimeRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void AnytimeTest()
		{
			Assert.AreEqual( TimeRange.Anytime.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( TimeRange.Anytime.End, TimeSpec.MaxPeriodDate );
			Assert.IsTrue( TimeRange.Anytime.IsAnytime );
			Assert.IsTrue( TimeRange.Anytime.IsReadOnly );
			Assert.IsFalse( TimeRange.Anytime.HasStart );
			Assert.IsFalse( TimeRange.Anytime.HasEnd );
		} // AnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultTest()
		{
			TimeRange timeRange = new TimeRange();
			Assert.AreNotEqual( timeRange, TimeRange.Anytime );
			Assert.AreEqual( timeRange.GetRelation( TimeRange.Anytime ), PeriodRelation.ExactMatch );
			Assert.IsTrue( timeRange.IsAnytime );
			Assert.IsFalse( timeRange.IsMoment );
			Assert.IsFalse( timeRange.IsReadOnly );
		} // DefaultTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeRange timeRange = new TimeRange( moment );
			Assert.AreEqual( timeRange.Start, moment );
			Assert.AreEqual( timeRange.End, moment );
			Assert.IsTrue( timeRange.IsMoment );
		} // MomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentByPeriodTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, TimeSpan.Zero );
			Assert.IsTrue( timeRange.IsMoment );
		} // MomentByPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void NonMomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeRange timeRange = new TimeRange( moment, moment.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeRange.IsMoment );
		} // NonMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasStartTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, TimeSpec.MaxPeriodDate );
			Assert.IsTrue( timeRange.HasStart );
			Assert.IsFalse( timeRange.HasEnd );
		} // HasStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasEndTest()
		{
			TimeRange timeRange = new TimeRange( TimeSpec.MinPeriodDate, ClockProxy.Clock.Now );
			Assert.IsFalse( timeRange.HasStart );
			Assert.IsTrue( timeRange.HasEnd );
		} // HasEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartEndTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.AreEqual( timeRange.Start, start );
			Assert.AreEqual( timeRange.End, end );
			Assert.AreEqual( timeRange.Duration, duration );
			Assert.IsFalse( timeRange.IsAnytime );
			Assert.IsFalse( timeRange.IsMoment );
			Assert.IsFalse( timeRange.IsReadOnly );
		} // StartEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartEndSwapTest()
		{
			TimeRange timeRange = new TimeRange( end, start );
			Assert.AreEqual( timeRange.Start, start );
			Assert.AreEqual( timeRange.Duration, duration );
			Assert.AreEqual( timeRange.End, end );
		} // StartEndSwapTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartTimeSpanTest()
		{
			TimeRange timeRange = new TimeRange( start, duration );
			Assert.AreEqual( timeRange.Start, start );
			Assert.AreEqual( timeRange.Duration, duration );
			Assert.AreEqual( timeRange.End, end );
			Assert.IsFalse( timeRange.IsAnytime );
			Assert.IsFalse( timeRange.IsMoment );
			Assert.IsFalse( timeRange.IsReadOnly );
		} // StartTimeSpanTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartNegativeTimeSpanTest()
		{
			TimeSpan timeSpan = Duration.Millisecond.Negate();
			TimeRange timeRange = new TimeRange( start, timeSpan );
			Assert.AreEqual( timeRange.Start, start.Add( timeSpan ) );
			Assert.AreEqual( timeRange.Duration, timeSpan.Negate() );
			Assert.AreEqual( timeRange.End, start );
			Assert.IsFalse( timeRange.IsAnytime );
			Assert.IsFalse( timeRange.IsMoment );
			Assert.IsFalse( timeRange.IsReadOnly );
		} // StartNegativeTimeSpanTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyConstructorTest()
		{
			TimeRange source = new TimeRange( start, start.AddHours( 1 ), true );
			TimeRange copy = new TimeRange( source );
			Assert.AreEqual( source.Start, copy.Start );
			Assert.AreEqual( source.End, copy.End );
			Assert.AreEqual( source.IsReadOnly, copy.IsReadOnly );
			Assert.AreEqual( source, copy );
		} // CopyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartTest()
		{
			TimeRange timeRange = new TimeRange( start, start.AddHours( 1 ) );
			Assert.AreEqual( timeRange.Start, start );
			DateTime changedStart = start.AddHours( -1 );
			timeRange.Start = changedStart;
			Assert.AreEqual( timeRange.Start, changedStart );
		} // StartTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void StartReadOnlyTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ), true );
			timeRange.Start = timeRange.Start.AddHours( -1 );
		} // StartReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void StartOutOfRangeTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ) );
			timeRange.Start = timeRange.Start.AddHours( 2 );
		} // StartOutOfRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void EndTest()
		{
			TimeRange timeRange = new TimeRange( end.AddHours( -1 ), end );
			Assert.AreEqual( timeRange.End, end );
			DateTime changedEnd = end.AddHours( 1 );
			timeRange.End = changedEnd;
			Assert.AreEqual( timeRange.End, changedEnd );
		} // EndTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void EndReadOnlyTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now, true );
			timeRange.End = timeRange.End.AddHours( 1 );
		} // EndReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void EndOutOfRangeTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now );
			timeRange.End = timeRange.End.AddHours( -2 );
		} // EndOutOfRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void SetupTest()
		{
			TimeRange timeRange1 = new TimeRange();
			timeRange1.Setup( TimeSpec.MinPeriodDate, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeRange1.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeRange1.End, TimeSpec.MinPeriodDate );

			TimeRange timeRange2 = new TimeRange();
			timeRange2.Setup( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate );
			Assert.AreEqual( timeRange2.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeRange2.End, TimeSpec.MaxPeriodDate );

			TimeRange timeRange3 = new TimeRange();
			timeRange3.Setup( TimeSpec.MaxPeriodDate, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeRange3.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeRange3.End, TimeSpec.MaxPeriodDate );
		} // SetupTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsideDateTimeTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.AreEqual( timeRange.End, end );

			// start
			Assert.IsFalse( timeRange.HasInside( start.AddMilliseconds( -1 ) ) );
			Assert.IsTrue( timeRange.HasInside( start ) );
			Assert.IsTrue( timeRange.HasInside( start.AddMilliseconds( 1 ) ) );

			// end
			Assert.IsTrue( timeRange.HasInside( end.AddMilliseconds( -1 ) ) );
			Assert.IsTrue( timeRange.HasInside( end ) );
			Assert.IsFalse( timeRange.HasInside( end.AddMilliseconds( 1 ) ) );
		} // HasInsideDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsidePeriodTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.AreEqual( timeRange.End, end );

			// before
			TimeRange before1 = new TimeRange( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.IsFalse( timeRange.HasInside( before1 ) );
			TimeRange before2 = new TimeRange( start.AddMilliseconds( -1 ), end );
			Assert.IsFalse( timeRange.HasInside( before2 ) );
			TimeRange before3 = new TimeRange( start.AddMilliseconds( -1 ), start );
			Assert.IsFalse( timeRange.HasInside( before3 ) );

			// after
			TimeRange after1 = new TimeRange( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.IsFalse( timeRange.HasInside( after1 ) );
			TimeRange after2 = new TimeRange( start, end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeRange.HasInside( after2 ) );
			TimeRange after3 = new TimeRange( end, end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeRange.HasInside( after3 ) );

			// inside
			Assert.IsTrue( timeRange.HasInside( timeRange ) );
			TimeRange inside1 = new TimeRange( start.AddMilliseconds( 1 ), end );
			Assert.IsTrue( timeRange.HasInside( inside1 ) );
			TimeRange inside2 = new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) );
			Assert.IsTrue( timeRange.HasInside( inside2 ) );
			TimeRange inside3 = new TimeRange( start, end.AddMilliseconds( -1 ) );
			Assert.IsTrue( timeRange.HasInside( inside3 ) );
		} // HasInsidePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyTest()
		{
			TimeRange readOnlyTimeRange = new TimeRange( start, end );
			Assert.AreEqual( readOnlyTimeRange.Copy( TimeSpan.Zero ), readOnlyTimeRange );

			TimeRange timeRange = new TimeRange( start, end );
			Assert.AreEqual( timeRange.Start, start );
			Assert.AreEqual( timeRange.End, end );

			ITimeRange noMoveTimeRange = timeRange.Copy( TimeSpan.Zero );
			Assert.AreEqual( noMoveTimeRange.Start, start );
			Assert.AreEqual( noMoveTimeRange.End, end );
			Assert.AreEqual( noMoveTimeRange.Duration, duration );

			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			ITimeRange forwardTimeRange = timeRange.Copy( forwardOffset );
			Assert.AreEqual( forwardTimeRange.Start, start.Add( forwardOffset ) );
			Assert.AreEqual( forwardTimeRange.End, end.Add( forwardOffset ) );

			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			ITimeRange backwardTimeRange = timeRange.Copy( backwardOffset );
			Assert.AreEqual( backwardTimeRange.Start, start.Add( backwardOffset ) );
			Assert.AreEqual( backwardTimeRange.End, end.Add( backwardOffset ) );
		} // CopyTest

		// ----------------------------------------------------------------------
		[Test]
		public void MoveTest()
		{
			TimeRange timeRangeMoveZero = new TimeRange( start, end );
			timeRangeMoveZero.Move( TimeSpan.Zero );
			Assert.AreEqual( timeRangeMoveZero.Start, start );
			Assert.AreEqual( timeRangeMoveZero.End, end );
			Assert.AreEqual( timeRangeMoveZero.Duration, duration );

			TimeRange timeRangeMoveForward = new TimeRange( start, end );
			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			timeRangeMoveForward.Move( forwardOffset );
			Assert.AreEqual( timeRangeMoveForward.Start, start.Add( forwardOffset ) );
			Assert.AreEqual( timeRangeMoveForward.End, end.Add( forwardOffset ) );

			TimeRange timeRangeMoveBackward = new TimeRange( start, end );
			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			timeRangeMoveBackward.Move( backwardOffset );
			Assert.AreEqual( timeRangeMoveBackward.Start, start.Add( backwardOffset ) );
			Assert.AreEqual( timeRangeMoveBackward.End, end.Add( backwardOffset ) );
		} // MoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandStartToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ExpandStartTo( start.AddMilliseconds( 1 ) );
			Assert.AreEqual( timeRange.Start, start );
			timeRange.ExpandStartTo( start.AddMinutes( -1 ) );
			Assert.AreEqual( timeRange.Start, start.AddMinutes( -1 ) );
		} // ExpandStartToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandEndToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ExpandEndTo( end.AddMilliseconds( -1 ) );
			Assert.AreEqual( timeRange.End, end );
			timeRange.ExpandEndTo( end.AddMinutes( 1 ) );
			Assert.AreEqual( timeRange.End, end.AddMinutes( 1 ) );
		} // ExpandEndToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandToDateTimeTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// start
			timeRange.ExpandTo( start.AddMilliseconds( 1 ) );
			Assert.AreEqual( timeRange.Start, start );
			timeRange.ExpandTo( start.AddMinutes( -1 ) );
			Assert.AreEqual( timeRange.Start, start.AddMinutes( -1 ) );

			// end
			timeRange.ExpandTo( end.AddMilliseconds( -1 ) );
			Assert.AreEqual( timeRange.End, end );
			timeRange.ExpandTo( end.AddMinutes( 1 ) );
			Assert.AreEqual( timeRange.End, end.AddMinutes( 1 ) );
		} // ExpandToDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandToPeriodTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// no expansion
			timeRange.ExpandTo( new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
			Assert.AreEqual( timeRange.Start, start );
			Assert.AreEqual( timeRange.End, end );

			// start
			DateTime changedStart = start.AddMinutes( -1 );
			timeRange.ExpandTo( new TimeRange( changedStart, end ) );
			Assert.AreEqual( timeRange.Start, changedStart );
			Assert.AreEqual( timeRange.End, end );

			// end
			DateTime changedEnd = end.AddMinutes( 1 );
			timeRange.ExpandTo( new TimeRange( changedStart, changedEnd ) );
			Assert.AreEqual( timeRange.Start, changedStart );
			Assert.AreEqual( timeRange.End, changedEnd );

			// start/end
			changedStart = changedStart.AddMinutes( -1 );
			changedEnd = changedEnd.AddMinutes( 1 );
			timeRange.ExpandTo( new TimeRange( changedStart, changedEnd ) );
			Assert.AreEqual( timeRange.Start, changedStart );
			Assert.AreEqual( timeRange.End, changedEnd );
		} // ExpandToPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void ShrinkStartToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ShrinkStartTo( start.AddMilliseconds( -1 ) );
			Assert.AreEqual( timeRange.Start, start );
			timeRange.ShrinkStartTo( start.AddMinutes( 1 ) );
			Assert.AreEqual( timeRange.Start, start.AddMinutes( 1 ) );
		} // ShrinkStartToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ShrinkEndToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ShrinkEndTo( end.AddMilliseconds( 1 ) );
			Assert.AreEqual( timeRange.End, end );
			timeRange.ShrinkEndTo( end.AddMinutes( -1 ) );
			Assert.AreEqual( timeRange.End, end.AddMinutes( -1 ) );
		} // ShrinkEndToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ShrinkToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// no shrink
			timeRange.ShrinkTo( new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( timeRange.Start, start );
			Assert.AreEqual( timeRange.End, end );

			// start
			DateTime changedStart = start.AddMinutes( 1 );
			timeRange.ShrinkTo( new TimeRange( changedStart, end ) );
			Assert.AreEqual( timeRange.Start, changedStart );
			Assert.AreEqual( timeRange.End, end );

			// end
			DateTime changedEnd = end.AddMinutes( -1 );
			timeRange.ShrinkTo( new TimeRange( changedStart, changedEnd ) );
			Assert.AreEqual( timeRange.Start, changedStart );
			Assert.AreEqual( timeRange.End, changedEnd );

			// start/end
			changedStart = changedStart.AddMinutes( 1 );
			changedEnd = changedEnd.AddMinutes( -1 );
			timeRange.ShrinkTo( new TimeRange( changedStart, changedEnd ) );
			Assert.AreEqual( timeRange.Start, changedStart );
			Assert.AreEqual( timeRange.End, changedEnd );
		} // ShrinkToTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSamePeriodTest()
		{
			TimeRange timeRange1 = new TimeRange( start, end );
			TimeRange timeRange2 = new TimeRange( start, end );

			Assert.IsTrue( timeRange1.IsSamePeriod( timeRange1 ) );
			Assert.IsTrue( timeRange2.IsSamePeriod( timeRange2 ) );

			Assert.IsTrue( timeRange1.IsSamePeriod( timeRange2 ) );
			Assert.IsTrue( timeRange2.IsSamePeriod( timeRange1 ) );

			Assert.IsFalse( timeRange1.IsSamePeriod( TimeRange.Anytime ) );
			Assert.IsFalse( timeRange2.IsSamePeriod( TimeRange.Anytime ) );

			timeRange1.Move( new TimeSpan( 1 ) );
			Assert.IsFalse( timeRange1.IsSamePeriod( timeRange2 ) );
			Assert.IsFalse( timeRange2.IsSamePeriod( timeRange1 ) );

			timeRange1.Move( new TimeSpan( -1 ) );
			Assert.IsTrue( timeRange1.IsSamePeriod( timeRange2 ) );
			Assert.IsTrue( timeRange2.IsSamePeriod( timeRange1 ) );
		} // IsSamePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsideTest()
		{
			Assert.IsFalse( testData.Reference.HasInside( testData.Before ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.StartTouching ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.StartInside ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.InsideStartTouching ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.EnclosingStartTouching ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.Enclosing ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.EnclosingEndTouching ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.ExactMatch ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.Inside ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.InsideEndTouching ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.EndInside ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.EndTouching ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.After ) );
		} // HasInsideTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectsWithTest()
		{
			Assert.IsFalse( testData.Reference.IntersectsWith( testData.Before ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.StartTouching ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.StartInside ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.InsideStartTouching ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.EnclosingStartTouching ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.Enclosing ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.EnclosingEndTouching ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.ExactMatch ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.Inside ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.InsideEndTouching ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.EndInside ) );
			Assert.IsTrue( testData.Reference.IntersectsWith( testData.EndTouching ) );
			Assert.IsFalse( testData.Reference.IntersectsWith( testData.After ) );
		} // IntersectsWithTest

		// ----------------------------------------------------------------------
		[Test]
		public void OverlapsWithTest()
		{
			Assert.IsFalse( testData.Reference.OverlapsWith( testData.Before ) );
			Assert.IsFalse( testData.Reference.OverlapsWith( testData.StartTouching ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.StartInside ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.InsideStartTouching ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.EnclosingStartTouching ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.Enclosing ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.EnclosingEndTouching ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.ExactMatch ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.Inside ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.InsideEndTouching ) );
			Assert.IsTrue( testData.Reference.OverlapsWith( testData.EndInside ) );
			Assert.IsFalse( testData.Reference.OverlapsWith( testData.EndTouching ) );
			Assert.IsFalse( testData.Reference.OverlapsWith( testData.After ) );
		} // OverlapsWithTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectsWithDateTimeTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// before
			TimeRange before1 = new TimeRange( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.IsFalse( timeRange.IntersectsWith( before1 ) );
			TimeRange before2 = new TimeRange( start.AddMilliseconds( -1 ), start );
			Assert.IsTrue( timeRange.IntersectsWith( before2 ) );
			TimeRange before3 = new TimeRange( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeRange.IntersectsWith( before3 ) );

			// after
			TimeRange after1 = new TimeRange( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.IsFalse( timeRange.IntersectsWith( after1 ) );
			TimeRange after2 = new TimeRange( end, end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeRange.IntersectsWith( after2 ) );
			TimeRange after3 = new TimeRange( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeRange.IntersectsWith( after3 ) );

			// intersect
			Assert.IsTrue( timeRange.IntersectsWith( timeRange ) );
			TimeRange itersect1 = new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeRange.IntersectsWith( itersect1 ) );
			TimeRange itersect2 = new TimeRange( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeRange.IntersectsWith( itersect2 ) );
			TimeRange itersect3 = new TimeRange( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeRange.IntersectsWith( itersect3 ) );
		} // IntersectsWithDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetIntersectionTest()
		{
			TimeRange readOnlyTimeRange = new TimeRange( start, end );
			Assert.AreEqual( readOnlyTimeRange.GetIntersection( readOnlyTimeRange ), new TimeRange( readOnlyTimeRange ) );

			TimeRange timeRange = new TimeRange( start, end );

			// before
			ITimeRange before1 = timeRange.GetIntersection( new TimeRange( start.AddHours( -2 ), start.AddHours( -1 ) ) );
			Assert.AreEqual( before1, null );
			ITimeRange before2 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( -1 ), start ) );
			Assert.AreEqual( before2, new TimeRange( start ) );
			ITimeRange before3 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( before3, new TimeRange( start, start.AddMilliseconds( 1 ) ) );

			// after
			ITimeRange after1 = timeRange.GetIntersection( new TimeRange( end.AddHours( 1 ), end.AddHours( 2 ) ) );
			Assert.AreEqual( after1, null );
			ITimeRange after2 = timeRange.GetIntersection( new TimeRange( end, end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( after2, new TimeRange( end ) );
			ITimeRange after3 = timeRange.GetIntersection( new TimeRange( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( after3, new TimeRange( end.AddMilliseconds( -1 ), end ) );

			// intersect
			Assert.AreEqual( timeRange.GetIntersection( timeRange ), timeRange );
			ITimeRange itersect1 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( itersect1, timeRange );
			ITimeRange itersect2 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
			Assert.AreEqual( itersect2, new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
		} // GetIntersectionTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetRelationTest()
		{
			Assert.AreEqual( testData.Reference.GetRelation( testData.Before ), PeriodRelation.Before );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartTouching ), PeriodRelation.StartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartInside ), PeriodRelation.StartInside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideStartTouching ), PeriodRelation.InsideStartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Enclosing ), PeriodRelation.Enclosing );
			Assert.AreEqual( testData.Reference.GetRelation( testData.ExactMatch ), PeriodRelation.ExactMatch );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Inside ), PeriodRelation.Inside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideEndTouching ), PeriodRelation.InsideEndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndInside ), PeriodRelation.EndInside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndTouching ), PeriodRelation.EndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.After ), PeriodRelation.After );

			// reference
			Assert.AreEqual( testData.Reference.Start, start );
			Assert.AreEqual( testData.Reference.End, end );
			Assert.IsTrue( testData.Reference.IsReadOnly );

			// after
			Assert.IsTrue( testData.After.IsReadOnly );
			Assert.Less( testData.After.Start, start );
			Assert.Less( testData.After.End, start );
			Assert.IsFalse( testData.Reference.HasInside( testData.After.Start ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.After.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.After ), PeriodRelation.After );

			// start touching
			Assert.IsTrue( testData.StartTouching.IsReadOnly );
			Assert.Less( testData.StartTouching.Start, start );
			Assert.AreEqual( testData.StartTouching.End, start );
			Assert.IsFalse( testData.Reference.HasInside( testData.StartTouching.Start ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.StartTouching.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartTouching ), PeriodRelation.StartTouching );

			// start inside
			Assert.IsTrue( testData.StartInside.IsReadOnly );
			Assert.Less( testData.StartInside.Start, start );
			Assert.Less( testData.StartInside.End, end );
			Assert.Greater( testData.StartInside.End, start );
			Assert.IsFalse( testData.Reference.HasInside( testData.StartInside.Start ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.StartInside.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartInside ), PeriodRelation.StartInside );

			// inside start touching
			Assert.IsTrue( testData.InsideStartTouching.IsReadOnly );
			Assert.AreEqual( testData.InsideStartTouching.Start, start );
			Assert.Greater( testData.InsideStartTouching.End, end );
			Assert.IsTrue( testData.Reference.HasInside( testData.InsideStartTouching.Start ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.InsideStartTouching.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideStartTouching ), PeriodRelation.InsideStartTouching );

			// enclosing start touching
			Assert.IsTrue( testData.EnclosingStartTouching.IsReadOnly );
			Assert.AreEqual( testData.EnclosingStartTouching.Start, start );
			Assert.Less( testData.EnclosingStartTouching.End, end );
			Assert.IsTrue( testData.Reference.HasInside( testData.EnclosingStartTouching.Start ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.EnclosingStartTouching.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingStartTouching ), PeriodRelation.EnclosingStartTouching );

			// enclosing
			Assert.IsTrue( testData.Enclosing.IsReadOnly );
			Assert.Greater( testData.Enclosing.Start, start );
			Assert.Less( testData.Enclosing.End, end );
			Assert.IsTrue( testData.Reference.HasInside( testData.Enclosing.Start ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.Enclosing.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Enclosing ), PeriodRelation.Enclosing );

			// enclosing end touching
			Assert.IsTrue( testData.EnclosingEndTouching.IsReadOnly );
			Assert.Greater( testData.EnclosingEndTouching.Start, start );
			Assert.AreEqual( testData.EnclosingEndTouching.End, end );
			Assert.IsTrue( testData.Reference.HasInside( testData.EnclosingEndTouching.Start ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.EnclosingEndTouching.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingEndTouching ), PeriodRelation.EnclosingEndTouching );

			// exact match
			Assert.IsTrue( testData.ExactMatch.IsReadOnly );
			Assert.AreEqual( testData.ExactMatch.Start, start );
			Assert.AreEqual( testData.ExactMatch.End, end );
			Assert.IsTrue( testData.Reference.Equals( testData.ExactMatch ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.ExactMatch.Start ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.ExactMatch.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.ExactMatch ), PeriodRelation.ExactMatch );

			// inside
			Assert.IsTrue( testData.Inside.IsReadOnly );
			Assert.Less( testData.Inside.Start, start );
			Assert.Greater( testData.Inside.End, end );
			Assert.IsFalse( testData.Reference.HasInside( testData.Inside.Start ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.Inside.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Inside ), PeriodRelation.Inside );

			// inside end touching
			Assert.IsTrue( testData.InsideEndTouching.IsReadOnly );
			Assert.Less( testData.InsideEndTouching.Start, start );
			Assert.AreEqual( testData.InsideEndTouching.End, end );
			Assert.IsFalse( testData.Reference.HasInside( testData.InsideEndTouching.Start ) );
			Assert.IsTrue( testData.Reference.HasInside( testData.InsideEndTouching.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideEndTouching ), PeriodRelation.InsideEndTouching );

			// end inside
			Assert.IsTrue( testData.EndInside.IsReadOnly );
			Assert.Greater( testData.EndInside.Start, start );
			Assert.Less( testData.EndInside.Start, end );
			Assert.Greater( testData.EndInside.End, end );
			Assert.IsTrue( testData.Reference.HasInside( testData.EndInside.Start ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.EndInside.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndInside ), PeriodRelation.EndInside );

			// end touching
			Assert.IsTrue( testData.EndTouching.IsReadOnly );
			Assert.AreEqual( testData.EndTouching.Start, end );
			Assert.Greater( testData.EndTouching.End, end );
			Assert.IsTrue( testData.Reference.HasInside( testData.EndTouching.Start ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.EndTouching.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndTouching ), PeriodRelation.EndTouching );

			// before
			Assert.IsTrue( testData.Before.IsReadOnly );
			Assert.Greater( testData.Before.Start, testData.Reference.End );
			Assert.Greater( testData.Before.End, testData.Reference.End );
			Assert.IsFalse( testData.Reference.HasInside( testData.Before.Start ) );
			Assert.IsFalse( testData.Reference.HasInside( testData.Before.End ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Before ), PeriodRelation.Before );
		} // GetRelationTest

		// ----------------------------------------------------------------------
		[Test]
		public void ResetTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.AreEqual( timeRange.Start, start );
			Assert.IsTrue( timeRange.HasStart );
			Assert.AreEqual( timeRange.End, end );
			Assert.IsTrue( timeRange.HasEnd );
			timeRange.Reset();
			Assert.AreEqual( timeRange.Start, TimeSpec.MinPeriodDate );
			Assert.IsFalse( timeRange.HasStart );
			Assert.AreEqual( timeRange.End, TimeSpec.MaxPeriodDate );
			Assert.IsFalse( timeRange.HasEnd );
		} // ResetTest

		// ----------------------------------------------------------------------
		[Test]
		public void EqualsTest()
		{
			TimeRange timeRange1 = new TimeRange( start, end );

			TimeRange timeRange2 = new TimeRange( start, end );
			Assert.IsTrue( timeRange1.Equals( timeRange2 ) );

			TimeRange timeRange3 = new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeRange1.Equals( timeRange3 ) );
		} // EqualsTest

		// ----------------------------------------------------------------------
		// members
		private readonly TimeSpan duration = Duration.Hour;
		private readonly DateTime start;
		private readonly DateTime end;
		private readonly TimeSpan offset = Duration.Millisecond;
		private readonly TimeRangePeriodRelationTestData testData;

	} // class TimeRangeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
