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
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
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
        [Trait("Category", "TimeRange")]
        [Fact]
		public void AnytimeTest()
		{
			Assert.Equal( TimeRange.Anytime.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( TimeRange.Anytime.End, TimeSpec.MaxPeriodDate );
			Assert.True( TimeRange.Anytime.IsAnytime );
			Assert.True( TimeRange.Anytime.IsReadOnly );
			Assert.False( TimeRange.Anytime.HasStart );
			Assert.False( TimeRange.Anytime.HasEnd );
		} // AnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void DefaultTest()
		{
			TimeRange timeRange = new TimeRange();
			Assert.NotEqual( timeRange, TimeRange.Anytime );
			Assert.Equal(PeriodRelation.ExactMatch, timeRange.GetRelation( TimeRange.Anytime ));
			Assert.True( timeRange.IsAnytime );
			Assert.False( timeRange.IsMoment );
			Assert.False( timeRange.IsReadOnly );
		} // DefaultTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void MomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeRange timeRange = new TimeRange( moment );
			Assert.Equal( timeRange.Start, moment );
			Assert.Equal( timeRange.End, moment );
			Assert.True( timeRange.IsMoment );
		} // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void MomentByPeriodTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, TimeSpan.Zero );
			Assert.True( timeRange.IsMoment );
		} // MomentByPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void NonMomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeRange timeRange = new TimeRange( moment, moment.AddMilliseconds( 1 ) );
			Assert.False( timeRange.IsMoment );
		} // NonMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void HasStartTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, TimeSpec.MaxPeriodDate );
			Assert.True( timeRange.HasStart );
			Assert.False( timeRange.HasEnd );
		} // HasStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void HasEndTest()
		{
			TimeRange timeRange = new TimeRange( TimeSpec.MinPeriodDate, ClockProxy.Clock.Now );
			Assert.False( timeRange.HasStart );
			Assert.True( timeRange.HasEnd );
		} // HasEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void StartEndTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.Equal( timeRange.Start, start );
			Assert.Equal( timeRange.End, end );
			Assert.Equal( timeRange.Duration, duration );
			Assert.False( timeRange.IsAnytime );
			Assert.False( timeRange.IsMoment );
			Assert.False( timeRange.IsReadOnly );
		} // StartEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void StartEndSwapTest()
		{
			TimeRange timeRange = new TimeRange( end, start );
			Assert.Equal( timeRange.Start, start );
			Assert.Equal( timeRange.Duration, duration );
			Assert.Equal( timeRange.End, end );
		} // StartEndSwapTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void StartTimeSpanTest()
		{
			TimeRange timeRange = new TimeRange( start, duration );
			Assert.Equal( timeRange.Start, start );
			Assert.Equal( timeRange.Duration, duration );
			Assert.Equal( timeRange.End, end );
			Assert.False( timeRange.IsAnytime );
			Assert.False( timeRange.IsMoment );
			Assert.False( timeRange.IsReadOnly );
		} // StartTimeSpanTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void StartNegativeTimeSpanTest()
		{
			TimeSpan timeSpan = Duration.Millisecond.Negate();
			TimeRange timeRange = new TimeRange( start, timeSpan );
			Assert.Equal( timeRange.Start, start.Add( timeSpan ) );
			Assert.Equal( timeRange.Duration, timeSpan.Negate() );
			Assert.Equal( timeRange.End, start );
			Assert.False( timeRange.IsAnytime );
			Assert.False( timeRange.IsMoment );
			Assert.False( timeRange.IsReadOnly );
		} // StartNegativeTimeSpanTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void CopyConstructorTest()
		{
			TimeRange source = new TimeRange( start, start.AddHours( 1 ), true );
			TimeRange copy = new TimeRange( source );
			Assert.Equal( source.Start, copy.Start );
			Assert.Equal( source.End, copy.End );
			Assert.Equal( source.IsReadOnly, copy.IsReadOnly );
			Assert.Equal( source, copy );
		} // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void StartTest()
		{
			TimeRange timeRange = new TimeRange( start, start.AddHours( 1 ) );
			Assert.Equal( timeRange.Start, start );
			DateTime changedStart = start.AddHours( -1 );
			timeRange.Start = changedStart;
			Assert.Equal( timeRange.Start, changedStart );
		} // StartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void StartReadOnlyTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ), true );
            Assert.NotNull(Assert.Throws<NotSupportedException>(() =>
                timeRange.Start = timeRange.Start.AddHours( -1 )));
		} // StartReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void StartOutOfRangeTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ) );
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                timeRange.Start = timeRange.Start.AddHours( 2 )));
		} // StartOutOfRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void EndTest()
		{
			TimeRange timeRange = new TimeRange( end.AddHours( -1 ), end );
			Assert.Equal( timeRange.End, end );
			DateTime changedEnd = end.AddHours( 1 );
			timeRange.End = changedEnd;
			Assert.Equal( timeRange.End, changedEnd );
		} // EndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void EndReadOnlyTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now, true );
            Assert.NotNull(Assert.Throws<NotSupportedException>(() =>
               timeRange.End = timeRange.End.AddHours( 1 )));
		} // EndReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void EndOutOfRangeTest()
		{
			TimeRange timeRange = new TimeRange( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now );
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                timeRange.End = timeRange.End.AddHours( -2 )));
		} // EndOutOfRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void SetupTest()
		{
			TimeRange timeRange1 = new TimeRange();
			timeRange1.Setup( TimeSpec.MinPeriodDate, TimeSpec.MinPeriodDate );
			Assert.Equal( timeRange1.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( timeRange1.End, TimeSpec.MinPeriodDate );

			TimeRange timeRange2 = new TimeRange();
			timeRange2.Setup( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate );
			Assert.Equal( timeRange2.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( timeRange2.End, TimeSpec.MaxPeriodDate );

			TimeRange timeRange3 = new TimeRange();
			timeRange3.Setup( TimeSpec.MaxPeriodDate, TimeSpec.MinPeriodDate );
			Assert.Equal( timeRange3.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( timeRange3.End, TimeSpec.MaxPeriodDate );
		} // SetupTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void HasInsideDateTimeTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.Equal( timeRange.End, end );

			// start
			Assert.False( timeRange.HasInside( start.AddMilliseconds( -1 ) ) );
			Assert.True( timeRange.HasInside( start ) );
			Assert.True( timeRange.HasInside( start.AddMilliseconds( 1 ) ) );

			// end
			Assert.True( timeRange.HasInside( end.AddMilliseconds( -1 ) ) );
			Assert.True( timeRange.HasInside( end ) );
			Assert.False( timeRange.HasInside( end.AddMilliseconds( 1 ) ) );
		} // HasInsideDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void HasInsidePeriodTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.Equal( timeRange.End, end );

			// before
			TimeRange before1 = new TimeRange( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.False( timeRange.HasInside( before1 ) );
			TimeRange before2 = new TimeRange( start.AddMilliseconds( -1 ), end );
			Assert.False( timeRange.HasInside( before2 ) );
			TimeRange before3 = new TimeRange( start.AddMilliseconds( -1 ), start );
			Assert.False( timeRange.HasInside( before3 ) );

			// after
			TimeRange after1 = new TimeRange( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.False( timeRange.HasInside( after1 ) );
			TimeRange after2 = new TimeRange( start, end.AddMilliseconds( 1 ) );
			Assert.False( timeRange.HasInside( after2 ) );
			TimeRange after3 = new TimeRange( end, end.AddMilliseconds( 1 ) );
			Assert.False( timeRange.HasInside( after3 ) );

			// inside
			Assert.True( timeRange.HasInside( timeRange ) );
			TimeRange inside1 = new TimeRange( start.AddMilliseconds( 1 ), end );
			Assert.True( timeRange.HasInside( inside1 ) );
			TimeRange inside2 = new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) );
			Assert.True( timeRange.HasInside( inside2 ) );
			TimeRange inside3 = new TimeRange( start, end.AddMilliseconds( -1 ) );
			Assert.True( timeRange.HasInside( inside3 ) );
		} // HasInsidePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void CopyTest()
		{
			TimeRange readOnlyTimeRange = new TimeRange( start, end );
			Assert.Equal( readOnlyTimeRange.Copy( TimeSpan.Zero ), readOnlyTimeRange );

			TimeRange timeRange = new TimeRange( start, end );
			Assert.Equal( timeRange.Start, start );
			Assert.Equal( timeRange.End, end );

			ITimeRange noMoveTimeRange = timeRange.Copy( TimeSpan.Zero );
			Assert.Equal( noMoveTimeRange.Start, start );
			Assert.Equal( noMoveTimeRange.End, end );
			Assert.Equal( noMoveTimeRange.Duration, duration );

			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			ITimeRange forwardTimeRange = timeRange.Copy( forwardOffset );
			Assert.Equal( forwardTimeRange.Start, start.Add( forwardOffset ) );
			Assert.Equal( forwardTimeRange.End, end.Add( forwardOffset ) );

			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			ITimeRange backwardTimeRange = timeRange.Copy( backwardOffset );
			Assert.Equal( backwardTimeRange.Start, start.Add( backwardOffset ) );
			Assert.Equal( backwardTimeRange.End, end.Add( backwardOffset ) );
		} // CopyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void MoveTest()
		{
			TimeRange timeRangeMoveZero = new TimeRange( start, end );
			timeRangeMoveZero.Move( TimeSpan.Zero );
			Assert.Equal( timeRangeMoveZero.Start, start );
			Assert.Equal( timeRangeMoveZero.End, end );
			Assert.Equal( timeRangeMoveZero.Duration, duration );

			TimeRange timeRangeMoveForward = new TimeRange( start, end );
			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			timeRangeMoveForward.Move( forwardOffset );
			Assert.Equal( timeRangeMoveForward.Start, start.Add( forwardOffset ) );
			Assert.Equal( timeRangeMoveForward.End, end.Add( forwardOffset ) );

			TimeRange timeRangeMoveBackward = new TimeRange( start, end );
			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			timeRangeMoveBackward.Move( backwardOffset );
			Assert.Equal( timeRangeMoveBackward.Start, start.Add( backwardOffset ) );
			Assert.Equal( timeRangeMoveBackward.End, end.Add( backwardOffset ) );
		} // MoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void ExpandStartToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ExpandStartTo( start.AddMilliseconds( 1 ) );
			Assert.Equal( timeRange.Start, start );
			timeRange.ExpandStartTo( start.AddMinutes( -1 ) );
			Assert.Equal( timeRange.Start, start.AddMinutes( -1 ) );
		} // ExpandStartToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void ExpandEndToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ExpandEndTo( end.AddMilliseconds( -1 ) );
			Assert.Equal( timeRange.End, end );
			timeRange.ExpandEndTo( end.AddMinutes( 1 ) );
			Assert.Equal( timeRange.End, end.AddMinutes( 1 ) );
		} // ExpandEndToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void ExpandToDateTimeTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// start
			timeRange.ExpandTo( start.AddMilliseconds( 1 ) );
			Assert.Equal( timeRange.Start, start );
			timeRange.ExpandTo( start.AddMinutes( -1 ) );
			Assert.Equal( timeRange.Start, start.AddMinutes( -1 ) );

			// end
			timeRange.ExpandTo( end.AddMilliseconds( -1 ) );
			Assert.Equal( timeRange.End, end );
			timeRange.ExpandTo( end.AddMinutes( 1 ) );
			Assert.Equal( timeRange.End, end.AddMinutes( 1 ) );
		} // ExpandToDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void ExpandToPeriodTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// no expansion
			timeRange.ExpandTo( new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
			Assert.Equal( timeRange.Start, start );
			Assert.Equal( timeRange.End, end );

			// start
			DateTime changedStart = start.AddMinutes( -1 );
			timeRange.ExpandTo( new TimeRange( changedStart, end ) );
			Assert.Equal( timeRange.Start, changedStart );
			Assert.Equal( timeRange.End, end );

			// end
			DateTime changedEnd = end.AddMinutes( 1 );
			timeRange.ExpandTo( new TimeRange( changedStart, changedEnd ) );
			Assert.Equal( timeRange.Start, changedStart );
			Assert.Equal( timeRange.End, changedEnd );

			// start/end
			changedStart = changedStart.AddMinutes( -1 );
			changedEnd = changedEnd.AddMinutes( 1 );
			timeRange.ExpandTo( new TimeRange( changedStart, changedEnd ) );
			Assert.Equal( timeRange.Start, changedStart );
			Assert.Equal( timeRange.End, changedEnd );
		} // ExpandToPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void ShrinkStartToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ShrinkStartTo( start.AddMilliseconds( -1 ) );
			Assert.Equal( timeRange.Start, start );
			timeRange.ShrinkStartTo( start.AddMinutes( 1 ) );
			Assert.Equal( timeRange.Start, start.AddMinutes( 1 ) );
		} // ShrinkStartToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void ShrinkEndToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			timeRange.ShrinkEndTo( end.AddMilliseconds( 1 ) );
			Assert.Equal( timeRange.End, end );
			timeRange.ShrinkEndTo( end.AddMinutes( -1 ) );
			Assert.Equal( timeRange.End, end.AddMinutes( -1 ) );
		} // ShrinkEndToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
        public void ShrinkToTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// no shrink
			timeRange.ShrinkTo( new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.Equal( timeRange.Start, start );
			Assert.Equal( timeRange.End, end );

			// start
			DateTime changedStart = start.AddMinutes( 1 );
			timeRange.ShrinkTo( new TimeRange( changedStart, end ) );
			Assert.Equal( timeRange.Start, changedStart );
			Assert.Equal( timeRange.End, end );

			// end
			DateTime changedEnd = end.AddMinutes( -1 );
			timeRange.ShrinkTo( new TimeRange( changedStart, changedEnd ) );
			Assert.Equal( timeRange.Start, changedStart );
			Assert.Equal( timeRange.End, changedEnd );

			// start/end
			changedStart = changedStart.AddMinutes( 1 );
			changedEnd = changedEnd.AddMinutes( -1 );
			timeRange.ShrinkTo( new TimeRange( changedStart, changedEnd ) );
			Assert.Equal( timeRange.Start, changedStart );
			Assert.Equal( timeRange.End, changedEnd );
		} // ShrinkToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void IsSamePeriodTest()
		{
			TimeRange timeRange1 = new TimeRange( start, end );
			TimeRange timeRange2 = new TimeRange( start, end );

			Assert.True( timeRange1.IsSamePeriod( timeRange1 ) );
			Assert.True( timeRange2.IsSamePeriod( timeRange2 ) );

			Assert.True( timeRange1.IsSamePeriod( timeRange2 ) );
			Assert.True( timeRange2.IsSamePeriod( timeRange1 ) );

			Assert.False( timeRange1.IsSamePeriod( TimeRange.Anytime ) );
			Assert.False( timeRange2.IsSamePeriod( TimeRange.Anytime ) );

			timeRange1.Move( new TimeSpan( 1 ) );
			Assert.False( timeRange1.IsSamePeriod( timeRange2 ) );
			Assert.False( timeRange2.IsSamePeriod( timeRange1 ) );

			timeRange1.Move( new TimeSpan( -1 ) );
			Assert.True( timeRange1.IsSamePeriod( timeRange2 ) );
			Assert.True( timeRange2.IsSamePeriod( timeRange1 ) );
		} // IsSamePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void HasInsideTest()
		{
			Assert.False( testData.Reference.HasInside( testData.Before ) );
			Assert.False( testData.Reference.HasInside( testData.StartTouching ) );
			Assert.False( testData.Reference.HasInside( testData.StartInside ) );
			Assert.False( testData.Reference.HasInside( testData.InsideStartTouching ) );
			Assert.True( testData.Reference.HasInside( testData.EnclosingStartTouching ) );
			Assert.True( testData.Reference.HasInside( testData.Enclosing ) );
			Assert.True( testData.Reference.HasInside( testData.EnclosingEndTouching ) );
			Assert.True( testData.Reference.HasInside( testData.ExactMatch ) );
			Assert.False( testData.Reference.HasInside( testData.Inside ) );
			Assert.False( testData.Reference.HasInside( testData.InsideEndTouching ) );
			Assert.False( testData.Reference.HasInside( testData.EndInside ) );
			Assert.False( testData.Reference.HasInside( testData.EndTouching ) );
			Assert.False( testData.Reference.HasInside( testData.After ) );
		} // HasInsideTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void IntersectsWithTest()
		{
			Assert.False( testData.Reference.IntersectsWith( testData.Before ) );
			Assert.True( testData.Reference.IntersectsWith( testData.StartTouching ) );
			Assert.True( testData.Reference.IntersectsWith( testData.StartInside ) );
			Assert.True( testData.Reference.IntersectsWith( testData.InsideStartTouching ) );
			Assert.True( testData.Reference.IntersectsWith( testData.EnclosingStartTouching ) );
			Assert.True( testData.Reference.IntersectsWith( testData.Enclosing ) );
			Assert.True( testData.Reference.IntersectsWith( testData.EnclosingEndTouching ) );
			Assert.True( testData.Reference.IntersectsWith( testData.ExactMatch ) );
			Assert.True( testData.Reference.IntersectsWith( testData.Inside ) );
			Assert.True( testData.Reference.IntersectsWith( testData.InsideEndTouching ) );
			Assert.True( testData.Reference.IntersectsWith( testData.EndInside ) );
			Assert.True( testData.Reference.IntersectsWith( testData.EndTouching ) );
			Assert.False( testData.Reference.IntersectsWith( testData.After ) );
		} // IntersectsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void OverlapsWithTest()
		{
			Assert.False( testData.Reference.OverlapsWith( testData.Before ) );
			Assert.False( testData.Reference.OverlapsWith( testData.StartTouching ) );
			Assert.True( testData.Reference.OverlapsWith( testData.StartInside ) );
			Assert.True( testData.Reference.OverlapsWith( testData.InsideStartTouching ) );
			Assert.True( testData.Reference.OverlapsWith( testData.EnclosingStartTouching ) );
			Assert.True( testData.Reference.OverlapsWith( testData.Enclosing ) );
			Assert.True( testData.Reference.OverlapsWith( testData.EnclosingEndTouching ) );
			Assert.True( testData.Reference.OverlapsWith( testData.ExactMatch ) );
			Assert.True( testData.Reference.OverlapsWith( testData.Inside ) );
			Assert.True( testData.Reference.OverlapsWith( testData.InsideEndTouching ) );
			Assert.True( testData.Reference.OverlapsWith( testData.EndInside ) );
			Assert.False( testData.Reference.OverlapsWith( testData.EndTouching ) );
			Assert.False( testData.Reference.OverlapsWith( testData.After ) );
		} // OverlapsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void IntersectsWithDateTimeTest()
		{
			TimeRange timeRange = new TimeRange( start, end );

			// before
			TimeRange before1 = new TimeRange( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.False( timeRange.IntersectsWith( before1 ) );
			TimeRange before2 = new TimeRange( start.AddMilliseconds( -1 ), start );
			Assert.True( timeRange.IntersectsWith( before2 ) );
			TimeRange before3 = new TimeRange( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.True( timeRange.IntersectsWith( before3 ) );

			// after
			TimeRange after1 = new TimeRange( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.False( timeRange.IntersectsWith( after1 ) );
			TimeRange after2 = new TimeRange( end, end.AddMilliseconds( 1 ) );
			Assert.True( timeRange.IntersectsWith( after2 ) );
			TimeRange after3 = new TimeRange( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.True( timeRange.IntersectsWith( after3 ) );

			// intersect
			Assert.True( timeRange.IntersectsWith( timeRange ) );
			TimeRange itersect1 = new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.True( timeRange.IntersectsWith( itersect1 ) );
			TimeRange itersect2 = new TimeRange( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.True( timeRange.IntersectsWith( itersect2 ) );
			TimeRange itersect3 = new TimeRange( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.True( timeRange.IntersectsWith( itersect3 ) );
		} // IntersectsWithDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void GetIntersectionTest()
		{
			TimeRange readOnlyTimeRange = new TimeRange( start, end );
			Assert.Equal( readOnlyTimeRange.GetIntersection( readOnlyTimeRange ), new TimeRange( readOnlyTimeRange ) );

			TimeRange timeRange = new TimeRange( start, end );

			// before
			ITimeRange before1 = timeRange.GetIntersection( new TimeRange( start.AddHours( -2 ), start.AddHours( -1 ) ) );
			Assert.Null(before1);
			ITimeRange before2 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( -1 ), start ) );
			Assert.Equal( before2, new TimeRange( start ) );
			ITimeRange before3 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) ) );
			Assert.Equal( before3, new TimeRange( start, start.AddMilliseconds( 1 ) ) );

			// after
			ITimeRange after1 = timeRange.GetIntersection( new TimeRange( end.AddHours( 1 ), end.AddHours( 2 ) ) );
			Assert.Null(after1);
			ITimeRange after2 = timeRange.GetIntersection( new TimeRange( end, end.AddMilliseconds( 1 ) ) );
			Assert.Equal( after2, new TimeRange( end ) );
			ITimeRange after3 = timeRange.GetIntersection( new TimeRange( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.Equal( after3, new TimeRange( end.AddMilliseconds( -1 ), end ) );

			// intersect
			Assert.Equal( timeRange.GetIntersection( timeRange ), timeRange );
			ITimeRange itersect1 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.Equal( itersect1, timeRange );
			ITimeRange itersect2 = timeRange.GetIntersection( new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
			Assert.Equal( itersect2, new TimeRange( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
		} // GetIntersectionTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void GetRelationTest()
		{
			Assert.Equal(PeriodRelation.Before, testData.Reference.GetRelation( testData.Before ));
			Assert.Equal(PeriodRelation.StartTouching, testData.Reference.GetRelation( testData.StartTouching ));
			Assert.Equal(PeriodRelation.StartInside, testData.Reference.GetRelation( testData.StartInside ));
			Assert.Equal(PeriodRelation.InsideStartTouching, testData.Reference.GetRelation( testData.InsideStartTouching ));
			Assert.Equal(PeriodRelation.Enclosing, testData.Reference.GetRelation( testData.Enclosing ));
			Assert.Equal(PeriodRelation.ExactMatch, testData.Reference.GetRelation( testData.ExactMatch ));
			Assert.Equal(PeriodRelation.Inside, testData.Reference.GetRelation( testData.Inside ));
			Assert.Equal(PeriodRelation.InsideEndTouching, testData.Reference.GetRelation( testData.InsideEndTouching ));
			Assert.Equal(PeriodRelation.EndInside, testData.Reference.GetRelation( testData.EndInside ));
			Assert.Equal(PeriodRelation.EndTouching, testData.Reference.GetRelation( testData.EndTouching ));
			Assert.Equal(PeriodRelation.After, testData.Reference.GetRelation( testData.After ));

			// reference
			Assert.Equal( testData.Reference.Start, start );
			Assert.Equal( testData.Reference.End, end );
			Assert.True( testData.Reference.IsReadOnly );

			// after
			Assert.True( testData.After.IsReadOnly );
			Assert.True( testData.After.Start < start );
			Assert.True( testData.After.End < start );
			Assert.False( testData.Reference.HasInside( testData.After.Start ) );
			Assert.False( testData.Reference.HasInside( testData.After.End ) );
			Assert.Equal(PeriodRelation.After, testData.Reference.GetRelation( testData.After ));

			// start touching
			Assert.True( testData.StartTouching.IsReadOnly );
			Assert.True( testData.StartTouching.Start < start );
			Assert.Equal( testData.StartTouching.End, start );
			Assert.False( testData.Reference.HasInside( testData.StartTouching.Start ) );
			Assert.True( testData.Reference.HasInside( testData.StartTouching.End ) );
			Assert.Equal(PeriodRelation.StartTouching, testData.Reference.GetRelation( testData.StartTouching ));

			// start inside
			Assert.True( testData.StartInside.IsReadOnly );
			Assert.True( testData.StartInside.Start < start );
			Assert.True( testData.StartInside.End < end );
			Assert.True( testData.StartInside.End > start );
			Assert.False( testData.Reference.HasInside( testData.StartInside.Start ) );
			Assert.True( testData.Reference.HasInside( testData.StartInside.End ) );
			Assert.Equal(PeriodRelation.StartInside, testData.Reference.GetRelation( testData.StartInside ));

			// inside start touching
			Assert.True( testData.InsideStartTouching.IsReadOnly );
			Assert.Equal( testData.InsideStartTouching.Start, start );
			Assert.True( testData.InsideStartTouching.End > end );
			Assert.True( testData.Reference.HasInside( testData.InsideStartTouching.Start ) );
			Assert.False( testData.Reference.HasInside( testData.InsideStartTouching.End ) );
			Assert.Equal(PeriodRelation.InsideStartTouching, testData.Reference.GetRelation( testData.InsideStartTouching ));

			// enclosing start touching
			Assert.True( testData.EnclosingStartTouching.IsReadOnly );
			Assert.Equal( testData.EnclosingStartTouching.Start, start );
			Assert.True( testData.EnclosingStartTouching.End < end );
			Assert.True( testData.Reference.HasInside( testData.EnclosingStartTouching.Start ) );
			Assert.True( testData.Reference.HasInside( testData.EnclosingStartTouching.End ) );
			Assert.Equal(PeriodRelation.EnclosingStartTouching, testData.Reference.GetRelation( testData.EnclosingStartTouching ));

			// enclosing
			Assert.True( testData.Enclosing.IsReadOnly );
			Assert.True( testData.Enclosing.Start > start );
			Assert.True( testData.Enclosing.End < end );
			Assert.True( testData.Reference.HasInside( testData.Enclosing.Start ) );
			Assert.True( testData.Reference.HasInside( testData.Enclosing.End ) );
			Assert.Equal(PeriodRelation.Enclosing, testData.Reference.GetRelation( testData.Enclosing ));

			// enclosing end touching
			Assert.True( testData.EnclosingEndTouching.IsReadOnly );
			Assert.True( testData.EnclosingEndTouching.Start > start );
			Assert.Equal( testData.EnclosingEndTouching.End, end );
			Assert.True( testData.Reference.HasInside( testData.EnclosingEndTouching.Start ) );
			Assert.True( testData.Reference.HasInside( testData.EnclosingEndTouching.End ) );
			Assert.Equal(PeriodRelation.EnclosingEndTouching, testData.Reference.GetRelation( testData.EnclosingEndTouching ));

			// exact match
			Assert.True( testData.ExactMatch.IsReadOnly );
			Assert.Equal( testData.ExactMatch.Start, start );
			Assert.Equal( testData.ExactMatch.End, end );
			Assert.True( testData.Reference.Equals( testData.ExactMatch ) );
			Assert.True( testData.Reference.HasInside( testData.ExactMatch.Start ) );
			Assert.True( testData.Reference.HasInside( testData.ExactMatch.End ) );
			Assert.Equal(PeriodRelation.ExactMatch, testData.Reference.GetRelation( testData.ExactMatch ));

			// inside
			Assert.True( testData.Inside.IsReadOnly );
			Assert.True( testData.Inside.Start < start );
			Assert.True( testData.Inside.End > end );
			Assert.False( testData.Reference.HasInside( testData.Inside.Start ) );
			Assert.False( testData.Reference.HasInside( testData.Inside.End ) );
			Assert.Equal(PeriodRelation.Inside, testData.Reference.GetRelation( testData.Inside ));

			// inside end touching
			Assert.True( testData.InsideEndTouching.IsReadOnly );
			Assert.True( testData.InsideEndTouching.Start < start );
			Assert.Equal( testData.InsideEndTouching.End, end );
			Assert.False( testData.Reference.HasInside( testData.InsideEndTouching.Start ) );
			Assert.True( testData.Reference.HasInside( testData.InsideEndTouching.End ) );
			Assert.Equal(PeriodRelation.InsideEndTouching, testData.Reference.GetRelation( testData.InsideEndTouching ));

			// end inside
			Assert.True( testData.EndInside.IsReadOnly );
			Assert.True( testData.EndInside.Start > start );
			Assert.True( testData.EndInside.Start < end );
			Assert.True( testData.EndInside.End > end );
			Assert.True( testData.Reference.HasInside( testData.EndInside.Start ) );
			Assert.False( testData.Reference.HasInside( testData.EndInside.End ) );
			Assert.Equal(PeriodRelation.EndInside, testData.Reference.GetRelation( testData.EndInside ));

			// end touching
			Assert.True( testData.EndTouching.IsReadOnly );
			Assert.Equal( testData.EndTouching.Start, end );
			Assert.True( testData.EndTouching.End > end );
			Assert.True( testData.Reference.HasInside( testData.EndTouching.Start ) );
			Assert.False( testData.Reference.HasInside( testData.EndTouching.End ) );
			Assert.Equal(PeriodRelation.EndTouching, testData.Reference.GetRelation( testData.EndTouching ));

			// before
			Assert.True( testData.Before.IsReadOnly );
			Assert.True( testData.Before.Start > testData.Reference.End );
			Assert.True( testData.Before.End > testData.Reference.End );
			Assert.False( testData.Reference.HasInside( testData.Before.Start ) );
			Assert.False( testData.Reference.HasInside( testData.Before.End ) );
			Assert.Equal(PeriodRelation.Before, testData.Reference.GetRelation( testData.Before ));
		} // GetRelationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void ResetTest()
		{
			TimeRange timeRange = new TimeRange( start, end );
			Assert.Equal( timeRange.Start, start );
			Assert.True( timeRange.HasStart );
			Assert.Equal( timeRange.End, end );
			Assert.True( timeRange.HasEnd );
			timeRange.Reset();
			Assert.Equal( timeRange.Start, TimeSpec.MinPeriodDate );
			Assert.False( timeRange.HasStart );
			Assert.Equal( timeRange.End, TimeSpec.MaxPeriodDate );
			Assert.False( timeRange.HasEnd );
		} // ResetTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeRange")]
        [Fact]
		public void EqualsTest()
		{
			TimeRange timeRange1 = new TimeRange( start, end );

			TimeRange timeRange2 = new TimeRange( start, end );
			Assert.True( timeRange1.Equals( timeRange2 ) );

			TimeRange timeRange3 = new TimeRange( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.False( timeRange1.Equals( timeRange3 ) );
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
