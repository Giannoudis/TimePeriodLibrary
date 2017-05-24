// -- FILE ------------------------------------------------------------------
// name       : CalendarDateDiffTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.09.15
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
	
	public sealed class CalendarDateDiffTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateDiff")]
        [Fact]
		public void EmptyDiffTest()
		{
			DateTime date = new DateTime( 2011, 4, 12 );

			CalendarDateDiff calendarDateDiff = new CalendarDateDiff();
			Assert.Equal( calendarDateDiff.Difference( date, date ), TimeSpan.Zero );
		} // EmptyDiffTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateDiff")]
        [Fact]
		public void NoFilterTest()
		{
			DateTime date1 = new DateTime( 2011, 4, 12 );

			CalendarDateDiff calendarDateDiff = new CalendarDateDiff();
			Assert.Equal( calendarDateDiff.Difference( date1, date1.AddHours( 1 ) ), new TimeSpan( 1, 0, 0 ) );
			Assert.Equal( calendarDateDiff.Difference( date1, date1.AddHours( -1 ) ), new TimeSpan( -1, 0, 0 ) );
		} // NoFilterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateDiff")]
        [Fact]
		public void HourFilterTest()
		{
			DateTime date1 = new DateTime( 2011, 9, 11, 18, 0, 0 );
			DateTime date2 = new DateTime( 2011, 9, 15, 9, 0, 0 );

			CalendarDateDiff calendarDateDiff = new CalendarDateDiff();
			calendarDateDiff.WorkingHours.Add( new HourRange( 8, 12 ) );
			Assert.Equal( calendarDateDiff.Difference( date1, date2 ), new TimeSpan( 74, 0, 0 ) );
			Assert.Equal( calendarDateDiff.Difference( date2, date1 ), new TimeSpan( -74, 0, 0 ) );
		} // HourFilterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarDateDiff")]
        [Fact]
		public void DayHourFilterTest()
		{
			DateTime date1 = new DateTime( 2011, 9, 11, 18, 0, 0 );
			DateTime date2 = new DateTime( 2011, 9, 15, 9, 0, 0 );

			CalendarDateDiff calendarDateDiff = new CalendarDateDiff();
			calendarDateDiff.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Monday, 8, 12 ) );
			calendarDateDiff.WorkingDayHours.Add( new DayHourRange( DayOfWeek.Tuesday, 8, 16 ) );
			Assert.Equal( calendarDateDiff.Difference( date1, date2 ), new TimeSpan( 75, 0, 0 ) );
			Assert.Equal( calendarDateDiff.Difference( date2, date1 ), new TimeSpan( -75, 0, 0 ) );
		} // DayHourFilterTest

	} // class CalendarDateDiffTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
