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
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class YearTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime thisYear = new DateTime( now.Year, 1, 1 );
			DateTime nextYear = thisYear.AddYears( 1 );
			Year year = new Year( now, TimeCalendar.NewEmptyOffset() );

			Assert.Equal( year.Start.Year, thisYear.Year );
			Assert.Equal( year.Start.Month, thisYear.Month );
			Assert.Equal( year.Start.Day, thisYear.Day );
			Assert.Equal(0, year.Start.Hour);
			Assert.Equal(0, year.Start.Minute);
			Assert.Equal(0, year.Start.Second);
			Assert.Equal(0, year.Start.Millisecond);

			Assert.Equal( year.End.Year, nextYear.Year );
			Assert.Equal( year.End.Month, nextYear.Month );
			Assert.Equal( year.End.Day, nextYear.Day );
			Assert.Equal(0, year.End.Hour);
			Assert.Equal(0, year.End.Minute);
			Assert.Equal(0, year.End.Second);
			Assert.Equal(0, year.End.Millisecond);
		} // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void DefaultCalendarTest()
		{
			DateTime yearStart = new DateTime( ClockProxy.Clock.Now.Year, 1, 1 );

			Year year = new Year( yearStart );
			Assert.Equal(YearMonth.January, year.YearBaseMonth);
			Assert.Equal( year.BaseYear, yearStart.Year );
			Assert.Equal( year.Start, yearStart.Add( year.Calendar.StartOffset ) );
			Assert.Equal( year.End, yearStart.AddYears( 1 ).Add( year.Calendar.EndOffset ) );
		} // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void YearBaseMonthTest()
		{
			Year year = new Year( TimeCalendar.New( YearMonth.April ) );
			Assert.Equal(YearMonth.April, year.YearBaseMonth);
			Assert.Equal(YearMonth.January, new Year().YearBaseMonth);
		} // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void IsCalendarYearTest()
		{
			Assert.False( new Year( TimeCalendar.New( YearMonth.April ) ).IsCalendarYear );
			Assert.True( new Year( TimeCalendar.New( YearMonth.January ) ).IsCalendarYear );
			Assert.False( new Year( 2008, TimeCalendar.New( YearMonth.April ) ).IsCalendarYear );
			Assert.True( new Year( 2008 ).IsCalendarYear );
			Assert.True( new Year().IsCalendarYear );
			Assert.True( new Year( 2008 ).IsCalendarYear );
		} // IsCalendarYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void StartYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.Equal(2008, new Year( 2008, TimeCalendar.New( YearMonth.April ) ).BaseYear);
			Assert.Equal( new Year( currentYear ).BaseYear, currentYear );
			Assert.Equal(2008, new Year( 2008 ).BaseYear);

			Assert.Equal(2007, new Year( new DateTime( 2008, 7, 20 ), TimeCalendar.New( YearMonth.October ) ).BaseYear);
			Assert.Equal(2008, new Year( new DateTime( 2008, 10, 1 ), TimeCalendar.New( YearMonth.October ) ).BaseYear);
		} // StartYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void CurrentYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Year year = new Year( currentYear );
			Assert.True( year.IsReadOnly );
			Assert.Equal( year.BaseYear, currentYear );
			Assert.Equal( year.Start, new DateTime( currentYear, 1, 1 ) );
			Assert.True( year.End < new DateTime( currentYear + 1, 1, 1 ) );
		} // CurrentYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void YearIndexTest()
		{
			const int yearIndex = 1994;
			Year year = new Year( yearIndex );
			Assert.True( year.IsReadOnly );
			Assert.Equal( year.BaseYear, yearIndex );
			Assert.Equal( year.Start, new DateTime( yearIndex, 1, 1 ) );
			Assert.True( year.End < new DateTime( yearIndex + 1, 1, 1 ) );
		} // YearIndexTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void YearMomentTest()
		{
			const int yearIndex = 2002;
			Year year = new Year( new DateTime( yearIndex, 3, 15 ) );
			Assert.True( year.IsReadOnly );
			Assert.Equal( year.BaseYear, yearIndex );
			Assert.Equal( year.Start, new DateTime( yearIndex, 1, 1 ) );
			Assert.True( year.End < new DateTime( yearIndex + 1, 1, 1 ) );
		} // YearMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void YearPeriodTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			DateTime yearStart = new DateTime( currentYear, 4, 1 );
			DateTime yearEnd = yearStart.AddYears( 1 );

			Year year = new Year( currentYear, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, YearMonth.April ) );
			Assert.True( year.IsReadOnly );
			Assert.Equal(YearMonth.April, year.YearBaseMonth);
			Assert.Equal( year.BaseYear, yearStart.Year );
			Assert.Equal( year.Start, yearStart );
			Assert.Equal( year.End, yearEnd );
		} // YearPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void YearCompareTest()
		{
			DateTime moment = new DateTime( 2008, 2, 18 );
			Year calendarYearSweden = new Year( moment, TimeCalendar.New( YearMonth.January ) );
			Assert.Equal(YearMonth.January, calendarYearSweden.YearBaseMonth);

			Year calendarYearGermany = new Year( moment, TimeCalendar.New( YearMonth.April ) );
			Assert.Equal(YearMonth.April, calendarYearGermany.YearBaseMonth);

			Year calendarYearUnitedStates = new Year( moment, TimeCalendar.New( YearMonth.October ) );
			Assert.Equal(YearMonth.October, calendarYearUnitedStates.YearBaseMonth);

			Assert.NotEqual( calendarYearSweden, calendarYearGermany );
			Assert.NotEqual( calendarYearSweden, calendarYearUnitedStates );
			Assert.NotEqual( calendarYearGermany, calendarYearUnitedStates );

			Assert.Equal( calendarYearSweden.BaseYear, calendarYearGermany.BaseYear + 1 );
			Assert.Equal( calendarYearSweden.BaseYear, calendarYearUnitedStates.BaseYear + 1 );

			Assert.Equal( calendarYearSweden.GetPreviousYear().BaseYear, calendarYearGermany.GetPreviousYear().BaseYear + 1 );
			Assert.Equal( calendarYearSweden.GetPreviousYear().BaseYear, calendarYearUnitedStates.GetPreviousYear().BaseYear + 1 );

			Assert.Equal( calendarYearSweden.GetNextYear().BaseYear, calendarYearGermany.GetNextYear().BaseYear + 1 );
			Assert.Equal( calendarYearSweden.GetNextYear().BaseYear, calendarYearUnitedStates.GetNextYear().BaseYear + 1 );

			Assert.True( calendarYearSweden.IntersectsWith( calendarYearGermany ) );
			Assert.True( calendarYearSweden.IntersectsWith( calendarYearUnitedStates ) );
			Assert.True( calendarYearGermany.IntersectsWith( calendarYearUnitedStates ) );
		} // YearCompareTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void GetPreviousYearTest()
		{
			DateTime currentYearStart = new DateTime( ClockProxy.Clock.Now.Year, 4, 1 );

			Year currentYear = new Year( currentYearStart.Year, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, YearMonth.April ) );

			Year previousYear = currentYear.GetPreviousYear();
			Assert.True( previousYear.IsReadOnly );
			Assert.Equal(YearMonth.April, previousYear.YearBaseMonth);
			Assert.Equal( previousYear.BaseYear, currentYearStart.Year - 1 );
			Assert.Equal( previousYear.Start, currentYearStart.AddYears( -1 ) );
			Assert.Equal( previousYear.End, currentYearStart );
		} // GetPreviousYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void GetNextYearTest()
		{
			DateTime currentYearStart = new DateTime( ClockProxy.Clock.Now.Year, 4, 1 );

			Year currentYear = new Year( currentYearStart.Year, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, YearMonth.April ) );

			Year nextYear = currentYear.GetNextYear();
			Assert.True( nextYear.IsReadOnly );
			Assert.Equal(YearMonth.April, nextYear.YearBaseMonth);
			Assert.Equal( nextYear.BaseYear, currentYearStart.Year + 1 );
			Assert.Equal( nextYear.Start, currentYearStart.AddYears( 1 ) );
			Assert.Equal( nextYear.End, currentYearStart.AddYears( 2 ) );
		} // GetNextYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void AddYearsTest()
		{
			Year currentYear = new Year( TimeCalendar.New( YearMonth.April ) );

			Assert.Equal( currentYear.AddYears( 0 ), currentYear );

			Year pastYear = currentYear.AddYears( -10 );
			Assert.Equal( pastYear.Start, currentYear.Start.AddYears( -10 ) );
			Assert.Equal( pastYear.End, currentYear.End.AddYears( -10 ) );

			Year futureYear = currentYear.AddYears( 10 );
			Assert.Equal( futureYear.Start, currentYear.Start.AddYears( 10 ) );
			Assert.Equal( futureYear.End, currentYear.End.AddYears( 10 ) );
		} // AddYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void GetHalfyearsTest()
		{
			Year year = new Year( TimeCalendar.New( YearMonth.October ) );

			ITimePeriodCollection halfyears = year.GetHalfyears();
			Assert.NotNull(halfyears);

			int index = 0;
			foreach ( Halfyear halfyear in halfyears )
			{
				Assert.Equal( halfyear.BaseYear, year.BaseYear );
				Assert.Equal( halfyear.Start, year.Start.AddMonths( index * TimeSpec.MonthsPerHalfyear ) );
				Assert.Equal( halfyear.End, halfyear.Calendar.MapEnd( halfyear.Start.AddMonths( TimeSpec.MonthsPerHalfyear ) ) );
				index++;
			}
			Assert.Equal( index, TimeSpec.HalfyearsPerYear );
		} // GetHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void GetQuartersTest()
		{
			Year year = new Year( TimeCalendar.New( YearMonth.October ) );

			ITimePeriodCollection quarters = year.GetQuarters();
			Assert.NotNull(quarters);

			int index = 0;
			foreach ( Quarter quarter in quarters )
			{
				Assert.Equal( quarter.BaseYear, year.BaseYear );
				Assert.Equal( quarter.Start, year.Start.AddMonths( index * TimeSpec.MonthsPerQuarter ) );
				Assert.Equal( quarter.End, quarter.Calendar.MapEnd( quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				index++;
			}
			Assert.Equal( index, TimeSpec.QuartersPerYear );
		} // GetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void GetMonthsTest()
		{
			const YearMonth startMonth = YearMonth.October;
			Year year = new Year( TimeCalendar.New( startMonth ) );

			ITimePeriodCollection months = year.GetMonths();
			Assert.NotNull(months);

			int index = 0;
			foreach ( Month month in months )
			{
				int monthYear;
				YearMonth monthMonth;
				TimeTool.AddMonth( year.YearValue, startMonth, index, out monthYear, out monthMonth );
				Assert.Equal( month.Year, monthYear );
				Assert.Equal( month.Start, year.Start.AddMonths( index ) );
				Assert.Equal( month.End, month.Calendar.MapEnd( month.Start.AddMonths( 1 ) ) );
				index++;
			}
			Assert.Equal( index, TimeSpec.MonthsPerYear );
		} // GetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
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
				Assert.Equal( expectedYear, new Year( fiscalYearStart, calendar ).YearValue );
				Assert.Equal( expectedYear, new Year( fiscalYearStart.AddTicks( 1 ), calendar ).YearValue );
				Assert.Equal( expectedYear - 1, new Year( fiscalYearStart.AddTicks( -1 ), calendar ).YearValue );
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
        [Trait("Category", "Year")]
        [Fact]
		public void FiscalYearTest()
		{
			Year year1 = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			Assert.Equal( year1.Start.Date, new DateTime( 2006, 8, 27 ) );
			Assert.Equal( year1.End.Date, new DateTime( 2007, 8, 25 ) );

			Year year2 = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			Assert.Equal( year2.Start.Date, new DateTime( 2006, 9, 3 ) );
			Assert.Equal( year2.End.Date, new DateTime( 2007, 9, 1 ) );
		} // FiscalYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void GetFiscalHalfyearsTest()
		{
			Year year = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection halfyears = year.GetHalfyears();

			Assert.NotNull(halfyears);
			Assert.Equal( halfyears.Count, TimeSpec.HalfyearsPerYear );

			Assert.Equal( halfyears[ 0 ].Start.Date, year.Start );
			Assert.Equal( halfyears[ 0 ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
			Assert.Equal( halfyears[ 1 ].Start.Date, year.Start.AddDays( TimeSpec.FiscalDaysPerHalfyear ) );
			Assert.Equal( halfyears[ 1 ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
		} // GetFiscalHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Year")]
        [Fact]
		public void GetFiscalQuartersTest()
		{
			Year year = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection quarters = year.GetQuarters();

			Assert.NotNull(quarters);
			Assert.Equal( quarters.Count, TimeSpec.QuartersPerYear );

			Assert.Equal( quarters[ 0 ].Start.Date, year.Start );
			Assert.Equal( quarters[ TimeSpec.QuartersPerYear - 1 ].End, year.End );
		} // GetFiscalQuartersTest

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "Year")]
        [Fact]
		public void FiscalYearGetMonthsTest()
		{
			Year year = new Year( 2006, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = year.GetMonths();
			Assert.NotNull(months);
			Assert.Equal( months.Count, TimeSpec.MonthsPerYear );

			Assert.Equal( months[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Assert.Equal( months[ i ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days,
					( i + 1 ) % 3 == 0 ? TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth );
			}
			Assert.Equal( months[ TimeSpec.MonthsPerYear - 1 ].End, year.End );
		} // FiscalYearGetMonthsTest

	} // class YearTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
