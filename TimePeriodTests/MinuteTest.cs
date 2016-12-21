// -- FILE ------------------------------------------------------------------
// name       : MinuteTest.cs
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
	public sealed class MinuteTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstMinute = new DateTime( now.Year, now.Month, now.Day, now.Hour, now.Minute, 0 );
			DateTime secondMinute = firstMinute.AddMinutes( 1 );
			Minute minute = new Minute( now, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( minute.Start.Year, firstMinute.Year );
			Assert.AreEqual( minute.Start.Month, firstMinute.Month );
			Assert.AreEqual( minute.Start.Day, firstMinute.Day );
			Assert.AreEqual( minute.Start.Hour, firstMinute.Hour );
			Assert.AreEqual( minute.Start.Minute, firstMinute.Minute );
			Assert.AreEqual( minute.Start.Second, 0 );
			Assert.AreEqual( minute.Start.Millisecond, 0 );

			Assert.AreEqual( minute.End.Year, secondMinute.Year );
			Assert.AreEqual( minute.End.Month, secondMinute.Month );
			Assert.AreEqual( minute.End.Day, secondMinute.Day );
			Assert.AreEqual( minute.End.Hour, secondMinute.Hour );
			Assert.AreEqual( minute.End.Minute, secondMinute.Minute );
			Assert.AreEqual( minute.End.Second, 0 );
			Assert.AreEqual( minute.End.Millisecond, 0 );
		} // InitValuesTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultCalendarTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime currentHour = new DateTime( now.Year, now.Month, now.Day, now.Hour, 0, 0 );
			for ( int hourMinute = 0; hourMinute < TimeSpec.MinutesPerHour; hourMinute++ )
			{
				Minute minute = new Minute( currentHour.AddMinutes( hourMinute ) );
				Assert.AreEqual( minute.Year, currentHour.Year );
				Assert.AreEqual( minute.Month, currentHour.Month );
				Assert.AreEqual( minute.Month, currentHour.Month );
				Assert.AreEqual( minute.Day, currentHour.Day );
				Assert.AreEqual( minute.Hour, currentHour.Hour );
				Assert.AreEqual( minute.MinuteValue, hourMinute );
				Assert.AreEqual( minute.Start, currentHour.AddMinutes( hourMinute ).Add( minute.Calendar.StartOffset ) );
				Assert.AreEqual( minute.End, currentHour.AddMinutes( hourMinute + 1 ).Add( minute.Calendar.EndOffset ) );
			}
		} // DefaultCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			Assert.AreEqual( new Minute( now ).Year, now.Year );
			Assert.AreEqual( new Minute( now ).Month, now.Month );
			Assert.AreEqual( new Minute( now ).Day, now.Day );
			Assert.AreEqual( new Minute( now ).Hour, now.Hour );
			Assert.AreEqual( new Minute( now ).MinuteValue, now.Minute );

			Assert.AreEqual( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Year, now.Year );
			Assert.AreEqual( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Month, now.Month );
			Assert.AreEqual( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Day, now.Day );
			Assert.AreEqual( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Hour, now.Hour );
			Assert.AreEqual( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).MinuteValue, now.Minute );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetPreviousMinuteTest()
		{
			Minute minute = new Minute();
			Assert.AreEqual( minute.GetPreviousMinute(), minute.AddMinutes( -1 ) );
		} // GetPreviousMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetNextMinuteTest()
		{
			Minute minute = new Minute();
			Assert.AreEqual( minute.GetNextMinute(), minute.AddMinutes( 1 ) );
		} // GetNextMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddMinutesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime nowMinute = new DateTime( now.Year, now.Month, now.Day, now.Hour, now.Minute, 0 );
			Minute minute = new Minute( now, TimeCalendar.NewEmptyOffset() );

			Assert.AreEqual( minute.AddMinutes( 0 ), minute );

			DateTime previousMinute = nowMinute.AddMinutes( -1 );
			Assert.AreEqual( minute.AddMinutes( -1 ).Year, previousMinute.Year );
			Assert.AreEqual( minute.AddMinutes( -1 ).Month, previousMinute.Month );
			Assert.AreEqual( minute.AddMinutes( -1 ).Day, previousMinute.Day );
			Assert.AreEqual( minute.AddMinutes( -1 ).Hour, previousMinute.Hour );
			Assert.AreEqual( minute.AddMinutes( -1 ).MinuteValue, previousMinute.Minute );

			DateTime nextMinute = nowMinute.AddMinutes( 1 );
			Assert.AreEqual( minute.AddMinutes( 1 ).Year, nextMinute.Year );
			Assert.AreEqual( minute.AddMinutes( 1 ).Month, nextMinute.Month );
			Assert.AreEqual( minute.AddMinutes( 1 ).Day, nextMinute.Day );
			Assert.AreEqual( minute.AddMinutes( 1 ).Hour, nextMinute.Hour );
			Assert.AreEqual( minute.AddMinutes( 1 ).MinuteValue, nextMinute.Minute );
		} // AddMinutes

	} // class MinuteTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
