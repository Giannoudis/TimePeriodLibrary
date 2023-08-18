// -- FILE ------------------------------------------------------------------
// name       : TimePeriodIntersectorTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.29
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

    public sealed class TimePeriodIntersectorTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void NoPeriodsTest()
        {
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection());
            Assert.Empty(periods);
        } // NoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void SinglePeriodAnytimeTest()
        {
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { TimeRange.Anytime });
            Assert.Empty(periods);
        } // SinglePeriodAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void MomentTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 05));
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2 });
            Assert.Empty(periods);
        } // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void TouchingPeriodsTest()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 14));
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3 });
            Assert.Empty(periods);
        } // TouchingPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void SingleIntersection1Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 20));
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10))));
        } // SingleIntersection1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void SingleIntersection2Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 15));
            var period2 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20));
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15))));
        } // SingleIntersection2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void SingleIntersection3Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 20));
            var period2 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20));
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20))));
        } // SingleIntersection3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void TouchingPeriodsWithIntersection1Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10));
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 10))));
        } // TouchingPeriodsWithIntersection1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void TouchingPeriodsWithIntersection2Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 20));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period3 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20));
            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3 });
            Assert.Equal(2, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20))));
        } // TouchingPeriodsWithIntersection2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void MultipleTouchingIntersection1Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 20));
            var period3 = new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 25));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3 });
            Assert.Equal(2, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20))));
        } // MultipeTouchingIntersection1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void MultipleTouchingIntersection2Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 06), new DateTime(2011, 3, 10));

            var period4 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 14));
            var period5 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 16));
            var period6 = new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3, period4, period5, period6 });
            Assert.Single(periods);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 16))));
        } // MultipeTouchingIntersection2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void MultipleTouchingIntersection3Test()
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 15));
            var period3 = new TimeRange(new DateTime(2011, 3, 12), new DateTime(2011, 3, 18));

            var period4 = new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 24));
            var period5 = new TimeRange(new DateTime(2011, 3, 22), new DateTime(2011, 3, 28));
            var period6 = new TimeRange(new DateTime(2011, 3, 24), new DateTime(2011, 3, 26));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3, period4, period5, period6 });
            Assert.Equal(3, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 12), new DateTime(2011, 3, 15))));
            Assert.True(periods[2].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 22), new DateTime(2011, 3, 26))));
        } // MultipeTouchingIntersection3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void NotCombinedIntersection1Test()
        {

            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period3 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3 }, false);
            Assert.Equal(2, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10))));
        } // NotCombinedIntersection1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void NotCombinedIntersection2Test()
        {

            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 02), new DateTime(2011, 3, 06));
            var period3 = new TimeRange(new DateTime(2011, 3, 03), new DateTime(2011, 3, 08));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3 }, false);
            Assert.Equal(3, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 02), new DateTime(2011, 3, 03))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 03), new DateTime(2011, 3, 06))));
            Assert.True(periods[2].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 06), new DateTime(2011, 3, 08))));
        } // NotCombinedIntersection2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void NotCombinedIntersection3Test()
        {

            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 02), new DateTime(2011, 3, 08));
            var period3 = new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 06));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3 }, false);
            Assert.Equal(3, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 02), new DateTime(2011, 3, 04))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 06))));
            Assert.True(periods[2].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 06), new DateTime(2011, 3, 08))));
        } // NotCombinedIntersection3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodIntersector")]
        [Fact]
        public void NotCombinedIntersection4Test()
        {

            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period3 = new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 06));
            var period4 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();
            var periods = periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3, period4 }, false);
            Assert.Equal(4, periods.Count);
            Assert.True(periods[0].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 04))));
            Assert.True(periods[1].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 05))));
            Assert.True(periods[2].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 06))));
            Assert.True(periods[3].IsSamePeriod(new TimeRange(new DateTime(2011, 3, 06), new DateTime(2011, 3, 10))));
        } // NotCombinedIntersection4Test

    } // class TimePeriodIntersectorTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
