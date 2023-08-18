// -- FILE ------------------------------------------------------------------
// name       : DateTimeSetTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class DateTimeSetTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void DefaultConstructorTest()
        {
            var dateTimeSet = new DateTimeSet();

            Assert.Empty(dateTimeSet);
            Assert.Null(dateTimeSet.Min);
            Assert.Null(dateTimeSet.Max);
            Assert.Null(dateTimeSet.Duration);
            Assert.True(dateTimeSet.IsEmpty);
            Assert.False(dateTimeSet.IsMoment);
            Assert.False(dateTimeSet.IsAnytime);
        } // DefaultConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void CopyConstructorTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var moments = new List<DateTime>
            {
                nextCalendarYear,
                previousCalendarYear,
                currentCalendarYear
            };

            var dateTimeSet = new DateTimeSet(moments);

            Assert.Equal(dateTimeSet.Count, moments.Count);
            Assert.Equal(dateTimeSet.Min, previousCalendarYear);
            Assert.Equal(dateTimeSet.Max, nextCalendarYear);
            Assert.Equal(dateTimeSet.Duration, nextCalendarYear - previousCalendarYear);
            Assert.False(dateTimeSet.IsEmpty);
            Assert.False(dateTimeSet.IsMoment);
            Assert.False(dateTimeSet.IsAnytime);
        } // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void ItemTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet
            {
                nextCalendarYear,
                currentCalendarYear,
                previousCalendarYear
            };

            Assert.Equal(dateTimeSet[0], previousCalendarYear);
            Assert.Equal(dateTimeSet[1], currentCalendarYear);
            Assert.Equal(dateTimeSet[2], nextCalendarYear);
        } // ItemTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void MinTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);

            var dateTimeSet = new DateTimeSet();
            Assert.Null(dateTimeSet.Min);

            dateTimeSet.Add(currentCalendarYear);
            Assert.Equal(dateTimeSet.Min, currentCalendarYear);

            dateTimeSet.Add(previousCalendarYear);
            Assert.Equal(dateTimeSet.Min, previousCalendarYear);

            dateTimeSet.Clear();
            Assert.Null(dateTimeSet.Min);
        } // MinTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void MaxTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.Null(dateTimeSet.Max);

            dateTimeSet.Add(currentCalendarYear);
            Assert.Equal(dateTimeSet.Max, currentCalendarYear);

            dateTimeSet.Add(nextCalendarYear);
            Assert.Equal(dateTimeSet.Max, nextCalendarYear);

            dateTimeSet.Clear();
            Assert.Null(dateTimeSet.Max);
        } // MaxTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void DurationTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.Null(dateTimeSet.Duration);

            dateTimeSet.Add(currentCalendarYear);
            Assert.Equal(dateTimeSet.Duration, TimeSpan.Zero);

            dateTimeSet.Add(nextCalendarYear);
            Assert.Equal(dateTimeSet.Duration, nextCalendarYear - currentCalendarYear);

            dateTimeSet.Remove(nextCalendarYear);
            Assert.Equal(dateTimeSet.Duration, TimeSpan.Zero);

            dateTimeSet.Clear();
            Assert.Null(dateTimeSet.Duration);
        } // DurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void IsEmptyTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.True(dateTimeSet.IsEmpty);

            dateTimeSet.Add(currentCalendarYear);
            Assert.False(dateTimeSet.IsEmpty);

            dateTimeSet.Add(nextCalendarYear);
            Assert.False(dateTimeSet.IsEmpty);

            dateTimeSet.Remove(nextCalendarYear);
            Assert.False(dateTimeSet.IsEmpty);

            dateTimeSet.Clear();
            Assert.True(dateTimeSet.IsEmpty);
        } // IsEmptyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void IsMomentTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.False(dateTimeSet.IsMoment);

            dateTimeSet.Add(currentCalendarYear);
            Assert.True(dateTimeSet.IsMoment);

            dateTimeSet.Add(nextCalendarYear);
            Assert.False(dateTimeSet.IsMoment);

            dateTimeSet.Remove(nextCalendarYear);
            Assert.True(dateTimeSet.IsMoment);

            dateTimeSet.Clear();
            Assert.False(dateTimeSet.IsMoment);
        } // IsMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void IsAnytimeTest()
        {
            var dateTimeSet = new DateTimeSet();
            Assert.False(dateTimeSet.IsAnytime);

            dateTimeSet.Add(DateTime.MinValue);
            Assert.False(dateTimeSet.IsAnytime);

            dateTimeSet.Add(DateTime.MaxValue);
            Assert.True(dateTimeSet.IsAnytime);

            dateTimeSet.Remove(DateTime.MinValue);
            Assert.False(dateTimeSet.IsAnytime);

            dateTimeSet.Clear();
            Assert.False(dateTimeSet.IsAnytime);
        } // IsAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void CountTest()
        {
            var dateTimeSet = new DateTimeSet();
            Assert.Empty(dateTimeSet);

            dateTimeSet.Add(DateTime.MinValue);
            Assert.Single(dateTimeSet);

            dateTimeSet.Add(DateTime.MinValue);
            Assert.Single(dateTimeSet);

            dateTimeSet.Add(DateTime.MaxValue);
            Assert.Equal(2, dateTimeSet.Count);

            dateTimeSet.Add(DateTime.MaxValue);
            Assert.Equal(2, dateTimeSet.Count);

            dateTimeSet.Remove(DateTime.MinValue);
            Assert.Single(dateTimeSet);

            dateTimeSet.Clear();
            Assert.Empty(dateTimeSet);
        } // CountTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void IndexOfTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();

            Assert.Equal(-1, dateTimeSet.IndexOf(currentCalendarYear));
            Assert.Equal(-1, dateTimeSet.IndexOf(previousCalendarYear));
            Assert.Equal(-1, dateTimeSet.IndexOf(nextCalendarYear));

            dateTimeSet.Add(previousCalendarYear);
            dateTimeSet.Add(nextCalendarYear);
            dateTimeSet.Add(currentCalendarYear);

            Assert.Equal(0, dateTimeSet.IndexOf(previousCalendarYear));
            Assert.Equal(1, dateTimeSet.IndexOf(currentCalendarYear));
            Assert.Equal(2, dateTimeSet.IndexOf(nextCalendarYear));
        } // IndexOfTest


        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void FindPreviousTest()
        {
            var now = ClockProxy.Clock.Now;
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);

            var dateTimeSet = new DateTimeSet();
            Assert.Null(dateTimeSet.FindPrevious(now));

            dateTimeSet.Add(currentCalendarYear);
            Assert.Equal(dateTimeSet.FindPrevious(now), now == currentCalendarYear ? null : currentCalendarYear);

            dateTimeSet.Add(previousCalendarYear);
            Assert.Equal(dateTimeSet.FindPrevious(now), now == currentCalendarYear ? previousCalendarYear : currentCalendarYear);
            Assert.Equal(dateTimeSet.FindPrevious(currentCalendarYear), previousCalendarYear);

            dateTimeSet.Remove(currentCalendarYear);
            Assert.Equal(dateTimeSet.FindPrevious(now), previousCalendarYear);

            dateTimeSet.Remove(previousCalendarYear);
            Assert.Null(dateTimeSet.FindPrevious(now));
        } // FindPreviousTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void FindNextTest()
        {
            var now = ClockProxy.Clock.Now;
            var currentCalendarYear = Now.CalendarYear;
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.Null(dateTimeSet.FindNext(now));

            dateTimeSet.Add(currentCalendarYear);
            Assert.Null(dateTimeSet.FindNext(now));

            dateTimeSet.Add(nextCalendarYear);
            Assert.Equal(dateTimeSet.FindNext(now), nextCalendarYear);
            Assert.Equal(dateTimeSet.FindNext(currentCalendarYear), nextCalendarYear);

            dateTimeSet.Remove(currentCalendarYear);
            Assert.Equal(dateTimeSet.FindNext(now), nextCalendarYear);

            dateTimeSet.Remove(nextCalendarYear);
            Assert.Null(dateTimeSet.FindNext(now));
        } // FindNextTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void AddTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.Empty(dateTimeSet);

            dateTimeSet.Add(previousCalendarYear);
            Assert.Single(dateTimeSet);
            Assert.Equal(dateTimeSet.Min, previousCalendarYear);
            Assert.Equal(dateTimeSet.Max, previousCalendarYear);

            dateTimeSet.Add(nextCalendarYear);
            Assert.Equal(2, dateTimeSet.Count);
            Assert.Equal(dateTimeSet.Min, previousCalendarYear);
            Assert.Equal(dateTimeSet.Max, nextCalendarYear);

            dateTimeSet.Add(currentCalendarYear);
            Assert.Equal(3, dateTimeSet.Count);
            Assert.Equal(dateTimeSet.Min, previousCalendarYear);
            Assert.Equal(dateTimeSet.Max, nextCalendarYear);

            dateTimeSet.Add(previousCalendarYear);
            Assert.Equal(3, dateTimeSet.Count);
            Assert.False(dateTimeSet.Add(previousCalendarYear));
            dateTimeSet.Add(currentCalendarYear);
            Assert.Equal(3, dateTimeSet.Count);
            Assert.False(dateTimeSet.Add(currentCalendarYear));
            dateTimeSet.Add(nextCalendarYear);
            Assert.Equal(3, dateTimeSet.Count);
            Assert.False(dateTimeSet.Add(nextCalendarYear));
        } // AddTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void AddAllTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var moments = new List<DateTime>
            {
                nextCalendarYear,
                currentCalendarYear,
                previousCalendarYear
            };

            var dateTimeSet = new DateTimeSet();

            Assert.Empty(dateTimeSet);
            Assert.Null(dateTimeSet.Min);
            Assert.Null(dateTimeSet.Max);
            Assert.Null(dateTimeSet.Duration);
            Assert.True(dateTimeSet.IsEmpty);
            Assert.False(dateTimeSet.IsMoment);
            Assert.False(dateTimeSet.IsAnytime);

            dateTimeSet.AddAll(moments);

            Assert.Equal(dateTimeSet.Count, moments.Count);
            Assert.Equal(dateTimeSet.Min, previousCalendarYear);
            Assert.Equal(dateTimeSet.Max, nextCalendarYear);
            Assert.Equal(dateTimeSet.Duration, nextCalendarYear - previousCalendarYear);
            Assert.False(dateTimeSet.IsEmpty);
            Assert.False(dateTimeSet.IsMoment);
            Assert.False(dateTimeSet.IsAnytime);
        } // AddAllTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void GetDurationsTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarMonth = currentCalendarYear.AddMonths(-1);
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarMonth = currentCalendarYear.AddMonths(1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet
            {
                currentCalendarYear,
                nextCalendarYear,
                nextCalendarMonth,
                previousCalendarYear,
                previousCalendarMonth
            };

            var durations1 = dateTimeSet.GetDurations(0, dateTimeSet.Count);
            Assert.Equal(4, durations1.Count);
            Assert.Equal(durations1[0], previousCalendarMonth - previousCalendarYear);
            Assert.Equal(durations1[1], currentCalendarYear - previousCalendarMonth);
            Assert.Equal(durations1[2], nextCalendarMonth - currentCalendarYear);
            Assert.Equal(durations1[3], nextCalendarYear - nextCalendarMonth);

            var durations2 = dateTimeSet.GetDurations(1, 2);
            Assert.Equal(2, durations2.Count);
            Assert.Equal(durations2[0], currentCalendarYear - previousCalendarMonth);
            Assert.Equal(durations2[1], nextCalendarMonth - currentCalendarYear);

            var durations3 = dateTimeSet.GetDurations(2, dateTimeSet.Count);
            Assert.Equal(2, durations3.Count);
            Assert.Equal(durations3[0], nextCalendarMonth - currentCalendarYear);
            Assert.Equal(durations3[1], nextCalendarYear - nextCalendarMonth);
        } // GetDurationsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void ClearTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.Empty(dateTimeSet);
            dateTimeSet.Add(previousCalendarYear);
            Assert.Single(dateTimeSet);
            dateTimeSet.Add(nextCalendarYear);
            Assert.Equal(2, dateTimeSet.Count);
            dateTimeSet.Add(currentCalendarYear);
            Assert.Equal(3, dateTimeSet.Count);

            dateTimeSet.Clear();
            Assert.Empty(dateTimeSet);
        } // ClearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void ContainsTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();
            Assert.DoesNotContain(currentCalendarYear, dateTimeSet);
            Assert.DoesNotContain(previousCalendarYear, dateTimeSet);
            Assert.DoesNotContain(nextCalendarYear, dateTimeSet);

            dateTimeSet.Add(previousCalendarYear);
            Assert.DoesNotContain(currentCalendarYear, dateTimeSet);
            Assert.Contains(previousCalendarYear, dateTimeSet);
            Assert.DoesNotContain(nextCalendarYear, dateTimeSet);

            dateTimeSet.Add(nextCalendarYear);
            Assert.DoesNotContain(currentCalendarYear, dateTimeSet);
            Assert.Contains(previousCalendarYear, dateTimeSet);
            Assert.Contains(nextCalendarYear, dateTimeSet);

            dateTimeSet.Add(currentCalendarYear);
            Assert.Contains(currentCalendarYear, dateTimeSet);
            Assert.Contains(previousCalendarYear, dateTimeSet);
            Assert.Contains(nextCalendarYear, dateTimeSet);

            dateTimeSet.Remove(nextCalendarYear);
            Assert.Contains(currentCalendarYear, dateTimeSet);
            Assert.Contains(previousCalendarYear, dateTimeSet);
            Assert.DoesNotContain(nextCalendarYear, dateTimeSet);

            dateTimeSet.Clear();
            Assert.DoesNotContain(currentCalendarYear, dateTimeSet);
            Assert.DoesNotContain(previousCalendarYear, dateTimeSet);
            Assert.DoesNotContain(nextCalendarYear, dateTimeSet);
        } // ContainsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void CopyToTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet
            {
                nextCalendarYear,
                currentCalendarYear,
                previousCalendarYear
            };

            var array = new DateTime[3];
            dateTimeSet.CopyTo(array, 0);
            Assert.Equal(array[0], previousCalendarYear);
            Assert.Equal(array[1], currentCalendarYear);
            Assert.Equal(array[2], nextCalendarYear);
        } // CopyToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
        public void RemoveTest()
        {
            var currentCalendarYear = Now.CalendarYear;
            var previousCalendarYear = currentCalendarYear.AddYears(-1);
            var nextCalendarYear = currentCalendarYear.AddYears(1);

            var dateTimeSet = new DateTimeSet();

            Assert.DoesNotContain(previousCalendarYear, dateTimeSet);

            dateTimeSet.Add(previousCalendarYear);
            Assert.Contains(previousCalendarYear, dateTimeSet);

            dateTimeSet.Remove(previousCalendarYear);
            Assert.DoesNotContain(previousCalendarYear, dateTimeSet);

            Assert.DoesNotContain(nextCalendarYear, dateTimeSet);
            dateTimeSet.Add(nextCalendarYear);
            Assert.Contains(nextCalendarYear, dateTimeSet);
            dateTimeSet.Remove(previousCalendarYear);
            Assert.Contains(nextCalendarYear, dateTimeSet);
        } // RemoveTest

    } // class DateTimeSetTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
