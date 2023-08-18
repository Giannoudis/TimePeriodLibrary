// -- FILE ------------------------------------------------------------------
// name       : DateAddTest.cs
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

    public sealed class DateAddTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void NoPeriodsTest()
        {
            var test = new DateTime(2011, 4, 12);

            var dateAdd = new DateAdd();
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(1, 0, 0, 0)), test.Add(new TimeSpan(1, 0, 0, 0)));
            Assert.Equal(dateAdd.Add(test, new TimeSpan(-1, 0, 0, 0)), test.Add(new TimeSpan(-1, 0, 0, 0)));
            Assert.Equal(dateAdd.Subtract(test, new TimeSpan(1, 0, 0, 0)), test.Subtract(new TimeSpan(1, 0, 0, 0)));
            Assert.Equal(dateAdd.Subtract(test, new TimeSpan(-1, 0, 0, 0)), test.Subtract(new TimeSpan(-1, 0, 0, 0)));
        } // NoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void PeriodLimitsAddTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 20), new DateTime(2011, 4, 25));
            var timeRange2 = new TimeRange(new DateTime(2011, 4, 30), DateTime.MaxValue);
            var dateAdd = new DateAdd();
            dateAdd.ExcludePeriods.Add(timeRange1);
            dateAdd.ExcludePeriods.Add(timeRange2);

            Assert.Equal(dateAdd.Add(test, new TimeSpan(8, 0, 0, 0)), timeRange1.End);
            Assert.Null(dateAdd.Add(test, new TimeSpan(20, 0, 0, 0)));
        } // PeriodLimitsAddTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void PeriodLimitsSubtractTest()
        {
            var test = new DateTime(2011, 4, 30);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 20), new DateTime(2011, 4, 25));
            var timeRange2 = new TimeRange(DateTime.MinValue, new DateTime(2011, 4, 10));
            var dateAdd = new DateAdd();
            dateAdd.ExcludePeriods.Add(timeRange1);
            dateAdd.ExcludePeriods.Add(timeRange2);

            Assert.Equal(dateAdd.Subtract(test, new TimeSpan(5, 0, 0, 0)), timeRange1.Start);
            Assert.Null(dateAdd.Subtract(test, new TimeSpan(20, 0, 0, 0)));
        } // PeriodLimitsSubtractTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeOutsideMaxTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange = new TimeRange(new DateTime(2011, 4, 20), DateTime.MaxValue);
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(timeRange);

            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), new DateTime(2011, 4, 20));
            Assert.Equal(dateAdd.Add(test, new TimeSpan(1, 0, 0, 0)), new DateTime(2011, 4, 21));

            Assert.Null(dateAdd.Subtract(test, TimeSpan.Zero));
            Assert.Null(dateAdd.Subtract(test, new TimeSpan(1, 0, 0, 0)));
        } // IncludeOutsideMaxTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeOutsideMinTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange = new TimeRange(DateTime.MinValue, new DateTime(2011, 4, 10));
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(timeRange);

            Assert.Null(dateAdd.Add(test, TimeSpan.Zero));
            Assert.Null(dateAdd.Add(test, new TimeSpan(1, 0, 0, 0)));

            Assert.Equal(dateAdd.Subtract(test, TimeSpan.Zero), new DateTime(2011, 4, 10));
            Assert.Equal(dateAdd.Subtract(test, new TimeSpan(1, 0, 0, 0)), new DateTime(2011, 4, 9));
        } // IncludeOutsideMinTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void AllExcludedTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange = new TimeRange(new DateTime(2011, 4, 10), new DateTime(2011, 4, 20));
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(timeRange);
            dateAdd.ExcludePeriods.Add(timeRange);

            Assert.Null(dateAdd.Add(test, TimeSpan.Zero));
        } // AllExcludedTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void AllExcluded2Test()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 10), new DateTime(2011, 4, 20));
            var timeRange2 = new TimeRange(new DateTime(2011, 4, 10), new DateTime(2011, 4, 15));
            var timeRange3 = new TimeRange(new DateTime(2011, 4, 15), new DateTime(2011, 4, 20));
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(timeRange1);
            dateAdd.ExcludePeriods.Add(timeRange2);
            dateAdd.ExcludePeriods.Add(timeRange3);

            Assert.Null(dateAdd.Add(test, TimeSpan.Zero));
        } // AllExcluded2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void AllExcluded3Test()
        {
            var test = new DateTime(2011, 4, 12);

            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 4, 10), new DateTime(2011, 4, 20)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 4, 15), new DateTime(2011, 4, 20)));

            Assert.Null(dateAdd.Add(test, new TimeSpan(3, 0, 0, 0)));
        } // AllExcluded3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void PeriodMomentTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange = new TimeRange(test, test);
            var dateAdd = new DateAdd();
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            dateAdd.IncludePeriods.Add(timeRange);
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            dateAdd.ExcludePeriods.Add(timeRange);
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            dateAdd.IncludePeriods.Clear();
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            dateAdd.ExcludePeriods.Clear();
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
        } // PeriodMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange = new TimeRange(new DateTime(2011, 4, 1), DateTime.MaxValue);
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(timeRange);

            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(1, 0, 0, 0)), test.Add(new TimeSpan(1, 0, 0, 0)));
        } // IncludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeSplitTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 1), new DateTime(2011, 4, 15));
            var timeRange2 = new TimeRange(new DateTime(2011, 4, 20), new DateTime(2011, 4, 24));
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(timeRange1);
            dateAdd.IncludePeriods.Add(timeRange2);

            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(1, 0, 0, 0)), test.Add(new TimeSpan(1, 0, 0, 0)));
            Assert.Equal(dateAdd.Add(test, new TimeSpan(5, 0, 0, 0)), timeRange2.Start.Add(new TimeSpan(2, 0, 0, 0)));
            Assert.Null(dateAdd.Add(test, new TimeSpan(15, 0, 0, 0)));
        } // IncludeSplitTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void ExcludeTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange = new TimeRange(new DateTime(2011, 4, 15), new DateTime(2011, 4, 20));
            var dateAdd = new DateAdd();
            dateAdd.ExcludePeriods.Add(timeRange);

            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(2, 0, 0, 0)), test.Add(new TimeSpan(2, 0, 0, 0)));
            Assert.Equal(dateAdd.Add(test, new TimeSpan(3, 0, 0, 0)), timeRange.End);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(3, 0, 0, 0, 1)), timeRange.End.Add(new TimeSpan(0, 0, 0, 0, 1)));
            Assert.Equal(dateAdd.Add(test, new TimeSpan(5, 0, 0, 0)), timeRange.End.Add(new TimeSpan(2, 0, 0, 0)));
        } // ExcludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void ExcludeSplitTest()
        {
            var test = new DateTime(2011, 4, 12);

            var timeRange1 = new TimeRange(new DateTime(2011, 4, 15), new DateTime(2011, 4, 20));
            var timeRange2 = new TimeRange(new DateTime(2011, 4, 22), new DateTime(2011, 4, 25));
            var dateAdd = new DateAdd();
            dateAdd.ExcludePeriods.Add(timeRange1);
            dateAdd.ExcludePeriods.Add(timeRange2);

            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(2, 0, 0, 0)), test.Add(new TimeSpan(2, 0, 0, 0)));
            Assert.Equal(dateAdd.Add(test, new TimeSpan(3, 0, 0, 0)), timeRange1.End);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(4, 0, 0, 0)), timeRange1.End.Add(new TimeSpan(1, 0, 0, 0)));
            Assert.Equal(dateAdd.Add(test, new TimeSpan(5, 0, 0, 0)), timeRange2.End);
            Assert.Equal(dateAdd.Add(test, new TimeSpan(7, 0, 0, 0)), timeRange2.End.Add(new TimeSpan(2, 0, 0, 0)));
        } // ExcludeSplitTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeEqualsExcludeTest()
        {
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 10)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 10)));

            var test = new DateTime(2011, 3, 5);
            Assert.Null(dateAdd.Add(test, TimeSpan.Zero));
            Assert.Null(dateAdd.Add(test, new TimeSpan(1)));
            Assert.Null(dateAdd.Add(test, new TimeSpan(-1)));
            Assert.Null(dateAdd.Subtract(test, TimeSpan.Zero));
            Assert.Null(dateAdd.Subtract(test, new TimeSpan(1)));
            Assert.Null(dateAdd.Subtract(test, new TimeSpan(-1)));
        } // IncludeEqualsExcludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeExcludeTest()
        {
            var dateAdd = new DateAdd();

            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 17), new DateTime(2011, 4, 20)));

            // setup some periods to exclude
            dateAdd.ExcludePeriods.Add(new TimeRange(
                new DateTime(2011, 3, 22), new DateTime(2011, 3, 25)));
            dateAdd.ExcludePeriods.Add(new TimeRange(
                new DateTime(2011, 4, 1), new DateTime(2011, 4, 7)));
            dateAdd.ExcludePeriods.Add(new TimeRange(
                new DateTime(2011, 4, 15), new DateTime(2011, 4, 16)));

            // positive
            var periodStart = new DateTime(2011, 3, 19);
            Assert.Equal(dateAdd.Add(periodStart, Duration.Hours(1)), new DateTime(2011, 3, 19, 1, 0, 0));
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(4)), new DateTime(2011, 3, 26, 0, 0, 0));
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(17)), new DateTime(2011, 4, 14));
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(20)), new DateTime(2011, 4, 18));
            Assert.Null(dateAdd.Add(periodStart, Duration.Days(22)));

            // negative
            var periodEnd = new DateTime(2011, 4, 18);
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Hours(-1)), new DateTime(2011, 4, 17, 23, 0, 0));
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-4)), new DateTime(2011, 4, 13));
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-17)), new DateTime(2011, 3, 22));
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-20)), new DateTime(2011, 3, 19));
            Assert.Null(dateAdd.Add(periodEnd, Duration.Days(-22)));
        } // IncludeExcludeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeExclude2Test()
        {
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 1), new DateTime(2011, 3, 5)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 10)));
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20)));
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 20), new DateTime(2011, 3, 25)));

            var periodStart = new DateTime(2011, 3, 1);
            var periodEnd = new DateTime(2011, 3, 25);

            // add from start
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(1)), new DateTime(2011, 3, 2));
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(4)), new DateTime(2011, 3, 10));
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(5)), new DateTime(2011, 3, 11));
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(9)), new DateTime(2011, 3, 20));
            Assert.Equal(dateAdd.Add(periodStart, Duration.Days(10)), new DateTime(2011, 3, 21));
            Assert.Null(dateAdd.Add(periodStart, Duration.Days(15)));
            // add from end
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-1)), new DateTime(2011, 3, 24));
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-5)), new DateTime(2011, 3, 15));
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-6)), new DateTime(2011, 3, 14));
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-10)), new DateTime(2011, 3, 5));
            Assert.Equal(dateAdd.Add(periodEnd, Duration.Days(-11)), new DateTime(2011, 3, 4));
            Assert.Null(dateAdd.Add(periodEnd, Duration.Days(-15)));

            // subtract form end
            Assert.Equal(dateAdd.Subtract(periodEnd, Duration.Days(1)), new DateTime(2011, 3, 24));
            Assert.Equal(dateAdd.Subtract(periodEnd, Duration.Days(5)), new DateTime(2011, 3, 15));
            Assert.Equal(dateAdd.Subtract(periodEnd, Duration.Days(6)), new DateTime(2011, 3, 14));
            Assert.Equal(dateAdd.Subtract(periodEnd, Duration.Days(10)), new DateTime(2011, 3, 5));
            Assert.Equal(dateAdd.Subtract(periodEnd, Duration.Days(11)), new DateTime(2011, 3, 4));
            Assert.Null(dateAdd.Subtract(periodStart, Duration.Days(15)));
            // subtract form start
            Assert.Equal(dateAdd.Subtract(periodStart, Duration.Days(-1)), new DateTime(2011, 3, 2));
            Assert.Equal(dateAdd.Subtract(periodStart, Duration.Days(-4)), new DateTime(2011, 3, 10));
            Assert.Equal(dateAdd.Subtract(periodStart, Duration.Days(-5)), new DateTime(2011, 3, 11));
            Assert.Equal(dateAdd.Subtract(periodStart, Duration.Days(-9)), new DateTime(2011, 3, 20));
            Assert.Equal(dateAdd.Subtract(periodStart, Duration.Days(-10)), new DateTime(2011, 3, 21));
            Assert.Null(dateAdd.Subtract(periodStart, Duration.Days(-15)));
        } // IncludeExclude2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeExclude3Test()
        {
            var dateAdd = new DateAdd();
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 10)));
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20)));

            var test = new DateTime(2011, 3, 10);
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), test);
            Assert.Equal(dateAdd.Add(test, Duration.Days(1)), new DateTime(2011, 3, 11));
            Assert.Null(dateAdd.Add(test, Duration.Days(5)));
        } // IncludeExclude3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeExclude4Test()
        {
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 15)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 20)));

            var test = new DateTime(2011, 3, 10);
            Assert.Null(dateAdd.Add(test, TimeSpan.Zero));
            Assert.Null(dateAdd.Add(test, Duration.Days(1)));
            Assert.Null(dateAdd.Add(test, Duration.Days(5)));
        } // IncludeExclude4Test

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeExclude5Test()
        {
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 15)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 15), new DateTime(2011, 3, 30)));

            var test = new DateTime(2011, 3, 10);
            Assert.Null(dateAdd.Add(test, TimeSpan.Zero));
            Assert.Null(dateAdd.Add(test, new TimeSpan(1)));
            Assert.Null(dateAdd.Add(test, new TimeSpan(-1)));
            Assert.Null(dateAdd.Subtract(test, TimeSpan.Zero));
            Assert.Null(dateAdd.Subtract(test, new TimeSpan(1)));
            Assert.Null(dateAdd.Subtract(test, new TimeSpan(-1)));
        } // IncludeExclude5Test

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void IncludeExclude6Test()
        {
            var dateAdd = new DateAdd();
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2011, 3, 10), new DateTime(2011, 3, 20)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 5), new DateTime(2011, 3, 12)));
            dateAdd.ExcludePeriods.Add(new TimeRange(new DateTime(2011, 3, 18), new DateTime(2011, 3, 30)));

            var test = new DateTime(2011, 3, 10);
            Assert.Equal(dateAdd.Add(test, TimeSpan.Zero), new DateTime(2011, 3, 12));
            Assert.Equal(dateAdd.Add(test, Duration.Days(1)), new DateTime(2011, 3, 13));
        } // IncludeExclude6Test

        // ----------------------------------------------------------------------
        [Trait("Category", "DateAdd")]
        [Fact]
        public void ExcludePeriodTest()
        {
            var dateAdd = new DateAdd();

            // include periods
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2013, 1, 1, 8, 0, 0), new DateTime(2013, 1, 1, 12, 0, 0)));
            dateAdd.IncludePeriods.Add(new TimeRange(new DateTime(2013, 1, 1, 13, 0, 0), new DateTime(2013, 1, 1, 17, 0, 0)));

            // exclude interval
            var interval = new TimeInterval(TimeSpec.MinPeriodDate, new DateTime(2013, 1, 1, 14, 0, 0),
                                                                                             IntervalEdge.Closed, IntervalEdge.Open);
            dateAdd.ExcludePeriods.Add(interval);
            var result = dateAdd.Add(dateAdd.IncludePeriods.Start, TimeSpan.FromHours(2), SeekBoundaryMode.Fill);
            Assert.NotNull(result);
            Assert.Equal(result, new DateTime(2013, 1, 1, 16, 0, 0).AddTicks(-1));

            // exclude time range
            dateAdd.ExcludePeriods.Clear();
            dateAdd.ExcludePeriods.Add(new TimeRange(interval.StartInterval, interval.EndInterval));
            result = dateAdd.Add(dateAdd.IncludePeriods.Start, TimeSpan.FromHours(2), SeekBoundaryMode.Fill);
            Assert.NotNull(result);
            Assert.Equal(result, new DateTime(2013, 1, 1, 16, 0, 0));
        } // ExcludePeriodTest

    } // class DateAddTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
