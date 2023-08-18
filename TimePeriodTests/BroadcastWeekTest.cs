// -- FILE ------------------------------------------------------------------
// name       : BroadcastWeekTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.09.30
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class BroadcastWeekTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "BroadcastWeek")]
        [Fact]
        public void SpecificMomentsTest()
        {
            Assert.Equal(2013, new BroadcastWeek(new DateTime(2013, 12, 29)).Year);
            Assert.Equal(52, new BroadcastWeek(new DateTime(2013, 12, 29)).Week);

            Assert.Equal(2014, new BroadcastWeek(new DateTime(2013, 12, 30)).Year);
            Assert.Equal(1, new BroadcastWeek(new DateTime(2013, 12, 30)).Week);

            Assert.Equal(2014, new BroadcastWeek(new DateTime(2014, 01, 05)).Year);
            Assert.Equal(1, new BroadcastWeek(new DateTime(2014, 01, 05)).Week);

            Assert.Equal(2014, new BroadcastWeek(new DateTime(2014, 01, 06)).Year);
            Assert.Equal(2, new BroadcastWeek(new DateTime(2014, 01, 06)).Week);
        } // SpecificMomentsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "BroadcastWeek")]
        [Fact]
        public void WeekDaysTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var weekCount = BroadcastCalendarTool.GetWeeksOfYear(currentYear);

            for (var week = 1; week <= weekCount; week++)
            {
                Assert.Equal(7, new BroadcastWeek().GetDays().Count);
                Assert.Equal(DayOfWeek.Monday, new BroadcastWeek().GetDays()[0].Start.DayOfWeek);
            }
        } // WeekDaysTest

    } // class BroadcastWeekTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
