// -- FILE ------------------------------------------------------------------
// name       : YearsTest.cs
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

    public sealed class YearsTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void YearBaseMonthTest()
        {
            var moment = new DateTime(2009, 2, 15);
            var year = TimeTool.GetYearOf(YearMonth.April, moment.Year, moment.Month);
            var years = new Years(moment, 3, TimeCalendar.New(YearMonth.April));
            Assert.Equal(YearMonth.April, years.YearBaseMonth);
            Assert.Equal(years.Start, new DateTime(year, (int)YearMonth.April, 1));
        } // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void SingleYearsTest()
        {
            const int startYear = 2004;
            var years = new Years(startYear, 1);

            Assert.Equal(1, years.YearCount);
            Assert.Equal(startYear, years.StartYear);
            Assert.Equal(startYear, years.EndYear);
            Assert.Single(years.GetYears());
            Assert.True(years.GetYears()[0].IsSamePeriod(new Year(startYear)));
        } // SingleYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void DefaultCalendarYearsTest()
        {
            const int startYear = 2004;
            const int yearCount = 3;
            var years = new Years(startYear, yearCount);

            Assert.Equal(yearCount, years.YearCount);
            Assert.Equal(startYear, years.StartYear);
            Assert.Equal(startYear + yearCount - 1, years.EndYear);

            var index = 0;
            foreach (var timePeriod in years.GetYears())
            {
                var year = (Year)timePeriod;
                Assert.True(year.IsSamePeriod(new Year(startYear + index)));
                index++;
            }
        } // DefaultCalendarYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void CustomCalendarYearsTest()
        {
            const int startYear = 2004;
            const int yearCount = 3;
            const int startMonth = 4;
            var years = new Years(startYear, yearCount, TimeCalendar.New((YearMonth)startMonth));

            Assert.Equal(yearCount, years.YearCount);
            Assert.Equal(startYear, years.StartYear);
            Assert.Equal(startYear + yearCount, years.EndYear);

            var index = 0;
            foreach (var timePeriod in years.GetYears())
            {
                var year = (Year)timePeriod;
                Assert.Equal(year.Start, new DateTime(startYear + index, startMonth, 1));
                index++;
            }
        } // CustomCalendarYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void GetMonthsTest()
        {
            const int startYear = 2004;
            const int yearCount = 10;
            const YearMonth startMonth = YearMonth.October;
            var years = new Years(startYear, yearCount, TimeCalendar.New(startMonth));

            var months = years.GetMonths();
            Assert.NotNull(months);

            var index = 0;
            foreach (var timePeriod in months)
            {
                var month = (Month)timePeriod;
                TimeTool.AddMonth(startYear, startMonth, index, out var monthYear, out _);
                Assert.Equal(month.Year, monthYear);
                Assert.Equal(month.Start, years.Start.AddMonths(index));
                Assert.Equal(month.End, month.Calendar.MapEnd(month.Start.AddMonths(1)));
                index++;
            }
            Assert.Equal(yearCount * TimeSpec.MonthsPerYear, index);
        } // GetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void GetHalfyearsTest()
        {
            const int startYear = 2004;
            const int yearCount = 10;
            const YearMonth startMonth = YearMonth.October;
            var years = new Years(startYear, yearCount, TimeCalendar.New(startMonth));

            var halfyears = years.GetHalfyears();
            Assert.NotNull(halfyears);

            var index = 0;
            foreach (var timePeriod in halfyears)
            {
                var halfyear = (Halfyear)timePeriod;
                var halfyearYear = startYear + (index / TimeSpec.HalfyearsPerYear);
                Assert.Equal(halfyear.Year, halfyearYear);
                Assert.Equal(halfyear.Start, years.Start.AddMonths(index * TimeSpec.MonthsPerHalfyear));
                Assert.Equal(halfyear.End, halfyear.Calendar.MapEnd(halfyear.Start.AddMonths(TimeSpec.MonthsPerHalfyear)));
                index++;
            }
            Assert.Equal(yearCount * TimeSpec.HalfyearsPerYear, index);
        } // GetHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void GetQuartersTest()
        {
            const int startYear = 2004;
            const int yearCount = 10;
            const YearMonth startMonth = YearMonth.October;
            const YearQuarter startQuarter = YearQuarter.Third;
            var years = new Years(startYear, yearCount, TimeCalendar.New(startMonth));

            var quarters = years.GetQuarters();
            Assert.NotNull(quarters);

            var index = 0;
            foreach (var timePeriod in quarters)
            {
                var quarter = (Quarter)timePeriod;
                var quarterYear = startYear + ((index + (int)startQuarter) / TimeSpec.QuartersPerYear);
                Assert.Equal(quarter.Year, quarterYear);
                Assert.Equal(quarter.Start, years.Start.AddMonths(index * TimeSpec.MonthsPerQuarter));
                Assert.Equal(quarter.End, quarter.Calendar.MapEnd(quarter.Start.AddMonths(TimeSpec.MonthsPerQuarter)));
                index++;
            }
            Assert.Equal(yearCount * TimeSpec.QuartersPerYear, index);
        } // GetQuartersTest

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
        [Trait("Category", "Years")]
        [Fact]
        public void FiscalYearsTest()
        {
            var years1 = new Years(2006, 13, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            Assert.Equal(13, years1.YearCount);
            Assert.Equal(years1.Start.Date, new DateTime(2006, 8, 27));
            Assert.Equal(years1.End.Date, new DateTime(2019, 8, 31));

            var years2 = new Years(2006, 13, GetFiscalYearCalendar(FiscalYearAlignment.NearestDay));
            Assert.Equal(13, years2.YearCount);
            Assert.Equal(years2.Start.Date, new DateTime(2006, 9, 3));
            Assert.Equal(years2.End.Date, new DateTime(2019, 8, 31));
        } // FiscalYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void FiscalYearsLastDayGetHalfyearsTest()
        {
            const int yearCount = 13;
            var years = new Years(2006, yearCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var halfyears = years.GetHalfyears();
            Assert.NotNull(halfyears);
            Assert.Equal(yearCount * TimeSpec.HalfyearsPerYear, halfyears.Count);

            Assert.Equal(halfyears[0].Start, new DateTime(2006, 8, 27));
            foreach (var timePeriod in halfyears)
            {
                var halfyear = (Halfyear)timePeriod;
                // last halfyear of a leap year
                // http://en.wikipedia.org/wiki/4-4-5_Calendar
                if ((halfyear.YearHalfyear == YearHalfyear.Second) && (halfyear.Year == 2008 || halfyear.Year == 2013 || halfyear.Year == 2019))
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerLeapHalfyear, halfyear.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
                else
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerHalfyear, halfyear.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
            }
        } // FiscalYearsLastDayGetHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void FiscalYearsNearestDayGetHalfyearsTest()
        {
            const int yearCount = 13;
            var years = new Years(2006, yearCount, GetFiscalYearCalendar(FiscalYearAlignment.NearestDay));
            var halfyears = years.GetHalfyears();
            Assert.NotNull(halfyears);
            Assert.Equal(yearCount * TimeSpec.HalfyearsPerYear, halfyears.Count);

            Assert.Equal(halfyears[0].Start, new DateTime(2006, 9, 3));
            foreach (var timePeriod in halfyears)
            {
                var halfyear = (Halfyear)timePeriod;
                // last halfyear of a leap year
                // http://en.wikipedia.org/wiki/4-4-5_Calendar
                if ((halfyear.YearHalfyear == YearHalfyear.Second) && (halfyear.Year == 2011 || halfyear.Year == 2016))
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerLeapHalfyear, halfyear.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
                else
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerHalfyear, halfyear.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
            }
        } // FiscalYearsNearestDayGetHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void FiscalYearsLastDayGetQuartersTest()
        {
            const int yearCount = 13;
            var years = new Years(2006, yearCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var quarters = years.GetQuarters();
            Assert.NotNull(quarters);
            Assert.Equal(yearCount * TimeSpec.QuartersPerYear, quarters.Count);

            Assert.Equal(quarters[0].Start, new DateTime(2006, 8, 27));
            foreach (var timePeriod in quarters)
            {
                var quarter = (Quarter)timePeriod;
                // last quarter of a leap year
                // http://en.wikipedia.org/wiki/4-4-5_Calendar
                if ((quarter.YearQuarter == YearQuarter.Fourth) && (quarter.Year == 2008 || quarter.Year == 2013 || quarter.Year == 2019))
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerLeapQuarter, quarter.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
                else
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerQuarter, quarter.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
            }
        } // FiscalYearsLastDayGetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void FiscalYearsNearestDayGetQuartersTest()
        {
            const int yearCount = 13;
            var years = new Years(2006, yearCount, GetFiscalYearCalendar(FiscalYearAlignment.NearestDay));
            var quarters = years.GetQuarters();
            Assert.NotNull(quarters);
            Assert.Equal(yearCount * TimeSpec.QuartersPerYear, quarters.Count);

            Assert.Equal(quarters[0].Start, new DateTime(2006, 9, 3));
            foreach (var timePeriod in quarters)
            {
                var quarter = (Quarter)timePeriod;
                // last quarter of a leap year
                // http://en.wikipedia.org/wiki/4-4-5_Calendar
                if ((quarter.YearQuarter == YearQuarter.Fourth) && (quarter.Year == 2011 || quarter.Year == 2016))
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerLeapQuarter, quarter.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
                else
                {
                    Assert.Equal(TimeSpec.FiscalDaysPerQuarter, quarter.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days);
                }
            }
        } // FiscalYearsNearestDayGetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void FiscalYearsLastDayGetMonthsTest()
        {
            const int yearCount = 13;
            var years = new Years(2006, yearCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var months = years.GetMonths();
            Assert.NotNull(months);
            Assert.Equal(yearCount * TimeSpec.MonthsPerYear, months.Count);

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
        } // FiscalYearsLastDayGetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
        public void FiscalYearsNearestDayGetMonthsTest()
        {
            const int yearCount = 13;
            var years = new Years(2006, yearCount, GetFiscalYearCalendar(FiscalYearAlignment.NearestDay));
            var months = years.GetMonths();
            Assert.NotNull(months);
            Assert.Equal(yearCount * TimeSpec.MonthsPerYear, months.Count);

            Assert.Equal(months[0].Start, new DateTime(2006, 9, 3));
            for (var i = 0; i < months.Count; i++)
            {
                var month = (Month)months[i];

                // last month of a leap year (6 weeks)
                // http://en.wikipedia.org/wiki/4-4-5_Calendar
                if ((month.YearMonth == YearMonth.August) && (month.Year == 2011 || month.Year == 2016))
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
        } // FiscalYearsNearestDayGetMonthsTest

    } // class YearsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
