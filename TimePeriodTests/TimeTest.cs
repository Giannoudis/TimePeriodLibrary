// -- FILE ------------------------------------------------------------------
// name       : TimeTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.12
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

    public sealed class TimeTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void ConstructorTest()
        {
            var time = new Time(18, 23, 56, 344);

            Assert.Equal(18, time.Hour);
            Assert.Equal(23, time.Minute);
            Assert.Equal(56, time.Second);
            Assert.Equal(344, time.Millisecond);
        } // ConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void ConstructorHourTest()
        {
            var time = new Time(18);

            Assert.Equal(18, time.Hour);
            Assert.Equal(0, time.Minute);
            Assert.Equal(0, time.Second);
            Assert.Equal(0, time.Millisecond);
        } // ConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void ConstructorMinuteTest()
        {
            var time = new Time(18, 23);

            Assert.Equal(18, time.Hour);
            Assert.Equal(23, time.Minute);
            Assert.Equal(0, time.Second);
            Assert.Equal(0, time.Millisecond);
        } // ConstructorMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void ConstructorSecondTest()
        {
            var time = new Time(18, 23, 56);

            Assert.Equal(18, time.Hour);
            Assert.Equal(23, time.Minute);
            Assert.Equal(56, time.Second);
            Assert.Equal(0, time.Millisecond);
        } // ConstructorSecondTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void EmptyConstructorTest()
        {
            var time = new Time();

            Assert.Equal(0, time.Hour);
            Assert.Equal(0, time.Minute);
            Assert.Equal(0, time.Second);
            Assert.Equal(0, time.Millisecond);
            Assert.Equal(0, time.Ticks);
            Assert.Equal(time.Duration, TimeSpan.Zero);
            Assert.Equal(0, time.TotalHours);
            Assert.Equal(0, time.TotalMinutes);
            Assert.Equal(0, time.TotalSeconds);
            Assert.Equal(0, time.TotalMilliseconds);
        } // EmptyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void DateTimeConstructorTest()
        {
            var test = new DateTime(2009, 7, 22, 18, 23, 56, 344);
            var time = new Time(test);

            Assert.Equal(time.Hour, test.Hour);
            Assert.Equal(time.Minute, test.Minute);
            Assert.Equal(time.Second, test.Second);
            Assert.Equal(time.Millisecond, test.Millisecond);
        } // DateTimeConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxHourMinuteConstructorTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(24, 1)));
        } // MaxHourMinuteConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxHourSecondConstructorTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(24, 0, 1)));
        } // MaxHourSecondConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxHourMillisecondConstructorTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(24, 0, 0, 1)));
        } // MaxHourMillisecondConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void EmptyDateTimeConstructorTest()
        {
            var test = new DateTime(2009, 7, 22);
            var time = new Time(test);

            Assert.Equal(0, time.Hour);
            Assert.Equal(0, time.Minute);
            Assert.Equal(0, time.Second);
            Assert.Equal(0, time.Millisecond);
            Assert.Equal(0, time.Ticks);
            Assert.Equal(time.Duration, TimeSpan.Zero);
            Assert.Equal(0, time.TotalHours);
            Assert.Equal(0, time.TotalMinutes);
            Assert.Equal(0, time.TotalSeconds);
            Assert.Equal(0, time.TotalMilliseconds);
        } // EmptyDateTimeConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MinValueTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Time(0);
        } // MinValueTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxValueTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Time(TimeSpec.HoursPerDay - 1, TimeSpec.MinutesPerHour - 1, TimeSpec.SecondsPerMinute - 1, TimeSpec.MillisecondsPerSecond - 1);
        } // MinValueTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MinHourTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(-1)));
        } // MinHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxHourTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(TimeSpec.HoursPerDay + 1)));
        } // MaxHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MinMinuteTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(0, -1)));
        } // MinMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxMinuteTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(0, TimeSpec.MinutesPerHour)));
        } // MaxMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MinSecondTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(0, 0, -1)));
        } // MinSecondTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxSecondTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(0, 0, TimeSpec.SecondsPerMinute)));
        } // MaxSecondTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MinMillisecondTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(0, 0, 0, -1)));
        } // MinMillisecondTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MaxMillisecondTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Time(0, 0, 0, TimeSpec.MillisecondsPerSecond)));
        } // MaxMillisecondTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void DurationTest()
        {

            var test = new TimeSpan(0, 18, 23, 56, 344);
            var time = new Time(test.Hours, test.Minutes, test.Seconds, test.Milliseconds);

            Assert.Equal(time.Hour, test.Hours);
            Assert.Equal(time.Minute, test.Minutes);
            Assert.Equal(time.Second, test.Seconds);
            Assert.Equal(time.Millisecond, test.Milliseconds);

            Assert.Equal(time.Duration.Ticks, test.Ticks);

            Assert.Equal(time.TotalHours, test.TotalHours);
            Assert.Equal(time.TotalMinutes, test.TotalMinutes);
            Assert.Equal(time.TotalSeconds, test.TotalSeconds);
            Assert.Equal(time.TotalMilliseconds, test.TotalMilliseconds);
        } // DurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void ToDateTimeTest()
        {
            var dateTime = new DateTime(2009, 7, 22);
            var timeSpan = new TimeSpan(0, 18, 23, 56, 344);
            var time = new Time(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

            Assert.Equal(time.ToDateTime(dateTime), dateTime.Add(timeSpan));
        } // ToDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void GetDateTimeFromDateTest()
        {
            var date = new Date(2009, 7, 22);
            var timeSpan = new TimeSpan(0, 18, 23, 56, 344);
            var time = new Time(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

            Assert.Equal(time.ToDateTime(date), date.DateTime.Add(timeSpan));
        } // GetDateTimeFromDateTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void GetEmptyDateTimeTest()
        {
            var dateTime = new DateTime(2009, 7, 22);
            var time = new Time();

            Assert.Equal(time.ToDateTime(dateTime), dateTime);
        } // GetEmptyDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void IsZeroTest()
        {
            var midnight = ClockProxy.Clock.Now.Date;
            var time0 = new Time(0);
            var timeBefore = new Time(midnight.AddMilliseconds(-1));
            var timeAfter = new Time(midnight.AddMilliseconds(1));
            var time24 = new Time(24);

            Assert.True(time0.IsZero);
            Assert.False(timeBefore.IsZero);
            Assert.False(timeAfter.IsZero);
            Assert.False(time24.IsZero);
        } // IsZeroTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void IsFullDayTest()
        {
            var midnight = ClockProxy.Clock.Now.Date;
            var time0 = new Time(0);
            var time24 = new Time(24);
            var timeBefore = new Time(midnight.AddMilliseconds(-1));
            var timeAfter = new Time(midnight.AddMilliseconds(1));

            Assert.False(time0.IsFullDay);
            Assert.True(time24.IsFullDay);
            Assert.False(timeBefore.IsFullDay);
            Assert.False(timeAfter.IsFullDay);
        } // IsFullDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void IsFullDayOrZeroTest()
        {
            var midnight = ClockProxy.Clock.Now.Date;
            var time0 = new Time(0);
            var timeBefore0 = new Time(midnight.AddMilliseconds(-1));
            var timeAfter0 = new Time(midnight.AddMilliseconds(1));

            Assert.True(time0.IsFullDayOrZero);
            Assert.False(time0.Equals(timeBefore0));
            Assert.False(time0.Equals(timeAfter0));

            var time24 = new Time(24);
            var timeBefore24 = new Time(midnight.AddMilliseconds(-1));
            var timeAfter24 = new Time(midnight.AddMilliseconds(1));

            Assert.NotEqual(time0, time24);
            Assert.True(time24.IsFullDayOrZero);
            Assert.False(time24.Equals(timeBefore24));
            Assert.False(time24.Equals(timeAfter24));
        } // IsFullDayOrZeroTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void CompareToTest()
        {
            var today = ClockProxy.Clock.Now.Date;

            var empty = new Time();
            Assert.Equal(0, empty.CompareTo(empty));
            Assert.Equal(-1, empty.CompareTo(new Time(today.AddMilliseconds(-1))));
            Assert.Equal(-1, empty.CompareTo(new Time(today.AddMilliseconds(1))));

            var midnight = new Time(today);
            Assert.Equal(0, midnight.CompareTo(midnight));
            Assert.Equal(-1, midnight.CompareTo(new Time(today.AddMilliseconds(-1))));
            Assert.Equal(-1, midnight.CompareTo(new Time(today.AddMilliseconds(1))));

            var midday = new Time(today.AddHours(12));
            Assert.Equal(0, midday.CompareTo(midday));
            Assert.Equal(1, midday.CompareTo(new Time(today.AddHours(12).AddMilliseconds(-1))));
            Assert.Equal(-1, midday.CompareTo(new Time(today.AddHours(12).AddMilliseconds(1))));

            var full = new Time(24);
            Assert.Equal(0, full.CompareTo(full));
            Assert.Equal(1, full.CompareTo(new Time(today.AddMilliseconds(-1))));
            Assert.Equal(1, full.CompareTo(new Time(today.AddMilliseconds(1))));
        } // CompareToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void EquatableTest()
        {
            var now = ClockProxy.Clock.Now.Date;
            var time = new Time(now);
            var timeBefore = new Time(now.AddMilliseconds(-1));
            var timeAfter = new Time(now.AddMilliseconds(1));

            Assert.True(time.Equals(time));
            Assert.False(time.Equals(timeBefore));
            Assert.False(time.Equals(timeAfter));
        } // EquatableTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MidnightOperatorTest()
        {
            var midnight = ClockProxy.Clock.Now.Date;
            var midday = midnight.AddHours(12);
            var time = new Time(midnight);
            var timeBefore = new Time(midnight.AddMilliseconds(-1));
            var timeAfter = new Time(midnight.AddMilliseconds(1));

            var oneDay = new TimeSpan(1, 0, 0, 0, 0);
            var hours18 = new TimeSpan(0, 18, 0, 0, 0);
            var hours12 = new TimeSpan(0, 12, 0, 0, 0);
            var hours6 = new TimeSpan(0, 6, 0, 0, 0);
            var oneMillisecond = new TimeSpan(0, 0, 0, 0, 1);

            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), time - timeBefore);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), timeAfter - time);
            Assert.Equal(time, time - oneDay);
            Assert.Equal(time, time - oneDay.Negate());
            Assert.Equal(new Time(midday + hours6), time - hours6);
            Assert.Equal(new Time(midday), time - hours12);
            Assert.Equal(new Time(midnight - hours18), time - hours18);
            Assert.Equal(timeBefore, time - oneMillisecond);
            Assert.Equal(oneMillisecond, time - timeBefore);

            Assert.Equal(new TimeSpan(0, 23, 59, 59, 999), time + timeBefore);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), time + timeAfter);
            Assert.Equal(timeAfter, time + oneMillisecond);
            Assert.Equal(time, time + oneDay);
            Assert.Equal(time, time + oneDay.Negate());
            Assert.Equal(new Time(midnight + hours18), time + hours18);
            Assert.Equal(new Time(midday), time + hours12);
            Assert.Equal(new Time(hours6), time + hours6);

            Assert.False(timeBefore < time);
            Assert.False(timeAfter < time);

            Assert.False(timeBefore <= time);
            Assert.False(timeAfter <= time);

            Assert.False(timeBefore == time);
            Assert.False(timeAfter == time);

            Assert.True(timeBefore != time);
            Assert.True(timeAfter != time);

            Assert.True(timeBefore >= time);
            Assert.True(timeAfter >= time);

            Assert.True(timeBefore > time);
            Assert.True(timeAfter > time);
        } // MidnightOperatorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Time")]
        [Fact]
        public void MiddayOperatorTest()
        {
            var midnight = ClockProxy.Clock.Now.Date;
            var midday = midnight.AddHours(12);
            var time = new Time(midday);
            var timeBefore = new Time(midday.AddMilliseconds(-1));
            var timeAfter = new Time(midday.AddMilliseconds(1));

            var oneDay = new TimeSpan(1, 0, 0, 0, 0);
            var hours18 = new TimeSpan(0, 18, 0, 0, 0);
            var hours12 = new TimeSpan(0, 12, 0, 0, 0);
            var hours6 = new TimeSpan(0, 6, 0, 0, 0);
            var oneMillisecond = new TimeSpan(0, 0, 0, 0, 1);

            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), time - timeBefore);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), timeAfter - time);
            Assert.Equal(time, time - oneDay);
            Assert.Equal(time, time - oneDay.Negate());
            Assert.Equal(new Time(midday - hours6), time - hours6);
            Assert.Equal(new Time(midnight), time - hours12);
            Assert.Equal(new Time(midday - hours18), time - hours18);
            Assert.Equal(timeBefore, time - oneMillisecond);
            Assert.Equal(oneMillisecond, time - timeBefore);

            Assert.Equal(new TimeSpan(0, 23, 59, 59, 999), time + timeBefore);
            Assert.Equal(new TimeSpan(0, 0, 0, 0, 1), time + timeAfter);
            Assert.Equal(timeAfter, time + oneMillisecond);
            Assert.Equal(time, time + oneDay);
            Assert.Equal(time, time + oneDay.Negate());
            Assert.Equal(new Time(midnight + hours6), time + hours18);
            Assert.Equal(new Time(midnight), time + hours12);
            Assert.Equal(new Time(midnight + hours18), time + hours6);

            Assert.True(timeBefore < time);
            Assert.False(timeAfter < time);

            Assert.True(timeBefore <= time);
            Assert.False(timeAfter <= time);

            Assert.False(timeBefore == time);
            Assert.False(timeAfter == time);

            Assert.True(timeBefore != time);
            Assert.True(timeAfter != time);

            Assert.False(timeBefore >= time);
            Assert.True(timeAfter >= time);

            Assert.False(timeBefore > time);
            Assert.True(timeAfter > time);
        } // MiddayOperatorTest

    } // class TimeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
