// -- FILE ------------------------------------------------------------------
// name       : MinutesTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class MinutesTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Minutes")]
        [Fact]
        public void SingleMinutesTest()
        {
            const int startYear = 2004;
            const int startMonth = 2;
            const int startDay = 22;
            const int startHour = 17;
            const int startMinute = 42;
            var minutes = new Minutes(startYear, startMonth, startDay, startHour, startMinute, 1);

            Assert.Equal(1, minutes.MinuteCount);
            Assert.Equal(startYear, minutes.StartYear);
            Assert.Equal(startMonth, minutes.StartMonth);
            Assert.Equal(startDay, minutes.StartDay);
            Assert.Equal(startHour, minutes.StartHour);
            Assert.Equal(startMinute, minutes.StartMinute);
            Assert.Equal(2004, minutes.EndYear);
            Assert.Equal(2, minutes.EndMonth);
            Assert.Equal(startDay, minutes.EndDay);
            Assert.Equal(17, minutes.EndHour);
            Assert.Equal(startMinute + 1, minutes.EndMinute);
            Assert.Single(minutes.GetMinutes());
            Assert.True(minutes.GetMinutes()[0].IsSamePeriod(new Minute(2004, 2, 22, 17, 42)));
        } // SingleMinutesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Minutes")]
        [Fact]
        public void CalendarMinutesTest()
        {
            const int startYear = 2004;
            const int startMonth = 2;
            const int startDay = 22;
            const int startHour = 23;
            const int startMinute = 59;
            const int minuteCount = 4;
            var minutes = new Minutes(startYear, startMonth, startDay, startHour, startMinute, minuteCount);

            Assert.Equal(minuteCount, minutes.MinuteCount);
            Assert.Equal(startYear, minutes.StartYear);
            Assert.Equal(startMonth, minutes.StartMonth);
            Assert.Equal(startDay, minutes.StartDay);
            Assert.Equal(startHour, minutes.StartHour);
            Assert.Equal(startMinute, minutes.StartMinute);
            Assert.Equal(2004, minutes.EndYear);
            Assert.Equal(2, minutes.EndMonth);
            Assert.Equal(23, minutes.EndDay);
            Assert.Equal(0, minutes.EndHour);
            Assert.Equal(3, minutes.EndMinute);
            Assert.Equal(minuteCount, minutes.GetMinutes().Count);
            Assert.True(minutes.GetMinutes()[0].IsSamePeriod(new Minute(2004, 2, 22, 23, 59)));
            Assert.True(minutes.GetMinutes()[1].IsSamePeriod(new Minute(2004, 2, 23, 0, 0)));
            Assert.True(minutes.GetMinutes()[2].IsSamePeriod(new Minute(2004, 2, 23, 0, 1)));
            Assert.True(minutes.GetMinutes()[3].IsSamePeriod(new Minute(2004, 2, 23, 0, 2)));
        } // CalendarMinutesTest

    } // class MinutesTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
