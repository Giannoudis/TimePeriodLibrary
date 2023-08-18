// -- FILE ------------------------------------------------------------------
// name       : DayTest.cs
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

    public sealed class DayTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void InitValuesTest()
        {
            var now = ClockProxy.Clock.Now;
            var firstDay = new DateTime(now.Year, now.Month, now.Day);
            var secondDay = firstDay.AddDays(1);
            var day = new Day(now, TimeCalendar.NewEmptyOffset());

            Assert.Equal(day.Start.Year, firstDay.Year);
            Assert.Equal(day.Start.Month, firstDay.Month);
            Assert.Equal(day.Start.Day, firstDay.Day);
            Assert.Equal(0, day.Start.Hour);
            Assert.Equal(0, day.Start.Minute);
            Assert.Equal(0, day.Start.Second);
            Assert.Equal(0, day.Start.Millisecond);

            Assert.Equal(day.End.Year, secondDay.Year);
            Assert.Equal(day.End.Month, secondDay.Month);
            Assert.Equal(day.End.Day, secondDay.Day);
            Assert.Equal(0, day.End.Hour);
            Assert.Equal(0, day.End.Minute);
            Assert.Equal(0, day.End.Second);
            Assert.Equal(0, day.End.Millisecond);
        } // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void DefaultCalendarTest()
        {
            var yearStart = new DateTime(ClockProxy.Clock.Now.Year, 1, 1);
            foreach (YearMonth yearMonth in Enum.GetValues(typeof(YearMonth)))
            {
                var monthStart = new DateTime(yearStart.Year, (int)yearMonth, 1);
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);

                for (var monthDay = monthStart.Day; monthDay < monthEnd.Day; monthDay++)
                {
                    var day = new Day(monthStart.AddDays(monthDay - 1));
                    Assert.Equal(day.Year, yearStart.Year);
                    Assert.Equal(day.Month, monthStart.Month);
                    Assert.Equal(day.Month, monthEnd.Month);
                    Assert.Equal(day.DayValue, monthDay);
                    Assert.Equal(day.Start, monthStart.AddDays(monthDay - 1).Add(day.Calendar.StartOffset));
                    Assert.Equal(day.End, monthStart.AddDays(monthDay).Add(day.Calendar.EndOffset));
                }
            }
        } // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void ConstructorTest()
        {
            var now = ClockProxy.Clock.Now;

            Assert.Equal(new Day(now).Year, now.Year);
            Assert.Equal(new Day(now).Month, now.Month);
            Assert.Equal(new Day(now).DayValue, now.Day);

            Assert.Equal(new Day(now.Year, now.Month, now.Day).Year, now.Year);
            Assert.Equal(new Day(now.Year, now.Month, now.Day).Month, now.Month);
            Assert.Equal(new Day(now.Year, now.Month, now.Day).DayValue, now.Day);
        } // ConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void DayOfWeekTest()
        {
            var now = ClockProxy.Clock.Now;
            var calendar = new TimeCalendar();

            Assert.Equal(new Day(now, calendar).DayOfWeek, calendar.Culture.Calendar.GetDayOfWeek(now));
        } // DayOfWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void GetPreviousDayTest()
        {
            var day = new Day();
            Assert.Equal(day.GetPreviousDay(), day.AddDays(-1));
        } // GetPreviousDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void GetNextDayTest()
        {
            var day = new Day();
            Assert.Equal(day.GetNextDay(), day.AddDays(1));
        } // GetNextDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void AddDaysTest()
        {
            var now = ClockProxy.Clock.Now;
            var nowDay = new DateTime(now.Year, now.Month, now.Day);
            var day = new Day(now, TimeCalendar.NewEmptyOffset());

            Assert.Equal(day.AddDays(0), day);

            var previousDay = nowDay.AddDays(-1);
            Assert.Equal(day.AddDays(-1).Year, previousDay.Year);
            Assert.Equal(day.AddDays(-1).Month, previousDay.Month);
            Assert.Equal(day.AddDays(-1).DayValue, previousDay.Day);

            var nextDay = nowDay.AddDays(1);
            Assert.Equal(day.AddDays(1).Year, nextDay.Year);
            Assert.Equal(day.AddDays(1).Month, nextDay.Month);
            Assert.Equal(day.AddDays(1).DayValue, nextDay.Day);
        } // AddDaysTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Day")]
        [Fact]
        public void GetHoursTest()
        {
            var day = new Day();

            var hours = day.GetHours();
            Assert.NotNull(hours);

            var index = 0;
            foreach (var timePeriod in hours)
            {
                var hour = (Hour)timePeriod;
                Assert.Equal(hour.Start, day.Start.AddHours(index));
                Assert.Equal(hour.End, hour.Calendar.MapEnd(hour.Start.AddHours(1)));
                index++;
            }
            Assert.Equal(TimeSpec.HoursPerDay, index);
        } // GetHoursTest

    } // class DayTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
