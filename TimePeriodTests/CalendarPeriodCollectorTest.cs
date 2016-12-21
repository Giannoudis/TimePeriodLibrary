// -- FILE ------------------------------------------------------------------
// name       : CalendarPeriodCollectorTest.cs
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
	public sealed class CalendarPeriodCollectorTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void CollectYearsTest()
		{
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.Years.Add( 2006 );
			filter.Years.Add( 2007 );
			filter.Years.Add( 2012 );

			CalendarTimeRange testPeriod = new CalendarTimeRange( new DateTime( 2001, 1, 1 ), new DateTime( 2019, 12, 31 ) );

			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod );
			collector.CollectYears();

			Assert.AreEqual( collector.Periods.Count, 3 );
			Assert.IsTrue( collector.Periods[ 0 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2006, 1, 1 ), new DateTime( 2007, 1, 1 ) ) ) );
			Assert.IsTrue( collector.Periods[ 1 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2007, 1, 1 ), new DateTime( 2008, 1, 1 ) ) ) );
			Assert.IsTrue( collector.Periods[ 2 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2012, 1, 1 ), new DateTime( 2013, 1, 1 ) ) ) );
		} // CollectYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CollectMonthsTest()
		{
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.Months.Add( YearMonth.January );

			CalendarTimeRange testPeriod = new CalendarTimeRange( new DateTime( 2010, 1, 1 ), new DateTime( 2011, 12, 31 ) );

			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod );
			collector.CollectMonths();

			Assert.AreEqual( collector.Periods.Count, 2 );
			Assert.IsTrue( collector.Periods[ 0 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 1 ), new DateTime( 2010, 2, 1 ) ) ) );
			Assert.IsTrue( collector.Periods[ 1 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 1 ), new DateTime( 2011, 2, 1 ) ) ) );
		} // CollectMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CollectDaysTest()
		{
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.Months.Add( YearMonth.January );
			filter.WeekDays.Add( DayOfWeek.Friday );

			CalendarTimeRange testPeriod = new CalendarTimeRange( new DateTime( 2010, 1, 1 ), new DateTime( 2011, 12, 31 ) );

			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod );
			collector.CollectDays();

			Assert.AreEqual( collector.Periods.Count, 9 );
			Assert.IsTrue( collector.Periods[ 0 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 01 ), new DateTime( 2010, 1, 02 ) ) ) );
			Assert.IsTrue( collector.Periods[ 1 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 08 ), new DateTime( 2010, 1, 09 ) ) ) );
			Assert.IsTrue( collector.Periods[ 2 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 15 ), new DateTime( 2010, 1, 16 ) ) ) );
			Assert.IsTrue( collector.Periods[ 3 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 22 ), new DateTime( 2010, 1, 23 ) ) ) );
			Assert.IsTrue( collector.Periods[ 4 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 29 ), new DateTime( 2010, 1, 30 ) ) ) );
			Assert.IsTrue( collector.Periods[ 5 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 07 ), new DateTime( 2011, 1, 08 ) ) ) );
			Assert.IsTrue( collector.Periods[ 6 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 14 ), new DateTime( 2011, 1, 15 ) ) ) );
			Assert.IsTrue( collector.Periods[ 7 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 21 ), new DateTime( 2011, 1, 22 ) ) ) );
			Assert.IsTrue( collector.Periods[ 8 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 28 ), new DateTime( 2011, 1, 29 ) ) ) );
		} // CollectDaysTest

		// ----------------------------------------------------------------------
		[Test]
		public void CollectHoursTest()
		{
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.Months.Add( YearMonth.January );
			filter.WeekDays.Add( DayOfWeek.Friday );
			filter.CollectingHours.Add( new HourRange( 8, 18 ) );

			CalendarTimeRange testPeriod = new CalendarTimeRange( new DateTime( 2010, 1, 1 ), new DateTime( 2011, 12, 31 ) );

			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod );
			collector.CollectHours();

			Assert.AreEqual( collector.Periods.Count, 9 );
			Assert.IsTrue( collector.Periods[ 0 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 01, 8, 0, 0 ), new DateTime( 2010, 1, 01, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 1 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 08, 8, 0, 0 ), new DateTime( 2010, 1, 08, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 2 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 15, 8, 0, 0 ), new DateTime( 2010, 1, 15, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 3 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 22, 8, 0, 0 ), new DateTime( 2010, 1, 22, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 4 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2010, 1, 29, 8, 0, 0 ), new DateTime( 2010, 1, 29, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 5 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 07, 8, 0, 0 ), new DateTime( 2011, 1, 07, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 6 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 14, 8, 0, 0 ), new DateTime( 2011, 1, 14, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 7 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 21, 8, 0, 0 ), new DateTime( 2011, 1, 21, 18, 0, 0 ) ) ) );
			Assert.IsTrue( collector.Periods[ 8 ].IsSamePeriod( new CalendarTimeRange( new DateTime( 2011, 1, 28, 8, 0, 0 ), new DateTime( 2011, 1, 28, 18, 0, 0 ) ) ) );
		} // CollectHoursTest

		// ----------------------------------------------------------------------
		[Test]
		public void Collect24HoursTest()
		{
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.Months.Add( YearMonth.January );
			filter.WeekDays.Add( DayOfWeek.Friday );
			filter.CollectingHours.Add( new HourRange( 8, 24 ) );

			TimeRange testPeriod = new TimeRange( new DateTime( 2010, 1, 1 ), new DateTime( 2012, 1, 1 ) );

			TimeCalendar calendar = new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } );
			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod, SeekDirection.Forward, calendar );
			collector.CollectHours();

			Assert.AreEqual( collector.Periods.Count, 9 );
			Assert.IsTrue( collector.Periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 01, 8, 0, 0 ), new DateTime( 2010, 1, 02 ) ) ) );
			Assert.IsTrue( collector.Periods[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 08, 8, 0, 0 ), new DateTime( 2010, 1, 09 ) ) ) );
			Assert.IsTrue( collector.Periods[ 2 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 15, 8, 0, 0 ), new DateTime( 2010, 1, 16 ) ) ) );
			Assert.IsTrue( collector.Periods[ 3 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 22, 8, 0, 0 ), new DateTime( 2010, 1, 23 ) ) ) );
			Assert.IsTrue( collector.Periods[ 4 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 29, 8, 0, 0 ), new DateTime( 2010, 1, 30 ) ) ) );
			Assert.IsTrue( collector.Periods[ 5 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 07, 8, 0, 0 ), new DateTime( 2011, 1, 08 ) ) ) );
			Assert.IsTrue( collector.Periods[ 6 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 14, 8, 0, 0 ), new DateTime( 2011, 1, 15 ) ) ) );
			Assert.IsTrue( collector.Periods[ 7 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 21, 8, 0, 0 ), new DateTime( 2011, 1, 22 ) ) ) );
			Assert.IsTrue( collector.Periods[ 8 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 28, 8, 0, 0 ), new DateTime( 2011, 1, 29 ) ) ) );
		} // Collect24HoursTest

		// ----------------------------------------------------------------------
		[Test]
		public void CollectAllDayHoursTest()
		{
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			filter.Months.Add( YearMonth.January );
			filter.WeekDays.Add( DayOfWeek.Friday );
			filter.CollectingHours.Add( new HourRange( 0, 24 ) );

			TimeRange testPeriod = new TimeRange( new DateTime( 2010, 1, 1 ), new DateTime( 2012, 1, 1 ) );

			TimeCalendar calendar = new TimeCalendar( new TimeCalendarConfig { EndOffset = TimeSpan.Zero } );
			CalendarPeriodCollector collector = new CalendarPeriodCollector( filter, testPeriod, SeekDirection.Forward, calendar );
			collector.CollectHours();

			Assert.AreEqual( collector.Periods.Count, 9 );
			Assert.IsTrue( collector.Periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 01 ), new DateTime( 2010, 1, 02 ) ) ) );
			Assert.IsTrue( collector.Periods[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 08 ), new DateTime( 2010, 1, 09 ) ) ) );
			Assert.IsTrue( collector.Periods[ 2 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 15 ), new DateTime( 2010, 1, 16 ) ) ) );
			Assert.IsTrue( collector.Periods[ 3 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 22 ), new DateTime( 2010, 1, 23 ) ) ) );
			Assert.IsTrue( collector.Periods[ 4 ].IsSamePeriod( new TimeRange( new DateTime( 2010, 1, 29 ), new DateTime( 2010, 1, 30 ) ) ) );
			Assert.IsTrue( collector.Periods[ 5 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 07 ), new DateTime( 2011, 1, 08 ) ) ) );
			Assert.IsTrue( collector.Periods[ 6 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 14 ), new DateTime( 2011, 1, 15 ) ) ) );
			Assert.IsTrue( collector.Periods[ 7 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 21 ), new DateTime( 2011, 1, 22 ) ) ) );
			Assert.IsTrue( collector.Periods[ 8 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 1, 28 ), new DateTime( 2011, 1, 29 ) ) ) );
		} // CollectAllDayHoursTest
		
		// ----------------------------------------------------------------------
		[Test]
		public void CollectExcludePeriodTest()
		{
			const int workingDays2011 = 365 - 2 - ( 51 * 2 ) - 1;
			const int workingDaysMarch2011 = 31 - 8; // total days - weekend days

			Year year2011 = new Year( 2011 );

			CalendarPeriodCollectorFilter filter1 = new CalendarPeriodCollectorFilter();
			filter1.AddWorkingWeekDays();
			CalendarPeriodCollector collector1 = new CalendarPeriodCollector( filter1, year2011 );
			collector1.CollectDays();
			Assert.AreEqual( collector1.Periods.Count, workingDays2011 );

			// exclude month
			CalendarPeriodCollectorFilter filter2 = new CalendarPeriodCollectorFilter();
			filter2.AddWorkingWeekDays();
			filter2.ExcludePeriods.Add( new Month( 2011, YearMonth.March ) );
			CalendarPeriodCollector collector2 = new CalendarPeriodCollector( filter2, year2011 );
			collector2.CollectDays();
			Assert.AreEqual( collector2.Periods.Count, workingDays2011 - workingDaysMarch2011 );

			// exclude weeks (holidays)
			CalendarPeriodCollectorFilter filter3 = new CalendarPeriodCollectorFilter();
			filter3.AddWorkingWeekDays();
			filter3.ExcludePeriods.Add( new Month( 2011, YearMonth.March ) );
			filter3.ExcludePeriods.Add( new Weeks( 2011, 26, 2 ) );
			CalendarPeriodCollector collector3 = new CalendarPeriodCollector( filter3, year2011 );
			collector3.CollectDays();
			Assert.AreEqual( collector3.Periods.Count, workingDays2011 - workingDaysMarch2011 - 10 );
		} // CollectExcludePeriodTest

	} // class CalendarPeriodCollectorTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
