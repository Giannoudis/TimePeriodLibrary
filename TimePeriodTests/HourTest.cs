// -- FILE ------------------------------------------------------------------
// name       : HourTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
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

    public sealed class HourTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Hour")]
        [Fact]
        public void InitValuesTest()
        {
            var now = ClockProxy.Clock.Now;
            var firstHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var secondHour = firstHour.AddHours(1);
            var hour = new Hour(now, TimeCalendar.NewEmptyOffset());

            Assert.Equal(hour.Start.Year, firstHour.Year);
            Assert.Equal(hour.Start.Month, firstHour.Month);
            Assert.Equal(hour.Start.Day, firstHour.Day);
            Assert.Equal(hour.Start.Hour, firstHour.Hour);
            Assert.Equal(0, hour.Start.Minute);
            Assert.Equal(0, hour.Start.Second);
            Assert.Equal(0, hour.Start.Millisecond);

            Assert.Equal(hour.End.Year, secondHour.Year);
            Assert.Equal(hour.End.Month, secondHour.Month);
            Assert.Equal(hour.End.Day, secondHour.Day);
            Assert.Equal(hour.End.Hour, secondHour.Hour);
            Assert.Equal(0, hour.End.Minute);
            Assert.Equal(0, hour.End.Second);
            Assert.Equal(0, hour.End.Millisecond);
        } // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Hour")]
        [Fact]
        public void DefaultCalendarTest()
        {
            var now = ClockProxy.Clock.Now;
            var todayStart = new DateTime(now.Year, now.Month, now.Day);
            for (var dayHour = 0; dayHour < TimeSpec.HoursPerDay; dayHour++)
            {
                var hour = new Hour(todayStart.AddHours(dayHour));
                Assert.Equal(hour.Year, todayStart.Year);
                Assert.Equal(hour.Month, todayStart.Month);
                Assert.Equal(hour.Month, todayStart.Month);
                Assert.Equal(hour.Day, todayStart.Day);
                Assert.Equal(hour.HourValue, dayHour);
                Assert.Equal(hour.Start, todayStart.AddHours(dayHour).Add(hour.Calendar.StartOffset));
                Assert.Equal(hour.End, todayStart.AddHours(dayHour + 1).Add(hour.Calendar.EndOffset));
            }
        } // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Hour")]
        [Fact]
        public void ConstructorTest()
        {
            var now = ClockProxy.Clock.Now;

            Assert.Equal(new Hour(now).Year, now.Year);
            Assert.Equal(new Hour(now).Month, now.Month);
            Assert.Equal(new Hour(now).Day, now.Day);
            Assert.Equal(new Hour(now).HourValue, now.Hour);

            Assert.Equal(new Hour(now.Year, now.Month, now.Day, now.Hour).Year, now.Year);
            Assert.Equal(new Hour(now.Year, now.Month, now.Day, now.Hour).Month, now.Month);
            Assert.Equal(new Hour(now.Year, now.Month, now.Day, now.Hour).Day, now.Day);
            Assert.Equal(new Hour(now.Year, now.Month, now.Day, now.Hour).HourValue, now.Hour);
        } // ConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Hour")]
        [Fact]
        public void GetPreviousHourTest()
        {
            var hour = new Hour();
            Assert.Equal(hour.GetPreviousHour(), hour.AddHours(-1));
        } // GetPreviousHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Hour")]
        [Fact]
        public void GetNextHourTest()
        {
            var hour = new Hour();
            Assert.Equal(hour.GetNextHour(), hour.AddHours(1));
        } // GetNextHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Hour")]
        [Fact]
        public void AddHoursTest()
        {
            var now = ClockProxy.Clock.Now;
            var nowHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var hour = new Hour(now, TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero));

            Assert.Equal(hour.AddHours(0), hour);

            var previousHour = nowHour.AddHours(-1);
            Assert.Equal(hour.AddHours(-1).Year, previousHour.Year);
            Assert.Equal(hour.AddHours(-1).Month, previousHour.Month);
            Assert.Equal(hour.AddHours(-1).Day, previousHour.Day);
            Assert.Equal(hour.AddHours(-1).HourValue, previousHour.Hour);

            var nextHour = nowHour.AddHours(1);
            Assert.Equal(hour.AddHours(1).Year, nextHour.Year);
            Assert.Equal(hour.AddHours(1).Month, nextHour.Month);
            Assert.Equal(hour.AddHours(1).Day, nextHour.Day);
            Assert.Equal(hour.AddHours(1).HourValue, nextHour.Hour);
        } // AddHoursTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Hour")]
        [Fact]
        public void GetMinutesTest()
        {
            var hour = new Hour();

            var minutes = hour.GetMinutes();
            Assert.NotNull(minutes);

            var index = 0;
            foreach (var timePeriod in minutes)
            {
                var minute = (Minute)timePeriod;
                Assert.Equal(minute.Start, hour.Start.AddMinutes(index));
                Assert.Equal(minute.End, minute.Calendar.MapEnd(minute.Start.AddMinutes(1)));
                index++;
            }
            Assert.Equal(TimeSpec.MinutesPerHour, index);
        } // GetMinutesTest

    } // class HourTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
