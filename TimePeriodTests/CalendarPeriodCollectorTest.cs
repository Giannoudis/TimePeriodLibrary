// -- FILE ------------------------------------------------------------------
// name       : CalendarPeriodCollectorTest.cs
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

    public sealed class CalendarPeriodCollectorTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CollectYearsTest()
        {
            var filter = new CalendarPeriodCollectorFilter();
            filter.Years.Add(2006);
            filter.Years.Add(2007);
            filter.Years.Add(2012);

            var testPeriod = new CalendarTimeRange(new DateTime(2001, 1, 1), new DateTime(2019, 12, 31));

            var collector = new CalendarPeriodCollector(filter, testPeriod);
            collector.CollectYears();

            Assert.Equal(3, collector.Periods.Count);
            Assert.True(collector.Periods[0].IsSamePeriod(new CalendarTimeRange(new DateTime(2006, 1, 1), new DateTime(2007, 1, 1))));
            Assert.True(collector.Periods[1].IsSamePeriod(new CalendarTimeRange(new DateTime(2007, 1, 1), new DateTime(2008, 1, 1))));
            Assert.True(collector.Periods[2].IsSamePeriod(new CalendarTimeRange(new DateTime(2012, 1, 1), new DateTime(2013, 1, 1))));
        } // CollectYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CollectMonthsTest()
        {
            var filter = new CalendarPeriodCollectorFilter();
            filter.Months.Add(YearMonth.January);

            var testPeriod = new CalendarTimeRange(new DateTime(2010, 1, 1), new DateTime(2011, 12, 31));

            var collector = new CalendarPeriodCollector(filter, testPeriod);
            collector.CollectMonths();

            Assert.Equal(2, collector.Periods.Count);
            Assert.True(collector.Periods[0].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 1), new DateTime(2010, 2, 1))));
            Assert.True(collector.Periods[1].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 1), new DateTime(2011, 2, 1))));
        } // CollectMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CollectDaysTest()
        {
            var filter = new CalendarPeriodCollectorFilter();
            filter.Months.Add(YearMonth.January);
            filter.WeekDays.Add(DayOfWeek.Friday);

            var testPeriod = new CalendarTimeRange(new DateTime(2010, 1, 1), new DateTime(2011, 12, 31));

            var collector = new CalendarPeriodCollector(filter, testPeriod);
            collector.CollectDays();

            Assert.Equal(9, collector.Periods.Count);
            Assert.True(collector.Periods[0].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 01), new DateTime(2010, 1, 02))));
            Assert.True(collector.Periods[1].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 08), new DateTime(2010, 1, 09))));
            Assert.True(collector.Periods[2].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 15), new DateTime(2010, 1, 16))));
            Assert.True(collector.Periods[3].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 22), new DateTime(2010, 1, 23))));
            Assert.True(collector.Periods[4].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 29), new DateTime(2010, 1, 30))));
            Assert.True(collector.Periods[5].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 07), new DateTime(2011, 1, 08))));
            Assert.True(collector.Periods[6].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 14), new DateTime(2011, 1, 15))));
            Assert.True(collector.Periods[7].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 21), new DateTime(2011, 1, 22))));
            Assert.True(collector.Periods[8].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 28), new DateTime(2011, 1, 29))));
        } // CollectDaysTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CollectHoursTest()
        {
            var filter = new CalendarPeriodCollectorFilter();
            filter.Months.Add(YearMonth.January);
            filter.WeekDays.Add(DayOfWeek.Friday);
            filter.CollectingHours.Add(new HourRange(8, 18));

            var testPeriod = new CalendarTimeRange(new DateTime(2010, 1, 1), new DateTime(2011, 12, 31));

            var collector = new CalendarPeriodCollector(filter, testPeriod);
            collector.CollectHours();

            Assert.Equal(9, collector.Periods.Count);
            Assert.True(collector.Periods[0].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 01, 8, 0, 0), new DateTime(2010, 1, 01, 18, 0, 0))));
            Assert.True(collector.Periods[1].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 08, 8, 0, 0), new DateTime(2010, 1, 08, 18, 0, 0))));
            Assert.True(collector.Periods[2].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 15, 8, 0, 0), new DateTime(2010, 1, 15, 18, 0, 0))));
            Assert.True(collector.Periods[3].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 22, 8, 0, 0), new DateTime(2010, 1, 22, 18, 0, 0))));
            Assert.True(collector.Periods[4].IsSamePeriod(new CalendarTimeRange(new DateTime(2010, 1, 29, 8, 0, 0), new DateTime(2010, 1, 29, 18, 0, 0))));
            Assert.True(collector.Periods[5].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 07, 8, 0, 0), new DateTime(2011, 1, 07, 18, 0, 0))));
            Assert.True(collector.Periods[6].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 14, 8, 0, 0), new DateTime(2011, 1, 14, 18, 0, 0))));
            Assert.True(collector.Periods[7].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 21, 8, 0, 0), new DateTime(2011, 1, 21, 18, 0, 0))));
            Assert.True(collector.Periods[8].IsSamePeriod(new CalendarTimeRange(new DateTime(2011, 1, 28, 8, 0, 0), new DateTime(2011, 1, 28, 18, 0, 0))));
        } // CollectHoursTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void Collect24HoursTest()
        {
            var filter = new CalendarPeriodCollectorFilter();
            filter.Months.Add(YearMonth.January);
            filter.WeekDays.Add(DayOfWeek.Friday);
            filter.CollectingHours.Add(new HourRange(8, 24));

            var testPeriod = new TimeRange(new DateTime(2010, 1, 1), new DateTime(2012, 1, 1));

            var calendar = new TimeCalendar(new TimeCalendarConfig { EndOffset = TimeSpan.Zero });
            var collector = new CalendarPeriodCollector(filter, testPeriod, SeekDirection.Forward, calendar);
            collector.CollectHours();

            Assert.Equal(9, collector.Periods.Count);
            Assert.True(collector.Periods[0].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 01, 8, 0, 0), new DateTime(2010, 1, 02))));
            Assert.True(collector.Periods[1].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 08, 8, 0, 0), new DateTime(2010, 1, 09))));
            Assert.True(collector.Periods[2].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 15, 8, 0, 0), new DateTime(2010, 1, 16))));
            Assert.True(collector.Periods[3].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 22, 8, 0, 0), new DateTime(2010, 1, 23))));
            Assert.True(collector.Periods[4].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 29, 8, 0, 0), new DateTime(2010, 1, 30))));
            Assert.True(collector.Periods[5].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 07, 8, 0, 0), new DateTime(2011, 1, 08))));
            Assert.True(collector.Periods[6].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 14, 8, 0, 0), new DateTime(2011, 1, 15))));
            Assert.True(collector.Periods[7].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 21, 8, 0, 0), new DateTime(2011, 1, 22))));
            Assert.True(collector.Periods[8].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 28, 8, 0, 0), new DateTime(2011, 1, 29))));
        } // Collect24HoursTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CollectAllDayHoursTest()
        {
            var filter = new CalendarPeriodCollectorFilter();
            filter.Months.Add(YearMonth.January);
            filter.WeekDays.Add(DayOfWeek.Friday);
            filter.CollectingHours.Add(new HourRange(0, 24));

            var testPeriod = new TimeRange(new DateTime(2010, 1, 1), new DateTime(2012, 1, 1));

            var calendar = new TimeCalendar(new TimeCalendarConfig { EndOffset = TimeSpan.Zero });
            var collector = new CalendarPeriodCollector(filter, testPeriod, SeekDirection.Forward, calendar);
            collector.CollectHours();

            Assert.Equal(9, collector.Periods.Count);
            Assert.True(collector.Periods[0].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 01), new DateTime(2010, 1, 02))));
            Assert.True(collector.Periods[1].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 08), new DateTime(2010, 1, 09))));
            Assert.True(collector.Periods[2].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 15), new DateTime(2010, 1, 16))));
            Assert.True(collector.Periods[3].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 22), new DateTime(2010, 1, 23))));
            Assert.True(collector.Periods[4].IsSamePeriod(new TimeRange(new DateTime(2010, 1, 29), new DateTime(2010, 1, 30))));
            Assert.True(collector.Periods[5].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 07), new DateTime(2011, 1, 08))));
            Assert.True(collector.Periods[6].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 14), new DateTime(2011, 1, 15))));
            Assert.True(collector.Periods[7].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 21), new DateTime(2011, 1, 22))));
            Assert.True(collector.Periods[8].IsSamePeriod(new TimeRange(new DateTime(2011, 1, 28), new DateTime(2011, 1, 29))));
        } // CollectAllDayHoursTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CollectExcludePeriodTest()
        {
            const int workingDays2011 = 365 - 2 - (51 * 2) - 1;
            const int workingDaysMarch2011 = 31 - 8; // total days - weekend days

            var year2011 = new Year(2011);

            var filter1 = new CalendarPeriodCollectorFilter();
            filter1.AddWorkingWeekDays();
            var collector1 = new CalendarPeriodCollector(filter1, year2011);
            collector1.CollectDays();
            Assert.Equal(workingDays2011, collector1.Periods.Count);

            // exclude month
            var filter2 = new CalendarPeriodCollectorFilter();
            filter2.AddWorkingWeekDays();
            filter2.ExcludePeriods.Add(new Month(2011, YearMonth.March));
            var collector2 = new CalendarPeriodCollector(filter2, year2011);
            collector2.CollectDays();
            Assert.Equal(workingDays2011 - workingDaysMarch2011, collector2.Periods.Count);

            // exclude weeks (holidays)
            var filter3 = new CalendarPeriodCollectorFilter();
            filter3.AddWorkingWeekDays();
            filter3.ExcludePeriods.Add(new Month(2011, YearMonth.March));
            filter3.ExcludePeriods.Add(new Weeks(2011, 26, 2));
            var collector3 = new CalendarPeriodCollector(filter3, year2011);
            collector3.CollectDays();
            Assert.Equal(workingDays2011 - workingDaysMarch2011 - 10, collector3.Periods.Count);
        } // CollectExcludePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CollectHoursMissingLastPeriodTest()
        {
            var filter = new CalendarPeriodCollectorFilter();
            filter.Months.Add(YearMonth.September);
            filter.WeekDays.Add(DayOfWeek.Monday);
            filter.WeekDays.Add(DayOfWeek.Tuesday);
            filter.WeekDays.Add(DayOfWeek.Wednesday);
            filter.WeekDays.Add(DayOfWeek.Thursday);
            filter.WeekDays.Add(DayOfWeek.Friday);
            filter.CollectingHours.Add(new HourRange(9, 17)); // working hours
            filter.ExcludePeriods.Add(new TimeBlock(new DateTime(2015, 9, 15, 00, 0, 0), new DateTime(2015, 9, 16, 0, 0, 0)));

            var testPeriod = new CalendarTimeRange(new DateTime(2015, 9, 14, 9, 0, 0), new DateTime(2015, 9, 17, 18, 0, 0));
            var collector = new CalendarPeriodCollector(filter, testPeriod);
            collector.CollectHours();
            Assert.Equal(3, collector.Periods.Count);
        } // CollectHoursMissingLastPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarPeriodCollector")]
        [Fact]
        public void CalendarPeriodCollectorDayHoursWithLimitsTest()
        {
            var testPeriod = new CalendarTimeRange(new DateTime(2017, 12, 4, 4, 0, 0), new DateTime(2017, 12, 6, 11, 0, 0));
            var filter = new CalendarPeriodCollectorFilter();
            foreach (var dayOfWeek in Enum.GetValues<DayOfWeek>())
            {
                filter.CollectingDayHours.Add(new DayHourRange(dayOfWeek, 0, 10)); // working hours
            }

            var calendar = new TimeCalendar(new TimeCalendarConfig
            {
                EndOffset =
                TimeSpan.Zero,
                StartOffset = TimeSpan.Zero
            });
            var collector = new CalendarPeriodCollector(filter, testPeriod, SeekDirection.Forward, calendar);
            collector.CollectHours();
         
            var resultPeriods = collector.Periods;
            Assert.Equal(3, resultPeriods.Count);
            Assert.True(collector.Periods[0].IsSamePeriod(new TimeRange(new DateTime(2017, 12, 04, 4, 0,0), new DateTime(2017, 12, 04, 10,0,0))));
            Assert.True(collector.Periods[1].IsSamePeriod(new TimeRange(new DateTime(2017, 12, 05, 0, 0,0), new DateTime(2017, 12, 05, 10,0,0))));
            Assert.True(collector.Periods[2].IsSamePeriod(new TimeRange(new DateTime(2017, 12, 06, 0, 0,0), new DateTime(2017, 12, 06, 10,0,0))));
        }

    } // class CalendarPeriodCollectorTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
