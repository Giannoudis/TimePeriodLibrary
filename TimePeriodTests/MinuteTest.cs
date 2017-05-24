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
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class MinuteTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "Minute")]
        [Fact]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstMinute = new DateTime( now.Year, now.Month, now.Day, now.Hour, now.Minute, 0 );
			DateTime secondMinute = firstMinute.AddMinutes( 1 );
			Minute minute = new Minute( now, TimeCalendar.NewEmptyOffset() );

			Assert.Equal( minute.Start.Year, firstMinute.Year );
			Assert.Equal( minute.Start.Month, firstMinute.Month );
			Assert.Equal( minute.Start.Day, firstMinute.Day );
			Assert.Equal( minute.Start.Hour, firstMinute.Hour );
			Assert.Equal( minute.Start.Minute, firstMinute.Minute );
			Assert.Equal(0, minute.Start.Second);
			Assert.Equal(0, minute.Start.Millisecond);

			Assert.Equal( minute.End.Year, secondMinute.Year );
			Assert.Equal( minute.End.Month, secondMinute.Month );
			Assert.Equal( minute.End.Day, secondMinute.Day );
			Assert.Equal( minute.End.Hour, secondMinute.Hour );
			Assert.Equal( minute.End.Minute, secondMinute.Minute );
			Assert.Equal(0, minute.End.Second);
			Assert.Equal(0, minute.End.Millisecond);
		} // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Minute")]
        [Fact]
		public void DefaultCalendarTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime currentHour = new DateTime( now.Year, now.Month, now.Day, now.Hour, 0, 0 );
			for ( int hourMinute = 0; hourMinute < TimeSpec.MinutesPerHour; hourMinute++ )
			{
				Minute minute = new Minute( currentHour.AddMinutes( hourMinute ) );
				Assert.Equal( minute.Year, currentHour.Year );
				Assert.Equal( minute.Month, currentHour.Month );
				Assert.Equal( minute.Month, currentHour.Month );
				Assert.Equal( minute.Day, currentHour.Day );
				Assert.Equal( minute.Hour, currentHour.Hour );
				Assert.Equal( minute.MinuteValue, hourMinute );
				Assert.Equal( minute.Start, currentHour.AddMinutes( hourMinute ).Add( minute.Calendar.StartOffset ) );
				Assert.Equal( minute.End, currentHour.AddMinutes( hourMinute + 1 ).Add( minute.Calendar.EndOffset ) );
			}
		} // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Minute")]
        [Fact]
		public void ConstructorTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			Assert.Equal( new Minute( now ).Year, now.Year );
			Assert.Equal( new Minute( now ).Month, now.Month );
			Assert.Equal( new Minute( now ).Day, now.Day );
			Assert.Equal( new Minute( now ).Hour, now.Hour );
			Assert.Equal( new Minute( now ).MinuteValue, now.Minute );

			Assert.Equal( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Year, now.Year );
			Assert.Equal( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Month, now.Month );
			Assert.Equal( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Day, now.Day );
			Assert.Equal( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).Hour, now.Hour );
			Assert.Equal( new Minute( now.Year, now.Month, now.Day, now.Hour, now.Minute ).MinuteValue, now.Minute );
		} // ConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Minute")]
        [Fact]
		public void GetPreviousMinuteTest()
		{
			Minute minute = new Minute();
			Assert.Equal( minute.GetPreviousMinute(), minute.AddMinutes( -1 ) );
		} // GetPreviousMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Minute")]
        [Fact]
		public void GetNextMinuteTest()
		{
			Minute minute = new Minute();
			Assert.Equal( minute.GetNextMinute(), minute.AddMinutes( 1 ) );
		} // GetNextMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Minute")]
        [Fact]
		public void AddMinutesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime nowMinute = new DateTime( now.Year, now.Month, now.Day, now.Hour, now.Minute, 0 );
			Minute minute = new Minute( now, TimeCalendar.NewEmptyOffset() );

			Assert.Equal( minute.AddMinutes( 0 ), minute );

			DateTime previousMinute = nowMinute.AddMinutes( -1 );
			Assert.Equal( minute.AddMinutes( -1 ).Year, previousMinute.Year );
			Assert.Equal( minute.AddMinutes( -1 ).Month, previousMinute.Month );
			Assert.Equal( minute.AddMinutes( -1 ).Day, previousMinute.Day );
			Assert.Equal( minute.AddMinutes( -1 ).Hour, previousMinute.Hour );
			Assert.Equal( minute.AddMinutes( -1 ).MinuteValue, previousMinute.Minute );

			DateTime nextMinute = nowMinute.AddMinutes( 1 );
			Assert.Equal( minute.AddMinutes( 1 ).Year, nextMinute.Year );
			Assert.Equal( minute.AddMinutes( 1 ).Month, nextMinute.Month );
			Assert.Equal( minute.AddMinutes( 1 ).Day, nextMinute.Day );
			Assert.Equal( minute.AddMinutes( 1 ).Hour, nextMinute.Hour );
			Assert.Equal( minute.AddMinutes( 1 ).MinuteValue, nextMinute.Minute );
		} // AddMinutes

	} // class MinuteTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
