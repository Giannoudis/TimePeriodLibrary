// -- FILE ------------------------------------------------------------------
// name       : HoursTest.cs
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

    public sealed class HoursTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Hours")]
        [Fact]
        public void SingleHoursTest()
        {
            const int startYear = 2004;
            const int startMonth = 2;
            const int startDay = 22;
            const int startHour = 17;
            var hours = new Hours(startYear, startMonth, startDay, startHour, 1);

            Assert.Equal(1, hours.HourCount);
            Assert.Equal(startYear, hours.StartYear);
            Assert.Equal(startMonth, hours.StartMonth);
            Assert.Equal(startDay, hours.StartDay);
            Assert.Equal(startHour, hours.StartHour);
            Assert.Equal(2004, hours.EndYear);
            Assert.Equal(2, hours.EndMonth);
            Assert.Equal(startDay, hours.EndDay);
            Assert.Equal(startHour + 1, hours.EndHour);
            Assert.Single(hours.GetHours());
            Assert.True(hours.GetHours()[0].IsSamePeriod(new Hour(2004, 2, 22, 17)));
        } // SingleHoursTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Hours")]
        [Fact]
        public void CalendarHoursTest()
        {
            const int startYear = 2004;
            const int startMonth = 2;
            const int startDay = 11;
            const int startHour = 22;
            const int hourCount = 4;
            var hours = new Hours(startYear, startMonth, startDay, startHour, hourCount);

            Assert.Equal(hourCount, hours.HourCount);
            Assert.Equal(startYear, hours.StartYear);
            Assert.Equal(startMonth, hours.StartMonth);
            Assert.Equal(startDay, hours.StartDay);
            Assert.Equal(startHour, hours.StartHour);
            Assert.Equal(2004, hours.EndYear);
            Assert.Equal(2, hours.EndMonth);
            Assert.Equal(startDay + 1, hours.EndDay);
            Assert.Equal(2, hours.EndHour);
            Assert.Equal(hourCount, hours.GetHours().Count);
            Assert.True(hours.GetHours()[0].IsSamePeriod(new Hour(2004, 2, 11, 22)));
            Assert.True(hours.GetHours()[1].IsSamePeriod(new Hour(2004, 2, 11, 23)));
            Assert.True(hours.GetHours()[2].IsSamePeriod(new Hour(2004, 2, 12, 0)));
            Assert.True(hours.GetHours()[3].IsSamePeriod(new Hour(2004, 2, 12, 1)));
        } // CalendarHoursTest

    } // class HoursTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
