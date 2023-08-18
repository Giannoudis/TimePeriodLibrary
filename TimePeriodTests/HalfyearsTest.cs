// -- FILE ------------------------------------------------------------------
// name       : HalfyearsTest.cs
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

    public sealed class HalfyearsTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void YearBaseMonthTest()
        {
            var moment = new DateTime(2009, 2, 15);
            var year = TimeTool.GetYearOf(YearMonth.April, moment.Year, moment.Month);
            var halfyears = new Halfyears(moment, YearHalfyear.First, 3, TimeCalendar.New(YearMonth.April));
            Assert.Equal(YearMonth.April, halfyears.YearBaseMonth);
            Assert.Equal(halfyears.Start, new DateTime(year, (int)YearMonth.April, 1));
        } // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void SingleHalfyearsTest()
        {
            const int startYear = 2004;
            const YearHalfyear startHalfyear = YearHalfyear.Second;
            var halfyears = new Halfyears(startYear, startHalfyear, 1);

            Assert.Equal(YearMonth.January, halfyears.YearBaseMonth);
            Assert.Equal(1, halfyears.HalfyearCount);
            Assert.Equal(startHalfyear, halfyears.StartHalfyear);
            Assert.Equal(startYear, halfyears.StartYear);
            Assert.Equal(startYear, halfyears.EndYear);
            Assert.Equal(YearHalfyear.Second, halfyears.EndHalfyear);
            Assert.Single(halfyears.GetHalfyears());
            Assert.True(halfyears.GetHalfyears()[0].IsSamePeriod(new Halfyear(2004, YearHalfyear.Second)));
        } // SingleHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void FirstCalendarHalfyearsTest()
        {
            const int startYear = 2004;
            const YearHalfyear startHalfyear = YearHalfyear.First;
            const int halfyearCount = 3;
            var halfyears = new Halfyears(startYear, startHalfyear, halfyearCount);

            Assert.Equal(YearMonth.January, halfyears.YearBaseMonth);
            Assert.Equal(halfyearCount, halfyears.HalfyearCount);
            Assert.Equal(startHalfyear, halfyears.StartHalfyear);
            Assert.Equal(startYear, halfyears.StartYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfyear.First, halfyears.EndHalfyear);
            Assert.Equal(halfyearCount, halfyears.GetHalfyears().Count);
            Assert.True(halfyears.GetHalfyears()[0].IsSamePeriod(new Halfyear(2004, YearHalfyear.First)));
            Assert.True(halfyears.GetHalfyears()[1].IsSamePeriod(new Halfyear(2004, YearHalfyear.Second)));
            Assert.True(halfyears.GetHalfyears()[2].IsSamePeriod(new Halfyear(2005, YearHalfyear.First)));
        } // FirstCalendarHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void SecondCalendarHalfyearsTest()
        {
            const int startYear = 2004;
            const YearHalfyear startHalfyear = YearHalfyear.Second;
            const int halfyearCount = 3;
            var halfyears = new Halfyears(startYear, startHalfyear, halfyearCount);

            Assert.Equal(YearMonth.January, halfyears.YearBaseMonth);
            Assert.Equal(halfyearCount, halfyears.HalfyearCount);
            Assert.Equal(startHalfyear, halfyears.StartHalfyear);
            Assert.Equal(startYear, halfyears.StartYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfyear.Second, halfyears.EndHalfyear);
            Assert.Equal(halfyearCount, halfyears.GetHalfyears().Count);
            Assert.True(halfyears.GetHalfyears()[0].IsSamePeriod(new Halfyear(2004, YearHalfyear.Second)));
            Assert.True(halfyears.GetHalfyears()[1].IsSamePeriod(new Halfyear(2005, YearHalfyear.First)));
            Assert.True(halfyears.GetHalfyears()[2].IsSamePeriod(new Halfyear(2005, YearHalfyear.Second)));
        } // SecondCalendarHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void FirstCustomCalendarHalfyearsTest()
        {
            var calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearHalfyear startHalfyear = YearHalfyear.First;
            const int halfyearCount = 3;
            var halfyears = new Halfyears(startYear, startHalfyear, halfyearCount, calendar);

            Assert.Equal(YearMonth.October, halfyears.YearBaseMonth);
            Assert.Equal(halfyearCount, halfyears.HalfyearCount);
            Assert.Equal(startHalfyear, halfyears.StartHalfyear);
            Assert.Equal(startYear, halfyears.StartYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfyear.First, halfyears.EndHalfyear);
            Assert.Equal(halfyearCount, halfyears.GetHalfyears().Count);
            Assert.True(halfyears.GetHalfyears()[0].IsSamePeriod(new Halfyear(2004, YearHalfyear.First, calendar)));
            Assert.True(halfyears.GetHalfyears()[1].IsSamePeriod(new Halfyear(2004, YearHalfyear.Second, calendar)));
            Assert.True(halfyears.GetHalfyears()[2].IsSamePeriod(new Halfyear(2005, YearHalfyear.First, calendar)));
        } // FirstCustomCalendarHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void SecondCustomCalendarHalfyearsTest()
        {
            var calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearHalfyear startHalfyear = YearHalfyear.Second;
            const int halfyearCount = 3;
            var halfyears = new Halfyears(startYear, startHalfyear, halfyearCount, calendar);

            Assert.Equal(YearMonth.October, halfyears.YearBaseMonth);
            Assert.Equal(halfyearCount, halfyears.HalfyearCount);
            Assert.Equal(startHalfyear, halfyears.StartHalfyear);
            Assert.Equal(startYear, halfyears.StartYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfyear.Second, halfyears.EndHalfyear);
            Assert.Equal(halfyearCount, halfyears.GetHalfyears().Count);
            Assert.True(halfyears.GetHalfyears()[0].IsSamePeriod(new Halfyear(2004, YearHalfyear.Second, calendar)));
            Assert.True(halfyears.GetHalfyears()[1].IsSamePeriod(new Halfyear(2005, YearHalfyear.First, calendar)));
            Assert.True(halfyears.GetHalfyears()[2].IsSamePeriod(new Halfyear(2005, YearHalfyear.Second, calendar)));
        } // SecondCustomCalendarHalfyearsTest

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
        [Trait("Category", "Halfyears")]
        [Fact]
        public void GetFiscalQuartersTest()
        {
            const int halfyearCount = 4;
            var halfyears = new Halfyears(2006, YearHalfyear.First, halfyearCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var quarters = halfyears.GetQuarters();

            Assert.NotNull(quarters);
            Assert.Equal(TimeSpec.QuartersPerHalfyear * halfyearCount, quarters.Count);

            Assert.Equal(quarters[0].Start.Date, halfyears.Start);
            Assert.Equal(quarters[(TimeSpec.QuartersPerHalfyear * halfyearCount) - 1].End, halfyears.End);
        } // GetFiscalQuartersTest

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "Halfyears")]
        [Fact]
        public void FiscalYearGetMonthsTest()
        {
            const int halfyearCount = 4;
            var halfyears = new Halfyears(2006, YearHalfyear.First, halfyearCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var months = halfyears.GetMonths();
            Assert.NotNull(months);
            Assert.Equal(TimeSpec.MonthsPerHalfyear * halfyearCount, months.Count);

            Assert.Equal(new DateTime(2006, 8, 27), months[0].Start);
            for (var i = 0; i < months.Count; i++)
            {
                var month = (Month)months[i];

                // last month of a leap year (6 weeks)
                // http://en.wikipedia.org/wiki/4-4-5_Calendar
                if ((month.YearMonth == YearMonth.August) && (month.Year == 2008 || month.Year == 2013 || month.Year == 2019))
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerLeapMonth, month.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
                else if ((i + 1) % 3 == 0) // first and second month of quarter (4 weeks)
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerLongMonth, month.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
                else // third month of quarter (5 weeks)
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerShortMonth, month.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
            }
            Assert.Equal(halfyears.End, months[(TimeSpec.MonthsPerHalfyear * halfyearCount) - 1].End);
        } // FiscalYearGetMonthsTest


        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void FirstHalfyearMonthTest()
        {
            var halfyears = new Halfyears(2016, YearHalfyear.First, 1);
            Assert.Equal(6, halfyears.GetMonths().Count);
            Assert.Equal(1, halfyears.GetMonths()[0].Start.Month);
            Assert.Equal(6, halfyears.GetMonths()[5].Start.Month);
        } // FirstHalfyearMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void SecondHalfyearMonthTest()
        {
            var halfyears = new Halfyears(2016, YearHalfyear.Second, 1);
            Assert.Equal(6, halfyears.GetMonths().Count);
            Assert.Equal(7, halfyears.GetMonths()[0].Start.Month);
            Assert.Equal(12, halfyears.GetMonths()[5].Start.Month);
        } // SecondHalfyearMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void FirstHalfyearQuarterTest()
        {
            var halfyears = new Halfyears(2016, YearHalfyear.First, 1);
            Assert.Equal(2, halfyears.GetQuarters().Count);
            Assert.Equal(1, halfyears.GetQuarters()[0].Start.Month);
            Assert.Equal(4, halfyears.GetQuarters()[1].Start.Month);
        } // FirstHalfyearQuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyears")]
        [Fact]
        public void SecondHalfyearQuarterTest()
        {
            var halfyears = new Halfyears(2016, YearHalfyear.Second, 1);
            Assert.Equal(2, halfyears.GetQuarters().Count);
            Assert.Equal(7, halfyears.GetQuarters()[0].Start.Month);
            Assert.Equal(10, halfyears.GetQuarters()[1].Start.Month);
        } // SecondHalfyearQuarterTest

    } // class HalfyearsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
