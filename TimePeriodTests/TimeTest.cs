// -- FILE ------------------------------------------------------------------
// name       : TimeTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.12
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
	public sealed class TimeTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTest()
		{
			Time time = new Time( 18, 23, 56, 344 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 23 );
			Assert.AreEqual( time.Second, 56 );
			Assert.AreEqual( time.Millisecond, 344 );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorHourTest()
		{
			Time time = new Time( 18 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 0 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorMinuteTest()
		{
			Time time = new Time( 18, 23 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 23 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
		} // ConstructorMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorSecondTest()
		{
			Time time = new Time( 18, 23, 56 );

			Assert.AreEqual( time.Hour, 18 );
			Assert.AreEqual( time.Minute, 23 );
			Assert.AreEqual( time.Second, 56 );
			Assert.AreEqual( time.Millisecond, 0 );
		} // ConstructorSecondTest

		// ----------------------------------------------------------------------
		[Test]
		public void EmptyConstructorTest()
		{
			Time time = new Time();

			Assert.AreEqual( time.Hour, 0 );
			Assert.AreEqual( time.Minute, 0 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
			Assert.AreEqual( time.Ticks, 0 );
			Assert.AreEqual( time.Duration, TimeSpan.Zero );
			Assert.AreEqual( time.TotalHours, 0 );
			Assert.AreEqual( time.TotalMinutes, 0 );
			Assert.AreEqual( time.TotalSeconds, 0 );
			Assert.AreEqual( time.TotalMilliseconds, 0 );
		} // EmptyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void DateTimeConstructorTest()
		{
			DateTime test = new DateTime( 2009, 7, 22, 18, 23, 56, 344 );
			Time time = new Time( test );

			Assert.AreEqual( time.Hour, test.Hour );
			Assert.AreEqual( time.Minute, test.Minute );
			Assert.AreEqual( time.Second, test.Second );
			Assert.AreEqual( time.Millisecond, test.Millisecond );
		} // DateTimeConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxHourMinuteConstructorTest()
		{
			new Time( 24, 1 );
		} // MaxHourMinuteConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxHourSecondConstructorTest()
		{
			new Time( 24, 0, 1 );
		} // MaxHourSecondConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxHourMillisecondConstructorTest()
		{
			new Time( 24, 0, 0, 1 );
		} // MaxHourMillisecondConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void EmptyDateTimeConstructorTest()
		{
			DateTime test = new DateTime( 2009, 7, 22 );
			Time time = new Time( test );

			Assert.AreEqual( time.Hour, 0 );
			Assert.AreEqual( time.Minute, 0 );
			Assert.AreEqual( time.Second, 0 );
			Assert.AreEqual( time.Millisecond, 0 );
			Assert.AreEqual( time.Ticks, 0 );
			Assert.AreEqual( time.Duration, TimeSpan.Zero );
			Assert.AreEqual( time.TotalHours, 0 );
			Assert.AreEqual( time.TotalMinutes, 0 );
			Assert.AreEqual( time.TotalSeconds, 0 );
			Assert.AreEqual( time.TotalMilliseconds, 0 );
		} // EmptyDateTimeConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinValueTest()
		{
			new Time( 0 );
		} // MinValueTest

		// ----------------------------------------------------------------------
		[Test]
		public void MaxValueTest()
		{
			new Time( TimeSpec.HoursPerDay - 1, TimeSpec.MinutesPerHour - 1, TimeSpec.SecondsPerMinute - 1, TimeSpec.MillisecondsPerSecond - 1 );
		} // MinValueTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinHourTest()
		{
			new Time( -1 );
		} // MinHourTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxHourTest()
		{
			new Time( TimeSpec.HoursPerDay + 1 );
		} // MaxHourTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinMinuteTest()
		{
			new Time( 0, -1 );
		} // MinMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxMinuteTest()
		{
			new Time( 0, TimeSpec.MinutesPerHour );
		} // MaxMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinSecondTest()
		{
			new Time( 0, 0, -1 );
		} // MinSecondTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxSecondTest()
		{
			new Time( 0, 0, TimeSpec.SecondsPerMinute );
		} // MaxSecondTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinMillisecondTest()
		{
			new Time( 0, 0, 0, -1 );
		} // MinMillisecondTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxMillisecondTest()
		{
			new Time( 0, 0, 0, TimeSpec.MillisecondsPerSecond );
		} // MaxMillisecondTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationTest()
		{

			TimeSpan test = new TimeSpan( 0, 18, 23, 56, 344 );
			Time time = new Time( test.Hours, test.Minutes, test.Seconds, test.Milliseconds );

			Assert.AreEqual( time.Hour, test.Hours );
			Assert.AreEqual( time.Minute, test.Minutes );
			Assert.AreEqual( time.Second, test.Seconds );
			Assert.AreEqual( time.Millisecond, test.Milliseconds );

			Assert.AreEqual( time.Duration.Ticks, test.Ticks );

			Assert.AreEqual( time.TotalHours, test.TotalHours );
			Assert.AreEqual( time.TotalMinutes, test.TotalMinutes );
			Assert.AreEqual( time.TotalSeconds, test.TotalSeconds );
			Assert.AreEqual( time.TotalMilliseconds, test.TotalMilliseconds );
		} // DurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void ToDateTimeTest()
		{
			DateTime dateTime = new DateTime( 2009, 7, 22 );
			TimeSpan timeSpan = new TimeSpan( 0, 18, 23, 56, 344 );
			Time time = new Time( timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds );

			Assert.AreEqual( time.ToDateTime( dateTime ), dateTime.Add( timeSpan ) );
		} // ToDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDateTimeFromDateTest()
		{
			Date date = new Date( 2009, 7, 22 );
			TimeSpan timeSpan = new TimeSpan( 0, 18, 23, 56, 344 );
			Time time = new Time( timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds );

			Assert.AreEqual( time.ToDateTime( date ), date.DateTime.Add( timeSpan ) );
		} // GetDateTimeFromDateTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetEmptyDateTimeTest()
		{
			DateTime dateTime = new DateTime( 2009, 7, 22 );
			Time time = new Time();

			Assert.AreEqual( time.ToDateTime( dateTime ), dateTime );
		} // GetEmptyDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsZeroTest()
		{
			DateTime midnight = ClockProxy.Clock.Now.Date;
			Time time0 = new Time( 0 );
			Time timeBefore = new Time( midnight.AddMilliseconds( -1 ) );
			Time timeAfter = new Time( midnight.AddMilliseconds( 1 ) );
			Time time24 = new Time( 24 );

			Assert.AreEqual( true, time0.IsZero );
			Assert.AreEqual( false, timeBefore.IsZero );
			Assert.AreEqual( false, timeAfter.IsZero );
			Assert.AreEqual( false, time24.IsZero );
		} // IsZeroTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsFullDayTest()
		{
			DateTime midnight = ClockProxy.Clock.Now.Date;
			Time time0 = new Time( 0 );
			Time time24 = new Time( 24 );
			Time timeBefore = new Time( midnight.AddMilliseconds( -1 ) );
			Time timeAfter = new Time( midnight.AddMilliseconds( 1 ) );

			Assert.AreEqual( false, time0.IsFullDay );
			Assert.AreEqual( true, time24.IsFullDay );
			Assert.AreEqual( false, timeBefore.IsFullDay );
			Assert.AreEqual( false, timeAfter.IsFullDay );
		} // IsFullDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsFullDayOrZeroTest()
		{
			DateTime midnight = ClockProxy.Clock.Now.Date;
			Time time0 = new Time( 0 );
			Time timeBefore0 = new Time( midnight.AddMilliseconds( -1 ) );
			Time timeAfter0 = new Time( midnight.AddMilliseconds( 1 ) );

			Assert.AreEqual( true, time0.IsFullDayOrZero );
			Assert.AreEqual( false, time0.Equals( timeBefore0 ) );
			Assert.AreEqual( false, time0.Equals( timeAfter0 ) );

			Time time24 = new Time( 24 );
			Time timeBefore24 = new Time( midnight.AddMilliseconds( -1 ) );
			Time timeAfter24 = new Time( midnight.AddMilliseconds( 1 ) );

			Assert.AreNotEqual( time0, time24 );
			Assert.AreEqual( true, time24.IsFullDayOrZero );
			Assert.AreEqual( false, time24.Equals( timeBefore24 ) );
			Assert.AreEqual( false, time24.Equals( timeAfter24 ) );
		} // IsFullDayOrZeroTest

		// ----------------------------------------------------------------------
		[Test]
		public void CompareToTest()
		{
			DateTime today = ClockProxy.Clock.Now.Date;

			Time empty = new Time();
			Assert.AreEqual( 0, empty.CompareTo( empty ) );
			Assert.AreEqual( -1, empty.CompareTo( new Time( today.AddMilliseconds( -1 ) ) ) );
			Assert.AreEqual( -1, empty.CompareTo( new Time( today.AddMilliseconds( 1 ) ) ) );

			Time midnight = new Time( today );
			Assert.AreEqual( 0, midnight.CompareTo( midnight ) );
			Assert.AreEqual( -1, midnight.CompareTo( new Time( today.AddMilliseconds( -1 ) ) ) );
			Assert.AreEqual( -1, midnight.CompareTo( new Time( today.AddMilliseconds( 1 ) ) ) );

			Time midday = new Time( today.AddHours( 12 ) );
			Assert.AreEqual( 0, midday.CompareTo( midday ) );
			Assert.AreEqual( 1, midday.CompareTo( new Time( today.AddHours( 12 ).AddMilliseconds( -1 ) ) ) );
			Assert.AreEqual( -1, midday.CompareTo( new Time( today.AddHours( 12 ).AddMilliseconds( 1 ) ) ) );

			Time full = new Time( 24 );
			Assert.AreEqual( 0, full.CompareTo( full ) );
			Assert.AreEqual( 1, full.CompareTo( new Time( today.AddMilliseconds( -1 ) ) ) );
			Assert.AreEqual( 1, full.CompareTo( new Time( today.AddMilliseconds( 1 ) ) ) );
		} // CompareToTest

		// ----------------------------------------------------------------------
		[Test]
		public void EquatableTest()
		{
			DateTime now = ClockProxy.Clock.Now.Date;
			Time time = new Time( now );
			Time timeBefore = new Time( now.AddMilliseconds( -1 ) );
			Time timeAfter = new Time( now.AddMilliseconds( 1 ) );

			Assert.AreEqual( true, time.Equals( time ) );
			Assert.AreEqual( false, time.Equals( timeBefore ) );
			Assert.AreEqual( false, time.Equals( timeAfter ) );
		} // EquatableTest

		// ----------------------------------------------------------------------
		[Test]
		public void MidnightOperatorTest()
		{
			DateTime midnight = ClockProxy.Clock.Now.Date;
			DateTime midday = midnight.AddHours( 12 );
			Time time = new Time( midnight );
			Time timeBefore = new Time( midnight.AddMilliseconds( -1 ) );
			Time timeAfter = new Time( midnight.AddMilliseconds( 1 ) );

			TimeSpan oneDay = new TimeSpan( 1, 0, 0, 0, 0 );
			TimeSpan hours18 = new TimeSpan( 0, 18, 0, 0, 0 );
			TimeSpan hours12 = new TimeSpan( 0, 12, 0, 0, 0 );
			TimeSpan hours6 = new TimeSpan( 0, 6, 0, 0, 0 );
			TimeSpan oneMillisecond = new TimeSpan( 0, 0, 0, 0, 1 );

			Assert.AreEqual( new TimeSpan( 0, 0, 0, 0, 1 ), time - timeBefore );
			Assert.AreEqual( new TimeSpan( 0, 0, 0, 0, 1 ), timeAfter - time );
			Assert.AreEqual( time, time - oneDay );
			Assert.AreEqual( time, time - oneDay.Negate() );
			Assert.AreEqual( new Time( midday + hours6 ), time - hours6 );
			Assert.AreEqual( new Time( midday ), time - hours12 );
			Assert.AreEqual( new Time( midnight - hours18 ), time - hours18 );
			Assert.AreEqual( timeBefore, time - oneMillisecond );
			Assert.AreEqual( oneMillisecond, time - timeBefore );

			Assert.AreEqual( new TimeSpan( 0, 23, 59, 59, 999 ), time + timeBefore );
			Assert.AreEqual( new TimeSpan( 0, 0, 0, 0, 1 ), time + timeAfter );
			Assert.AreEqual( timeAfter, time + oneMillisecond );
			Assert.AreEqual( time, time + oneDay );
			Assert.AreEqual( time, time + oneDay.Negate() );
			Assert.AreEqual( new Time( midnight + hours18 ), time + hours18 );
			Assert.AreEqual( new Time( midday ), time + hours12 );
			Assert.AreEqual( new Time( hours6 ), time + hours6 );

			Assert.AreEqual( false, timeBefore < time );
			Assert.AreEqual( false, timeAfter < time );

			Assert.AreEqual( false, timeBefore <= time );
			Assert.AreEqual( false, timeAfter <= time );

			Assert.AreEqual( false, timeBefore == time );
			Assert.AreEqual( false, timeAfter == time );

			Assert.AreEqual( true, timeBefore != time );
			Assert.AreEqual( true, timeAfter != time );

			Assert.AreEqual( true, timeBefore >= time );
			Assert.AreEqual( true, timeAfter >= time );

			Assert.AreEqual( true, timeBefore > time );
			Assert.AreEqual( true, timeAfter > time );
		} // MidnightOperatorTest

		// ----------------------------------------------------------------------
		[Test]
		public void MiddayOperatorTest()
		{
			DateTime midnight = ClockProxy.Clock.Now.Date;
			DateTime midday = midnight.AddHours( 12 );
			Time time = new Time( midday );
			Time timeBefore = new Time( midday.AddMilliseconds( -1 ) );
			Time timeAfter = new Time( midday.AddMilliseconds( 1 ) );

			TimeSpan oneDay = new TimeSpan( 1, 0, 0, 0, 0 );
			TimeSpan hours18 = new TimeSpan( 0, 18, 0, 0, 0 );
			TimeSpan hours12 = new TimeSpan( 0, 12, 0, 0, 0 );
			TimeSpan hours6 = new TimeSpan( 0, 6, 0, 0, 0 );
			TimeSpan oneMillisecond = new TimeSpan( 0, 0, 0, 0, 1 );

			Assert.AreEqual( new TimeSpan( 0, 0, 0, 0, 1 ), time - timeBefore );
			Assert.AreEqual( new TimeSpan( 0, 0, 0, 0, 1 ), timeAfter - time );
			Assert.AreEqual( time, time - oneDay );
			Assert.AreEqual( time, time - oneDay.Negate() );
			Assert.AreEqual( new Time( midday - hours6 ), time - hours6 );
			Assert.AreEqual( new Time( midnight ), time - hours12 );
			Assert.AreEqual( new Time( midday - hours18 ), time - hours18 );
			Assert.AreEqual( timeBefore, time - oneMillisecond );
			Assert.AreEqual( oneMillisecond, time - timeBefore );

			Assert.AreEqual( new TimeSpan( 0, 23, 59, 59, 999 ), time + timeBefore );
			Assert.AreEqual( new TimeSpan( 0, 0, 0, 0, 1 ), time + timeAfter );
			Assert.AreEqual( timeAfter, time + oneMillisecond );
			Assert.AreEqual( time, time + oneDay );
			Assert.AreEqual( time, time + oneDay.Negate() );
			Assert.AreEqual( new Time( midnight + hours6 ), time + hours18 );
			Assert.AreEqual( new Time( midnight ), time + hours12 );
			Assert.AreEqual( new Time( midnight + hours18 ), time + hours6 );

			Assert.AreEqual( true, timeBefore < time );
			Assert.AreEqual( false, timeAfter < time );

			Assert.AreEqual( true, timeBefore <= time );
			Assert.AreEqual( false, timeAfter <= time );

			Assert.AreEqual( false, timeBefore == time );
			Assert.AreEqual( false, timeAfter == time );

			Assert.AreEqual( true, timeBefore != time );
			Assert.AreEqual( true, timeAfter != time );

			Assert.AreEqual( false, timeBefore >= time );
			Assert.AreEqual( true, timeAfter >= time );

			Assert.AreEqual( false, timeBefore > time );
			Assert.AreEqual( true, timeAfter > time );
		} // MiddayOperatorTest

	} // class TimeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
