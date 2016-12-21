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
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class YearsTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void YearBaseMonthTest()
		{
			DateTime moment = new DateTime( 2009, 2, 15 );
			int year = TimeTool.GetYearOf( YearMonth.April, moment.Year, moment.Month );
			Years years = new Years( moment, 3, TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( years.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( years.Start, new DateTime( year, (int)YearMonth.April, 1 ) );
		} // YearBaseMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void SingleYearsTest()
		{
			const int startYear = 2004;
			Years years = new Years( startYear, 1 );

			Assert.AreEqual( years.YearCount, 1 );
			Assert.AreEqual( years.StartYear, startYear );
			Assert.AreEqual( years.EndYear, startYear );
			Assert.AreEqual( years.GetYears().Count, 1 );
			Assert.IsTrue( years.GetYears()[ 0 ].IsSamePeriod( new Year( startYear ) ) );
		} // SingleYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarYearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 3;
			Years years = new Years( startYear, yearCount );

			Assert.AreEqual( years.YearCount, yearCount );
			Assert.AreEqual( years.StartYear, startYear );
			Assert.AreEqual( years.EndYear, startYear + yearCount - 1 );

			int index = 0;
			foreach ( Year year in years.GetYears() )
			{
				Assert.IsTrue( year.IsSamePeriod( new Year( startYear + index ) ) );
				index++;
			}
		} // DefaultCalendarYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CustomCalendarYearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 3;
			const int startMonth = 4;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( (YearMonth)startMonth ) );

			Assert.AreEqual( years.YearCount, yearCount );
			Assert.AreEqual( years.StartYear, startYear );
			Assert.AreEqual( years.EndYear, startYear + yearCount );

			int index = 0;
			foreach ( Year year in years.GetYears() )
			{
				Assert.AreEqual( year.Start, new DateTime( startYear + index, startMonth, 1 ) );
				index++;
			}
		} // CustomCalendarYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthsTest()
		{
			const int startYear = 2004;
			const int yearCount = 10;
			const YearMonth startMonth = YearMonth.October;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( startMonth ) );

			ITimePeriodCollection months = years.GetMonths();
			Assert.AreNotEqual( months, null );

			int index = 0;
			foreach ( Month month in months )
			{
				int monthYear;
				YearMonth monthMonth;
				TimeTool.AddMonth( startYear, startMonth, index, out monthYear, out monthMonth );
				Assert.AreEqual( month.Year, monthYear );
				Assert.AreEqual( month.Start, years.Start.AddMonths( index ) );
				Assert.AreEqual( month.End, month.Calendar.MapEnd( month.Start.AddMonths( 1 ) ) );
				index++;
			}
			Assert.AreEqual( index, yearCount * TimeSpec.MonthsPerYear );
		} // GetMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetHalfyearsTest()
		{
			const int startYear = 2004;
			const int yearCount = 10;
			const YearMonth startMonth = YearMonth.October;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( startMonth ) );

			ITimePeriodCollection halfyears = years.GetHalfyears();
			Assert.AreNotEqual( halfyears, null );

			int index = 0;
			foreach ( Halfyear halfyear in halfyears )
			{
				int halfyearYear = startYear + ( index / TimeSpec.HalfyearsPerYear );
				Assert.AreEqual( halfyear.Year, halfyearYear );
				Assert.AreEqual( halfyear.Start, years.Start.AddMonths( index * TimeSpec.MonthsPerHalfyear ) );
				Assert.AreEqual( halfyear.End, halfyear.Calendar.MapEnd( halfyear.Start.AddMonths( TimeSpec.MonthsPerHalfyear ) ) );
				index++;
			}
			Assert.AreEqual( index, yearCount * TimeSpec.HalfyearsPerYear );
		} // GetHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetQuartersTest()
		{
			const int startYear = 2004;
			const int yearCount = 10;
			const YearMonth startMonth = YearMonth.October;
			const YearQuarter startQuarter = YearQuarter.Third;
			Years years = new Years( startYear, yearCount, TimeCalendar.New( startMonth ) );

			ITimePeriodCollection quarters = years.GetQuarters();
			Assert.AreNotEqual( quarters, null );

			int index = 0;
			foreach ( Quarter quarter in quarters )
			{
				int quarterYear = startYear + ( ( index + (int)startQuarter ) / TimeSpec.QuartersPerYear );
				Assert.AreEqual( quarter.Year, quarterYear );
				Assert.AreEqual( quarter.Start, years.Start.AddMonths( index * TimeSpec.MonthsPerQuarter ) );
				Assert.AreEqual( quarter.End, quarter.Calendar.MapEnd( quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				index++;
			}
			Assert.AreEqual( index, yearCount * TimeSpec.QuartersPerYear );
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
		[Test]
		public void FiscalYearsTest()
		{
			Years years1 = new Years( 2006, 13, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			Assert.AreEqual( years1.YearCount, 13 );
			Assert.AreEqual( years1.Start.Date, new DateTime( 2006, 8, 27 ) );
			Assert.AreEqual( years1.End.Date, new DateTime( 2019, 8, 31 ) );

			Years years2 = new Years( 2006, 13, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			Assert.AreEqual( years2.YearCount, 13 );
			Assert.AreEqual( years2.Start.Date, new DateTime( 2006, 9, 3 ) );
			Assert.AreEqual( years2.End.Date, new DateTime( 2019, 8, 31 ) );
		} // FiscalYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearsLastDayGetHalfyearsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection halfyears = years.GetHalfyears();
			Assert.AreNotEqual( halfyears, null );
			Assert.AreEqual( halfyears.Count, yearCount * TimeSpec.HalfyearsPerYear );

			Assert.AreEqual( halfyears[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
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

					Assert.AreEqual( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapHalfyear );
				}
				else
				{
					Assert.AreEqual( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
				}
			}
		} // FiscalYearsLastDayGetHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearsNearestDayGetHalfyearsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			ITimePeriodCollection halfyears = years.GetHalfyears();
			Assert.AreNotEqual( halfyears, null );
			Assert.AreEqual( halfyears.Count, yearCount * TimeSpec.HalfyearsPerYear );

			Assert.AreEqual( halfyears[ 0 ].Start, new DateTime( 2006, 9, 3 ) );
			foreach ( Halfyear halfyear in halfyears )
			{
				// last halfyear of a leap year
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( halfyear.YearHalfyear == YearHalfyear.Second ) && ( halfyear.Year == 2011 || halfyear.Year == 2016 ) )
				{
					Assert.AreEqual( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapHalfyear );
				}
				else
				{
					Assert.AreEqual( halfyear.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerHalfyear );
				}
			}
		} // FiscalYearsNearestDayGetHalfyearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearsLastDayGetQuartersTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection quarters = years.GetQuarters();
			Assert.AreNotEqual( quarters, null );
			Assert.AreEqual( quarters.Count, yearCount * TimeSpec.QuartersPerYear );

			Assert.AreEqual( quarters[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			foreach ( Quarter quarter in quarters )
			{
				// last quarter of a leap year
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( quarter.YearQuarter == YearQuarter.Fourth ) && ( quarter.Year == 2008 || quarter.Year == 2013 || quarter.Year == 2019 ) )
				{
					Assert.AreEqual( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapQuarter );
				}
				else
				{
					Assert.AreEqual( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerQuarter );
				}
			}
		} // FiscalYearsLastDayGetQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearsNearestDayGetQuartersTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			ITimePeriodCollection quarters = years.GetQuarters();
			Assert.AreNotEqual( quarters, null );
			Assert.AreEqual( quarters.Count, yearCount * TimeSpec.QuartersPerYear );

			Assert.AreEqual( quarters[ 0 ].Start, new DateTime( 2006, 9, 3 ) );
			foreach ( Quarter quarter in quarters )
			{
				// last quarter of a leap year
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( quarter.YearQuarter == YearQuarter.Fourth ) && ( quarter.Year == 2011 || quarter.Year == 2016 ) )
				{
					Assert.AreEqual( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerLeapQuarter );
				}
				else
				{
					Assert.AreEqual( quarter.Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days, TimeSpec.FiscalDaysPerQuarter );
				}
			}
		} // FiscalYearsNearestDayGetQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearsLastDayGetMonthsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = years.GetMonths();
			Assert.AreNotEqual( months, null );
			Assert.AreEqual( months.Count, yearCount * TimeSpec.MonthsPerYear );

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
		} // FiscalYearsLastDayGetMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void FiscalYearsNearestDayGetMonthsTest()
		{
			const int yearCount = 13;
			Years years = new Years( 2006, yearCount, GetFiscalYearCalendar( FiscalYearAlignment.NearestDay ) );
			ITimePeriodCollection months = years.GetMonths();
			Assert.AreNotEqual( months, null );
			Assert.AreEqual( months.Count, yearCount * TimeSpec.MonthsPerYear );

			Assert.AreEqual( months[ 0 ].Start, new DateTime( 2006, 9, 3 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Month month = (Month)months[ i ];

				// last month of a leap year (6 weeks)
				// http://en.wikipedia.org/wiki/4-4-5_Calendar
				if ( ( month.YearMonth == YearMonth.August ) && ( month.Year == 2011 || month.Year == 2016 ) )
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
		} // FiscalYearsNearestDayGetMonthsTest

	} // class YearsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
