// -- FILE ------------------------------------------------------------------
// name       : TimeBlockTest.cs
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
	
	public sealed class TimeBlockTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		public TimeBlockTest()
		{
			start = ClockProxy.Clock.Now;
			end = start.Add( duration );
			testData = new TimeBlockPeriodRelationTestData( start, duration, offset );
		} // TimeBlockTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void AnytimeTest()
		{
			Assert.Equal( TimeBlock.Anytime.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( TimeBlock.Anytime.End, TimeSpec.MaxPeriodDate );
			Assert.Equal( TimeBlock.Anytime.Duration, TimeSpec.MaxPeriodDuration );
			Assert.True( TimeBlock.Anytime.IsAnytime );
			Assert.True( TimeBlock.Anytime.IsReadOnly );
			Assert.False( TimeBlock.Anytime.HasStart );
			Assert.False( TimeBlock.Anytime.HasEnd );
		} // AnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void DefaultTest()
		{
			TimeBlock timeBlock = new TimeBlock();
			Assert.NotEqual( timeBlock, TimeBlock.Anytime );
			Assert.Equal(PeriodRelation.ExactMatch, timeBlock.GetRelation( TimeBlock.Anytime ));
			Assert.True( timeBlock.IsAnytime );
			Assert.False( timeBlock.IsMoment );
			Assert.False( timeBlock.IsReadOnly );
		} // DefaultTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void MomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeBlock timeBlock = new TimeBlock( moment );
			Assert.Equal( timeBlock.Start, moment );
			Assert.Equal( timeBlock.End, moment );
			Assert.Equal( timeBlock.Duration, TimeSpec.MinPeriodDuration );
			Assert.True( timeBlock.IsMoment );
		} // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void MomentByPeriodTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now, TimeSpan.Zero );
			Assert.True( timeBlock.IsMoment );
		} // MomentByPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void NonMomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeSpan delta = Duration.Millisecond;
			TimeBlock timeBlock = new TimeBlock( moment, moment.Add( delta ) );
			Assert.False( timeBlock.IsMoment );
			Assert.Equal( timeBlock.Duration, delta );
		} // NonMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void HasStartTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now, TimeSpec.MaxPeriodDate );
			Assert.True( timeBlock.HasStart );
			Assert.False( timeBlock.HasEnd );
		} // HasStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void HasEndTest()
		{
			TimeBlock timeBlock = new TimeBlock( TimeSpec.MinPeriodDate, ClockProxy.Clock.Now );
			Assert.False( timeBlock.HasStart );
			Assert.True( timeBlock.HasEnd );
		} // HasEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void StartEndTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, end );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );
			Assert.False( timeBlock.IsAnytime );
			Assert.False( timeBlock.IsMoment );
			Assert.False( timeBlock.IsReadOnly );
		} // StartEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void StartEndSwapTest()
		{
			TimeBlock timeBlock = new TimeBlock( end, start );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.Duration, duration );
			Assert.Equal( timeBlock.End, end );
		} // StartEndSwapTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void StartTimeSpanTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.Duration, duration );
			Assert.Equal( timeBlock.End, end );
			Assert.False( timeBlock.IsAnytime );
			Assert.False( timeBlock.IsMoment );
			Assert.False( timeBlock.IsReadOnly );
		} // StartTimeSpanTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void StartNegativeTimeSpanTest()
		{
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               new TimeBlock( start, Duration.Millisecond.Negate() )));
		} // StartNegativeTimeSpanTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void CopyConstructorTest()
		{
			TimeBlock source = new TimeBlock( start, duration );
			TimeBlock copy = new TimeBlock( source );
			Assert.Equal( source.Start, copy.Start );
			Assert.Equal( source.End, copy.End );
			Assert.Equal( source.Duration, copy.Duration );
			Assert.Equal( source.IsReadOnly, copy.IsReadOnly );
			Assert.Equal( source, copy );
		} // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void StartTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.Duration, duration );
			DateTime changedStart = start.AddHours( -1 );
			timeBlock.Start = changedStart;
			Assert.Equal( timeBlock.Start, changedStart );
			Assert.Equal( timeBlock.Duration, duration );
		} // StartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void StartReadOnlyTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ), true );
            Assert.NotNull(Assert.Throws<NotSupportedException>(() =>
               timeBlock.Start = timeBlock.Start.AddHours( -1 )));
		} // StartReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void EndTest()
		{
			TimeBlock timeBlock = new TimeBlock( duration, end );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );
			DateTime changedEnd = end.AddHours( 1 );
			timeBlock.End = changedEnd;
			Assert.Equal( timeBlock.End, changedEnd );
			Assert.Equal( timeBlock.Duration, duration );
		} // EndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void EndReadOnlyTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now, true );
            Assert.NotNull(Assert.Throws<NotSupportedException>(() =>
               timeBlock.End = timeBlock.End.AddHours( 1 )));
		} // EndReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void DurationTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );

			TimeSpan delta = Duration.Hour;
			TimeSpan newDuration = timeBlock.Duration + delta;
			timeBlock.Duration = newDuration;
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end.Add( delta ) );
			Assert.Equal( timeBlock.Duration, newDuration );

			timeBlock.Duration = TimeSpec.MinPeriodDuration;
			Assert.Equal( timeBlock.Duration, TimeSpec.MinPeriodDuration );
		} // DurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void MaxDurationOutOfRangeTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               timeBlock.Duration = TimeSpec.MaxPeriodDuration ));
		} // MaxDurationOutOfRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void DurationOutOfRangeTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               timeBlock.Duration = Duration.Millisecond.Negate()));
		} // DurationOutOfRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void DurationFromStartTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );

			TimeSpan delta = Duration.Hour;
			TimeSpan newDuration = timeBlock.Duration + delta;
			timeBlock.DurationFromStart( newDuration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end.Add( delta ) );
			Assert.Equal( timeBlock.Duration, newDuration );

			timeBlock.DurationFromStart( TimeSpec.MinPeriodDuration );
			Assert.Equal( timeBlock.Duration, TimeSpec.MinPeriodDuration );
		} // DurationFromStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void DurationFromEndTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );

			TimeSpan delta = Duration.Hour;
			TimeSpan newDuration = timeBlock.Duration + delta;
			timeBlock.DurationFromEnd( newDuration );
			Assert.Equal( timeBlock.Start, start.Subtract( delta ) );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, newDuration );

			timeBlock.DurationFromEnd( TimeSpec.MinPeriodDuration );
			Assert.Equal( timeBlock.Duration, TimeSpec.MinPeriodDuration );
		} // DurationFromEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void SetupTest()
		{
			TimeBlock timeBlock1 = new TimeBlock();
			timeBlock1.Setup( TimeSpec.MinPeriodDate, TimeSpec.MinPeriodDate );
			Assert.Equal( timeBlock1.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( timeBlock1.End, TimeSpec.MinPeriodDate );

			TimeBlock timeBlock2 = new TimeBlock();
			timeBlock2.Setup( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate );
			Assert.Equal( timeBlock2.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( timeBlock2.End, TimeSpec.MaxPeriodDate );

			TimeBlock timeBlock3 = new TimeBlock();
			timeBlock3.Setup( TimeSpec.MaxPeriodDate, TimeSpec.MinPeriodDate );
			Assert.Equal( timeBlock3.Start, TimeSpec.MinPeriodDate );
			Assert.Equal( timeBlock3.End, TimeSpec.MaxPeriodDate );
		} // SetupTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void HasInsideDateTimeTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Duration, duration );

			// start
			Assert.False( timeBlock.HasInside( start.AddMilliseconds( -1 ) ) );
			Assert.True( timeBlock.HasInside( start ) );
			Assert.True( timeBlock.HasInside( start.AddMilliseconds( 1 ) ) );

			// end
			Assert.True( timeBlock.HasInside( end.AddMilliseconds( -1 ) ) );
			Assert.True( timeBlock.HasInside( end ) );
			Assert.False( timeBlock.HasInside( end.AddMilliseconds( 1 ) ) );
		} // HasInsideDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void HasInsidePeriodTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Duration, duration );

			// before
			TimeBlock before1 = new TimeBlock( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.False( timeBlock.HasInside( before1 ) );
			TimeBlock before2 = new TimeBlock( start.AddMilliseconds( -1 ), end );
			Assert.False( timeBlock.HasInside( before2 ) );
			TimeBlock before3 = new TimeBlock( start.AddMilliseconds( -1 ), start );
			Assert.False( timeBlock.HasInside( before3 ) );

			// after
			TimeBlock after1 = new TimeBlock( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.False( timeBlock.HasInside( after1 ) );
			TimeBlock after2 = new TimeBlock( start, end.AddMilliseconds( 1 ) );
			Assert.False( timeBlock.HasInside( after2 ) );
			TimeBlock after3 = new TimeBlock( end, end.AddMilliseconds( 1 ) );
			Assert.False( timeBlock.HasInside( after3 ) );

			// inside
			Assert.True( timeBlock.HasInside( timeBlock ) );
			TimeBlock inside1 = new TimeBlock( start.AddMilliseconds( 1 ), end );
			Assert.True( timeBlock.HasInside( inside1 ) );
			TimeBlock inside2 = new TimeBlock( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) );
			Assert.True( timeBlock.HasInside( inside2 ) );
			TimeBlock inside3 = new TimeBlock( start, end.AddMilliseconds( -1 ) );
			Assert.True( timeBlock.HasInside( inside3 ) );
		} // HasInsidePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void CopyTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration );
			Assert.Equal( readOnlyTimeBlock.Copy( TimeSpan.Zero ), readOnlyTimeBlock );

			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );

			ITimeBlock noMoveTimeBlock = timeBlock.Copy( TimeSpan.Zero );
			Assert.Equal( noMoveTimeBlock.Start, start );
			Assert.Equal( noMoveTimeBlock.End, end );
			Assert.Equal( noMoveTimeBlock.Duration, duration );

			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			ITimeBlock forwardTimeBlock = timeBlock.Copy( forwardOffset );
			Assert.Equal( forwardTimeBlock.Start, start.Add( forwardOffset ) );
			Assert.Equal( forwardTimeBlock.End, end.Add( forwardOffset ) );
			Assert.Equal( forwardTimeBlock.Duration, duration );

			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			ITimeBlock backwardTimeBlock = timeBlock.Copy( backwardOffset );
			Assert.Equal( backwardTimeBlock.Start, start.Add( backwardOffset ) );
			Assert.Equal( backwardTimeBlock.End, end.Add( backwardOffset ) );
			Assert.Equal( backwardTimeBlock.Duration, duration );
		} // CopyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void MoveTest()
		{
			TimeBlock timeBlockMoveZero = new TimeBlock( start, duration );
			timeBlockMoveZero.Move( TimeSpan.Zero );
			Assert.Equal( timeBlockMoveZero.Start, start );
			Assert.Equal( timeBlockMoveZero.End, end );
			Assert.Equal( timeBlockMoveZero.Duration, duration );

			TimeBlock timeBlockMoveForward = new TimeBlock( start, duration );
			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			timeBlockMoveForward.Move( forwardOffset );
			Assert.Equal( timeBlockMoveForward.Start, start.Add( forwardOffset ) );
			Assert.Equal( timeBlockMoveForward.End, end.Add( forwardOffset ) );

			TimeBlock timeBlockMoveBackward = new TimeBlock( start, duration );
			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			timeBlockMoveBackward.Move( backwardOffset );
			Assert.Equal( timeBlockMoveBackward.Start, start.Add( backwardOffset ) );
			Assert.Equal( timeBlockMoveBackward.End, end.Add( backwardOffset ) );
		} // MoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void GetPreviousPeriodTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration, true );
			Assert.True( readOnlyTimeBlock.GetPreviousPeriod().IsReadOnly );

			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );

			ITimeBlock previousTimeBlock = timeBlock.GetPreviousPeriod();
			Assert.Equal( previousTimeBlock.Start, start.Subtract( duration ) );
			Assert.Equal( previousTimeBlock.End, start );
			Assert.Equal( previousTimeBlock.Duration, duration );

			TimeSpan previousOffset = Duration.Hour.Negate();
			ITimeBlock previousOffsetTimeBlock = timeBlock.GetPreviousPeriod( previousOffset );
			Assert.Equal( previousOffsetTimeBlock.Start, start.Subtract( duration ).Add( previousOffset )  );
			Assert.Equal( previousOffsetTimeBlock.End, end.Subtract( duration ) .Add( previousOffset )  );
			Assert.Equal( previousOffsetTimeBlock.Duration, duration );
		} // GetPreviousPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void GetNextPeriodTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration, true );
			Assert.True( readOnlyTimeBlock.GetNextPeriod().IsReadOnly );

			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.Equal( timeBlock.End, end );
			Assert.Equal( timeBlock.Duration, duration );

			ITimeBlock nextTimeBlock = timeBlock.GetNextPeriod();
			Assert.Equal( nextTimeBlock.Start, end );
			Assert.Equal( nextTimeBlock.End, end.Add( duration ) );
			Assert.Equal( nextTimeBlock.Duration, duration );

			TimeSpan nextOffset = Duration.Hour;
			ITimeBlock nextOffsetTimeBlock = timeBlock.GetNextPeriod( nextOffset );
			Assert.Equal( nextOffsetTimeBlock.Start, end.Add( nextOffset ) );
			Assert.Equal( nextOffsetTimeBlock.End, end.Add( duration + nextOffset ) );
			Assert.Equal( nextOffsetTimeBlock.Duration, duration );
		} // GetNextPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void IntersectsWithDurationTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );

			// before
			TimeBlock before1 = new TimeBlock( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.False( timeBlock.IntersectsWith( before1 ) );
			TimeBlock before2 = new TimeBlock( start.AddMilliseconds( -1 ), start );
			Assert.True( timeBlock.IntersectsWith( before2 ) );
			TimeBlock before3 = new TimeBlock( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.True( timeBlock.IntersectsWith( before3 ) );

			// after
			TimeBlock after1 = new TimeBlock( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.False( timeBlock.IntersectsWith( after1 ) );
			TimeBlock after2 = new TimeBlock( end, end.AddMilliseconds( 1 ) );
			Assert.True( timeBlock.IntersectsWith( after2 ) );
			TimeBlock after3 = new TimeBlock( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.True( timeBlock.IntersectsWith( after3 ) );

			// intersect
			Assert.True( timeBlock.IntersectsWith( timeBlock ) );
			TimeBlock itersect1 = new TimeBlock( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.True( timeBlock.IntersectsWith( itersect1 ) );
			TimeBlock itersect2 = new TimeBlock( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.True( timeBlock.IntersectsWith( itersect2 ) );
			TimeBlock itersect3 = new TimeBlock( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.True( timeBlock.IntersectsWith( itersect3 ) );
		} // IntersectsWithDurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void GetIntersectionTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration );
			Assert.Equal( readOnlyTimeBlock.GetIntersection( readOnlyTimeBlock ), new TimeBlock( readOnlyTimeBlock ) );

			TimeBlock timeBlock = new TimeBlock( start, duration );

			// before
			ITimeBlock before1 = timeBlock.GetIntersection( new TimeBlock( start.AddHours( -2 ), start.AddHours( -1 ) ) );
			Assert.Null(before1);
			ITimeBlock before2 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( -1 ), start ) );
			Assert.Equal( before2, new TimeBlock( start ) );
			ITimeBlock before3 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) ) );
			Assert.Equal( before3, new TimeBlock( start, start.AddMilliseconds( 1 ) ) );

			// after
			ITimeBlock after1 = timeBlock.GetIntersection( new TimeBlock( end.AddHours( 1 ), end.AddHours( 2 ) ) );
			Assert.Null(after1);
			ITimeBlock after2 = timeBlock.GetIntersection( new TimeBlock( end, end.AddMilliseconds( 1 ) ) );
			Assert.Equal( after2, new TimeBlock( end ) );
			ITimeBlock after3 = timeBlock.GetIntersection( new TimeBlock( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.Equal( after3, new TimeBlock( end.AddMilliseconds( -1 ), end ) );

			// intersect
			Assert.Equal( timeBlock.GetIntersection( timeBlock ), timeBlock );
			ITimeBlock itersect1 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.Equal( itersect1, timeBlock );
			ITimeBlock itersect2 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
			Assert.Equal( itersect2, new TimeBlock( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
		} // GetIntersectionTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void IsSamePeriodTest()
		{
			TimeBlock timeBlock1 = new TimeBlock( start, duration );
			TimeBlock timeBlock2 = new TimeBlock( start, duration );

			Assert.True( timeBlock1.IsSamePeriod( timeBlock1 ) );
			Assert.True( timeBlock2.IsSamePeriod( timeBlock2 ) );

			Assert.True( timeBlock1.IsSamePeriod( timeBlock2 ) );
			Assert.True( timeBlock2.IsSamePeriod( timeBlock1 ) );

			Assert.False( timeBlock1.IsSamePeriod( TimeBlock.Anytime ) );
			Assert.False( timeBlock2.IsSamePeriod( TimeBlock.Anytime ) );

			timeBlock1.Move( new TimeSpan( 1 ) );
			Assert.False( timeBlock1.IsSamePeriod( timeBlock2 ) );
			Assert.False( timeBlock2.IsSamePeriod( timeBlock1 ) );

			timeBlock1.Move( new TimeSpan( -1 ) );
			Assert.True( timeBlock1.IsSamePeriod( timeBlock2 ) );
			Assert.True( timeBlock2.IsSamePeriod( timeBlock1 ) );
		} // IsSamePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
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
        [Trait("Category", "TimeBlock")]
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
        [Trait("Category", "TimeBlock")]
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
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void GetRelationTest()
		{
			Assert.Equal(PeriodRelation.Before, testData.Reference.GetRelation( testData.Before ));
			Assert.Equal(PeriodRelation.StartTouching, testData.Reference.GetRelation( testData.StartTouching ));
			Assert.Equal(PeriodRelation.StartInside, testData.Reference.GetRelation( testData.StartInside ));
			Assert.Equal(PeriodRelation.InsideStartTouching, testData.Reference.GetRelation( testData.InsideStartTouching ));
			Assert.Equal(PeriodRelation.EnclosingStartTouching, testData.Reference.GetRelation( testData.EnclosingStartTouching ));
			Assert.Equal(PeriodRelation.Enclosing, testData.Reference.GetRelation( testData.Enclosing ));
			Assert.Equal(PeriodRelation.EnclosingEndTouching, testData.Reference.GetRelation( testData.EnclosingEndTouching ));
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
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void ResetTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.Equal( timeBlock.Start, start );
			Assert.True( timeBlock.HasStart );
			Assert.Equal( timeBlock.End, end );
			Assert.True( timeBlock.HasEnd );
			Assert.Equal( timeBlock.Duration, duration );
			timeBlock.Reset();
			Assert.Equal( timeBlock.Start, TimeSpec.MinPeriodDate );
			Assert.False( timeBlock.HasStart );
			Assert.Equal( timeBlock.End, TimeSpec.MaxPeriodDate );
			Assert.False( timeBlock.HasEnd );
		} // ResetTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeBlock")]
        [Fact]
		public void EqualsTest()
		{
			TimeBlock timeBlock1 = new TimeBlock( start, duration );

			TimeBlock timeBlock2 = new TimeBlock( start, duration );
			Assert.True( timeBlock1.Equals( timeBlock2 ) );

			TimeBlock timeBlock3 = new TimeBlock( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.False( timeBlock1.Equals( timeBlock3 ) );
		} // EqualsTest

		// ----------------------------------------------------------------------
		// members
		private readonly TimeSpan duration = Duration.Hour;
		private readonly DateTime start;
		private readonly DateTime end;
		private readonly TimeSpan offset = Duration.Millisecond;
		private readonly TimeBlockPeriodRelationTestData testData;

	} // class TimeBlockTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
