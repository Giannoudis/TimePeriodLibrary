// -- FILE ------------------------------------------------------------------
// name       : TimeToolTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
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

    public sealed class TimeToolTest : TestUnitBase
    {

        #region Date and Time

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetDateTest()
        {
            Assert.Equal(TimeTool.GetDate(testDate).Year, testDate.Year);
            Assert.Equal(TimeTool.GetDate(testDate).Month, testDate.Month);
            Assert.Equal(TimeTool.GetDate(testDate).Day, testDate.Day);
            Assert.Equal(0, TimeTool.GetDate(testDate).Hour);
            Assert.Equal(0, TimeTool.GetDate(testDate).Minute);
            Assert.Equal(0, TimeTool.GetDate(testDate).Second);
            Assert.Equal(0, TimeTool.GetDate(testDate).Millisecond);
        } // GetDateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void SetDateTest()
        {
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate).Year, testDiffDate.Year);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate).Month, testDiffDate.Month);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate).Day, testDiffDate.Day);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate).Hour, testDate.Hour);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate).Minute, testDate.Minute);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate).Second, testDate.Second);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate).Millisecond, testDate.Millisecond);

            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day).Year, testDiffDate.Year);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day).Month, testDiffDate.Month);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day).Day, testDiffDate.Day);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day).Hour, testDate.Hour);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day).Minute, testDate.Minute);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day).Second, testDate.Second);
            Assert.Equal(TimeTool.SetDate(testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day).Millisecond, testDate.Millisecond);
        } // SetDateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void HasTimeOfDayTest()
        {
            var now = ClockProxy.Clock.Now;
            Assert.False(TimeTool.HasTimeOfDay(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0)));
            Assert.True(TimeTool.HasTimeOfDay(new DateTime(now.Year, now.Month, now.Day, 1, 0, 0, 0)));
            Assert.True(TimeTool.HasTimeOfDay(new DateTime(now.Year, now.Month, now.Day, 0, 1, 0, 0)));
            Assert.True(TimeTool.HasTimeOfDay(new DateTime(now.Year, now.Month, now.Day, 0, 0, 1, 0)));
            Assert.True(TimeTool.HasTimeOfDay(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 1)));
        } // HasTimeOfDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void SetTimeOfDayTest()
        {
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate).Year, testDate.Year);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate).Month, testDate.Month);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate).Day, testDate.Day);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate).Hour, testDiffDate.Hour);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate).Minute, testDiffDate.Minute);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate).Second, testDiffDate.Second);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate).Millisecond, testDiffDate.Millisecond);

            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond).Year, testDate.Year);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond).Month, testDate.Month);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond).Day, testDate.Day);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond).Hour, testDiffDate.Hour);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond).Minute, testDiffDate.Minute);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond).Second, testDiffDate.Second);
            Assert.Equal(TimeTool.SetTimeOfDay(testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond).Millisecond, testDiffDate.Millisecond);
        } // SetTimeOfDayTest

        #endregion

        #region Year

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetYearOfTest()
        {
            Assert.Equal(2000, TimeTool.GetYearOf(YearMonth.January, new DateTime(2000, 1, 1)));
            Assert.Equal(2000, TimeTool.GetYearOf(YearMonth.April, new DateTime(2000, 4, 1)));
            Assert.Equal(2000, TimeTool.GetYearOf(YearMonth.April, new DateTime(2001, 3, 31)));
            Assert.Equal(1999, TimeTool.GetYearOf(YearMonth.April, new DateTime(2000, 3, 31)));
        } // GetYearOfTest

        #endregion

        #region Halfyear

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void NextHalfyearTest()
        {
            TimeTool.NextHalfyear(YearHalfyear.First, out _, out var halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);

            TimeTool.NextHalfyear(YearHalfyear.Second, out _, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);
        } // NextHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void PreviousHalfyearTest()
        {
            TimeTool.PreviousHalfyear(YearHalfyear.First, out _, out var halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);

            TimeTool.PreviousHalfyear(YearHalfyear.Second, out _, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);
        } // PreviousHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void AddHalfyearTest()
        {
            TimeTool.AddHalfyear(YearHalfyear.First, 1, out var year, out var halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.First, -1, out year, out halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.Second, 1, out year, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.Second, -1, out year, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);

            TimeTool.AddHalfyear(YearHalfyear.First, 2, out year, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.First, -2, out year, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.Second, 2, out year, out halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.Second, -2, out year, out halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);

            TimeTool.AddHalfyear(YearHalfyear.First, 5, out year, out halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.First, -5, out year, out halfyear);
            Assert.Equal(YearHalfyear.Second, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.Second, 5, out year, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);
            TimeTool.AddHalfyear(YearHalfyear.Second, -5, out year, out halfyear);
            Assert.Equal(YearHalfyear.First, halfyear);

            TimeTool.AddHalfyear(2008, YearHalfyear.First, 1, out year, out halfyear);
            Assert.Equal(2008, year);
            Assert.Equal(YearHalfyear.Second, halfyear);
            TimeTool.AddHalfyear(2008, YearHalfyear.Second, 1, out year, out halfyear);
            Assert.Equal(2009, year);
            Assert.Equal(YearHalfyear.First, halfyear);

            TimeTool.AddHalfyear(2008, YearHalfyear.First, 2, out year, out halfyear);
            Assert.Equal(2009, year);
            Assert.Equal(YearHalfyear.First, halfyear);
            TimeTool.AddHalfyear(2008, YearHalfyear.Second, 2, out year, out halfyear);
            Assert.Equal(2009, year);
            Assert.Equal(YearHalfyear.Second, halfyear);

            TimeTool.AddHalfyear(2008, YearHalfyear.First, 3, out year, out halfyear);
            Assert.Equal(2009, year);
            Assert.Equal(YearHalfyear.Second, halfyear);
            TimeTool.AddHalfyear(2008, YearHalfyear.Second, 3, out year, out halfyear);
            Assert.Equal(2010, year);
            Assert.Equal(YearHalfyear.First, halfyear);
        } // AddHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetCalendarHalfyearOfMonthTest()
        {
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.January));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.February));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.March));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.April));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.May));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.June));

            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.July));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.August));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.September));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.October));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.November));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.December));
        } // GetCalendarHalfyearOfMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetHalfyearOfMonthTest()
        {
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.October));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.November));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.December));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.January));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.February));
            Assert.Equal(YearHalfyear.First, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.March));

            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.April));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.May));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.June));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.July));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.August));
            Assert.Equal(YearHalfyear.Second, TimeTool.GetHalfyearOfMonth(YearMonth.October, YearMonth.September));
        } // GetHalfyearOfMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetMonthsOfHalfyearTest()
        {
            Assert.Equal(TimeTool.GetMonthsOfHalfyear(YearHalfyear.First), TimeSpec.FirstHalfyearMonths);
            Assert.Equal(TimeTool.GetMonthsOfHalfyear(YearHalfyear.Second), TimeSpec.SecondHalfyearMonths);
        } // GetMonthsOfQuarterTest

        #endregion

        #region Quarter

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void NextQuarterTest()
        {
            TimeTool.NextQuarter(YearQuarter.First, out _, out var quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.NextQuarter(YearQuarter.Second, out _, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.NextQuarter(YearQuarter.Third, out _, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.NextQuarter(YearQuarter.Fourth, out _, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
        } // NextQuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void PreviousQuarterTest()
        {
            TimeTool.PreviousQuarter(YearQuarter.First, out _, out var quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.PreviousQuarter(YearQuarter.Second, out _, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.PreviousQuarter(YearQuarter.Third, out _, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.PreviousQuarter(YearQuarter.Fourth, out _, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
        } // PreviousQuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void AddQuarterTest()
        {
            TimeTool.AddQuarter(YearQuarter.First, 1, out var year, out var quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, 1, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, 1, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, 1, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);

            TimeTool.AddQuarter(YearQuarter.First, -1, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, -1, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, -1, out year, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, -1, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);

            TimeTool.AddQuarter(YearQuarter.First, 2, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, 2, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, 2, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, 2, out year, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);

            TimeTool.AddQuarter(YearQuarter.First, -2, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, -2, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, -2, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, -2, out year, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);

            TimeTool.AddQuarter(YearQuarter.First, 3, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, 3, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, 3, out year, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, 3, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);

            TimeTool.AddQuarter(YearQuarter.First, -3, out year, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, -3, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, -3, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, -3, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);

            TimeTool.AddQuarter(YearQuarter.First, 4, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, 4, out year, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, 4, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, 4, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);

            TimeTool.AddQuarter(YearQuarter.First, -4, out year, out quarter);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.AddQuarter(YearQuarter.Second, -4, out year, out quarter);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(YearQuarter.Third, -4, out year, out quarter);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(YearQuarter.Fourth, -4, out year, out quarter);
            Assert.Equal(YearQuarter.Fourth, quarter);

            TimeTool.AddQuarter(2008, YearQuarter.First, 1, out year, out quarter);
            Assert.Equal(2008, year);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Second, 1, out year, out quarter);
            Assert.Equal(2008, year);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Third, 1, out year, out quarter);
            Assert.Equal(2008, year);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Fourth, 1, out year, out quarter);
            Assert.Equal(2009, year);
            Assert.Equal(YearQuarter.First, quarter);

            TimeTool.AddQuarter(2008, YearQuarter.First, 2, out year, out quarter);
            Assert.Equal(2008, year);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Second, 2, out year, out quarter);
            Assert.Equal(2008, year);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Third, 2, out year, out quarter);
            Assert.Equal(2009, year);
            Assert.Equal(YearQuarter.First, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Fourth, 2, out year, out quarter);
            Assert.Equal(2009, year);
            Assert.Equal(YearQuarter.Second, quarter);

            TimeTool.AddQuarter(2008, YearQuarter.First, 5, out year, out quarter);
            Assert.Equal(2009, year);
            Assert.Equal(YearQuarter.Second, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Second, 5, out year, out quarter);
            Assert.Equal(2009, year);
            Assert.Equal(YearQuarter.Third, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Third, 5, out year, out quarter);
            Assert.Equal(2009, year);
            Assert.Equal(YearQuarter.Fourth, quarter);
            TimeTool.AddQuarter(2008, YearQuarter.Fourth, 5, out year, out quarter);
            Assert.Equal(2010, year);
            Assert.Equal(YearQuarter.First, quarter);
        } // AddQuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetCalendarQuarterOfMonthTest()
        {
            Assert.Equal(YearQuarter.First, TimeTool.GetQuarterOfMonth(YearMonth.January));
            Assert.Equal(YearQuarter.First, TimeTool.GetQuarterOfMonth(YearMonth.February));
            Assert.Equal(YearQuarter.First, TimeTool.GetQuarterOfMonth(YearMonth.March));

            Assert.Equal(YearQuarter.Second, TimeTool.GetQuarterOfMonth(YearMonth.April));
            Assert.Equal(YearQuarter.Second, TimeTool.GetQuarterOfMonth(YearMonth.May));
            Assert.Equal(YearQuarter.Second, TimeTool.GetQuarterOfMonth(YearMonth.June));

            Assert.Equal(YearQuarter.Third, TimeTool.GetQuarterOfMonth(YearMonth.July));
            Assert.Equal(YearQuarter.Third, TimeTool.GetQuarterOfMonth(YearMonth.August));
            Assert.Equal(YearQuarter.Third, TimeTool.GetQuarterOfMonth(YearMonth.September));

            Assert.Equal(YearQuarter.Fourth, TimeTool.GetQuarterOfMonth(YearMonth.October));
            Assert.Equal(YearQuarter.Fourth, TimeTool.GetQuarterOfMonth(YearMonth.November));
            Assert.Equal(YearQuarter.Fourth, TimeTool.GetQuarterOfMonth(YearMonth.December));
        } // GetCalendarQuarterOfMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetQuarterOfMonthTest()
        {
            Assert.Equal(YearQuarter.First, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.October));
            Assert.Equal(YearQuarter.First, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.November));
            Assert.Equal(YearQuarter.First, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.December));

            Assert.Equal(YearQuarter.Second, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.January));
            Assert.Equal(YearQuarter.Second, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.February));
            Assert.Equal(YearQuarter.Second, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.March));

            Assert.Equal(YearQuarter.Third, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.April));
            Assert.Equal(YearQuarter.Third, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.May));
            Assert.Equal(YearQuarter.Third, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.June));

            Assert.Equal(YearQuarter.Fourth, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.July));
            Assert.Equal(YearQuarter.Fourth, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.August));
            Assert.Equal(YearQuarter.Fourth, TimeTool.GetQuarterOfMonth(YearMonth.October, YearMonth.September));
        } // GetQuarterOfMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void GetMonthsOfQuarterTest()
        {
            Assert.Equal(TimeTool.GetMonthsOfQuarter(YearQuarter.First), TimeSpec.FirstQuarterMonths);
            Assert.Equal(TimeTool.GetMonthsOfQuarter(YearQuarter.Second), TimeSpec.SecondQuarterMonths);
            Assert.Equal(TimeTool.GetMonthsOfQuarter(YearQuarter.Third), TimeSpec.ThirdQuarterMonths);
            Assert.Equal(TimeTool.GetMonthsOfQuarter(YearQuarter.Fourth), TimeSpec.FourthQuarterMonths);
        } // GetMonthsOfQuarterTest

        #endregion

        #region Month

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void NextMonthTest()
        {
            TimeTool.NextMonth(YearMonth.January, out _, out var month);
            Assert.Equal(YearMonth.February, month);
            TimeTool.NextMonth(YearMonth.February, out _, out month);
            Assert.Equal(YearMonth.March, month);
            TimeTool.NextMonth(YearMonth.March, out _, out month);
            Assert.Equal(YearMonth.April, month);
            TimeTool.NextMonth(YearMonth.April, out _, out month);
            Assert.Equal(YearMonth.May, month);
            TimeTool.NextMonth(YearMonth.May, out _, out month);
            Assert.Equal(YearMonth.June, month);
            TimeTool.NextMonth(YearMonth.June, out _, out month);
            Assert.Equal(YearMonth.July, month);
            TimeTool.NextMonth(YearMonth.July, out _, out month);
            Assert.Equal(YearMonth.August, month);
            TimeTool.NextMonth(YearMonth.August, out _, out month);
            Assert.Equal(YearMonth.September, month);
            TimeTool.NextMonth(YearMonth.September, out _, out month);
            Assert.Equal(YearMonth.October, month);
            TimeTool.NextMonth(YearMonth.October, out _, out month);
            Assert.Equal(YearMonth.November, month);
            TimeTool.NextMonth(YearMonth.November, out _, out month);
            Assert.Equal(YearMonth.December, month);
            TimeTool.NextMonth(YearMonth.December, out _, out month);
            Assert.Equal(YearMonth.January, month);
        } // NextMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void PreviousMonthTest()
        {
            TimeTool.PreviousMonth(YearMonth.January, out _, out var month);
            Assert.Equal(YearMonth.December, month);
            TimeTool.PreviousMonth(YearMonth.February, out _, out month);
            Assert.Equal(YearMonth.January, month);
            TimeTool.PreviousMonth(YearMonth.March, out _, out month);
            Assert.Equal(YearMonth.February, month);
            TimeTool.PreviousMonth(YearMonth.April, out _, out month);
            Assert.Equal(YearMonth.March, month);
            TimeTool.PreviousMonth(YearMonth.May, out _, out month);
            Assert.Equal(YearMonth.April, month);
            TimeTool.PreviousMonth(YearMonth.June, out _, out month);
            Assert.Equal(YearMonth.May, month);
            TimeTool.PreviousMonth(YearMonth.July, out _, out month);
            Assert.Equal(YearMonth.June, month);
            TimeTool.PreviousMonth(YearMonth.August, out _, out month);
            Assert.Equal(YearMonth.July, month);
            TimeTool.PreviousMonth(YearMonth.September, out _, out month);
            Assert.Equal(YearMonth.August, month);
            TimeTool.PreviousMonth(YearMonth.October, out _, out month);
            Assert.Equal(YearMonth.September, month);
            TimeTool.PreviousMonth(YearMonth.November, out _, out month);
            Assert.Equal(YearMonth.October, month);
            TimeTool.PreviousMonth(YearMonth.December, out _, out month);
            Assert.Equal(YearMonth.November, month);
        } // PreviousMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void AddMonthsTest()
        {
            TimeTool.AddMonth(YearMonth.January, 1, out var year, out var month);
            Assert.Equal(YearMonth.February, month);
            TimeTool.AddMonth(YearMonth.February, 1, out year, out month);
            Assert.Equal(YearMonth.March, month);
            TimeTool.AddMonth(YearMonth.March, 1, out year, out month);
            Assert.Equal(YearMonth.April, month);
            TimeTool.AddMonth(YearMonth.April, 1, out year, out month);
            Assert.Equal(YearMonth.May, month);
            TimeTool.AddMonth(YearMonth.May, 1, out year, out month);
            Assert.Equal(YearMonth.June, month);
            TimeTool.AddMonth(YearMonth.June, 1, out year, out month);
            Assert.Equal(YearMonth.July, month);
            TimeTool.AddMonth(YearMonth.July, 1, out year, out month);
            Assert.Equal(YearMonth.August, month);
            TimeTool.AddMonth(YearMonth.August, 1, out year, out month);
            Assert.Equal(YearMonth.September, month);
            TimeTool.AddMonth(YearMonth.September, 1, out year, out month);
            Assert.Equal(YearMonth.October, month);
            TimeTool.AddMonth(YearMonth.October, 1, out year, out month);
            Assert.Equal(YearMonth.November, month);
            TimeTool.AddMonth(YearMonth.November, 1, out year, out month);
            Assert.Equal(YearMonth.December, month);
            TimeTool.AddMonth(YearMonth.December, 1, out year, out month);
            Assert.Equal(YearMonth.January, month);

            TimeTool.AddMonth(YearMonth.January, -1, out year, out month);
            Assert.Equal(YearMonth.December, month);
            TimeTool.AddMonth(YearMonth.February, -1, out year, out month);
            Assert.Equal(YearMonth.January, month);
            TimeTool.AddMonth(YearMonth.March, -1, out year, out month);
            Assert.Equal(YearMonth.February, month);
            TimeTool.AddMonth(YearMonth.April, -1, out year, out month);
            Assert.Equal(YearMonth.March, month);
            TimeTool.AddMonth(YearMonth.May, -1, out year, out month);
            Assert.Equal(YearMonth.April, month);
            TimeTool.AddMonth(YearMonth.June, -1, out year, out month);
            Assert.Equal(YearMonth.May, month);
            TimeTool.AddMonth(YearMonth.July, -1, out year, out month);
            Assert.Equal(YearMonth.June, month);
            TimeTool.AddMonth(YearMonth.August, -1, out year, out month);
            Assert.Equal(YearMonth.July, month);
            TimeTool.AddMonth(YearMonth.September, -1, out year, out month);
            Assert.Equal(YearMonth.August, month);
            TimeTool.AddMonth(YearMonth.October, -1, out year, out month);
            Assert.Equal(YearMonth.September, month);
            TimeTool.AddMonth(YearMonth.November, -1, out year, out month);
            Assert.Equal(YearMonth.October, month);
            TimeTool.AddMonth(YearMonth.December, -1, out year, out month);
            Assert.Equal(YearMonth.November, month);

            for (var i = -36; i <= 36; i += 36)
            {

                TimeTool.AddMonth(YearMonth.January, i, out year, out month);
                Assert.Equal(YearMonth.January, month);
                TimeTool.AddMonth(YearMonth.February, i, out year, out month);
                Assert.Equal(YearMonth.February, month);
                TimeTool.AddMonth(YearMonth.March, i, out year, out month);
                Assert.Equal(YearMonth.March, month);
                TimeTool.AddMonth(YearMonth.April, i, out year, out month);
                Assert.Equal(YearMonth.April, month);
                TimeTool.AddMonth(YearMonth.May, i, out year, out month);
                Assert.Equal(YearMonth.May, month);
                TimeTool.AddMonth(YearMonth.June, i, out year, out month);
                Assert.Equal(YearMonth.June, month);
                TimeTool.AddMonth(YearMonth.July, i, out year, out month);
                Assert.Equal(YearMonth.July, month);
                TimeTool.AddMonth(YearMonth.August, i, out year, out month);
                Assert.Equal(YearMonth.August, month);
                TimeTool.AddMonth(YearMonth.September, i, out year, out month);
                Assert.Equal(YearMonth.September, month);
                TimeTool.AddMonth(YearMonth.October, i, out year, out month);
                Assert.Equal(YearMonth.October, month);
                TimeTool.AddMonth(YearMonth.November, i, out year, out month);
                Assert.Equal(YearMonth.November, month);
                TimeTool.AddMonth(YearMonth.December, i, out year, out month);
                Assert.Equal(YearMonth.December, month);
            }

            for (var i = 1; i < (3 * TimeSpec.MonthsPerYear); i++)
            {
                TimeTool.AddMonth(2008, (YearMonth)i, 1, out year, out month);
                Assert.Equal(year, 2008 + (i / 12));
                Assert.Equal(month, (YearMonth)((i % TimeSpec.MonthsPerYear) + 1));
            }
        } // AddMonthsTest

        #endregion

        #region Week

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void WeekOfYearCalendarTest()
        {
            var moment = new DateTime(2007, 12, 31);
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                var calendarWeekOfYear = culture.Calendar.GetWeekOfYear(
                    moment,
                    culture.DateTimeFormat.CalendarWeekRule,
                    culture.DateTimeFormat.FirstDayOfWeek);
                TimeTool.GetWeekOfYear(moment, culture, YearWeekType.Calendar, out _, out var weekOfYear);
                Assert.Equal(weekOfYear, calendarWeekOfYear);
            }
        } // WeekOfYearCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void WeekOfYearIsoTest()
        {
            var moment = new DateTime(2007, 12, 31);
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                if (culture.DateTimeFormat.CalendarWeekRule != CalendarWeekRule.FirstFourDayWeek ||
                         culture.DateTimeFormat.FirstDayOfWeek != DayOfWeek.Monday)
                {
                    continue;
                }

                TimeTool.GetWeekOfYear(moment, culture, YearWeekType.Iso8601, out _, out var weekOfYear);
                Assert.Equal(1, weekOfYear);
            }
        } // WeekOfYearIsoTest

        #endregion

        #region Day

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void DayStartTest()
        {
            Assert.Equal(TimeTool.DayStart(testDate).Year, testDate.Year);
            Assert.Equal(TimeTool.DayStart(testDate).Month, testDate.Month);
            Assert.Equal(TimeTool.DayStart(testDate).Day, testDate.Day);
            Assert.Equal(0, TimeTool.DayStart(testDate).Hour);
            Assert.Equal(0, TimeTool.DayStart(testDate).Minute);
            Assert.Equal(0, TimeTool.DayStart(testDate).Second);
            Assert.Equal(0, TimeTool.DayStart(testDate).Millisecond);
        } // DayStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void NextDayTest()
        {
            Assert.Equal(DayOfWeek.Tuesday, TimeTool.NextDay(DayOfWeek.Monday));
            Assert.Equal(DayOfWeek.Wednesday, TimeTool.NextDay(DayOfWeek.Tuesday));
            Assert.Equal(DayOfWeek.Thursday, TimeTool.NextDay(DayOfWeek.Wednesday));
            Assert.Equal(DayOfWeek.Friday, TimeTool.NextDay(DayOfWeek.Thursday));
            Assert.Equal(DayOfWeek.Saturday, TimeTool.NextDay(DayOfWeek.Friday));
            Assert.Equal(DayOfWeek.Sunday, TimeTool.NextDay(DayOfWeek.Saturday));
            Assert.Equal(DayOfWeek.Monday, TimeTool.NextDay(DayOfWeek.Sunday));
        } // NextDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void PreviousDayTest()
        {
            Assert.Equal(DayOfWeek.Sunday, TimeTool.PreviousDay(DayOfWeek.Monday));
            Assert.Equal(DayOfWeek.Monday, TimeTool.PreviousDay(DayOfWeek.Tuesday));
            Assert.Equal(DayOfWeek.Tuesday, TimeTool.PreviousDay(DayOfWeek.Wednesday));
            Assert.Equal(DayOfWeek.Wednesday, TimeTool.PreviousDay(DayOfWeek.Thursday));
            Assert.Equal(DayOfWeek.Thursday, TimeTool.PreviousDay(DayOfWeek.Friday));
            Assert.Equal(DayOfWeek.Friday, TimeTool.PreviousDay(DayOfWeek.Saturday));
            Assert.Equal(DayOfWeek.Saturday, TimeTool.PreviousDay(DayOfWeek.Sunday));
        } // PreviousDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeTool")]
        [Fact]
        public void AddDaysTest()
        {
            Assert.Equal(DayOfWeek.Monday, TimeTool.AddDays(DayOfWeek.Sunday, 1));
            Assert.Equal(DayOfWeek.Tuesday, TimeTool.AddDays(DayOfWeek.Monday, 1));
            Assert.Equal(DayOfWeek.Wednesday, TimeTool.AddDays(DayOfWeek.Tuesday, 1));
            Assert.Equal(DayOfWeek.Thursday, TimeTool.AddDays(DayOfWeek.Wednesday, 1));
            Assert.Equal(DayOfWeek.Friday, TimeTool.AddDays(DayOfWeek.Thursday, 1));
            Assert.Equal(DayOfWeek.Saturday, TimeTool.AddDays(DayOfWeek.Friday, 1));
            Assert.Equal(DayOfWeek.Sunday, TimeTool.AddDays(DayOfWeek.Saturday, 1));

            Assert.Equal(DayOfWeek.Sunday, TimeTool.AddDays(DayOfWeek.Monday, -1));
            Assert.Equal(DayOfWeek.Monday, TimeTool.AddDays(DayOfWeek.Tuesday, -1));
            Assert.Equal(DayOfWeek.Tuesday, TimeTool.AddDays(DayOfWeek.Wednesday, -1));
            Assert.Equal(DayOfWeek.Wednesday, TimeTool.AddDays(DayOfWeek.Thursday, -1));
            Assert.Equal(DayOfWeek.Thursday, TimeTool.AddDays(DayOfWeek.Friday, -1));
            Assert.Equal(DayOfWeek.Friday, TimeTool.AddDays(DayOfWeek.Saturday, -1));
            Assert.Equal(DayOfWeek.Saturday, TimeTool.AddDays(DayOfWeek.Sunday, -1));

            Assert.Equal(DayOfWeek.Sunday, TimeTool.AddDays(DayOfWeek.Sunday, 14));
            Assert.Equal(DayOfWeek.Monday, TimeTool.AddDays(DayOfWeek.Monday, 14));
            Assert.Equal(DayOfWeek.Tuesday, TimeTool.AddDays(DayOfWeek.Tuesday, 14));
            Assert.Equal(DayOfWeek.Wednesday, TimeTool.AddDays(DayOfWeek.Wednesday, 14));
            Assert.Equal(DayOfWeek.Thursday, TimeTool.AddDays(DayOfWeek.Thursday, 14));
            Assert.Equal(DayOfWeek.Friday, TimeTool.AddDays(DayOfWeek.Friday, 14));
            Assert.Equal(DayOfWeek.Saturday, TimeTool.AddDays(DayOfWeek.Saturday, 14));

            Assert.Equal(DayOfWeek.Sunday, TimeTool.AddDays(DayOfWeek.Sunday, -14));
            Assert.Equal(DayOfWeek.Monday, TimeTool.AddDays(DayOfWeek.Monday, -14));
            Assert.Equal(DayOfWeek.Tuesday, TimeTool.AddDays(DayOfWeek.Tuesday, -14));
            Assert.Equal(DayOfWeek.Wednesday, TimeTool.AddDays(DayOfWeek.Wednesday, -14));
            Assert.Equal(DayOfWeek.Thursday, TimeTool.AddDays(DayOfWeek.Thursday, -14));
            Assert.Equal(DayOfWeek.Friday, TimeTool.AddDays(DayOfWeek.Friday, -14));
            Assert.Equal(DayOfWeek.Saturday, TimeTool.AddDays(DayOfWeek.Saturday, -14));
        } // AddDaysTest

        #endregion

        // ----------------------------------------------------------------------
        // members
        private readonly DateTime testDate = new(2000, 10, 2, 13, 45, 53, 673);
        private readonly DateTime testDiffDate = new(2002, 9, 3, 7, 14, 22, 234);

    } // class TimeToolTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
