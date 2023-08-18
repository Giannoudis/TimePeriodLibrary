// -- FILE ------------------------------------------------------------------
// name       : MonthTest.cs
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

    public sealed class MonthTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void InitValuesTest()
        {
            var now = ClockProxy.Clock.Now;
            var firstMonth = new DateTime(now.Year, now.Month, 1);
            var secondMonth = firstMonth.AddMonths(1);
            var month = new Month(now.Year, (YearMonth)now.Month, TimeCalendar.NewEmptyOffset());

            Assert.Equal(month.Start.Year, firstMonth.Year);
            Assert.Equal(month.Start.Month, firstMonth.Month);
            Assert.Equal(month.Start.Day, firstMonth.Day);
            Assert.Equal(0, month.Start.Hour);
            Assert.Equal(0, month.Start.Minute);
            Assert.Equal(0, month.Start.Second);
            Assert.Equal(0, month.Start.Millisecond);

            Assert.Equal(month.End.Year, secondMonth.Year);
            Assert.Equal(month.End.Month, secondMonth.Month);
            Assert.Equal(month.End.Day, secondMonth.Day);
            Assert.Equal(0, month.End.Hour);
            Assert.Equal(0, month.End.Minute);
            Assert.Equal(0, month.End.Second);
            Assert.Equal(0, month.End.Millisecond);
        } // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void DefaultCalendarTest()
        {
            var yearStart = new DateTime(ClockProxy.Clock.Now.Year, 1, 1);
            foreach (YearMonth yearMonth in Enum.GetValues(typeof(YearMonth)))
            {
                var offset = (int)yearMonth - 1;
                var month = new Month(yearStart.AddMonths(offset));
                Assert.Equal(month.Year, yearStart.Year);
                Assert.Equal(month.YearMonth, yearMonth);
                Assert.Equal(month.Start, yearStart.AddMonths(offset).Add(month.Calendar.StartOffset));
                Assert.Equal(month.End, yearStart.AddMonths(offset + 1).Add(month.Calendar.EndOffset));
            }
        } // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void CurrentMonthTest()
        {
            var now = ClockProxy.Clock.Now;

            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                var currentYearMonthStart = new DateTime(now.Year, now.Month, 1);
                var currentYearMonthEnd =
                    currentYearMonthStart.AddDays(culture.Calendar.GetDaysInMonth(currentYearMonthStart.Year,
                                                                                                                                                    currentYearMonthStart.Month));
                var month = new Month(currentYearMonthStart, TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero));

                Assert.Equal(month.Year, now.Year);
                Assert.Equal(month.YearMonth, (YearMonth)now.Month);
                Assert.Equal(month.Start, currentYearMonthStart);
                Assert.Equal(month.End, currentYearMonthEnd);
            }
        } // CurrentMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void YearMonthTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var calendar = TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero);

            var january = new Month(currentYear, TimeSpec.CalendarYearStartMonth, calendar);
            Assert.Equal(TimeSpec.CalendarYearStartMonth, january.YearMonth);
            Assert.Equal(january.Start, new DateTime(currentYear, 1, 1));
            Assert.Equal(january.End, new DateTime(currentYear, 2, 1));
        } // YearMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void MonthMomentTest()
        {
            var month = new Month(new DateTime(2008, 1, 15), TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero));
            Assert.True(month.IsReadOnly);
            Assert.Equal(2008, month.Year);
            Assert.Equal(TimeSpec.CalendarYearStartMonth, month.YearMonth);
            Assert.Equal(month.Start, new DateTime(2008, 1, 1));
            Assert.Equal(month.End, new DateTime(2008, 2, 1));

            var previous = month.GetPreviousMonth();
            Assert.Equal(2007, previous.Year);
            Assert.Equal(YearMonth.December, previous.YearMonth);
            Assert.Equal(previous.Start, new DateTime(2007, 12, 1));
            Assert.Equal(previous.End, new DateTime(2008, 1, 1));

            var next = month.GetNextMonth();
            Assert.Equal(2008, next.Year);
            Assert.Equal(YearMonth.February, next.YearMonth);
            Assert.Equal(next.Start, new DateTime(2008, 2, 1));
            Assert.Equal(next.End, new DateTime(2008, 3, 1));
        } // MonthMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void LastDayStartTest()
        {
            Assert.Equal(new Month(2001, YearMonth.January).LastDayStart, new DateTime(2001, 1, 31));
            Assert.Equal(new Month(2001, YearMonth.February).LastDayStart, new DateTime(2001, 2, 28));
            Assert.Equal(new Month(2001, YearMonth.March).LastDayStart, new DateTime(2001, 3, 31));
            Assert.Equal(new Month(2001, YearMonth.April).LastDayStart, new DateTime(2001, 4, 30));
            Assert.Equal(new Month(2001, YearMonth.May).LastDayStart, new DateTime(2001, 5, 31));
            Assert.Equal(new Month(2001, YearMonth.June).LastDayStart, new DateTime(2001, 6, 30));
            Assert.Equal(new Month(2001, YearMonth.July).LastDayStart, new DateTime(2001, 7, 31));
            Assert.Equal(new Month(2001, YearMonth.August).LastDayStart, new DateTime(2001, 8, 31));
            Assert.Equal(new Month(2001, YearMonth.September).LastDayStart, new DateTime(2001, 9, 30));
            Assert.Equal(new Month(2001, YearMonth.October).LastDayStart, new DateTime(2001, 10, 31));
            Assert.Equal(new Month(2001, YearMonth.November).LastDayStart, new DateTime(2001, 11, 30));
            Assert.Equal(new Month(2001, YearMonth.December).LastDayStart, new DateTime(2001, 12, 31));

            Assert.Equal(new Month(2000, YearMonth.February).LastDayStart, new DateTime(2000, 2, 29));
        } // LastDayStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void GetDaysTest()
        {
            var month = new Month();

            var days = month.GetDays();
            Assert.NotNull(days);

            var index = 0;
            foreach (var timePeriod in days)
            {
                var day = (Day)timePeriod;
                Assert.Equal(day.Start, month.Start.AddDays(index));
                Assert.Equal(day.End, day.Calendar.MapEnd(day.Start.AddDays(1)));
                index++;
            }
        } // GetDaysTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void GetPreviousMonthTest()
        {
            var month = new Month();
            Assert.Equal(month.GetPreviousMonth(), month.AddMonths(-1));
        } // GetPreviousMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void GetNextMonthTest()
        {
            var month = new Month();
            Assert.Equal(month.GetNextMonth(), month.AddMonths(1));
        } // GetNextMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Month")]
        [Fact]
        public void AddMonthsTest()
        {
            var now = ClockProxy.Clock.Now;

            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {

                var currentYearMonthStart = new DateTime(now.Year, now.Month, 1);
                var currentYearMonthEnd =
                    currentYearMonthStart.AddDays(culture.Calendar.GetDaysInMonth(currentYearMonthStart.Year,
                                                                                                                                                    currentYearMonthStart.Month));
                var currentYearMonth = new Month(currentYearMonthStart,
                                                                                        TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero));
                Assert.Equal(currentYearMonth.Start, currentYearMonthStart);
                Assert.Equal(currentYearMonth.End, currentYearMonthEnd);

                Assert.Equal(currentYearMonth.AddMonths(0), currentYearMonth);

                var previousYearMonthStart = new DateTime(now.Year - 1, now.Month, 1);
                var previousYearMonthEnd =
                    previousYearMonthStart.AddDays(culture.Calendar.GetDaysInMonth(previousYearMonthStart.Year,
                                                                                                                                                     previousYearMonthStart.Month));
                var previousYearMonth = currentYearMonth.AddMonths(TimeSpec.MonthsPerYear * -1);
                Assert.Equal(previousYearMonth.Start, previousYearMonthStart);
                Assert.Equal(previousYearMonth.End, previousYearMonthEnd);
                Assert.Equal(previousYearMonth.YearMonth, currentYearMonth.YearMonth);
                Assert.Equal(previousYearMonth.Start.Year, currentYearMonth.Start.Year - 1);
                Assert.Equal(previousYearMonth.End.Year, currentYearMonth.End.Year - 1);

                var nextYearMonthStart = new DateTime(now.Year + 1, now.Month, 1);
                var nextYearMonthEnd =
                    nextYearMonthStart.AddDays(culture.Calendar.GetDaysInMonth(nextYearMonthStart.Year, nextYearMonthStart.Month));
                var nextYearMonth = currentYearMonth.AddMonths(TimeSpec.MonthsPerYear);
                Assert.Equal(nextYearMonth.Start, nextYearMonthStart);
                Assert.Equal(nextYearMonth.End, nextYearMonthEnd);
                Assert.Equal(nextYearMonth.YearMonth, currentYearMonth.YearMonth);
                Assert.Equal(nextYearMonth.Start.Year, currentYearMonth.Start.Year + 1);
                Assert.Equal(nextYearMonth.End.Year, currentYearMonth.End.Year + 1);
            }
        } // AddMonthsTest

    } // class MonthTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
