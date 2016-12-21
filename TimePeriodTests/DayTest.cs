// -- FILE ------------------------------------------------------------------
// name       : DayTest.cs
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
	public sealed class DayTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstDay = new DateTime( now.Year, now.Month, now.Day );
			DateTime secondDay = firstDay.AddDays( 1 );
			Day day = new Day( now, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( day.Start.Year, firstDay.Year );
			Assert.AreEqual( day.Start.Month, firstDay.Month );
			Assert.AreEqual( day.Start.Day, firstDay.Day );
			Assert.AreEqual( day.Start.Hour, 0 );
			Assert.AreEqual( day.Start.Minute, 0 );
			Assert.AreEqual( day.Start.Second, 0 );
			Assert.AreEqual( day.Start.Millisecond, 0 );

			Assert.AreEqual( day.End.Year, secondDay.Year );
			Assert.AreEqual( day.End.Month, secondDay.Month );
			Assert.AreEqual( day.End.Day, secondDay.Day );
			Assert.AreEqual( day.End.Hour, 0 );
			Assert.AreEqual( day.End.Minute, 0 );
			Assert.AreEqual( day.End.Second, 0 );
			Assert.AreEqual( day.End.Millisecond, 0 );
		} // InitValuesTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			DateTime yearStart = new DateTime( ClockProxy.Clock.Now.Year, 1, 1 );
			foreach ( YearMonth yearMonth in Enum.GetValues( typeof( YearMonth ) ) )
			{
				DateTime monthStart = new DateTime( yearStart.Year, (int)yearMonth, 1 );
				DateTime monthEnd = monthStart.AddMonths( 1 ).AddDays( -1 );

				for ( int monthDay = monthStart.Day; monthDay < monthEnd.Day; monthDay++ )
				{
					Day day = new Day( monthStart.AddDays( monthDay - 1 ) );
					Assert.AreEqual( day.Year, yearStart.Year );
					Assert.AreEqual( day.Month, monthStart.Month );
					Assert.AreEqual( day.Month, monthEnd.Month );
					Assert.AreEqual( day.DayValue, monthDay );
					Assert.AreEqual( day.Start, monthStart.AddDays( monthDay - 1 ).Add( day.Calendar.StartOffset ) );
					Assert.AreEqual( day.End, monthStart.AddDays( monthDay ).Add( day.Calendar.EndOffset ) );
				}
			}
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			Assert.AreEqual( new Day( now ).Year, now.Year );
			Assert.AreEqual( new Day( now ).Month, now.Month );
			Assert.AreEqual( new Day( now ).DayValue, now.Day );

			Assert.AreEqual( new Day( now.Year, now.Month, now.Day ).Year, now.Year );
			Assert.AreEqual( new Day( now.Year, now.Month, now.Day ).Month, now.Month );
			Assert.AreEqual( new Day( now.Year, now.Month, now.Day ).DayValue, now.Day );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void DayOfWeekTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeCalendar calendar = new TimeCalendar();

			Assert.AreEqual( new Day( now, calendar ).DayOfWeek, calendar.Culture.Calendar.GetDayOfWeek( now ) );
		} // DayOfWeekTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetPreviousDayTest()
		{
			Day day = new Day();
			Assert.AreEqual( day.GetPreviousDay(), day.AddDays( -1 ) );
		} // GetPreviousDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetNextDayTest()
		{
			Day day = new Day();
			Assert.AreEqual( day.GetNextDay(), day.AddDays( 1 ) );
		} // GetNextDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddDaysTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime nowDay = new DateTime( now.Year, now.Month, now.Day );
			Day day = new Day( now, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( day.AddDays( 0 ), day );

			DateTime previousDay = nowDay.AddDays( -1 );
			Assert.AreEqual( day.AddDays( -1 ).Year, previousDay.Year );
			Assert.AreEqual( day.AddDays( -1 ).Month, previousDay.Month );
			Assert.AreEqual( day.AddDays( -1 ).DayValue, previousDay.Day );

			DateTime nextDay = nowDay.AddDays( 1 );
			Assert.AreEqual( day.AddDays( 1 ).Year, nextDay.Year );
			Assert.AreEqual( day.AddDays( 1 ).Month, nextDay.Month );
			Assert.AreEqual( day.AddDays( 1 ).DayValue, nextDay.Day );
		} // AddDaysTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetHoursTest()
		{
			Day day = new Day();

			ITimePeriodCollection hours = day.GetHours();
			Assert.AreNotEqual( hours, null );

			int index = 0;
			foreach ( Hour hour in hours )
			{
				Assert.AreEqual( hour.Start, day.Start.AddHours( index ) );
				Assert.AreEqual( hour.End, hour.Calendar.MapEnd( hour.Start.AddHours( 1 ) ) );
				index++;
			}
			Assert.AreEqual( index, TimeSpec.HoursPerDay );
		} // GetHoursTest

	} // class DayTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
