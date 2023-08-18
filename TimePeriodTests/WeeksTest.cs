// -- FILE ------------------------------------------------------------------
// name       : WeeksTest.cs
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

    public sealed class WeeksTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Weeks")]
        [Fact]
        public void SingleWeeksTest()
        {
            const int startYear = 2004;
            const int startWeek = 22;
            var weeks = new Weeks(startYear, startWeek, 1);

            Assert.Equal(startYear, weeks.Year);
            Assert.Equal(1, weeks.WeekCount);
            Assert.Equal(startWeek, weeks.StartWeek);
            Assert.Equal(startWeek, weeks.EndWeek);
            Assert.Single(weeks.GetWeeks());
            Assert.True(weeks.GetWeeks()[0].IsSamePeriod(new Week(2004, 22)));
        } // SingleWeeksTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Weeks")]
        [Fact]
        public void MultiWeekTest()
        {
            const int startYear = 2004;
            const int startWeek = 22;
            const int weekCount = 4;
            var weeks = new Weeks(startYear, startWeek, weekCount);

            Assert.Equal(startYear, weeks.Year);
            Assert.Equal(weekCount, weeks.WeekCount);
            Assert.Equal(startWeek, weeks.StartWeek);
            Assert.Equal(startWeek + weekCount - 1, weeks.EndWeek);
            Assert.Equal(weekCount, weeks.GetWeeks().Count);
            Assert.True(weeks.GetWeeks()[0].IsSamePeriod(new Week(2004, 22)));
        } // MultiWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Weeks")]
        [Fact]
        public void CalendarWeeksTest()
        {
            const int startYear = 2004;
            const int startWeek = 22;
            const int weekCount = 5;
            var weeks = new Weeks(startYear, startWeek, weekCount);

            Assert.Equal(startYear, weeks.Year);
            Assert.Equal(weekCount, weeks.WeekCount);
            Assert.Equal(startWeek, weeks.StartWeek);
            Assert.Equal(startWeek + weekCount - 1, weeks.EndWeek);
            Assert.Equal(weekCount, weeks.GetWeeks().Count);
            Assert.True(weeks.GetWeeks()[0].IsSamePeriod(new Week(2004, 22)));
            Assert.True(weeks.GetWeeks()[1].IsSamePeriod(new Week(2004, 23)));
            Assert.True(weeks.GetWeeks()[2].IsSamePeriod(new Week(2004, 24)));
            Assert.True(weeks.GetWeeks()[3].IsSamePeriod(new Week(2004, 25)));
            Assert.True(weeks.GetWeeks()[4].IsSamePeriod(new Week(2004, 26)));
        } // CalendarWeeksTest

    } // class WeeksTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
