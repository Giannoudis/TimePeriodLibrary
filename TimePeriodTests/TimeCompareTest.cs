// -- FILE ------------------------------------------------------------------
// name       : TimeCompareTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimeCompareTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameYearTest()
        {
            Assert.False(TimeCompare.IsSameYear(testDate, testDiffDate));
            Assert.True(TimeCompare.IsSameYear(new DateTime(2000, 1, 1), new DateTime(2000, 12, 31)));

            Assert.True(TimeCompare.IsSameYear(YearMonth.April, new DateTime(2000, 4, 1), new DateTime(2001, 3, 31)));
            Assert.False(TimeCompare.IsSameYear(YearMonth.April, new DateTime(2000, 1, 1), new DateTime(2000, 4, 1)));
        } // IsSameYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameHalfyearTest()
        {
            Assert.True(TimeCompare.IsSameHalfyear(new DateTime(2000, 1, 1), new DateTime(2000, 6, 30)));
            Assert.True(TimeCompare.IsSameHalfyear(new DateTime(2000, 7, 1), new DateTime(2000, 12, 31)));
            Assert.False(TimeCompare.IsSameHalfyear(new DateTime(2000, 1, 1), new DateTime(2000, 7, 1)));
            Assert.False(TimeCompare.IsSameHalfyear(new DateTime(2000, 7, 1), new DateTime(2001, 1, 1)));

            Assert.True(TimeCompare.IsSameHalfyear(YearMonth.April, new DateTime(2000, 4, 1), new DateTime(2000, 9, 30)));
            Assert.True(TimeCompare.IsSameHalfyear(YearMonth.April, new DateTime(2000, 10, 1), new DateTime(2001, 3, 31)));
            Assert.False(TimeCompare.IsSameHalfyear(YearMonth.April, new DateTime(2000, 4, 1), new DateTime(2000, 10, 1)));
            Assert.False(TimeCompare.IsSameHalfyear(YearMonth.April, new DateTime(2000, 10, 1), new DateTime(2001, 4, 1)));
        } // IsSameHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameQuarterTest()
        {
            Assert.True(TimeCompare.IsSameQuarter(new DateTime(2000, 1, 1), new DateTime(2000, 3, 31)));
            Assert.True(TimeCompare.IsSameQuarter(new DateTime(2000, 4, 1), new DateTime(2000, 6, 30)));
            Assert.True(TimeCompare.IsSameQuarter(new DateTime(2000, 7, 1), new DateTime(2000, 9, 30)));
            Assert.True(TimeCompare.IsSameQuarter(new DateTime(2000, 10, 1), new DateTime(2000, 12, 31)));

            Assert.False(TimeCompare.IsSameQuarter(new DateTime(2000, 1, 1), new DateTime(2000, 4, 1)));
            Assert.False(TimeCompare.IsSameQuarter(new DateTime(2000, 4, 1), new DateTime(2000, 7, 1)));
            Assert.False(TimeCompare.IsSameQuarter(new DateTime(2000, 7, 1), new DateTime(2000, 10, 1)));
            Assert.False(TimeCompare.IsSameQuarter(new DateTime(2000, 10, 1), new DateTime(2001, 1, 1)));

            Assert.True(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2000, 4, 1), new DateTime(2000, 6, 30)));
            Assert.True(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2000, 7, 1), new DateTime(2000, 9, 30)));
            Assert.True(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2000, 10, 1), new DateTime(2000, 12, 31)));
            Assert.True(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2001, 1, 1), new DateTime(2001, 3, 30)));

            Assert.False(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2000, 4, 1), new DateTime(2000, 7, 1)));
            Assert.False(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2000, 7, 1), new DateTime(2000, 10, 1)));
            Assert.False(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2000, 10, 1), new DateTime(2001, 1, 1)));
            Assert.False(TimeCompare.IsSameQuarter(YearMonth.April, new DateTime(2001, 1, 1), new DateTime(2001, 4, 1)));
        } // IsSameQuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameMonthTest()
        {
            Assert.False(TimeCompare.IsSameMonth(testDate, testDiffDate));
            Assert.True(TimeCompare.IsSameMonth(new DateTime(2000, 10, 1), new DateTime(2000, 10, 31)));
            Assert.True(TimeCompare.IsSameMonth(new DateTime(2000, 10, 1), new DateTime(2000, 10, 1)));
            Assert.True(TimeCompare.IsSameMonth(new DateTime(2000, 10, 31), new DateTime(2000, 10, 1)));
            Assert.False(TimeCompare.IsSameMonth(new DateTime(2000, 10, 1), new DateTime(2000, 11, 1)));
            Assert.False(TimeCompare.IsSameMonth(new DateTime(2000, 10, 1), new DateTime(2000, 9, 30)));
        } // IsSameMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameWeekTest()
        {
            var previousWeek = testDate.AddDays(-(TimeSpec.DaysPerWeek + 1));
            var nextWeek = testDate.AddDays(TimeSpec.DaysPerWeek + 1);

            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                foreach (CalendarWeekRule weekRule in Enum.GetValues(typeof(CalendarWeekRule)))
                {
                    culture.DateTimeFormat.CalendarWeekRule = weekRule;

                    foreach (YearWeekType yearWeekType in Enum.GetValues(typeof(YearWeekType)))
                    {
                        TimeTool.GetWeekOfYear(testDate, culture, yearWeekType, out var year, out var weekOfYear);
                        var startOfWeek = TimeTool.GetStartOfYearWeek(year, weekOfYear, culture, yearWeekType);

                        Assert.True(TimeCompare.IsSameWeek(testDate, startOfWeek, culture, yearWeekType));
                        Assert.True(TimeCompare.IsSameWeek(testDate, testDate, culture, yearWeekType));
                        Assert.True(TimeCompare.IsSameWeek(testDiffDate, testDiffDate, culture, yearWeekType));
                        Assert.False(TimeCompare.IsSameWeek(testDate, testDiffDate, culture, yearWeekType));
                        Assert.False(TimeCompare.IsSameWeek(testDate, previousWeek, culture, yearWeekType));
                        Assert.False(TimeCompare.IsSameWeek(testDate, nextWeek, culture, yearWeekType));

                        foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
                        {
                            culture.DateTimeFormat.FirstDayOfWeek = dayOfWeek;
                            TimeTool.GetWeekOfYear(testDate, culture, weekRule, dayOfWeek, yearWeekType, out year, out weekOfYear);
                            startOfWeek = TimeTool.GetStartOfYearWeek(year, weekOfYear, culture, weekRule, dayOfWeek, yearWeekType);

                            Assert.True(TimeCompare.IsSameWeek(testDate, startOfWeek, culture, yearWeekType));
                            Assert.True(TimeCompare.IsSameWeek(testDate, testDate, culture, yearWeekType));
                            Assert.True(TimeCompare.IsSameWeek(testDiffDate, testDiffDate, culture, yearWeekType));
                            Assert.False(TimeCompare.IsSameWeek(testDate, testDiffDate, culture, yearWeekType));
                            Assert.False(TimeCompare.IsSameWeek(testDate, previousWeek, culture, yearWeekType));
                            Assert.False(TimeCompare.IsSameWeek(testDate, nextWeek, culture, yearWeekType));
                        }

                    }

                }
            }
        } // IsSameWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameDayTest()
        {
            Assert.False(TimeCompare.IsSameDay(testDate, testDiffDate));
            Assert.True(TimeCompare.IsSameDay(new DateTime(2000, 10, 19), new DateTime(2000, 10, 19)));
            Assert.True(TimeCompare.IsSameDay(new DateTime(2000, 10, 19), new DateTime(2000, 10, 19).AddDays(1).AddMilliseconds(-1)));
            Assert.False(TimeCompare.IsSameDay(new DateTime(1978, 10, 19), new DateTime(2000, 10, 19)));
            Assert.False(TimeCompare.IsSameDay(new DateTime(2000, 10, 18), new DateTime(2000, 10, 17)));
            Assert.False(TimeCompare.IsSameDay(new DateTime(2000, 10, 18), new DateTime(2000, 10, 19)));
        } // IsSameDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameHourTest()
        {
            Assert.False(TimeCompare.IsSameHour(testDate, testDiffDate));
            Assert.True(TimeCompare.IsSameHour(new DateTime(2000, 10, 19, 18, 0, 0), new DateTime(2000, 10, 19, 18, 0, 0).AddHours(1).AddMilliseconds(-1)));
            Assert.False(TimeCompare.IsSameHour(new DateTime(1978, 10, 19, 18, 0, 0), new DateTime(2000, 10, 19, 17, 0, 0)));
            Assert.False(TimeCompare.IsSameHour(new DateTime(1978, 10, 19, 18, 0, 0), new DateTime(2000, 10, 19, 19, 0, 0)));
        } // IsSameHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameMinuteTest()
        {
            Assert.False(TimeCompare.IsSameMinute(testDate, testDiffDate));
            Assert.True(TimeCompare.IsSameMinute(new DateTime(2000, 10, 19, 18, 20, 0), new DateTime(2000, 10, 19, 18, 20, 0).AddMinutes(1).AddMilliseconds(-1)));
            Assert.False(TimeCompare.IsSameMinute(new DateTime(1978, 10, 19, 18, 20, 0), new DateTime(2000, 10, 19, 18, 19, 0)));
            Assert.False(TimeCompare.IsSameMinute(new DateTime(1978, 10, 19, 18, 20, 0), new DateTime(2000, 10, 19, 18, 21, 0)));
        } // IsSameMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCompare")]
        [Fact]
        public void IsSameSecondTest()
        {
            Assert.False(TimeCompare.IsSameSecond(testDate, testDiffDate));
            Assert.True(TimeCompare.IsSameSecond(new DateTime(2000, 10, 19, 18, 20, 30), new DateTime(2000, 10, 19, 18, 20, 30).AddSeconds(1).AddMilliseconds(-1)));
            Assert.False(TimeCompare.IsSameSecond(new DateTime(1978, 10, 19, 18, 20, 30), new DateTime(2000, 10, 19, 18, 20, 29)));
            Assert.False(TimeCompare.IsSameSecond(new DateTime(1978, 10, 19, 18, 20, 30), new DateTime(2000, 10, 19, 18, 20, 31)));
        } // IsSameSecondTest

        // ----------------------------------------------------------------------
        // members
        private readonly DateTime testDate = new(2000, 10, 2, 13, 45, 53, 673);
        private readonly DateTime testDiffDate = new(2002, 9, 3, 7, 14, 22, 234);

    } // class TimeCompareTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
