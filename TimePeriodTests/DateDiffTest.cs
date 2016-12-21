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
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class DateDiffTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultsTest()
		{
			DateTime test = new DateTime( 2008, 10, 12, 15, 32, 44, 243 );

			DateDiff dateDiff = new DateDiff( test, test );

			Assert.AreEqual( dateDiff.YearBaseMonth, TimeSpec.CalendarYearStartMonth );
			Assert.AreEqual( dateDiff.FirstDayOfWeek, DateDiff.SafeCurrentInfo.FirstDayOfWeek );
		} // DefaultsTest

		// ----------------------------------------------------------------------
		[Test]
		public void EmptyDateDiffTest()
		{
			DateTime test = new DateTime( 2008, 10, 12, 15, 32, 44, 243 );

			DateDiff dateDiff = new DateDiff( test, test );

			Assert.IsTrue( dateDiff.IsEmpty );
			Assert.AreEqual( dateDiff.Difference, TimeSpan.Zero );
			Assert.AreEqual( dateDiff.Years, 0 );
			Assert.AreEqual( dateDiff.Quarters, 0 );
			Assert.AreEqual( dateDiff.Months, 0 );
			Assert.AreEqual( dateDiff.Weeks, 0 );
			Assert.AreEqual( dateDiff.Days, 0 );
			Assert.AreEqual( dateDiff.Hours, 0 );
			Assert.AreEqual( dateDiff.Minutes, 0 );
			Assert.AreEqual( dateDiff.Seconds, 0 );

			Assert.AreEqual( dateDiff.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff.ElapsedSeconds, 0 );
		} // EmptyDateDiffTest

		// ----------------------------------------------------------------------
		[Test]
		public void DifferenceTest()
		{
			DateTime date1 = new DateTime( 2008, 10, 12, 15, 32, 44, 243 );
			DateTime date2 = new DateTime( 2010, 1, 3, 23, 22, 9, 345 );

			DateDiff dateDiff = new DateDiff( date1, date2 );

			Assert.AreEqual( dateDiff.Difference, date2.Subtract( date1 ) );
		} // DifferenceTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearsTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddYears( 1 );
			DateTime date3 = date1.AddYears( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.ElapsedYears, 1 );
			Assert.AreEqual( dateDiff12.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff12.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff12.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff12.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff12.Years, 1 );
			Assert.AreEqual( dateDiff12.Quarters, TimeSpec.QuartersPerYear );
			Assert.AreEqual( dateDiff12.Months, TimeSpec.MonthsPerYear );
			Assert.AreEqual( dateDiff12.Weeks, 52 );
			Assert.AreEqual( dateDiff12.Days, 365 );
			Assert.AreEqual( dateDiff12.Hours, 365 * TimeSpec.HoursPerDay );
			Assert.AreEqual( dateDiff12.Minutes, 365 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.AreEqual( dateDiff12.Seconds, 365 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.ElapsedYears, -1 );
			Assert.AreEqual( dateDiff13.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff13.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff13.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff13.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff13.Years, -1 );
			Assert.AreEqual( dateDiff13.Quarters, TimeSpec.QuartersPerYear * -1 );
			Assert.AreEqual( dateDiff13.Months, TimeSpec.MonthsPerYear * -1 );
			Assert.AreEqual( dateDiff13.Weeks, -52 );
			Assert.AreEqual( dateDiff13.Days, -366 );
			Assert.AreEqual( dateDiff13.Hours, 366 * TimeSpec.HoursPerDay * -1 );
			Assert.AreEqual( dateDiff13.Minutes, 366 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.AreEqual( dateDiff13.Seconds, 366 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // YearsTest

		// ----------------------------------------------------------------------
		[Test]
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

			Assert.AreEqual( new DateDiff( dateBefore1, dateTest ).Years, 1 );
			Assert.AreEqual( new DateDiff( dateTest, dateBefore1 ).Years, -1 );
			Assert.AreEqual( new DateDiff( dateBefore2, dateTest ).Years, 1 );
			Assert.AreEqual( new DateDiff( dateTest, dateBefore2 ).Years, -1 );
			Assert.AreEqual( new DateDiff( dateBefore3, dateTest ).Years, 0 );

			Assert.AreEqual( new DateDiff( leapDateBefore, dateTest ).Years, 4 );
			Assert.AreEqual( new DateDiff( dateTest, leapDateBefore ).Years, -4 );
			Assert.AreEqual( new DateDiff( dateTest, leapDateAfter ).Years, 4 );
			Assert.AreEqual( new DateDiff( leapDateAfter, dateTest ).Years, -4 );

			Assert.AreEqual( new DateDiff( dateTest, dateAfter1 ).Years, 0 );
			Assert.AreEqual( new DateDiff( dateTest, dateAfter2 ).Years, 0 );
			Assert.AreEqual( new DateDiff( dateAfter2, dateTest ).Years, -1 );
			Assert.AreEqual( new DateDiff( dateTest, dateAfter3 ).Years, 1 );
			Assert.AreEqual( new DateDiff( dateAfter3, dateTest ).Years, -1 );
		} // LeayYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void QuartersTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddMonths( 3 );
			DateTime date3 = date1.AddMonths( -3 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMonths, 3 );
			Assert.AreEqual( dateDiff12.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff12.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff12.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff12.Years, 0 );
			Assert.AreEqual( dateDiff12.Quarters, 1 );
			Assert.AreEqual( dateDiff12.Months, TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( dateDiff12.Weeks, 13 );
			Assert.AreEqual( dateDiff12.Days, 92 );
			Assert.AreEqual( dateDiff12.Hours, 92 * TimeSpec.HoursPerDay );
			Assert.AreEqual( dateDiff12.Minutes, 92 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.AreEqual( dateDiff12.Seconds, 92 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMonths, -3 );
			Assert.AreEqual( dateDiff13.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff13.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff13.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff13.Years, 0 );
			Assert.AreEqual( dateDiff13.Quarters, -1 );
			Assert.AreEqual( dateDiff13.Months, -TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( dateDiff13.Weeks, -13 );
			Assert.AreEqual( dateDiff13.Days, -90 );
			Assert.AreEqual( dateDiff13.Hours, 90 * TimeSpec.HoursPerDay * -1 );
			Assert.AreEqual( dateDiff13.Minutes, 90 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.AreEqual( dateDiff13.Seconds, 90 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // YearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void MonthsTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddMonths( 1 );
			DateTime date3 = date1.AddMonths( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMonths, 1 );
			Assert.AreEqual( dateDiff12.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff12.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff12.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff12.Years, 0 );
			Assert.AreEqual( dateDiff12.Quarters, 0 );
			Assert.AreEqual( dateDiff12.Months, 1 );
			Assert.AreEqual( dateDiff12.Weeks, 4 );
			Assert.AreEqual( dateDiff12.Days, 31 );
			Assert.AreEqual( dateDiff12.Hours, 31 * TimeSpec.HoursPerDay );
			Assert.AreEqual( dateDiff12.Minutes, 31 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.AreEqual( dateDiff12.Seconds, 31 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMonths, -1 );
			Assert.AreEqual( dateDiff13.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff13.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff13.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff13.Years, 0 );
			Assert.AreEqual( dateDiff13.Quarters, 0 );
			Assert.AreEqual( dateDiff13.Months, -1 );
			Assert.AreEqual( dateDiff13.Weeks, -4 );
			Assert.AreEqual( dateDiff13.Days, 30 * -1 );
			Assert.AreEqual( dateDiff13.Hours, 30 * TimeSpec.HoursPerDay * -1 );
			Assert.AreEqual( dateDiff13.Minutes, 30 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.AreEqual( dateDiff13.Seconds, 30 * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // MonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void WeeksTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddDays( TimeSpec.DaysPerWeek );
			DateTime date3 = date1.AddDays( -TimeSpec.DaysPerWeek );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.Years, 0 );
			Assert.AreEqual( dateDiff12.Quarters, 0 );
			Assert.AreEqual( dateDiff12.Months, 0 );
			Assert.AreEqual( dateDiff12.Weeks, 1 );
			Assert.AreEqual( dateDiff12.Days, TimeSpec.DaysPerWeek );
			Assert.AreEqual( dateDiff12.Hours, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay );
			Assert.AreEqual( dateDiff12.Minutes, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.AreEqual( dateDiff12.Seconds, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.Years, 0 );
			Assert.AreEqual( dateDiff13.Quarters, 0 );
			Assert.AreEqual( dateDiff13.Months, 0 );
			Assert.AreEqual( dateDiff13.Weeks, -1 );
			Assert.AreEqual( dateDiff13.Days, TimeSpec.DaysPerWeek * -1 );
			Assert.AreEqual( dateDiff13.Hours, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * -1 );
			Assert.AreEqual( dateDiff13.Minutes, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.AreEqual( dateDiff13.Seconds, TimeSpec.DaysPerWeek * TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // WeeksTest

		// ----------------------------------------------------------------------
		[Test]
		public void DaysTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddDays( 1 );
			DateTime date3 = date1.AddDays( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff12.ElapsedDays, 1 );
			Assert.AreEqual( dateDiff12.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff12.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff12.Years, 0 );
			Assert.AreEqual( dateDiff12.Quarters, 0 );
			Assert.AreEqual( dateDiff12.Months, 0 );
			Assert.AreEqual( dateDiff12.Weeks, 0 );
			Assert.AreEqual( dateDiff12.Days, 1 );
			Assert.AreEqual( dateDiff12.Hours, TimeSpec.HoursPerDay );
			Assert.AreEqual( dateDiff12.Minutes, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour );
			Assert.AreEqual( dateDiff12.Seconds, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff13.ElapsedDays, -1 );
			Assert.AreEqual( dateDiff13.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff13.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff13.Years, 0 );
			Assert.AreEqual( dateDiff13.Quarters, 0 );
			Assert.AreEqual( dateDiff13.Months, 0 );
			Assert.AreEqual( dateDiff13.Weeks, 0 );
			Assert.AreEqual( dateDiff13.Days, -1 );
			Assert.AreEqual( dateDiff13.Hours, -TimeSpec.HoursPerDay );
			Assert.AreEqual( dateDiff13.Minutes, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * -1 );
			Assert.AreEqual( dateDiff13.Seconds, TimeSpec.HoursPerDay * TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // DaysTest

		// ----------------------------------------------------------------------
		[Test]
		public void HoursTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddHours( 1 );
			DateTime date3 = date1.AddHours( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff12.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff12.ElapsedHours, 1 );
			Assert.AreEqual( dateDiff12.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff12.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff12.Years, 0 );
			Assert.AreEqual( dateDiff12.Quarters, 0 );
			Assert.AreEqual( dateDiff12.Months, 0 );
			Assert.AreEqual( dateDiff12.Weeks, 0 );
			Assert.AreEqual( dateDiff12.Days, 0 );
			Assert.AreEqual( dateDiff12.Hours, 1 );
			Assert.AreEqual( dateDiff12.Minutes, TimeSpec.MinutesPerHour );
			Assert.AreEqual( dateDiff12.Seconds, TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff13.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff13.ElapsedHours, -1 );
			Assert.AreEqual( dateDiff13.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff13.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff13.Years, 0 );
			Assert.AreEqual( dateDiff13.Quarters, 0 );
			Assert.AreEqual( dateDiff13.Months, 0 );
			Assert.AreEqual( dateDiff13.Weeks, 0 );
			Assert.AreEqual( dateDiff13.Days, 0 );
			Assert.AreEqual( dateDiff13.Hours, -1 );
			Assert.AreEqual( dateDiff13.Minutes, TimeSpec.MinutesPerHour * -1 );
			Assert.AreEqual( dateDiff13.Seconds, TimeSpec.MinutesPerHour * TimeSpec.SecondsPerMinute * -1 );
		} // HoursTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinutesTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddMinutes( 1 );
			DateTime date3 = date1.AddMinutes( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff12.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff12.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMinutes, 1 );
			Assert.AreEqual( dateDiff12.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff12.Years, 0 );
			Assert.AreEqual( dateDiff12.Quarters, 0 );
			Assert.AreEqual( dateDiff12.Months, 0 );
			Assert.AreEqual( dateDiff12.Weeks, 0 );
			Assert.AreEqual( dateDiff12.Days, 0 );
			Assert.AreEqual( dateDiff12.Hours, 0 );
			Assert.AreEqual( dateDiff12.Minutes, 1 );
			Assert.AreEqual( dateDiff12.Seconds, TimeSpec.SecondsPerMinute );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff13.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff13.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMinutes, -1 );
			Assert.AreEqual( dateDiff13.ElapsedSeconds, 0 );

			Assert.AreEqual( dateDiff13.Years, 0 );
			Assert.AreEqual( dateDiff13.Quarters, 0 );
			Assert.AreEqual( dateDiff13.Months, 0 );
			Assert.AreEqual( dateDiff13.Weeks, 0 );
			Assert.AreEqual( dateDiff13.Days, 0 );
			Assert.AreEqual( dateDiff13.Hours, 0 );
			Assert.AreEqual( dateDiff13.Minutes, -1 );
			Assert.AreEqual( dateDiff13.Seconds, TimeSpec.SecondsPerMinute * -1 );
		} // MinutesTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondsTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddSeconds( 1 );
			DateTime date3 = date1.AddSeconds( -1 );

			DateDiff dateDiff12 = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff12.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff12.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff12.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff12.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff12.ElapsedSeconds, 1 );

			Assert.AreEqual( dateDiff12.Years, 0 );
			Assert.AreEqual( dateDiff12.Quarters, 0 );
			Assert.AreEqual( dateDiff12.Months, 0 );
			Assert.AreEqual( dateDiff12.Weeks, 0 );
			Assert.AreEqual( dateDiff12.Days, 0 );
			Assert.AreEqual( dateDiff12.Hours, 0 );
			Assert.AreEqual( dateDiff12.Minutes, 0 );
			Assert.AreEqual( dateDiff12.Seconds, 1 );

			DateDiff dateDiff13 = new DateDiff( date1, date3 );
			Assert.AreEqual( dateDiff13.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff13.ElapsedDays, 0 );
			Assert.AreEqual( dateDiff13.ElapsedHours, 0 );
			Assert.AreEqual( dateDiff13.ElapsedMinutes, 0 );
			Assert.AreEqual( dateDiff13.ElapsedSeconds, -1 );

			Assert.AreEqual( dateDiff13.Years, 0 );
			Assert.AreEqual( dateDiff13.Quarters, 0 );
			Assert.AreEqual( dateDiff13.Months, 0 );
			Assert.AreEqual( dateDiff13.Weeks, 0 );
			Assert.AreEqual( dateDiff13.Days, 0 );
			Assert.AreEqual( dateDiff13.Hours, 0 );
			Assert.AreEqual( dateDiff13.Minutes, 0 );
			Assert.AreEqual( dateDiff13.Seconds, -1 );
		} // SecondsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PositiveDurationTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddYears( 1 ).AddMonths( 1 ).AddDays( 1 ).AddHours( 1 ).AddMinutes( 1 ).AddSeconds( 1 );

			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 1 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 1 );
			Assert.AreEqual( dateDiff.ElapsedDays, 1 );
			Assert.AreEqual( dateDiff.ElapsedHours, 1 );
			Assert.AreEqual( dateDiff.ElapsedMinutes, 1 );
			Assert.AreEqual( dateDiff.ElapsedSeconds, 1 );
		} // PositiveDurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void NegativeDurationTest()
		{
			DateTime date1 = new DateTime( 2008, 5, 14, 15, 32, 44, 243 );
			DateTime date2 = date1.AddYears( -1 ).AddMonths( -1 ).AddDays( -1 ).AddHours( -1 ).AddMinutes( -1 ).AddSeconds( -1 );

			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, -1 );
			Assert.AreEqual( dateDiff.ElapsedMonths, -1 );
			Assert.AreEqual( dateDiff.ElapsedDays, -1 );
			Assert.AreEqual( dateDiff.ElapsedHours, -1 );
			Assert.AreEqual( dateDiff.ElapsedMinutes, -1 );
			Assert.AreEqual( dateDiff.ElapsedSeconds, -1 );
		} // NegativeDurationTest

		#region Richard
		// http://stackoverflow.com/questions/1083955/how-to-get-difference-between-two-dates-in-year-month-week-day/17537472#17537472

		// ----------------------------------------------------------------------
		[Test]
		public void RichardAlmostThreeYearsTest()
		{
			DateTime date1 = new DateTime( 2009, 7, 29 );
			DateTime date2 = new DateTime( 2012, 7, 14 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 2 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 11 );
			Assert.AreEqual( dateDiff.ElapsedDays, 15 );
		} // 	RichardAlmostThreeYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void RichardAlmostTwoYearsTest()
		{
			DateTime date1 = new DateTime( 2010, 8, 29 );
			DateTime date2 = new DateTime( 2012, 8, 14 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 1 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 11 );
			Assert.AreEqual( dateDiff.ElapsedDays, 16 );
		} // 	RichardAlmostTwoYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void RichardBasicTest()
		{
			DateTime date1 = new DateTime( 2012, 12, 1 );
			DateTime date2 = new DateTime( 2012, 12, 25 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff.ElapsedDays, 24 );
		} // 	RichardBasicTest

		// ----------------------------------------------------------------------
		[Test]
		public void RichardBornOnALeapYearTest()
		{
			DateTime date1 = new DateTime( 2008, 2, 29 );
			DateTime date2 = new DateTime( 2009, 2, 28 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 11 );
			Assert.AreEqual( dateDiff.ElapsedDays, 30 );
		} // 	RichardBornOnALeapYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void RichardBornOnALeapYearTest2()
		{
			DateTime date1 = new DateTime( 2008, 2, 29 );
			DateTime date2 = new DateTime( 2009, 3, 01 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 1 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff.ElapsedDays, 1 );
		} // 	RichardBornOnALeapYearTest2

		// ----------------------------------------------------------------------
		[Test]
		public void RichardLongMonthToLongMonthTest()
		{
			DateTime date1 = new DateTime( 2010, 1, 31 );
			DateTime date2 = new DateTime( 2010, 3, 31 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 2 );
			Assert.AreEqual( dateDiff.ElapsedDays, 0 );
		} // 	RichardLongMonthToLongMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void RichardLongMonthToLongMonthPenultimateDayTest()
		{
			DateTime date1 = new DateTime( 2009, 1, 31 );
			DateTime date2 = new DateTime( 2009, 3, 30 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 1 );
			Assert.AreEqual( dateDiff.ElapsedDays, 30 );
		} // 	RichardLongMonthToLongMonthPenultimateDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void RichardLongMonthToPartWayThruShortMonthTest()
		{
			DateTime date1 = new DateTime( 2009, 8, 31 );
			DateTime date2 = new DateTime( 2009, 9, 10 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff.ElapsedDays, 10 );
		} // 	RichardLongMonthToPartWayThruShortMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void RichardLongMonthToShortMonthTest()
		{
			DateTime date1 = new DateTime( 2009, 8, 31 );
			DateTime date2 = new DateTime( 2009, 9, 30 );
			DateDiff dateDiff = new DateDiff( date1, date2 );
			Assert.AreEqual( dateDiff.ElapsedYears, 0 );
			Assert.AreEqual( dateDiff.ElapsedMonths, 0 );
			Assert.AreEqual( dateDiff.ElapsedDays, 30 );
		} // 	RichardLongMonthToShortMonthTest

		#endregion

		// ----------------------------------------------------------------------
		[Test]
		public void TimeSpanConstructorTest()
		{
			Assert.AreEqual( new DateDiff( TimeSpan.Zero ).Difference, TimeSpan.Zero );

			DateTime date1 = new DateTime( 2014, 3, 4, 7, 57, 36, 234 );
			TimeSpan diffence = new TimeSpan( 234, 23, 23, 43, 233 );
			DateDiff dateDiff = new DateDiff( date1, diffence );
			Assert.AreEqual( dateDiff.Date1, date1 );
			Assert.AreEqual( dateDiff.Date2, date1.Add( diffence ) );
			Assert.AreEqual( dateDiff.Difference, diffence );
		} // TimeSpanConstructorTest

	} // class DateDiffTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
