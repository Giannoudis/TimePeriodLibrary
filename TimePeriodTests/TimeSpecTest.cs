// -- FILE ------------------------------------------------------------------
// name       : TimeSpecTest.cs
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

    public sealed class TimeSpecTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeSpec")]
        [Fact]
        public void WeeksPerTimeSpecTest()
        {
            // relations
            Assert.Equal(12, TimeSpec.MonthsPerYear);
            Assert.Equal(2, TimeSpec.HalfyearsPerYear);
            Assert.Equal(4, TimeSpec.QuartersPerYear);
            Assert.Equal(TimeSpec.QuartersPerHalfyear, TimeSpec.QuartersPerYear / TimeSpec.HalfyearsPerYear);
            Assert.Equal(53, TimeSpec.MaxWeeksPerYear);
            Assert.Equal(TimeSpec.MonthsPerHalfyear, TimeSpec.MonthsPerYear / TimeSpec.HalfyearsPerYear);
            Assert.Equal(TimeSpec.MonthsPerQuarter, TimeSpec.MonthsPerYear / TimeSpec.QuartersPerYear);
            Assert.Equal(31, TimeSpec.MaxDaysPerMonth);
            Assert.Equal(7, TimeSpec.DaysPerWeek);
            Assert.Equal(24, TimeSpec.HoursPerDay);
            Assert.Equal(60, TimeSpec.MinutesPerHour);
            Assert.Equal(60, TimeSpec.SecondsPerMinute);
            Assert.Equal(1000, TimeSpec.MillisecondsPerSecond);

            // halfyear
            Assert.Equal(TimeSpec.MonthsPerHalfyear, TimeSpec.FirstHalfyearMonths.Length);
            Assert.Equal(YearMonth.January, TimeSpec.FirstHalfyearMonths[0]);
            Assert.Equal(YearMonth.February, TimeSpec.FirstHalfyearMonths[1]);
            Assert.Equal(YearMonth.March, TimeSpec.FirstHalfyearMonths[2]);
            Assert.Equal(YearMonth.April, TimeSpec.FirstHalfyearMonths[3]);
            Assert.Equal(YearMonth.May, TimeSpec.FirstHalfyearMonths[4]);
            Assert.Equal(YearMonth.June, TimeSpec.FirstHalfyearMonths[5]);

            Assert.Equal(TimeSpec.MonthsPerHalfyear, TimeSpec.SecondHalfyearMonths.Length);
            Assert.Equal(YearMonth.July, TimeSpec.SecondHalfyearMonths[0]);
            Assert.Equal(YearMonth.August, TimeSpec.SecondHalfyearMonths[1]);
            Assert.Equal(YearMonth.September, TimeSpec.SecondHalfyearMonths[2]);
            Assert.Equal(YearMonth.October, TimeSpec.SecondHalfyearMonths[3]);
            Assert.Equal(YearMonth.November, TimeSpec.SecondHalfyearMonths[4]);
            Assert.Equal(YearMonth.December, TimeSpec.SecondHalfyearMonths[5]);

            // quarter
            Assert.Equal(1, TimeSpec.FirstQuarterMonthIndex);
            Assert.Equal(TimeSpec.SecondQuarterMonthIndex, TimeSpec.FirstQuarterMonthIndex + TimeSpec.MonthsPerQuarter);
            Assert.Equal(TimeSpec.ThirdQuarterMonthIndex, TimeSpec.SecondQuarterMonthIndex + TimeSpec.MonthsPerQuarter);
            Assert.Equal(TimeSpec.FourthQuarterMonthIndex, TimeSpec.ThirdQuarterMonthIndex + TimeSpec.MonthsPerQuarter);

            Assert.Equal(TimeSpec.MonthsPerQuarter, TimeSpec.FirstQuarterMonths.Length);
            Assert.Equal(YearMonth.January, TimeSpec.FirstQuarterMonths[0]);
            Assert.Equal(YearMonth.February, TimeSpec.FirstQuarterMonths[1]);
            Assert.Equal(YearMonth.March, TimeSpec.FirstQuarterMonths[2]);

            Assert.Equal(TimeSpec.MonthsPerQuarter, TimeSpec.SecondQuarterMonths.Length);
            Assert.Equal(YearMonth.April, TimeSpec.SecondQuarterMonths[0]);
            Assert.Equal(YearMonth.May, TimeSpec.SecondQuarterMonths[1]);
            Assert.Equal(YearMonth.June, TimeSpec.SecondQuarterMonths[2]);

            Assert.Equal(TimeSpec.MonthsPerQuarter, TimeSpec.ThirdQuarterMonths.Length);
            Assert.Equal(YearMonth.July, TimeSpec.ThirdQuarterMonths[0]);
            Assert.Equal(YearMonth.August, TimeSpec.ThirdQuarterMonths[1]);
            Assert.Equal(YearMonth.September, TimeSpec.ThirdQuarterMonths[2]);

            Assert.Equal(TimeSpec.MonthsPerQuarter, TimeSpec.FourthQuarterMonths.Length);
            Assert.Equal(YearMonth.October, TimeSpec.FourthQuarterMonths[0]);
            Assert.Equal(YearMonth.November, TimeSpec.FourthQuarterMonths[1]);
            Assert.Equal(YearMonth.December, TimeSpec.FourthQuarterMonths[2]);

            // duration
            Assert.Equal(TimeSpec.NoDuration, TimeSpan.Zero);
            Assert.Equal(TimeSpec.MinPositiveDuration, new TimeSpan(1));
            Assert.Equal(TimeSpec.MinNegativeDuration, new TimeSpan(-1));

            // period
            Assert.Equal(TimeSpec.MinPeriodDate, DateTime.MinValue);
            Assert.Equal(TimeSpec.MaxPeriodDate, DateTime.MaxValue);
            Assert.Equal(TimeSpec.MinPeriodDuration, TimeSpan.Zero);
            Assert.Equal(TimeSpec.MaxPeriodDuration, TimeSpec.MaxPeriodDate - TimeSpec.MinPeriodDate);

        } // WeeksPerTimeSpecTest

    } // class TimeSpecTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
