// -- FILE ------------------------------------------------------------------
// name       : TimeGapCalculatorTest.cs
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

    public sealed class TimeGapCalculatorTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void NoPeriodsTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 5));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var gaps = gapCalculator.GetGaps(new TimePeriodCollection(), limits);
            Assert.Single(gaps);
            Assert.True(gaps[0].IsSamePeriod(limits));
        } // NoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void PeriodEqualsLimitsTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 5));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection { limits };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Empty(gaps);
        } // PeriodEqualsLimitsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void PeriodLargerThanLimitsTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 5));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection { new TimeRange(new DateTime(2011, 2, 1), new DateTime(2011, 4, 1)) };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Empty(gaps);
        } // PeriodLargerThanLimitsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void PeriodOutsideLimitsTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 5));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 2, 1), new DateTime(2011, 2, 15)),
                new TimeRange(new DateTime(2011, 4, 1), new DateTime(2011, 4, 15))
            };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Single(gaps);
            Assert.True(gaps[0].IsSamePeriod(limits));
        } // PeriodOutsideLimitsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void PeriodOutsideTouchingLimitsTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 31));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 2, 1), new DateTime(2011, 3, 5)),
                new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 4, 15))
            };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Single(gaps);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 20))));
        } // PeriodOutsideTouchingLimitsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void SimpleGapsTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection { new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15)) };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Equal(2, gaps.Count);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10))));
            Assert.True(gaps[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20))));
        } // SimpleGapsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void PeriodTouchingLimitsStartTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection { new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10)) };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Single(gaps);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20))));
        } // PeriodTouchingLimitsStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void PeriodTouchingLimitsEndTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection { new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20)) };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Single(gaps);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10))));
        } // PeriodTouchingLimitsEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void MomentPeriodTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection { new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 10)) };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Single(gaps);
            Assert.True(gaps[0].IsSamePeriod(limits));
        } // MomentPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void TouchingPeriodsTest()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 4, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 30, 08, 30, 0)),
                new TimeRange(new DateTime(2011, 3, 30, 08, 30, 0), new DateTime(2011, 3, 30, 12, 00, 0)),
                new TimeRange( new DateTime( 2011, 3, 30, 10, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) )
            };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Equal(2, gaps.Count);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 3, 30))));
            Assert.True(gaps[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 31), new DateTime(2011, 4, 01))));
        } // TouchingPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void OverlappingPeriods1Test()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 4, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 30, 12, 00, 0)),
                new TimeRange( new DateTime( 2011, 3, 30, 12, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) )
            };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Equal(2, gaps.Count);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 3, 30))));
            Assert.True(gaps[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 31), new DateTime(2011, 4, 01))));
        } // OverlappingPeriods1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void OverlappingPeriods2Test()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 4, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 30, 06, 30, 0)),
                new TimeRange( new DateTime( 2011, 3, 30, 08, 30, 0 ), new DateTime( 2011, 3, 30, 12, 00, 0 ) ),
                new TimeRange( new DateTime( 2011, 3, 30, 22, 30, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) )
            };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Equal(2, gaps.Count);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 3, 30))));
            Assert.True(gaps[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 31), new DateTime(2011, 4, 01))));
        } // OverlappingPeriods2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void OverlappingPeriods3Test()
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 4, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0))
            };

            var gaps = gapCalculator.GetGaps(excludePeriods, limits);
            Assert.Equal(2, gaps.Count);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 3, 30))));
            Assert.True(gaps[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 31), new DateTime(2011, 4, 01))));
        } // OverlappingPeriods3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void GetGapTest()
        {
            var now = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(now);
            var excludePeriods = new TimePeriodCollection();
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            excludePeriods.AddAll(schoolDay);

            Assert.Empty(gapCalculator.GetGaps(excludePeriods));
            Assert.Empty(gapCalculator.GetGaps(excludePeriods, schoolDay));

            excludePeriods.Clear();
            excludePeriods.Add(schoolDay.Lesson1);
            excludePeriods.Add(schoolDay.Lesson2);
            excludePeriods.Add(schoolDay.Lesson3);
            excludePeriods.Add(schoolDay.Lesson4);

            var gaps2 = gapCalculator.GetGaps(excludePeriods);
            Assert.Equal(3, gaps2.Count);
            Assert.True(gaps2[0].IsSamePeriod(schoolDay.Break1));
            Assert.True(gaps2[1].IsSamePeriod(schoolDay.Break2));
            Assert.True(gaps2[2].IsSamePeriod(schoolDay.Break3));

            var testRange3 = new TimeRange(schoolDay.Lesson1.Start, schoolDay.Lesson4.End);
            var gaps3 = gapCalculator.GetGaps(excludePeriods, testRange3);
            Assert.Equal(3, gaps3.Count);
            Assert.True(gaps3[0].IsSamePeriod(schoolDay.Break1));
            Assert.True(gaps3[1].IsSamePeriod(schoolDay.Break2));
            Assert.True(gaps3[2].IsSamePeriod(schoolDay.Break3));

            var testRange4 = new TimeRange(schoolDay.Start.AddHours(-1), schoolDay.End.AddHours(1));
            var gaps4 = gapCalculator.GetGaps(excludePeriods, testRange4);
            Assert.Equal(5, gaps4.Count);
            Assert.True(gaps4[0].IsSamePeriod(new TimeRange(testRange4.Start, schoolDay.Start)));
            Assert.True(gaps4[1].IsSamePeriod(schoolDay.Break1));
            Assert.True(gaps4[2].IsSamePeriod(schoolDay.Break2));
            Assert.True(gaps4[3].IsSamePeriod(schoolDay.Break3));
            Assert.True(gaps4[4].IsSamePeriod(new TimeRange(testRange4.End, testRange3.End)));

            excludePeriods.Clear();
            excludePeriods.Add(schoolDay.Lesson1);
            var gaps8 = gapCalculator.GetGaps(excludePeriods, schoolDay.Lesson1);
            Assert.Empty(gaps8);

            var testRange9 = new TimeRange(schoolDay.Lesson1.Start.Subtract(new TimeSpan(1)), schoolDay.Lesson1.End.Add(new TimeSpan(1)));
            var gaps9 = gapCalculator.GetGaps(excludePeriods, testRange9);
            Assert.Equal(2, gaps9.Count);
            Assert.Equal(gaps9[0].Duration, new TimeSpan(1));
            Assert.Equal(gaps9[1].Duration, new TimeSpan(1));
        } // GetGapsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeGapCalculator")]
        [Fact]
        public void CalendarGetGapTest()
        {
            // simulation of some reservations
            var periods = new TimePeriodCollection
            {
                new Days(2011, 3, 7, 2),
                new Days(2011, 3, 16, 2)
            };

            // the overall search range
            var limits = new CalendarTimeRange(new DateTime(2011, 3, 4), new DateTime(2011, 3, 21));
            var days = new Days(limits.Start, limits.Duration.Days + 1);
            var dayList = days.GetDays();
            foreach (var timePeriod in dayList)
            {
                var day = (Day)timePeriod;
                if (!limits.HasInside(day))
                {
                    continue; // outside of the search scope
                }
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    periods.Add(day); // // exclude weekend day
                }
            }

            var gapCalculator = new TimeGapCalculator<TimeRange>(new TimeCalendar());
            var gaps = gapCalculator.GetGaps(periods, limits);

            Assert.Equal(4, gaps.Count);
            Assert.True(gaps[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 4), Duration.Days(1))));
            Assert.True(gaps[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 9), Duration.Days(3))));
            Assert.True(gaps[2].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 14), Duration.Days(2))));
            Assert.True(gaps[3].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 18), Duration.Days(1))));
        } // CalendarGetGapTest

    } // class TimeGapCalculatorTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
