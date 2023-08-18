// -- FILE ------------------------------------------------------------------
// name       : DaysTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class DaysTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Days")]
        [Fact]
        public void SingleDaysTest()
        {
            const int startYear = 2004;
            const int startMonth = 2;
            const int startDay = 22;
            var days = new Days(startYear, startMonth, startDay, 1);

            Assert.Equal(1, days.DayCount);
            Assert.Equal(startYear, days.StartYear);
            Assert.Equal(startMonth, days.StartMonth);
            Assert.Equal(startDay, days.StartDay);
            Assert.Equal(2004, days.EndYear);
            Assert.Equal(2, days.EndMonth);
            Assert.Equal(startDay, days.EndDay);
            Assert.Single(days.GetDays());
            Assert.True(days.GetDays()[0].IsSamePeriod(new Day(2004, 2, 22)));
        } // SingleDaysTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Days")]
        [Fact]
        public void CalendarDaysTest()
        {
            const int startYear = 2004;
            const int startMonth = 2;
            const int startDay = 27;
            const int dayCount = 5;
            var days = new Days(startYear, startMonth, startDay, dayCount);

            Assert.Equal(dayCount, days.DayCount);
            Assert.Equal(startYear, days.StartYear);
            Assert.Equal(startMonth, days.StartMonth);
            Assert.Equal(startDay, days.StartDay);
            Assert.Equal(2004, days.EndYear);
            Assert.Equal(3, days.EndMonth);
            Assert.Equal(2, days.EndDay);
            Assert.Equal(dayCount, days.GetDays().Count);
            Assert.True(days.GetDays()[0].IsSamePeriod(new Day(2004, 2, 27)));
            Assert.True(days.GetDays()[1].IsSamePeriod(new Day(2004, 2, 28)));
            Assert.True(days.GetDays()[2].IsSamePeriod(new Day(2004, 2, 29)));
            Assert.True(days.GetDays()[3].IsSamePeriod(new Day(2004, 3, 1)));
            Assert.True(days.GetDays()[4].IsSamePeriod(new Day(2004, 3, 2)));
        } // CalendarDaysTest

    } // class DaysTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
