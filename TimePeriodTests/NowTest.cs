// -- FILE ------------------------------------------------------------------
// name       : NowTest.cs
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

    public sealed class NowTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void CalendarYearTest()
        {
            var now = ClockProxy.Clock.Now;
            var calendarYear = Now.CalendarYear;
            Assert.Equal(calendarYear.Year, now.Year);
            Assert.Equal(1, calendarYear.Month);
            Assert.Equal(1, calendarYear.Day);
            Assert.Equal(0, calendarYear.Hour);
            Assert.Equal(0, calendarYear.Minute);
            Assert.Equal(0, calendarYear.Second);
            Assert.Equal(0, calendarYear.Millisecond);
        } // CalendarYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void CalendarHalfyearTest()
        {
            var now = ClockProxy.Clock.Now;
            var calendarHalfyear = Now.CalendarHalfyear;
            var halfyear = ((now.Month - 1) / TimeSpec.MonthsPerHalfyear) + 1;
            var halfyearMonth = ((halfyear - 1) * TimeSpec.MonthsPerHalfyear) + 1;
            Assert.Equal(calendarHalfyear.Year, now.Year);
            Assert.Equal(calendarHalfyear.Month, halfyearMonth);
            Assert.Equal(1, calendarHalfyear.Day);
            Assert.Equal(0, calendarHalfyear.Hour);
            Assert.Equal(0, calendarHalfyear.Minute);
            Assert.Equal(0, calendarHalfyear.Second);
            Assert.Equal(0, calendarHalfyear.Millisecond);
        } // CalendarHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void CalendarQuarterTest()
        {
            var now = ClockProxy.Clock.Now;
            var calendarQuarter = Now.CalendarQuarter;
            var quarter = ((now.Month - 1) / TimeSpec.MonthsPerQuarter) + 1;
            var quarterMonth = ((quarter - 1) * TimeSpec.MonthsPerQuarter) + 1;
            Assert.Equal(calendarQuarter.Year, now.Year);
            Assert.Equal(calendarQuarter.Month, quarterMonth);
            Assert.Equal(1, calendarQuarter.Day);
            Assert.Equal(0, calendarQuarter.Hour);
            Assert.Equal(0, calendarQuarter.Minute);
            Assert.Equal(0, calendarQuarter.Second);
            Assert.Equal(0, calendarQuarter.Millisecond);
        } // CalendarQuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void MonthTest()
        {
            var now = ClockProxy.Clock.Now;
            var month = Now.Month;
            Assert.Equal(month.Year, now.Year);
            Assert.Equal(month.Month, now.Month);
            Assert.Equal(1, month.Day);
            Assert.Equal(0, month.Hour);
            Assert.Equal(0, month.Minute);
            Assert.Equal(0, month.Second);
            Assert.Equal(0, month.Millisecond);
        } // MonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void YearMonthTest()
        {
            var now = ClockProxy.Clock.Now;
            Assert.Equal((int)Now.YearMonth, now.Month);
        } // YearMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void TodayTest()
        {
            var now = ClockProxy.Clock.Now;
            var today = Now.Today;
            Assert.Equal(today.Year, now.Year);
            Assert.Equal(today.Month, now.Month);
            Assert.Equal(today.Day, now.Day);
            Assert.Equal(0, today.Hour);
            Assert.Equal(0, today.Minute);
            Assert.Equal(0, today.Second);
            Assert.Equal(0, today.Millisecond);
        } // TodayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void HourTest()
        {
            var now = ClockProxy.Clock.Now;
            var hour = Now.Hour;
            Assert.Equal(hour.Year, now.Year);
            Assert.Equal(hour.Month, now.Month);
            Assert.Equal(hour.Day, now.Day);
            Assert.Equal(hour.Hour, now.Hour);
            Assert.Equal(0, hour.Minute);
            Assert.Equal(0, hour.Second);
            Assert.Equal(0, hour.Millisecond);
        } // HourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void MinuteTest()
        {
            var now = ClockProxy.Clock.Now;
            var minute = Now.Minute;
            Assert.Equal(minute.Year, now.Year);
            Assert.Equal(minute.Month, now.Month);
            Assert.Equal(minute.Day, now.Day);
            Assert.Equal(minute.Hour, now.Hour);
            Assert.Equal(minute.Minute, now.Minute);
            Assert.Equal(0, minute.Second);
            Assert.Equal(0, minute.Millisecond);
        } // MinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void SecondTest()
        {
            var now = ClockProxy.Clock.Now;
            var second = Now.Second;
            Assert.Equal(second.Year, now.Year);
            Assert.Equal(second.Month, now.Month);
            Assert.Equal(second.Day, now.Day);
            Assert.Equal(second.Hour, now.Hour);
            Assert.Equal(second.Minute, now.Minute);
            Assert.Equal(second.Second, now.Second);
            Assert.Equal(0, second.Millisecond);
        } // SecondTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void YearTest()
        {
            var now = ClockProxy.Clock.Now;

            var year = Now.Year((YearMonth)now.Month);
            Assert.Equal(year.Year, now.Year);
            Assert.Equal(year.Month, now.Month);
            Assert.Equal(1, year.Day);
            Assert.Equal(0, year.Hour);
            Assert.Equal(0, year.Minute);
            Assert.Equal(0, year.Second);
            Assert.Equal(0, year.Millisecond);

            TimeTool.PreviousMonth((YearMonth)now.Month, out _, out var previousMonth);
            var previousYear = Now.Year(previousMonth);
            Assert.Equal(previousYear.Year, now.AddMonths(-1).Year);
            Assert.Equal(previousYear.Month, now.AddMonths(-1).Month);
            Assert.Equal(1, previousYear.Day);
            Assert.Equal(0, previousYear.Hour);
            Assert.Equal(0, previousYear.Minute);
            Assert.Equal(0, previousYear.Second);
            Assert.Equal(0, previousYear.Millisecond);
        } // YearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void HalfyearTest()
        {
            var now = ClockProxy.Clock.Now;

            var halfyear = Now.Halfyear((YearMonth)now.Month);
            Assert.Equal(halfyear.Year, now.Year);
            Assert.Equal(halfyear.Month, now.Month);
            Assert.Equal(1, halfyear.Day);
            Assert.Equal(0, halfyear.Hour);
            Assert.Equal(0, halfyear.Minute);
            Assert.Equal(0, halfyear.Second);
            Assert.Equal(0, halfyear.Millisecond);

            TimeTool.PreviousMonth((YearMonth)now.Month, out _, out var previousMonth);
            var previousHalfyear = Now.Halfyear(previousMonth);
            Assert.Equal(previousHalfyear.Year, now.AddMonths(-1).Year);
            Assert.Equal(previousHalfyear.Month, now.AddMonths(-1).Month);
            Assert.Equal(1, previousHalfyear.Day);
            Assert.Equal(0, previousHalfyear.Hour);
            Assert.Equal(0, previousHalfyear.Minute);
            Assert.Equal(0, previousHalfyear.Second);
            Assert.Equal(0, previousHalfyear.Millisecond);
        } // HalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Now")]
        [Fact]
        public void WeekTest()
        {
            var now = ClockProxy.Clock.Now;

            var week = Now.Week(now.DayOfWeek);
            Assert.Equal(week.Year, now.Year);
            Assert.Equal(week.Month, now.Month);
            Assert.Equal(week.Day, now.Day);
            Assert.Equal(week.DayOfWeek, now.DayOfWeek);
            Assert.Equal(0, week.Hour);
            Assert.Equal(0, week.Minute);
            Assert.Equal(0, week.Second);
            Assert.Equal(0, week.Millisecond);

            var previousDay = TimeTool.PreviousDay(now.DayOfWeek);
            var previousWeek = Now.Week(previousDay);
            Assert.Equal(previousWeek.Year, now.AddDays(-1).Year);
            Assert.Equal(previousWeek.Month, now.AddDays(-1).Month);
            Assert.Equal(previousWeek.Day, now.AddDays(-1).Day);
            Assert.Equal(previousWeek.DayOfWeek, previousDay);
            Assert.Equal(0, previousWeek.Hour);
            Assert.Equal(0, previousWeek.Minute);
            Assert.Equal(0, previousWeek.Second);
            Assert.Equal(0, previousWeek.Millisecond);
        } // WeekTest

    } // class NowTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
