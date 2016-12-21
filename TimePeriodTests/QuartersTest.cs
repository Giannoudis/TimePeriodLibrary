// -- FILE ------------------------------------------------------------------
// name       : QuartersTest.cs
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
	public sealed class QuartersTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void YearBaseMonthTest()
		{
			DateTime moment = new DateTime( 2009, 2, 15 );
			int year = TimeTool.GetYearOf( YearMonth.April, moment.Year, moment.Month );
			Quarters quarters = new Quarters( moment, YearQuarter.First, 3, TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( quarters.Start, new DateTime( year, (int)YearMonth.April, 1 ) );
		} // YearBaseMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void SingleQuartersTest()
		{
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Second;
			Quarters quarters = new Quarters( startYear, startQuarter, 1 );

			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( quarters.QuarterCount, 1 );
			Assert.AreEqual( quarters.StartQuarter, startQuarter );
			Assert.AreEqual( quarters.StartYear, startYear );
			Assert.AreEqual( quarters.EndYear, startYear );
			Assert.AreEqual( quarters.EndQuarter, YearQuarter.Second );
			Assert.AreEqual( quarters.GetQuarters().Count, 1 );
			Assert.IsTrue( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second ) ) );
		} // SingleQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstCalendarQuartersTest()
		{
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.First;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount );

			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( quarters.QuarterCount, quarterCount );
			Assert.AreEqual( quarters.StartQuarter, startQuarter );
			Assert.AreEqual( quarters.StartYear, startYear );
			Assert.AreEqual( quarters.EndYear, 2005 );
			Assert.AreEqual( quarters.EndQuarter, YearQuarter.First );
			Assert.AreEqual( quarters.GetQuarters().Count, quarterCount );
			Assert.IsTrue( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.First ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First ) ) );
		} // FirstCalendarQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondCalendarQuartersTest()
		{
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Second;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount );

			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( quarters.QuarterCount, quarterCount );
			Assert.AreEqual( quarters.StartQuarter, startQuarter );
			Assert.AreEqual( quarters.StartYear, startYear );
			Assert.AreEqual( quarters.EndYear, 2005 );
			Assert.AreEqual( quarters.EndQuarter, YearQuarter.Second );
			Assert.AreEqual( quarters.GetQuarters().Count, quarterCount );
			Assert.IsTrue( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second ) ) );
		} // SecondCalendarQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.First;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( quarters.QuarterCount, quarterCount );
			Assert.AreEqual( quarters.StartQuarter, startQuarter );
			Assert.AreEqual( quarters.StartYear, startYear );
			Assert.AreEqual( quarters.EndYear, 2005 );
			Assert.AreEqual( quarters.EndQuarter, YearQuarter.First );
			Assert.AreEqual( quarters.GetQuarters().Count, quarterCount );
			Assert.IsTrue( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.First, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
		} // FirstCustomCalendarQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Second;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( quarters.QuarterCount, quarterCount );
			Assert.AreEqual( quarters.StartQuarter, startQuarter );
			Assert.AreEqual( quarters.StartYear, startYear );
			Assert.AreEqual( quarters.EndYear, 2005 );
			Assert.AreEqual( quarters.EndQuarter, YearQuarter.Second );
			Assert.AreEqual( quarters.GetQuarters().Count, quarterCount );
			Assert.IsTrue( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second, calendar ) ) );
		} // SecondCustomCalendarQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void ThirdCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Third;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( quarters.QuarterCount, quarterCount );
			Assert.AreEqual( quarters.StartQuarter, startQuarter );
			Assert.AreEqual( quarters.StartYear, startYear );
			Assert.AreEqual( quarters.EndYear, 2005 );
			Assert.AreEqual( quarters.EndQuarter, YearQuarter.Third );
			Assert.AreEqual( quarters.GetQuarters().Count, quarterCount );
			Assert.IsTrue( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Third, calendar ) ) );
		} // ThirdCustomCalendarQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void FourthCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Fourth;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.AreEqual( quarters.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( quarters.QuarterCount, quarterCount );
			Assert.AreEqual( quarters.StartQuarter, startQuarter );
			Assert.AreEqual( quarters.StartYear, startYear );
			Assert.AreEqual( quarters.EndYear, 2005 );
			Assert.AreEqual( quarters.EndQuarter, YearQuarter.Fourth );
			Assert.AreEqual( quarters.GetQuarters().Count, quarterCount );
			Assert.IsTrue( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Third, calendar ) ) );
			Assert.IsTrue( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Fourth, calendar ) ) );
		} // FourthCustomCalendarQuartersTest

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
		public void FiscalYearGetMonthsTest()
		{
			const int quarterCount = 8;
			Quarters halfyears = new Quarters( 2006, YearQuarter.First, quarterCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = halfyears.GetMonths();
			Assert.AreNotEqual( months, null );
			Assert.AreEqual( months.Count, TimeSpec.MonthsPerQuarter * quarterCount );

			Assert.AreEqual( months[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Month month = (Month)months[ i ];

				// last month of a leap year (6 weeks)
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( month.YearMonth == YearMonth.August ) && ( month.Year == 2008 || month.Year == 2013 || month.Year == 2019 ) )
				{
					Assert.AreEqual( month.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapMonth );
				}
				else if ( ( i + 1 ) % 3 == 0 ) // first and second month of quarter (4 weeks)
				{
					Assert.AreEqual( month.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLongMonth );
				}
				else // third month of quarter (5 weeks)
				{
					Assert.AreEqual( month.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerShortMonth );
				}
			}
			Assert.AreEqual( months[ ( TimeSpec.MonthsPerQuarter * quarterCount ) - 1 ].End, halfyears.End );
		} // FiscalYearGetMonthsTest

	} // class QuartersTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
