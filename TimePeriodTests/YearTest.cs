// -- FILE ------------------------------------------------------------------
// name       : YearTest.cs
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
	public sealed class YearTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime thisYear = new DateTime( now.Year, 1, 1 );
			DateTime nextYear = thisYear.AddYears( 1 );
			Year year = new Year( now, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( year.Start.Year, thisYear.Year );
			Assert.AreEqual( year.Start.Month, thisYear.Month );
			Assert.AreEqual( year.Start.Day, thisYear.Day );
			Assert.AreEqual( year.Start.Hour, 0 );
			Assert.AreEqual( year.Start.Minute, 0 );
			Assert.AreEqual( year.Start.Second, 0 );
			Assert.AreEqual( year.Start.Millisecond, 0 );

			Assert.AreEqual( year.End.Year, nextYear.Year );
			Assert.AreEqual( year.End.Month, nextYear.Month );
			Assert.AreEqual( year.End.Day, nextYear.Day );
			Assert.AreEqual( year.End.Hour, 0 );
			Assert.AreEqual( year.End.Minute, 0 );
			Assert.AreEqual( year.End.Second, 0 );
			Assert.AreEqual( year.End.Millisecond, 0 );
		} // InitValuesTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			DateTime yearStart = new DateTime( ClockProxy.Clock.Now.Year, 1, 1 );

			Year year = new Year( yearStart );
			Assert.AreEqual( year.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( year.BaseYear, yearStart.Year );
			Assert.AreEqual( year.Start, yearStart.Add( year.Calendar.StartOffset ) );
			Assert.AreEqual( year.End, yearStart.AddYears( 1 ).Add( year.Calendar.EndOffset ) );
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearBaseMonthTest()
		{
			Year year = new Year( TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( year.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( new Year().YearBaseMonth, YearMonth.January );
		} // YearBaseMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsCalendarYearTest()
		{
			Assert.IsFalse( new Year( TimeCalendar.New( YearMonth.April ) ).IsCalendarYear );
			Assert.IsTrue( new Year( TimeCalendar.New( YearMonth.January ) ).IsCalendarYear );
			Assert.IsFalse( new Year( 2008, TimeCalendar.New( YearMonth.April ) ).IsCalendarYear );
			Assert.IsTrue( new Year( 2008 ).IsCalendarYear );
			Assert.IsTrue( new Year().IsCalendarYear );
			Assert.IsTrue( new Year( 2008 ).IsCalendarYear );
		} // IsCalendarYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.AreEqual( new Year( 2008, TimeCalendar.New( YearMonth.April ) ).BaseYear, 2008 );
			Assert.AreEqual( new Year( currentYear ).BaseYear, currentYear );
			Assert.AreEqual( new Year( 2008 ).BaseYear, 2008 );

			Assert.AreEqual( new Year( new DateTime( 2008, 7, 20 ), TimeCalendar.New( YearMonth.October ) ).BaseYear, 2007 );
			Assert.AreEqual( new Year( new DateTime( 2008, 10, 1 ), TimeCalendar.New( YearMonth.October ) ).BaseYear, 2008 );
		} // StartYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void CurrentYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Year year = new Year( currentYear );
			Assert.IsTrue( year.IsReadOnly );
			Assert.AreEqual( year.BaseYear, currentYear );
			Assert.AreEqual( year.Start, new DateTime( currentYear, 1, 1 ) );
			Assert.Less( year.End, new DateTime( currentYear + 1, 1, 1 ) );
		} // CurrentYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearIndexTest()
		{
			const int yearIndex = 1994;
			Year year = new Year( yearIndex );
			Assert.IsTrue( year.IsReadOnly );
			Assert.AreEqual( year.BaseYear, yearIndex );
			Assert.AreEqual( year.Start, new DateTime( yearIndex, 1, 1 ) );
			Assert.Less( year.End, new DateTime( yearIndex + 1, 1, 1 ) );
		} // YearIndexTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearMomentTest()
		{
			const int yearIndex = 2002;
			Year year = new Year( new DateTime( yearIndex, 3, 15 ) );
			Assert.IsTrue( year.IsReadOnly );
			Assert.AreEqual( year.BaseYear, yearIndex );
			Assert.AreEqual( year.Start, new DateTime( yearIndex, 1, 1 ) );
			Assert.Less( year.End, new DateTime( yearIndex + 1, 1, 1 ) );
		} // YearMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearPeriodTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			DateTime yearStart = new DateTime( currentYear, 4, 1 );
			DateTime yearEnd = yearStart.AddYears( 1 );

			Year year = new Year( currentYear, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, YearMonth.April ) );
			Assert.IsTrue( year.IsReadOnly );
			Assert.AreEqual( year.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( year.BaseYear, yearStart.Year );
			Assert.AreEqual( year.Start, yearStart );
			Assert.AreEqual( year.End, yearEnd );
		} // YearPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearCompareTest()
		{
			DateTime moment = new DateTime( 2008, 2, 18 );
			Year calendarYearSweden = new Year( moment, TimeCalendar.New( YearMonth.January ) );
			Assert.AreEqual( calendarYearSweden.YearBaseMonth, YearMonth.January );

			Year calendarYearGermany = new Year( moment, TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( calendarYearGermany.YearBaseMonth, YearMonth.April );

			Year calendarYearUnitedStates = new Year( moment, TimeCalendar.New( YearMonth.October ) );
			Assert.AreEqual( calendarYearUnitedStates.YearBaseMonth, YearMonth.October );

			Assert.AreNotEqual( calendarYearSweden, calendarYearGermany );
			Assert.AreNotEqual( calendarYearSweden, calendarYearUnitedStates );
			Assert.AreNotEqual( calendarYearGermany, calendarYearUnitedStates );

			Assert.AreEqual( calendarYearSweden.BaseYear, calendarYearGermany.BaseYear + 1 );
			Assert.AreEqual( calendarYearSweden.BaseYear, calendarYearUnitedStates.BaseYear + 1 );

			Assert.AreEqual( calendarYearSweden.GetPreviousYear().BaseYear, calendarYearGermany.GetPreviousYear().BaseYear + 1 );
			Assert.AreEqual( calendarYearSweden.GetPreviousYear().BaseYear, calendarYearUnitedStates.GetPreviousYear().BaseYear + 1 );

			Assert.AreEqual( calendarYearSweden.GetNextYear().BaseYear, calendarYearGermany.GetNextYear().BaseYear + 1 );
			Assert.AreEqual( calendarYearSweden.GetNextYear().BaseYear, calendarYearUnitedStates.GetNextYear().BaseYear + 1 );

			Assert.IsTrue( calendarYearSweden.IntersectsWith( calendarYearGermany ) );
			Assert.IsTrue( calendarYearSweden.IntersectsWith( calendarYearUnitedStates ) );
			Assert.IsTrue( calendarYearGermany.IntersectsWith( calendarYearUnitedStates ) );
		} // YearCompareTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetPreviousYearTest()
		{
			DateTime currentYearStart = new DateTime( ClockProxy.Clock.Now.Year, 4, 1 );

			Year currentYear = new Year( currentYearStart.Year, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, YearMonth.April ) );

			Year previousYear = currentYear.GetPreviousYear();
			Assert.IsTrue( previousYear.IsReadOnly );
			Assert.AreEqual( previousYear.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( previousYear.BaseYear, currentYearStart.Year - 1 );
			Assert.AreEqual( previousYear.Start, currentYearStart.AddYears( -1 ) );
			Assert.AreEqual( previousYear.End, currentYearStart );
		} // GetPreviousYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetNextYearTest()
		{
			DateTime currentYearStart = new DateTime( ClockProxy.Clock.Now.Year, 4, 1 );

			Year currentYear = new Year( currentYearStart.Year, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, YearMonth.April ) );

			Year nextYear = currentYear.GetNextYear();
			Assert.IsTrue( nextYear.IsReadOnly );
			Assert.AreEqual( nextYear.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( nextYear.BaseYear, currentYearStart.Year + 1 );
			Assert.AreEqual( nextYear.Start, currentYearStart.AddYears( 1 ) );
			Assert.AreEqual( nextYear.End, currentYearStart.AddYears( 2 ) );
		} // GetNextYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddYearsTest()
		{
			Year currentYear = new Year( TimeCalendar.New( YearMonth.April ) );

			Assert.AreEqual( currentYear.AddYears( 0 ), currentYear );

			Year pastYear = currentYear.AddYears( -10 );
			Assert.AreEqual( pastYear.Start, currentYear.Start.AddYears( -10 ) );
			Assert.AreEqual( pastYear.End, currentYear.End.AddYears( -10 ) );

			Year futureYear = currentYear.AddYears( 10 );
			Assert.AreEqual( futureYear.Start, currentYear.Start.AddYears( 10 ) );
			Assert.AreEqual( futureYear.End, currentYear.End.AddYears( 10 ) );
		} // AddYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetHalfyearsTest()
		{
			Year year = new Year( TimeCalendar.New( YearMonth.October ) );

			ITimePeriodCollection halfyears = year.GetHalfyears();
			Assert.AreNotEqual( halfyears, null );

			int index = 0;
			foreach ( Halfyear halfyear in halfyears )
			{
				Assert.AreEqual( halfyear.BaseYear, year.BaseYear );
				Assert.AreEqual( halfyear.Start, year.Start.AddMonths( index * TimeSpec.MonthsPerHalfyear ) );
				Assert.AreEqual( halfyear.End, halfyear.Calendar.MapEnd( halfyear.Start.AddMonths( TimeSpec.MonthsPerHalfyear ) ) );
				index++;
			}
			Assert.AreEqual( index, TimeSpec.HalfyearsPerYear );
		} // GetHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetQuartersTest()
		{
			Year year = new Year( TimeCalendar.New( YearMonth.October ) );

			ITimePeriodCollection quarters = year.GetQuarters();
			Assert.AreNotEqual( quarters, null );

			int index = 0;
			foreach ( Quarter quarter in quarters )
			{
				Assert.AreEqual( quarter.BaseYear, year.BaseYear );
				Assert.AreEqual( quarter.Start, year.Start.AddMonths( index * TimeSpec.MonthsPerQuarter ) );
				Assert.AreEqual( quarter.End, quarter.Calendar.MapEnd( quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				index++;
			}
			Assert.AreEqual( index, TimeSpec.QuartersPerYear );
		} // GetQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthsTest()
		{
			const YearMonth startMonth = YearMonth.October;
			Year year = new Year( TimeCalendar.New( startMonth ) );

			ITimePeriodCollection months = year.GetMonths();
			Assert.AreNotEqual( months, null );

			int index = 0;
			foreach ( Month month in months )
			{
				int monthYear;
				YearMonth monthMonth;
				TimeTool.AddMonth( year.YearValue, startMonth, index, out monthYear, out monthMonth );
				Assert.AreEqual( month.Year, monthYear );
				Assert.AreEqual( month.Start, year.Start.AddMonths( index ) );
				Assert.AreEqual( month.End, month.Calendar.MapEnd( month.Start.AddMonths( 1 ) ) );
				index++;
			}
			Assert.AreEqual( index, TimeSpec.MonthsPerYear );
		} // GetMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearStartMonthTest()
		{
			int year = ClockProxy.Clock.Now.Year;
			for ( YearMonth month = YearMonth.January; month <= YearMonth.December; month++ )
			{
				int expectedYear = month < YearMonth.July ? year : year + 1;
				DateTime fiscalYearStart = new DateTime( year, (int)month, 1 );
				TimeCalendar calendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearBaseMonth = month,
						YearType = YearType.FiscalYear
					} );
				Assert.AreEqual( expectedYear, new Year( fiscalYearStart, calendar ).YearValue );
				Assert.AreEqual( expectedYear, new Year( fiscalYearStart.AddTicks( 1 ), calendar ).YearValue );
				Assert.AreEqual( expectedYear - 1, new Year( fiscalYearStart.AddTicks( -1 ), calendar ).YearValue );
			}
		} // FiscalYearStartMonthTest

		// ----------------------------------------------------------------------
		private ITimeCalendar GetFiscalYearCalendar( FiscalYearAlignment yearAlignment )
		{
			return new TimeCalendar(
				new TimeCalendarConfig
					{
						YearType = YearType.FiscalYear,
						YearBaseMonth = YearMonth.September,
						FiscalFirstDayOfYear = DayOfWeek.Sunday,
						FiscalYearAlignment = yearAlignment,
						FiscalQuarterGrouping = FiscalQuarterGrouping.FourFourFiveWeeks
					} );
		} // GetFiscalYearCalendar

		// ----------------------------------------------------------------------
		// http://en.wikipedia.org/wiki/4-4-5_Calendar
		[Test]
		public void FiscalYearTest()
		{
			Year year1 = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			Assert.AreEqual( year1.Start.Date, new DateTime( 2006, 8, 27 ) );
			Assert.AreEqual( year1.End.Date, new DateTime( 2007, 8, 25 ) );

			Year year2 = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			Assert.AreEqual( year2.Start.Date, new DateTime( 2006, 9, 3 ) );
			Assert.AreEqual( year2.End.Date, new DateTime( 2007, 9, 1 ) );
		} // FiscalYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetFiscalHalfyearsTest()
		{
			Year year = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection halfyears = year.GetHalfyears();

			Assert.AreNotEqual( halfyears, null );
			Assert.AreEqual( halfyears.Count, TimeSpec.HalfyearsPerYear );

			Assert.AreEqual( halfyears[ 0 ].Start.Date, year.Start );
			Assert.AreEqual( halfyears[ 0 ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
			Assert.AreEqual( halfyears[ 1 ].Start.Date, year.Start.AddDays( TimeSpec.FiscalDaysPerHalfyear ) );
			Assert.AreEqual( halfyears[ 1 ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
		} // GetFiscalHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetFiscalQuartersTest()
		{
			Year year = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection quarters = year.GetQuarters();

			Assert.AreNotEqual( quarters, null );
			Assert.AreEqual( quarters.Count, TimeSpec.QuartersPerYear );

			Assert.AreEqual( quarters[ 0 ].Start.Date, year.Start );
			Assert.AreEqual( quarters[ TimeSpec.QuartersPerYear - 1 ].End, year.End );
		} // GetFiscalQuartersTest

		// ----------------------------------------------------------------------
		// http://en.wikipedia.org/wiki/4-4-5_Calendar
		[Test]
		public void FiscalYearGetMonthsTest()
		{
			Year year = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = year.GetMonths();
			Assert.AreNotEqual( months, null );
			Assert.AreEqual( months.Count, TimeSpec.MonthsPerYear );

			Assert.AreEqual( months[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Assert.AreEqual( months[ i ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days,
					( i + 1 ) % 3 == 0 ? TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth );
			}
			Assert.AreEqual( months[ TimeSpec.MonthsPerYear - 1 ].End, year.End );
		} // FiscalYearGetMonthsTest

	} // class YearTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
