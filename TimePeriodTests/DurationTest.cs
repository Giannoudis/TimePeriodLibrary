// -- FILE ------------------------------------------------------------------
// name       : DurationTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class DurationTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void YearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Calendar calendar = DateDiff.SafeCurrentInfo.Calendar;

			Assert.AreEqual( Duration.Year( currentYear ), new TimeSpan( calendar.GetDaysInYear( currentYear ), 0, 0, 0 ) );
			Assert.AreEqual( Duration.Year( currentYear + 1 ), new TimeSpan( calendar.GetDaysInYear( currentYear + 1 ), 0, 0, 0 ) );
			Assert.AreEqual( Duration.Year( currentYear - 1 ), new TimeSpan( calendar.GetDaysInYear( currentYear - 1 ), 0, 0, 0 ) );

			Assert.AreEqual( Duration.Year( calendar, currentYear ), new TimeSpan( calendar.GetDaysInYear( currentYear ), 0, 0, 0 ) );
			Assert.AreEqual( Duration.Year( calendar, currentYear + 1 ), new TimeSpan( calendar.GetDaysInYear( currentYear + 1 ), 0, 0, 0 ) );
			Assert.AreEqual( Duration.Year( calendar, currentYear - 1 ), new TimeSpan( calendar.GetDaysInYear( currentYear - 1 ), 0, 0, 0 ) );
		} // YearTest

		// ----------------------------------------------------------------------
		[Test]
		public void HalfyearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Calendar calendar = DateDiff.SafeCurrentInfo.Calendar;

			foreach ( YearHalfyear yearHalfyear in Enum.GetValues( typeof( YearHalfyear ) ) )
			{
				YearMonth[] halfyearMonths = TimeTool.GetMonthsOfHalfyear( yearHalfyear );
				TimeSpan duration = TimeSpan.Zero;
				foreach ( YearMonth halfyearMonth in halfyearMonths )
				{
					int monthDays = calendar.GetDaysInMonth( currentYear, (int)halfyearMonth );
					duration = duration.Add( new TimeSpan( monthDays, 0, 0, 0 ) );
				}

				Assert.AreEqual( Duration.Halfyear( currentYear, yearHalfyear ), duration );
				Assert.AreEqual( Duration.Halfyear( calendar, currentYear, yearHalfyear ), duration );
			}
		} // HalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void QuarterTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Calendar calendar = DateDiff.SafeCurrentInfo.Calendar;

			foreach ( YearQuarter yearQuarter in Enum.GetValues( typeof( YearQuarter ) ) )
			{
				YearMonth[] quarterMonths = TimeTool.GetMonthsOfQuarter( yearQuarter );
				TimeSpan duration = TimeSpan.Zero;
				foreach ( YearMonth quarterMonth in quarterMonths )
				{
					int monthDays = calendar.GetDaysInMonth( currentYear, (int)quarterMonth );
					duration = duration.Add( new TimeSpan( monthDays, 0, 0, 0 ) );
				}

				Assert.AreEqual( Duration.Quarter( currentYear, yearQuarter ), duration );
				Assert.AreEqual( Duration.Quarter( calendar, currentYear, yearQuarter ), duration );
			}
		} // QuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void MonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			int currentYear = now.Year;
			Calendar calendar = DateDiff.SafeCurrentInfo.Calendar;

			foreach ( YearMonth yearMonth in Enum.GetValues( typeof( YearMonth ) ) )
			{
				Assert.AreEqual( Duration.Month( currentYear, yearMonth ), new TimeSpan( calendar.GetDaysInMonth( currentYear, (int)yearMonth ), 0, 0, 0 ) );
				Assert.AreEqual( Duration.Month( calendar, currentYear, yearMonth ), new TimeSpan( calendar.GetDaysInMonth( currentYear, (int)yearMonth ), 0, 0, 0 ) );
			}
		} // MonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void WeekTest()
		{
			Assert.AreEqual( Duration.Week, new TimeSpan( TimeSpec.DaysPerWeek * 1, 0, 0, 0 ) );

			Assert.AreEqual( Duration.Weeks( 0 ), TimeSpan.Zero );
			Assert.AreEqual( Duration.Weeks( 1 ), new TimeSpan( TimeSpec.DaysPerWeek * 1, 0, 0, 0 ) );
			Assert.AreEqual( Duration.Weeks( 2 ), new TimeSpan( TimeSpec.DaysPerWeek * 2, 0, 0, 0 ) );
			Assert.AreEqual( Duration.Weeks( -1 ), new TimeSpan( TimeSpec.DaysPerWeek * -1, 0, 0, 0 ) );
			Assert.AreEqual( Duration.Weeks( -2 ), new TimeSpan( TimeSpec.DaysPerWeek * -2, 0, 0, 0 ) );
		} // WeekTest

		// ----------------------------------------------------------------------
		[Test]
		public void DayTest()
		{
			Assert.AreEqual( Duration.Day, new TimeSpan( 1, 0, 0, 0 ) );

			Assert.AreEqual( Duration.Days( 0 ), TimeSpan.Zero );
			Assert.AreEqual( Duration.Days( 1 ), new TimeSpan( 1, 0, 0, 0 ) );
			Assert.AreEqual( Duration.Days( 2 ), new TimeSpan( 2, 0, 0, 0 ) );
			Assert.AreEqual( Duration.Days( -1 ), new TimeSpan( -1, 0, 0, 0 ) );
			Assert.AreEqual( Duration.Days( -2 ), new TimeSpan( -2, 0, 0, 0 ) );

			Assert.AreEqual( Duration.Days( 1, 23 ), new TimeSpan( 1, 23, 0, 0 ) );
			Assert.AreEqual( Duration.Days( 1, 23, 22 ), new TimeSpan( 1, 23, 22, 0 ) );
			Assert.AreEqual( Duration.Days( 1, 23, 22, 18 ), new TimeSpan( 1, 23, 22, 18 ) );
			Assert.AreEqual( Duration.Days( 1, 23, 22, 18, 875 ), new TimeSpan( 1, 23, 22, 18, 875 ) );
		} // DayTest

		// ----------------------------------------------------------------------
		[Test]
		public void HourTest()
		{
			Assert.AreEqual( Duration.Hour, new TimeSpan( 1, 0, 0 ) );

			Assert.AreEqual( Duration.Hours( 0 ), TimeSpan.Zero );
			Assert.AreEqual( Duration.Hours( 1 ), new TimeSpan( 1, 0, 0 ) );
			Assert.AreEqual( Duration.Hours( 2 ), new TimeSpan( 2, 0, 0 ) );
			Assert.AreEqual( Duration.Hours( -1 ), new TimeSpan( -1, 0, 0 ) );
			Assert.AreEqual( Duration.Hours( -2 ), new TimeSpan( -2, 0, 0 ) );

			Assert.AreEqual( Duration.Hours( 23 ), new TimeSpan( 0, 23, 0, 0 ) );
			Assert.AreEqual( Duration.Hours( 23, 22 ), new TimeSpan( 0, 23, 22, 0 ) );
			Assert.AreEqual( Duration.Hours( 23, 22, 18 ), new TimeSpan( 0, 23, 22, 18 ) );
			Assert.AreEqual( Duration.Hours( 23, 22, 18, 875 ), new TimeSpan( 0, 23, 22, 18, 875 ) );
		} // HourTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinuteTest()
		{
			Assert.AreEqual( Duration.Minute, new TimeSpan( 0, 1, 0 ) );

			Assert.AreEqual( Duration.Minutes( 0 ), TimeSpan.Zero );
			Assert.AreEqual( Duration.Minutes( 1 ), new TimeSpan( 0, 1, 0 ) );
			Assert.AreEqual( Duration.Minutes( 2 ), new TimeSpan( 0, 2, 0 ) );
			Assert.AreEqual( Duration.Minutes( -1 ), new TimeSpan( 0, -1, 0 ) );
			Assert.AreEqual( Duration.Minutes( -2 ), new TimeSpan( 0, -2, 0 ) );

			Assert.AreEqual( Duration.Minutes( 22 ), new TimeSpan( 0, 0, 22, 0 ) );
			Assert.AreEqual( Duration.Minutes( 22, 18 ), new TimeSpan( 0, 0, 22, 18 ) );
			Assert.AreEqual( Duration.Minutes( 22, 18, 875 ), new TimeSpan( 0, 0, 22, 18, 875 ) );
		} // MinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondTest()
		{
			Assert.AreEqual( Duration.Second, new TimeSpan( 0, 0, 1 ) );

			Assert.AreEqual( Duration.Seconds( 0 ), TimeSpan.Zero );
			Assert.AreEqual( Duration.Seconds( 1 ), new TimeSpan( 0, 0, 1 ) );
			Assert.AreEqual( Duration.Seconds( 2 ), new TimeSpan( 0, 0, 2 ) );
			Assert.AreEqual( Duration.Seconds( -1 ), new TimeSpan( 0, 0, -1 ) );
			Assert.AreEqual( Duration.Seconds( -2 ), new TimeSpan( 0, 0, -2 ) );

			Assert.AreEqual( Duration.Seconds( 18 ), new TimeSpan( 0, 0, 0, 18 ) );
			Assert.AreEqual( Duration.Seconds( 18, 875 ), new TimeSpan( 0, 0, 0, 18, 875 ) );
		} // SecondTest

		// ----------------------------------------------------------------------
		[Test]
		public void MillisecondTest()
		{
			Assert.AreEqual( Duration.Millisecond, new TimeSpan( 0, 0, 0, 0, 1 ) );

			Assert.AreEqual( Duration.Milliseconds( 0 ), TimeSpan.Zero );
			Assert.AreEqual( Duration.Milliseconds( 1 ), new TimeSpan( 0, 0, 0, 0, 1 ) );
			Assert.AreEqual( Duration.Milliseconds( 2 ), new TimeSpan( 0, 0, 0, 0, 2 ) );
			Assert.AreEqual( Duration.Milliseconds( -1 ), new TimeSpan( 0, 0, 0, 0, -1 ) );
			Assert.AreEqual( Duration.Milliseconds( -2 ), new TimeSpan( 0, 0, 0, 0, -2 ) );
		} // MillisecondTest

	} // class DurationTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
