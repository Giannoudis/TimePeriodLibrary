// -- FILE ------------------------------------------------------------------
// name       : DateDiffTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.22
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

    public sealed class DateDiffTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void DefaultsTest()
        {
            var test = new DateTime(2008, 10, 12, 15, 32, 44, 243);

            var dateDiff = new DateDiff(test, test);

            Assert.Equal(TimeSpec.CalendarYearStartMonth, dateDiff.YearBaseMonth);
            Assert.Equal(DateDiff.SafeCurrentInfo.FirstDayOfWeek, dateDiff.FirstDayOfWeek);
        } // DefaultsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void EmptyDateDiffTest()
        {
            var test = new DateTime(2008, 10, 12, 15, 32, 44, 243);

            var dateDiff = new DateDiff(test, test);

            Assert.True(dateDiff.IsEmpty);
            Assert.Equal(dateDiff.Difference, TimeSpan.Zero);
            Assert.Equal(0, dateDiff.Years);
            Assert.Equal(0, dateDiff.Quarters);
            Assert.Equal(0, dateDiff.Months);
            Assert.Equal(0, dateDiff.Weeks);
            Assert.Equal(0, dateDiff.Days);
            Assert.Equal(0, dateDiff.Hours);
            Assert.Equal(0, dateDiff.Minutes);
            Assert.Equal(0, dateDiff.Seconds);

            Assert.Equal(0, dateDiff.ElapsedYears);
            Assert.Equal(0, dateDiff.ElapsedMonths);
            Assert.Equal(0, dateDiff.ElapsedDays);
            Assert.Equal(0, dateDiff.ElapsedHours);
            Assert.Equal(0, dateDiff.ElapsedMinutes);
            Assert.Equal(0, dateDiff.ElapsedSeconds);
        } // EmptyDateDiffTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void DifferenceTest()
        {
            var date1 = new DateTime(2008, 10, 12, 15, 32, 44, 243);
            var date2 = new DateTime(2010, 1, 3, 23, 22, 9, 345);

            var dateDiff = new DateDiff(date1, date2);

            Assert.Equal(dateDiff.Difference, date2.Subtract(date1));
        } // DifferenceTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void YearsTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddYears(1);
            var date3 = date1.AddYears(-1);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(1, dateDiff12.ElapsedYears);
            Assert.Equal(0, dateDiff12.ElapsedMonths);
            Assert.Equal(0, dateDiff12.ElapsedDays);
            Assert.Equal(0, dateDiff12.ElapsedHours);
            Assert.Equal(0, dateDiff12.ElapsedMinutes);
            Assert.Equal(0, dateDiff12.ElapsedSeconds);

            Assert.Equal(1, dateDiff12.Years);
            Assert.Equal(TimeSpec.QuartersPerYear, dateDiff12.Quarters);
            Assert.Equal(TimeSpec.MonthsPerYear, dateDiff12.Months);
            Assert.Equal(52, dateDiff12.Weeks);
            Assert.Equal(365, dateDiff12.Days);
            Assert.Equal(365 * TimeSpec.HoursPerDay, dateDiff12.Hours);
            Assert.Equal(365 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour, dateDiff12.Minutes);
            Assert.Equal(365 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute, dateDiff12.Seconds);

            var dateDiff13 = new DateDiff(date1, date3);
            Assert.Equal(-1, dateDiff13.ElapsedYears);
            Assert.Equal(0, dateDiff13.ElapsedMonths);
            Assert.Equal(0, dateDiff13.ElapsedDays);
            Assert.Equal(0, dateDiff13.ElapsedHours);
            Assert.Equal(0, dateDiff13.ElapsedMinutes);
            Assert.Equal(0, dateDiff13.ElapsedSeconds);

            Assert.Equal(-1, dateDiff13.Years);
            Assert.Equal(TimeSpec.QuartersPerYear * -1, dateDiff13.Quarters);
            Assert.Equal(TimeSpec.MonthsPerYear * -1, dateDiff13.Months);
            Assert.Equal(-52, dateDiff13.Weeks);
            Assert.Equal(-366, dateDiff13.Days);
            Assert.Equal(366 * TimeSpec.HoursPerDay * -1, dateDiff13.Hours);
            Assert.Equal(366 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1, dateDiff13.Minutes);
            Assert.Equal(366 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1, dateDiff13.Seconds);
        } // YearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void LeayYearTest()
        {
            var dateBefore1 = new DateTime(2011, 2, 27);
            var dateBefore2 = new DateTime(2011, 2, 28);
            var dateBefore3 = new DateTime(2011, 3, 01);

            var leapDateBefore = new DateTime(2008, 2, 29);
            var dateTest = new DateTime(2012, 2, 29);
            var leapDateAfter = new DateTime(2016, 2, 29);

            var dateAfter1 = new DateTime(2013, 2, 27);
            var dateAfter2 = new DateTime(2013, 2, 28);
            var dateAfter3 = new DateTime(2013, 3, 01);

            Assert.Equal(1, new DateDiff(dateBefore1, dateTest).Years);
            Assert.Equal(-1, new DateDiff(dateTest, dateBefore1).Years);
            Assert.Equal(1, new DateDiff(dateBefore2, dateTest).Years);
            Assert.Equal(-1, new DateDiff(dateTest, dateBefore2).Years);
            Assert.Equal(0, new DateDiff(dateBefore3, dateTest).Years);

            Assert.Equal(4, new DateDiff(leapDateBefore, dateTest).Years);
            Assert.Equal(-4, new DateDiff(dateTest, leapDateBefore).Years);
            Assert.Equal(4, new DateDiff(dateTest, leapDateAfter).Years);
            Assert.Equal(-4, new DateDiff(leapDateAfter, dateTest).Years);

            Assert.Equal(0, new DateDiff(dateTest, dateAfter1).Years);
            Assert.Equal(0, new DateDiff(dateTest, dateAfter2).Years);
            Assert.Equal(-1, new DateDiff(dateAfter2, dateTest).Years);
            Assert.Equal(1, new DateDiff(dateTest, dateAfter3).Years);
            Assert.Equal(-1, new DateDiff(dateAfter3, dateTest).Years);
        } // LeayYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void QuartersTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddMonths(3);
            var date3 = date1.AddMonths(-3);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff12.ElapsedYears);
            Assert.Equal(3, dateDiff12.ElapsedMonths);
            Assert.Equal(0, dateDiff12.ElapsedDays);
            Assert.Equal(0, dateDiff12.ElapsedHours);
            Assert.Equal(0, dateDiff12.ElapsedMinutes);
            Assert.Equal(0, dateDiff12.ElapsedSeconds);

            Assert.Equal(0, dateDiff12.Years);
            Assert.Equal(1, dateDiff12.Quarters);
            Assert.Equal(TimeSpec.MonthsPerQuarter, dateDiff12.Months);
            Assert.Equal(13, dateDiff12.Weeks);
            Assert.Equal(92, dateDiff12.Days);
            Assert.Equal(92 * TimeSpec.HoursPerDay, dateDiff12.Hours);
            Assert.Equal(92 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour, dateDiff12.Minutes);
            Assert.Equal(92 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute, dateDiff12.Seconds);

            DateDiff dateDiff13 = new(date1, date3);
            Assert.Equal(0, dateDiff13.ElapsedYears);
            Assert.Equal(-3, dateDiff13.ElapsedMonths);
            Assert.Equal(0, dateDiff13.ElapsedDays);
            Assert.Equal(0, dateDiff13.ElapsedHours);
            Assert.Equal(0, dateDiff13.ElapsedMinutes);
            Assert.Equal(0, dateDiff13.ElapsedSeconds);

            Assert.Equal(0, dateDiff13.Years);
            Assert.Equal(-1, dateDiff13.Quarters);
            Assert.Equal(-TimeSpec.MonthsPerQuarter, dateDiff13.Months);
            Assert.Equal(-13, dateDiff13.Weeks);
            Assert.Equal(-90, dateDiff13.Days);
            Assert.Equal(90 * TimeSpec.HoursPerDay * -1, dateDiff13.Hours);
            Assert.Equal(90 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1, dateDiff13.Minutes);
            Assert.Equal(90 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1, dateDiff13.Seconds);
        } // YearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void MonthsTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddMonths(1);
            var date3 = date1.AddMonths(-1);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff12.ElapsedYears);
            Assert.Equal(1, dateDiff12.ElapsedMonths);
            Assert.Equal(0, dateDiff12.ElapsedDays);
            Assert.Equal(0, dateDiff12.ElapsedHours);
            Assert.Equal(0, dateDiff12.ElapsedMinutes);
            Assert.Equal(0, dateDiff12.ElapsedSeconds);

            Assert.Equal(0, dateDiff12.Years);
            Assert.Equal(0, dateDiff12.Quarters);
            Assert.Equal(1, dateDiff12.Months);
            Assert.Equal(4, dateDiff12.Weeks);
            Assert.Equal(31, dateDiff12.Days);
            Assert.Equal(31 * TimeSpec.HoursPerDay, dateDiff12.Hours);
            Assert.Equal(31 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour, dateDiff12.Minutes);
            Assert.Equal(31 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute, dateDiff12.Seconds);

            var dateDiff13 = new DateDiff(date1, date3);
            Assert.Equal(0, dateDiff13.ElapsedYears);
            Assert.Equal(-1, dateDiff13.ElapsedMonths);
            Assert.Equal(0, dateDiff13.ElapsedDays);
            Assert.Equal(0, dateDiff13.ElapsedHours);
            Assert.Equal(0, dateDiff13.ElapsedMinutes);
            Assert.Equal(0, dateDiff13.ElapsedSeconds);

            Assert.Equal(0, dateDiff13.Years);
            Assert.Equal(0, dateDiff13.Quarters);
            Assert.Equal(-1, dateDiff13.Months);
            Assert.Equal(-4, dateDiff13.Weeks);
            Assert.Equal(30 * -1, dateDiff13.Days);
            Assert.Equal(30 * TimeSpec.HoursPerDay * -1, dateDiff13.Hours);
            Assert.Equal(30 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1, dateDiff13.Minutes);
            Assert.Equal(30 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1, dateDiff13.Seconds);
        } // MonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void WeeksTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddDays(TimeSpec.DaysPerWeek);
            var date3 = date1.AddDays(-TimeSpec.DaysPerWeek);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff12.Years);
            Assert.Equal(0, dateDiff12.Quarters);
            Assert.Equal(0, dateDiff12.Months);
            Assert.Equal(1, dateDiff12.Weeks);
            Assert.Equal(TimeSpec.DaysPerWeek, dateDiff12.Days);
            Assert.Equal(TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay, dateDiff12.Hours);
            Assert.Equal(TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour, dateDiff12.Minutes);
            Assert.Equal(TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute, dateDiff12.Seconds);

            var dateDiff13 = new DateDiff(date1, date3);
            Assert.Equal(0, dateDiff13.Years);
            Assert.Equal(0, dateDiff13.Quarters);
            Assert.Equal(0, dateDiff13.Months);
            Assert.Equal(-1, dateDiff13.Weeks);
            Assert.Equal(TimeSpec.DaysPerWeek * -1, dateDiff13.Days);
            Assert.Equal(TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * -1, dateDiff13.Hours);
            Assert.Equal(TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1, dateDiff13.Minutes);
            Assert.Equal(TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1, dateDiff13.Seconds);
        } // WeeksTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void DaysTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddDays(1);
            var date3 = date1.AddDays(-1);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff12.ElapsedYears);
            Assert.Equal(0, dateDiff12.ElapsedMonths);
            Assert.Equal(1, dateDiff12.ElapsedDays);
            Assert.Equal(0, dateDiff12.ElapsedHours);
            Assert.Equal(0, dateDiff12.ElapsedMinutes);
            Assert.Equal(0, dateDiff12.ElapsedSeconds);

            Assert.Equal(0, dateDiff12.Years);
            Assert.Equal(0, dateDiff12.Quarters);
            Assert.Equal(0, dateDiff12.Months);
            Assert.Equal(0, dateDiff12.Weeks);
            Assert.Equal(1, dateDiff12.Days);
            Assert.Equal(TimeSpec.HoursPerDay, dateDiff12.Hours);
            Assert.Equal(TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour, dateDiff12.Minutes);
            Assert.Equal(TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute, dateDiff12.Seconds);

            var dateDiff13 = new DateDiff(date1, date3);
            Assert.Equal(0, dateDiff13.ElapsedYears);
            Assert.Equal(0, dateDiff13.ElapsedMonths);
            Assert.Equal(-1, dateDiff13.ElapsedDays);
            Assert.Equal(0, dateDiff13.ElapsedHours);
            Assert.Equal(0, dateDiff13.ElapsedMinutes);
            Assert.Equal(0, dateDiff13.ElapsedSeconds);

            Assert.Equal(0, dateDiff13.Years);
            Assert.Equal(0, dateDiff13.Quarters);
            Assert.Equal(0, dateDiff13.Months);
            Assert.Equal(0, dateDiff13.Weeks);
            Assert.Equal(-1, dateDiff13.Days);
            Assert.Equal(-TimeSpec.HoursPerDay, dateDiff13.Hours);
            Assert.Equal(TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1, dateDiff13.Minutes);
            Assert.Equal(TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1, dateDiff13.Seconds);
        } // DaysTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void HoursTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddHours(1);
            var date3 = date1.AddHours(-1);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff12.ElapsedYears);
            Assert.Equal(0, dateDiff12.ElapsedMonths);
            Assert.Equal(0, dateDiff12.ElapsedDays);
            Assert.Equal(1, dateDiff12.ElapsedHours);
            Assert.Equal(0, dateDiff12.ElapsedMinutes);
            Assert.Equal(0, dateDiff12.ElapsedSeconds);

            Assert.Equal(0, dateDiff12.Years);
            Assert.Equal(0, dateDiff12.Quarters);
            Assert.Equal(0, dateDiff12.Months);
            Assert.Equal(0, dateDiff12.Weeks);
            Assert.Equal(0, dateDiff12.Days);
            Assert.Equal(1, dateDiff12.Hours);
            Assert.Equal(TimeSpec.MinutesPerHour, dateDiff12.Minutes);
            Assert.Equal(TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute, dateDiff12.Seconds);

            var dateDiff13 = new DateDiff(date1, date3);
            Assert.Equal(0, dateDiff13.ElapsedYears);
            Assert.Equal(0, dateDiff13.ElapsedMonths);
            Assert.Equal(0, dateDiff13.ElapsedDays);
            Assert.Equal(-1, dateDiff13.ElapsedHours);
            Assert.Equal(0, dateDiff13.ElapsedMinutes);
            Assert.Equal(0, dateDiff13.ElapsedSeconds);

            Assert.Equal(0, dateDiff13.Years);
            Assert.Equal(0, dateDiff13.Quarters);
            Assert.Equal(0, dateDiff13.Months);
            Assert.Equal(0, dateDiff13.Weeks);
            Assert.Equal(0, dateDiff13.Days);
            Assert.Equal(-1, dateDiff13.Hours);
            Assert.Equal(TimeSpec.MinutesPerHour * -1, dateDiff13.Minutes);
            Assert.Equal(TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1, dateDiff13.Seconds);
        } // HoursTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void MinutesTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddMinutes(1);
            var date3 = date1.AddMinutes(-1);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff12.ElapsedYears);
            Assert.Equal(0, dateDiff12.ElapsedMonths);
            Assert.Equal(0, dateDiff12.ElapsedDays);
            Assert.Equal(0, dateDiff12.ElapsedHours);
            Assert.Equal(1, dateDiff12.ElapsedMinutes);
            Assert.Equal(0, dateDiff12.ElapsedSeconds);

            Assert.Equal(0, dateDiff12.Years);
            Assert.Equal(0, dateDiff12.Quarters);
            Assert.Equal(0, dateDiff12.Months);
            Assert.Equal(0, dateDiff12.Weeks);
            Assert.Equal(0, dateDiff12.Days);
            Assert.Equal(0, dateDiff12.Hours);
            Assert.Equal(1, dateDiff12.Minutes);
            Assert.Equal(TimeSpec.SecondsPerMinute, dateDiff12.Seconds);

            var dateDiff13 = new DateDiff(date1, date3);
            Assert.Equal(0, dateDiff13.ElapsedYears);
            Assert.Equal(0, dateDiff13.ElapsedMonths);
            Assert.Equal(0, dateDiff13.ElapsedDays);
            Assert.Equal(0, dateDiff13.ElapsedHours);
            Assert.Equal(-1, dateDiff13.ElapsedMinutes);
            Assert.Equal(0, dateDiff13.ElapsedSeconds);

            Assert.Equal(0, dateDiff13.Years);
            Assert.Equal(0, dateDiff13.Quarters);
            Assert.Equal(0, dateDiff13.Months);
            Assert.Equal(0, dateDiff13.Weeks);
            Assert.Equal(0, dateDiff13.Days);
            Assert.Equal(0, dateDiff13.Hours);
            Assert.Equal(-1, dateDiff13.Minutes);
            Assert.Equal(TimeSpec.SecondsPerMinute * -1, dateDiff13.Seconds);
        } // MinutesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void SecondsTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddSeconds(1);
            var date3 = date1.AddSeconds(-1);

            var dateDiff12 = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff12.ElapsedYears);
            Assert.Equal(0, dateDiff12.ElapsedMonths);
            Assert.Equal(0, dateDiff12.ElapsedDays);
            Assert.Equal(0, dateDiff12.ElapsedHours);
            Assert.Equal(0, dateDiff12.ElapsedMinutes);
            Assert.Equal(1, dateDiff12.ElapsedSeconds);

            Assert.Equal(0, dateDiff12.Years);
            Assert.Equal(0, dateDiff12.Quarters);
            Assert.Equal(0, dateDiff12.Months);
            Assert.Equal(0, dateDiff12.Weeks);
            Assert.Equal(0, dateDiff12.Days);
            Assert.Equal(0, dateDiff12.Hours);
            Assert.Equal(0, dateDiff12.Minutes);
            Assert.Equal(1, dateDiff12.Seconds);

            var dateDiff13 = new DateDiff(date1, date3);
            Assert.Equal(0, dateDiff13.ElapsedYears);
            Assert.Equal(0, dateDiff13.ElapsedMonths);
            Assert.Equal(0, dateDiff13.ElapsedDays);
            Assert.Equal(0, dateDiff13.ElapsedHours);
            Assert.Equal(0, dateDiff13.ElapsedMinutes);
            Assert.Equal(-1, dateDiff13.ElapsedSeconds);

            Assert.Equal(0, dateDiff13.Years);
            Assert.Equal(0, dateDiff13.Quarters);
            Assert.Equal(0, dateDiff13.Months);
            Assert.Equal(0, dateDiff13.Weeks);
            Assert.Equal(0, dateDiff13.Days);
            Assert.Equal(0, dateDiff13.Hours);
            Assert.Equal(0, dateDiff13.Minutes);
            Assert.Equal(-1, dateDiff13.Seconds);
        } // SecondsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void PositiveDurationTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddYears(1).AddMonths(1).AddDays(1).AddHours(1).AddMinutes(1).AddSeconds(1);

            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(1, dateDiff.ElapsedYears);
            Assert.Equal(1, dateDiff.ElapsedMonths);
            Assert.Equal(1, dateDiff.ElapsedDays);
            Assert.Equal(1, dateDiff.ElapsedHours);
            Assert.Equal(1, dateDiff.ElapsedMinutes);
            Assert.Equal(1, dateDiff.ElapsedSeconds);
        } // PositiveDurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void NegativeDurationTest()
        {
            var date1 = new DateTime(2008, 5, 14, 15, 32, 44, 243);
            var date2 = date1.AddYears(-1).AddMonths(-1).AddDays(-1).AddHours(-1).AddMinutes(-1).AddSeconds(-1);

            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(-1, dateDiff.ElapsedYears);
            Assert.Equal(-1, dateDiff.ElapsedMonths);
            Assert.Equal(-1, dateDiff.ElapsedDays);
            Assert.Equal(-1, dateDiff.ElapsedHours);
            Assert.Equal(-1, dateDiff.ElapsedMinutes);
            Assert.Equal(-1, dateDiff.ElapsedSeconds);
        } // NegativeDurationTest

        #region Richard
        // http://stackoverflow.com/questions/1083955/how-to-get-difference-between-two-dates-in-year-month-week-day/17537472#17537472

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardAlmostThreeYearsTest()
        {
            var date1 = new DateTime(2009, 7, 29);
            var date2 = new DateTime(2012, 7, 14);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(2, dateDiff.ElapsedYears);
            Assert.Equal(11, dateDiff.ElapsedMonths);
            Assert.Equal(15, dateDiff.ElapsedDays);
        } // 	RichardAlmostThreeYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardAlmostTwoYearsTest()
        {
            var date1 = new DateTime(2010, 8, 29);
            var date2 = new DateTime(2012, 8, 14);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(1, dateDiff.ElapsedYears);
            Assert.Equal(11, dateDiff.ElapsedMonths);
            Assert.Equal(16, dateDiff.ElapsedDays);
        } // 	RichardAlmostTwoYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardBasicTest()
        {
            var date1 = new DateTime(2012, 12, 1);
            var date2 = new DateTime(2012, 12, 25);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff.ElapsedYears);
            Assert.Equal(0, dateDiff.ElapsedMonths);
            Assert.Equal(24, dateDiff.ElapsedDays);
        } // 	RichardBasicTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardBornOnALeapYearTest()
        {
            var date1 = new DateTime(2008, 2, 29);
            var date2 = new DateTime(2009, 2, 28);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff.ElapsedYears);
            Assert.Equal(11, dateDiff.ElapsedMonths);
            Assert.Equal(30, dateDiff.ElapsedDays);
        } // 	RichardBornOnALeapYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardBornOnALeapYearTest2()
        {
            var date1 = new DateTime(2008, 2, 29);
            var date2 = new DateTime(2009, 3, 01);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(1, dateDiff.ElapsedYears);
            Assert.Equal(0, dateDiff.ElapsedMonths);
            Assert.Equal(1, dateDiff.ElapsedDays);
        } // 	RichardBornOnALeapYearTest2

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardLongMonthToLongMonthTest()
        {
            var date1 = new DateTime(2010, 1, 31);
            var date2 = new DateTime(2010, 3, 31);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff.ElapsedYears);
            Assert.Equal(2, dateDiff.ElapsedMonths);
            Assert.Equal(0, dateDiff.ElapsedDays);
        } // 	RichardLongMonthToLongMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardLongMonthToLongMonthPenultimateDayTest()
        {
            var date1 = new DateTime(2009, 1, 31);
            var date2 = new DateTime(2009, 3, 30);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff.ElapsedYears);
            Assert.Equal(1, dateDiff.ElapsedMonths);
            Assert.Equal(30, dateDiff.ElapsedDays);
        } // 	RichardLongMonthToLongMonthPenultimateDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardLongMonthToPartWayThruShortMonthTest()
        {
            var date1 = new DateTime(2009, 8, 31);
            var date2 = new DateTime(2009, 9, 10);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff.ElapsedYears);
            Assert.Equal(0, dateDiff.ElapsedMonths);
            Assert.Equal(10, dateDiff.ElapsedDays);
        } // 	RichardLongMonthToPartWayThruShortMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void RichardLongMonthToShortMonthTest()
        {
            var date1 = new DateTime(2009, 8, 31);
            var date2 = new DateTime(2009, 9, 30);
            var dateDiff = new DateDiff(date1, date2);
            Assert.Equal(0, dateDiff.ElapsedYears);
            Assert.Equal(0, dateDiff.ElapsedMonths);
            Assert.Equal(30, dateDiff.ElapsedDays);
        } // 	RichardLongMonthToShortMonthTest

        #endregion

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
        public void TimeSpanConstructorTest()
        {
            Assert.Equal(new DateDiff(TimeSpan.Zero).Difference, TimeSpan.Zero);

            var date1 = new DateTime(2014, 3, 4, 7, 57, 36, 234);
            var difference = new TimeSpan(234, 23, 23, 43, 233);
            var dateDiff = new DateDiff(date1, difference);
            Assert.Equal(date1, dateDiff.Date1);
            Assert.Equal(date1.Add(difference), dateDiff.Date2);
            Assert.Equal(difference, dateDiff.Difference);
        } // TimeSpanConstructorTest

    } // class DateDiffTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
