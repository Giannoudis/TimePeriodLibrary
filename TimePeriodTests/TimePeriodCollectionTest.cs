// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCollectionTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
#if (!PCL)
#endif
using Itenso.TimePeriod;
using Xunit;
using ListSortDirection = Itenso.TimePeriod.ListSortDirection;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimePeriodCollectionTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        public TimePeriodCollectionTest()
        {
            var testData = ClockProxy.Clock.Now;
            var endTestData = testData.Add(durationTesData);
            timeRangeTestData = new TimeRangePeriodRelationTestData(testData, endTestData, offsetTestData);
        } // TimePeriodCollectionTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void DefaultConstructorTest()
        {
            var timePeriods = new TimePeriodCollection();
            Assert.Empty(timePeriods);
            Assert.False(timePeriods.HasStart);
            Assert.False(timePeriods.HasEnd);
            Assert.False(timePeriods.IsReadOnly);
        } // DefaultConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void CopyConstructorTest()
        {
            var timePeriods = new TimePeriodCollection(timeRangeTestData.AllPeriods);
            Assert.Equal(timePeriods.Count, timeRangeTestData.AllPeriods.Count);
            Assert.True(timePeriods.HasStart);
            Assert.True(timePeriods.HasEnd);
            Assert.False(timePeriods.IsReadOnly);
        } // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void CountTest()
        {
            var timePeriods = new TimePeriodCollection();
            Assert.Empty(timePeriods);

            timePeriods.AddAll(timeRangeTestData.AllPeriods);
            Assert.Equal(timePeriods.Count, timeRangeTestData.AllPeriods.Count);

            timePeriods.Clear();
            Assert.Empty(timePeriods);
        } // CountTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void ItemIndexTest()
        {
            var timePeriods = new TimePeriodCollection();
            var schoolDay = new SchoolDay();
            timePeriods.AddAll(schoolDay);

            Assert.Equal(timePeriods[0], schoolDay.Lesson1);
            Assert.Equal(timePeriods[1], schoolDay.Break1);
            Assert.Equal(timePeriods[2], schoolDay.Lesson2);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson3);
            Assert.Equal(timePeriods[5], schoolDay.Break3);
            Assert.Equal(timePeriods[6], schoolDay.Lesson4);
        } // ItemIndexTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void IsAnytimeTest()
        {
            var now = ClockProxy.Clock.Now;
            var timePeriods = new TimePeriodCollection();
            Assert.True(timePeriods.IsAnytime);

            timePeriods.Add(TimeRange.Anytime);
            Assert.True(timePeriods.IsAnytime);

            timePeriods.Clear();
            Assert.True(timePeriods.IsAnytime);

            timePeriods.Add(new TimeRange(TimeSpec.MinPeriodDate, now));
            Assert.False(timePeriods.IsAnytime);

            timePeriods.Add(new TimeRange(now, TimeSpec.MaxPeriodDate));
            Assert.True(timePeriods.IsAnytime);

            timePeriods.Clear();
            Assert.True(timePeriods.IsAnytime);
        } // IsAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void IsMomentTest()
        {
            var now = ClockProxy.Clock.Now;
            var timePeriods = new TimePeriodCollection();
            Assert.False(timePeriods.IsMoment);

            timePeriods.Add(TimeRange.Anytime);
            Assert.False(timePeriods.IsMoment);

            timePeriods.Clear();
            Assert.False(timePeriods.IsMoment);

            timePeriods.Add(new TimeRange(now));
            Assert.True(timePeriods.IsMoment);

            timePeriods.Add(new TimeRange(now));
            Assert.True(timePeriods.IsMoment);

            timePeriods.Clear();
            Assert.True(timePeriods.IsAnytime);
        } // IsMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void HasStartTest()
        {
            var timePeriods = new TimePeriodCollection();
            Assert.False(timePeriods.HasStart);

            timePeriods.Add(new TimeBlock(TimeSpec.MinPeriodDate, Duration.Hour));
            Assert.False(timePeriods.HasStart);

            timePeriods.Clear();
            timePeriods.Add(new TimeBlock(ClockProxy.Clock.Now, Duration.Hour));
            Assert.True(timePeriods.HasStart);
        } // HasStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void StartTest()
        {
            var now = ClockProxy.Clock.Now;
            var timePeriods = new TimePeriodCollection();
            Assert.Equal(timePeriods.Start, TimeSpec.MinPeriodDate);

            timePeriods.Add(new TimeBlock(now, Duration.Hour));
            Assert.Equal(timePeriods.Start, now);

            timePeriods.Clear();
            Assert.Equal(timePeriods.Start, TimeSpec.MinPeriodDate);
        } // StartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void StartMoveTest()
        {
            var now = ClockProxy.Clock.Now;
            var timePeriods = new TimePeriodCollection(new SchoolDay(now))
            {
                Start = now.AddHours(0)
            };

            Assert.Equal(timePeriods.Start, now);
            timePeriods.Start = now.AddHours(1);
            Assert.Equal(timePeriods.Start, now.AddHours(1));
            timePeriods.Start = now.AddHours(-1);
            Assert.Equal(timePeriods.Start, now.AddHours(-1));
        } // StartMoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void SortByTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection
            {
                // start
                schoolDay.Lesson4,
                schoolDay.Break3,
                schoolDay.Lesson3,
                schoolDay.Break2,
                schoolDay.Lesson2,
                schoolDay.Break1,
                schoolDay.Lesson1
            };

            timePeriods.SortBy(TimePeriodStartComparer.Comparer);

            Assert.Equal(timePeriods[0], schoolDay.Lesson1);
            Assert.Equal(timePeriods[1], schoolDay.Break1);
            Assert.Equal(timePeriods[2], schoolDay.Lesson2);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson3);
            Assert.Equal(timePeriods[5], schoolDay.Break3);
            Assert.Equal(timePeriods[6], schoolDay.Lesson4);

            timePeriods.SortReverseBy(TimePeriodStartComparer.Comparer);

            Assert.Equal(timePeriods[0], schoolDay.Lesson4);
            Assert.Equal(timePeriods[1], schoolDay.Break3);
            Assert.Equal(timePeriods[2], schoolDay.Lesson3);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson2);
            Assert.Equal(timePeriods[5], schoolDay.Break1);
            Assert.Equal(timePeriods[6], schoolDay.Lesson1);

            // end
            timePeriods.Clear();
            timePeriods.AddAll(schoolDay);

            timePeriods.SortReverseBy(TimePeriodEndComparer.Comparer);

            Assert.Equal(timePeriods[0], schoolDay.Lesson4);
            Assert.Equal(timePeriods[1], schoolDay.Break3);
            Assert.Equal(timePeriods[2], schoolDay.Lesson3);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson2);
            Assert.Equal(timePeriods[5], schoolDay.Break1);
            Assert.Equal(timePeriods[6], schoolDay.Lesson1);

            timePeriods.SortBy(TimePeriodEndComparer.Comparer);

            Assert.Equal(timePeriods[0], schoolDay.Lesson1);
            Assert.Equal(timePeriods[1], schoolDay.Break1);
            Assert.Equal(timePeriods[2], schoolDay.Lesson2);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson3);
            Assert.Equal(timePeriods[5], schoolDay.Break3);
            Assert.Equal(timePeriods[6], schoolDay.Lesson4);

            // duration
            timePeriods.Clear();
            var oneHour = new TimeSpan(1, 0, 0);
            var twoHours = new TimeSpan(2, 0, 0);
            var threeHours = new TimeSpan(3, 0, 0);
            var fourHours = new TimeSpan(4, 0, 0);
            timePeriods.Add(new TimeRange(now, oneHour));
            timePeriods.Add(new TimeRange(now, twoHours));
            timePeriods.Add(new TimeRange(now, threeHours));
            timePeriods.Add(new TimeRange(now, fourHours));

            timePeriods.SortReverseBy(TimePeriodDurationComparer.Comparer);

            Assert.Equal(fourHours, timePeriods[0].Duration);
            Assert.Equal(threeHours, timePeriods[1].Duration);
            Assert.Equal(twoHours, timePeriods[2].Duration);
            Assert.Equal(oneHour, timePeriods[3].Duration);

            timePeriods.SortBy(TimePeriodDurationComparer.Comparer);

            Assert.Equal(oneHour, timePeriods[0].Duration);
            Assert.Equal(twoHours, timePeriods[1].Duration);
            Assert.Equal(threeHours, timePeriods[2].Duration);
            Assert.Equal(fourHours, timePeriods[3].Duration);
        } // SortByTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void SortByStartTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection
            {
                schoolDay.Lesson4,
                schoolDay.Break3,
                schoolDay.Lesson3,
                schoolDay.Break2,
                schoolDay.Lesson2,
                schoolDay.Break1,
                schoolDay.Lesson1
            };

            timePeriods.SortByStart(ListSortDirection.Descending);

            Assert.Equal(timePeriods[0], schoolDay.Lesson4);
            Assert.Equal(timePeriods[1], schoolDay.Break3);
            Assert.Equal(timePeriods[2], schoolDay.Lesson3);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson2);
            Assert.Equal(timePeriods[5], schoolDay.Break1);
            Assert.Equal(timePeriods[6], schoolDay.Lesson1);

            timePeriods.SortByStart();

            Assert.Equal(timePeriods[0], schoolDay.Lesson1);
            Assert.Equal(timePeriods[1], schoolDay.Break1);
            Assert.Equal(timePeriods[2], schoolDay.Lesson2);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson3);
            Assert.Equal(timePeriods[5], schoolDay.Break3);
            Assert.Equal(timePeriods[6], schoolDay.Lesson4);
        } // SortByStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void SortByEndTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection();

            timePeriods.AddAll(schoolDay);

            timePeriods.SortByEnd(ListSortDirection.Descending);

            Assert.Equal(timePeriods[0], schoolDay.Lesson4);
            Assert.Equal(timePeriods[1], schoolDay.Break3);
            Assert.Equal(timePeriods[2], schoolDay.Lesson3);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson2);
            Assert.Equal(timePeriods[5], schoolDay.Break1);
            Assert.Equal(timePeriods[6], schoolDay.Lesson1);

            timePeriods.SortByEnd();

            Assert.Equal(timePeriods[0], schoolDay.Lesson1);
            Assert.Equal(timePeriods[1], schoolDay.Break1);
            Assert.Equal(timePeriods[2], schoolDay.Lesson2);
            Assert.Equal(timePeriods[3], schoolDay.Break2);
            Assert.Equal(timePeriods[4], schoolDay.Lesson3);
            Assert.Equal(timePeriods[5], schoolDay.Break3);
            Assert.Equal(timePeriods[6], schoolDay.Lesson4);
        } // SortByEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void SortByDurationTest()
        {
            var now = ClockProxy.Clock.Now;
            var timePeriods = new TimePeriodCollection();
            var oneHour = new TimeSpan(1, 0, 0);
            var twoHours = new TimeSpan(2, 0, 0);
            var threeHours = new TimeSpan(3, 0, 0);
            var fourHours = new TimeSpan(4, 0, 0);
            timePeriods.Add(new TimeRange(now, oneHour));
            timePeriods.Add(new TimeRange(now, twoHours));
            timePeriods.Add(new TimeRange(now, threeHours));
            timePeriods.Add(new TimeRange(now, fourHours));

            timePeriods.SortByDuration(ListSortDirection.Descending);

            Assert.Equal(fourHours, timePeriods[0].Duration);
            Assert.Equal(threeHours, timePeriods[1].Duration);
            Assert.Equal(twoHours, timePeriods[2].Duration);
            Assert.Equal(oneHour, timePeriods[3].Duration);

            timePeriods.SortByDuration();

            Assert.Equal(oneHour, timePeriods[0].Duration);
            Assert.Equal(twoHours, timePeriods[1].Duration);
            Assert.Equal(threeHours, timePeriods[2].Duration);
            Assert.Equal(fourHours, timePeriods[3].Duration);
        } // SortByDurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void InsidePeriodsTimePeriodTest()
        {
            var now = ClockProxy.Clock.Now;
            var timeRange1 = new TimeRange(new DateTime(now.Year, now.Month, 8), new DateTime(now.Year, now.Month, 18));
            var timeRange2 = new TimeRange(new DateTime(now.Year, now.Month, 10), new DateTime(now.Year, now.Month, 11));
            var timeRange3 = new TimeRange(new DateTime(now.Year, now.Month, 13), new DateTime(now.Year, now.Month, 15));
            var timeRange4 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 13));
            var timeRange5 = new TimeRange(new DateTime(now.Year, now.Month, 15), new DateTime(now.Year, now.Month, 17));

            var timePeriods = new TimePeriodCollection
            {
                timeRange1,
                timeRange2,
                timeRange3,
                timeRange4,
                timeRange5
            };

            Assert.Equal(5, timePeriods.InsidePeriods(timeRange1).Count);
            Assert.Single(timePeriods.InsidePeriods(timeRange2));
            Assert.Single(timePeriods.InsidePeriods(timeRange3));
            Assert.Equal(2, timePeriods.InsidePeriods(timeRange4).Count);
            Assert.Single(timePeriods.InsidePeriods(timeRange5));

            var test1 = timeRange1.Copy(new TimeSpan(100, 0, 0, 0).Negate());
            var insidePeriods1 = timePeriods.InsidePeriods(test1);
            Assert.Empty(insidePeriods1);

            var test2 = timeRange1.Copy(new TimeSpan(100, 0, 0, 0));
            var insidePeriods2 = timePeriods.InsidePeriods(test2);
            Assert.Empty(insidePeriods2);

            var test3 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 11));
            var insidePeriods3 = timePeriods.InsidePeriods(test3);
            Assert.Single(insidePeriods3);

            var test4 = new TimeRange(new DateTime(now.Year, now.Month, 14), new DateTime(now.Year, now.Month, 17));
            var insidePeriods4 = timePeriods.InsidePeriods(test4);
            Assert.Single(insidePeriods4);
        } // InsidePeriodsTimePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void OverlapPeriodsTest()
        {
            var now = ClockProxy.Clock.Now;
            var timeRange1 = new TimeRange(new DateTime(now.Year, now.Month, 8), new DateTime(now.Year, now.Month, 18));
            var timeRange2 = new TimeRange(new DateTime(now.Year, now.Month, 10), new DateTime(now.Year, now.Month, 11));
            var timeRange3 = new TimeRange(new DateTime(now.Year, now.Month, 13), new DateTime(now.Year, now.Month, 15));
            var timeRange4 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 13));
            var timeRange5 = new TimeRange(new DateTime(now.Year, now.Month, 15), new DateTime(now.Year, now.Month, 17));

            var timePeriods = new TimePeriodCollection
            {
                timeRange1,
                timeRange2,
                timeRange3,
                timeRange4,
                timeRange5
            };

            Assert.Equal(5, timePeriods.OverlapPeriods(timeRange1).Count);
            Assert.Equal(3, timePeriods.OverlapPeriods(timeRange2).Count);
            Assert.Equal(2, timePeriods.OverlapPeriods(timeRange3).Count);
            Assert.Equal(3, timePeriods.OverlapPeriods(timeRange4).Count);
            Assert.Equal(2, timePeriods.OverlapPeriods(timeRange5).Count);

            var test1 = timeRange1.Copy(new TimeSpan(100, 0, 0, 0).Negate());
            var insidePeriods1 = timePeriods.OverlapPeriods(test1);
            Assert.Empty(insidePeriods1);

            var test2 = timeRange1.Copy(new TimeSpan(100, 0, 0, 0));
            var insidePeriods2 = timePeriods.OverlapPeriods(test2);
            Assert.Empty(insidePeriods2);

            var test3 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 11));
            var insidePeriods3 = timePeriods.OverlapPeriods(test3);
            Assert.Equal(3, insidePeriods3.Count);

            var test4 = new TimeRange(new DateTime(now.Year, now.Month, 14), new DateTime(now.Year, now.Month, 17));
            var insidePeriods4 = timePeriods.OverlapPeriods(test4);
            Assert.Equal(3, insidePeriods4.Count);
        } // OverlapPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void IntersectionPeriodsDateTimeTest()
        {
            var now = ClockProxy.Clock.Now;
            var timeRange1 = new TimeRange(new DateTime(now.Year, now.Month, 8), new DateTime(now.Year, now.Month, 18));
            var timeRange2 = new TimeRange(new DateTime(now.Year, now.Month, 10), new DateTime(now.Year, now.Month, 11));
            var timeRange3 = new TimeRange(new DateTime(now.Year, now.Month, 13), new DateTime(now.Year, now.Month, 15));
            var timeRange4 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 14));
            var timeRange5 = new TimeRange(new DateTime(now.Year, now.Month, 16), new DateTime(now.Year, now.Month, 17));

            var timePeriods = new TimePeriodCollection
            {
                timeRange1,
                timeRange2,
                timeRange3,
                timeRange4,
                timeRange5
            };

            Assert.Single(timePeriods.IntersectionPeriods(timeRange1.Start));
            Assert.Single(timePeriods.IntersectionPeriods(timeRange1.End));

            Assert.Equal(3, timePeriods.IntersectionPeriods(timeRange2.Start).Count);
            Assert.Equal(3, timePeriods.IntersectionPeriods(timeRange2.End).Count);

            Assert.Equal(3, timePeriods.IntersectionPeriods(timeRange3.Start).Count);
            Assert.Equal(2, timePeriods.IntersectionPeriods(timeRange3.End).Count);

            Assert.Equal(2, timePeriods.IntersectionPeriods(timeRange4.Start).Count);
            Assert.Equal(3, timePeriods.IntersectionPeriods(timeRange4.End).Count);

            Assert.Equal(2, timePeriods.IntersectionPeriods(timeRange5.Start).Count);
            Assert.Equal(2, timePeriods.IntersectionPeriods(timeRange5.End).Count);

            var test1 = timeRange1.Start.AddMilliseconds(-1);
            var insidePeriods1 = timePeriods.IntersectionPeriods(test1);
            Assert.Empty(insidePeriods1);

            var test2 = timeRange1.End.AddMilliseconds(1);
            var insidePeriods2 = timePeriods.IntersectionPeriods(test2);
            Assert.Empty(insidePeriods2);

            var test3 = new DateTime(now.Year, now.Month, 12);
            var insidePeriods3 = timePeriods.IntersectionPeriods(test3);
            Assert.Equal(2, insidePeriods3.Count);

            var test4 = new DateTime(now.Year, now.Month, 14);
            var insidePeriods4 = timePeriods.IntersectionPeriods(test4);
            Assert.Equal(3, insidePeriods4.Count);
        } // IntersectionPeriodsDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void RelationPeriodsTest()
        {
            var now = ClockProxy.Clock.Now;
            var timeRange1 = new TimeRange(new DateTime(now.Year, now.Month, 8), new DateTime(now.Year, now.Month, 18));
            var timeRange2 = new TimeRange(new DateTime(now.Year, now.Month, 10), new DateTime(now.Year, now.Month, 11));
            var timeRange3 = new TimeRange(new DateTime(now.Year, now.Month, 13), new DateTime(now.Year, now.Month, 15));
            var timeRange4 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 14));
            var timeRange5 = new TimeRange(new DateTime(now.Year, now.Month, 16), new DateTime(now.Year, now.Month, 17));

            var timePeriods = new TimePeriodCollection
            {
                timeRange1,
                timeRange2,
                timeRange3,
                timeRange4,
                timeRange5
            };

            Assert.Single(timePeriods.RelationPeriods(timeRange1, PeriodRelation.ExactMatch));
            Assert.Single(timePeriods.RelationPeriods(timeRange2, PeriodRelation.ExactMatch));
            Assert.Single(timePeriods.RelationPeriods(timeRange3, PeriodRelation.ExactMatch));
            Assert.Single(timePeriods.RelationPeriods(timeRange4, PeriodRelation.ExactMatch));
            Assert.Single(timePeriods.RelationPeriods(timeRange5, PeriodRelation.ExactMatch));

            // all
            Assert.Equal(5, timePeriods.RelationPeriods(new TimeRange(new DateTime(now.Year, now.Month, 7), new DateTime(now.Year, now.Month, 19)), PeriodRelation.Enclosing).Count);

            // time range 3
            Assert.Single(timePeriods.RelationPeriods(new TimeRange(new DateTime(now.Year, now.Month, 11), new DateTime(now.Year, now.Month, 16)), PeriodRelation.Enclosing));
        } // RelationPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void IntersectionPeriodsTimePeriodTest()
        {
            var now = ClockProxy.Clock.Now;
            var timeRange1 = new TimeRange(new DateTime(now.Year, now.Month, 8), new DateTime(now.Year, now.Month, 18));
            var timeRange2 = new TimeRange(new DateTime(now.Year, now.Month, 10), new DateTime(now.Year, now.Month, 11));
            var timeRange3 = new TimeRange(new DateTime(now.Year, now.Month, 13), new DateTime(now.Year, now.Month, 15));
            var timeRange4 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 13));
            var timeRange5 = new TimeRange(new DateTime(now.Year, now.Month, 15), new DateTime(now.Year, now.Month, 17));

            var timePeriods = new TimePeriodCollection
            {
                timeRange1,
                timeRange2,
                timeRange3,
                timeRange4,
                timeRange5
            };

            Assert.Equal(5, timePeriods.IntersectionPeriods(timeRange1).Count);
            Assert.Equal(3, timePeriods.IntersectionPeriods(timeRange2).Count);
            Assert.Equal(4, timePeriods.IntersectionPeriods(timeRange3).Count);
            Assert.Equal(4, timePeriods.IntersectionPeriods(timeRange4).Count);
            Assert.Equal(3, timePeriods.IntersectionPeriods(timeRange5).Count);

            var test1 = timeRange1.Copy(new TimeSpan(100, 0, 0, 0).Negate());
            var insidePeriods1 = timePeriods.IntersectionPeriods(test1);
            Assert.Empty(insidePeriods1);

            var test2 = timeRange1.Copy(new TimeSpan(100, 0, 0, 0));
            var insidePeriods2 = timePeriods.IntersectionPeriods(test2);
            Assert.Empty(insidePeriods2);

            var test3 = new TimeRange(new DateTime(now.Year, now.Month, 9), new DateTime(now.Year, now.Month, 11));
            var insidePeriods3 = timePeriods.IntersectionPeriods(test3);
            Assert.Equal(3, insidePeriods3.Count);

            var test4 = new TimeRange(new DateTime(now.Year, now.Month, 14), new DateTime(now.Year, now.Month, 17));
            var insidePeriods4 = timePeriods.IntersectionPeriods(test4);
            Assert.Equal(3, insidePeriods4.Count);
        } // IntersectionPeriodsTimePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void HasEndTest()
        {
            var now = ClockProxy.Clock.Now;
            var timePeriods = new TimePeriodCollection();
            Assert.False(timePeriods.HasEnd);

            timePeriods.Add(new TimeBlock(Duration.Hour, TimeSpec.MaxPeriodDate));
            Assert.False(timePeriods.HasEnd);

            timePeriods.Clear();
            timePeriods.Add(new TimeBlock(now, Duration.Hour));
            Assert.True(timePeriods.HasEnd);
        } // HasEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void EndTest()
        {
            var now = ClockProxy.Clock.Now;
            var timePeriods = new TimePeriodCollection();
            Assert.Equal(timePeriods.End, TimeSpec.MaxPeriodDate);

            timePeriods.Add(new TimeBlock(Duration.Hour, now));
            Assert.Equal(timePeriods.End, now);

            timePeriods.Clear();
            Assert.Equal(timePeriods.End, TimeSpec.MaxPeriodDate);
        } // EndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void EndMoveTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection(schoolDay);

            var end = schoolDay.End;
            timePeriods.End = end.AddHours(0);
            Assert.Equal(timePeriods.End, end);
            timePeriods.End = end.AddHours(1);
            Assert.Equal(timePeriods.End, end.AddHours(1));
            timePeriods.End = end.AddHours(-1);
            Assert.Equal(timePeriods.End, end.AddHours(-1));
        } // EndMoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void DurationTest()
        {
            var timePeriods = new TimePeriodCollection();
            Assert.Equal(timePeriods.Duration, TimeSpec.MaxPeriodDuration);

            var duration = Duration.Hour;
            timePeriods.Add(new TimeBlock(ClockProxy.Clock.Now, duration));
            Assert.Equal(timePeriods.Duration, duration);
        } // DurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void TotalDurationTest()
        {
            var now = ClockProxy.Clock.Now;
            var timeRange1 = new TimeRange(new DateTime(now.Year, now.Month, 8), new DateTime(now.Year, now.Month, 18));
            var timeRange2 = new TimeRange(new DateTime(now.Year, now.Month, 10), new DateTime(now.Year, now.Month, 11));

            var timePeriods = new TimePeriodCollection();
            Assert.Equal(timePeriods.TotalDuration, TimeSpan.Zero);

            timePeriods.Add(timeRange1);
            Assert.Equal(timePeriods.TotalDuration, timeRange1.End.Subtract(timeRange1.Start));
            Assert.Equal(timePeriods.TotalDuration, timeRange1.Duration);

            timePeriods.Add(timeRange2);
            Assert.Equal(timePeriods.TotalDuration, timeRange1.End.Subtract(timeRange1.Start).
                Add(timeRange2.End.Subtract(timeRange2.Start)));
            Assert.Equal(timePeriods.TotalDuration, timeRange1.Duration.Add(timeRange2.Duration));
        } // TotalDurationTest

        /*
		// ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
		[Fact]
		public void TotalDaylightDurationTest()
		{
			DateTime dstStart = new DateTime( 2014, 3, 30, 2, 0, 0 ).AddHours( 1 );
			DateTime dstEnd = new DateTime( 2014, 10, 26, 3, 0, 0 ).AddHours( -1 );
			TimeRange timeRange1 = new TimeRange( dstStart.AddHours( -2 ), dstStart.AddHours( 2 ) );
			TimeRange timeRange2 = new TimeRange( dstEnd.AddHours( -2 ), dstEnd.AddHours( 2 ) );

			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById( "Central Europe Standard Time" );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal( timePeriods.GetTotalDaylightDuration( timeZone ), TimeSpan.Zero );

			timePeriods.Add( timeRange1 );
			Assert.Equal( timePeriods.GetTotalDaylightDuration( timeZone ), timeRange1.GetDaylightDuration( timeZone ) );

			timePeriods.Add( timeRange2 );
			Assert.Equal( timePeriods.GetTotalDaylightDuration( timeZone ), timeRange1.GetDaylightDuration( timeZone ).
				Add( timeRange2.GetDaylightDuration( timeZone ) ) );
		} // TotalDaylightDurationTest
		*/

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void MoveTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection(schoolDay);

            var startDate = schoolDay.Start;
            var endDate = schoolDay.End;
            var startDuration = timePeriods.Duration;

            var duration = Duration.Hour;
            timePeriods.Move(duration);

            Assert.Equal(timePeriods.Start, startDate.Add(duration));
            Assert.Equal(timePeriods.End, endDate.Add(duration));
            Assert.Equal(timePeriods.Duration, startDuration);
        } // MoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void AddTest()
        {
            var timePeriods = new TimePeriodCollection();
            Assert.Empty(timePeriods);

            timePeriods.Add(new TimeRange());
            Assert.Single(timePeriods);

            timePeriods.Add(new TimeRange());
            Assert.Equal(2, timePeriods.Count);

            timePeriods.Clear();
            Assert.Empty(timePeriods);
        } // AddTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void ContainsPeriodTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection(schoolDay);

            var timeRange = new TimeRange(schoolDay.Lesson1.Start, schoolDay.Lesson1.End);
            Assert.DoesNotContain(timeRange, timePeriods);
            Assert.True(timePeriods.ContainsPeriod(timeRange));

            timePeriods.Add(timeRange);
            Assert.Contains(timeRange, timePeriods);
            Assert.True(timePeriods.ContainsPeriod(timeRange));
        } // ContainsPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void AddAllTest()
        {
            var now = ClockProxy.Clock.Now;
            // ReSharper disable once CollectionNeverUpdated.Local
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection();

            Assert.Empty(timePeriods);

            timePeriods.AddAll(schoolDay);
            Assert.Equal(timePeriods.Count, schoolDay.Count);

            timePeriods.Clear();
            Assert.Empty(timePeriods);
        } // AddAllTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void InsertTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection();
            Assert.Empty(timePeriods);

            timePeriods.Add(schoolDay.Lesson1);
            Assert.Single(timePeriods);
            timePeriods.Add(schoolDay.Lesson3);
            Assert.Equal(2, timePeriods.Count);
            timePeriods.Add(schoolDay.Lesson4);
            Assert.Equal(3, timePeriods.Count);

            // between
            Assert.Equal(timePeriods[1], schoolDay.Lesson3);
            timePeriods.Insert(1, schoolDay.Lesson2);
            Assert.Equal(timePeriods[1], schoolDay.Lesson2);

            // first
            Assert.Equal(timePeriods[0], schoolDay.Lesson1);
            timePeriods.Insert(0, schoolDay.Break1);
            Assert.Equal(timePeriods[0], schoolDay.Break1);

            // last
            Assert.Equal(timePeriods[timePeriods.Count - 1], schoolDay.Lesson4);
            timePeriods.Insert(timePeriods.Count, schoolDay.Break3);
            Assert.Equal(timePeriods[timePeriods.Count - 1], schoolDay.Break3);
        } // InsertTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void ContainsTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection();

            Assert.DoesNotContain(schoolDay.Lesson1, timePeriods);
            timePeriods.Add(schoolDay.Lesson1);
            Assert.Contains(schoolDay.Lesson1, timePeriods);
            timePeriods.Remove(schoolDay.Lesson1);
            Assert.DoesNotContain(schoolDay.Lesson1, timePeriods);
        } // ContainsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void IndexOfTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection();

            Assert.Equal(-1, timePeriods.IndexOf(new TimeRange()));
            Assert.Equal(-1, timePeriods.IndexOf(new TimeBlock()));

            timePeriods.AddAll(schoolDay);

            Assert.Equal(0, timePeriods.IndexOf(schoolDay.Lesson1));
            Assert.Equal(1, timePeriods.IndexOf(schoolDay.Break1));
            Assert.Equal(2, timePeriods.IndexOf(schoolDay.Lesson2));
            Assert.Equal(3, timePeriods.IndexOf(schoolDay.Break2));
            Assert.Equal(4, timePeriods.IndexOf(schoolDay.Lesson3));
            Assert.Equal(5, timePeriods.IndexOf(schoolDay.Break3));
            Assert.Equal(6, timePeriods.IndexOf(schoolDay.Lesson4));

            timePeriods.Remove(schoolDay.Lesson1);
            Assert.Equal(-1, timePeriods.IndexOf(schoolDay.Lesson1));
        } // IndexOfTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void CopyToTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection(schoolDay);

            var array = new ITimePeriod[schoolDay.Count];
            timePeriods.CopyTo(array, 0);
            Assert.Equal(array[0], schoolDay.Lesson1);
            Assert.Equal(array[1], schoolDay.Break1);
            Assert.Equal(array[2], schoolDay.Lesson2);
            Assert.Equal(array[3], schoolDay.Break2);
            Assert.Equal(array[4], schoolDay.Lesson3);
            Assert.Equal(array[5], schoolDay.Break3);
            Assert.Equal(array[6], schoolDay.Lesson4);
        } // CopyToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void ClearTest()
        {
            var timePeriods = new TimePeriodCollection();
            Assert.Empty(timePeriods);
            timePeriods.Clear();
            Assert.Empty(timePeriods);

            timePeriods.AddAll(new SchoolDay());
            Assert.Equal(7, timePeriods.Count);
            timePeriods.Clear();
            Assert.Empty(timePeriods);
        } // ClearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void RemoveTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection();

            Assert.DoesNotContain(schoolDay.Lesson1, timePeriods);
            timePeriods.Add(schoolDay.Lesson1);
            Assert.Contains(schoolDay.Lesson1, timePeriods);
            timePeriods.Remove(schoolDay.Lesson1);
            Assert.DoesNotContain(schoolDay.Lesson1, timePeriods);
        } // RemoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void RemoveAtTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection(schoolDay);

            // inside
            Assert.Equal(timePeriods[2], schoolDay.Lesson2);
            timePeriods.RemoveAt(2);
            Assert.Equal(timePeriods[2], schoolDay.Break2);

            // first
            Assert.Equal(timePeriods[0], schoolDay.Lesson1);
            timePeriods.RemoveAt(0);
            Assert.Equal(timePeriods[0], schoolDay.Break1);

            // last
            Assert.Equal(timePeriods[timePeriods.Count - 1], schoolDay.Lesson4);
            timePeriods.RemoveAt(timePeriods.Count - 1);
            Assert.Equal(timePeriods[timePeriods.Count - 1], schoolDay.Break3);
        } // RemoveAtTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void IsSamePeriodTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var timePeriods = new TimePeriodCollection(schoolDay);

            Assert.True(timePeriods.IsSamePeriod(timePeriods));
            Assert.True(timePeriods.IsSamePeriod(schoolDay));

            Assert.True(schoolDay.IsSamePeriod(schoolDay));
            Assert.True(schoolDay.IsSamePeriod(timePeriods));

            Assert.False(timePeriods.IsSamePeriod(TimeBlock.Anytime));
            Assert.False(schoolDay.IsSamePeriod(TimeBlock.Anytime));

            timePeriods.RemoveAt(0);
            Assert.False(timePeriods.IsSamePeriod(schoolDay));
        } // IsSamePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void HasInsideTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second;
            var testData = new TimeRangePeriodRelationTestData(now, now.AddHours(1), offset);
            var timePeriods = new TimePeriodCollection { testData.Reference };

            Assert.False(timePeriods.HasInside(testData.Before));
            Assert.False(timePeriods.HasInside(testData.StartTouching));
            Assert.False(timePeriods.HasInside(testData.StartInside));
            Assert.False(timePeriods.HasInside(testData.InsideStartTouching));
            Assert.True(timePeriods.HasInside(testData.EnclosingStartTouching));
            Assert.True(timePeriods.HasInside(testData.Enclosing));
            Assert.True(timePeriods.HasInside(testData.EnclosingEndTouching));
            Assert.True(timePeriods.HasInside(testData.ExactMatch));
            Assert.False(timePeriods.HasInside(testData.Inside));
            Assert.False(timePeriods.HasInside(testData.InsideEndTouching));
            Assert.False(timePeriods.HasInside(testData.EndInside));
            Assert.False(timePeriods.HasInside(testData.EndTouching));
            Assert.False(timePeriods.HasInside(testData.After));
        } // HasInsideTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void IntersectsWithTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second;
            var testData = new TimeRangePeriodRelationTestData(now, now.AddHours(1), offset);
            var timePeriods = new TimePeriodCollection { testData.Reference };

            Assert.False(timePeriods.IntersectsWith(testData.Before));
            Assert.True(timePeriods.IntersectsWith(testData.StartTouching));
            Assert.True(timePeriods.IntersectsWith(testData.StartInside));
            Assert.True(timePeriods.IntersectsWith(testData.InsideStartTouching));
            Assert.True(timePeriods.IntersectsWith(testData.EnclosingStartTouching));
            Assert.True(timePeriods.IntersectsWith(testData.Enclosing));
            Assert.True(timePeriods.IntersectsWith(testData.EnclosingEndTouching));
            Assert.True(timePeriods.IntersectsWith(testData.ExactMatch));
            Assert.True(timePeriods.IntersectsWith(testData.Inside));
            Assert.True(timePeriods.IntersectsWith(testData.InsideEndTouching));
            Assert.True(timePeriods.IntersectsWith(testData.EndInside));
            Assert.True(timePeriods.IntersectsWith(testData.EndTouching));
            Assert.False(timePeriods.IntersectsWith(testData.After));
        } // IntersectsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void OverlapsWithTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second;
            var testData = new TimeRangePeriodRelationTestData(now, now.AddHours(1), offset);
            var timePeriods = new TimePeriodCollection { testData.Reference };

            Assert.False(timePeriods.OverlapsWith(testData.Before));
            Assert.False(timePeriods.OverlapsWith(testData.StartTouching));
            Assert.True(timePeriods.OverlapsWith(testData.StartInside));
            Assert.True(timePeriods.OverlapsWith(testData.InsideStartTouching));
            Assert.True(timePeriods.OverlapsWith(testData.EnclosingStartTouching));
            Assert.True(timePeriods.OverlapsWith(testData.Enclosing));
            Assert.True(timePeriods.OverlapsWith(testData.EnclosingEndTouching));
            Assert.True(timePeriods.OverlapsWith(testData.ExactMatch));
            Assert.True(timePeriods.OverlapsWith(testData.Inside));
            Assert.True(timePeriods.OverlapsWith(testData.InsideEndTouching));
            Assert.True(timePeriods.OverlapsWith(testData.EndInside));
            Assert.False(timePeriods.OverlapsWith(testData.EndTouching));
            Assert.False(timePeriods.OverlapsWith(testData.After));
        } // OverlapsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
        public void GetRelationTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second;
            var testData = new TimeRangePeriodRelationTestData(now, now.AddHours(1), offset);
            var timePeriods = new TimePeriodCollection { testData.Reference };

            Assert.Equal(PeriodRelation.Before, timePeriods.GetRelation(testData.Before));
            Assert.Equal(PeriodRelation.StartTouching, timePeriods.GetRelation(testData.StartTouching));
            Assert.Equal(PeriodRelation.StartInside, timePeriods.GetRelation(testData.StartInside));
            Assert.Equal(PeriodRelation.InsideStartTouching, timePeriods.GetRelation(testData.InsideStartTouching));
            Assert.Equal(PeriodRelation.EnclosingStartTouching, timePeriods.GetRelation(testData.EnclosingStartTouching));
            Assert.Equal(PeriodRelation.Enclosing, timePeriods.GetRelation(testData.Enclosing));
            Assert.Equal(PeriodRelation.EnclosingEndTouching, timePeriods.GetRelation(testData.EnclosingEndTouching));
            Assert.Equal(PeriodRelation.ExactMatch, timePeriods.GetRelation(testData.ExactMatch));
            Assert.Equal(PeriodRelation.Inside, timePeriods.GetRelation(testData.Inside));
            Assert.Equal(PeriodRelation.InsideEndTouching, timePeriods.GetRelation(testData.InsideEndTouching));
            Assert.Equal(PeriodRelation.EndInside, timePeriods.GetRelation(testData.EndInside));
            Assert.Equal(PeriodRelation.EndTouching, timePeriods.GetRelation(testData.EndTouching));
            Assert.Equal(PeriodRelation.After, timePeriods.GetRelation(testData.After));
        } // GetRelationTest

        // ----------------------------------------------------------------------
        // members
        private readonly TimeSpan durationTesData = Duration.Hour;
        private readonly TimeSpan offsetTestData = Duration.Millisecond;
        private readonly TimeRangePeriodRelationTestData timeRangeTestData;

    } // class TimePeriodCollectionTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
