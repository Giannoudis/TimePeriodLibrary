// -- FILE ------------------------------------------------------------------
// name       : BusinessCaseTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
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
	public sealed class BusinessCaseTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void TimeRangeCalendarTimeRangeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange fiveSeconds = new TimeRange(
				new DateTime( now.Year, now.Month, now.Day, now.Hour, now.Minute, 15, 0 ),
				new DateTime( now.Year, now.Month, now.Day, now.Hour, now.Minute, 20, 0 ) );
			Assert.IsTrue( new Year( now ).HasInside( fiveSeconds ) );
			Assert.IsTrue( new Quarter( now ).HasInside( fiveSeconds ) );
			Assert.IsTrue( new Month( now ).HasInside( fiveSeconds ) );
			// fo nto test the week: can be outside of the current days
			//Assert.IsTrue( new Week( now ).HasInside( fiveSeconds ) );
			Assert.IsTrue( new Day( now ).HasInside( fiveSeconds ) );
			Assert.IsTrue( new Hour( now ).HasInside( fiveSeconds ) );
			Assert.IsTrue( new Minute( now ).HasInside( fiveSeconds ) );

			TimeRange anytime = new TimeRange();
			Assert.IsFalse( new Year().HasInside( anytime ) );
			Assert.IsFalse( new Quarter().HasInside( anytime ) );
			Assert.IsFalse( new Month().HasInside( anytime ) );
			Assert.IsFalse( new Week().HasInside( anytime ) );
			Assert.IsFalse( new Day().HasInside( anytime ) );
			Assert.IsFalse( new Hour().HasInside( anytime ) );
			Assert.IsFalse( new Minute().HasInside( anytime ) );
		} // TimeRangeCalendarTimeRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearTest()
		{
			DateTime testDate = new DateTime( 2008, 11, 18 );
			Year year = new Year( testDate, TimeCalendar.New( YearMonth.October ) );

			Assert.AreEqual( year.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( year.BaseYear, 2008 );

			// start & end
			Assert.AreEqual( year.Start.Year, testDate.Year );
			Assert.AreEqual( year.Start.Month, 10 );
			Assert.AreEqual( year.Start.Day, 1 );
			Assert.AreEqual( year.End.Year, testDate.Year + 1 );
			Assert.AreEqual( year.End.Month, 9 );
			Assert.AreEqual( year.End.Day, 30 );

			// half years
			ITimePeriodCollection halfyears = year.GetHalfyears();
			foreach ( Halfyear halfyear in halfyears )
			{
				switch ( halfyear.YearHalfyear )
				{
					case YearHalfyear.First:
						Assert.AreEqual( halfyear.Start, year.Start );
						Assert.AreEqual( halfyear.Start.Year, testDate.Year );
						Assert.AreEqual( halfyear.Start.Month, 10 );
						Assert.AreEqual( halfyear.Start.Day, 1 );
						Assert.AreEqual( halfyear.End.Year, testDate.Year + 1 );
						Assert.AreEqual( halfyear.End.Month, 3 );
						Assert.AreEqual( halfyear.End.Day, 31 );
						break;
					case YearHalfyear.Second:
						Assert.AreEqual( halfyear.End, year.End );
						Assert.AreEqual( halfyear.Start.Year, testDate.Year + 1 );
						Assert.AreEqual( halfyear.Start.Month, 4 );
						Assert.AreEqual( halfyear.Start.Day, 1 );
						Assert.AreEqual( halfyear.End.Year, testDate.Year + 1 );
						Assert.AreEqual( halfyear.End.Month, 9 );
						Assert.AreEqual( halfyear.End.Day, 30 );
						break;
				}
			}

			// half years
			ITimePeriodCollection quarters = year.GetQuarters();
			foreach ( Quarter quarter in quarters )
			{
				switch ( quarter.YearQuarter )
				{
					case YearQuarter.First:
						Assert.AreEqual( quarter.Start, year.Start );
						Assert.AreEqual( quarter.Start.Year, testDate.Year );
						Assert.AreEqual( quarter.Start.Month, 10 );
						Assert.AreEqual( quarter.Start.Day, 1 );
						Assert.AreEqual( quarter.End.Year, testDate.Year );
						Assert.AreEqual( quarter.End.Month, 12 );
						Assert.AreEqual( quarter.End.Day, 31 );
						break;
					case YearQuarter.Second:
						Assert.AreEqual( quarter.Start.Year, testDate.Year + 1 );
						Assert.AreEqual( quarter.Start.Month, 1 );
						Assert.AreEqual( quarter.Start.Day, 1 );
						Assert.AreEqual( quarter.End.Year, testDate.Year + 1 );
						Assert.AreEqual( quarter.End.Month, 3 );
						Assert.AreEqual( quarter.End.Day, 31 );
						break;
					case YearQuarter.Third:
						Assert.AreEqual( quarter.Start.Year, testDate.Year + 1 );
						Assert.AreEqual( quarter.Start.Month, 4 );
						Assert.AreEqual( quarter.Start.Day, 1 );
						Assert.AreEqual( quarter.End.Year, testDate.Year + 1 );
						Assert.AreEqual( quarter.End.Month, 6 );
						Assert.AreEqual( quarter.End.Day, 30 );
						break;
					case YearQuarter.Fourth:
						Assert.AreEqual( quarter.End, year.End );
						Assert.AreEqual( quarter.Start.Year, testDate.Year + 1 );
						Assert.AreEqual( quarter.Start.Month, 7 );
						Assert.AreEqual( quarter.Start.Day, 1 );
						Assert.AreEqual( quarter.End.Year, testDate.Year + 1 );
						Assert.AreEqual( quarter.End.Month, 9 );
						Assert.AreEqual( quarter.End.Day, 30 );
						break;
				}
			}

			// months
			ITimePeriodCollection months = year.GetMonths();
			int monthIndex = 0;
			foreach ( Month month in months )
			{
				switch ( monthIndex )
				{
					case 0:
						Assert.AreEqual( month.Start, year.Start );
						break;
					case TimeSpec.MonthsPerYear - 1:
						Assert.AreEqual( month.End, year.End );
						break;
				}

				DateTime startDate = new DateTime( year.BaseYear, year.Start.Month, 1 ).AddMonths( monthIndex );
				Assert.AreEqual( month.Start.Year, startDate.Year );
				Assert.AreEqual( month.Start.Month, startDate.Month );
				Assert.AreEqual( month.Start.Day, startDate.Day );
				Assert.AreEqual( month.End.Year, startDate.Year );
				Assert.AreEqual( month.End.Month, startDate.Month );

				monthIndex++;
			}
		} // FiscalYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarQuarterOfYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;

			TimeCalendar timeCalendar = TimeCalendar.New( YearMonth.October );
			Year calendarYear = new Year( currentYear, timeCalendar );
			Assert.AreEqual( calendarYear.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( calendarYear.BaseYear, currentYear );
			Assert.AreEqual( calendarYear.Start, new DateTime( currentYear, 10, 1 ) );
			Assert.AreEqual( calendarYear.End, calendarYear.Calendar.MapEnd( calendarYear.Start.AddYears( 1 ) ) );

			// Q1
			Quarter q1 = new Quarter( calendarYear.BaseYear, YearQuarter.First, timeCalendar );
			Assert.AreEqual( q1.YearBaseMonth, calendarYear.YearBaseMonth );
			Assert.AreEqual( q1.BaseYear, calendarYear.BaseYear );
			Assert.AreEqual( q1.Start, new DateTime( currentYear, 10, 1 ) );
			Assert.AreEqual( q1.End, q1.Calendar.MapEnd( q1.Start.AddMonths( 3 ) ) );

			// Q2
			Quarter q2 = new Quarter( calendarYear.BaseYear, YearQuarter.Second, timeCalendar );
			Assert.AreEqual( q2.YearBaseMonth, calendarYear.YearBaseMonth );
			Assert.AreEqual( q2.BaseYear, calendarYear.BaseYear );
			Assert.AreEqual( q2.Start, new DateTime( currentYear + 1, 1, 1 ) );
			Assert.AreEqual( q2.End, q2.Calendar.MapEnd( q2.Start.AddMonths( 3 ) ) );

			// Q3
			Quarter q3 = new Quarter( calendarYear.BaseYear, YearQuarter.Third, timeCalendar );
			Assert.AreEqual( q3.YearBaseMonth, calendarYear.YearBaseMonth );
			Assert.AreEqual( q3.BaseYear, calendarYear.BaseYear );
			Assert.AreEqual( q3.Start, new DateTime( currentYear + 1, 4, 1 ) );
			Assert.AreEqual( q3.End, q3.Calendar.MapEnd( q3.Start.AddMonths( 3 ) ) );

			// Q4
			Quarter q4 = new Quarter( calendarYear.BaseYear, YearQuarter.Fourth, timeCalendar );
			Assert.AreEqual( q4.YearBaseMonth, calendarYear.YearBaseMonth );
			Assert.AreEqual( q4.BaseYear, calendarYear.BaseYear );
			Assert.AreEqual( q4.Start, new DateTime( currentYear + 1, 7, 1 ) );
			Assert.AreEqual( q4.End, q4.Calendar.MapEnd( q4.Start.AddMonths( 3 ) ) );

		} // CalendarQuarterOfYearTest

	} // class BusinessCaseTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
