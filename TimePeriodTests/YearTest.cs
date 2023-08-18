// -- FILE ------------------------------------------------------------------
// name       : YearTest.cs
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

    public sealed class YearTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void InitValuesTest()
        {
            var now = ClockProxy.Clock.Now;
            var thisYear = new DateTime(now.Year, 1, 1);
            var nextYear = thisYear.AddYears(1);
            var year = new Year(now, TimeCalendar.NewEmptyOffset());

            Assert.Equal(year.Start.Year, thisYear.Year);
            Assert.Equal(year.Start.Month, thisYear.Month);
            Assert.Equal(year.Start.Day, thisYear.Day);
            Assert.Equal(0, year.Start.Hour);
            Assert.Equal(0, year.Start.Minute);
            Assert.Equal(0, year.Start.Second);
            Assert.Equal(0, year.Start.Millisecond);

            Assert.Equal(year.End.Year, nextYear.Year);
            Assert.Equal(year.End.Month, nextYear.Month);
            Assert.Equal(year.End.Day, nextYear.Day);
            Assert.Equal(0, year.End.Hour);
            Assert.Equal(0, year.End.Minute);
            Assert.Equal(0, year.End.Second);
            Assert.Equal(0, year.End.Millisecond);
        } // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void DefaultCalendarTest()
        {
            var yearStart = new DateTime(ClockProxy.Clock.Now.Year, 1, 1);

            var year = new Year(yearStart);
            Assert.Equal(YearMonth.January, year.YearBaseMonth);
            Assert.Equal(year.BaseYear, yearStart.Year);
            Assert.Equal(year.Start, yearStart.Add(year.Calendar.StartOffset));
            Assert.Equal(year.End, yearStart.AddYears(1).Add(year.Calendar.EndOffset));
        } // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void YearBaseMonthTest()
        {
            var year = new Year(TimeCalendar.New(YearMonth.April));
            Assert.Equal(YearMonth.April, year.YearBaseMonth);
            Assert.Equal(YearMonth.January, new Year().YearBaseMonth);
        } // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void IsCalendarYearTest()
        {
            Assert.False(new Year(TimeCalendar.New(YearMonth.April)).IsCalendarYear);
            Assert.True(new Year(TimeCalendar.New(YearMonth.January)).IsCalendarYear);
            Assert.False(new Year(2008, TimeCalendar.New(YearMonth.April)).IsCalendarYear);
            Assert.True(new Year(2008).IsCalendarYear);
            Assert.True(new Year().IsCalendarYear);
            Assert.True(new Year(2008).IsCalendarYear);
        } // IsCalendarYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void StartYearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            Assert.Equal(2008, new Year(2008, TimeCalendar.New(YearMonth.April)).BaseYear);
            Assert.Equal(new Year(currentYear).BaseYear, currentYear);
            Assert.Equal(2008, new Year(2008).BaseYear);

            Assert.Equal(2007, new Year(new DateTime(2008, 7, 20), TimeCalendar.New(YearMonth.October)).BaseYear);
            Assert.Equal(2008, new Year(new DateTime(2008, 10, 1), TimeCalendar.New(YearMonth.October)).BaseYear);
        } // StartYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void CurrentYearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var year = new Year(currentYear);
            Assert.True(year.IsReadOnly);
            Assert.Equal(year.BaseYear, currentYear);
            Assert.Equal(year.Start, new DateTime(currentYear, 1, 1));
            Assert.True(year.End < new DateTime(currentYear + 1, 1, 1));
        } // CurrentYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void YearIndexTest()
        {
            const int yearIndex = 1994;
            var year = new Year(yearIndex);
            Assert.True(year.IsReadOnly);
            Assert.Equal(yearIndex, year.BaseYear);
            Assert.Equal(year.Start, new DateTime(yearIndex, 1, 1));
            Assert.True(year.End < new DateTime(yearIndex + 1, 1, 1));
        } // YearIndexTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void YearMomentTest()
        {
            const int yearIndex = 2002;
            var year = new Year(new DateTime(yearIndex, 3, 15));
            Assert.True(year.IsReadOnly);
            Assert.Equal(yearIndex, year.BaseYear);
            Assert.Equal(year.Start, new DateTime(yearIndex, 1, 1));
            Assert.True(year.End < new DateTime(yearIndex + 1, 1, 1));
        } // YearMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void YearPeriodTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var yearStart = new DateTime(currentYear, 4, 1);
            var yearEnd = yearStart.AddYears(1);

            var year = new Year(currentYear, TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero, YearMonth.April));
            Assert.True(year.IsReadOnly);
            Assert.Equal(YearMonth.April, year.YearBaseMonth);
            Assert.Equal(year.BaseYear, yearStart.Year);
            Assert.Equal(year.Start, yearStart);
            Assert.Equal(year.End, yearEnd);
        } // YearPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void YearCompareTest()
        {
            var moment = new DateTime(2008, 2, 18);
            var calendarYearSweden = new Year(moment, TimeCalendar.New(YearMonth.January));
            Assert.Equal(YearMonth.January, calendarYearSweden.YearBaseMonth);

            var calendarYearGermany = new Year(moment, TimeCalendar.New(YearMonth.April));
            Assert.Equal(YearMonth.April, calendarYearGermany.YearBaseMonth);

            var calendarYearUnitedStates = new Year(moment, TimeCalendar.New(YearMonth.October));
            Assert.Equal(YearMonth.October, calendarYearUnitedStates.YearBaseMonth);

            Assert.NotEqual(calendarYearSweden, calendarYearGermany);
            Assert.NotEqual(calendarYearSweden, calendarYearUnitedStates);
            Assert.NotEqual(calendarYearGermany, calendarYearUnitedStates);

            Assert.Equal(calendarYearSweden.BaseYear, calendarYearGermany.BaseYear + 1);
            Assert.Equal(calendarYearSweden.BaseYear, calendarYearUnitedStates.BaseYear + 1);

            Assert.Equal(calendarYearSweden.GetPreviousYear().BaseYear, calendarYearGermany.GetPreviousYear().BaseYear + 1);
            Assert.Equal(calendarYearSweden.GetPreviousYear().BaseYear, calendarYearUnitedStates.GetPreviousYear().BaseYear + 1);

            Assert.Equal(calendarYearSweden.GetNextYear().BaseYear, calendarYearGermany.GetNextYear().BaseYear + 1);
            Assert.Equal(calendarYearSweden.GetNextYear().BaseYear, calendarYearUnitedStates.GetNextYear().BaseYear + 1);

            Assert.True(calendarYearSweden.IntersectsWith(calendarYearGermany));
            Assert.True(calendarYearSweden.IntersectsWith(calendarYearUnitedStates));
            Assert.True(calendarYearGermany.IntersectsWith(calendarYearUnitedStates));
        } // YearCompareTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void GetPreviousYearTest()
        {
            var currentYearStart = new DateTime(ClockProxy.Clock.Now.Year, 4, 1);

            var currentYear = new Year(currentYearStart.Year, TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero, YearMonth.April));

            var previousYear = currentYear.GetPreviousYear();
            Assert.True(previousYear.IsReadOnly);
            Assert.Equal(YearMonth.April, previousYear.YearBaseMonth);
            Assert.Equal(previousYear.BaseYear, currentYearStart.Year - 1);
            Assert.Equal(previousYear.Start, currentYearStart.AddYears(-1));
            Assert.Equal(previousYear.End, currentYearStart);
        } // GetPreviousYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void GetNextYearTest()
        {
            var currentYearStart = new DateTime(ClockProxy.Clock.Now.Year, 4, 1);

            var currentYear = new Year(currentYearStart.Year, TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero, YearMonth.April));

            var nextYear = currentYear.GetNextYear();
            Assert.True(nextYear.IsReadOnly);
            Assert.Equal(YearMonth.April, nextYear.YearBaseMonth);
            Assert.Equal(nextYear.BaseYear, currentYearStart.Year + 1);
            Assert.Equal(nextYear.Start, currentYearStart.AddYears(1));
            Assert.Equal(nextYear.End, currentYearStart.AddYears(2));
        } // GetNextYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void AddYearsTest()
        {
            var currentYear = new Year(TimeCalendar.New(YearMonth.April));

            Assert.Equal(currentYear.AddYears(0), currentYear);

            var pastYear = currentYear.AddYears(-10);
            Assert.Equal(pastYear.Start, currentYear.Start.AddYears(-10));
            Assert.Equal(pastYear.End, currentYear.End.AddYears(-10));

            var futureYear = currentYear.AddYears(10);
            Assert.Equal(futureYear.Start, currentYear.Start.AddYears(10));
            Assert.Equal(futureYear.End, currentYear.End.AddYears(10));
        } // AddYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void GetHalfYearsTest()
        {
            var year = new Year(TimeCalendar.New(YearMonth.October));

            var halfyears = year.GetHalfyears();
            Assert.NotNull(halfyears);

            var index = 0;
            foreach (var timePeriod in halfyears)
            {
                var halfyear = (Halfyear)timePeriod;
                Assert.Equal(halfyear.BaseYear, year.BaseYear);
                Assert.Equal(halfyear.Start, year.Start.AddMonths(index * TimeSpec.MonthsPerHalfyear));
                Assert.Equal(halfyear.End, halfyear.Calendar.MapEnd(halfyear.Start.AddMonths(TimeSpec.MonthsPerHalfyear)));
                index++;
            }
            Assert.Equal(TimeSpec.HalfyearsPerYear, index);
        } // GetHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void GetQuartersTest()
        {
            var year = new Year(TimeCalendar.New(YearMonth.October));

            var quarters = year.GetQuarters();
            Assert.NotNull(quarters);

            var index = 0;
            foreach (var timePeriod in quarters)
            {
                var quarter = (Quarter)timePeriod;
                Assert.Equal(quarter.BaseYear, year.BaseYear);
                Assert.Equal(quarter.Start, year.Start.AddMonths(index * TimeSpec.MonthsPerQuarter));
                Assert.Equal(quarter.End, quarter.Calendar.MapEnd(quarter.Start.AddMonths(TimeSpec.MonthsPerQuarter)));
                index++;
            }
            Assert.Equal(TimeSpec.QuartersPerYear, index);
        } // GetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void GetMonthsTest()
        {
            const YearMonth startMonth = YearMonth.October;
            var year = new Year(TimeCalendar.New(startMonth));

            var months = year.GetMonths();
            Assert.NotNull(months);

            var index = 0;
            foreach (var timePeriod in months)
            {
                var month = (Month)timePeriod;
                TimeTool.AddMonth(year.YearValue, startMonth, index, out var monthYear, out _);
                Assert.Equal(monthYear, month.Year);
                Assert.Equal(year.Start.AddMonths(index), month.Start);
                Assert.Equal(month.Calendar.MapEnd(month.Start.AddMonths(1)), month.End);
                index++;
            }
            Assert.Equal(TimeSpec.MonthsPerYear, index);
        } // GetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void FiscalYearStartMonthTest()
        {
            var year = ClockProxy.Clock.Now.Year;
            for (var month = YearMonth.January; month <= YearMonth.December; month++)
            {
                var expectedYear = month < YearMonth.July ? year : year + 1;
                var fiscalYearStart = new DateTime(year, (int)month, 1);
                var calendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearBaseMonth = month,
                        YearType = YearType.FiscalYear
                    });
                Assert.Equal(new Year(fiscalYearStart, calendar).YearValue, expectedYear);
                Assert.Equal(new Year(fiscalYearStart.AddTicks(1), calendar).YearValue, expectedYear);
                Assert.Equal(expectedYear - 1, new Year(fiscalYearStart.AddTicks(-1), calendar).YearValue);
            }
        } // FiscalYearStartMonthTest

        // ----------------------------------------------------------------------
        private ITimeCalendar GetFiscalYearCalendar(FiscalYearAlignment yearAlignment)
        {
            return new TimeCalendar(
                new TimeCalendarConfig
                {
                    YearType = YearType.FiscalYear,
                    YearBaseMonth = YearMonth.September,
                    FiscalFirstDayOfYear = DayOfWeek.Sunday,
                    FiscalYearAlignment = yearAlignment,
                    FiscalQuarterGrouping = FiscalQuarterGrouping.FourFourFiveWeeks
                });
        } // GetFiscalYearCalendar

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "Year")]
        [Fact]
        public void FiscalYearTest()
        {
            var year1 = new Year(2006, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            Assert.Equal(year1.Start.Date, new DateTime(2006, 8, 27));
            Assert.Equal(year1.End.Date, new DateTime(2007, 8, 25));

            var year2 = new Year(2006, GetFiscalYearCalendar(FiscalYearAlignment.NearestDay));
            Assert.Equal(year2.Start.Date, new DateTime(2006, 9, 3));
            Assert.Equal(year2.End.Date, new DateTime(2007, 9, 1));
        } // FiscalYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void GetFiscalHalfyearsTest()
        {
            var year = new Year(2006, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var halfyears = year.GetHalfyears();

            Assert.NotNull(halfyears);
            Assert.Equal(TimeSpec.HalfyearsPerYear, halfyears.Count);

            Assert.Equal(halfyears[0].Start.Date, year.Start);
            Assert.Equal(TimeSpec.FiscalDaysPerHalfyear, halfyears[0].Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
            Assert.Equal(halfyears[1].Start.Date, year.Start.AddDays(TimeSpec.FiscalDaysPerHalfyear));
            Assert.Equal(TimeSpec.FiscalDaysPerHalfyear, halfyears[1].Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
        } // GetFiscalHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
        public void GetFiscalQuartersTest()
        {
            var year = new Year(2006, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var quarters = year.GetQuarters();

            Assert.NotNull(quarters);
            Assert.Equal(TimeSpec.QuartersPerYear, quarters.Count);

            Assert.Equal(year.Start, quarters[0].Start.Date);
            Assert.Equal(year.End, quarters[TimeSpec.QuartersPerYear - 1].End);
        } // GetFiscalQuartersTest

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "Year")]
        [Fact]
        public void FiscalYearGetMonthsTest()
        {
            var year = new Year(2006, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var months = year.GetMonths();
            Assert.NotNull(months);
            Assert.Equal(TimeSpec.MonthsPerYear, months.Count);

            Assert.Equal(new DateTime(2006, 8, 27), months[0].Start);
            for (var i = 0; i < months.Count; i++)
            {
                Assert.Equal(months[i].Duration.Subtract(TimeCalendar.DefaultEndOffset).Days,
                    (i + 1) % 3 == 0 ? TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth);
            }
            Assert.Equal(months[TimeSpec.MonthsPerYear - 1].End, year.End);
        } // FiscalYearGetMonthsTest

    } // class YearTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
