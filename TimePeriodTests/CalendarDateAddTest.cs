// -- FILE ------------------------------------------------------------------
// name       : CalendarDateAddTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.04
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
	public sealed class CalendarDateAddTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void NoPeriodsTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			Assert.AreEqual( calendarDateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 1, 0, 0, 0 ) ), test.Add( new TimeSpan( 1, 0, 0, 0 ) ) );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( -1, 0, 0, 0 ) ), test.Add( new TimeSpan( -1, 0, 0, 0 ) ) );
			Assert.AreEqual( calendarDateAdd.Subtract( test, new TimeSpan( 1, 0, 0, 0 ) ), test.Subtract( new TimeSpan( 1, 0, 0, 0 ) ) );
			Assert.AreEqual( calendarDateAdd.Subtract( test, new TimeSpan( -1, 0, 0, 0 ) ), test.Subtract( new TimeSpan( -1, 0, 0, 0 ) ) );
		} // NoPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodLimitsAddTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 20 ), new DateTime( 2011, 4, 25 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( 2011, 4, 30 ), DateTime.MaxValue );
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			calendarDateAdd.ExcludePeriods.Add( timeRange1 );
			calendarDateAdd.ExcludePeriods.Add( timeRange2 );

			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 8, 0, 0, 0 ) ), timeRange1.End );
			Assert.IsNull( calendarDateAdd.Add( test, new TimeSpan( 20, 0, 0, 0 ) ) );
		} // PeriodLimitsAddTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodLimitsSubtractTest()
		{
			DateTime test = new DateTime( 2011, 4, 30 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 20 ), new DateTime( 2011, 4, 25 ) );
			TimeRange timeRange2 = new TimeRange( DateTime.MinValue, new DateTime( 2011, 4, 10 ) );
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			calendarDateAdd.ExcludePeriods.Add( timeRange1 );
			calendarDateAdd.ExcludePeriods.Add( timeRange2 );

			Assert.AreEqual( calendarDateAdd.Subtract( test, new TimeSpan( 5, 0, 0, 0 ) ), timeRange1.Start );
			Assert.IsNull( calendarDateAdd.Subtract( test, new TimeSpan( 20, 0, 0, 0 ) ) );
		} // PeriodLimitsSubtractTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExcludeTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange = new TimeRange( new DateTime( 2011, 4, 15 ), new DateTime( 2011, 4, 20 ) );
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			calendarDateAdd.ExcludePeriods.Add( timeRange );

			Assert.AreEqual( calendarDateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 2, 0, 0, 0 ) ), test.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 3, 0, 0, 0 ) ), timeRange.End );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 3, 0, 0, 0, 1 ) ), timeRange.End.Add( new TimeSpan( 0, 0, 0, 0, 1 ) ) );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 5, 0, 0, 0 ) ), timeRange.End.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
		} // ExcludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExcludeSplitTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 15 ), new DateTime( 2011, 4, 20 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( 2011, 4, 22 ), new DateTime( 2011, 4, 25 ) );
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			calendarDateAdd.ExcludePeriods.Add( timeRange1 );
			calendarDateAdd.ExcludePeriods.Add( timeRange2 );

			Assert.AreEqual( calendarDateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 2, 0, 0, 0 ) ), test.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 3, 0, 0, 0 ) ), timeRange1.End );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 4, 0, 0, 0 ) ), timeRange1.End.Add( new TimeSpan( 1, 0, 0, 0 ) ) );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 5, 0, 0, 0 ) ), timeRange2.End );
			Assert.AreEqual( calendarDateAdd.Add( test, new TimeSpan( 7, 0, 0, 0 ) ), timeRange2.End.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
		} // ExcludeSplitTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarDateAddSeekBoundaryModeTest()
		{
			TimeCalendar timeCalendar = new TimeCalendar( new TimeCalendarConfig
			{
				Culture = new CultureInfo( "en-AU" ),
				EndOffset = TimeSpan.Zero
			} );

			CalendarDateAdd calendarDateAdd = new CalendarDateAdd( timeCalendar );
			calendarDateAdd.AddWorkingWeekDays();
			calendarDateAdd.ExcludePeriods.Add( new Day( 2011, 4, 4, calendarDateAdd.Calendar ) );
			calendarDateAdd.WorkingHours.Add( new HourRange( 8, 18 ) );

			DateTime start = new DateTime( 2011, 4, 1, 9, 0, 0 );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 29, 0, 0 ), SeekBoundaryMode.Fill ), new DateTime( 2011, 4, 6, 18, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 29, 0, 0 ) ), new DateTime( 2011, 4, 7, 8, 0, 0 ) );
		} // CalendarDateAddSeekBoundaryModeTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarDateAdd1Test()
		{
			TimeCalendar timeCalendar = new TimeCalendar( new TimeCalendarConfig
			{
				Culture = new CultureInfo( "en-AU" ),
				EndOffset = TimeSpan.Zero
			} );

			CalendarDateAdd calendarDateAdd = new CalendarDateAdd( timeCalendar );
			calendarDateAdd.AddWorkingWeekDays();
			calendarDateAdd.ExcludePeriods.Add( new Day( 2011, 4, 4, calendarDateAdd.Calendar ) );
			calendarDateAdd.WorkingHours.Add( new HourRange( 8, 18 ) );

			DateTime start = new DateTime( 2011, 4, 1, 9, 0, 0 );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 22, 0, 0 ) ), new DateTime( 2011, 4, 6, 11, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 29, 0, 0 ) ), new DateTime( 2011, 4, 7, 8, 0, 0 ) );
		} // CalendarDateAdd1Test

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarDateAdd2Test()
		{
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			calendarDateAdd.AddWorkingWeekDays();
			calendarDateAdd.ExcludePeriods.Add( new Day( 2011, 4, 4, calendarDateAdd.Calendar ) );
			calendarDateAdd.WorkingHours.Add( new HourRange( 8, 12 ) );
			calendarDateAdd.WorkingHours.Add( new HourRange( 13, 18 ) );

			DateTime start = new DateTime( 2011, 4, 1, 9, 0, 0 );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 03, 0, 0 ) ), new DateTime( 2011, 4, 1, 13, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 04, 0, 0 ) ), new DateTime( 2011, 4, 1, 14, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 08, 0, 0 ) ), new DateTime( 2011, 4, 5, 08, 0, 0 ) );
		} // CalendarDateAdd2Test

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarDateAdd3Test()
		{
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			calendarDateAdd.AddWorkingWeekDays();
			calendarDateAdd.ExcludePeriods.Add( new Day( 2011, 4, 4, calendarDateAdd.Calendar ) );
			calendarDateAdd.WorkingHours.Add( new HourRange( new Time( 8, 30 ), new Time( 12 ) ) );
			calendarDateAdd.WorkingHours.Add( new HourRange( new Time( 13, 30 ), new Time( 18 ) ) );

			DateTime start = new DateTime( 2011, 4, 1, 9, 0, 0 );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 03, 0, 0 ) ), new DateTime( 2011, 4, 1, 13, 30, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 04, 0, 0 ) ), new DateTime( 2011, 4, 1, 14, 30, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 08, 0, 0 ) ), new DateTime( 2011, 4, 5, 09, 00, 0 ) );
		} // CalendarDateAdd3Test

		// ----------------------------------------------------------------------
		[Test]
		public void EmptyStartWeekTest()
		{
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();
			// weekdays
			calendarDateAdd.AddWorkingWeekDays();
			//Start on a Saturday
			DateTime start = new DateTime( 2011, 4, 2, 13, 0, 0 );
			TimeSpan offset = new TimeSpan( 20, 0, 0 ); // 20 hours

			Assert.AreEqual( calendarDateAdd.Add( start, offset ), new DateTime( 2011, 4, 4, 20, 00, 0 ) );
		} // EmptyStartWeekTest

		// ----------------------------------------------------------------------
		[Test]
		public void WorkingDayHoursTest()
		{
			CalendarDateAdd calendarDateAdd = new CalendarDateAdd();

			calendarDateAdd.AddWorkingWeekDays();

			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Monday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Tuesday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Wednesday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Thursday, 09, 16 ) );
			calendarDateAdd.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Friday, 09, 13 ) );

			DateTime start = new DateTime( 2011, 08, 15 );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 00, 0, 0 ) ), new DateTime( 2011, 8, 15, 09, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 07, 0, 0 ) ), new DateTime( 2011, 8, 16, 09, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 28, 0, 0 ) ), new DateTime( 2011, 8, 19, 09, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 31, 0, 0 ) ), new DateTime( 2011, 8, 19, 12, 0, 0 ) );
			Assert.AreEqual( calendarDateAdd.Add( start, new TimeSpan( 32, 0, 0 ) ), new DateTime( 2011, 8, 22, 09, 0, 0 ) );
		} // WorkingDayHoursTest

	} // class CalendarDateAddTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
