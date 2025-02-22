// -- FILE ------------------------------------------------------------------
// name       : QuartersTest.cs
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

    public sealed class QuartersTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void YearBaseMonthTest()
        {
            var moment = new DateTime(2009, 2, 15);
            var year = TimeTool.GetYearOf(YearMonth.April, moment.Year, moment.Month);
            var quarters = new Quarters(moment, YearQuarter.First, 3, TimeCalendar.New(YearMonth.April));
            Assert.Equal(YearMonth.April, quarters.YearBaseMonth);
            Assert.Equal(quarters.Start, new DateTime(year, (int)YearMonth.April, 1));
        } // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void SingleQuartersTest()
        {
            const int startYear = 2004;
            const YearQuarter startQuarter = YearQuarter.Second;
            var quarters = new Quarters(startYear, startQuarter, 1);

            Assert.Equal(YearMonth.January, quarters.YearBaseMonth);
            Assert.Equal(1, quarters.QuarterCount);
            Assert.Equal(startQuarter, quarters.StartQuarter);
            Assert.Equal(startYear, quarters.StartYear);
            Assert.Equal(startYear, quarters.EndYear);
            Assert.Equal(YearQuarter.Second, quarters.EndQuarter);
            Assert.Single(quarters.GetQuarters());
            Assert.True(quarters.GetQuarters()[0].IsSamePeriod(new Quarter(2004, YearQuarter.Second)));
        } // SingleQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void QuartersGetMonthsTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;

            var q2and3 = new Quarters(currentYear, YearQuarter.Second, 2);

            var months = q2and3.GetMonths();
            Assert.Equal(6, months.Count);
            Assert.Equal(months[0].Start, new DateTime(currentYear, TimeSpec.SecondQuarterMonthIndex, 1));
            Assert.Equal(months[1].Start, new DateTime(currentYear, TimeSpec.SecondQuarterMonthIndex, 1).AddMonths(1));
            Assert.Equal(months[2].Start, new DateTime(currentYear, TimeSpec.SecondQuarterMonthIndex, 1).AddMonths(2));
            Assert.Equal(months[3].Start, new DateTime(currentYear, TimeSpec.ThirdQuarterMonthIndex, 1));
            Assert.Equal(months[4].Start, new DateTime(currentYear, TimeSpec.ThirdQuarterMonthIndex, 1).AddMonths(1));
            Assert.Equal(months[5].Start, new DateTime(currentYear, TimeSpec.ThirdQuarterMonthIndex, 1).AddMonths(2));
        }

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void FirstCalendarQuartersTest()
        {
            const int startYear = 2004;
            const YearQuarter startQuarter = YearQuarter.First;
            const int quarterCount = 5;
            var quarters = new Quarters(startYear, startQuarter, quarterCount);

            Assert.Equal(YearMonth.January, quarters.YearBaseMonth);
            Assert.Equal(quarterCount, quarters.QuarterCount);
            Assert.Equal(startQuarter, quarters.StartQuarter);
            Assert.Equal(startYear, quarters.StartYear);
            Assert.Equal(2005, quarters.EndYear);
            Assert.Equal(YearQuarter.First, quarters.EndQuarter);
            Assert.Equal(quarterCount, quarters.GetQuarters().Count);
            Assert.True(quarters.GetQuarters()[0].IsSamePeriod(new Quarter(2004, YearQuarter.First)));
            Assert.True(quarters.GetQuarters()[1].IsSamePeriod(new Quarter(2004, YearQuarter.Second)));
            Assert.True(quarters.GetQuarters()[2].IsSamePeriod(new Quarter(2004, YearQuarter.Third)));
            Assert.True(quarters.GetQuarters()[3].IsSamePeriod(new Quarter(2004, YearQuarter.Fourth)));
            Assert.True(quarters.GetQuarters()[4].IsSamePeriod(new Quarter(2005, YearQuarter.First)));
        } // FirstCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void SecondCalendarQuartersTest()
        {
            const int startYear = 2004;
            const YearQuarter startQuarter = YearQuarter.Second;
            const int quarterCount = 5;
            var quarters = new Quarters(startYear, startQuarter, quarterCount);

            Assert.Equal(YearMonth.January, quarters.YearBaseMonth);
            Assert.Equal(quarterCount, quarters.QuarterCount);
            Assert.Equal(startQuarter, quarters.StartQuarter);
            Assert.Equal(startYear, quarters.StartYear);
            Assert.Equal(2005, quarters.EndYear);
            Assert.Equal(YearQuarter.Second, quarters.EndQuarter);
            Assert.Equal(quarterCount, quarters.GetQuarters().Count);
            Assert.True(quarters.GetQuarters()[0].IsSamePeriod(new Quarter(2004, YearQuarter.Second)));
            Assert.True(quarters.GetQuarters()[1].IsSamePeriod(new Quarter(2004, YearQuarter.Third)));
            Assert.True(quarters.GetQuarters()[2].IsSamePeriod(new Quarter(2004, YearQuarter.Fourth)));
            Assert.True(quarters.GetQuarters()[3].IsSamePeriod(new Quarter(2005, YearQuarter.First)));
            Assert.True(quarters.GetQuarters()[4].IsSamePeriod(new Quarter(2005, YearQuarter.Second)));
        } // SecondCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void FirstCustomCalendarQuartersTest()
        {
            var calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearQuarter startQuarter = YearQuarter.First;
            const int quarterCount = 5;
            var quarters = new Quarters(startYear, startQuarter, quarterCount, calendar);

            Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
            Assert.Equal(quarterCount, quarters.QuarterCount);
            Assert.Equal(startQuarter, quarters.StartQuarter);
            Assert.Equal(startYear, quarters.StartYear);
            Assert.Equal(2005, quarters.EndYear);
            Assert.Equal(YearQuarter.First, quarters.EndQuarter);
            Assert.Equal(quarterCount, quarters.GetQuarters().Count);
            Assert.True(quarters.GetQuarters()[0].IsSamePeriod(new Quarter(2004, YearQuarter.First, calendar)));
            Assert.True(quarters.GetQuarters()[1].IsSamePeriod(new Quarter(2004, YearQuarter.Second, calendar)));
            Assert.True(quarters.GetQuarters()[2].IsSamePeriod(new Quarter(2004, YearQuarter.Third, calendar)));
            Assert.True(quarters.GetQuarters()[3].IsSamePeriod(new Quarter(2004, YearQuarter.Fourth, calendar)));
            Assert.True(quarters.GetQuarters()[4].IsSamePeriod(new Quarter(2005, YearQuarter.First, calendar)));
        } // FirstCustomCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void SecondCustomCalendarQuartersTest()
        {
            var calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearQuarter startQuarter = YearQuarter.Second;
            const int quarterCount = 5;
            var quarters = new Quarters(startYear, startQuarter, quarterCount, calendar);

            Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
            Assert.Equal(quarterCount, quarters.QuarterCount);
            Assert.Equal(startQuarter, quarters.StartQuarter);
            Assert.Equal(startYear, quarters.StartYear);
            Assert.Equal(2005, quarters.EndYear);
            Assert.Equal(YearQuarter.Second, quarters.EndQuarter);
            Assert.Equal(quarterCount, quarters.GetQuarters().Count);
            Assert.True(quarters.GetQuarters()[0].IsSamePeriod(new Quarter(2004, YearQuarter.Second, calendar)));
            Assert.True(quarters.GetQuarters()[1].IsSamePeriod(new Quarter(2004, YearQuarter.Third, calendar)));
            Assert.True(quarters.GetQuarters()[2].IsSamePeriod(new Quarter(2004, YearQuarter.Fourth, calendar)));
            Assert.True(quarters.GetQuarters()[3].IsSamePeriod(new Quarter(2005, YearQuarter.First, calendar)));
            Assert.True(quarters.GetQuarters()[4].IsSamePeriod(new Quarter(2005, YearQuarter.Second, calendar)));
        } // SecondCustomCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void ThirdCustomCalendarQuartersTest()
        {
            var calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearQuarter startQuarter = YearQuarter.Third;
            const int quarterCount = 5;
            var quarters = new Quarters(startYear, startQuarter, quarterCount, calendar);

            Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
            Assert.Equal(quarterCount, quarters.QuarterCount);
            Assert.Equal(startQuarter, quarters.StartQuarter);
            Assert.Equal(startYear, quarters.StartYear);
            Assert.Equal(2005, quarters.EndYear);
            Assert.Equal(YearQuarter.Third, quarters.EndQuarter);
            Assert.Equal(quarterCount, quarters.GetQuarters().Count);
            Assert.True(quarters.GetQuarters()[0].IsSamePeriod(new Quarter(2004, YearQuarter.Third, calendar)));
            Assert.True(quarters.GetQuarters()[1].IsSamePeriod(new Quarter(2004, YearQuarter.Fourth, calendar)));
            Assert.True(quarters.GetQuarters()[2].IsSamePeriod(new Quarter(2005, YearQuarter.First, calendar)));
            Assert.True(quarters.GetQuarters()[3].IsSamePeriod(new Quarter(2005, YearQuarter.Second, calendar)));
            Assert.True(quarters.GetQuarters()[4].IsSamePeriod(new Quarter(2005, YearQuarter.Third, calendar)));
        } // ThirdCustomCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
        public void FourthCustomCalendarQuartersTest()
        {
            var calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearQuarter startQuarter = YearQuarter.Fourth;
            const int quarterCount = 5;
            var quarters = new Quarters(startYear, startQuarter, quarterCount, calendar);

            Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
            Assert.Equal(quarterCount, quarters.QuarterCount);
            Assert.Equal(startQuarter, quarters.StartQuarter);
            Assert.Equal(startYear, quarters.StartYear);
            Assert.Equal(2005, quarters.EndYear);
            Assert.Equal(YearQuarter.Fourth, quarters.EndQuarter);
            Assert.Equal(quarterCount, quarters.GetQuarters().Count);
            Assert.True(quarters.GetQuarters()[0].IsSamePeriod(new Quarter(2004, YearQuarter.Fourth, calendar)));
            Assert.True(quarters.GetQuarters()[1].IsSamePeriod(new Quarter(2005, YearQuarter.First, calendar)));
            Assert.True(quarters.GetQuarters()[2].IsSamePeriod(new Quarter(2005, YearQuarter.Second, calendar)));
            Assert.True(quarters.GetQuarters()[3].IsSamePeriod(new Quarter(2005, YearQuarter.Third, calendar)));
            Assert.True(quarters.GetQuarters()[4].IsSamePeriod(new Quarter(2005, YearQuarter.Fourth, calendar)));
        } // FourthCustomCalendarQuartersTest

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
        [Trait("Category", "Quarters")]
        [Fact]
        public void FiscalYearGetMonthsTest()
        {
            const int quarterCount = 8;
            var halfyears = new Quarters(2006, YearQuarter.First, quarterCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var months = halfyears.GetMonths();
            Assert.NotNull(months);
            Assert.Equal(TimeSpec.MonthsPerQuarter * quarterCount, months.Count);

            Assert.Equal(months[0].Start, new DateTime(2006, 8, 27));
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
            Assert.Equal(months[(TimeSpec.MonthsPerQuarter * quarterCount) - 1].End, halfyears.End);
        } // FiscalYearGetMonthsTest

    } // class QuartersTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
