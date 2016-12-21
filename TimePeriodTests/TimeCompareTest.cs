// -- FILE ------------------------------------------------------------------
// name       : TimeCompareTest.cs
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
	public sealed class TimeCompareTest : TestUnitBase
	{
		// ----------------------------------------------------------------------
		[Test]
		public void IsSameYearTest()
		{
			Assert.IsFalse( TimeCompare.IsSameYear( testDate, testDiffDate ) );
			Assert.IsTrue( TimeCompare.IsSameYear( new DateTime( 2000, 1, 1 ), new DateTime( 2000, 12, 31 ) ) );

			Assert.IsTrue( TimeCompare.IsSameYear( YearMonth.April, new DateTime( 2000, 4, 1 ), new DateTime( 2001, 3, 31 ) ) );
			Assert.IsFalse( TimeCompare.IsSameYear( YearMonth.April, new DateTime( 2000, 1, 1 ), new DateTime( 2000, 4, 1 ) ) );
		} // IsSameYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameHalfyearTest()
		{
			Assert.IsTrue( TimeCompare.IsSameHalfyear( new DateTime( 2000, 1, 1 ), new DateTime( 2000, 6, 30 ) ) );
			Assert.IsTrue( TimeCompare.IsSameHalfyear( new DateTime( 2000, 7, 1 ), new DateTime( 2000, 12, 31 ) ) );
			Assert.IsFalse( TimeCompare.IsSameHalfyear( new DateTime( 2000, 1, 1 ), new DateTime( 2000, 7, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameHalfyear( new DateTime( 2000, 7, 1 ), new DateTime( 2001, 1, 1 ) ) );

			Assert.IsTrue( TimeCompare.IsSameHalfyear( YearMonth.April, new DateTime( 2000, 4, 1 ), new DateTime( 2000, 9, 30 ) ) );
			Assert.IsTrue( TimeCompare.IsSameHalfyear( YearMonth.April, new DateTime( 2000, 10, 1 ), new DateTime( 2001, 3, 31 ) ) );
			Assert.IsFalse( TimeCompare.IsSameHalfyear( YearMonth.April, new DateTime( 2000, 4, 1 ), new DateTime( 2000, 10, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameHalfyear( YearMonth.April, new DateTime( 2000, 10, 1 ), new DateTime( 2001, 4, 1 ) ) );
		} // IsSameHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameQuarterTest()
		{
			Assert.IsTrue( TimeCompare.IsSameQuarter( new DateTime( 2000, 1, 1 ), new DateTime( 2000, 3, 31 ) ) );
			Assert.IsTrue( TimeCompare.IsSameQuarter( new DateTime( 2000, 4, 1 ), new DateTime( 2000, 6, 30 ) ) );
			Assert.IsTrue( TimeCompare.IsSameQuarter( new DateTime( 2000, 7, 1 ), new DateTime( 2000, 9, 30 ) ) );
			Assert.IsTrue( TimeCompare.IsSameQuarter( new DateTime( 2000, 10, 1 ), new DateTime( 2000, 12, 31 ) ) );

			Assert.IsFalse( TimeCompare.IsSameQuarter( new DateTime( 2000, 1, 1 ), new DateTime( 2000, 4, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameQuarter( new DateTime( 2000, 4, 1 ), new DateTime( 2000, 7, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameQuarter( new DateTime( 2000, 7, 1 ), new DateTime( 2000, 10, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameQuarter( new DateTime( 2000, 10, 1 ), new DateTime( 2001, 1, 1 ) ) );

			Assert.IsTrue( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2000, 4, 1 ), new DateTime( 2000, 6, 30 ) ) );
			Assert.IsTrue( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2000, 7, 1 ), new DateTime( 2000, 9, 30 ) ) );
			Assert.IsTrue( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2000, 10, 1 ), new DateTime( 2000, 12, 31 ) ) );
			Assert.IsTrue( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2001, 1, 1 ), new DateTime( 2001, 3, 30 ) ) );

			Assert.IsFalse( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2000, 4, 1 ), new DateTime( 2000, 7, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2000, 7, 1 ), new DateTime( 2000, 10, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2000, 10, 1 ), new DateTime( 2001, 1, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameQuarter( YearMonth.April, new DateTime( 2001, 1, 1 ), new DateTime( 2001, 4, 1 ) ) );
		} // IsSameQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameMonthTest()
		{
			Assert.IsFalse( TimeCompare.IsSameMonth( testDate, testDiffDate ) );
			Assert.IsTrue( TimeCompare.IsSameMonth( new DateTime( 2000, 10, 1 ), new DateTime( 2000, 10, 31 ) ) );
			Assert.IsTrue( TimeCompare.IsSameMonth( new DateTime( 2000, 10, 1 ), new DateTime( 2000, 10, 1 ) ) );
			Assert.IsTrue( TimeCompare.IsSameMonth( new DateTime( 2000, 10, 31 ), new DateTime( 2000, 10, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameMonth( new DateTime( 2000, 10, 1 ), new DateTime( 2000, 11, 1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameMonth( new DateTime( 2000, 10, 1 ), new DateTime( 2000, 9, 30 ) ) );
		} // IsSameMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameWeekTest()
		{
			DateTime previousWeek = testDate.AddDays( TimeSpec.DaysPerWeek + 1 );
			DateTime nextWeek = testDate.AddDays( TimeSpec.DaysPerWeek + 1 );

			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				foreach ( CalendarWeekRule weekRule in Enum.GetValues( typeof( CalendarWeekRule ) ) )
				{
					culture.DateTimeFormat.CalendarWeekRule = weekRule;

					foreach ( YearWeekType yearWeekType in Enum.GetValues( typeof( YearWeekType ) ) )
					{
						int year;
						int weekOfYear;

						TimeTool.GetWeekOfYear( testDate, culture, yearWeekType, out year, out weekOfYear );
						DateTime startOfWeek = TimeTool.GetStartOfYearWeek( year, weekOfYear, culture, yearWeekType );

						Assert.IsTrue( TimeCompare.IsSameWeek( testDate, startOfWeek, culture, yearWeekType ) );
						Assert.IsTrue( TimeCompare.IsSameWeek( testDate, testDate, culture, yearWeekType ) );
						Assert.IsTrue( TimeCompare.IsSameWeek( testDiffDate, testDiffDate, culture, yearWeekType ) );
						Assert.IsFalse( TimeCompare.IsSameWeek( testDate, testDiffDate, culture, yearWeekType ) );
						Assert.IsFalse( TimeCompare.IsSameWeek( testDate, previousWeek, culture, yearWeekType ) );
						Assert.IsFalse( TimeCompare.IsSameWeek( testDate, nextWeek, culture, yearWeekType ) );

						foreach ( DayOfWeek dayOfWeek in Enum.GetValues( typeof( DayOfWeek ) ) )
						{
							culture.DateTimeFormat.FirstDayOfWeek = dayOfWeek;
							TimeTool.GetWeekOfYear( testDate, culture, weekRule, dayOfWeek, yearWeekType, out year, out weekOfYear );
							startOfWeek = TimeTool.GetStartOfYearWeek( year, weekOfYear, culture, weekRule, dayOfWeek, yearWeekType );

							Assert.IsTrue( TimeCompare.IsSameWeek( testDate, startOfWeek, culture, yearWeekType ) );
							Assert.IsTrue( TimeCompare.IsSameWeek( testDate, testDate, culture, yearWeekType ) );
							Assert.IsTrue( TimeCompare.IsSameWeek( testDiffDate, testDiffDate, culture, yearWeekType ) );
							Assert.IsFalse( TimeCompare.IsSameWeek( testDate, testDiffDate, culture, yearWeekType ) );
							Assert.IsFalse( TimeCompare.IsSameWeek( testDate, previousWeek, culture, yearWeekType ) );
							Assert.IsFalse( TimeCompare.IsSameWeek( testDate, nextWeek, culture, yearWeekType ) );
						}
						
					}

				}
			}
		} // IsSameWeekTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameDayTest()
		{
			Assert.IsFalse( TimeCompare.IsSameDay( testDate, testDiffDate ) );
			Assert.IsTrue( TimeCompare.IsSameDay( new DateTime( 2000, 10, 19 ), new DateTime( 2000, 10, 19 ) ) );
			Assert.IsTrue( TimeCompare.IsSameDay( new DateTime( 2000, 10, 19 ), new DateTime( 2000, 10, 19 ).AddDays( 1 ).AddMilliseconds( -1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameDay( new DateTime( 1978, 10, 19 ), new DateTime( 2000, 10, 19 ) ) );
			Assert.IsFalse( TimeCompare.IsSameDay( new DateTime( 2000, 10, 18 ), new DateTime( 2000, 10, 17 ) ) );
			Assert.IsFalse( TimeCompare.IsSameDay( new DateTime( 2000, 10, 18 ), new DateTime( 2000, 10, 19 ) ) );
		} // IsSameDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameHourTest()
		{
			Assert.IsFalse( TimeCompare.IsSameHour( testDate, testDiffDate ) );
			Assert.IsTrue( TimeCompare.IsSameHour( new DateTime( 2000, 10, 19, 18, 0, 0 ), new DateTime( 2000, 10, 19, 18, 0, 0 ).AddHours( 1 ).AddMilliseconds( -1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameHour( new DateTime( 1978, 10, 19, 18, 0, 0 ), new DateTime( 2000, 10, 19, 17, 0, 0 ) ) );
			Assert.IsFalse( TimeCompare.IsSameHour( new DateTime( 1978, 10, 19, 18, 0, 0 ), new DateTime( 2000, 10, 19, 19, 0, 0 ) ) );
		} // IsSameHourTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameMinuteTest()
		{
			Assert.IsFalse( TimeCompare.IsSameMinute( testDate, testDiffDate ) );
			Assert.IsTrue( TimeCompare.IsSameMinute( new DateTime( 2000, 10, 19, 18, 20, 0 ), new DateTime( 2000, 10, 19, 18, 20, 0 ).AddMinutes( 1 ).AddMilliseconds( -1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameMinute( new DateTime( 1978, 10, 19, 18, 20, 0 ), new DateTime( 2000, 10, 19, 18, 19, 0 ) ) );
			Assert.IsFalse( TimeCompare.IsSameMinute( new DateTime( 1978, 10, 19, 18, 20, 0 ), new DateTime( 2000, 10, 19, 18, 21, 0 ) ) );
		} // IsSameMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSameSecondTest()
		{
			Assert.IsFalse( TimeCompare.IsSameSecond( testDate, testDiffDate ) );
			Assert.IsTrue( TimeCompare.IsSameSecond( new DateTime( 2000, 10, 19, 18, 20, 30 ), new DateTime( 2000, 10, 19, 18, 20, 30 ).AddSeconds( 1 ).AddMilliseconds( -1 ) ) );
			Assert.IsFalse( TimeCompare.IsSameSecond( new DateTime( 1978, 10, 19, 18, 20, 30 ), new DateTime( 2000, 10, 19, 18, 20, 29 ) ) );
			Assert.IsFalse( TimeCompare.IsSameSecond( new DateTime( 1978, 10, 19, 18, 20, 30 ), new DateTime( 2000, 10, 19, 18, 20, 31 ) ) );
		} // IsSameSecondTest

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime testDate = new DateTime( 2000, 10, 2, 13, 45, 53, 673 );
		private readonly DateTime testDiffDate = new DateTime( 2002, 9, 3, 7, 14, 22, 234 );

	} // class TimeCompareTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
