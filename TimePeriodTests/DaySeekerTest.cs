// -- FILE ------------------------------------------------------------------
// name       : DaySeekerTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.22
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
	
	public sealed class DaySeekerTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
		public void SimpleForwardTest()
		{
			Day start = new Day( new DateTime( 2011, 2, 15 ) );

			DaySeeker daySeeker = new DaySeeker();
			Day day1 = daySeeker.FindDay( start, 0 );
			Assert.True( day1.IsSamePeriod( start ) );

			Day day2 = daySeeker.FindDay( start, 1 );
			Assert.True( day2.IsSamePeriod( start.GetNextDay() ) );

			Day day3 = daySeeker.FindDay( start, 100 );
			Assert.True( day3.IsSamePeriod( start.AddDays( 100 ) ) );
		} // SimpleForwardTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
		public void SimpleBackwardTest()
		{
			Day start = new Day( new DateTime( 2011, 2, 15 ) );

			DaySeeker daySeeker = new DaySeeker( SeekDirection.Backward );
			Day day1 = daySeeker.FindDay( start, 0 );
			Assert.True( day1.IsSamePeriod( start ) );

			Day day2 = daySeeker.FindDay( start, 1 );
			Assert.True( day2.IsSamePeriod( start.GetPreviousDay() ) );

			Day day3 = daySeeker.FindDay( start, 100 );
			Assert.True( day3.IsSamePeriod( start.AddDays( -100 ) ) );
		} // SimpleBackwardTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
		public void SeekDirectionTest()
		{
			Day start = new Day( new DateTime( 2011, 2, 15 ) );

			DaySeeker forwardSeeker = new DaySeeker();
			Day day1 = forwardSeeker.FindDay( start, 1 );
			Assert.True( day1.IsSamePeriod( start.GetNextDay() ) );
			Day day2 = forwardSeeker.FindDay( start, -1 );
			Assert.True( day2.IsSamePeriod( start.GetPreviousDay() ) );

			DaySeeker backwardSeeker = new DaySeeker( SeekDirection.Backward );
			Day day3 = backwardSeeker.FindDay( start, 1 );
			Assert.True( day3.IsSamePeriod( start.GetPreviousDay() ) );
			Day day4 = backwardSeeker.FindDay( start, -1 );
			Assert.True( day4.IsSamePeriod( start.GetNextDay() ) );
		} // SeekDirectionTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
		public void MinDateTest()
		{
			DaySeeker daySeeker = new DaySeeker();
			Day day = daySeeker.FindDay( new Day( DateTime.MinValue ), -10 );
			Assert.Null( day );
		} // MinDateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
		public void MaxDateTest()
		{
			DaySeeker daySeeker = new DaySeeker();
			Day day = daySeeker.FindDay( new Day( DateTime.MaxValue.AddDays( -1 ) ), 10 );
			Assert.Null( day );
		} // MaxDateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
		public void SeekWeekendHolidayTest()
		{
		  Day start = new Day( new DateTime( 2011, 2, 15 ) );

			CalendarVisitorFilter filter = new CalendarVisitorFilter();
			filter.AddWorkingWeekDays();
			filter.ExcludePeriods.Add( new Days( 2011, 2, 28, 14 ) );  // 14 days -> week 9 and 10

			DaySeeker daySeeker = new DaySeeker( filter );

			Day day1 = daySeeker.FindDay( start, 3 ); // wtihtin the same working week
			Assert.True( day1.IsSamePeriod( new Day( 2011, 2, 18 ) ) );

			Day day2 = daySeeker.FindDay( start, 4 ); // saturday -> next monday
			Assert.True( day2.IsSamePeriod( new Day( 2011, 2, 21 ) ) );

			Day day3 = daySeeker.FindDay( start, 10 ); // holidays -> next monday
			Assert.True( day3.IsSamePeriod( new Day( 2011, 3, 15 ) ) );
		} // SeekWeekendHolidayTest

	} // class DaySeekerTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
