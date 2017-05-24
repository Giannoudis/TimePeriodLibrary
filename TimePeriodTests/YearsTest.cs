// -- FILE ------------------------------------------------------------------
// name       : YearsTest.cs
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
	
	public sealed class YearsTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void YearBaseMonthTest()
		{
			DateTime moment = new DateTime( 2009, 2, 15 );
			int year = TimeTool.GetYearOf( YearMonth.April, moment.Year, moment.Month );
			Years years = new Years( moment, 3, TimeCalendar.New( YearMonth.April ) );
			Assert.Equal(YearMonth.April, years.YearBaseMonth);
			Assert.Equal( years.Start, new DateTime( year, (int)YearMonth.April, 1 ) );
		} // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void SingleYearsTest()
		{
			const int startYear = 2004;
			Years years = new Years( startYear, 1 );

			Assert.Equal(1, years.YearCount);
			Assert.Equal( years.StartYear, startYear );
			Assert.Equal( years.EndYear, startYear );
			Assert.Equal(1, years.GetYears().Count);
			Assert.True( years.GetYears()[ 0 ].IsSamePeriod( new Year( startYear ) ) );
		} // SingleYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void DefaultCalendarYearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 3;
			Years years = new Years( startYear, yearCount );

			Assert.Equal( years.YearCount, yearCount );
			Assert.Equal( years.StartYear, startYear );
			Assert.Equal( years.EndYear, startYear + yearCount - 1 );

			int index = 0;
			foreach ( Year year in years.GetYears() )
			{
				Assert.True( year.IsSamePeriod( new Year( startYear + index ) ) );
				index++;
			}
		} // DefaultCalendarYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void CustomCalendarYearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 3;
			const int startMonth = 4;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( (YearMonth)startMonth ) );

			Assert.Equal( years.YearCount, yearCount );
			Assert.Equal( years.StartYear, startYear );
			Assert.Equal( years.EndYear, startYear + yearCount );

			int index = 0;
			foreach ( Year year in years.GetYears() )
			{
				Assert.Equal( year.Start, new DateTime( startYear + index, startMonth, 1 ) );
				index++;
			}
		} // CustomCalendarYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void GetMonthsTest()
		{
			const int startYear = 2004;
			const int yearCount = 10;
			const YearMonth startMonth = YearMonth.October;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( startMonth ) );

			ITimePeriodCollection months = years.GetMonths();
			Assert.NotNull(months);

			int index = 0;
			foreach ( Month month in months )
			{
				int monthYear;
				YearMonth monthMonth;
				TimeTool.AddMonth( startYear, startMonth, index, out monthYear, out monthMonth );
				Assert.Equal( month.Year, monthYear );
				Assert.Equal( month.Start, years.Start.AddMonths( index ) );
				Assert.Equal( month.End, month.Calendar.MapEnd( month.Start.AddMonths( 1 ) ) );
				index++;
			}
			Assert.Equal( index, yearCount * TimeSpec.MonthsPerYear );
		} // GetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void GetHalfyearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 10;
			const YearMonth startMonth = YearMonth.October;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( startMonth ) );

			ITimePeriodCollection halfyears = years.GetHalfyears();
			Assert.NotNull(halfyears);

			int index = 0;
			foreach ( Halfyear halfyear in halfyears )
			{
				int halfyearYear = startYear + ( index / TimeSpec.HalfyearsPerYear );
				Assert.Equal( halfyear.Year, halfyearYear );
				Assert.Equal( halfyear.Start, years.Start.AddMonths( index * TimeSpec.MonthsPerHalfyear ) );
				Assert.Equal( halfyear.End, halfyear.Calendar.MapEnd( halfyear.Start.AddMonths( TimeSpec.MonthsPerHalfyear ) ) );
				index++;
			}
			Assert.Equal( index, yearCount * TimeSpec.HalfyearsPerYear );
		} // GetHalfyearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void GetQuartersTest()
		{
			const int startYear = 2004;
			const int yearCount = 10;
			const YearMonth startMonth = YearMonth.October;
			const YearQuarter startQuarter = YearQuarter.Third;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( startMonth ) );

			ITimePeriodCollection quarters = years.GetQuarters();
			Assert.NotNull(quarters);

			int index = 0;
			foreach ( Quarter quarter in quarters )
			{
				int quarterYear = startYear + ( ( index + (int)startQuarter ) / TimeSpec.QuartersPerYear );
				Assert.Equal( quarter.Year, quarterYear );
				Assert.Equal( quarter.Start, years.Start.AddMonths( index * TimeSpec.MonthsPerQuarter ) );
				Assert.Equal( quarter.End, quarter.Calendar.MapEnd( quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				index++;
			}
			Assert.Equal( index, yearCount * TimeSpec.QuartersPerYear );
		} // GetQuartersTest

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
        [Trait("Category", "Years")]
        [Fact]
		public void FiscalYearsTest()
		{
			Years years1 = new Years( 2006, 13, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			Assert.Equal(13, years1.YearCount);
			Assert.Equal( years1.Start.Date, new DateTime( 2006, 8, 27 ) );
			Assert.Equal( years1.End.Date, new DateTime( 2019, 8, 31 ) );

			Years years2 = new Years( 2006, 13, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			Assert.Equal(13, years2.YearCount);
			Assert.Equal( years2.Start.Date, new DateTime( 2006, 9, 3 ) );
			Assert.Equal( years2.End.Date, new DateTime( 2019, 8, 31 ) );
		} // FiscalYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void FiscalYearsLastDayGetHalfyearsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection halfyears = years.GetHalfyears();
			Assert.NotNull(halfyears);
			Assert.Equal( halfyears.Count, yearCount * TimeSpec.HalfyearsPerYear );

			Assert.Equal( halfyears[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			foreach ( Halfyear halfyear in halfyears )
			{
				// last halfyear of a leap year
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( halfyear.YearHalfyear == YearHalfyear.Second ) && ( halfyear.Year == 2008 || halfyear.Year == 2013 || halfyear.Year == 2019 ) )
				{
					if ( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days != TimeSpec.FiscalDaysPerLeapHalfyear )
					{
						Console.WriteLine();
					}

					Assert.Equal( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapHalfyear );
				}
				else
				{
					Assert.Equal( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
				}
			}
		} // FiscalYearsLastDayGetHalfyearsTest

		// ----------------------------------------------------------------------
        [Trait("Category", "Years")]
		[Fact]
        public void FiscalYearsNearestDayGetHalfyearsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			ITimePeriodCollection halfyears = years.GetHalfyears();
			Assert.NotNull(halfyears);
			Assert.Equal( halfyears.Count, yearCount * TimeSpec.HalfyearsPerYear );

			Assert.Equal( halfyears[ 0 ].Start, new DateTime( 2006, 9, 3 ) );
			foreach ( Halfyear halfyear in halfyears )
			{
				// last halfyear of a leap year
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( halfyear.YearHalfyear == YearHalfyear.Second ) && ( halfyear.Year == 2011 || halfyear.Year == 2016 ) )
				{
					Assert.Equal( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapHalfyear );
				}
				else
				{
					Assert.Equal( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
				}
			}
		} // FiscalYearsNearestDayGetHalfyearsTest

		// ----------------------------------------------------------------------
        [Trait("Category", "Years")]
		[Fact]
        public void FiscalYearsLastDayGetQuartersTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection quarters = years.GetQuarters();
			Assert.NotNull(quarters);
			Assert.Equal( quarters.Count, yearCount * TimeSpec.QuartersPerYear );

			Assert.Equal( quarters[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			foreach ( Quarter quarter in quarters )
			{
				// last quarter of a leap year
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( quarter.YearQuarter == YearQuarter.Fourth ) && ( quarter.Year == 2008 || quarter.Year == 2013 || quarter.Year == 2019 ) )
				{
					Assert.Equal( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapQuarter );
				}
				else
				{
					Assert.Equal( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerQuarter );
				}
			}
		} // FiscalYearsLastDayGetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void FiscalYearsNearestDayGetQuartersTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			ITimePeriodCollection quarters = years.GetQuarters();
			Assert.NotNull(quarters);
			Assert.Equal( quarters.Count, yearCount * TimeSpec.QuartersPerYear );

			Assert.Equal( quarters[ 0 ].Start, new DateTime( 2006, 9, 3 ) );
			foreach ( Quarter quarter in quarters )
			{
				// last quarter of a leap year
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( quarter.YearQuarter == YearQuarter.Fourth ) && ( quarter.Year == 2011 || quarter.Year == 2016 ) )
				{
					Assert.Equal( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapQuarter );
				}
				else
				{
					Assert.Equal( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerQuarter );
				}
			}
		} // FiscalYearsNearestDayGetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void FiscalYearsLastDayGetMonthsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = years.GetMonths();
			Assert.NotNull(months);
			Assert.Equal( months.Count, yearCount * TimeSpec.MonthsPerYear );

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
		} // FiscalYearsLastDayGetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Years")]
        [Fact]
		public void FiscalYearsNearestDayGetMonthsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			ITimePeriodCollection months = years.GetMonths();
			Assert.NotNull(months);
			Assert.Equal( months.Count, yearCount * TimeSpec.MonthsPerYear );

			Assert.Equal( months[ 0 ].Start, new DateTime( 2006, 9, 3 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Month month = (Month)months[ i ];

				// last month of a leap year (6 weeks)
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( month.YearMonth == YearMonth.August ) && ( month.Year == 2011 || month.Year == 2016 ) )
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
		} // FiscalYearsNearestDayGetMonthsTest

	} // class YearsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
