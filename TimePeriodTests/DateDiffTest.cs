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
			DateTime test = new DateTime( 2008, 10, 12, 15, 32, 44, 243 );

			DateDiff dateDiff = new DateDiff( test, test );

			Assert.Equal( dateDiff.YearBaseMonth, TimeSpec.CalendarYearStartMonth );
			Assert.Equal( dateDiff.FirstDayOfWeek, DateDiff.SafeCurrentInfo.FirstDayOfWeek );
		} // DefaultsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void EmptyDateDiffTest()
		{
			DateTime test = new DateTime( 2008, 10, 12, 15, 32, 44, 243 );

			DateDiff dateDiff = new DateDiff( test, test );

			Assert.True( dateDiff.IsEmpty );
			Assert.Equal( dateDiff.Difference, TimeSpan.Zero );
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
			DateTime date1 = new DateTime( 2008, 10, 12, 15, 32, 44, 243 );
			DateTime date2 = new DateTime( 2010, 1, 3, 23, 22, 9, 345 );

			DateDiff dateDiff = new DateDiff( date1, date2 );

			Assert.Equal( dateDiff.Difference, date2.Subtract( date1 ) );
		} // DifferenceTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void YearsTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddYears( 1 );
			DateTime date3 = date1.AddYears( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.Equal(1, dateDiff12.ElapsedYears);
			Assert.Equal(0, dateDiff12.ElapsedMonths);
			Assert.Equal(0, dateDiff12.ElapsedDays);
			Assert.Equal(0, dateDiff12.ElapsedHours);
			Assert.Equal(0, dateDiff12.ElapsedMinutes);
			Assert.Equal(0, dateDiff12.ElapsedSeconds);

			Assert.Equal(1, dateDiff12.Years);
			Assert.Equal( dateDiff12.Quarters, TimeSpec.QuartersPerYear );
			Assert.Equal( dateDiff12.Months, TimeSpec.MonthsPerYear );
			Assert.Equal(52, dateDiff12.Weeks);
			Assert.Equal(365, dateDiff12.Days);
			Assert.Equal( dateDiff12.Hours, 365 * TimeSpec.HoursPerDay );
			Assert.Equal( dateDiff12.Minutes, 365 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.Equal( dateDiff12.Seconds, 365 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal( dateDiff13.ElapsedYears, -1 );
			Assert.Equal(0, dateDiff13.ElapsedMonths);
			Assert.Equal(0, dateDiff13.ElapsedDays);
			Assert.Equal(0, dateDiff13.ElapsedHours);
			Assert.Equal(0, dateDiff13.ElapsedMinutes);
			Assert.Equal(0, dateDiff13.ElapsedSeconds);

			Assert.Equal( dateDiff13.Years, -1 );
			Assert.Equal( dateDiff13.Quarters, TimeSpec.QuartersPerYear * -1 );
			Assert.Equal( dateDiff13.Months, TimeSpec.MonthsPerYear * -1 );
			Assert.Equal( dateDiff13.Weeks, -52 );
			Assert.Equal( dateDiff13.Days, -366 );
			Assert.Equal( dateDiff13.Hours, 366 * TimeSpec.HoursPerDay * -1 );
			Assert.Equal( dateDiff13.Minutes, 366 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.Equal( dateDiff13.Seconds, 366 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // YearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void LeayYearTest()
		{
			DateTime dateBefore1 = new DateTime( 2011, 2, 27 );
			DateTime dateBefore2 = new DateTime( 2011, 2, 28 );
			DateTime dateBefore3 = new DateTime( 2011, 3, 01 );

			DateTime leapDateBefore = new DateTime( 2008, 2, 29 );
			DateTime dateTest = new DateTime( 2012, 2, 29 );
			DateTime leapDateAfter = new DateTime( 2016, 2, 29 );

			DateTime dateAfter1 = new DateTime( 2013, 2, 27 );
			DateTime dateAfter2 = new DateTime( 2013, 2, 28 );
			DateTime dateAfter3 = new DateTime( 2013, 3, 01 );

			Assert.Equal(1, new DateDiff( dateBefore1, dateTest ).Years);
			Assert.Equal( new DateDiff( dateTest, dateBefore1 ).Years, -1 );
			Assert.Equal(1, new DateDiff( dateBefore2, dateTest ).Years);
			Assert.Equal( new DateDiff( dateTest, dateBefore2 ).Years, -1 );
			Assert.Equal(0, new DateDiff( dateBefore3, dateTest ).Years);

			Assert.Equal(4, new DateDiff( leapDateBefore, dateTest ).Years);
			Assert.Equal( new DateDiff( dateTest, leapDateBefore ).Years, -4 );
			Assert.Equal(4, new DateDiff( dateTest, leapDateAfter ).Years);
			Assert.Equal( new DateDiff( leapDateAfter, dateTest ).Years, -4 );

			Assert.Equal(0, new DateDiff( dateTest, dateAfter1 ).Years);
			Assert.Equal(0, new DateDiff( dateTest, dateAfter2 ).Years);
			Assert.Equal( new DateDiff( dateAfter2, dateTest ).Years, -1 );
			Assert.Equal(1, new DateDiff( dateTest, dateAfter3 ).Years);
			Assert.Equal( new DateDiff( dateAfter3, dateTest ).Years, -1 );
		} // LeayYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void QuartersTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddMonths( 3 );
			DateTime date3 = date1.AddMonths( -3 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.Equal(0, dateDiff12.ElapsedYears);
			Assert.Equal(3, dateDiff12.ElapsedMonths);
			Assert.Equal(0, dateDiff12.ElapsedDays);
			Assert.Equal(0, dateDiff12.ElapsedHours);
			Assert.Equal(0, dateDiff12.ElapsedMinutes);
			Assert.Equal(0, dateDiff12.ElapsedSeconds);

			Assert.Equal(0, dateDiff12.Years);
			Assert.Equal(1, dateDiff12.Quarters);
			Assert.Equal( dateDiff12.Months, TimeSpec.MonthsPerQuarter );
			Assert.Equal(13, dateDiff12.Weeks);
			Assert.Equal(92, dateDiff12.Days);
			Assert.Equal( dateDiff12.Hours, 92 * TimeSpec.HoursPerDay );
			Assert.Equal( dateDiff12.Minutes, 92 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.Equal( dateDiff12.Seconds, 92 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal(0, dateDiff13.ElapsedYears);
			Assert.Equal( dateDiff13.ElapsedMonths, -3 );
			Assert.Equal(0, dateDiff13.ElapsedDays);
			Assert.Equal(0, dateDiff13.ElapsedHours);
			Assert.Equal(0, dateDiff13.ElapsedMinutes);
			Assert.Equal(0, dateDiff13.ElapsedSeconds);

			Assert.Equal(0, dateDiff13.Years);
			Assert.Equal( dateDiff13.Quarters, -1 );
			Assert.Equal( dateDiff13.Months, -TimeSpec.MonthsPerQuarter );
			Assert.Equal( dateDiff13.Weeks, -13 );
			Assert.Equal( dateDiff13.Days, -90 );
			Assert.Equal( dateDiff13.Hours, 90 * TimeSpec.HoursPerDay * -1 );
			Assert.Equal( dateDiff13.Minutes, 90 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.Equal( dateDiff13.Seconds, 90 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // YearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void MonthsTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddMonths( 1 );
			DateTime date3 = date1.AddMonths( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
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
			Assert.Equal( dateDiff12.Hours, 31 * TimeSpec.HoursPerDay );
			Assert.Equal( dateDiff12.Minutes, 31 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.Equal( dateDiff12.Seconds, 31 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal(0, dateDiff13.ElapsedYears);
			Assert.Equal( dateDiff13.ElapsedMonths, -1 );
			Assert.Equal(0, dateDiff13.ElapsedDays);
			Assert.Equal(0, dateDiff13.ElapsedHours);
			Assert.Equal(0, dateDiff13.ElapsedMinutes);
			Assert.Equal(0, dateDiff13.ElapsedSeconds);

			Assert.Equal(0, dateDiff13.Years);
			Assert.Equal(0, dateDiff13.Quarters);
			Assert.Equal( dateDiff13.Months, -1 );
			Assert.Equal( dateDiff13.Weeks, -4 );
			Assert.Equal( dateDiff13.Days, 30 * -1 );
			Assert.Equal( dateDiff13.Hours, 30 * TimeSpec.HoursPerDay * -1 );
			Assert.Equal( dateDiff13.Minutes, 30 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.Equal( dateDiff13.Seconds, 30 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // MonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void WeeksTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddDays( TimeSpec.DaysPerWeek );
			DateTime date3 = date1.AddDays( -TimeSpec.DaysPerWeek );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.Equal(0, dateDiff12.Years);
			Assert.Equal(0, dateDiff12.Quarters);
			Assert.Equal(0, dateDiff12.Months);
			Assert.Equal(1, dateDiff12.Weeks);
			Assert.Equal( dateDiff12.Days, TimeSpec.DaysPerWeek );
			Assert.Equal( dateDiff12.Hours, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay );
			Assert.Equal( dateDiff12.Minutes, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.Equal( dateDiff12.Seconds, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal(0, dateDiff13.Years);
			Assert.Equal(0, dateDiff13.Quarters);
			Assert.Equal(0, dateDiff13.Months);
			Assert.Equal( dateDiff13.Weeks, -1 );
			Assert.Equal( dateDiff13.Days, TimeSpec.DaysPerWeek * -1 );
			Assert.Equal( dateDiff13.Hours, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * -1 );
			Assert.Equal( dateDiff13.Minutes, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.Equal( dateDiff13.Seconds, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // WeeksTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void DaysTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddDays( 1 );
			DateTime date3 = date1.AddDays( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
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
			Assert.Equal( dateDiff12.Hours, TimeSpec.HoursPerDay );
			Assert.Equal( dateDiff12.Minutes, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.Equal( dateDiff12.Seconds, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal(0, dateDiff13.ElapsedYears);
			Assert.Equal(0, dateDiff13.ElapsedMonths);
			Assert.Equal( dateDiff13.ElapsedDays, -1 );
			Assert.Equal(0, dateDiff13.ElapsedHours);
			Assert.Equal(0, dateDiff13.ElapsedMinutes);
			Assert.Equal(0, dateDiff13.ElapsedSeconds);

			Assert.Equal(0, dateDiff13.Years);
			Assert.Equal(0, dateDiff13.Quarters);
			Assert.Equal(0, dateDiff13.Months);
			Assert.Equal(0, dateDiff13.Weeks);
			Assert.Equal( dateDiff13.Days, -1 );
			Assert.Equal( dateDiff13.Hours, -TimeSpec.HoursPerDay );
			Assert.Equal( dateDiff13.Minutes, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.Equal( dateDiff13.Seconds, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // DaysTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void HoursTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddHours( 1 );
			DateTime date3 = date1.AddHours( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
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
			Assert.Equal( dateDiff12.Minutes, TimeSpec.MinutesPerHour );
			Assert.Equal( dateDiff12.Seconds, TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal(0, dateDiff13.ElapsedYears);
			Assert.Equal(0, dateDiff13.ElapsedMonths);
			Assert.Equal(0, dateDiff13.ElapsedDays);
			Assert.Equal( dateDiff13.ElapsedHours, -1 );
			Assert.Equal(0, dateDiff13.ElapsedMinutes);
			Assert.Equal(0, dateDiff13.ElapsedSeconds);

			Assert.Equal(0, dateDiff13.Years);
			Assert.Equal(0, dateDiff13.Quarters);
			Assert.Equal(0, dateDiff13.Months);
			Assert.Equal(0, dateDiff13.Weeks);
			Assert.Equal(0, dateDiff13.Days);
			Assert.Equal( dateDiff13.Hours, -1 );
			Assert.Equal( dateDiff13.Minutes, TimeSpec.MinutesPerHour * -1 );
			Assert.Equal( dateDiff13.Seconds, TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // HoursTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void MinutesTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddMinutes( 1 );
			DateTime date3 = date1.AddMinutes( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
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
			Assert.Equal( dateDiff12.Seconds, TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal(0, dateDiff13.ElapsedYears);
			Assert.Equal(0, dateDiff13.ElapsedMonths);
			Assert.Equal(0, dateDiff13.ElapsedDays);
			Assert.Equal(0, dateDiff13.ElapsedHours);
			Assert.Equal( dateDiff13.ElapsedMinutes, -1 );
			Assert.Equal(0, dateDiff13.ElapsedSeconds);

			Assert.Equal(0, dateDiff13.Years);
			Assert.Equal(0, dateDiff13.Quarters);
			Assert.Equal(0, dateDiff13.Months);
			Assert.Equal(0, dateDiff13.Weeks);
			Assert.Equal(0, dateDiff13.Days);
			Assert.Equal(0, dateDiff13.Hours);
			Assert.Equal( dateDiff13.Minutes, -1 );
			Assert.Equal( dateDiff13.Seconds, TimeSpec.SecondsPerMinute * -1 );
		} // MinutesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void SecondsTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddSeconds( 1 );
			DateTime date3 = date1.AddSeconds( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
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

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.Equal(0, dateDiff13.ElapsedYears);
			Assert.Equal(0, dateDiff13.ElapsedMonths);
			Assert.Equal(0, dateDiff13.ElapsedDays);
			Assert.Equal(0, dateDiff13.ElapsedHours);
			Assert.Equal(0, dateDiff13.ElapsedMinutes);
			Assert.Equal( dateDiff13.ElapsedSeconds, -1 );

			Assert.Equal(0, dateDiff13.Years);
			Assert.Equal(0, dateDiff13.Quarters);
			Assert.Equal(0, dateDiff13.Months);
			Assert.Equal(0, dateDiff13.Weeks);
			Assert.Equal(0, dateDiff13.Days);
			Assert.Equal(0, dateDiff13.Hours);
			Assert.Equal(0, dateDiff13.Minutes);
			Assert.Equal( dateDiff13.Seconds, -1 );
		} // SecondsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void PositiveDurationTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddYears( 1 ).AddMonths( 1 ).AddDays( 1 ).AddHours( 1 ).AddMinutes( 1 ).AddSeconds( 1 );

			DateDiff dateDiff = new DateDiff( date1, date2 );
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
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddYears( -1 ).AddMonths( -1 ).AddDays( -1 ).AddHours( -1 ).AddMinutes( -1 ).AddSeconds( -1 );

			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal( dateDiff.ElapsedYears, -1 );
			Assert.Equal( dateDiff.ElapsedMonths, -1 );
			Assert.Equal( dateDiff.ElapsedDays, -1 );
			Assert.Equal( dateDiff.ElapsedHours, -1 );
			Assert.Equal( dateDiff.ElapsedMinutes, -1 );
			Assert.Equal( dateDiff.ElapsedSeconds, -1 );
		} // NegativeDurationTest

        #region Richard
        // http://stackoverflow.com/questions/1083955/how-to-get-difference-between-two-dates-in-year-month-week-day/17537472#17537472

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardAlmostThreeYearsTest()
		{
			DateTime date1 = new DateTime( 2009, 7, 29 );
			DateTime date2 = new DateTime( 2012, 7, 14 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(2, dateDiff.ElapsedYears);
			Assert.Equal(11, dateDiff.ElapsedMonths);
			Assert.Equal(15, dateDiff.ElapsedDays);
		} // 	RichardAlmostThreeYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardAlmostTwoYearsTest()
		{
			DateTime date1 = new DateTime( 2010, 8, 29 );
			DateTime date2 = new DateTime( 2012, 8, 14 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(1, dateDiff.ElapsedYears);
			Assert.Equal(11, dateDiff.ElapsedMonths);
			Assert.Equal(16, dateDiff.ElapsedDays);
		} // 	RichardAlmostTwoYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardBasicTest()
		{
			DateTime date1 = new DateTime( 2012, 12, 1 );
			DateTime date2 = new DateTime( 2012, 12, 25 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(0, dateDiff.ElapsedYears);
			Assert.Equal(0, dateDiff.ElapsedMonths);
			Assert.Equal(24, dateDiff.ElapsedDays);
		} // 	RichardBasicTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardBornOnALeapYearTest()
		{
			DateTime date1 = new DateTime( 2008, 2, 29 );
			DateTime date2 = new DateTime( 2009, 2, 28 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(0, dateDiff.ElapsedYears);
			Assert.Equal(11, dateDiff.ElapsedMonths);
			Assert.Equal(30, dateDiff.ElapsedDays);
		} // 	RichardBornOnALeapYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardBornOnALeapYearTest2()
		{
			DateTime date1 = new DateTime( 2008, 2, 29 );
			DateTime date2 = new DateTime( 2009, 3, 01 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(1, dateDiff.ElapsedYears);
			Assert.Equal(0, dateDiff.ElapsedMonths);
			Assert.Equal(1, dateDiff.ElapsedDays);
		} // 	RichardBornOnALeapYearTest2

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardLongMonthToLongMonthTest()
		{
			DateTime date1 = new DateTime( 2010, 1, 31 );
			DateTime date2 = new DateTime( 2010, 3, 31 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(0, dateDiff.ElapsedYears);
			Assert.Equal(2, dateDiff.ElapsedMonths);
			Assert.Equal(0, dateDiff.ElapsedDays);
		} // 	RichardLongMonthToLongMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardLongMonthToLongMonthPenultimateDayTest()
		{
			DateTime date1 = new DateTime( 2009, 1, 31 );
			DateTime date2 = new DateTime( 2009, 3, 30 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(0, dateDiff.ElapsedYears);
			Assert.Equal(1, dateDiff.ElapsedMonths);
			Assert.Equal(30, dateDiff.ElapsedDays);
		} // 	RichardLongMonthToLongMonthPenultimateDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardLongMonthToPartWayThruShortMonthTest()
		{
			DateTime date1 = new DateTime( 2009, 8, 31 );
			DateTime date2 = new DateTime( 2009, 9, 10 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.Equal(0, dateDiff.ElapsedYears);
			Assert.Equal(0, dateDiff.ElapsedMonths);
			Assert.Equal(10, dateDiff.ElapsedDays);
		} // 	RichardLongMonthToPartWayThruShortMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateDiff")]
        [Fact]
		public void RichardLongMonthToShortMonthTest()
		{
			DateTime date1 = new DateTime( 2009, 8, 31 );
			DateTime date2 = new DateTime( 2009, 9, 30 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
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
			Assert.Equal( new DateDiff( TimeSpan.Zero ).Difference, TimeSpan.Zero );

			DateTime date1 = new DateTime( 2014, 3, 4, 7, 57, 36, 234 );
			TimeSpan diffence = new TimeSpan( 234, 23, 23, 43, 233 );
			DateDiff dateDiff = new DateDiff( date1, diffence );
			Assert.Equal( dateDiff.Date1, date1 );
			Assert.Equal( dateDiff.Date2, date1.Add( diffence ) );
			Assert.Equal( dateDiff.Difference, diffence );
		} // TimeSpanConstructorTest

	} // class DateDiffTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
