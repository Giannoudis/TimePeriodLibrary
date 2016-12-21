// -- FILE ------------------------------------------------------------------
// name       : HourTest.cs
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
	public sealed class HourTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstHour = new DateTime( now.Year, now.Month, now.Day, now.Hour, 0, 0 );
			DateTime secondHour = firstHour.AddHours( 1 );
			Hour hour = new Hour( now, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( hour.Start.Year, firstHour.Year );
			Assert.AreEqual( hour.Start.Month, firstHour.Month );
			Assert.AreEqual( hour.Start.Day, firstHour.Day );
			Assert.AreEqual( hour.Start.Hour, firstHour.Hour );
			Assert.AreEqual( hour.Start.Minute, 0 );
			Assert.AreEqual( hour.Start.Second, 0 );
			Assert.AreEqual( hour.Start.Millisecond, 0 );

			Assert.AreEqual( hour.End.Year, secondHour.Year );
			Assert.AreEqual( hour.End.Month, secondHour.Month );
			Assert.AreEqual( hour.End.Day, secondHour.Day );
			Assert.AreEqual( hour.End.Hour, secondHour.Hour );
			Assert.AreEqual( hour.End.Minute, 0 );
			Assert.AreEqual( hour.End.Second, 0 );
			Assert.AreEqual( hour.End.Millisecond, 0 );
		} // InitValuesTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime todayStart = new DateTime( now.Year, now.Month, now.Day );
			for ( int dayHour = 0; dayHour < TimeSpec.HoursPerDay; dayHour++ )
			{
				Hour hour = new Hour( todayStart.AddHours( dayHour ) );
				Assert.AreEqual( hour.Year, todayStart.Year );
				Assert.AreEqual( hour.Month, todayStart.Month );
				Assert.AreEqual( hour.Month, todayStart.Month );
				Assert.AreEqual( hour.Day, todayStart.Day );
				Assert.AreEqual( hour.HourValue, dayHour );
				Assert.AreEqual( hour.Start, todayStart.AddHours( dayHour ).Add( hour.Calendar.StartOffset ) );
				Assert.AreEqual( hour.End, todayStart.AddHours( dayHour + 1 ).Add( hour.Calendar.EndOffset ) );
			}
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			Assert.AreEqual( new Hour( now ).Year, now.Year );
			Assert.AreEqual( new Hour( now ).Month, now.Month );
			Assert.AreEqual( new Hour( now ).Day, now.Day );
			Assert.AreEqual( new Hour( now ).HourValue, now.Hour );

			Assert.AreEqual( new Hour( now.Year, now.Month, now.Day, now.Hour ).Year, now.Year );
			Assert.AreEqual( new Hour( now.Year, now.Month, now.Day, now.Hour ).Month, now.Month );
			Assert.AreEqual( new Hour( now.Year, now.Month, now.Day, now.Hour ).Day, now.Day );
			Assert.AreEqual( new Hour( now.Year, now.Month, now.Day, now.Hour ).HourValue, now.Hour );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetPreviousHourTest()
		{
			Hour hour = new Hour();
			Assert.AreEqual( hour.GetPreviousHour(), hour.AddHours( -1 ) );
		} // GetPreviousHourTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetNextHourTest()
		{
			Hour hour = new Hour();
			Assert.AreEqual( hour.GetNextHour(), hour.AddHours( 1 ) );
		} // GetNextHourTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddHoursTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime nowHour = new DateTime( now.Year, now.Month, now.Day, now.Hour, 0, 0 );
			Hour hour = new Hour( now, TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero ) );

			Assert.AreEqual( hour.AddHours( 0 ), hour );

			DateTime previousHour = nowHour.AddHours( -1 );
			Assert.AreEqual( hour.AddHours( -1 ).Year, previousHour.Year );
			Assert.AreEqual( hour.AddHours( -1 ).Month, previousHour.Month );
			Assert.AreEqual( hour.AddHours( -1 ).Day, previousHour.Day );
			Assert.AreEqual( hour.AddHours( -1 ).HourValue, previousHour.Hour );

			DateTime nextHour = nowHour.AddHours( 1 );
			Assert.AreEqual( hour.AddHours( 1 ).Year, nextHour.Year );
			Assert.AreEqual( hour.AddHours( 1 ).Month, nextHour.Month );
			Assert.AreEqual( hour.AddHours( 1 ).Day, nextHour.Day );
			Assert.AreEqual( hour.AddHours( 1 ).HourValue, nextHour.Hour );
		} // AddHoursTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMinutesTest()
		{
			Hour hour = new Hour();

			ITimePeriodCollection minutes = hour.GetMinutes();
			Assert.AreNotEqual( minutes, null );

			int index = 0;
			foreach ( Minute minute in minutes )
			{
				Assert.AreEqual( minute.Start, hour.Start.AddMinutes( index ) );
				Assert.AreEqual( minute.End, minute.Calendar.MapEnd( minute.Start.AddMinutes( 1 ) ) );
				index++;
			}
			Assert.AreEqual( index, TimeSpec.MinutesPerHour );
		} // GetMinutesTest

	} // class HourTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
