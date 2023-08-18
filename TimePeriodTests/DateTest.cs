// -- FILE ------------------------------------------------------------------
// name       : DateTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.08.24
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

    public sealed class DateTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void ConstructorTest()
        {
            var date = new Date(2009, 7, 22);

            Assert.Equal(2009, date.Year);
            Assert.Equal(7, date.Month);
            Assert.Equal(22, date.Day);
        } // ConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void ConstructorYearTest()
        {
            var date = new Date(2009);

            Assert.Equal(2009, date.Year);
            Assert.Equal(1, date.Month);
            Assert.Equal(1, date.Day);
        } // ConstructorYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void ConstructorMonthTest()
        {
            var date = new Date(2009, 7);

            Assert.Equal(2009, date.Year);
            Assert.Equal(7, date.Month);
            Assert.Equal(1, date.Day);
        } // ConstructorMonthTest


        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void DefaultConstructorTest()
        {
            const int year = 2009;
            var date = new Date(year);

            Assert.Equal(year, date.Year);
            Assert.Equal(1, date.Month);
            Assert.Equal(1, date.Day);
        } // DefaultConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void DateTimeConstructorTest()
        {
            var dateTime = new DateTime(2009, 7, 22, 18, 23, 56, 344);
            var date = new Date(dateTime);

            Assert.Equal(date.Year, dateTime.Year);
            Assert.Equal(date.Month, dateTime.Month);
            Assert.Equal(date.Day, dateTime.Day);
        } // DateTimeConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MinValueTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Date(DateTime.MinValue);
        } // MinValueTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MaxValueTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Date(DateTime.MaxValue);
        } // MinValueTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MinYearTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               new Date(DateTime.MinValue.Year - 1)));
        } // MinYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MaxYearTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               new Date(DateTime.MaxValue.Year + 1)));
        } // MaxYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MinMonthTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               new Date(DateTime.MinValue.Year, 0)));
        } // MinMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MaxMonthTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               new Date(DateTime.MaxValue.Year, TimeSpec.MonthsPerYear + 1)));
        } // MaxMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MinDayTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               new Date(DateTime.MinValue.Year, 1, 0)));
        } // MinDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void MaxDayTest()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
               new Date(DateTime.MinValue.Year, 1, TimeSpec.MaxDaysPerMonth + 1)));
        } // MaxDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void DateTimeTest1()
        {
            var dateTime1 = new DateTime(2009, 7, 22);
            var date1 = new Date(dateTime1);
            Assert.Equal(date1.DateTime, dateTime1.Date);

            var dateTime2 = new DateTime(2009, 7, 22, 18, 23, 56, 344);
            var date2 = new Date(dateTime2);
            Assert.Equal(date2.DateTime, dateTime2.Date);
        } // DateTimeTest1

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void ToDateTimeTest2()
        {
            var dateTime = new DateTime(2009, 7, 22);
            var date = new Date(dateTime);

            Assert.Equal(date.DateTime, dateTime);
            Assert.Equal(date.ToDateTime(1), new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 1, 0, 0, 0));
            Assert.Equal(date.ToDateTime(1, 1), new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 1, 1, 0, 0));
            Assert.Equal(date.ToDateTime(1, 1, 1), new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 1, 1, 1, 0));
            Assert.Equal(date.ToDateTime(1, 1, 1, 1), new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 1, 1, 1, 1));
        } // ToDateTimeTest2

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void GetDateTimeFromTimeTest()
        {
            var dateTime = new DateTime(2009, 7, 22);
            var date = new Date(dateTime);
            var timeSpan = new TimeSpan(0, 18, 23, 56, 344);
            var time = new Time(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

            Assert.Equal(date.ToDateTime(time), dateTime.Add(timeSpan));
        } // GetDateTimeFromTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void CompareToTest()
        {
            var now = ClockProxy.Clock.Now.Date;
            var date = new Date(now);
            var dateBefore = new Date(now.AddDays(-1));
            var dateAfter = new Date(now.AddDays(1));

            Assert.Equal(0, date.CompareTo(date));
            Assert.Equal(1, date.CompareTo(dateBefore));
            Assert.Equal(-1, date.CompareTo(dateAfter));
        } // CompareToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void EquatableTest()
        {
            var now = ClockProxy.Clock.Now.Date;
            var date = new Date(now);
            var dateBefore = new Date(now.AddDays(-1));
            var dateAfter = new Date(now.AddDays(1));

            Assert.True(date.Equals(date));
            Assert.False(date.Equals(dateBefore));
            Assert.False(date.Equals(dateAfter));
        } // EquatableTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Date")]
        [Fact]
        public void OperatorTest()
        {
            var now = ClockProxy.Clock.Now.Date;
            var date = new Date(now);
            var dateBefore = new Date(now.AddDays(-1));
            var dateAfter = new Date(now.AddDays(1));

            var oneDay = new TimeSpan(1, 0, 0, 0, 0);
            var halfDay = new TimeSpan(0, 12, 30, 30, 500);

            Assert.Equal(dateBefore, date - oneDay);
            Assert.Equal(dateBefore, date - halfDay);
            Assert.Equal(dateBefore, date - oneDay);
            Assert.Equal(oneDay, date - dateBefore);
            // no  operator +( Date, Date )
            Assert.Equal(dateAfter, date + oneDay);
            Assert.Equal(dateAfter, date + oneDay + halfDay);

            Assert.True(dateBefore < date);
            Assert.False(dateAfter <= date);

            Assert.True(dateBefore <= date);
            Assert.False(dateAfter <= date);

            Assert.False(dateBefore == date);
            Assert.False(dateAfter == date);

            Assert.True(dateBefore != date);
            Assert.True(dateAfter != date);

            Assert.False(dateBefore >= date);
            Assert.True(dateAfter >= date);

            Assert.False(dateBefore > date);
            Assert.True(dateAfter > date);
        } // OperatorTest

    } // class DateTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
