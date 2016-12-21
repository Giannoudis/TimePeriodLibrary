// -- FILE ------------------------------------------------------------------
// name       : NowTest.cs
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
	public sealed class NowTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarYearTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime calendarYear = Now.CalendarYear;
			Assert.AreEqual( calendarYear.Year, now.Year );
			Assert.AreEqual( calendarYear.Month, 1 );
			Assert.AreEqual( calendarYear.Day, 1 );
			Assert.AreEqual( calendarYear.Hour, 0 );
			Assert.AreEqual( calendarYear.Minute, 0 );
			Assert.AreEqual( calendarYear.Second, 0 );
			Assert.AreEqual( calendarYear.Millisecond, 0 );
		} // CalendarYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarHalfyearTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime calendarHalfyear = Now.CalendarHalfyear;
			int halfyear = ( ( now.Month - 1 ) / TimeSpec.MonthsPerHalfyear ) + 1;
			int halfyearMonth = ( ( halfyear - 1 ) * TimeSpec.MonthsPerHalfyear ) + 1;
			Assert.AreEqual( calendarHalfyear.Year, now.Year );
			Assert.AreEqual( calendarHalfyear.Month, halfyearMonth );
			Assert.AreEqual( calendarHalfyear.Day, 1 );
			Assert.AreEqual( calendarHalfyear.Hour, 0 );
			Assert.AreEqual( calendarHalfyear.Minute, 0 );
			Assert.AreEqual( calendarHalfyear.Second, 0 );
			Assert.AreEqual( calendarHalfyear.Millisecond, 0 );
		} // CalendarHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarQuarterTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime calendarQuarter = Now.CalendarQuarter;
			int quarter = ( ( now.Month - 1 ) / TimeSpec.MonthsPerQuarter ) + 1;
			int quarterMonth = ( ( quarter - 1 ) * TimeSpec.MonthsPerQuarter ) + 1;
			Assert.AreEqual( calendarQuarter.Year, now.Year );
			Assert.AreEqual( calendarQuarter.Month, quarterMonth );
			Assert.AreEqual( calendarQuarter.Day, 1 );
			Assert.AreEqual( calendarQuarter.Hour, 0 );
			Assert.AreEqual( calendarQuarter.Minute, 0 );
			Assert.AreEqual( calendarQuarter.Second, 0 );
			Assert.AreEqual( calendarQuarter.Millisecond, 0 );
		} // CalendarQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void MonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime month = Now.Month;
			Assert.AreEqual( month.Year, now.Year );
			Assert.AreEqual( month.Month, now.Month );
			Assert.AreEqual( month.Day, 1 );
			Assert.AreEqual( month.Hour, 0 );
			Assert.AreEqual( month.Minute, 0 );
			Assert.AreEqual( month.Second, 0 );
			Assert.AreEqual( month.Millisecond, 0 );
		} // MonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			Assert.AreEqual( (int)Now.YearMonth, now.Month );
		} // YearMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void TodayTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime today = Now.Today;
			Assert.AreEqual( today.Year, now.Year );
			Assert.AreEqual( today.Month, now.Month );
			Assert.AreEqual( today.Day, now.Day );
			Assert.AreEqual( today.Hour, 0 );
			Assert.AreEqual( today.Minute, 0 );
			Assert.AreEqual( today.Second, 0 );
			Assert.AreEqual( today.Millisecond, 0 );
		} // TodayTest

		// ----------------------------------------------------------------------
		[Test]
		public void HourTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime hour = Now.Hour;
			Assert.AreEqual( hour.Year, now.Year );
			Assert.AreEqual( hour.Month, now.Month );
			Assert.AreEqual( hour.Day, now.Day );
			Assert.AreEqual( hour.Hour, now.Hour );
			Assert.AreEqual( hour.Minute, 0 );
			Assert.AreEqual( hour.Second, 0 );
			Assert.AreEqual( hour.Millisecond, 0 );
		} // HourTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinuteTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime minute = Now.Minute;
			Assert.AreEqual( minute.Year, now.Year );
			Assert.AreEqual( minute.Month, now.Month );
			Assert.AreEqual( minute.Day, now.Day );
			Assert.AreEqual( minute.Hour, now.Hour );
			Assert.AreEqual( minute.Minute, now.Minute );
			Assert.AreEqual( minute.Second, 0 );
			Assert.AreEqual( minute.Millisecond, 0 );
		} // MinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime second = Now.Second;
			Assert.AreEqual( second.Year, now.Year );
			Assert.AreEqual( second.Month, now.Month );
			Assert.AreEqual( second.Day, now.Day );
			Assert.AreEqual( second.Hour, now.Hour );
			Assert.AreEqual( second.Minute, now.Minute );
			Assert.AreEqual( second.Second, now.Second );
			Assert.AreEqual( second.Millisecond, 0 );
		} // SecondTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			DateTime year = Now.Year( (YearMonth)now.Month );
			Assert.AreEqual( year.Year, now.Year );
			Assert.AreEqual( year.Month, now.Month );
			Assert.AreEqual( year.Day, 1 );
			Assert.AreEqual( year.Hour, 0 );
			Assert.AreEqual( year.Minute, 0 );
			Assert.AreEqual( year.Second, 0 );
			Assert.AreEqual( year.Millisecond, 0 );

			int testYear;
			YearMonth previousMonth;
			TimeTool.PreviousMonth( (YearMonth)now.Month, out testYear, out previousMonth );
			DateTime previousYear = Now.Year( previousMonth );
			Assert.AreEqual( previousYear.Year, now.AddMonths( -1 ).Year );
			Assert.AreEqual( previousYear.Month, now.AddMonths( -1 ).Month );
			Assert.AreEqual( previousYear.Day, 1 );
			Assert.AreEqual( previousYear.Hour, 0 );
			Assert.AreEqual( previousYear.Minute, 0 );
			Assert.AreEqual( previousYear.Second, 0 );
			Assert.AreEqual( previousYear.Millisecond, 0 );
		} // YearTest

		// ----------------------------------------------------------------------
		[Test]
		public void HalfyearTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			DateTime halfyear = Now.Halfyear( (YearMonth)now.Month );
			Assert.AreEqual( halfyear.Year, now.Year );
			Assert.AreEqual( halfyear.Month, now.Month );
			Assert.AreEqual( halfyear.Day, 1 );
			Assert.AreEqual( halfyear.Hour, 0 );
			Assert.AreEqual( halfyear.Minute, 0 );
			Assert.AreEqual( halfyear.Second, 0 );
			Assert.AreEqual( halfyear.Millisecond, 0 );

			int testYear;
			YearMonth previousMonth;
			TimeTool.PreviousMonth( (YearMonth)now.Month, out testYear, out previousMonth );
			DateTime previousHalfyear = Now.Halfyear( previousMonth );
			Assert.AreEqual( previousHalfyear.Year, now.AddMonths( -1 ).Year );
			Assert.AreEqual( previousHalfyear.Month, now.AddMonths( -1 ).Month );
			Assert.AreEqual( previousHalfyear.Day, 1 );
			Assert.AreEqual( previousHalfyear.Hour, 0 );
			Assert.AreEqual( previousHalfyear.Minute, 0 );
			Assert.AreEqual( previousHalfyear.Second, 0 );
			Assert.AreEqual( previousHalfyear.Millisecond, 0 );
		} // HalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void WeekTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			DateTime week = Now.Week( now.DayOfWeek );
			Assert.AreEqual( week.Year, now.Year );
			Assert.AreEqual( week.Month, now.Month );
			Assert.AreEqual( week.Day, now.Day );
			Assert.AreEqual( week.DayOfWeek, now.DayOfWeek );
			Assert.AreEqual( week.Hour, 0 );
			Assert.AreEqual( week.Minute, 0 );
			Assert.AreEqual( week.Second, 0 );
			Assert.AreEqual( week.Millisecond, 0 );

			DayOfWeek previousDay = TimeTool.PreviousDay( now.DayOfWeek );
			DateTime previousWeek = Now.Week( previousDay );
			Assert.AreEqual( previousWeek.Year, now.AddDays( -1 ).Year );
			Assert.AreEqual( previousWeek.Month, now.AddDays( -1 ).Month );
			Assert.AreEqual( previousWeek.Day, now.AddDays( -1 ).Day );
			Assert.AreEqual( previousWeek.DayOfWeek, previousDay );
			Assert.AreEqual( previousWeek.Hour, 0 );
			Assert.AreEqual( previousWeek.Minute, 0 );
			Assert.AreEqual( previousWeek.Second, 0 );
			Assert.AreEqual( previousWeek.Millisecond, 0 );
		} // WeekTest

	} // class NowTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
