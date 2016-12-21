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
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
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
		[Test]
		public void AnytimeTest()
		{
			Assert.AreEqual( TimeBlock.Anytime.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( TimeBlock.Anytime.End, TimeSpec.MaxPeriodDate );
			Assert.AreEqual( TimeBlock.Anytime.Duration, TimeSpec.MaxPeriodDuration );
			Assert.IsTrue( TimeBlock.Anytime.IsAnytime );
			Assert.IsTrue( TimeBlock.Anytime.IsReadOnly );
			Assert.IsFalse( TimeBlock.Anytime.HasStart );
			Assert.IsFalse( TimeBlock.Anytime.HasEnd );
		} // AnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultTest()
		{
			TimeBlock timeBlock = new TimeBlock();
			Assert.AreNotEqual( timeBlock, TimeBlock.Anytime );
			Assert.AreEqual( timeBlock.GetRelation( TimeBlock.Anytime ), PeriodRelation.ExactMatch );
			Assert.IsTrue( timeBlock.IsAnytime );
			Assert.IsFalse( timeBlock.IsMoment );
			Assert.IsFalse( timeBlock.IsReadOnly );
		} // DefaultTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeBlock timeBlock = new TimeBlock( moment );
			Assert.AreEqual( timeBlock.Start, moment );
			Assert.AreEqual( timeBlock.End, moment );
			Assert.AreEqual( timeBlock.Duration, TimeSpec.MinPeriodDuration );
			Assert.IsTrue( timeBlock.IsMoment );
		} // MomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentByPeriodTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now, TimeSpan.Zero );
			Assert.IsTrue( timeBlock.IsMoment );
		} // MomentByPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void NonMomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeSpan delta = Duration.Millisecond;
			TimeBlock timeBlock = new TimeBlock( moment, moment.Add( delta ) );
			Assert.IsFalse( timeBlock.IsMoment );
			Assert.AreEqual( timeBlock.Duration, delta );
		} // NonMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasStartTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now, TimeSpec.MaxPeriodDate );
			Assert.IsTrue( timeBlock.HasStart );
			Assert.IsFalse( timeBlock.HasEnd );
		} // HasStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasEndTest()
		{
			TimeBlock timeBlock = new TimeBlock( TimeSpec.MinPeriodDate, ClockProxy.Clock.Now );
			Assert.IsFalse( timeBlock.HasStart );
			Assert.IsTrue( timeBlock.HasEnd );
		} // HasEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartEndTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, end );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );
			Assert.IsFalse( timeBlock.IsAnytime );
			Assert.IsFalse( timeBlock.IsMoment );
			Assert.IsFalse( timeBlock.IsReadOnly );
		} // StartEndTest


		// ----------------------------------------------------------------------
		[Test]
		public void StartEndSwapTest()
		{
			TimeBlock timeBlock = new TimeBlock( end, start );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.Duration, duration );
			Assert.AreEqual( timeBlock.End, end );
		} // StartEndSwapTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartTimeSpanTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.Duration, duration );
			Assert.AreEqual( timeBlock.End, end );
			Assert.IsFalse( timeBlock.IsAnytime );
			Assert.IsFalse( timeBlock.IsMoment );
			Assert.IsFalse( timeBlock.IsReadOnly );
		} // StartTimeSpanTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void StartNegativeTimeSpanTest()
		{
			new TimeBlock( start, Duration.Millisecond.Negate() );
		} // StartNegativeTimeSpanTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyConstructorTest()
		{
			TimeBlock source = new TimeBlock( start, duration );
			TimeBlock copy = new TimeBlock( source );
			Assert.AreEqual( source.Start, copy.Start );
			Assert.AreEqual( source.End, copy.End );
			Assert.AreEqual( source.Duration, copy.Duration );
			Assert.AreEqual( source.IsReadOnly, copy.IsReadOnly );
			Assert.AreEqual( source, copy );
		} // CopyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.Duration, duration );
			DateTime changedStart = start.AddHours( -1 );
			timeBlock.Start = changedStart;
			Assert.AreEqual( timeBlock.Start, changedStart );
			Assert.AreEqual( timeBlock.Duration, duration );
		} // StartTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void StartReadOnlyTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ), true );
			timeBlock.Start = timeBlock.Start.AddHours( -1 );
		} // StartReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		public void EndTest()
		{
			TimeBlock timeBlock = new TimeBlock( duration, end );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );
			DateTime changedEnd = end.AddHours( 1 );
			timeBlock.End = changedEnd;
			Assert.AreEqual( timeBlock.End, changedEnd );
			Assert.AreEqual( timeBlock.Duration, duration );
		} // EndTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void EndReadOnlyTest()
		{
			TimeBlock timeBlock = new TimeBlock( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now, true );
			timeBlock.End = timeBlock.End.AddHours( 1 );
		} // EndReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );

			TimeSpan delta = Duration.Hour;
			TimeSpan newDuration = timeBlock.Duration + delta;
			timeBlock.Duration = newDuration;
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end.Add( delta ) );
			Assert.AreEqual( timeBlock.Duration, newDuration );

			timeBlock.Duration = TimeSpec.MinPeriodDuration;
			Assert.AreEqual( timeBlock.Duration, TimeSpec.MinPeriodDuration );
		} // DurationTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxDurationOutOfRangeTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			timeBlock.Duration = TimeSpec.MaxPeriodDuration;
		} // MaxDurationOutOfRangeTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void DurationOutOfRangeTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			timeBlock.Duration = Duration.Millisecond.Negate();
		} // DurationOutOfRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationFromStartTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );

			TimeSpan delta = Duration.Hour;
			TimeSpan newDuration = timeBlock.Duration + delta;
			timeBlock.DurationFromStart( newDuration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end.Add( delta ) );
			Assert.AreEqual( timeBlock.Duration, newDuration );

			timeBlock.DurationFromStart( TimeSpec.MinPeriodDuration );
			Assert.AreEqual( timeBlock.Duration, TimeSpec.MinPeriodDuration );
		} // DurationFromStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationFromEndTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );

			TimeSpan delta = Duration.Hour;
			TimeSpan newDuration = timeBlock.Duration + delta;
			timeBlock.DurationFromEnd( newDuration );
			Assert.AreEqual( timeBlock.Start, start.Subtract( delta ) );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, newDuration );

			timeBlock.DurationFromEnd( TimeSpec.MinPeriodDuration );
			Assert.AreEqual( timeBlock.Duration, TimeSpec.MinPeriodDuration );
		} // DurationFromEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void SetupTest()
		{
			TimeBlock timeBlock1 = new TimeBlock();
			timeBlock1.Setup( TimeSpec.MinPeriodDate, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeBlock1.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeBlock1.End, TimeSpec.MinPeriodDate );

			TimeBlock timeBlock2 = new TimeBlock();
			timeBlock2.Setup( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate );
			Assert.AreEqual( timeBlock2.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeBlock2.End, TimeSpec.MaxPeriodDate );

			TimeBlock timeBlock3 = new TimeBlock();
			timeBlock3.Setup( TimeSpec.MaxPeriodDate, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeBlock3.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeBlock3.End, TimeSpec.MaxPeriodDate );
		} // SetupTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsideDateTimeTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Duration, duration );

			// start
			Assert.IsFalse( timeBlock.HasInside( start.AddMilliseconds( -1 ) ) );
			Assert.IsTrue( timeBlock.HasInside( start ) );
			Assert.IsTrue( timeBlock.HasInside( start.AddMilliseconds( 1 ) ) );

			// end
			Assert.IsTrue( timeBlock.HasInside( end.AddMilliseconds( -1 ) ) );
			Assert.IsTrue( timeBlock.HasInside( end ) );
			Assert.IsFalse( timeBlock.HasInside( end.AddMilliseconds( 1 ) ) );
		} // HasInsideDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsidePeriodTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Duration, duration );

			// before
			TimeBlock before1 = new TimeBlock( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.IsFalse( timeBlock.HasInside( before1 ) );
			TimeBlock before2 = new TimeBlock( start.AddMilliseconds( -1 ), end );
			Assert.IsFalse( timeBlock.HasInside( before2 ) );
			TimeBlock before3 = new TimeBlock( start.AddMilliseconds( -1 ), start );
			Assert.IsFalse( timeBlock.HasInside( before3 ) );

			// after
			TimeBlock after1 = new TimeBlock( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.IsFalse( timeBlock.HasInside( after1 ) );
			TimeBlock after2 = new TimeBlock( start, end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeBlock.HasInside( after2 ) );
			TimeBlock after3 = new TimeBlock( end, end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeBlock.HasInside( after3 ) );

			// inside
			Assert.IsTrue( timeBlock.HasInside( timeBlock ) );
			TimeBlock inside1 = new TimeBlock( start.AddMilliseconds( 1 ), end );
			Assert.IsTrue( timeBlock.HasInside( inside1 ) );
			TimeBlock inside2 = new TimeBlock( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) );
			Assert.IsTrue( timeBlock.HasInside( inside2 ) );
			TimeBlock inside3 = new TimeBlock( start, end.AddMilliseconds( -1 ) );
			Assert.IsTrue( timeBlock.HasInside( inside3 ) );
		} // HasInsidePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( readOnlyTimeBlock.Copy( TimeSpan.Zero ), readOnlyTimeBlock );

			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );

			ITimeBlock noMoveTimeBlock = timeBlock.Copy( TimeSpan.Zero );
			Assert.AreEqual( noMoveTimeBlock.Start, start );
			Assert.AreEqual( noMoveTimeBlock.End, end );
			Assert.AreEqual( noMoveTimeBlock.Duration, duration );

			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			ITimeBlock forwardTimeBlock = timeBlock.Copy( forwardOffset );
			Assert.AreEqual( forwardTimeBlock.Start, start.Add( forwardOffset ) );
			Assert.AreEqual( forwardTimeBlock.End, end.Add( forwardOffset ) );
			Assert.AreEqual( forwardTimeBlock.Duration, duration );

			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			ITimeBlock backwardTimeBlock = timeBlock.Copy( backwardOffset );
			Assert.AreEqual( backwardTimeBlock.Start, start.Add( backwardOffset ) );
			Assert.AreEqual( backwardTimeBlock.End, end.Add( backwardOffset ) );
			Assert.AreEqual( backwardTimeBlock.Duration, duration );
		} // CopyTest

		// ----------------------------------------------------------------------
		[Test]
		public void MoveTest()
		{
			TimeBlock timeBlockMoveZero = new TimeBlock( start, duration );
			timeBlockMoveZero.Move( TimeSpan.Zero );
			Assert.AreEqual( timeBlockMoveZero.Start, start );
			Assert.AreEqual( timeBlockMoveZero.End, end );
			Assert.AreEqual( timeBlockMoveZero.Duration, duration );

			TimeBlock timeBlockMoveForward = new TimeBlock( start, duration );
			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			timeBlockMoveForward.Move( forwardOffset );
			Assert.AreEqual( timeBlockMoveForward.Start, start.Add( forwardOffset ) );
			Assert.AreEqual( timeBlockMoveForward.End, end.Add( forwardOffset ) );

			TimeBlock timeBlockMoveBackward = new TimeBlock( start, duration );
			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			timeBlockMoveBackward.Move( backwardOffset );
			Assert.AreEqual( timeBlockMoveBackward.Start, start.Add( backwardOffset ) );
			Assert.AreEqual( timeBlockMoveBackward.End, end.Add( backwardOffset ) );
		} // MoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetPreviousPeriodTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration, true );
			Assert.IsTrue( readOnlyTimeBlock.GetPreviousPeriod().IsReadOnly );

			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );

			ITimeBlock previousTimeBlock = timeBlock.GetPreviousPeriod();
			Assert.AreEqual( previousTimeBlock.Start, start.Subtract( duration ) );
			Assert.AreEqual( previousTimeBlock.End, start );
			Assert.AreEqual( previousTimeBlock.Duration, duration );

			TimeSpan previousOffset = Duration.Hour.Negate();
			ITimeBlock previousOffsetTimeBlock = timeBlock.GetPreviousPeriod( previousOffset );
			Assert.AreEqual( previousOffsetTimeBlock.Start, start.Subtract( duration ).Add( previousOffset )  );
			Assert.AreEqual( previousOffsetTimeBlock.End, end.Subtract( duration ) .Add( previousOffset )  );
			Assert.AreEqual( previousOffsetTimeBlock.Duration, duration );
		} // GetPreviousPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetNextPeriodTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration, true );
			Assert.IsTrue( readOnlyTimeBlock.GetNextPeriod().IsReadOnly );

			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.AreEqual( timeBlock.End, end );
			Assert.AreEqual( timeBlock.Duration, duration );

			ITimeBlock nextTimeBlock = timeBlock.GetNextPeriod();
			Assert.AreEqual( nextTimeBlock.Start, end );
			Assert.AreEqual( nextTimeBlock.End, end.Add( duration ) );
			Assert.AreEqual( nextTimeBlock.Duration, duration );

			TimeSpan nextOffset = Duration.Hour;
			ITimeBlock nextOffsetTimeBlock = timeBlock.GetNextPeriod( nextOffset );
			Assert.AreEqual( nextOffsetTimeBlock.Start, end.Add( nextOffset ) );
			Assert.AreEqual( nextOffsetTimeBlock.End, end.Add( duration + nextOffset ) );
			Assert.AreEqual( nextOffsetTimeBlock.Duration, duration );
		} // GetNextPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectsWithDurationTest()
		{
			TimeBlock timeBlock = new TimeBlock( start, duration );

			// before
			TimeBlock before1 = new TimeBlock( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.IsFalse( timeBlock.IntersectsWith( before1 ) );
			TimeBlock before2 = new TimeBlock( start.AddMilliseconds( -1 ), start );
			Assert.IsTrue( timeBlock.IntersectsWith( before2 ) );
			TimeBlock before3 = new TimeBlock( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeBlock.IntersectsWith( before3 ) );

			// after
			TimeBlock after1 = new TimeBlock( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.IsFalse( timeBlock.IntersectsWith( after1 ) );
			TimeBlock after2 = new TimeBlock( end, end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeBlock.IntersectsWith( after2 ) );
			TimeBlock after3 = new TimeBlock( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeBlock.IntersectsWith( after3 ) );

			// intersect
			Assert.IsTrue( timeBlock.IntersectsWith( timeBlock ) );
			TimeBlock itersect1 = new TimeBlock( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeBlock.IntersectsWith( itersect1 ) );
			TimeBlock itersect2 = new TimeBlock( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeBlock.IntersectsWith( itersect2 ) );
			TimeBlock itersect3 = new TimeBlock( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsTrue( timeBlock.IntersectsWith( itersect3 ) );
		} // IntersectsWithDurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetIntersectionTest()
		{
			TimeBlock readOnlyTimeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( readOnlyTimeBlock.GetIntersection( readOnlyTimeBlock ), new TimeBlock( readOnlyTimeBlock ) );

			TimeBlock timeBlock = new TimeBlock( start, duration );

			// before
			ITimeBlock before1 = timeBlock.GetIntersection( new TimeBlock( start.AddHours( -2 ), start.AddHours( -1 ) ) );
			Assert.AreEqual( before1, null );
			ITimeBlock before2 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( -1 ), start ) );
			Assert.AreEqual( before2, new TimeBlock( start ) );
			ITimeBlock before3 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( -1 ), start.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( before3, new TimeBlock( start, start.AddMilliseconds( 1 ) ) );

			// after
			ITimeBlock after1 = timeBlock.GetIntersection( new TimeBlock( end.AddHours( 1 ), end.AddHours( 2 ) ) );
			Assert.AreEqual( after1, null );
			ITimeBlock after2 = timeBlock.GetIntersection( new TimeBlock( end, end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( after2, new TimeBlock( end ) );
			ITimeBlock after3 = timeBlock.GetIntersection( new TimeBlock( end.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( after3, new TimeBlock( end.AddMilliseconds( -1 ), end ) );

			// intersect
			Assert.AreEqual( timeBlock.GetIntersection( timeBlock ), timeBlock );
			ITimeBlock itersect1 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( itersect1, timeBlock );
			ITimeBlock itersect2 = timeBlock.GetIntersection( new TimeBlock( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
			Assert.AreEqual( itersect2, new TimeBlock( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
		} // GetIntersectionTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSamePeriodTest()
		{
			TimeBlock timeBlock1 = new TimeBlock( start, duration );
			TimeBlock timeBlock2 = new TimeBlock( start, duration );

			Assert.IsTrue( timeBlock1.IsSamePeriod( timeBlock1 ) );
			Assert.IsTrue( timeBlock2.IsSamePeriod( timeBlock2 ) );

			Assert.IsTrue( timeBlock1.IsSamePeriod( timeBlock2 ) );
			Assert.IsTrue( timeBlock2.IsSamePeriod( timeBlock1 ) );

			Assert.IsFalse( timeBlock1.IsSamePeriod( TimeBlock.Anytime ) );
			Assert.IsFalse( timeBlock2.IsSamePeriod( TimeBlock.Anytime ) );

			timeBlock1.Move( new TimeSpan( 1 ) );
			Assert.IsFalse( timeBlock1.IsSamePeriod( timeBlock2 ) );
			Assert.IsFalse( timeBlock2.IsSamePeriod( timeBlock1 ) );

			timeBlock1.Move( new TimeSpan( -1 ) );
			Assert.IsTrue( timeBlock1.IsSamePeriod( timeBlock2 ) );
			Assert.IsTrue( timeBlock2.IsSamePeriod( timeBlock1 ) );
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
		public void GetRelationTest()
		{
			Assert.AreEqual( testData.Reference.GetRelation( testData.Before ), PeriodRelation.Before );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartTouching ), PeriodRelation.StartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartInside ), PeriodRelation.StartInside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideStartTouching ), PeriodRelation.InsideStartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingStartTouching ), PeriodRelation.EnclosingStartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Enclosing ), PeriodRelation.Enclosing );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingEndTouching ), PeriodRelation.EnclosingEndTouching );
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
			TimeBlock timeBlock = new TimeBlock( start, duration );
			Assert.AreEqual( timeBlock.Start, start );
			Assert.IsTrue( timeBlock.HasStart );
			Assert.AreEqual( timeBlock.End, end );
			Assert.IsTrue( timeBlock.HasEnd );
			Assert.AreEqual( timeBlock.Duration, duration );
			timeBlock.Reset();
			Assert.AreEqual( timeBlock.Start, TimeSpec.MinPeriodDate );
			Assert.IsFalse( timeBlock.HasStart );
			Assert.AreEqual( timeBlock.End, TimeSpec.MaxPeriodDate );
			Assert.IsFalse( timeBlock.HasEnd );
		} // ResetTest

		// ----------------------------------------------------------------------
		[Test]
		public void EqualsTest()
		{
			TimeBlock timeBlock1 = new TimeBlock( start, duration );

			TimeBlock timeBlock2 = new TimeBlock( start, duration );
			Assert.IsTrue( timeBlock1.Equals( timeBlock2 ) );

			TimeBlock timeBlock3 = new TimeBlock( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeBlock1.Equals( timeBlock3 ) );
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
