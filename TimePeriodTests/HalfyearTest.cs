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
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class HalfyearTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstHalfyear = new DateTime( now.Year, 1, 1 );
			DateTime secondHalfyear = new DateTime( now.Year, 7, 1 );
			Halfyear halfyear = new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( halfyear.Start.Year, firstHalfyear.Year );
			Assert.AreEqual( halfyear.Start.Month, firstHalfyear.Month );
			Assert.AreEqual( halfyear.Start.Day, firstHalfyear.Day );
			Assert.AreEqual( halfyear.Start.Hour, 0 );
			Assert.AreEqual( halfyear.Start.Minute, 0 );
			Assert.AreEqual( halfyear.Start.Second, 0 );
			Assert.AreEqual( halfyear.Start.Millisecond, 0 );

			Assert.AreEqual( halfyear.End.Year, secondHalfyear.Year );
			Assert.AreEqual( halfyear.End.Month, secondHalfyear.Month );
			Assert.AreEqual( halfyear.End.Day, secondHalfyear.Day );
			Assert.AreEqual( halfyear.End.Hour, 0 );
			Assert.AreEqual( halfyear.End.Minute, 0 );
			Assert.AreEqual( halfyear.End.Second, 0 );
			Assert.AreEqual( halfyear.End.Millisecond, 0 );
		} // InitValuesTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			DateTime yearStart = new DateTime( ClockProxy.Clock.Now.Year, 1, 1 );
			foreach ( YearHalfyear yearHalfyear in Enum.GetValues( typeof( YearHalfyear ) ) )
			{
				int offset = (int)yearHalfyear - 1;
				Halfyear halfyear = new Halfyear( yearStart.AddMonths( TimeSpec.MonthsPerHalfyear * offset ) );
				Assert.AreEqual( halfyear.YearBaseMonth, YearMonth.January );
				Assert.AreEqual( halfyear.BaseYear, yearStart.Year );
				Assert.AreEqual( halfyear.Start, yearStart.AddMonths( TimeSpec.MonthsPerHalfyear * offset ).Add( halfyear.Calendar.StartOffset ) );
				Assert.AreEqual( halfyear.End, yearStart.AddMonths( TimeSpec.MonthsPerHalfyear * ( offset + 1 ) ).Add( halfyear.Calendar.EndOffset ) );
			}
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeCalendar timeCalendar = TimeCalendar.New( YearMonth.April );

			Assert.AreEqual( new Halfyear().YearHalfyear, now.Month <= 6 ? YearHalfyear.First : YearHalfyear.Second );

			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 1, 1 ) ).YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 6, 30 ) ).YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 7, 1 ) ).YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 12, 31 ) ).YearHalfyear, YearHalfyear.Second );

			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 4, 1 ), timeCalendar ).YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 9, 30 ), timeCalendar ).YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 10, 1 ), timeCalendar ).YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( new Halfyear( new DateTime( now.Year, 3, 31 ), timeCalendar ).YearHalfyear, YearHalfyear.Second );
		} // MomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearBaseMonthTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Halfyear halfyear = new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.April ) );
			Assert.AreEqual( halfyear.YearBaseMonth, YearMonth.April );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second ).YearBaseMonth, YearMonth.January );
		} // YearBaseMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.April ) ).BaseYear, currentYear );
			Assert.AreEqual( new Halfyear( 2006, YearHalfyear.First ).BaseYear, 2006 );
		} // YearTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearHalfyearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.April ) ).YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.April ) ).YearHalfyear, YearHalfyear.Second );
		} // YearHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartMonthTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;

			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.January ) ).StartMonth, YearMonth.January );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.February ) ).StartMonth, YearMonth.February );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.March ) ).StartMonth, YearMonth.March );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.April ) ).StartMonth, YearMonth.April );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.May ) ).StartMonth, YearMonth.May );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.June ) ).StartMonth, YearMonth.June );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.July ) ).StartMonth, YearMonth.July );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.August ) ).StartMonth, YearMonth.August );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.September ) ).StartMonth, YearMonth.September );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.October ) ).StartMonth, YearMonth.October );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.November ) ).StartMonth, YearMonth.November );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.First, TimeCalendar.New( YearMonth.December ) ).StartMonth, YearMonth.December );

			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.January ) ).StartMonth, YearMonth.July );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.February ) ).StartMonth, YearMonth.August );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.March ) ).StartMonth, YearMonth.September );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.April ) ).StartMonth, YearMonth.October );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.May ) ).StartMonth, YearMonth.November );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.June ) ).StartMonth, YearMonth.December );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.July ) ).StartMonth, YearMonth.January );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.August ) ).StartMonth, YearMonth.February );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.September ) ).StartMonth, YearMonth.March );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.October ) ).StartMonth, YearMonth.April );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.November ) ).StartMonth, YearMonth.May );
			Assert.AreEqual( new Halfyear( currentYear, YearHalfyear.Second, TimeCalendar.New( YearMonth.December ) ).StartMonth, YearMonth.June );
		} // StartMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsCalendarHalfyearTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			foreach ( YearHalfyear yearHalfyear in Enum.GetValues( typeof( YearHalfyear ) ) )
			{
				Assert.IsTrue( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.January ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.February ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.March ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.April ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.May ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.June ) ).IsCalendarHalfyear );
				Assert.IsTrue( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.July ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.August ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.September ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.October ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.November ) ).IsCalendarHalfyear );
				Assert.IsFalse( new Halfyear( now.Year, yearHalfyear, TimeCalendar.New( YearMonth.December ) ).IsCalendarHalfyear );
			}
		} // IsCalendarHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void MultipleCalendarYearsTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.First, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );

			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.IsTrue( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.IsFalse( new Halfyear( now.Year, YearHalfyear.Second, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );
		} // MultipleCalendarYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarHalfyearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero );

			Halfyear h1 = new Halfyear( currentYear, YearHalfyear.First, calendar );
			Assert.IsTrue( h1.IsReadOnly );
			Assert.IsTrue( h1.IsCalendarHalfyear );
			Assert.AreEqual( h1.YearBaseMonth, TimeSpec.CalendarYearStartMonth );
			Assert.AreEqual( h1.YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( h1.BaseYear, currentYear );
			Assert.AreEqual( h1.Start, new DateTime( currentYear, 1, 1 ) );
			Assert.AreEqual( h1.End, new DateTime( currentYear, 7, 1 ) );

			Halfyear h2 = new Halfyear( currentYear, YearHalfyear.Second, calendar );
			Assert.IsTrue( h2.IsReadOnly );
			Assert.IsTrue( h2.IsCalendarHalfyear );
			Assert.AreEqual( h2.YearBaseMonth, TimeSpec.CalendarYearStartMonth );
			Assert.AreEqual( h2.YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( h2.BaseYear, currentYear );
			Assert.AreEqual( h2.Start, new DateTime( currentYear, 7, 1 ) );
			Assert.AreEqual( h2.End, new DateTime( currentYear + 1, 1, 1 ) );
		} // CalendarHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultHalfyearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			const YearMonth yearStartMonth = YearMonth.April;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, yearStartMonth );

			Halfyear h1 = new Halfyear( currentYear, YearHalfyear.First, calendar );
			Assert.IsTrue( h1.IsReadOnly );
			Assert.IsFalse( h1.IsCalendarHalfyear );
			Assert.AreEqual( h1.YearBaseMonth, yearStartMonth );
			Assert.AreEqual( h1.YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( h1.BaseYear, currentYear );
			Assert.AreEqual( h1.Start, new DateTime( currentYear, 4, 1 ) );
			Assert.AreEqual( h1.End, new DateTime( currentYear, 10, 1 ) );

			Halfyear h2 = new Halfyear( currentYear, YearHalfyear.Second, calendar );
			Assert.IsTrue( h2.IsReadOnly );
			Assert.IsFalse( h2.IsCalendarHalfyear );
			Assert.AreEqual( h2.YearBaseMonth, yearStartMonth );
			Assert.AreEqual( h2.YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( h2.BaseYear, currentYear );
			Assert.AreEqual( h2.Start, new DateTime( currentYear, 10, 1 ) );
			Assert.AreEqual( h2.End, new DateTime( currentYear + 1, 4, 1 ) );
		} // DefaultHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetQuartersTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar timeCalendar = TimeCalendar.New( YearMonth.October );
			Halfyear h1 = new Halfyear( currentYear, YearHalfyear.First, timeCalendar );

			ITimePeriodCollection h1Quarters = h1.GetQuarters();
			Assert.AreNotEqual( h1Quarters, null );

			int h1Index = 0;
			foreach ( Quarter h1Quarter in h1Quarters )
			{
				Assert.AreEqual( h1Quarter.BaseYear, h1.BaseYear );
				Assert.AreEqual( h1Quarter.YearQuarter, h1Index == 0 ? YearQuarter.First : YearQuarter.Second );
				Assert.AreEqual( h1Quarter.Start, h1.Start.AddMonths( h1Index * TimeSpec.MonthsPerQuarter ) );
				Assert.AreEqual( h1Quarter.End, h1Quarter.Calendar.MapEnd( h1Quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				h1Index++;
			}
			Assert.AreEqual( h1Index, TimeSpec.QuartersPerHalfyear );

			Halfyear h2 = new Halfyear( currentYear, YearHalfyear.Second, timeCalendar );

			ITimePeriodCollection h2Quarters = h2.GetQuarters();
			Assert.AreNotEqual( h2Quarters, null );

			int h2Index = 0;
			foreach ( Quarter h2Quarter in h2Quarters )
			{
				Assert.AreEqual( h2Quarter.BaseYear, h2.BaseYear );
				Assert.AreEqual( h2Quarter.YearQuarter, h2Index == 0 ? YearQuarter.Third : YearQuarter.Fourth );
				Assert.AreEqual( h2Quarter.Start, h2.Start.AddMonths( h2Index * TimeSpec.MonthsPerQuarter ) );
				Assert.AreEqual( h2Quarter.End, h2Quarter.Calendar.MapEnd( h2Quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				h2Index++;
			}
			Assert.AreEqual( h2Index, TimeSpec.QuartersPerHalfyear );
		} // GetQuartersTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthsTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar timeCalendar = TimeCalendar.New( YearMonth.October );
			Halfyear halfyear = new Halfyear( currentYear, YearHalfyear.First, timeCalendar );

			ITimePeriodCollection months = halfyear.GetMonths();
			Assert.AreNotEqual( months, null );

			int index = 0;
			foreach ( Month month in months )
			{
				Assert.AreEqual( month.Start, halfyear.Start.AddMonths( index ) );
				Assert.AreEqual( month.End, month.Calendar.MapEnd( month.Start.AddMonths( 1 ) ) );
				index++;
			}
			Assert.AreEqual( index, TimeSpec.MonthsPerHalfyear );
		} // GetMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddHalfyearsTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			const YearMonth yearStartMonth = YearMonth.April;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, yearStartMonth );

			DateTime calendarStartDate = new DateTime( currentYear, 4, 1 );
			Halfyear calendarHalfyear = new Halfyear( currentYear, YearHalfyear.First, calendar );

			Assert.AreEqual( calendarHalfyear.AddHalfyears( 0 ), calendarHalfyear );

			Halfyear prevH1 = calendarHalfyear.AddHalfyears( -1 );
			Assert.AreEqual( prevH1.YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( prevH1.BaseYear, currentYear - 1 );
			Assert.AreEqual( prevH1.Start, calendarStartDate.AddMonths( -6 ) );
			Assert.AreEqual( prevH1.End, calendarStartDate );

			Halfyear prevH2 = calendarHalfyear.AddHalfyears( -2 );
			Assert.AreEqual( prevH2.YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( prevH2.BaseYear, currentYear - 1 );
			Assert.AreEqual( prevH2.Start, calendarStartDate.AddMonths( -12 ) );
			Assert.AreEqual( prevH2.End, calendarStartDate.AddMonths( -6 ) );

			Halfyear prevH3 = calendarHalfyear.AddHalfyears( -3 );
			Assert.AreEqual( prevH3.YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( prevH3.BaseYear, currentYear - 2 );
			Assert.AreEqual( prevH3.Start, calendarStartDate.AddMonths( -18 ) );
			Assert.AreEqual( prevH3.End, calendarStartDate.AddMonths( -12 ) );

			Halfyear futureH1 = calendarHalfyear.AddHalfyears( 1 );
			Assert.AreEqual( futureH1.YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( futureH1.BaseYear, currentYear );
			Assert.AreEqual( futureH1.Start, calendarStartDate.AddMonths( 6 ) );
			Assert.AreEqual( futureH1.End, calendarStartDate.AddMonths( 12 ) );

			Halfyear futureH2 = calendarHalfyear.AddHalfyears( 2 );
			Assert.AreEqual( futureH2.YearHalfyear, YearHalfyear.First );
			Assert.AreEqual( futureH2.BaseYear, currentYear + 1 );
			Assert.AreEqual( futureH2.Start, calendarStartDate.AddMonths( 12 ) );
			Assert.AreEqual( futureH2.End, calendarStartDate.AddMonths( 18 ) );

			Halfyear futureH3 = calendarHalfyear.AddHalfyears( 3 );
			Assert.AreEqual( futureH3.YearHalfyear, YearHalfyear.Second );
			Assert.AreEqual( futureH3.BaseYear, currentYear + 1 );
			Assert.AreEqual( futureH3.Start, calendarStartDate.AddMonths( 18 ) );
			Assert.AreEqual( futureH3.End, calendarStartDate.AddMonths( 24 ) );
		} // AddHalfyearsTest

		// ----------------------------------------------------------------------
		private ITimeCalendar GetFiscalYearCalendar( FiscalYearAlignment yearAlignment )
		{
			return new TimeCalendar(
				new TimeCalendarConfig
				{
					YearType = YearType.FiscalYear,
					YearBaseMonth = YearMonth.September,
					FiscalFirstDayOfYear = DayOfWeek.Sunday,
					FiscalYearAlignment = yearAlignment,
					FiscalQuarterGrouping = FiscalQuarterGrouping.FourFourFiveWeeks
				} );
		} // GetFiscalYearCalendar

		// ----------------------------------------------------------------------
		// http://en.wikipedia.org/wiki/4-4-5_Calendar
		[Test]
		public void GetFiscalQuartersTest()
		{
			Halfyear halfyear = new Halfyear( 2006, YearHalfyear.First, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection quarters = halfyear.GetQuarters();

			Assert.AreNotEqual( quarters, null );
			Assert.AreEqual( quarters.Count, TimeSpec.QuartersPerHalfyear );

			Assert.AreEqual( quarters[ 0 ].Start.Date, halfyear.Start );
			Assert.AreEqual( quarters[ TimeSpec.QuartersPerHalfyear - 1 ].End, halfyear.End );
		} // GetFiscalQuartersTest

		// ----------------------------------------------------------------------
		// http://en.wikipedia.org/wiki/4-4-5_Calendar
		[Test]
		public void FiscalYearGetMonthsTest()
		{
			Halfyear halfyear = new Halfyear( 2006,YearHalfyear.First, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = halfyear.GetMonths();
			Assert.AreNotEqual( months, null );
			Assert.AreEqual( months.Count, TimeSpec.MonthsPerHalfyear );

			Assert.AreEqual( months[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Assert.AreEqual( months[ i ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days,
					( i + 1 ) % 3 == 0 ? TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth );
			}
			Assert.AreEqual( months[ TimeSpec.MonthsPerHalfyear - 1 ].End, halfyear.End );
		} // FiscalYearGetMonthsTest

	} // class HalfyearTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
