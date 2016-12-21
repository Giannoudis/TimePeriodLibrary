// -- FILE ------------------------------------------------------------------
// name       : HalfyearsTest.cs
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
	public sealed class HalfyearsTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void YearBaseMonthTest()
		{
			DateTime moment = new DateTime( 2009, 2, 15 );
			int year = TimeTool.GetYearOf( YearMonth.April, moment.Year, moment.Month );
			Halfyears halfyears = new Halfyears( moment, YearHalfyear.First, 3, TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( halfyears.Start, new DateTime( year, (int)YearMonth.April, 1 ) );
		} // YearBaseMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void SingleHalfyearsTest()
		{
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.Second;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, 1 );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( halfyears.HalfyearCount, 1 );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, startYear );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.Second );
			Assert.AreEqual( halfyears.GetHalfyears().Count, 1 );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second ) ) );
		} // SingleHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstCalendarHalfyearsTest()
		{
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.First;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.First );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.First ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First ) ) );
		} // FirstCalendarHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondCalendarHalfyearsTest()
		{
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.Second;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.January );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.Second );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.Second ) ) );
		} // SecondCalendarHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstCustomCalendarHalfyearsTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.First;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount, calendar );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.First );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.First, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First, calendar ) ) );
		} // FirstCustomCalendarHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondCustomCalendarHalfyearsTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearHalfyear startHalfyear = YearHalfyear.Second;
			const int halfyearCount = 3;
			Halfyears halfyears = new Halfyears( startYear, startHalfyear, halfyearCount, calendar );

			Assert.AreEqual( halfyears.YearBaseMonth, YearMonth.October );
			Assert.AreEqual( halfyears.HalfyearCount, halfyearCount );
			Assert.AreEqual( halfyears.StartHalfyear, startHalfyear );
			Assert.AreEqual( halfyears.StartYear, startYear );
			Assert.AreEqual( halfyears.EndYear, 2005 );
			Assert.AreEqual( halfyears.EndHalfyear, YearHalfyear.Second );
			Assert.AreEqual( halfyears.GetHalfyears().Count, halfyearCount );
			Assert.IsTrue( halfyears.GetHalfyears()[ 0 ].IsSamePeriod( new Halfyear( 2004, YearHalfyear.Second, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 1 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.First, calendar ) ) );
			Assert.IsTrue( halfyears.GetHalfyears()[ 2 ].IsSamePeriod( new Halfyear( 2005, YearHalfyear.Second, calendar ) ) );
		} // SecondCustomCalendarHalfyearsTest

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
		public void GetFiscalQuartersTest()
		{
			const int halfyearCount = 4;
			Halfyears halfyears = new Halfyears( 2006, YearHalfyear.First, halfyearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection quarters = halfyears.GetQuarters();

			Assert.AreNotEqual( quarters, null );
			Assert.AreEqual( quarters.Count, TimeSpec.QuartersPerHalfyear * halfyearCount );

			Assert.AreEqual( quarters[ 0 ].Start.Date, halfyears.Start );
			Assert.AreEqual( quarters[ ( TimeSpec.QuartersPerHalfyear * halfyearCount ) - 1 ].End, halfyears.End );
		} // GetFiscalQuartersTest

		// ----------------------------------------------------------------------
		// http://en.wikipedia.org/wiki/4-4-5_Calendar
		[Test]
		public void FiscalYearGetMonthsTest()
		{
			const int halfyearCount = 4;
			Halfyears halfyears = new Halfyears( 2006, YearHalfyear.First, halfyearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = halfyears.GetMonths();
			Assert.AreNotEqual( months, null );
			Assert.AreEqual( months.Count, TimeSpec.MonthsPerHalfyear * halfyearCount );

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
			Assert.AreEqual( months[ ( TimeSpec.MonthsPerHalfyear * halfyearCount ) - 1 ].End, halfyears.End );
		} // FiscalYearGetMonthsTest

	} // class HalfyearsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
