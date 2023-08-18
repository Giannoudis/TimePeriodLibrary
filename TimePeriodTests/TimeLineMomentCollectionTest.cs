// -- FILE ------------------------------------------------------------------
// name       : TimeLineMomentCollectionTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.26
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimeLineMomentCollectionTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeLineMomentCollection")]
        [Fact]
        public void HasOverlapsTest()
        {
            var start1 = ClockProxy.Clock.Now.Date;
            var end1 = start1.AddDays(1);
            var start2 = end1;
            var end2 = start2.AddDays(1);

            var periods = new TimePeriodCollection();
            var timeLineMoments = new TimeLine<TimeRange>(periods, null, null);
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start1, start1));
            Assert.False(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);

            periods.Add(new TimeRange(start1, end1));
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start1, end1));
            Assert.True(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start1.AddHours(1), end1.AddHours(1)));
            Assert.True(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start1.AddHours(-1), end1.AddHours(-1)));
            Assert.True(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start1.AddHours(-1), end1.AddHours(+1)));
            Assert.True(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start1.AddHours(+1), end1.AddHours(-1)));
            Assert.True(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start2, end2));
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Add(new TimeRange(start2, end2.AddHours(1)));
            Assert.True(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);

            periods.Add(new TimeRange(start2, end2.AddHours(-1)));
            Assert.True(timeLineMoments.HasOverlaps());
            periods.RemoveAt(periods.Count - 1);

            periods.Add(new TimeRange(start1, end2));
            Assert.True(timeLineMoments.HasOverlaps());

            periods.RemoveAt(0);
            Assert.True(timeLineMoments.HasOverlaps());

            periods.RemoveAt(0);
            Assert.False(timeLineMoments.HasOverlaps());

            periods.Clear();
            Assert.False(timeLineMoments.HasOverlaps());
        } // HasOverlapsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeLineMomentCollection")]
        [Fact]
        public void HasGapsTest()
        {
            var start1 = ClockProxy.Clock.Now.Date;
            var end1 = start1.AddDays(1);
            var start2 = end1;
            var end2 = start2.AddDays(1);
            var start3 = end2;
            var end3 = start3.AddDays(1);
            var start4 = end3;
            var end4 = start4.AddDays(1);

            var periods = new TimePeriodCollection();
            var timeLineMoments = new TimeLine<TimeRange>(periods, null, null);
            Assert.False(timeLineMoments.HasGaps());

            periods.Add(new TimeRange(start1, start1));
            Assert.False(timeLineMoments.HasGaps());
            periods.RemoveAt(periods.Count - 1);

            periods.Add(new TimeRange(start1, end1));
            Assert.False(timeLineMoments.HasGaps());

            periods.Add(new TimeRange(start1, end1));
            Assert.False(timeLineMoments.HasGaps());
            periods.RemoveAt(periods.Count - 1);
            Assert.False(timeLineMoments.HasGaps());

            periods.Add(new TimeRange(start2, end2));
            Assert.False(timeLineMoments.HasGaps());

            periods.Add(new TimeRange(start1, end2));
            Assert.False(timeLineMoments.HasGaps());
            periods.RemoveAt(periods.Count - 1);
            Assert.False(timeLineMoments.HasGaps());

            periods.Add(new TimeRange(start3, end3));
            Assert.False(timeLineMoments.HasGaps());

            periods.Add(new TimeRange(start4, end4));
            Assert.False(timeLineMoments.HasGaps());

            periods.RemoveAt(2); // start/end 3
            Assert.True(timeLineMoments.HasGaps());

            periods.RemoveAt(1); // start/end 1
            Assert.True(timeLineMoments.HasGaps());

            periods.RemoveAt(periods.Count - 1); // start/end 4
            Assert.False(timeLineMoments.HasGaps());

            periods.RemoveAt(0);
            Assert.False(timeLineMoments.HasGaps());

            periods.Clear();
            Assert.False(timeLineMoments.HasGaps());
        } // HasGapsTest

    } // class TimeLineMomentCollectionTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
