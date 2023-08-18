// -- FILE ------------------------------------------------------------------
// name       : HalfyearTest.cs
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

    public sealed class HalfyearTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void InitValuesTest()
        {
            var now = ClockProxy.Clock.Now;
            var firstHalfyear = new DateTime(now.Year, 1, 1);
            var secondHalfyear = new DateTime(now.Year, 7, 1);
            var halfyear = new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.NewEmptyOffset());

            Assert.Equal(halfyear.Start.Year, firstHalfyear.Year);
            Assert.Equal(halfyear.Start.Month, firstHalfyear.Month);
            Assert.Equal(halfyear.Start.Day, firstHalfyear.Day);
            Assert.Equal(0, halfyear.Start.Hour);
            Assert.Equal(0, halfyear.Start.Minute);
            Assert.Equal(0, halfyear.Start.Second);
            Assert.Equal(0, halfyear.Start.Millisecond);

            Assert.Equal(halfyear.End.Year, secondHalfyear.Year);
            Assert.Equal(halfyear.End.Month, secondHalfyear.Month);
            Assert.Equal(halfyear.End.Day, secondHalfyear.Day);
            Assert.Equal(0, halfyear.End.Hour);
            Assert.Equal(0, halfyear.End.Minute);
            Assert.Equal(0, halfyear.End.Second);
            Assert.Equal(0, halfyear.End.Millisecond);
        } // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void DefaultCalendarTest()
        {
            var yearStart = new DateTime(ClockProxy.Clock.Now.Year, 1, 1);
            foreach (YearHalfyear yearHalfyear in Enum.GetValues(typeof(YearHalfyear)))
            {
                var offset = (int)yearHalfyear - 1;
                var halfyear = new Halfyear(yearStart.AddMonths(TimeSpec.MonthsPerHalfyear * offset));
                Assert.Equal(YearMonth.January, halfyear.YearBaseMonth);
                Assert.Equal(halfyear.BaseYear, yearStart.Year);
                Assert.Equal(halfyear.Start, yearStart.AddMonths(TimeSpec.MonthsPerHalfyear * offset).Add(halfyear.Calendar.StartOffset));
                Assert.Equal(halfyear.End, yearStart.AddMonths(TimeSpec.MonthsPerHalfyear * (offset + 1)).Add(halfyear.Calendar.EndOffset));
            }
        } // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void MomentTest()
        {
            var now = ClockProxy.Clock.Now;
            var timeCalendar = TimeCalendar.New(YearMonth.April);

            Assert.Equal(new Halfyear().YearHalfyear, now.Month <= 6 ? YearHalfyear.First : YearHalfyear.Second);

            Assert.Equal(YearHalfyear.First, new Halfyear(new DateTime(now.Year, 1, 1)).YearHalfyear);
            Assert.Equal(YearHalfyear.First, new Halfyear(new DateTime(now.Year, 6, 30)).YearHalfyear);
            Assert.Equal(YearHalfyear.Second, new Halfyear(new DateTime(now.Year, 7, 1)).YearHalfyear);
            Assert.Equal(YearHalfyear.Second, new Halfyear(new DateTime(now.Year, 12, 31)).YearHalfyear);

            Assert.Equal(YearHalfyear.First, new Halfyear(new DateTime(now.Year, 4, 1), timeCalendar).YearHalfyear);
            Assert.Equal(YearHalfyear.First, new Halfyear(new DateTime(now.Year, 9, 30), timeCalendar).YearHalfyear);
            Assert.Equal(YearHalfyear.Second, new Halfyear(new DateTime(now.Year, 10, 1), timeCalendar).YearHalfyear);
            Assert.Equal(YearHalfyear.Second, new Halfyear(new DateTime(now.Year, 3, 31), timeCalendar).YearHalfyear);
        } // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void YearBaseMonthTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var halfyear = new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.April));
            Assert.Equal(YearMonth.April, halfyear.YearBaseMonth);
            Assert.Equal(YearMonth.January, new Halfyear(currentYear, YearHalfyear.Second).YearBaseMonth);
        } // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void YearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            Assert.Equal(new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.April)).BaseYear, currentYear);
            Assert.Equal(2006, new Halfyear(2006, YearHalfyear.First).BaseYear);
        } // YearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void YearHalfyearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            Assert.Equal(YearHalfyear.First, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.April)).YearHalfyear);
            Assert.Equal(YearHalfyear.Second, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.April)).YearHalfyear);
        } // YearHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void StartMonthTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;

            Assert.Equal(YearMonth.January, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.January)).StartMonth);
            Assert.Equal(YearMonth.February, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.February)).StartMonth);
            Assert.Equal(YearMonth.March, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.March)).StartMonth);
            Assert.Equal(YearMonth.April, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.April)).StartMonth);
            Assert.Equal(YearMonth.May, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.May)).StartMonth);
            Assert.Equal(YearMonth.June, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.June)).StartMonth);
            Assert.Equal(YearMonth.July, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.July)).StartMonth);
            Assert.Equal(YearMonth.August, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.August)).StartMonth);
            Assert.Equal(YearMonth.September, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.September)).StartMonth);
            Assert.Equal(YearMonth.October, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.October)).StartMonth);
            Assert.Equal(YearMonth.November, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.November)).StartMonth);
            Assert.Equal(YearMonth.December, new Halfyear(currentYear, YearHalfyear.First, TimeCalendar.New(YearMonth.December)).StartMonth);

            Assert.Equal(YearMonth.July, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.January)).StartMonth);
            Assert.Equal(YearMonth.August, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.February)).StartMonth);
            Assert.Equal(YearMonth.September, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.March)).StartMonth);
            Assert.Equal(YearMonth.October, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.April)).StartMonth);
            Assert.Equal(YearMonth.November, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.May)).StartMonth);
            Assert.Equal(YearMonth.December, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.June)).StartMonth);
            Assert.Equal(YearMonth.January, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.July)).StartMonth);
            Assert.Equal(YearMonth.February, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.August)).StartMonth);
            Assert.Equal(YearMonth.March, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.September)).StartMonth);
            Assert.Equal(YearMonth.April, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.October)).StartMonth);
            Assert.Equal(YearMonth.May, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.November)).StartMonth);
            Assert.Equal(YearMonth.June, new Halfyear(currentYear, YearHalfyear.Second, TimeCalendar.New(YearMonth.December)).StartMonth);
        } // StartMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void IsCalendarHalfyearTest()
        {
            var now = ClockProxy.Clock.Now;

            foreach (YearHalfyear yearHalfyear in Enum.GetValues(typeof(YearHalfyear)))
            {
                Assert.True(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.January)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.February)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.March)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.April)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.May)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.June)).IsCalendarHalfyear);
                Assert.True(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.July)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.August)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.September)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.October)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.November)).IsCalendarHalfyear);
                Assert.False(new Halfyear(now.Year, yearHalfyear, TimeCalendar.New(YearMonth.December)).IsCalendarHalfyear);
            }
        } // IsCalendarHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void MultipleCalendarYearsTest()
        {
            var now = ClockProxy.Clock.Now;

            Assert.False(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.January)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.February)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.March)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.April)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.May)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.June)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.July)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.August)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.September)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.October)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.November)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.First, TimeCalendar.New(YearMonth.December)).MultipleCalendarYears);

            Assert.False(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.January)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.February)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.March)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.April)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.May)).MultipleCalendarYears);
            Assert.True(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.June)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.July)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.August)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.September)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.October)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.November)).MultipleCalendarYears);
            Assert.False(new Halfyear(now.Year, YearHalfyear.Second, TimeCalendar.New(YearMonth.December)).MultipleCalendarYears);
        } // MultipleCalendarYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void CalendarHalfyearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var calendar = TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero);

            var h1 = new Halfyear(currentYear, YearHalfyear.First, calendar);
            Assert.True(h1.IsReadOnly);
            Assert.True(h1.IsCalendarHalfyear);
            Assert.Equal(TimeSpec.CalendarYearStartMonth, h1.YearBaseMonth);
            Assert.Equal(YearHalfyear.First, h1.YearHalfyear);
            Assert.Equal(h1.BaseYear, currentYear);
            Assert.Equal(h1.Start, new DateTime(currentYear, 1, 1));
            Assert.Equal(h1.End, new DateTime(currentYear, 7, 1));

            var h2 = new Halfyear(currentYear, YearHalfyear.Second, calendar);
            Assert.True(h2.IsReadOnly);
            Assert.True(h2.IsCalendarHalfyear);
            Assert.Equal(TimeSpec.CalendarYearStartMonth, h2.YearBaseMonth);
            Assert.Equal(YearHalfyear.Second, h2.YearHalfyear);
            Assert.Equal(h2.BaseYear, currentYear);
            Assert.Equal(h2.Start, new DateTime(currentYear, 7, 1));
            Assert.Equal(h2.End, new DateTime(currentYear + 1, 1, 1));
        } // CalendarHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void DefaultHalfyearTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            const YearMonth yearStartMonth = YearMonth.April;
            var calendar = TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero, yearStartMonth);

            var h1 = new Halfyear(currentYear, YearHalfyear.First, calendar);
            Assert.True(h1.IsReadOnly);
            Assert.False(h1.IsCalendarHalfyear);
            Assert.Equal(yearStartMonth, h1.YearBaseMonth);
            Assert.Equal(YearHalfyear.First, h1.YearHalfyear);
            Assert.Equal(h1.BaseYear, currentYear);
            Assert.Equal(h1.Start, new DateTime(currentYear, 4, 1));
            Assert.Equal(h1.End, new DateTime(currentYear, 10, 1));

            var h2 = new Halfyear(currentYear, YearHalfyear.Second, calendar);
            Assert.True(h2.IsReadOnly);
            Assert.False(h2.IsCalendarHalfyear);
            Assert.Equal(yearStartMonth, h2.YearBaseMonth);
            Assert.Equal(YearHalfyear.Second, h2.YearHalfyear);
            Assert.Equal(h2.BaseYear, currentYear);
            Assert.Equal(h2.Start, new DateTime(currentYear, 10, 1));
            Assert.Equal(h2.End, new DateTime(currentYear + 1, 4, 1));
        } // DefaultHalfyearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void GetQuartersTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var timeCalendar = TimeCalendar.New(YearMonth.October);
            var h1 = new Halfyear(currentYear, YearHalfyear.First, timeCalendar);

            var h1Quarters = h1.GetQuarters();
            Assert.NotNull(h1Quarters);

            var h1Index = 0;
            foreach (var timePeriod in h1Quarters)
            {
                var h1Quarter = (Quarter)timePeriod;
                Assert.Equal(h1Quarter.BaseYear, h1.BaseYear);
                Assert.Equal(h1Quarter.YearQuarter, h1Index == 0 ? YearQuarter.First : YearQuarter.Second);
                Assert.Equal(h1Quarter.Start, h1.Start.AddMonths(h1Index * TimeSpec.MonthsPerQuarter));
                Assert.Equal(h1Quarter.End, h1Quarter.Calendar.MapEnd(h1Quarter.Start.AddMonths(TimeSpec.MonthsPerQuarter)));
                h1Index++;
            }
            Assert.Equal(TimeSpec.QuartersPerHalfyear, h1Index);

            var h2 = new Halfyear(currentYear, YearHalfyear.Second, timeCalendar);

            var h2Quarters = h2.GetQuarters();
            Assert.NotNull(h2Quarters);

            var h2Index = 0;
            foreach (var timePeriod in h2Quarters)
            {
                var h2Quarter = (Quarter)timePeriod;
                Assert.Equal(h2Quarter.BaseYear, h2.BaseYear);
                Assert.Equal(h2Quarter.YearQuarter, h2Index == 0 ? YearQuarter.Third : YearQuarter.Fourth);
                Assert.Equal(h2Quarter.Start, h2.Start.AddMonths(h2Index * TimeSpec.MonthsPerQuarter));
                Assert.Equal(h2Quarter.End, h2Quarter.Calendar.MapEnd(h2Quarter.Start.AddMonths(TimeSpec.MonthsPerQuarter)));
                h2Index++;
            }
            Assert.Equal(TimeSpec.QuartersPerHalfyear, h2Index);
        } // GetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void GetMonthsTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            var timeCalendar = TimeCalendar.New(YearMonth.October);
            var halfyear = new Halfyear(currentYear, YearHalfyear.First, timeCalendar);

            var months = halfyear.GetMonths();
            Assert.NotNull(months);

            var index = 0;
            foreach (var timePeriod in months)
            {
                var month = (Month)timePeriod;
                Assert.Equal(month.Start, halfyear.Start.AddMonths(index));
                Assert.Equal(month.End, month.Calendar.MapEnd(month.Start.AddMonths(1)));
                index++;
            }
            Assert.Equal(TimeSpec.MonthsPerHalfyear, index);
        } // GetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Halfyear")]
        [Fact]
        public void AddHalfyearsTest()
        {
            var currentYear = ClockProxy.Clock.Now.Year;
            const YearMonth yearStartMonth = YearMonth.April;
            var calendar = TimeCalendar.New(TimeSpan.Zero, TimeSpan.Zero, yearStartMonth);

            var calendarStartDate = new DateTime(currentYear, 4, 1);
            var calendarHalfyear = new Halfyear(currentYear, YearHalfyear.First, calendar);

            Assert.Equal(calendarHalfyear.AddHalfyears(0), calendarHalfyear);

            var prevH1 = calendarHalfyear.AddHalfyears(-1);
            Assert.Equal(YearHalfyear.Second, prevH1.YearHalfyear);
            Assert.Equal(prevH1.BaseYear, currentYear - 1);
            Assert.Equal(prevH1.Start, calendarStartDate.AddMonths(-6));
            Assert.Equal(prevH1.End, calendarStartDate);

            var prevH2 = calendarHalfyear.AddHalfyears(-2);
            Assert.Equal(YearHalfyear.First, prevH2.YearHalfyear);
            Assert.Equal(prevH2.BaseYear, currentYear - 1);
            Assert.Equal(prevH2.Start, calendarStartDate.AddMonths(-12));
            Assert.Equal(prevH2.End, calendarStartDate.AddMonths(-6));

            var prevH3 = calendarHalfyear.AddHalfyears(-3);
            Assert.Equal(YearHalfyear.Second, prevH3.YearHalfyear);
            Assert.Equal(prevH3.BaseYear, currentYear - 2);
            Assert.Equal(prevH3.Start, calendarStartDate.AddMonths(-18));
            Assert.Equal(prevH3.End, calendarStartDate.AddMonths(-12));

            var futureH1 = calendarHalfyear.AddHalfyears(1);
            Assert.Equal(YearHalfyear.Second, futureH1.YearHalfyear);
            Assert.Equal(futureH1.BaseYear, currentYear);
            Assert.Equal(futureH1.Start, calendarStartDate.AddMonths(6));
            Assert.Equal(futureH1.End, calendarStartDate.AddMonths(12));

            var futureH2 = calendarHalfyear.AddHalfyears(2);
            Assert.Equal(YearHalfyear.First, futureH2.YearHalfyear);
            Assert.Equal(futureH2.BaseYear, currentYear + 1);
            Assert.Equal(futureH2.Start, calendarStartDate.AddMonths(12));
            Assert.Equal(futureH2.End, calendarStartDate.AddMonths(18));

            var futureH3 = calendarHalfyear.AddHalfyears(3);
            Assert.Equal(YearHalfyear.Second, futureH3.YearHalfyear);
            Assert.Equal(futureH3.BaseYear, currentYear + 1);
            Assert.Equal(futureH3.Start, calendarStartDate.AddMonths(18));
            Assert.Equal(futureH3.End, calendarStartDate.AddMonths(24));
        } // AddHalfyearsTest

        // ----------------------------------------------------------------------
        private ITimeCalendar GetFiscalYearCalendar(FiscalYearAlignment yearAlignment)
        {
            return new TimeCalendar(
                new TimeCalendarConfig
                {
                    YearType = YearType.FiscalYear,
                    YearBaseMonth = YearMonth.September,
                    FiscalFirstDayOfYear = DayOfWeek.Sunday,
                    FiscalYearAlignment = yearAlignment,
                    FiscalQuarterGrouping = FiscalQuarterGrouping.FourFourFiveWeeks
                });
        } // GetFiscalYearCalendar

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "Halfyear")]
        [Fact]
        public void GetFiscalQuartersTest()
        {
            var halfyear = new Halfyear(2006, YearHalfyear.First, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var quarters = halfyear.GetQuarters();

            Assert.NotNull(quarters);
            Assert.Equal(TimeSpec.QuartersPerHalfyear, quarters.Count);

            Assert.Equal(quarters[0].Start.Date, halfyear.Start);
            Assert.Equal(quarters[TimeSpec.QuartersPerHalfyear - 1].End, halfyear.End);
        } // GetFiscalQuartersTest

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "Halfyear")]
        [Fact]
        public void FiscalYearGetMonthsTest()
        {
            var halfyear = new Halfyear(2006, YearHalfyear.First, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            var months = halfyear.GetMonths();
            Assert.NotNull(months);
            Assert.Equal(TimeSpec.MonthsPerHalfyear, months.Count);

            Assert.Equal(months[0].Start, new DateTime(2006, 8, 27));
            for (var i = 0; i < months.Count; i++)
            {
                Assert.Equal(months[i].Duration.Subtract(TimeCalendar.DefaultEndOffset).Days,
                    (i + 1) % 3 == 0 ? TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth);
            }
            Assert.Equal(months[TimeSpec.MonthsPerHalfyear - 1].End, halfyear.End);
        } // FiscalYearGetMonthsTest

    } // class HalfyearTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
