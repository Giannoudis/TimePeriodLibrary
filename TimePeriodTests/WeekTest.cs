// -- FILE ------------------------------------------------------------------
// name       : WeekTest.cs
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

    public sealed class WeekTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void DefaultCalendarTest()
        {
            const int startWeek = 1;
            var currentYear = ClockProxy.Clock.Now.Year;
            var cultures = new CultureTestData();
            foreach (var culture in cultures)
            {
                foreach (YearWeekType yearWeekType in Enum.GetValues(typeof(YearWeekType)))
                {
                    var weeksOfYear = TimeTool.GetWeeksOfYear(currentYear, culture, yearWeekType);
                    for (var weekOfYear = startWeek; weekOfYear < weeksOfYear; weekOfYear++)
                    {
                        var week = new Week(currentYear, weekOfYear, TimeCalendar.New(culture, YearMonth.January, yearWeekType));
                        Assert.Equal(currentYear, week.Year);

                        var weekStart = TimeTool.GetStartOfYearWeek(currentYear, weekOfYear, culture, yearWeekType);
                        var weekEnd = weekStart.AddDays(TimeSpec.DaysPerWeek);
                        Assert.Equal(weekStart.Add(week.Calendar.StartOffset), week.Start);
                        Assert.Equal(weekEnd.Add(week.Calendar.EndOffset), week.End);
                    }
                }
            }
        } // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void EnAuCultureTest()
        {
            var cultureInfo = new CultureInfo("en-AU")
            {
                DateTimeFormat =
                {
                    FirstDayOfWeek = DayOfWeek.Monday
                }
            };
            var calendar = new TimeCalendar(new TimeCalendarConfig
            {
                Culture = cultureInfo
            });
            var week = new Week(new DateTime(2011, 4, 1, 9, 0, 0), calendar);
            Assert.Equal(new DateTime(2011, 3, 28), week.Start);
        } // EnAuCultureTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void DanishUsCultureTest()
        {
            var danishCulture = new CultureInfo("da-DK");
            var danishWeek = new Week(2011, 36, new TimeCalendar(new TimeCalendarConfig { Culture = danishCulture }));
            Assert.Equal(new DateTime(2011, 9, 5), danishWeek.Start.Date);
            Assert.Equal(new DateTime(2011, 9, 11), danishWeek.End.Date);

            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek
                }
            };
            var usWeek = new Week(2011, 36, new TimeCalendar(new TimeCalendarConfig { Culture = usCulture }));
            Assert.Equal(new DateTime(2011, 9, 4), usWeek.Start.Date);
            Assert.Equal(new DateTime(2011, 9, 10), usWeek.End.Date);
        } // DanishUsCultureTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void Iso8601WithMondayAsStartWeekTest()
        {
            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek = DayOfWeek.Monday
                }
            };

            var isoCalendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearWeekType = YearWeekType.Iso8601,
                        Culture = usCulture
                    });

            var weekFrom20131229 = new Week(new DateTime(2013, 12, 29), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 23), weekFrom20131229.Start.Date);
            Assert.Equal(new DateTime(2013, 12, 29), weekFrom20131229.End.Date);
            Assert.Equal(52, weekFrom20131229.WeekOfYear);

            var weekFrom20131230 = new Week(new DateTime(2013, 12, 30), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 30), weekFrom20131230.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 5), weekFrom20131230.End.Date);
            Assert.Equal(1, weekFrom20131230.WeekOfYear);

            var weekFrom20140105 = new Week(new DateTime(2014, 1, 5), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 30), weekFrom20140105.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 5), weekFrom20140105.End.Date);
            Assert.Equal(1, weekFrom20140105.WeekOfYear);

            var weekFrom20140106 = new Week(new DateTime(2014, 1, 6), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 6), weekFrom20140106.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 12), weekFrom20140106.End.Date);
            Assert.Equal(2, weekFrom20140106.WeekOfYear);
        } // Iso8601WithMondayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void Iso8601WithTuesdayAsStartWeekTest()
        {
            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek = DayOfWeek.Tuesday
                }
            };

            var isoCalendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearWeekType = YearWeekType.Iso8601,
                        Culture = usCulture
                    });

            var weekFrom20131230 = new Week(new DateTime(2013, 12, 30), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 24), weekFrom20131230.Start.Date);
            Assert.Equal(new DateTime(2013, 12, 30), weekFrom20131230.End.Date);
            Assert.Equal(52, weekFrom20131230.WeekOfYear);

            var weekFrom20131231 = new Week(new DateTime(2013, 12, 31), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 31), weekFrom20131231.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 6), weekFrom20131231.End.Date);
            Assert.Equal(1, weekFrom20131231.WeekOfYear);

            var weekFrom20140106 = new Week(new DateTime(2014, 1, 6), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 31), weekFrom20140106.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 6), weekFrom20140106.End.Date);
            Assert.Equal(1, weekFrom20140106.WeekOfYear);

            var weekFrom20140107 = new Week(new DateTime(2014, 1, 7), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 7), weekFrom20140107.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 13), weekFrom20140107.End.Date);
            Assert.Equal(2, weekFrom20140107.WeekOfYear);
        } // Iso8601WithTuesdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void Iso8601WithWednesdayAsStartWeekTest()
        {
            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek = DayOfWeek.Wednesday
                }
            };

            var isoCalendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearWeekType = YearWeekType.Iso8601,
                        Culture = usCulture
                    });

            var weekFrom20131231 = new Week(new DateTime(2013, 12, 31), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 25), weekFrom20131231.Start.Date);
            Assert.Equal(new DateTime(2013, 12, 31), weekFrom20131231.End.Date);
            Assert.Equal(52, weekFrom20131231.WeekOfYear);

            var weekFrom20140101 = new Week(new DateTime(2014, 1, 1), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 1), weekFrom20140101.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 7), weekFrom20140101.End.Date);
            Assert.Equal(1, weekFrom20140101.WeekOfYear);

            var weekFrom20140107 = new Week(new DateTime(2014, 1, 7), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 1), weekFrom20140107.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 7), weekFrom20140107.End.Date);
            Assert.Equal(1, weekFrom20140107.WeekOfYear);

            var weekFrom20140108 = new Week(new DateTime(2014, 1, 8), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 8), weekFrom20140108.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 14), weekFrom20140108.End.Date);
            Assert.Equal(2, weekFrom20140108.WeekOfYear);
        } // Iso8601WithWednesdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void Iso8601WithThursdayAsStartWeekTest()
        {
            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek = DayOfWeek.Thursday
                }
            };

            var isoCalendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearWeekType = YearWeekType.Iso8601,
                        Culture = usCulture
                    });

            var weekFrom20140101 = new Week(new DateTime(2014, 1, 1), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 26), weekFrom20140101.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 1), weekFrom20140101.End.Date);
            Assert.Equal(52, weekFrom20140101.WeekOfYear);

            var weekFrom20140102 = new Week(new DateTime(2014, 1, 2), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 2), weekFrom20140102.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 8), weekFrom20140102.End.Date);
            Assert.Equal(1, weekFrom20140102.WeekOfYear);

            var weekFrom20140108 = new Week(new DateTime(2014, 1, 8), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 2), weekFrom20140108.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 8), weekFrom20140108.End.Date);
            Assert.Equal(1, weekFrom20140108.WeekOfYear);

            var weekFrom20140109 = new Week(new DateTime(2014, 1, 9), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 9), weekFrom20140109.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 15), weekFrom20140109.End.Date);
            Assert.Equal(2, weekFrom20140109.WeekOfYear);
        } // Iso8601WithThursdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void Iso8601WithFridayAsStartWeekTest()
        {
            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek = DayOfWeek.Friday
                }
            };

            var isoCalendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearWeekType = YearWeekType.Iso8601,
                        Culture = usCulture
                    });

            var weekFrom20140102 = new Week(new DateTime(2014, 1, 2), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 27), weekFrom20140102.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 2), weekFrom20140102.End.Date);
            Assert.Equal(52, weekFrom20140102.WeekOfYear);

            var weekFrom20140103 = new Week(new DateTime(2014, 1, 3), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 3), weekFrom20140103.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 9), weekFrom20140103.End.Date);
            Assert.Equal(1, weekFrom20140103.WeekOfYear);

            var weekFrom20140109 = new Week(new DateTime(2014, 1, 9), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 3), weekFrom20140109.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 9), weekFrom20140109.End.Date);
            Assert.Equal(1, weekFrom20140109.WeekOfYear);

            var weekFrom20140110 = new Week(new DateTime(2014, 1, 10), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 10), weekFrom20140110.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 16), weekFrom20140110.End.Date);
            Assert.Equal(2, weekFrom20140110.WeekOfYear);
        } // Iso8601WithFridayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void Iso8601WithSaturdayAsStartWeekTest()
        {
            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek = DayOfWeek.Saturday
                }
            };

            var isoCalendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearWeekType = YearWeekType.Iso8601,
                        Culture = usCulture
                    });

            var weekFrom20140103 = new Week(new DateTime(2014, 1, 3), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 28), weekFrom20140103.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 3), weekFrom20140103.End.Date);
            Assert.Equal(53, weekFrom20140103.WeekOfYear);

            var weekFrom20140104 = new Week(new DateTime(2014, 1, 4), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 4), weekFrom20140104.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 10), weekFrom20140104.End.Date);
            Assert.Equal(1, weekFrom20140104.WeekOfYear);

            var weekFrom20140110 = new Week(new DateTime(2014, 1, 10), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 4), weekFrom20140110.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 10), weekFrom20140110.End.Date);
            Assert.Equal(1, weekFrom20140110.WeekOfYear);

            var weekFrom20140111 = new Week(new DateTime(2014, 1, 11), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 11), weekFrom20140111.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 17), weekFrom20140111.End.Date);
            Assert.Equal(2, weekFrom20140111.WeekOfYear);
        } // Iso8601WithSaturdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
        public void Iso8601WithSundayAsStartWeekTest()
        {
            var usCulture = new CultureInfo("en-US")
            {
                DateTimeFormat =
                {
                    CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek = DayOfWeek.Sunday
                }
            };

            var isoCalendar = new TimeCalendar(
                    new TimeCalendarConfig
                    {
                        YearWeekType = YearWeekType.Iso8601,
                        Culture = usCulture
                    });

            var weekFrom20131228 = new Week(new DateTime(2013, 12, 28), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 22), weekFrom20131228.Start.Date);
            Assert.Equal(new DateTime(2013, 12, 28), weekFrom20131228.End.Date);
            Assert.Equal(52, weekFrom20131228.WeekOfYear);

            var weekFrom20131229 = new Week(new DateTime(2013, 12, 29), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 29), weekFrom20131229.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 4), weekFrom20131229.End.Date);
            Assert.Equal(1, weekFrom20131229.WeekOfYear);

            var weekFrom20140104 = new Week(new DateTime(2014, 1, 4), isoCalendar);
            Assert.Equal(new DateTime(2013, 12, 29), weekFrom20140104.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 4), weekFrom20140104.End.Date);
            Assert.Equal(1, weekFrom20140104.WeekOfYear);

            var weekFrom20140105 = new Week(new DateTime(2014, 1, 5), isoCalendar);
            Assert.Equal(new DateTime(2014, 1, 5), weekFrom20140105.Start.Date);
            Assert.Equal(new DateTime(2014, 1, 11), weekFrom20140105.End.Date);
            Assert.Equal(2, weekFrom20140105.WeekOfYear);
        } // Iso8601WithSundayAsStartWeekTest

    } // class WeekTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
