// -- FILE ------------------------------------------------------------------
// name       : MonthsTest.cs
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

    public sealed class MonthsTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Months")]
        [Fact]
        public void SingleMonthsTest()
        {
            const int startYear = 2004;
            const YearMonth startMonth = YearMonth.June;
            var months = new Months(startYear, startMonth, 1);

            Assert.Equal(1, months.MonthCount);
            Assert.Equal(startMonth, months.StartMonth);
            Assert.Equal(startYear, months.StartYear);
            Assert.Equal(startYear, months.EndYear);
            Assert.Equal(YearMonth.June, months.EndMonth);
            Assert.Single(months.GetMonths());
            Assert.True(months.GetMonths()[0].IsSamePeriod(new Month(2004, YearMonth.June)));
        } // SingleMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Months")]
        [Fact]
        public void CalendarMonthsTest()
        {
            const int startYear = 2004;
            const YearMonth startMonth = YearMonth.November;
            const int monthCount = 5;
            var months = new Months(startYear, startMonth, monthCount);

            Assert.Equal(monthCount, months.MonthCount);
            Assert.Equal(startMonth, months.StartMonth);
            Assert.Equal(startYear, months.StartYear);
            Assert.Equal(2005, months.EndYear);
            Assert.Equal(YearMonth.March, months.EndMonth);
            Assert.Equal(monthCount, months.GetMonths().Count);
            Assert.True(months.GetMonths()[0].IsSamePeriod(new Month(2004, YearMonth.November)));
            Assert.True(months.GetMonths()[1].IsSamePeriod(new Month(2004, YearMonth.December)));
            Assert.True(months.GetMonths()[2].IsSamePeriod(new Month(2005, YearMonth.January)));
            Assert.True(months.GetMonths()[3].IsSamePeriod(new Month(2005, YearMonth.February)));
            Assert.True(months.GetMonths()[4].IsSamePeriod(new Month(2005, YearMonth.March)));
        } // CalendarMonthsTest

    } // class MonthsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
