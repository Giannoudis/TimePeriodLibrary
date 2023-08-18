// -- FILE ------------------------------------------------------------------
// name       : TimeLineTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.08.23
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimeLineTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeLine")]
        [Fact]
        public void TimeRangeCombinedPeriodsTest()
        {
            var periods = new TimePeriodCollection();
            var date1 = ClockProxy.Clock.Now;
            var date2 = date1.AddDays(1);
            var date3 = date2.AddDays(1);
            periods.Add(new TimeRange(date1, date2));
            periods.Add(new TimeRange(date2, date3));

            var timeLine = new TimeLine<TimeRange>(periods, null, null);
            var combinedPeriods = timeLine.CombinePeriods();
            Assert.Single(combinedPeriods);
            Assert.Equal(new TimeRange(date1, date3), combinedPeriods[0]);
        } // TimeRangeCombinedPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeLine")]
        [Fact]
        public void TimeBlockCombinedPeriodsTest()
        {
            var periods = new TimePeriodCollection();
            var date1 = ClockProxy.Clock.Now;
            var date2 = date1.AddDays(1);
            var date3 = date2.AddDays(1);
            periods.Add(new TimeBlock(date1, date2));
            periods.Add(new TimeBlock(date2, date3));

            var timeLine = new TimeLine<TimeRange>(periods, null, null);
            var combinedPeriods = timeLine.CombinePeriods();
            Assert.Single(combinedPeriods);
            Assert.Equal(new TimeRange(date1, date3), combinedPeriods[0]);
        } // TimeBlockCombinedPeriodsTest

    } // class TimeLineTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
