// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCombinerTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.29
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using Xunit;
using System.Collections.Generic;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimePeriodCombinerTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void NoPeriodsTest()
        {
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection());
            Assert.Empty(periods);
        } // NoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void SinglePeriodAnytimeTest()
        {
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { TimeRange.Anytime });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(TimeRange.Anytime));
        } // SinglePeriodAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void SinglePeriodTest()
        {
            var period = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 5));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(period));
        } // SinglePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void MomentTest()
        {
            var period = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 1));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(period));
        } // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void TwoPeriodsTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 5));
            var period2 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2 });
            Assert.Equal(2, periods.Count);
            Assert.True(periods[0].IsSamePeriod(period1));
            Assert.True(periods[1].IsSamePeriod(period2));
        } // TwoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void TwoPeriodsTouchingTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20))));
        } // TwoPeriodsTouchingTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void TwoPeriodsOverlap1Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 15));
            var period2 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20))));
        } // TwoPeriodsOverlap1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void TwoPeriodsOverlap2Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 20));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20))));
        } // TwoPeriodsOverlap2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void TwoPeriodsInside1Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20));
            var period2 = new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 15));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20))));
        } // TwoPeriodsInside1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void TwoPeriodsInside2Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 15));
            var period2 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20))));
        } // TwoPeriodsInside2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void Pattern1Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15));
            var period4 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20));
            var period5 = new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 25));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2, period3, period4, period5 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 25))));
        } // Pattern1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void Pattern2Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 15));

            var period3 = new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 22));
            var period4 = new TimeRange(new DateTime(2011, 3, 22), new DateTime(2011, 3, 28));
            var period5 = new TimeRange(new DateTime(2011, 3, 25), new DateTime(2011, 3, 30));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2, period3, period4, period5 });
            Assert.Equal(2, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 15))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 30))));
        } // Pattern2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void Pattern3Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 15));
            var period4 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 20));
            var period5 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 25));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2, period3, period4, period5 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 25))));
        } // Pattern3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void Pattern4Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 25));
            var period2 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 25));
            var period3 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 25));
            var period4 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 25));
            var period5 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 25));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2, period3, period4, period5 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 25))));
        } // Pattern4Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void Pattern5Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 15));
            var period4 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 25));
            var period5 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 25));
            var period6 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 25));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2, period3, period4, period5, period6 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 25))));
        } // Pattern5Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void Pattern6Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 08));

            var period3 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 18));
            var period4 = new TimeRange(new DateTime(2011, 3, 18), new DateTime(2011, 3, 22));
            var period5 = new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 24));

            var period6 = new TimeRange(new DateTime(2011, 3, 26), new DateTime(2011, 3, 30));
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var periods = periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2, period3, period4, period5, period6 });
            Assert.Equal(3, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 24))));
            Assert.True(periods[2].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 26), new DateTime(2011, 3, 30))));
        } // Pattern6Test


        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void CombinePeriodsIssue9()
        {
            // Combine Periods
            var timeRanges = new List<TimeRange>
            {
                new() { Start = new DateTime(2016, 01, 01), End = new DateTime(2016, 01, 01) },
                new() { Start = new DateTime(2016, 12, 31), End = new DateTime(2016, 12, 31) }
            };

            var periods = new TimePeriodCollection(timeRanges);
            var periodCombiner = new TimePeriodCombiner<TimeRange>();
            var combinedPeriods = periodCombiner.CombinePeriods(periods);
            Assert.Empty(combinedPeriods);
        }

    } // class TimePeriodCombinerTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
