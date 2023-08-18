// -- FILE ------------------------------------------------------------------
// name       : DurationTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
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

    public sealed class DurationTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void YearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var calendar = DateDiff.SafeCurrentInfo.Calendar;

            Assert.Equal(Duration.Year(currentYear), new TimeSpan(calendar.GetDaysInYear(currentYear), 0, 0, 0));
            Assert.Equal(Duration.Year(currentYear + 1), new TimeSpan(calendar.GetDaysInYear(currentYear + 1), 0, 0, 0));
            Assert.Equal(Duration.Year(currentYear - 1), new TimeSpan(calendar.GetDaysInYear(currentYear - 1), 0, 0, 0));

            Assert.Equal(Duration.Year(calendar, currentYear), new TimeSpan(calendar.GetDaysInYear(currentYear), 0, 0, 0));
            Assert.Equal(Duration.Year(calendar, currentYear + 1), new TimeSpan(calendar.GetDaysInYear(currentYear + 1), 0, 0, 0));
            Assert.Equal(Duration.Year(calendar, currentYear - 1), new TimeSpan(calendar.GetDaysInYear(currentYear - 1), 0, 0, 0));
        } // YearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void HalfyearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var calendar = DateDiff.SafeCurrentInfo.Calendar;

            foreach (YearHalfyear yearHalfyear in Enum.GetValues(typeof(YearHalfyear)))
            {
                var halfyearMonths = TimeTool.GetMonthsOfHalfyear(yearHalfyear);
                var duration = TimeSpan.Zero;
                foreach (var halfyearMonth in halfyearMonths)
                {
                    var monthDays = calendar.GetDaysInMonth(currentYear, (int)halfyearMonth);
                    duration = duration.Add(new TimeSpan(monthDays, 0, 0, 0));
                }

                Assert.Equal(Duration.Halfyear(currentYear, yearHalfyear), duration);
                Assert.Equal(Duration.Halfyear(calendar, currentYear, yearHalfyear), duration);
            }
        } // HalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void QuarterTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var calendar = DateDiff.SafeCurrentInfo.Calendar;

            foreach (YearQuarter yearQuarter in Enum.GetValues(typeof(YearQuarter)))
            {
                var quarterMonths = TimeTool.GetMonthsOfQuarter(yearQuarter);
                var duration = TimeSpan.Zero;
                foreach (var quarterMonth in quarterMonths)
                {
                    var monthDays = calendar.GetDaysInMonth(currentYear, (int)quarterMonth);
                    duration = duration.Add(new TimeSpan(monthDays, 0, 0, 0));
                }

                Assert.Equal(Duration.Quarter(currentYear, yearQuarter), duration);
                Assert.Equal(Duration.Quarter(calendar, currentYear, yearQuarter), duration);
            }
        } // QuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void MonthTest()
        {
            var now = ClockProxy.Clock.Now;
            var currentYear = now.Year;
            var calendar = DateDiff.SafeCurrentInfo.Calendar;

            foreach (YearMonth yearMonth in Enum.GetValues(typeof(YearMonth)))
            {
                Assert.Equal(Duration.Month(currentYear, yearMonth), new TimeSpan(calendar.GetDaysInMonth(currentYear, (int)yearMonth), 0, 0, 0));
                Assert.Equal(Duration.Month(calendar, currentYear, yearMonth), new TimeSpan(calendar.GetDaysInMonth(currentYear, (int)yearMonth), 0, 0, 0));
            }
        } // MonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void WeekTest()
        {
            Assert.Equal(Duration.Week, new TimeSpan(TimeSpec.DaysPerWeek * 1, 0, 0, 0));

            Assert.Equal(Duration.Weeks(0), TimeSpan.Zero);
            Assert.Equal(Duration.Weeks(1), new TimeSpan(TimeSpec.DaysPerWeek * 1, 0, 0, 0));
            Assert.Equal(Duration.Weeks(2), new TimeSpan(TimeSpec.DaysPerWeek * 2, 0, 0, 0));
            Assert.Equal(Duration.Weeks(-1), new TimeSpan(TimeSpec.DaysPerWeek * -1, 0, 0, 0));
            Assert.Equal(Duration.Weeks(-2), new TimeSpan(TimeSpec.DaysPerWeek * -2, 0, 0, 0));
        } // WeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void DayTest()
        {
            Assert.Equal(Duration.Day, new TimeSpan(1, 0, 0, 0));

            Assert.Equal(Duration.Days(0), TimeSpan.Zero);
            Assert.Equal(Duration.Days(1), new TimeSpan(1, 0, 0, 0));
            Assert.Equal(Duration.Days(2), new TimeSpan(2, 0, 0, 0));
            Assert.Equal(Duration.Days(-1), new TimeSpan(-1, 0, 0, 0));
            Assert.Equal(Duration.Days(-2), new TimeSpan(-2, 0, 0, 0));

            Assert.Equal(Duration.Days(1, 23), new TimeSpan(1, 23, 0, 0));
            Assert.Equal(Duration.Days(1, 23, 22), new TimeSpan(1, 23, 22, 0));
            Assert.Equal(Duration.Days(1, 23, 22, 18), new TimeSpan(1, 23, 22, 18));
            Assert.Equal(Duration.Days(1, 23, 22, 18, 875), new TimeSpan(1, 23, 22, 18, 875));
        } // DayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void HourTest()
        {
            Assert.Equal(Duration.Hour, new TimeSpan(1, 0, 0));

            Assert.Equal(Duration.Hours(0), TimeSpan.Zero);
            Assert.Equal(Duration.Hours(1), new TimeSpan(1, 0, 0));
            Assert.Equal(Duration.Hours(2), new TimeSpan(2, 0, 0));
            Assert.Equal(Duration.Hours(-1), new TimeSpan(-1, 0, 0));
            Assert.Equal(Duration.Hours(-2), new TimeSpan(-2, 0, 0));

            Assert.Equal(Duration.Hours(23), new TimeSpan(0, 23, 0, 0));
            Assert.Equal(Duration.Hours(23, 22), new TimeSpan(0, 23, 22, 0));
            Assert.Equal(Duration.Hours(23, 22, 18), new TimeSpan(0, 23, 22, 18));
            Assert.Equal(Duration.Hours(23, 22, 18, 875), new TimeSpan(0, 23, 22, 18, 875));
        } // HourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void MinuteTest()
        {
            Assert.Equal(Duration.Minute, new TimeSpan(0, 1, 0));

            Assert.Equal(Duration.Minutes(0), TimeSpan.Zero);
            Assert.Equal(Duration.Minutes(1), new TimeSpan(0, 1, 0));
            Assert.Equal(Duration.Minutes(2), new TimeSpan(0, 2, 0));
            Assert.Equal(Duration.Minutes(-1), new TimeSpan(0, -1, 0));
            Assert.Equal(Duration.Minutes(-2), new TimeSpan(0, -2, 0));

            Assert.Equal(Duration.Minutes(22), new TimeSpan(0, 0, 22, 0));
            Assert.Equal(Duration.Minutes(22, 18), new TimeSpan(0, 0, 22, 18));
            Assert.Equal(Duration.Minutes(22, 18, 875), new TimeSpan(0, 0, 22, 18, 875));
        } // MinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void SecondTest()
        {
            Assert.Equal(Duration.Second, new TimeSpan(0, 0, 1));

            Assert.Equal(Duration.Seconds(0), TimeSpan.Zero);
            Assert.Equal(Duration.Seconds(1), new TimeSpan(0, 0, 1));
            Assert.Equal(Duration.Seconds(2), new TimeSpan(0, 0, 2));
            Assert.Equal(Duration.Seconds(-1), new TimeSpan(0, 0, -1));
            Assert.Equal(Duration.Seconds(-2), new TimeSpan(0, 0, -2));

            Assert.Equal(Duration.Seconds(18), new TimeSpan(0, 0, 0, 18));
            Assert.Equal(Duration.Seconds(18, 875), new TimeSpan(0, 0, 0, 18, 875));
        } // SecondTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Duration")]
        [Fact]
        public void MillisecondTest()
        {
            Assert.Equal(Duration.Millisecond, new TimeSpan(0, 0, 0, 0, 1));

            Assert.Equal(Duration.Milliseconds(0), TimeSpan.Zero);
            Assert.Equal(Duration.Milliseconds(1), new TimeSpan(0, 0, 0, 0, 1));
            Assert.Equal(Duration.Milliseconds(2), new TimeSpan(0, 0, 0, 0, 2));
            Assert.Equal(Duration.Milliseconds(-1), new TimeSpan(0, 0, 0, 0, -1));
            Assert.Equal(Duration.Milliseconds(-2), new TimeSpan(0, 0, 0, 0, -2));
        } // MillisecondTest

    } // class DurationTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
