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
            var start = new Day(new DateTime(2011, 2, 15));

            var daySeeker = new DaySeeker();
            var day1 = daySeeker.FindDay(start, 0);
            Assert.True(day1.IsSamePeriod(start));

            var day2 = daySeeker.FindDay(start, 1);
            Assert.True(day2.IsSamePeriod(start.GetNextDay()));

            var day3 = daySeeker.FindDay(start, 100);
            Assert.True(day3.IsSamePeriod(start.AddDays(100)));
        } // SimpleForwardTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
        public void SimpleBackwardTest()
        {
            var start = new Day(new DateTime(2011, 2, 15));

            var daySeeker = new DaySeeker(SeekDirection.Backward);
            var day1 = daySeeker.FindDay(start, 0);
            Assert.True(day1.IsSamePeriod(start));

            var day2 = daySeeker.FindDay(start, 1);
            Assert.True(day2.IsSamePeriod(start.GetPreviousDay()));

            var day3 = daySeeker.FindDay(start, 100);
            Assert.True(day3.IsSamePeriod(start.AddDays(-100)));
        } // SimpleBackwardTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
        public void SeekDirectionTest()
        {
            var start = new Day(new DateTime(2011, 2, 15));

            var forwardSeeker = new DaySeeker();
            var day1 = forwardSeeker.FindDay(start, 1);
            Assert.True(day1.IsSamePeriod(start.GetNextDay()));
            var day2 = forwardSeeker.FindDay(start, -1);
            Assert.True(day2.IsSamePeriod(start.GetPreviousDay()));

            var backwardSeeker = new DaySeeker(SeekDirection.Backward);
            var day3 = backwardSeeker.FindDay(start, 1);
            Assert.True(day3.IsSamePeriod(start.GetPreviousDay()));
            var day4 = backwardSeeker.FindDay(start, -1);
            Assert.True(day4.IsSamePeriod(start.GetNextDay()));
        } // SeekDirectionTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
        public void MinDateTest()
        {
            var daySeeker = new DaySeeker();
            var day = daySeeker.FindDay(new Day(DateTime.MinValue), -10);
            Assert.Null(day);
        } // MinDateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
        public void MaxDateTest()
        {
            var daySeeker = new DaySeeker();
            var day = daySeeker.FindDay(new Day(DateTime.MaxValue.AddDays(-1)), 10);
            Assert.Null(day);
        } // MaxDateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DaySeeker")]
        [Fact]
        public void SeekWeekendHolidayTest()
        {
            var start = new Day(new DateTime(2011, 2, 15));

            var filter = new CalendarVisitorFilter();
            filter.AddWorkingWeekDays();
            filter.ExcludePeriods.Add(new Days(2011, 2, 28, 14));  // 14 days -> week 9 and 10

            var daySeeker = new DaySeeker(filter);

            var day1 = daySeeker.FindDay(start, 3); // within the same working week
            Assert.True(day1.IsSamePeriod(new Day(2011, 2, 18)));

            var day2 = daySeeker.FindDay(start, 4); // saturday -> next monday
            Assert.True(day2.IsSamePeriod(new Day(2011, 2, 21)));

            var day3 = daySeeker.FindDay(start, 10); // holidays -> next monday
            Assert.True(day3.IsSamePeriod(new Day(2011, 3, 15)));
        } // SeekWeekendHolidayTest

    } // class DaySeekerTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
