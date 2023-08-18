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
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimeIntervalTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        public TimeIntervalTest()
        {
            start = ClockProxy.Clock.Now;
            end = start.Add(duration);
        } // TimeIntervalTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void AnytimeTest()
        {
            Assert.Equal(TimeInterval.Anytime.Start, TimeSpec.MinPeriodDate);
            Assert.Equal(TimeInterval.Anytime.End, TimeSpec.MaxPeriodDate);
            Assert.True(TimeInterval.Anytime.IsAnytime);
            Assert.True(TimeInterval.Anytime.IsReadOnly);
            Assert.True(TimeInterval.Anytime.IsClosed);
            Assert.False(TimeInterval.Anytime.IsOpen);
            Assert.False(TimeInterval.Anytime.HasStart);
            Assert.False(TimeInterval.Anytime.HasEnd);
        } // AnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void DefaultTest()
        {
            var timeInterval = new TimeInterval();
            Assert.NotEqual(timeInterval, TimeInterval.Anytime);
            Assert.Equal(PeriodRelation.ExactMatch, timeInterval.GetRelation(TimeInterval.Anytime));
            Assert.True(timeInterval.IsAnytime);
            Assert.True(timeInterval.IsClosed);
            Assert.False(timeInterval.IsOpen);
            Assert.False(timeInterval.IsMoment);
            Assert.False(timeInterval.IsReadOnly);
        } // DefaultTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void MomentTest()
        {
            var moment = ClockProxy.Clock.Now;
            var timeInterval = new TimeInterval(moment);
            Assert.Equal(timeInterval.Start, moment);
            Assert.Equal(timeInterval.End, moment);
            Assert.True(timeInterval.IsMoment);
            Assert.True(timeInterval.IsDegenerate);
        } // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void NonMomentTest()
        {
            var moment = ClockProxy.Clock.Now;
            var timeInterval = new TimeInterval(moment, moment.AddMilliseconds(1));
            Assert.False(timeInterval.IsMoment);
            Assert.False(timeInterval.IsDegenerate);
        } // NonMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void HasStartTest()
        {
            var timeInterval1 = new TimeInterval(ClockProxy.Clock.Now, TimeSpec.MaxPeriodDate);
            Assert.True(timeInterval1.HasStart);
            Assert.False(timeInterval1.HasEnd);

            var timeInterval2 = new TimeInterval(TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate);
            Assert.False(timeInterval2.HasStart);

            var timeInterval3 = new TimeInterval(
                TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, IntervalEdge.Open, IntervalEdge.Open);
            Assert.True(timeInterval3.HasStart);
        } // HasStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void HasEndTest()
        {
            var timeInterval1 = new TimeInterval(TimeSpec.MinPeriodDate, ClockProxy.Clock.Now);
            Assert.False(timeInterval1.HasStart);
            Assert.True(timeInterval1.HasEnd);

            var timeInterval2 = new TimeInterval(TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate);
            Assert.False(timeInterval2.HasEnd);

            var timeInterval3 = new TimeInterval(
                TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, IntervalEdge.Open, IntervalEdge.Open);
            Assert.True(timeInterval3.HasEnd);
        } // HasEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void StartEndIncludeTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.Equal(timeInterval.Start, start);
            Assert.Equal(timeInterval.StartInterval, start);
            Assert.Equal(timeInterval.End, end);
            Assert.Equal(timeInterval.EndInterval, end);
            Assert.Equal(timeInterval.Duration, duration);
            Assert.False(timeInterval.IsAnytime);
            Assert.False(timeInterval.IsMoment);
            Assert.False(timeInterval.IsReadOnly);
        } // StartEndIncludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void StartEndExcludeTest()
        {
            var timeInterval = new TimeInterval(start, end, IntervalEdge.Open, IntervalEdge.Open);
            Assert.NotEqual(timeInterval.Start, start);
            Assert.Equal(timeInterval.StartInterval, start);
            Assert.NotEqual(timeInterval.End, end);
            Assert.Equal(timeInterval.EndInterval, end);
            Assert.Equal(timeInterval.Duration, duration);
            Assert.False(timeInterval.IsAnytime);
            Assert.False(timeInterval.IsMoment);
            Assert.False(timeInterval.IsReadOnly);
        } // StartEndExcludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void StartEndSwapTest()
        {
            var timeInterval = new TimeInterval(end, start);
            Assert.Equal(timeInterval.Start, start);
            Assert.Equal(timeInterval.StartInterval, start);
            Assert.Equal(timeInterval.Duration, duration);
            Assert.Equal(timeInterval.End, end);
            Assert.Equal(timeInterval.EndInterval, end);
        } // StartEndSwapTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void CopyConstructorTest()
        {
            var source = new TimeInterval(start, start.AddHours(1), IntervalEdge.Closed, IntervalEdge.Open, false, true);
            var copy = new TimeInterval(source);
            Assert.Equal(source.Start, copy.Start);
            Assert.Equal(source.StartInterval, copy.StartInterval);
            Assert.Equal(source.StartEdge, copy.StartEdge);
            Assert.Equal(source.End, copy.End);
            Assert.Equal(source.EndInterval, copy.EndInterval);
            Assert.Equal(source.EndEdge, copy.EndEdge);
            Assert.Equal(source.IsIntervalEnabled, copy.IsIntervalEnabled);
            Assert.Equal(source.IsReadOnly, copy.IsReadOnly);
            Assert.Equal(source, copy);
        } // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void StartIntervalIncludeTest()
        {
            var timeInterval = new TimeInterval(start, start.AddHours(1));
            Assert.Equal(timeInterval.Start, start);
            var changedStart = start.AddHours(-1);
            timeInterval.StartInterval = changedStart;
            Assert.Equal(timeInterval.StartInterval, changedStart);
        } // StartIntervalIncludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void StartIntervalExcludeTest()
        {
            var now = DateTime.Now;
            var startHour = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            var timeInterval = new TimeInterval(startHour, startHour.AddHours(1),
                IntervalEdge.Open, IntervalEdge.Open);

            Assert.Equal(IntervalEdge.Open, timeInterval.StartEdge);
            Assert.NotEqual(timeInterval.Start, timeInterval.StartInterval);
        } // StartIntervalExcludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void StartIntervalReadOnlyTest()
        {
            var timeInterval = new TimeInterval(ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours(1),
                IntervalEdge.Closed, IntervalEdge.Closed, true, true);
            Assert.NotNull(Assert.Throws<NotSupportedException>(() =>
               timeInterval.StartInterval = timeInterval.Start.AddHours(-1)));
        } // StartIntervalReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void StartOutOfRangeTest()
        {
            var timeInterval = new TimeInterval(ClockProxy.Clock.Now, ClockProxy.Clock.Now.AddHours(1));
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                timeInterval.StartInterval = timeInterval.Start.AddHours(2)));
        } // StartOutOfRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void EndIntervalIncludeTest()
        {
            var timeInterval = new TimeInterval(end.AddHours(-1), end);
            Assert.Equal(timeInterval.End, end);
            var changedEnd = end.AddHours(1);
            timeInterval.EndInterval = changedEnd;
            Assert.Equal(timeInterval.EndInterval, changedEnd);
        } // IntervalIncludeEnd

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void EndIntervalExcludeTest()
        {
            var now = DateTime.Now;
            var startHour = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            var timeInterval = new TimeInterval(startHour, startHour.AddHours(1),
                IntervalEdge.Open, IntervalEdge.Open);

            Assert.Equal(IntervalEdge.Open, timeInterval.EndEdge);
            Assert.NotEqual(timeInterval.End, timeInterval.EndInterval);
        } // EndIntervalExcludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void EndIntervalReadOnlyTest()
        {
            var timeInterval = new TimeInterval(ClockProxy.Clock.Now.AddHours(-1), ClockProxy.Clock.Now,
                IntervalEdge.Closed, IntervalEdge.Closed, true, true);
            Assert.NotNull(Assert.Throws<NotSupportedException>(() =>
                timeInterval.EndInterval = timeInterval.End.AddHours(1)));
        } // EndIntervalReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void EndIntervalOutOfRangeTest()
        {
            var timeInterval = new TimeInterval(ClockProxy.Clock.Now.AddHours(-1), ClockProxy.Clock.Now);
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                timeInterval.EndInterval = timeInterval.End.AddHours(-2)));
        } // EndIntervalOutOfRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsStartOpenTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.False(timeInterval.IsStartOpen);
            timeInterval.StartEdge = IntervalEdge.Open;
            Assert.True(timeInterval.IsStartOpen);
            timeInterval.StartEdge = IntervalEdge.Closed;
            Assert.False(timeInterval.IsStartOpen);
        } // IsStartOpenTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsEndOpenTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.False(timeInterval.IsEndOpen);
            timeInterval.EndEdge = IntervalEdge.Open;
            Assert.True(timeInterval.IsEndOpen);
            timeInterval.EndEdge = IntervalEdge.Closed;
            Assert.False(timeInterval.IsEndOpen);
        } // IsEndOpenTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsOpenTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.False(timeInterval.IsOpen);
            timeInterval.StartEdge = IntervalEdge.Open;
            Assert.False(timeInterval.IsOpen);
            timeInterval.EndEdge = IntervalEdge.Open;
            Assert.True(timeInterval.IsOpen);
            timeInterval.StartEdge = IntervalEdge.Closed;
            Assert.False(timeInterval.IsOpen);
            timeInterval.EndEdge = IntervalEdge.Closed;
            Assert.False(timeInterval.IsOpen);
        } // IsOpenTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsStartClosedTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.True(timeInterval.IsStartClosed);
            timeInterval.StartEdge = IntervalEdge.Open;
            Assert.False(timeInterval.IsStartClosed);
            timeInterval.StartEdge = IntervalEdge.Closed;
            Assert.True(timeInterval.IsStartClosed);
        } // IsStartClosedTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsEndClosedTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.True(timeInterval.IsEndClosed);
            timeInterval.EndEdge = IntervalEdge.Open;
            Assert.False(timeInterval.IsEndClosed);
            timeInterval.EndEdge = IntervalEdge.Closed;
            Assert.True(timeInterval.IsEndClosed);
        } // IsEndClosedTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsClosedTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.True(timeInterval.IsClosed);
            timeInterval.StartEdge = IntervalEdge.Open;
            Assert.False(timeInterval.IsClosed);
            timeInterval.EndEdge = IntervalEdge.Open;
            Assert.False(timeInterval.IsClosed);
            timeInterval.StartEdge = IntervalEdge.Closed;
            Assert.False(timeInterval.IsClosed);
            timeInterval.EndEdge = IntervalEdge.Closed;
            Assert.True(timeInterval.IsClosed);
        } // IsClosedTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsEmptyTest()
        {
            var timeInterval = new TimeInterval(start);
            Assert.False(timeInterval.IsEmpty);
            timeInterval.StartEdge = IntervalEdge.Open;
            Assert.True(timeInterval.IsEmpty);
            timeInterval.EndEdge = IntervalEdge.Open;
            Assert.True(timeInterval.IsEmpty);
            timeInterval.StartEdge = IntervalEdge.Closed;
            Assert.True(timeInterval.IsEmpty);
            timeInterval.EndEdge = IntervalEdge.Closed;
            Assert.False(timeInterval.IsEmpty);
        } // IsEmptyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsDegenerateTest()
        {
            var timeInterval = new TimeInterval(start);
            Assert.True(timeInterval.IsDegenerate);
            timeInterval.StartEdge = IntervalEdge.Open;
            Assert.False(timeInterval.IsDegenerate);
            timeInterval.EndEdge = IntervalEdge.Open;
            Assert.False(timeInterval.IsDegenerate);
            timeInterval.StartEdge = IntervalEdge.Closed;
            Assert.False(timeInterval.IsDegenerate);
            timeInterval.EndEdge = IntervalEdge.Closed;
            Assert.True(timeInterval.IsDegenerate);
        } // IsDegenerateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsIntervalEnabledTest()
        {
            var timeInterval1 = new TimeInterval(start, end);
            var timeInterval2 = new TimeInterval(end, end.AddHours(1));

            Assert.Equal(PeriodRelation.EndTouching, timeInterval1.GetRelation(timeInterval2));

            timeInterval1.EndEdge = IntervalEdge.Open;
            Assert.Equal(PeriodRelation.Before, timeInterval1.GetRelation(timeInterval2));

            timeInterval1.IsIntervalEnabled = false;
            Assert.Equal(PeriodRelation.EndTouching, timeInterval1.GetRelation(timeInterval2));

            timeInterval1.IsIntervalEnabled = true;
            Assert.Equal(PeriodRelation.Before, timeInterval1.GetRelation(timeInterval2));

            timeInterval1.EndEdge = IntervalEdge.Closed;
            Assert.Equal(PeriodRelation.EndTouching, timeInterval1.GetRelation(timeInterval2));
        } // IsIntervalEnabledTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void SetupTest()
        {
            var timeInterval1 = new TimeInterval();
            timeInterval1.Setup(TimeSpec.MinPeriodDate, TimeSpec.MinPeriodDate);
            Assert.Equal(timeInterval1.Start, TimeSpec.MinPeriodDate);
            Assert.Equal(timeInterval1.End, TimeSpec.MinPeriodDate);

            var timeInterval2 = new TimeInterval();
            timeInterval2.Setup(TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate);
            Assert.Equal(timeInterval2.Start, TimeSpec.MinPeriodDate);
            Assert.Equal(timeInterval2.End, TimeSpec.MaxPeriodDate);

            var timeInterval3 = new TimeInterval();
            timeInterval3.Setup(TimeSpec.MaxPeriodDate, TimeSpec.MinPeriodDate);
            Assert.Equal(timeInterval3.Start, TimeSpec.MinPeriodDate);
            Assert.Equal(timeInterval3.End, TimeSpec.MaxPeriodDate);
        } // SetupTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void HasInsideDateTimeTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.Equal(timeInterval.End, end);

            // start
            Assert.False(timeInterval.HasInside(start.AddMilliseconds(-1)));
            Assert.True(timeInterval.HasInside(start));
            Assert.True(timeInterval.HasInside(start.AddMilliseconds(1)));

            // end
            Assert.True(timeInterval.HasInside(end.AddMilliseconds(-1)));
            Assert.True(timeInterval.HasInside(end));
            Assert.False(timeInterval.HasInside(end.AddMilliseconds(1)));
        } // HasInsideDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void HasInsidePeriodTest()
        {
            var timeInterval = new TimeInterval(start, end);
            Assert.Equal(timeInterval.End, end);

            // before
            var before1 = new TimeInterval(start.AddHours(-2), start.AddHours(-1));
            Assert.False(timeInterval.HasInside(before1));
            var before2 = new TimeInterval(start.AddMilliseconds(-1), end);
            Assert.False(timeInterval.HasInside(before2));
            var before3 = new TimeInterval(start.AddMilliseconds(-1), start);
            Assert.False(timeInterval.HasInside(before3));

            // after
            var after1 = new TimeInterval(end.AddHours(1), end.AddHours(2));
            Assert.False(timeInterval.HasInside(after1));
            var after2 = new TimeInterval(start, end.AddMilliseconds(1));
            Assert.False(timeInterval.HasInside(after2));
            var after3 = new TimeInterval(end, end.AddMilliseconds(1));
            Assert.False(timeInterval.HasInside(after3));

            // inside
            Assert.True(timeInterval.HasInside(timeInterval));
            var inside1 = new TimeInterval(start.AddMilliseconds(1), end);
            Assert.True(timeInterval.HasInside(inside1));
            var inside2 = new TimeInterval(start.AddMilliseconds(1), end.AddMilliseconds(-1));
            Assert.True(timeInterval.HasInside(inside2));
            var inside3 = new TimeInterval(start, end.AddMilliseconds(-1));
            Assert.True(timeInterval.HasInside(inside3));
        } // HasInsidePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void CopyTest()
        {
            var readOnlyTimeInterval = new TimeInterval(start, end);
            Assert.Equal(readOnlyTimeInterval.Copy(TimeSpan.Zero), readOnlyTimeInterval);

            var timeInterval = new TimeInterval(start, end);
            Assert.Equal(timeInterval.Start, start);
            Assert.Equal(timeInterval.End, end);

            var noMoveTimeInterval = timeInterval.Copy(TimeSpan.Zero);
            Assert.Equal(noMoveTimeInterval.Start, start);
            Assert.Equal(noMoveTimeInterval.End, end);
            Assert.Equal(noMoveTimeInterval.Duration, duration);

            var forwardOffset = new TimeSpan(2, 30, 15);
            var forwardTimeInterval = timeInterval.Copy(forwardOffset);
            Assert.Equal(forwardTimeInterval.Start, start.Add(forwardOffset));
            Assert.Equal(forwardTimeInterval.End, end.Add(forwardOffset));

            var backwardOffset = new TimeSpan(-1, 10, 30);
            var backwardTimeInterval = timeInterval.Copy(backwardOffset);
            Assert.Equal(backwardTimeInterval.Start, start.Add(backwardOffset));
            Assert.Equal(backwardTimeInterval.End, end.Add(backwardOffset));
        } // CopyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void MoveTest()
        {
            var timeIntervalMoveZero = new TimeInterval(start, end);
            timeIntervalMoveZero.Move(TimeSpan.Zero);
            Assert.Equal(timeIntervalMoveZero.Start, start);
            Assert.Equal(timeIntervalMoveZero.End, end);
            Assert.Equal(timeIntervalMoveZero.Duration, duration);

            var timeIntervalMoveForward = new TimeInterval(start, end);
            var forwardOffset = new TimeSpan(2, 30, 15);
            timeIntervalMoveForward.Move(forwardOffset);
            Assert.Equal(timeIntervalMoveForward.Start, start.Add(forwardOffset));
            Assert.Equal(timeIntervalMoveForward.End, end.Add(forwardOffset));

            var timeIntervalMoveBackward = new TimeInterval(start, end);
            var backwardOffset = new TimeSpan(-1, 10, 30);
            timeIntervalMoveBackward.Move(backwardOffset);
            Assert.Equal(timeIntervalMoveBackward.Start, start.Add(backwardOffset));
            Assert.Equal(timeIntervalMoveBackward.End, end.Add(backwardOffset));
        } // MoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void ExpandStartToTest()
        {
            var timeInterval = new TimeInterval(start, end);
            timeInterval.ExpandStartTo(start.AddMilliseconds(1));
            Assert.Equal(timeInterval.Start, start);
            timeInterval.ExpandStartTo(start.AddMinutes(-1));
            Assert.Equal(timeInterval.Start, start.AddMinutes(-1));
        } // ExpandStartToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void ExpandEndToTest()
        {
            var timeInterval = new TimeInterval(start, end);
            timeInterval.ExpandEndTo(end.AddMilliseconds(-1));
            Assert.Equal(timeInterval.End, end);
            timeInterval.ExpandEndTo(end.AddMinutes(1));
            Assert.Equal(timeInterval.End, end.AddMinutes(1));
        } // ExpandEndToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void ExpandToDateTimeTest()
        {
            var timeInterval = new TimeInterval(start, end);

            // start
            timeInterval.ExpandTo(start.AddMilliseconds(1));
            Assert.Equal(timeInterval.Start, start);
            timeInterval.ExpandTo(start.AddMinutes(-1));
            Assert.Equal(timeInterval.Start, start.AddMinutes(-1));

            // end
            timeInterval.ExpandTo(end.AddMilliseconds(-1));
            Assert.Equal(timeInterval.End, end);
            timeInterval.ExpandTo(end.AddMinutes(1));
            Assert.Equal(timeInterval.End, end.AddMinutes(1));
        } // ExpandToDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void ExpandToPeriodTest()
        {
            var timeInterval = new TimeInterval(start, end);

            // no expansion
            timeInterval.ExpandTo(new TimeInterval(start.AddMilliseconds(1), end.AddMilliseconds(-1)));
            Assert.Equal(timeInterval.Start, start);
            Assert.Equal(timeInterval.End, end);

            // start
            var changedStart = start.AddMinutes(-1);
            timeInterval.ExpandTo(new TimeInterval(changedStart, end));
            Assert.Equal(timeInterval.Start, changedStart);
            Assert.Equal(timeInterval.End, end);

            // end
            var changedEnd = end.AddMinutes(1);
            timeInterval.ExpandTo(new TimeInterval(changedStart, changedEnd));
            Assert.Equal(timeInterval.Start, changedStart);
            Assert.Equal(timeInterval.End, changedEnd);

            // start/end
            changedStart = changedStart.AddMinutes(-1);
            changedEnd = changedEnd.AddMinutes(1);
            timeInterval.ExpandTo(new TimeInterval(changedStart, changedEnd));
            Assert.Equal(timeInterval.Start, changedStart);
            Assert.Equal(timeInterval.End, changedEnd);
        } // ExpandToPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void ShrinkStartToTest()
        {
            var timeInterval = new TimeInterval(start, end);
            timeInterval.ShrinkStartTo(start.AddMilliseconds(-1));
            Assert.Equal(timeInterval.Start, start);
            timeInterval.ShrinkStartTo(start.AddMinutes(1));
            Assert.Equal(timeInterval.Start, start.AddMinutes(1));
        } // ShrinkStartToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void ShrinkEndToTest()
        {
            var timeInterval = new TimeInterval(start, end);
            timeInterval.ShrinkEndTo(end.AddMilliseconds(1));
            Assert.Equal(timeInterval.End, end);
            timeInterval.ShrinkEndTo(end.AddMinutes(-1));
            Assert.Equal(timeInterval.End, end.AddMinutes(-1));
        } // ShrinkEndToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void ShrinkToTest()
        {
            var timeInterval = new TimeInterval(start, end);

            // no shrink
            timeInterval.ShrinkTo(new TimeInterval(start.AddMilliseconds(-1), end.AddMilliseconds(1)));
            Assert.Equal(timeInterval.Start, start);
            Assert.Equal(timeInterval.End, end);

            // start
            var changedStart = start.AddMinutes(1);
            timeInterval.ShrinkTo(new TimeInterval(changedStart, end));
            Assert.Equal(timeInterval.Start, changedStart);
            Assert.Equal(timeInterval.End, end);

            // end
            var changedEnd = end.AddMinutes(-1);
            timeInterval.ShrinkTo(new TimeInterval(changedStart, changedEnd));
            Assert.Equal(timeInterval.Start, changedStart);
            Assert.Equal(timeInterval.End, changedEnd);

            // start/end
            changedStart = changedStart.AddMinutes(1);
            changedEnd = changedEnd.AddMinutes(-1);
            timeInterval.ShrinkTo(new TimeInterval(changedStart, changedEnd));
            Assert.Equal(timeInterval.Start, changedStart);
            Assert.Equal(timeInterval.End, changedEnd);
        } // ShrinkToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void IsSamePeriodTest()
        {
            var timeInterval1 = new TimeInterval(start, end);
            var timeInterval2 = new TimeInterval(start, end);

            Assert.True(timeInterval1.IsSamePeriod(timeInterval1));
            Assert.True(timeInterval2.IsSamePeriod(timeInterval2));

            Assert.True(timeInterval1.IsSamePeriod(timeInterval2));
            Assert.True(timeInterval2.IsSamePeriod(timeInterval1));

            Assert.False(timeInterval1.IsSamePeriod(TimeInterval.Anytime));
            Assert.False(timeInterval2.IsSamePeriod(TimeInterval.Anytime));

            timeInterval1.Move(new TimeSpan(1));
            Assert.False(timeInterval1.IsSamePeriod(timeInterval2));
            Assert.False(timeInterval2.IsSamePeriod(timeInterval1));

            timeInterval1.Move(new TimeSpan(-1));
            Assert.True(timeInterval1.IsSamePeriod(timeInterval2));
            Assert.True(timeInterval2.IsSamePeriod(timeInterval1));
        } // IsSamePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeInterval")]
        [Fact]
        public void TouchingIntervalTest()
        {
            var timeInterval1 = new TimeInterval(start, end);
            var timeInterval2 = new TimeInterval(end, end.AddHours(1));

            Assert.NotNull(timeInterval1.GetIntersection(timeInterval2));
            Assert.True(timeInterval1.IntersectsWith(timeInterval2));
            Assert.Equal(PeriodRelation.EndTouching, timeInterval1.GetRelation(timeInterval2));
            Assert.Equal(PeriodRelation.StartTouching, timeInterval2.GetRelation(timeInterval1));

            timeInterval1.EndEdge = IntervalEdge.Open;
            Assert.Null(timeInterval1.GetIntersection(timeInterval2));
            Assert.False(timeInterval1.IntersectsWith(timeInterval2));
            Assert.Equal(PeriodRelation.Before, timeInterval1.GetRelation(timeInterval2));
            Assert.Equal(PeriodRelation.After, timeInterval2.GetRelation(timeInterval1));

            timeInterval1.EndEdge = IntervalEdge.Closed;
            Assert.NotNull(timeInterval1.GetIntersection(timeInterval2));
            Assert.True(timeInterval1.IntersectsWith(timeInterval2));
            Assert.Equal(PeriodRelation.EndTouching, timeInterval1.GetRelation(timeInterval2));
            Assert.Equal(PeriodRelation.StartTouching, timeInterval2.GetRelation(timeInterval1));

            timeInterval2.StartEdge = IntervalEdge.Open;
            Assert.Null(timeInterval1.GetIntersection(timeInterval2));
            Assert.False(timeInterval1.IntersectsWith(timeInterval2));
            Assert.Equal(PeriodRelation.Before, timeInterval1.GetRelation(timeInterval2));
            Assert.Equal(PeriodRelation.After, timeInterval2.GetRelation(timeInterval1));

            timeInterval2.StartEdge = IntervalEdge.Closed;
            Assert.NotNull(timeInterval1.GetIntersection(timeInterval2));
            Assert.True(timeInterval1.IntersectsWith(timeInterval2));
            Assert.Equal(PeriodRelation.EndTouching, timeInterval1.GetRelation(timeInterval2));
            Assert.Equal(PeriodRelation.StartTouching, timeInterval2.GetRelation(timeInterval1));
        } // TouchingIntervalTest

        // ----------------------------------------------------------------------
        // members
        private readonly TimeSpan duration = Duration.Hour;
        private readonly DateTime start;
        private readonly DateTime end;

    } // class TimeIntervalTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
