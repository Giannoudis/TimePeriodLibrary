// -- FILE ------------------------------------------------------------------
// name       : TimeFormatterTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.20
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimeFormatterTest : TestUnitBase
    {

#if (!NETCOREAPP1_1)
        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
        public void GetShortDateTest()
        {
            var moment = new DateTime(2013, 12, 15, 17, 34, 54, 900);

            Assert.Equal(moment.ToShortDateString(), moment.ToString("d"));
            Assert.Equal(moment.ToShortTimeString(), moment.ToString("t"));
            Assert.Equal(moment.ToLongTimeString(), moment.ToString("T"));
        } // GetShortDateTest
#endif

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
        public void SameDayTest()
        {
            var start = new DateTime(2013, 12, 15, 17, 34, 54, 900);
            var end = new DateTime(2013, 12, 15, 17, 35, 0, 0);
            var duration = new TimeSpan(3, 4, 5);

            var formatter = new TimeFormatter();
            var interval = formatter.GetInterval(
                start,
                end,
                IntervalEdge.Open,
                IntervalEdge.Open,
                duration);

            var expectedInterval = string.Concat(
                "(",
                formatter.GetDateTime(start),
                formatter.StartEndSeparator,
                formatter.GetLongTime(end),
                ")",
                formatter.DurationSeparator,
                formatter.GetDuration(duration));
            Assert.Equal(interval, expectedInterval);
        } // SameDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
        public void DifferentDayWithTimeTest()
        {
            var start = new DateTime(2013, 12, 15, 17, 34, 54, 900);
            var end = new DateTime(2013, 12, 16, 17, 35, 0, 0);
            var duration = new TimeSpan(3, 4, 5);

            var formatter = new TimeFormatter();
            var interval = formatter.GetInterval(
                start,
                end,
                IntervalEdge.Open,
                IntervalEdge.Open,
                duration);

            var expectedInterval = string.Concat(
                "(",
                formatter.GetDateTime(start),
                formatter.StartEndSeparator,
                formatter.GetDateTime(end),
                ")",
                formatter.DurationSeparator,
                formatter.GetDuration(duration));
            Assert.Equal(interval, expectedInterval);
        } // DifferentDayWithTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
        public void DifferentDayWithoutTimeTest()
        {
            var start = new DateTime(2013, 12, 15);
            var end = new DateTime(2013, 12, 16);
            var duration = new TimeSpan(3, 4, 5);

            var formatter = new TimeFormatter();
            var interval = formatter.GetInterval(
                start,
                end,
                IntervalEdge.Open,
                IntervalEdge.Open,
                duration);

            var expectedInterval = string.Concat(
                "(",
                formatter.GetShortDate(start),
                formatter.StartEndSeparator,
                formatter.GetShortDate(end),
                ")",
                formatter.DurationSeparator,
                formatter.GetDuration(duration));
            Assert.Equal(interval, expectedInterval);
        } // DifferentDayWithoutTimeTest


        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
public void ToStringCultureTest()
{
    var start = new DateTime(2013, 12, 15);

    var culture = new CultureInfo("en-US");
    var enUsFormatter = new TimeFormatter(culture);
    Assert.Equal(start.ToString(culture), enUsFormatter.GetDateTime(start) );
} // ToStringCultureTest

    } // class TimeCompareTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
