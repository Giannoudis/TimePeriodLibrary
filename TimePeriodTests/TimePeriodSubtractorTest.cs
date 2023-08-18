// -- FILE ------------------------------------------------------------------
// name       : TimePeriodSubtractorTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.01.29
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

    public sealed class TimePeriodSubtractorTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void NoPeriodsTest()
        {
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection(), new TimePeriodCollection());
            Assert.Empty(periods);
        } // NoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void SinglePeriodAnytimeTest()
        {
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { TimeRange.Anytime }, new TimePeriodCollection { TimeRange.Anytime });
            Assert.Empty(periods);
        } // SinglePeriodAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void MomentTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 05));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { period1 }, new TimePeriodCollection { period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(period1));
        } // MomentTest


        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void TouchingPeriodsTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 4, 01));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 4, 01));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { period1 }, new TimePeriodCollection { period2, period3 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20))));
        } // TouchingPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void IntersectionPeriodsTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 4, 01));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 25));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { period1 }, new TimePeriodCollection { period2, period3 });
            Assert.Equal(3, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20))));
            Assert.True(periods[2].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 25), new DateTime(2011, 4, 01))));
        } // IntersectionPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void NoSubtractionTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15));
            var period4 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { period1, period3 }, new TimePeriodCollection { period2, period4 });
            Assert.Equal(2, periods.Count);
            Assert.True(periods[0].IsSamePeriod(period1));
            Assert.True(periods[1].IsSamePeriod(period3));
        } // NoSubtractionTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void CombineTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { period1, period2 }, new TimePeriodCollection());
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(period1.Start, period2.End)));
        } // CombineTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void NotCombineTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { period1, period2 },
                new TimePeriodCollection(), false);
            Assert.Equal(2, periods.Count);
            Assert.True(periods[0].IsSamePeriod(period1));
            Assert.True(periods[1].IsSamePeriod(period2));
        } // NotCombineTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void NoSubtractionCombineTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15));
            var period4 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(new TimePeriodCollection { period1, period2 }, new TimePeriodCollection { period3, period4 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(period1.Start, period2.End)));
        } // NoSubtractionCombineTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodSubtractor")]
        [Fact]
        public void SubtractionNotCombineTest()
        {
            var sourcePeriod = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var subtractPeriod = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 05));
            var periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            var periods = periodSubtractor.SubtractPeriods(
                new TimePeriodCollection { sourcePeriod }, new TimePeriodCollection { subtractPeriod }, false);
            Assert.Single(periods);
        } // NoSubtractionNotCombineTest

        [Fact]
        public void SubtractionNotCombineTest2()
        {
            TimeRange sourcePeriod1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 04));
            TimeRange sourcePeriod2 = new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 10));
            TimeRange subtractPeriod = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 06));
            TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
            ITimePeriodCollection periods = periodSubtractor.SubtractPeriods(
                new TimePeriodCollection { sourcePeriod1, sourcePeriod2 }, new TimePeriodCollection { subtractPeriod }, false);
            Assert.Equal(3, periods.Count);
        } // NoSubtractionNotCombineTest

    } // class TimePeriodSubtractorTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
