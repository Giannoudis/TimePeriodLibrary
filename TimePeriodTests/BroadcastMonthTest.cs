// -- FILE ------------------------------------------------------------------
// name       : BroadcastMonthTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.09.30
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class BroadcastMonthTest : TestUnitBase
    {
        // ----------------------------------------------------------------------
        [Trait("Category", "BroadcastMonth")]
        [Fact]
        public void SpecificMomentsTest()
        {
            Assert.Equal(2013, new BroadcastMonth(new DateTime(2013, 12, 29)).Year);
            Assert.Equal(YearMonth.December, new BroadcastMonth(new DateTime(2013, 12, 29)).Month);

            Assert.Equal(2014, new BroadcastMonth(new DateTime(2013, 12, 30)).Year);
            Assert.Equal(YearMonth.January, new BroadcastMonth(new DateTime(2013, 12, 30)).Month);

            Assert.Equal(2014, new BroadcastMonth(new DateTime(2014, 12, 28)).Year);
            Assert.Equal(YearMonth.December, new BroadcastMonth(new DateTime(2014, 12, 28)).Month);

            Assert.Equal(2015, new BroadcastMonth(new DateTime(2014, 12, 29)).Year);
            Assert.Equal(YearMonth.January, new BroadcastMonth(new DateTime(2014, 12, 29)).Month);
        } // SpecificMomentsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "BroadcastMonth")]
        [Fact]
        public void MonthDaysTest()
        {
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.January).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.February).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.March).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.April).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.May).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.June).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.July).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.August).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.September).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.October).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.November).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.December).GetDays().Count);

            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.January).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.February).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.March).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.April).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.May).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.June).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.July).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.August).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.September).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.October).GetDays().Count);
            Assert.Equal(4 * TimeSpec.DaysPerWeek, new BroadcastMonth(2013, YearMonth.November).GetDays().Count);
            Assert.Equal(5 * TimeSpec.DaysPerWeek, new BroadcastMonth(2012, YearMonth.December).GetDays().Count);
        } // MonthDaysTest

        // ----------------------------------------------------------------------
        [Trait("Category", "BroadcastMonth")]
        [Fact]
        public void MonthWeeksTest()
        {
            Assert.Equal(5, new BroadcastMonth(2012, YearMonth.January).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2012, YearMonth.February).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2012, YearMonth.March).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2012, YearMonth.April).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2012, YearMonth.May).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2012, YearMonth.June).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2012, YearMonth.July).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2012, YearMonth.August).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2012, YearMonth.September).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2012, YearMonth.October).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2012, YearMonth.November).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2012, YearMonth.December).GetWeeks().Count);

            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.January).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.February).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2013, YearMonth.March).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.April).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.May).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2013, YearMonth.June).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.July).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.August).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2013, YearMonth.September).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.October).GetWeeks().Count);
            Assert.Equal(4, new BroadcastMonth(2013, YearMonth.November).GetWeeks().Count);
            Assert.Equal(5, new BroadcastMonth(2012, YearMonth.December).GetWeeks().Count);
        } // MonthWeeksTest

    } // class BroadcastMonthTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
