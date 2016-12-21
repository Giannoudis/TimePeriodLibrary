// -- FILE ------------------------------------------------------------------
// name       : MonthTest.cs
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
	public sealed class MonthTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstMonth = new DateTime( now.Year, now.Month, 1 );
			DateTime secondMonth = firstMonth.AddMonths( 1 );
			Month month = new Month( now.Year, (YearMonth)now.Month, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( month.Start.Year, firstMonth.Year );
			Assert.AreEqual( month.Start.Month, firstMonth.Month );
			Assert.AreEqual( month.Start.Day, firstMonth.Day );
			Assert.AreEqual( month.Start.Hour, 0 );
			Assert.AreEqual( month.Start.Minute, 0 );
			Assert.AreEqual( month.Start.Second, 0 );
			Assert.AreEqual( month.Start.Millisecond, 0 );

			Assert.AreEqual( month.End.Year, secondMonth.Year );
			Assert.AreEqual( month.End.Month, secondMonth.Month );
			Assert.AreEqual( month.End.Day, secondMonth.Day );
			Assert.AreEqual( month.End.Hour, 0 );
			Assert.AreEqual( month.End.Minute, 0 );
			Assert.AreEqual( month.End.Second, 0 );
			Assert.AreEqual( month.End.Millisecond, 0 );
		} // InitValuesTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			DateTime yearStart = new DateTime( ClockProxy.Clock.Now.Year, 1, 1 );
			foreach ( YearMonth yearMonth in Enum.GetValues( typeof( YearMonth ) ) )
			{
				int offset = (int)yearMonth - 1;
				Month month = new Month( yearStart.AddMonths( offset ) );
				Assert.AreEqual( month.Year, yearStart.Year );
				Assert.AreEqual( month.YearMonth, yearMonth );
				Assert.AreEqual( month.Start, yearStart.AddMonths( offset ).Add( month.Calendar.StartOffset ) );
				Assert.AreEqual( month.End, yearStart.AddMonths( offset + 1 ).Add( month.Calendar.EndOffset ) );
			}
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void CurrentMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				DateTime currentYearMonthStart = new DateTime( now.Year, now.Month, 1 );
				DateTime currentYearMonthEnd =
					currentYearMonthStart.AddDays( culture.Calendar.GetDaysInMonth( currentYearMonthStart.Year,
																																					currentYearMonthStart.Month ) );
				Month month = new Month( currentYearMonthStart, TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ) );

				Assert.AreEqual( month.Year, now.Year );
				Assert.AreEqual( month.YearMonth, (YearMonth)now.Month );
				Assert.AreEqual( month.Start, currentYearMonthStart );
				Assert.AreEqual( month.End, currentYearMonthEnd );
			}
		} // CurrentMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearMonthTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero );

			Month january = new Month( currentYear, TimeSpec.CalendarYearStartMonth, calendar );
			Assert.AreEqual( january.YearMonth, TimeSpec.CalendarYearStartMonth );
			Assert.AreEqual( january.Start, new DateTime( currentYear, 1, 1 ) );
			Assert.AreEqual( january.End, new DateTime( currentYear, 2, 1 ) );
		} // YearMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void MonthMomentTest()
		{
			Month month = new Month( new DateTime( 2008, 1, 15 ), TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero ) );
			Assert.IsTrue( month.IsReadOnly );
			Assert.AreEqual( month.Year, 2008 );
			Assert.AreEqual( month.YearMonth, TimeSpec.CalendarYearStartMonth );
			Assert.AreEqual( month.Start, new DateTime( 2008, 1, 1 ) );
			Assert.AreEqual( month.End, new DateTime( 2008, 2, 1 ) );

			Month previous = month.GetPreviousMonth();
			Assert.AreEqual( previous.Year, 2007 );
			Assert.AreEqual( previous.YearMonth, YearMonth.December );
			Assert.AreEqual( previous.Start, new DateTime( 2007, 12, 1 ) );
			Assert.AreEqual( previous.End, new DateTime( 2008, 1, 1 ) );

			Month next = month.GetNextMonth();
			Assert.AreEqual( next.Year, 2008 );
			Assert.AreEqual( next.YearMonth, YearMonth.February );
			Assert.AreEqual( next.Start, new DateTime( 2008, 2, 1 ) );
			Assert.AreEqual( next.End, new DateTime( 2008, 3, 1 ) );
		} // MonthMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void LastDayStartTest()
		{
			Assert.AreEqual( new Month( 2001, YearMonth.January ).LastDayStart, new DateTime( 2001, 1, 31 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.February ).LastDayStart, new DateTime( 2001, 2, 28 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.March ).LastDayStart, new DateTime( 2001, 3, 31 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.April ).LastDayStart, new DateTime( 2001, 4, 30 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.May ).LastDayStart, new DateTime( 2001, 5, 31 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.June ).LastDayStart, new DateTime( 2001, 6, 30 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.July ).LastDayStart, new DateTime( 2001, 7, 31 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.August ).LastDayStart, new DateTime( 2001, 8, 31 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.September ).LastDayStart, new DateTime( 2001, 9, 30 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.October ).LastDayStart, new DateTime( 2001, 10, 31 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.November ).LastDayStart, new DateTime( 2001, 11, 30 ) );
			Assert.AreEqual( new Month( 2001, YearMonth.December ).LastDayStart, new DateTime( 2001, 12, 31 ) );

			Assert.AreEqual( new Month( 2000, YearMonth.February ).LastDayStart, new DateTime( 2000, 2, 29 ) );
		} // LastDayStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDaysTest()
		{
			Month month = new Month();

			ITimePeriodCollection days = month.GetDays();
			Assert.AreNotEqual( days, null );

			int index = 0;
			foreach ( Day day in days )
			{
				Assert.AreEqual( day.Start, month.Start.AddDays( index ) );
				Assert.AreEqual( day.End, day.Calendar.MapEnd( day.Start.AddDays( 1 ) ) );
				index++;
			}
		} // GetDaysTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetPreviousMonthTest()
		{
			Month month = new Month();
			Assert.AreEqual( month.GetPreviousMonth(), month.AddMonths( -1 ) );
		} // GetPreviousMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetNextMonthTest()
		{
			Month month = new Month();
			Assert.AreEqual( month.GetNextMonth(), month.AddMonths( 1 ) );
		} // GetNextMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddMonthsTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{

				DateTime currentYearMonthStart = new DateTime( now.Year, now.Month, 1 );
				DateTime currentYearMonthEnd =
					currentYearMonthStart.AddDays( culture.Calendar.GetDaysInMonth( currentYearMonthStart.Year,
																																					currentYearMonthStart.Month ) );
				Month currentYearMonth = new Month( currentYearMonthStart,
																						TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ) );
				Assert.AreEqual( currentYearMonth.Start, currentYearMonthStart );
				Assert.AreEqual( currentYearMonth.End, currentYearMonthEnd );

				Assert.AreEqual( currentYearMonth.AddMonths( 0 ), currentYearMonth );

				DateTime previousYearMonthStart = new DateTime( now.Year - 1, now.Month, 1 );
				DateTime previousYearMonthEnd =
					previousYearMonthStart.AddDays( culture.Calendar.GetDaysInMonth( previousYearMonthStart.Year,
																																					 previousYearMonthStart.Month ) );
				Month previousYearMonth = currentYearMonth.AddMonths( TimeSpec.MonthsPerYear * -1 );
				Assert.AreEqual( previousYearMonth.Start, previousYearMonthStart );
				Assert.AreEqual( previousYearMonth.End, previousYearMonthEnd );
				Assert.AreEqual( previousYearMonth.YearMonth, currentYearMonth.YearMonth );
				Assert.AreEqual( previousYearMonth.Start.Year, currentYearMonth.Start.Year - 1 );
				Assert.AreEqual( previousYearMonth.End.Year, currentYearMonth.End.Year - 1 );

				DateTime nextYearMonthStart = new DateTime( now.Year + 1, now.Month, 1 );
				DateTime nextYearMonthEnd =
					nextYearMonthStart.AddDays( culture.Calendar.GetDaysInMonth( nextYearMonthStart.Year, nextYearMonthStart.Month ) );
				Month nextYearMonth = currentYearMonth.AddMonths( TimeSpec.MonthsPerYear );
				Assert.AreEqual( nextYearMonth.Start, nextYearMonthStart );
				Assert.AreEqual( nextYearMonth.End, nextYearMonthEnd );
				Assert.AreEqual( nextYearMonth.YearMonth, currentYearMonth.YearMonth );
				Assert.AreEqual( nextYearMonth.Start.Year, currentYearMonth.Start.Year + 1 );
				Assert.AreEqual( nextYearMonth.End.Year, currentYearMonth.End.Year + 1 );
			}
		} // AddMonthsTest

	} // class MonthTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
