// -- FILE ------------------------------------------------------------------
// name       : CalendarDateAddTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.04
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

    public sealed class CalendarDateAddTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void NoPeriodsTest()
        {
            var test = new DateTime(2011, 4, 12);

            var calendarDateAdd = new CalendarDateAdd();
            Assert.Equal(calendarDateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(1, 0, 0, 0)), test.Add(new TimeSpan(1, 0, 0, 0)));
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(-1, 0, 0, 0)), test.Add(new TimeSpan(-1, 0, 0, 0)));
            Assert.Equal(calendarDateAdd.Subtract(test, new TimeSpan(1, 0, 0, 0)), test.Subtract(new TimeSpan(1, 0, 0, 0)));
            Assert.Equal(calendarDateAdd.Subtract(test, new TimeSpan(-1, 0, 0, 0)), test.Subtract(new TimeSpan(-1, 0, 0, 0)));
        } // NoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void PeriodLimitsAddTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 20), new DateTime(2011, 4, 25));
            var timeRange2 = new TimeRange(new DateTime(2011, 4, 30), DateTime.MaxValue);
            var calendarDateAdd = new CalendarDateAdd();
            calendarDateAdd.ExcludePeriods.Add(timeRange1);
            calendarDateAdd.ExcludePeriods.Add(timeRange2);

            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(8, 0, 0, 0)), timeRange1.End);
            Assert.Null(calendarDateAdd.Add(test, new TimeSpan(20, 0, 0, 0)));
        } // PeriodLimitsAddTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void PeriodLimitsSubtractTest()
        {
            var test = new DateTime(2011, 4, 30);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 20), new DateTime(2011, 4, 25));
            var timeRange2 = new TimeRange(DateTime.MinValue, new DateTime(2011, 4, 10));
            var calendarDateAdd = new CalendarDateAdd();
            calendarDateAdd.ExcludePeriods.Add(timeRange1);
            calendarDateAdd.ExcludePeriods.Add(timeRange2);

            Assert.Equal(calendarDateAdd.Subtract(test, new TimeSpan(5, 0, 0, 0)), timeRange1.Start);
            Assert.Null(calendarDateAdd.Subtract(test, new TimeSpan(20, 0, 0, 0)));
        } // PeriodLimitsSubtractTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void ExcludeTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange = new TimeRange(new DateTime(2011, 4, 15), new DateTime(2011, 4, 20));
            var calendarDateAdd = new CalendarDateAdd();
            calendarDateAdd.ExcludePeriods.Add(timeRange);

            Assert.Equal(calendarDateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(2, 0, 0, 0)), test.Add(new TimeSpan(2, 0, 0, 0)));
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(3, 0, 0, 0)), timeRange.End);
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(3, 0, 0, 0, 1)), timeRange.End.Add(new TimeSpan(0, 0, 0, 0, 1)));
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(5, 0, 0, 0)), timeRange.End.Add(new TimeSpan(2, 0, 0, 0)));
        } // ExcludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void ExcludeSplitTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 15), new DateTime(2011, 4, 20));
            var timeRange2 = new TimeRange(new DateTime(2011, 4, 22), new DateTime(2011, 4, 25));
            var calendarDateAdd = new CalendarDateAdd();
            calendarDateAdd.ExcludePeriods.Add(timeRange1);
            calendarDateAdd.ExcludePeriods.Add(timeRange2);

            Assert.Equal(calendarDateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(2, 0, 0, 0)), test.Add(new TimeSpan(2, 0, 0, 0)));
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(3, 0, 0, 0)), timeRange1.End);
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(4, 0, 0, 0)), timeRange1.End.Add(new TimeSpan(1, 0, 0, 0)));
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(5, 0, 0, 0)), timeRange2.End);
            Assert.Equal(calendarDateAdd.Add(test, new TimeSpan(7, 0, 0, 0)), timeRange2.End.Add(new TimeSpan(2, 0, 0, 0)));
        } // ExcludeSplitTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void CalendarDateAddSeekBoundaryModeTest()
        {
            var timeCalendar = new TimeCalendar(new TimeCalendarConfig
            {
                Culture = new CultureInfo("en-AU"),
                EndOffset = TimeSpan.Zero
            });
            timeCalendar.Culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

            var calendarDateAdd = new CalendarDateAdd(timeCalendar);
            calendarDateAdd.AddWorkingWeekDays();
            calendarDateAdd.ExcludePeriods.Add(new Day(2011, 4, 4, calendarDateAdd.Calendar));
            calendarDateAdd.WorkingHours.Add(new HourRange(8, 18));

            var start = new DateTime(2011, 4, 1, 9, 0, 0);
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(29, 0, 0), SeekBoundaryMode.Fill), new DateTime(2011, 4, 6, 18, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(29, 0, 0)), new DateTime(2011, 4, 7, 8, 0, 0));
        } // CalendarDateAddSeekBoundaryModeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void CalendarDateAdd1Test()
        {
            var timeCalendar = new TimeCalendar(new TimeCalendarConfig
            {
                Culture = new CultureInfo("en-AU"),
                EndOffset = TimeSpan.Zero
            });
            timeCalendar.Culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

            var calendarDateAdd = new CalendarDateAdd(timeCalendar);
            calendarDateAdd.AddWorkingWeekDays();
            calendarDateAdd.ExcludePeriods.Add(new Day(2011, 4, 4, calendarDateAdd.Calendar));
            calendarDateAdd.WorkingHours.Add(new HourRange(8, 18));

            var start = new DateTime(2011, 4, 1, 9, 0, 0);
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(22, 0, 0)), new DateTime(2011, 4, 6, 11, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(29, 0, 0)), new DateTime(2011, 4, 7, 8, 0, 0));
        } // CalendarDateAdd1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void CalendarDateAdd2Test()
        {
            var calendarDateAdd = new CalendarDateAdd();
            calendarDateAdd.AddWorkingWeekDays();
            calendarDateAdd.ExcludePeriods.Add(new Day(2011, 4, 4, calendarDateAdd.Calendar));
            calendarDateAdd.WorkingHours.Add(new HourRange(8, 12));
            calendarDateAdd.WorkingHours.Add(new HourRange(13, 18));

            var start = new DateTime(2011, 4, 1, 9, 0, 0);
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(03, 0, 0)), new DateTime(2011, 4, 1, 13, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(04, 0, 0)), new DateTime(2011, 4, 1, 14, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(08, 0, 0)), new DateTime(2011, 4, 5, 08, 0, 0));
        } // CalendarDateAdd2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void CalendarDateAdd3Test()
        {
            var calendarDateAdd = new CalendarDateAdd();
            calendarDateAdd.AddWorkingWeekDays();
            calendarDateAdd.ExcludePeriods.Add(new Day(2011, 4, 4, calendarDateAdd.Calendar));
            calendarDateAdd.WorkingHours.Add(new HourRange(new Time(8, 30), new Time(12)));
            calendarDateAdd.WorkingHours.Add(new HourRange(new Time(13, 30), new Time(18)));

            var start = new DateTime(2011, 4, 1, 9, 0, 0);
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(03, 0, 0)), new DateTime(2011, 4, 1, 13, 30, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(04, 0, 0)), new DateTime(2011, 4, 1, 14, 30, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(08, 0, 0)), new DateTime(2011, 4, 5, 09, 00, 0));
        } // CalendarDateAdd3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void EmptyStartWeekTest()
        {
            var calendarDateAdd = new CalendarDateAdd();
            // weekdays
            calendarDateAdd.AddWorkingWeekDays();
            //Start on a Saturday
            var start = new DateTime(2011, 4, 2, 13, 0, 0);
            var offset = new TimeSpan(20, 0, 0); // 20 hours

            Assert.Equal(calendarDateAdd.Add(start, offset), new DateTime(2011, 4, 4, 20, 00, 0));
        } // EmptyStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateAdd")]
        [Fact]
        public void WorkingDayHoursTest()
        {
            var calendarDateAdd = new CalendarDateAdd();

            calendarDateAdd.AddWorkingWeekDays();

            calendarDateAdd.WorkingDayHours.Add(new DayHourRange(DayOfWeek.Monday, 09, 16));
            calendarDateAdd.WorkingDayHours.Add(new DayHourRange(DayOfWeek.Tuesday, 09, 16));
            calendarDateAdd.WorkingDayHours.Add(new DayHourRange(DayOfWeek.Wednesday, 09, 16));
            calendarDateAdd.WorkingDayHours.Add(new DayHourRange(DayOfWeek.Thursday, 09, 16));
            calendarDateAdd.WorkingDayHours.Add(new DayHourRange(DayOfWeek.Friday, 09, 13));

            var start = new DateTime(2011, 08, 15);
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(00, 0, 0)), new DateTime(2011, 8, 15, 09, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(07, 0, 0)), new DateTime(2011, 8, 16, 09, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(28, 0, 0)), new DateTime(2011, 8, 19, 09, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(31, 0, 0)), new DateTime(2011, 8, 19, 12, 0, 0));
            Assert.Equal(calendarDateAdd.Add(start, new TimeSpan(32, 0, 0)), new DateTime(2011, 8, 22, 09, 0, 0));
        } // WorkingDayHoursTest

    } // class CalendarDateAddTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
