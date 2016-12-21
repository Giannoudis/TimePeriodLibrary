// -- FILE ------------------------------------------------------------------
// name       : QuarterTest.cs
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
	public sealed class QuarterTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstQuarter = new DateTime( now.Year, 1, 1 );
			DateTime secondQuarter = firstQuarter.AddMonths( TimeSpec.MonthsPerQuarter );
			Quarter quarter = new Quarter( now.Year, YearQuarter.First, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( quarter.Start.Year, firstQuarter.Year );
			Assert.AreEqual( quarter.Start.Month, firstQuarter.Month );
			Assert.AreEqual( quarter.Start.Day, firstQuarter.Day );
			Assert.AreEqual( quarter.Start.Hour, 0 );
			Assert.AreEqual( quarter.Start.Minute, 0 );
			Assert.AreEqual( quarter.Start.Second, 0 );
			Assert.AreEqual( quarter.Start.Millisecond, 0 );

			Assert.AreEqual( quarter.End.Year, secondQuarter.Year );
			Assert.AreEqual( quarter.End.Month, secondQuarter.Month );
			Assert.AreEqual( quarter.End.Day, secondQuarter.Day );
			Assert.AreEqual( quarter.End.Hour, 0 );
			Assert.AreEqual( quarter.End.Minute, 0 );
			Assert.AreEqual( quarter.End.Second, 0 );
			Assert.AreEqual( quarter.End.Millisecond, 0 );
		} // InitValuesTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			DateTime yearStart = new DateTime( ClockProxy.Clock.Now.Year, 1, 1 );
			foreach ( YearQuarter yearQuarter in Enum.GetValues( typeof( YearQuarter ) ) )
			{
				int offset = (int)yearQuarter - 1;
				Quarter quarter = new Quarter( yearStart.AddMonths( TimeSpec.MonthsPerQuarter * offset ) );
				Assert.AreEqual( quarter.YearBaseMonth, YearMonth.January );
				Assert.AreEqual( quarter.BaseYear, yearStart.Year );
				Assert.AreEqual( quarter.Start, yearStart.AddMonths( TimeSpec.MonthsPerQuarter * offset ).Add( quarter.Calendar.StartOffset ) );
				Assert.AreEqual( quarter.End, yearStart.AddMonths( TimeSpec.MonthsPerQuarter * ( offset + 1 ) ).Add( quarter.Calendar.EndOffset ) );
			}
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeCalendar calendar = TimeCalendar.New( YearMonth.April );

			Assert.AreEqual( new Quarter( new DateTime( now.Year, 1, 1 ) ).YearQuarter, YearQuarter.First );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 3, 30 ) ).YearQuarter, YearQuarter.First );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 4, 1 ) ).YearQuarter, YearQuarter.Second );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 6, 30 ) ).YearQuarter, YearQuarter.Second );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 7, 1 ) ).YearQuarter, YearQuarter.Third );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 9, 30 ) ).YearQuarter, YearQuarter.Third );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 10, 1 ) ).YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 12, 31 ) ).YearQuarter, YearQuarter.Fourth );

			Assert.AreEqual( new Quarter( new DateTime( now.Year, 4, 1 ), calendar ).YearQuarter, YearQuarter.First );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 6, 30 ), calendar ).YearQuarter, YearQuarter.First );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 7, 1 ), calendar ).YearQuarter, YearQuarter.Second );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 9, 30 ), calendar ).YearQuarter, YearQuarter.Second );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 10, 1 ), calendar ).YearQuarter, YearQuarter.Third );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 12, 31 ), calendar ).YearQuarter, YearQuarter.Third );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 1, 1 ), calendar ).YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( new Quarter( new DateTime( now.Year, 3, 30 ), calendar ).YearQuarter, YearQuarter.Fourth );
		} // MomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.April ) ).BaseYear, currentYear );
			Assert.AreEqual( new Quarter( 2006, YearQuarter.Fourth ).BaseYear, 2006 );
		} // YearTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearQuarterTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.April ) ).YearQuarter, YearQuarter.Third );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third ).YearQuarter, YearQuarter.Third );
		} // YearQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void CurrentQuarterTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Quarter quarter = new Quarter( currentYear, YearQuarter.First );
			Assert.AreEqual( quarter.BaseYear, currentYear );
		} // CurrentQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartMonthTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;

			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.January ) ).StartMonth, YearMonth.January );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.February ) ).StartMonth, YearMonth.February );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.March ) ).StartMonth, YearMonth.March );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.April ) ).StartMonth, YearMonth.April );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.May ) ).StartMonth, YearMonth.May );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.June ) ).StartMonth, YearMonth.June );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.July ) ).StartMonth, YearMonth.July );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.August ) ).StartMonth, YearMonth.August );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.September ) ).StartMonth, YearMonth.September );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.October ) ).StartMonth, YearMonth.October );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.November ) ).StartMonth, YearMonth.November );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.First, TimeCalendar.New( YearMonth.December ) ).StartMonth, YearMonth.December );

			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.January ) ).StartMonth, YearMonth.April );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.February ) ).StartMonth, YearMonth.May );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.March ) ).StartMonth, YearMonth.June );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.April ) ).StartMonth, YearMonth.July );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.May ) ).StartMonth, YearMonth.August );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.June ) ).StartMonth, YearMonth.September );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.July ) ).StartMonth, YearMonth.October );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.August ) ).StartMonth, YearMonth.November );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.September ) ).StartMonth, YearMonth.December );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.October ) ).StartMonth, YearMonth.January );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.November ) ).StartMonth, YearMonth.February );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Second, TimeCalendar.New( YearMonth.December ) ).StartMonth, YearMonth.March );

			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.January ) ).StartMonth, YearMonth.July );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.February ) ).StartMonth, YearMonth.August );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.March ) ).StartMonth, YearMonth.September );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.April ) ).StartMonth, YearMonth.October );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.May ) ).StartMonth, YearMonth.November );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.June ) ).StartMonth, YearMonth.December );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.July ) ).StartMonth, YearMonth.January );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.August ) ).StartMonth, YearMonth.February );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.September ) ).StartMonth, YearMonth.March );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.October ) ).StartMonth, YearMonth.April );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.November ) ).StartMonth, YearMonth.May );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Third, TimeCalendar.New( YearMonth.December ) ).StartMonth, YearMonth.June );

			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.January ) ).StartMonth, YearMonth.October );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.February ) ).StartMonth, YearMonth.November );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.March ) ).StartMonth, YearMonth.December );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.April ) ).StartMonth, YearMonth.January );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.May ) ).StartMonth, YearMonth.February );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.June ) ).StartMonth, YearMonth.March );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.July ) ).StartMonth, YearMonth.April );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.August ) ).StartMonth, YearMonth.May );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.September ) ).StartMonth, YearMonth.June );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.October ) ).StartMonth, YearMonth.July );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.November ) ).StartMonth, YearMonth.August );
			Assert.AreEqual( new Quarter( currentYear, YearQuarter.Fourth, TimeCalendar.New( YearMonth.December ) ).StartMonth, YearMonth.September );
		} // StartMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarYearQuarterTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero );

			Quarter current = new Quarter( currentYear, YearQuarter.First );
			Assert.AreEqual( current.GetNextQuarter().GetNextQuarter().GetNextQuarter().GetNextQuarter().YearQuarter, current.YearQuarter );
			Assert.AreEqual( current.GetPreviousQuarter().GetPreviousQuarter().GetPreviousQuarter().GetPreviousQuarter().YearQuarter, current.YearQuarter );

			Quarter q1 = new Quarter( currentYear, YearQuarter.First, calendar );
			Assert.IsTrue( q1.IsReadOnly );
			Assert.AreEqual( q1.YearQuarter, YearQuarter.First );
			Assert.AreEqual( q1.Start, new DateTime( currentYear, TimeSpec.FirstQuarterMonthIndex, 1 ) );
			Assert.AreEqual( q1.End, new DateTime( currentYear, TimeSpec.SecondQuarterMonthIndex, 1 ) );

			Quarter q2 = new Quarter( currentYear, YearQuarter.Second, calendar );
			Assert.IsTrue( q2.IsReadOnly );
			Assert.AreEqual( q2.YearQuarter, YearQuarter.Second );
			Assert.AreEqual( q2.Start, new DateTime( currentYear, TimeSpec.SecondQuarterMonthIndex, 1 ) );
			Assert.AreEqual( q2.End, new DateTime( currentYear, TimeSpec.ThirdQuarterMonthIndex, 1 ) );

			Quarter q3 = new Quarter( currentYear, YearQuarter.Third, calendar );
			Assert.IsTrue( q3.IsReadOnly );
			Assert.AreEqual( q3.YearQuarter, YearQuarter.Third );
			Assert.AreEqual( q3.Start, new DateTime( currentYear, TimeSpec.ThirdQuarterMonthIndex, 1 ) );
			Assert.AreEqual( q3.End, new DateTime( currentYear, TimeSpec.FourthQuarterMonthIndex, 1 ) );

			Quarter q4 = new Quarter( currentYear, YearQuarter.Fourth, calendar );
			Assert.IsTrue( q4.IsReadOnly );
			Assert.AreEqual( q4.YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( q4.Start, new DateTime( currentYear, TimeSpec.FourthQuarterMonthIndex, 1 ) );
			Assert.AreEqual( q4.End, new DateTime( currentYear + 1, TimeSpec.FirstQuarterMonthIndex, 1 ) );
		} // CalendarYearQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void QuarterMomentTest()
		{
			Quarter quarter = new Quarter( 2008, YearQuarter.First, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero ) );
			Assert.IsTrue( quarter.IsReadOnly );
			Assert.AreEqual( quarter.BaseYear, 2008 );
			Assert.AreEqual( quarter.YearQuarter, YearQuarter.First );
			Assert.AreEqual( quarter.Start, new DateTime( 2008, TimeSpec.FirstQuarterMonthIndex, 1 ) );
			Assert.AreEqual( quarter.End, new DateTime( 2008, TimeSpec.SecondQuarterMonthIndex, 1 ) );

			Quarter previous = quarter.GetPreviousQuarter();
			Assert.AreEqual( previous.BaseYear, 2007 );
			Assert.AreEqual( previous.YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( previous.Start, new DateTime( 2007, TimeSpec.FourthQuarterMonthIndex, 1 ) );
			Assert.AreEqual( previous.End, new DateTime( 2008, TimeSpec.FirstQuarterMonthIndex, 1 ) );

			Quarter next = quarter.GetNextQuarter();
			Assert.AreEqual( next.BaseYear, 2008 );
			Assert.AreEqual( next.YearQuarter, YearQuarter.Second );
			Assert.AreEqual( next.Start, new DateTime( 2008, TimeSpec.SecondQuarterMonthIndex, 1 ) );
			Assert.AreEqual( next.End, new DateTime( 2008, TimeSpec.ThirdQuarterMonthIndex, 1 ) );
		} // QuarterMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsCalendarQuarterTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;

			foreach ( YearQuarter yearQuarter in Enum.GetValues( typeof( YearQuarter ) ) )
			{
				Assert.IsTrue( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.January ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.February ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.March ) ).IsCalendarQuarter );
				Assert.IsTrue( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.April ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.May ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.June ) ).IsCalendarQuarter );
				Assert.IsTrue( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.July ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.August ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.September ) ).IsCalendarQuarter );
				Assert.IsTrue( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.October ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.November ) ).IsCalendarQuarter );
				Assert.IsFalse( new Quarter( currentYear, yearQuarter, TimeCalendar.New( YearMonth.December ) ).IsCalendarQuarter );
			}
		} // IsCalendarQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void MultipleCalendarYearsTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );

			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Second, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );

			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Third, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );

			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.IsTrue( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.IsFalse( new Quarter( now.Year, YearQuarter.Fourth, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );
		} // MultipleCalendarYearsTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultQuarterTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			const YearMonth yearStartMonth = YearMonth.April;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, yearStartMonth );

			Quarter q1 = new Quarter( currentYear, YearQuarter.First, calendar );
			Assert.IsTrue( q1.IsReadOnly );
			Assert.AreEqual( q1.YearBaseMonth, yearStartMonth );
			Assert.AreEqual( q1.YearQuarter, YearQuarter.First );
			Assert.AreEqual( q1.BaseYear, currentYear );
			Assert.AreEqual( q1.Start, new DateTime( currentYear, 4, 1 ) );
			Assert.AreEqual( q1.End, new DateTime( currentYear, 7, 1 ) );

			Quarter q2 = new Quarter( currentYear, YearQuarter.Second, calendar );
			Assert.IsTrue( q2.IsReadOnly );
			Assert.AreEqual( q2.YearBaseMonth, yearStartMonth );
			Assert.AreEqual( q2.YearQuarter, YearQuarter.Second );
			Assert.AreEqual( q2.BaseYear, currentYear );
			Assert.AreEqual( q2.Start, new DateTime( currentYear, 7, 1 ) );
			Assert.AreEqual( q2.End, new DateTime( currentYear, 10, 1 ) );

			Quarter q3 = new Quarter( currentYear, YearQuarter.Third, calendar );
			Assert.IsTrue( q3.IsReadOnly );
			Assert.AreEqual( q3.YearBaseMonth, yearStartMonth );
			Assert.AreEqual( q3.YearQuarter, YearQuarter.Third );
			Assert.AreEqual( q3.BaseYear, currentYear );
			Assert.AreEqual( q3.Start, new DateTime( currentYear, 10, 1 ) );
			Assert.AreEqual( q3.End, new DateTime( currentYear + 1, 1, 1 ) );

			Quarter q4 = new Quarter( currentYear, YearQuarter.Fourth, calendar );
			Assert.IsTrue( q4.IsReadOnly );
			Assert.AreEqual( q4.YearBaseMonth, yearStartMonth );
			Assert.AreEqual( q4.YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( q4.BaseYear, currentYear );
			Assert.AreEqual( q4.Start, new DateTime( currentYear + 1, 1, 1 ) );
			Assert.AreEqual( q4.End, new DateTime( currentYear + 1, 4, 1 ) );
		} // DefaultQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthsTest()
		{
			Quarter quarter = new Quarter( ClockProxy.Clock.Now.Year, YearQuarter.First, TimeCalendar.New( YearMonth.October ) );

			ITimePeriodCollection months = quarter.GetMonths();
			Assert.AreNotEqual( months, null );

			int index = 0;
			foreach ( Month month in months )
			{
				Assert.AreEqual( month.Start, quarter.Start.AddMonths( index ) );
				Assert.AreEqual( month.End, month.Calendar.MapEnd( month.Start.AddMonths( 1 ) ) );
				index++;
			}
			Assert.AreEqual( index, TimeSpec.MonthsPerQuarter );
		} // GetMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddQuartersTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			const YearMonth yearStartMonth = YearMonth.April;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, yearStartMonth );

			DateTime calendarStartDate = new DateTime( currentYear, 4, 1 );
			Quarter calendarQuarter = new Quarter( currentYear, YearQuarter.First, calendar );

			Assert.AreEqual( calendarQuarter.AddQuarters( 0 ), calendarQuarter );

			Quarter prevQ1 = calendarQuarter.AddQuarters( -1 );
			Assert.AreEqual( prevQ1.YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( prevQ1.BaseYear, currentYear - 1 );
			Assert.AreEqual( prevQ1.Start, calendarStartDate.AddMonths( -3 ) );
			Assert.AreEqual( prevQ1.End, calendarStartDate );

			Quarter prevQ2 = calendarQuarter.AddQuarters( -2 );
			Assert.AreEqual( prevQ2.YearQuarter, YearQuarter.Third );
			Assert.AreEqual( prevQ2.BaseYear, currentYear - 1 );
			Assert.AreEqual( prevQ2.Start, calendarStartDate.AddMonths( -6 ) );
			Assert.AreEqual( prevQ2.End, calendarStartDate.AddMonths( -3 ) );

			Quarter prevQ3 = calendarQuarter.AddQuarters( -3 );
			Assert.AreEqual( prevQ3.YearQuarter, YearQuarter.Second );
			Assert.AreEqual( prevQ3.BaseYear, currentYear - 1 );
			Assert.AreEqual( prevQ3.Start, calendarStartDate.AddMonths( -9 ) );
			Assert.AreEqual( prevQ3.End, calendarStartDate.AddMonths( -6 ) );

			Quarter prevQ4 = calendarQuarter.AddQuarters( -4 );
			Assert.AreEqual( prevQ4.YearQuarter, YearQuarter.First );
			Assert.AreEqual( prevQ4.BaseYear, currentYear - 1 );
			Assert.AreEqual( prevQ4.Start, calendarStartDate.AddMonths( -12 ) );
			Assert.AreEqual( prevQ4.End, calendarStartDate.AddMonths( -9 ) );

			Quarter prevQ5 = calendarQuarter.AddQuarters( -5 );
			Assert.AreEqual( prevQ5.YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( prevQ5.BaseYear, currentYear - 2 );
			Assert.AreEqual( prevQ5.Start, calendarStartDate.AddMonths( -15 ) );
			Assert.AreEqual( prevQ5.End, calendarStartDate.AddMonths( -12 ) );

			Quarter futureQ1 = calendarQuarter.AddQuarters( 1 );
			Assert.AreEqual( futureQ1.YearQuarter, YearQuarter.Second );
			Assert.AreEqual( futureQ1.BaseYear, currentYear );
			Assert.AreEqual( futureQ1.Start, calendarStartDate.AddMonths( 3 ) );
			Assert.AreEqual( futureQ1.End, calendarStartDate.AddMonths( 6 ) );

			Quarter futureQ2 = calendarQuarter.AddQuarters( 2 );
			Assert.AreEqual( futureQ2.YearQuarter, YearQuarter.Third );
			Assert.AreEqual( futureQ2.BaseYear, currentYear );
			Assert.AreEqual( futureQ2.Start, calendarStartDate.AddMonths( 6 ) );
			Assert.AreEqual( futureQ2.End, calendarStartDate.AddMonths( 9 ) );

			Quarter futureQ3 = calendarQuarter.AddQuarters( 3 );
			Assert.AreEqual( futureQ3.YearQuarter, YearQuarter.Fourth );
			Assert.AreEqual( futureQ3.BaseYear, currentYear );
			Assert.AreEqual( futureQ3.Start, calendarStartDate.AddMonths( 9 ) );
			Assert.AreEqual( futureQ3.End, calendarStartDate.AddMonths( 12 ) );

			Quarter futureQ4 = calendarQuarter.AddQuarters( 4 );
			Assert.AreEqual( futureQ4.YearQuarter, YearQuarter.First );
			Assert.AreEqual( futureQ4.BaseYear, currentYear + 1 );
			Assert.AreEqual( futureQ4.Start, calendarStartDate.AddMonths( 12 ) );
			Assert.AreEqual( futureQ4.End, calendarStartDate.AddMonths( 15 ) );

			Quarter futureQ5 = calendarQuarter.AddQuarters( 5 );
			Assert.AreEqual( futureQ5.YearQuarter, YearQuarter.Second );
			Assert.AreEqual( futureQ5.BaseYear, currentYear + 1 );
			Assert.AreEqual( futureQ5.Start, calendarStartDate.AddMonths( 15 ) );
			Assert.AreEqual( futureQ5.End, calendarStartDate.AddMonths( 18 ) );
		} // AddQuartersTest

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
		public void FiscalYearGetMonthsTest()
		{
			Quarter quarter = new Quarter( 2006, YearQuarter.First, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = quarter.GetMonths();
			Assert.AreNotEqual( months, null );
			Assert.AreEqual( months.Count, TimeSpec.MonthsPerQuarter );

			Assert.AreEqual( months[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Assert.AreEqual( months[ i ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days,
					( i + 1 ) % 3 == 0 ? TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth );
			}
			Assert.AreEqual( months[ TimeSpec.MonthsPerQuarter - 1 ].End, quarter.End );
		} // FiscalYearGetMonthsTest

	} // class QuarterTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
