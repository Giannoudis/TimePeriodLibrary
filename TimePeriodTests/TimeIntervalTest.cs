// -- FILE ------------------------------------------------------------------
// name       : TimeIntervalTest.cs
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
	public sealed class TimeIntervalTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		public TimeIntervalTest()
		{
			start = ClockProxy.Clock.Now;
			end = start.Add( duration );
		} // TimeIntervalTest

		// ----------------------------------------------------------------------
		[Test]
		public void AnytimeTest()
		{
			Assert.AreEqual( TimeInterval.Anytime.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( TimeInterval.Anytime.End, TimeSpec.MaxPeriodDate );
			Assert.IsTrue( TimeInterval.Anytime.IsAnytime );
			Assert.IsTrue( TimeInterval.Anytime.IsReadOnly );
			Assert.IsTrue( TimeInterval.Anytime.IsClosed );
			Assert.IsFalse( TimeInterval.Anytime.IsOpen );
			Assert.IsFalse( TimeInterval.Anytime.HasStart );
			Assert.IsFalse( TimeInterval.Anytime.HasEnd );
		} // AnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultTest()
		{
			TimeInterval timeInterval = new TimeInterval();
			Assert.AreNotEqual( timeInterval, TimeInterval.Anytime );
			Assert.AreEqual( timeInterval.GetRelation( TimeInterval.Anytime ), PeriodRelation.ExactMatch );
			Assert.IsTrue( timeInterval.IsAnytime );
			Assert.IsTrue( timeInterval.IsClosed );
			Assert.IsFalse( timeInterval.IsOpen );
			Assert.IsFalse( timeInterval.IsMoment );
			Assert.IsFalse( timeInterval.IsReadOnly );
		} // DefaultTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeInterval timeInterval = new TimeInterval( moment );
			Assert.AreEqual( timeInterval.Start, moment );
			Assert.AreEqual( timeInterval.End, moment );
			Assert.IsTrue( timeInterval.IsMoment );
			Assert.IsTrue( timeInterval.IsDegenerate );
		} // MomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void NonMomentTest()
		{
			DateTime moment = ClockProxy.Clock.Now;
			TimeInterval timeInterval = new TimeInterval( moment, moment.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeInterval.IsMoment );
			Assert.IsFalse( timeInterval.IsDegenerate );
		} // NonMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasStartTest()
		{
			TimeInterval timeInterval1 = new TimeInterval( ClockProxy.Clock.Now, TimeSpec.MaxPeriodDate );
			Assert.IsTrue( timeInterval1.HasStart );
			Assert.IsFalse( timeInterval1.HasEnd );

			TimeInterval timeInterval2 = new TimeInterval( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate );
			Assert.IsFalse( timeInterval2.HasStart );

			TimeInterval timeInterval3 = new TimeInterval(
				TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, IntervalEdge.Open, IntervalEdge.Open );
			Assert.IsTrue( timeInterval3.HasStart );
		} // HasStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasEndTest()
		{
			TimeInterval timeInterval1 = new TimeInterval( TimeSpec.MinPeriodDate, ClockProxy.Clock.Now );
			Assert.IsFalse( timeInterval1.HasStart );
			Assert.IsTrue( timeInterval1.HasEnd );

			TimeInterval timeInterval2 = new TimeInterval( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate );
			Assert.IsFalse( timeInterval2.HasEnd );

			TimeInterval timeInterval3 = new TimeInterval(
				TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, IntervalEdge.Open, IntervalEdge.Open );
			Assert.IsTrue( timeInterval3.HasEnd );
		} // HasEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartEndIncludeTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.AreEqual( timeInterval.Start, start );
			Assert.AreEqual( timeInterval.StartInterval, start );
			Assert.AreEqual( timeInterval.End, end );
			Assert.AreEqual( timeInterval.EndInterval, end );
			Assert.AreEqual( timeInterval.Duration, duration );
			Assert.IsFalse( timeInterval.IsAnytime );
			Assert.IsFalse( timeInterval.IsMoment );
			Assert.IsFalse( timeInterval.IsReadOnly );
		} // StartEndIncludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartEndExcludeTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end, IntervalEdge.Open, IntervalEdge.Open );
			Assert.AreNotEqual( timeInterval.Start, start );
			Assert.AreEqual( timeInterval.StartInterval, start );
			Assert.AreNotEqual( timeInterval.End, end );
			Assert.AreEqual( timeInterval.EndInterval, end );
			Assert.AreEqual( timeInterval.Duration, duration );
			Assert.IsFalse( timeInterval.IsAnytime );
			Assert.IsFalse( timeInterval.IsMoment );
			Assert.IsFalse( timeInterval.IsReadOnly );
		} // StartEndExcludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartEndSwapTest()
		{
			TimeInterval timeInterval = new TimeInterval( end, start );
			Assert.AreEqual( timeInterval.Start, start );
			Assert.AreEqual( timeInterval.StartInterval, start );
			Assert.AreEqual( timeInterval.Duration, duration );
			Assert.AreEqual( timeInterval.End, end );
			Assert.AreEqual( timeInterval.EndInterval, end );
		} // StartEndSwapTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyConstructorTest()
		{
			TimeInterval source = new TimeInterval( start, start.AddHours( 1 ), IntervalEdge.Closed, IntervalEdge.Open, false, true );
			TimeInterval copy = new TimeInterval( source );
			Assert.AreEqual( source.Start, copy.Start );
			Assert.AreEqual( source.StartInterval, copy.StartInterval );
			Assert.AreEqual( source.StartEdge, copy.StartEdge );
			Assert.AreEqual( source.End, copy.End );
			Assert.AreEqual( source.EndInterval, copy.EndInterval );
			Assert.AreEqual( source.EndEdge, copy.EndEdge );
			Assert.AreEqual( source.IsIntervalEnabled, copy.IsIntervalEnabled );
			Assert.AreEqual( source.IsReadOnly, copy.IsReadOnly );
			Assert.AreEqual( source, copy );
		} // CopyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartIntervalIncludeTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, start.AddHours( 1 ) );
			Assert.AreEqual( timeInterval.Start, start );
			DateTime changedStart = start.AddHours( -1 );
			timeInterval.StartInterval = changedStart;
			Assert.AreEqual( timeInterval.StartInterval, changedStart );
		} // StartIntervalIncludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartIntervalExcludeTest()
		{
			DateTime now = DateTime.Now;
			DateTime startHour = new DateTime( now.Year, now.Month, now.Day, 8, 0, 0 );
			TimeInterval timeInterval = new TimeInterval( startHour, startHour.AddHours( 1 ),
				IntervalEdge.Open, IntervalEdge.Open );

			Assert.AreEqual( timeInterval.StartEdge, IntervalEdge.Open );
			Assert.AreNotEqual( timeInterval.Start, timeInterval.StartInterval );
		} // StartIntervalExcludeTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void StartIntervalReadOnlyTest()
		{
			TimeInterval timeInterval = new TimeInterval( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ),
				IntervalEdge.Closed, IntervalEdge.Closed, true, true );
			timeInterval.StartInterval = timeInterval.Start.AddHours( -1 );
		} // StartIntervalReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void StartOutOfRangeTest()
		{
			TimeInterval timeInterval = new TimeInterval( ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours( 1 ) );
			timeInterval.StartInterval = timeInterval.Start.AddHours( 2 );
		} // StartOutOfRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void EndIntervalIncludeTest()
		{
			TimeInterval timeInterval = new TimeInterval( end.AddHours( -1 ), end );
			Assert.AreEqual( timeInterval.End, end );
			DateTime changedEnd = end.AddHours( 1 );
			timeInterval.EndInterval = changedEnd;
			Assert.AreEqual( timeInterval.EndInterval, changedEnd );
		} // IntervalIncludeEnd

		// ----------------------------------------------------------------------
		[Test]
		public void EndIntervalExcludeTest()
		{
			DateTime now = DateTime.Now;
			DateTime startHour = new DateTime( now.Year, now.Month, now.Day, 8, 0, 0 );
			TimeInterval timeInterval = new TimeInterval( startHour, startHour.AddHours( 1 ),
				IntervalEdge.Open, IntervalEdge.Open );

			Assert.AreEqual( timeInterval.EndEdge, IntervalEdge.Open );
			Assert.AreNotEqual( timeInterval.End, timeInterval.EndInterval );
		} // EndIntervalExcludeTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void EndIntervalReadOnlyTest()
		{
			TimeInterval timeInterval = new TimeInterval( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now,
				IntervalEdge.Closed, IntervalEdge.Closed, true, true );
			timeInterval.EndInterval = timeInterval.End.AddHours( 1 );
		} // EndIntervalReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void EndIntervalOutOfRangeTest()
		{
			TimeInterval timeInterval = new TimeInterval( ClockProxy.Clock.Now.AddHours( -1 ), ClockProxy.Clock.Now );
			timeInterval.EndInterval = timeInterval.End.AddHours( -2 );
		} // EndIntervalOutOfRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsStartOpenTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.IsFalse( timeInterval.IsStartOpen );
			timeInterval.StartEdge = IntervalEdge.Open;
			Assert.IsTrue( timeInterval.IsStartOpen );
			timeInterval.StartEdge = IntervalEdge.Closed;
			Assert.IsFalse( timeInterval.IsStartOpen );
		} // IsStartOpenTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsEndOpenTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.IsFalse( timeInterval.IsEndOpen );
			timeInterval.EndEdge = IntervalEdge.Open;
			Assert.IsTrue( timeInterval.IsEndOpen );
			timeInterval.EndEdge = IntervalEdge.Closed;
			Assert.IsFalse( timeInterval.IsEndOpen );
		} // IsEndOpenTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsOpenTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.IsFalse( timeInterval.IsOpen );
			timeInterval.StartEdge = IntervalEdge.Open;
			Assert.IsFalse( timeInterval.IsOpen );
			timeInterval.EndEdge = IntervalEdge.Open;
			Assert.IsTrue( timeInterval.IsOpen );
			timeInterval.StartEdge = IntervalEdge.Closed;
			Assert.IsFalse( timeInterval.IsOpen );
			timeInterval.EndEdge = IntervalEdge.Closed;
			Assert.IsFalse( timeInterval.IsOpen );
		} // IsOpenTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsStartClosedTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.IsTrue( timeInterval.IsStartClosed );
			timeInterval.StartEdge = IntervalEdge.Open;
			Assert.IsFalse( timeInterval.IsStartClosed );
			timeInterval.StartEdge = IntervalEdge.Closed;
			Assert.IsTrue( timeInterval.IsStartClosed );
		} // IsStartClosedTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsEndClosedTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.IsTrue( timeInterval.IsEndClosed );
			timeInterval.EndEdge = IntervalEdge.Open;
			Assert.IsFalse( timeInterval.IsEndClosed );
			timeInterval.EndEdge = IntervalEdge.Closed;
			Assert.IsTrue( timeInterval.IsEndClosed );
		} // IsEndClosedTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsClosedTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.IsTrue( timeInterval.IsClosed );
			timeInterval.StartEdge = IntervalEdge.Open;
			Assert.IsFalse( timeInterval.IsClosed );
			timeInterval.EndEdge = IntervalEdge.Open;
			Assert.IsFalse( timeInterval.IsClosed );
			timeInterval.StartEdge = IntervalEdge.Closed;
			Assert.IsFalse( timeInterval.IsClosed );
			timeInterval.EndEdge = IntervalEdge.Closed;
			Assert.IsTrue( timeInterval.IsClosed );
		} // IsClosedTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsEmptyTest()
		{
			TimeInterval timeInterval = new TimeInterval( start );
			Assert.IsFalse( timeInterval.IsEmpty );
			timeInterval.StartEdge = IntervalEdge.Open;
			Assert.IsTrue( timeInterval.IsEmpty );
			timeInterval.EndEdge = IntervalEdge.Open;
			Assert.IsTrue( timeInterval.IsEmpty );
			timeInterval.StartEdge = IntervalEdge.Closed;
			Assert.IsTrue( timeInterval.IsEmpty );
			timeInterval.EndEdge = IntervalEdge.Closed;
			Assert.IsFalse( timeInterval.IsEmpty );
		} // IsEmptyTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsDegenerateTest()
		{
			TimeInterval timeInterval = new TimeInterval( start );
			Assert.IsTrue( timeInterval.IsDegenerate );
			timeInterval.StartEdge = IntervalEdge.Open;
			Assert.IsFalse( timeInterval.IsDegenerate );
			timeInterval.EndEdge = IntervalEdge.Open;
			Assert.IsFalse( timeInterval.IsDegenerate );
			timeInterval.StartEdge = IntervalEdge.Closed;
			Assert.IsFalse( timeInterval.IsDegenerate );
			timeInterval.EndEdge = IntervalEdge.Closed;
			Assert.IsTrue( timeInterval.IsDegenerate );
		} // IsDegenerateTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsIntervalEnabledTest()
		{
			TimeInterval timeInterval1 = new TimeInterval( start, end );
			TimeInterval timeInterval2 = new TimeInterval( end, end.AddHours( 1 ) );

			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.EndTouching );

			timeInterval1.EndEdge = IntervalEdge.Open;
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.Before );

			timeInterval1.IsIntervalEnabled = false;
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.EndTouching );

			timeInterval1.IsIntervalEnabled = true;
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.Before );

			timeInterval1.EndEdge = IntervalEdge.Closed;
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.EndTouching );
		} // IsIntervalEnabledTest

		// ----------------------------------------------------------------------
		[Test]
		public void SetupTest()
		{
			TimeInterval timeInterval1 = new TimeInterval();
			timeInterval1.Setup( TimeSpec.MinPeriodDate, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeInterval1.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeInterval1.End, TimeSpec.MinPeriodDate );

			TimeInterval timeInterval2 = new TimeInterval();
			timeInterval2.Setup( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate );
			Assert.AreEqual( timeInterval2.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeInterval2.End, TimeSpec.MaxPeriodDate );

			TimeInterval timeInterval3 = new TimeInterval();
			timeInterval3.Setup( TimeSpec.MaxPeriodDate, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeInterval3.Start, TimeSpec.MinPeriodDate );
			Assert.AreEqual( timeInterval3.End, TimeSpec.MaxPeriodDate );
		} // SetupTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsideDateTimeTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.AreEqual( timeInterval.End, end );

			// start
			Assert.IsFalse( timeInterval.HasInside( start.AddMilliseconds( -1 ) ) );
			Assert.IsTrue( timeInterval.HasInside( start ) );
			Assert.IsTrue( timeInterval.HasInside( start.AddMilliseconds( 1 ) ) );

			// end
			Assert.IsTrue( timeInterval.HasInside( end.AddMilliseconds( -1 ) ) );
			Assert.IsTrue( timeInterval.HasInside( end ) );
			Assert.IsFalse( timeInterval.HasInside( end.AddMilliseconds( 1 ) ) );
		} // HasInsideDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsidePeriodTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.AreEqual( timeInterval.End, end );

			// before
			TimeInterval before1 = new TimeInterval( start.AddHours( -2 ), start.AddHours( -1 ) );
			Assert.IsFalse( timeInterval.HasInside( before1 ) );
			TimeInterval before2 = new TimeInterval( start.AddMilliseconds( -1 ), end );
			Assert.IsFalse( timeInterval.HasInside( before2 ) );
			TimeInterval before3 = new TimeInterval( start.AddMilliseconds( -1 ), start );
			Assert.IsFalse( timeInterval.HasInside( before3 ) );

			// after
			TimeInterval after1 = new TimeInterval( end.AddHours( 1 ), end.AddHours( 2 ) );
			Assert.IsFalse( timeInterval.HasInside( after1 ) );
			TimeInterval after2 = new TimeInterval( start, end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeInterval.HasInside( after2 ) );
			TimeInterval after3 = new TimeInterval( end, end.AddMilliseconds( 1 ) );
			Assert.IsFalse( timeInterval.HasInside( after3 ) );

			// inside
			Assert.IsTrue( timeInterval.HasInside( timeInterval ) );
			TimeInterval inside1 = new TimeInterval( start.AddMilliseconds( 1 ), end );
			Assert.IsTrue( timeInterval.HasInside( inside1 ) );
			TimeInterval inside2 = new TimeInterval( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) );
			Assert.IsTrue( timeInterval.HasInside( inside2 ) );
			TimeInterval inside3 = new TimeInterval( start, end.AddMilliseconds( -1 ) );
			Assert.IsTrue( timeInterval.HasInside( inside3 ) );
		} // HasInsidePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyTest()
		{
			TimeInterval readOnlyTimeInterval = new TimeInterval( start, end );
			Assert.AreEqual( readOnlyTimeInterval.Copy( TimeSpan.Zero ), readOnlyTimeInterval );

			TimeInterval timeInterval = new TimeInterval( start, end );
			Assert.AreEqual( timeInterval.Start, start );
			Assert.AreEqual( timeInterval.End, end );

			ITimeInterval noMoveTimeInterval = timeInterval.Copy( TimeSpan.Zero );
			Assert.AreEqual( noMoveTimeInterval.Start, start );
			Assert.AreEqual( noMoveTimeInterval.End, end );
			Assert.AreEqual( noMoveTimeInterval.Duration, duration );

			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			ITimeInterval forwardTimeInterval = timeInterval.Copy( forwardOffset );
			Assert.AreEqual( forwardTimeInterval.Start, start.Add( forwardOffset ) );
			Assert.AreEqual( forwardTimeInterval.End, end.Add( forwardOffset ) );

			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			ITimeInterval backwardTimeInterval = timeInterval.Copy( backwardOffset );
			Assert.AreEqual( backwardTimeInterval.Start, start.Add( backwardOffset ) );
			Assert.AreEqual( backwardTimeInterval.End, end.Add( backwardOffset ) );
		} // CopyTest

		// ----------------------------------------------------------------------
		[Test]
		public void MoveTest()
		{
			TimeInterval timeIntervalMoveZero = new TimeInterval( start, end );
			timeIntervalMoveZero.Move( TimeSpan.Zero );
			Assert.AreEqual( timeIntervalMoveZero.Start, start );
			Assert.AreEqual( timeIntervalMoveZero.End, end );
			Assert.AreEqual( timeIntervalMoveZero.Duration, duration );

			TimeInterval timeIntervalMoveForward = new TimeInterval( start, end );
			TimeSpan forwardOffset = new TimeSpan( 2, 30, 15 );
			timeIntervalMoveForward.Move( forwardOffset );
			Assert.AreEqual( timeIntervalMoveForward.Start, start.Add( forwardOffset ) );
			Assert.AreEqual( timeIntervalMoveForward.End, end.Add( forwardOffset ) );

			TimeInterval timeIntervalMoveBackward = new TimeInterval( start, end );
			TimeSpan backwardOffset = new TimeSpan( -1, 10, 30 );
			timeIntervalMoveBackward.Move( backwardOffset );
			Assert.AreEqual( timeIntervalMoveBackward.Start, start.Add( backwardOffset ) );
			Assert.AreEqual( timeIntervalMoveBackward.End, end.Add( backwardOffset ) );
		} // MoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandStartToTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			timeInterval.ExpandStartTo( start.AddMilliseconds( 1 ) );
			Assert.AreEqual( timeInterval.Start, start );
			timeInterval.ExpandStartTo( start.AddMinutes( -1 ) );
			Assert.AreEqual( timeInterval.Start, start.AddMinutes( -1 ) );
		} // ExpandStartToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandEndToTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			timeInterval.ExpandEndTo( end.AddMilliseconds( -1 ) );
			Assert.AreEqual( timeInterval.End, end );
			timeInterval.ExpandEndTo( end.AddMinutes( 1 ) );
			Assert.AreEqual( timeInterval.End, end.AddMinutes( 1 ) );
		} // ExpandEndToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandToDateTimeTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );

			// start
			timeInterval.ExpandTo( start.AddMilliseconds( 1 ) );
			Assert.AreEqual( timeInterval.Start, start );
			timeInterval.ExpandTo( start.AddMinutes( -1 ) );
			Assert.AreEqual( timeInterval.Start, start.AddMinutes( -1 ) );

			// end
			timeInterval.ExpandTo( end.AddMilliseconds( -1 ) );
			Assert.AreEqual( timeInterval.End, end );
			timeInterval.ExpandTo( end.AddMinutes( 1 ) );
			Assert.AreEqual( timeInterval.End, end.AddMinutes( 1 ) );
		} // ExpandToDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExpandToPeriodTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );

			// no expansion
			timeInterval.ExpandTo( new TimeInterval( start.AddMilliseconds( 1 ), end.AddMilliseconds( -1 ) ) );
			Assert.AreEqual( timeInterval.Start, start );
			Assert.AreEqual( timeInterval.End, end );

			// start
			DateTime changedStart = start.AddMinutes( -1 );
			timeInterval.ExpandTo( new TimeInterval( changedStart, end ) );
			Assert.AreEqual( timeInterval.Start, changedStart );
			Assert.AreEqual( timeInterval.End, end );

			// end
			DateTime changedEnd = end.AddMinutes( 1 );
			timeInterval.ExpandTo( new TimeInterval( changedStart, changedEnd ) );
			Assert.AreEqual( timeInterval.Start, changedStart );
			Assert.AreEqual( timeInterval.End, changedEnd );

			// start/end
			changedStart = changedStart.AddMinutes( -1 );
			changedEnd = changedEnd.AddMinutes( 1 );
			timeInterval.ExpandTo( new TimeInterval( changedStart, changedEnd ) );
			Assert.AreEqual( timeInterval.Start, changedStart );
			Assert.AreEqual( timeInterval.End, changedEnd );
		} // ExpandToPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void ShrinkStartToTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			timeInterval.ShrinkStartTo( start.AddMilliseconds( -1 ) );
			Assert.AreEqual( timeInterval.Start, start );
			timeInterval.ShrinkStartTo( start.AddMinutes( 1 ) );
			Assert.AreEqual( timeInterval.Start, start.AddMinutes( 1 ) );
		} // ShrinkStartToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ShrinkEndToTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );
			timeInterval.ShrinkEndTo( end.AddMilliseconds( 1 ) );
			Assert.AreEqual( timeInterval.End, end );
			timeInterval.ShrinkEndTo( end.AddMinutes( -1 ) );
			Assert.AreEqual( timeInterval.End, end.AddMinutes( -1 ) );
		} // ShrinkEndToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ShrinkToTest()
		{
			TimeInterval timeInterval = new TimeInterval( start, end );

			// no shrink
			timeInterval.ShrinkTo( new TimeInterval( start.AddMilliseconds( -1 ), end.AddMilliseconds( 1 ) ) );
			Assert.AreEqual( timeInterval.Start, start );
			Assert.AreEqual( timeInterval.End, end );

			// start
			DateTime changedStart = start.AddMinutes( 1 );
			timeInterval.ShrinkTo( new TimeInterval( changedStart, end ) );
			Assert.AreEqual( timeInterval.Start, changedStart );
			Assert.AreEqual( timeInterval.End, end );

			// end
			DateTime changedEnd = end.AddMinutes( -1 );
			timeInterval.ShrinkTo( new TimeInterval( changedStart, changedEnd ) );
			Assert.AreEqual( timeInterval.Start, changedStart );
			Assert.AreEqual( timeInterval.End, changedEnd );

			// start/end
			changedStart = changedStart.AddMinutes( 1 );
			changedEnd = changedEnd.AddMinutes( -1 );
			timeInterval.ShrinkTo( new TimeInterval( changedStart, changedEnd ) );
			Assert.AreEqual( timeInterval.Start, changedStart );
			Assert.AreEqual( timeInterval.End, changedEnd );
		} // ShrinkToTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSamePeriodTest()
		{
			TimeInterval timeInterval1 = new TimeInterval( start, end );
			TimeInterval timeInterval2 = new TimeInterval( start, end );

			Assert.IsTrue( timeInterval1.IsSamePeriod( timeInterval1 ) );
			Assert.IsTrue( timeInterval2.IsSamePeriod( timeInterval2 ) );

			Assert.IsTrue( timeInterval1.IsSamePeriod( timeInterval2 ) );
			Assert.IsTrue( timeInterval2.IsSamePeriod( timeInterval1 ) );

			Assert.IsFalse( timeInterval1.IsSamePeriod( TimeInterval.Anytime ) );
			Assert.IsFalse( timeInterval2.IsSamePeriod( TimeInterval.Anytime ) );

			timeInterval1.Move( new TimeSpan( 1 ) );
			Assert.IsFalse( timeInterval1.IsSamePeriod( timeInterval2 ) );
			Assert.IsFalse( timeInterval2.IsSamePeriod( timeInterval1 ) );

			timeInterval1.Move( new TimeSpan( -1 ) );
			Assert.IsTrue( timeInterval1.IsSamePeriod( timeInterval2 ) );
			Assert.IsTrue( timeInterval2.IsSamePeriod( timeInterval1 ) );
		} // IsSamePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void TouchingIntervalTest()
		{
			TimeInterval timeInterval1 = new TimeInterval( start, end );
			TimeInterval timeInterval2 = new TimeInterval( end, end.AddHours( 1 ) );

			Assert.AreNotEqual( timeInterval1.GetIntersection( timeInterval2 ), null );
			Assert.AreEqual( timeInterval1.IntersectsWith( timeInterval2 ), true );
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.EndTouching );
			Assert.AreEqual( timeInterval2.GetRelation( timeInterval1 ), PeriodRelation.StartTouching );

			timeInterval1.EndEdge = IntervalEdge.Open;
			Assert.AreEqual( timeInterval1.GetIntersection( timeInterval2 ), null );
			Assert.AreEqual( timeInterval1.IntersectsWith( timeInterval2 ), false );
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.Before );
			Assert.AreEqual( timeInterval2.GetRelation( timeInterval1 ), PeriodRelation.After );

			timeInterval1.EndEdge = IntervalEdge.Closed;
			Assert.AreNotEqual( timeInterval1.GetIntersection( timeInterval2 ), null );
			Assert.AreEqual( timeInterval1.IntersectsWith( timeInterval2 ), true );
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.EndTouching );
			Assert.AreEqual( timeInterval2.GetRelation( timeInterval1 ), PeriodRelation.StartTouching );

			timeInterval2.StartEdge = IntervalEdge.Open;
			Assert.AreEqual( timeInterval1.GetIntersection( timeInterval2 ), null );
			Assert.AreEqual( timeInterval1.IntersectsWith( timeInterval2 ), false );
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.Before );
			Assert.AreEqual( timeInterval2.GetRelation( timeInterval1 ), PeriodRelation.After );

			timeInterval2.StartEdge = IntervalEdge.Closed;
			Assert.AreNotEqual( timeInterval1.GetIntersection( timeInterval2 ), null );
			Assert.AreEqual( timeInterval1.IntersectsWith( timeInterval2 ), true );
			Assert.AreEqual( timeInterval1.GetRelation( timeInterval2 ), PeriodRelation.EndTouching );
			Assert.AreEqual( timeInterval2.GetRelation( timeInterval1 ), PeriodRelation.StartTouching );
		} // TouchingIntervalTest

		// ----------------------------------------------------------------------
		// members
		private readonly TimeSpan duration = Duration.Hour;
		private readonly DateTime start;
		private readonly DateTime end;

	} // class TimeIntervalTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
