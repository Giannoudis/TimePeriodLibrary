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
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class QuartersTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void YearBaseMonthTest()
		{
			DateTime moment = new DateTime( 2009, 2, 15 );
			int year = TimeTool.GetYearOf( YearMonth.April, moment.Year, moment.Month );
			Quarters quarters = new Quarters( moment, YearQuarter.First, 3, TimeCalendar.New( YearMonth.April ) );
			Assert.Equal(YearMonth.April, quarters.YearBaseMonth);
			Assert.Equal( quarters.Start, new DateTime( year, (int)YearMonth.April, 1 ) );
		} // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void SingleQuartersTest()
		{
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Second;
			Quarters quarters = new Quarters( startYear, startQuarter, 1 );

			Assert.Equal(YearMonth.January, quarters.YearBaseMonth);
			Assert.Equal(1, quarters.QuarterCount);
			Assert.Equal( quarters.StartQuarter, startQuarter );
			Assert.Equal( quarters.StartYear, startYear );
			Assert.Equal( quarters.EndYear, startYear );
			Assert.Equal(YearQuarter.Second, quarters.EndQuarter);
			Assert.Equal(1, quarters.GetQuarters().Count);
			Assert.True( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second ) ) );
		} // SingleQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void FirstCalendarQuartersTest()
		{
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.First;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount );

			Assert.Equal(YearMonth.January, quarters.YearBaseMonth);
			Assert.Equal( quarters.QuarterCount, quarterCount );
			Assert.Equal( quarters.StartQuarter, startQuarter );
			Assert.Equal( quarters.StartYear, startYear );
			Assert.Equal(2005, quarters.EndYear);
			Assert.Equal(YearQuarter.First, quarters.EndQuarter);
			Assert.Equal( quarters.GetQuarters().Count, quarterCount );
			Assert.True( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.First ) ) );
			Assert.True( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second ) ) );
			Assert.True( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third ) ) );
			Assert.True( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth ) ) );
			Assert.True( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First ) ) );
		} // FirstCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void SecondCalendarQuartersTest()
		{
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Second;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount );

			Assert.Equal(YearMonth.January, quarters.YearBaseMonth);
			Assert.Equal( quarters.QuarterCount, quarterCount );
			Assert.Equal( quarters.StartQuarter, startQuarter );
			Assert.Equal( quarters.StartYear, startYear );
			Assert.Equal(2005, quarters.EndYear);
			Assert.Equal(YearQuarter.Second, quarters.EndQuarter);
			Assert.Equal( quarters.GetQuarters().Count, quarterCount );
			Assert.True( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second ) ) );
			Assert.True( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third ) ) );
			Assert.True( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth ) ) );
			Assert.True( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First ) ) );
			Assert.True( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second ) ) );
		} // SecondCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void FirstCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.First;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
			Assert.Equal( quarters.QuarterCount, quarterCount );
			Assert.Equal( quarters.StartQuarter, startQuarter );
			Assert.Equal( quarters.StartYear, startYear );
			Assert.Equal(2005, quarters.EndYear);
			Assert.Equal(YearQuarter.First, quarters.EndQuarter);
			Assert.Equal( quarters.GetQuarters().Count, quarterCount );
			Assert.True( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.First, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
		} // FirstCustomCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void SecondCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Second;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
			Assert.Equal( quarters.QuarterCount, quarterCount );
			Assert.Equal( quarters.StartQuarter, startQuarter );
			Assert.Equal( quarters.StartYear, startYear );
			Assert.Equal(2005, quarters.EndYear);
			Assert.Equal(YearQuarter.Second, quarters.EndQuarter);
			Assert.Equal( quarters.GetQuarters().Count, quarterCount );
			Assert.True( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Second, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second, calendar ) ) );
		} // SecondCustomCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void ThirdCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Third;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
			Assert.Equal( quarters.QuarterCount, quarterCount );
			Assert.Equal( quarters.StartQuarter, startQuarter );
			Assert.Equal( quarters.StartYear, startYear );
			Assert.Equal(2005, quarters.EndYear);
			Assert.Equal(YearQuarter.Third, quarters.EndQuarter);
			Assert.Equal( quarters.GetQuarters().Count, quarterCount );
			Assert.True( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Third, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Third, calendar ) ) );
		} // ThirdCustomCalendarQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Quarters")]
        [Fact]
		public void FourthCustomCalendarQuartersTest()
		{
			TimeCalendar calendar = TimeCalendar.New( YearMonth.October );
			const int startYear = 2004;
			const YearQuarter startQuarter = YearQuarter.Fourth;
			const int quarterCount = 5;
			Quarters quarters = new Quarters( startYear, startQuarter, quarterCount, calendar );

			Assert.Equal(YearMonth.October, quarters.YearBaseMonth);
			Assert.Equal( quarters.QuarterCount, quarterCount );
			Assert.Equal( quarters.StartQuarter, startQuarter );
			Assert.Equal( quarters.StartYear, startYear );
			Assert.Equal(2005, quarters.EndYear);
			Assert.Equal(YearQuarter.Fourth, quarters.EndQuarter);
			Assert.Equal( quarters.GetQuarters().Count, quarterCount );
			Assert.True( quarters.GetQuarters()[ 0 ].IsSamePeriod( new Quarter( 2004, YearQuarter.Fourth, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 1 ].IsSamePeriod( new Quarter( 2005, YearQuarter.First, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 2 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Second, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 3 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Third, calendar ) ) );
			Assert.True( quarters.GetQuarters()[ 4 ].IsSamePeriod( new Quarter( 2005, YearQuarter.Fourth, calendar ) ) );
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
        [Trait("Category", "Quarters")]
        [Fact]
		public void FiscalYearGetMonthsTest()
		{
			const int quarterCount = 8;
			Quarters halfyears = new Quarters( 2006, YearQuarter.First, quarterCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = halfyears.GetMonths();
			Assert.NotNull(months);
			Assert.Equal( months.Count, TimeSpec.MonthsPerQuarter * quarterCount );

			Assert.Equal( months[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Month month = (Month)months[ i ];

				// last month of a leap year (6 weeks)
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( month.YearMonth == YearMonth.August ) && ( month.Year == 2008 || month.Year == 2013 || month.Year == 2019 ) )
				{
					Assert.Equal( month.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapMonth );
				}
				else if ( ( i + 1 ) % 3 == 0 ) // first and second month of quarter (4 weeks)
				{
					Assert.Equal( month.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLongMonth );
				}
				else // third month of quarter (5 weeks)
				{
					Assert.Equal( month.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerShortMonth );
				}
			}
			Assert.Equal( months[ ( TimeSpec.MonthsPerQuarter * quarterCount ) - 1 ].End, halfyears.End );
		} // FiscalYearGetMonthsTest

	} // class QuartersTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
