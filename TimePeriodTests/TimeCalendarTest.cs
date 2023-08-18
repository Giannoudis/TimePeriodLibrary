// -- FILE ------------------------------------------------------------------
// name       : TimeCalendarTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TimeCalendarTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void StartOffsetTest()
        {
            Assert.Equal(new TimeCalendar().StartOffset, TimeCalendar.DefaultStartOffset);

            var offset = Duration.Second;
            var timeCalendar = new TimeCalendar(new TimeCalendarConfig
            {
                StartOffset = offset,
                EndOffset = TimeSpan.Zero
            });
            Assert.Equal(timeCalendar.StartOffset, offset);
        } // StartOffsetTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void EndOffsetTest()
        {
            Assert.Equal(new TimeCalendar().EndOffset, TimeCalendar.DefaultEndOffset);

            var offset = Duration.Second.Negate();
            var timeCalendar = new TimeCalendar(new TimeCalendarConfig
            {
                StartOffset = TimeSpan.Zero,
                EndOffset = offset
            });
            Assert.Equal(timeCalendar.EndOffset, offset);
        } // EndOffsetTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void InvalidOffset1Test()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TimeCalendar(new TimeCalendarConfig
                {
                    StartOffset = Duration.Second.Negate(),
                    EndOffset = TimeSpan.Zero
                })));
        } // InvalidOffset1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void InvalidOffset2Test()
        {
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TimeCalendar(new TimeCalendarConfig
                {
                    StartOffset = TimeSpan.Zero,
                    EndOffset = Duration.Second
                })));
        } // InvalidOffset2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void CultureTest()
        {
            Assert.Equal(new TimeCalendar().Culture, CultureInfo.CurrentCulture);
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).Culture, culture);
            }
        } // CultureTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void FirstDayOfWeekTest()
        {
            Assert.Equal(new TimeCalendar().FirstDayOfWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).FirstDayOfWeek, culture.DateTimeFormat.FirstDayOfWeek);
            }
        } // FirstDayOfWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void MapStartTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).MapStart(now), now);
                Assert.Equal(TimeCalendar.New(culture, offset, TimeSpan.Zero).MapStart(now), now.Add(offset));

                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).MapStart(testDate), testDate);
                Assert.Equal(TimeCalendar.New(culture, offset, TimeSpan.Zero).MapStart(testDate), testDate.Add(offset));
            }
        } // MapStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void UnmapStartTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).UnmapStart(now), now);
                Assert.Equal(TimeCalendar.New(culture, offset, TimeSpan.Zero).UnmapStart(now), now.Subtract(offset));

                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).UnmapStart(testDate), testDate);
                Assert.Equal(TimeCalendar.New(culture, offset, TimeSpan.Zero).UnmapStart(testDate), testDate.Subtract(offset));
            }
        } // UnmapStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void MapEndTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second.Negate();
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).MapEnd(now), now);
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, offset).MapEnd(now), now.Add(offset));

                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).MapEnd(testDate), testDate);
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, offset).MapEnd(testDate), testDate.Add(offset));
            }
        } // MapEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void UnmapEndTest()
        {
            var now = ClockProxy.Clock.Now;
            var offset = Duration.Second.Negate();
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).UnmapEnd(now), now);
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, offset).UnmapEnd(now), now.Subtract(offset));

                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, TimeSpan.Zero).UnmapEnd(testDate), testDate);
                Assert.Equal(TimeCalendar.New(culture, TimeSpan.Zero, offset).UnmapEnd(testDate), testDate.Subtract(offset));
            }
        } // UnmapEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetYearTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetYear(now), culture.Calendar.GetYear(now));
                Assert.Equal(TimeCalendar.New(culture).GetYear(testDate), culture.Calendar.GetYear(testDate));
            }
        } // GetYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetMonthTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetMonth(now), culture.Calendar.GetMonth(now));
                Assert.Equal(TimeCalendar.New(culture).GetMonth(testDate), culture.Calendar.GetMonth(testDate));
            }
        } // GetMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetHourTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetHour(now), culture.Calendar.GetHour(now));
                Assert.Equal(TimeCalendar.New(culture).GetHour(testDate), culture.Calendar.GetHour(testDate));
            }
        } // GetHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetMinuteTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetMinute(now), culture.Calendar.GetMinute(now));
                Assert.Equal(TimeCalendar.New(culture).GetMinute(testDate), culture.Calendar.GetMinute(testDate));
            }
        } // GetMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetDayOfMonthTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetDayOfMonth(now), culture.Calendar.GetDayOfMonth(now));
                Assert.Equal(TimeCalendar.New(culture).GetDayOfMonth(testDate), culture.Calendar.GetDayOfMonth(testDate));
            }
        } // GetDayOfMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetDayOfWeekTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetDayOfWeek(now), culture.Calendar.GetDayOfWeek(now));
                Assert.Equal(TimeCalendar.New(culture).GetDayOfWeek(testDate), culture.Calendar.GetDayOfWeek(testDate));
            }
        } // GetDayOfWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetDaysInMonthTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetDaysInMonth(now.Year, now.Month), culture.Calendar.GetDaysInMonth(now.Year, now.Month));
                Assert.Equal(TimeCalendar.New(culture).GetDaysInMonth(testDate.Year, testDate.Month), culture.Calendar.GetDaysInMonth(testDate.Year, testDate.Month));
            }
        } // GetDaysInMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetMonthNameTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetMonthName(now.Month), culture.DateTimeFormat.GetMonthName(now.Month));
                Assert.Equal(TimeCalendar.New(culture).GetMonthName(testDate.Month), culture.DateTimeFormat.GetMonthName(testDate.Month));
            }
        } // GetMonthNameTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetDayNameTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                Assert.Equal(TimeCalendar.New(culture).GetDayName(now.DayOfWeek), culture.DateTimeFormat.GetDayName(now.DayOfWeek));
                Assert.Equal(TimeCalendar.New(culture).GetDayName(testDate.DayOfWeek), culture.DateTimeFormat.GetDayName(testDate.DayOfWeek));
            }
        } // GetDayNameTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetWeekOfYearTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                foreach (CalendarWeekRule weekRule in Enum.GetValues(typeof(CalendarWeekRule)))
                {
                    culture.DateTimeFormat.CalendarWeekRule = weekRule;

                    // calendar week
                    var timeCalendar = TimeCalendar.New(culture);
                    TimeTool.GetWeekOfYear(now, culture, weekRule, culture.DateTimeFormat.FirstDayOfWeek, YearWeekType.Calendar,
                                                                    out _, out var weekOfYear);
                    Assert.Equal(timeCalendar.GetWeekOfYear(now), weekOfYear);

                    // iso 8601 calendar week
                    var timeCalendarIso8601 = TimeCalendar.New(culture, YearMonth.January, YearWeekType.Iso8601);
                    TimeTool.GetWeekOfYear(now, culture, weekRule, culture.DateTimeFormat.FirstDayOfWeek, YearWeekType.Iso8601,
                                                                    out _, out weekOfYear);
                    Assert.Equal(timeCalendarIso8601.GetWeekOfYear(now), weekOfYear);
                }
            }
        } // GetWeekOfYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
        public void GetStartOfWeekTest()
        {
            var now = ClockProxy.Clock.Now;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                foreach (CalendarWeekRule weekRule in Enum.GetValues(typeof(CalendarWeekRule)))
                {
                    culture.DateTimeFormat.CalendarWeekRule = weekRule;

                    // calendar week
                    TimeTool.GetWeekOfYear(now, culture, YearWeekType.Calendar, out var year, out var weekOfYear);
                    var weekStartCalendar = TimeTool.GetStartOfYearWeek(year, weekOfYear, culture, YearWeekType.Calendar);
                    var timeCalendar = TimeCalendar.New(culture);
                    Assert.Equal(timeCalendar.GetStartOfYearWeek(year, weekOfYear), weekStartCalendar);

                    // iso 8601 calendar week
                    TimeTool.GetWeekOfYear(now, culture, YearWeekType.Iso8601, out year, out weekOfYear);
                    var weekStartCalendarIso8601 = TimeTool.GetStartOfYearWeek(year, weekOfYear, culture, YearWeekType.Iso8601);
                    var timeCalendarIso8601 = TimeCalendar.New(culture, YearMonth.January, YearWeekType.Iso8601);
                    Assert.Equal(timeCalendarIso8601.GetStartOfYearWeek(year, weekOfYear), weekStartCalendarIso8601);
                }
            }
        } // GetStartOfWeekTest

        // ----------------------------------------------------------------------
        // members
        private readonly DateTime testDate = new(2010, 3, 18, 14, 9, 34, 234);

    } // class TimeCalendarTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
