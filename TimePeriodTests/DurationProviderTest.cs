// -- FILE ------------------------------------------------------------------
// name       : DurationProviderTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.11.03
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class DurationProviderTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void DaylightDurationTest()
		{
			DateTime dstStart = new DateTime( 2014, 3, 30, 2, 0, 0 ).AddHours( 1 );
			DateTime dstStartBefore = dstStart.AddHours( -2 );
			DateTime dstStartAfter = dstStart.AddHours( 2 );
			DateTime dstEnd = new DateTime( 2014, 10, 26, 3, 0, 0 ).AddHours( -1 );
			DateTime dstEndBefore = dstEnd.AddHours( -2 );
			DateTime dstEndAfter = dstEnd.AddHours( 2 );

			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById( "Central Europe Standard Time" );

			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstStart.AddTicks( -1 ) ), false );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstStart ), true );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstStart.AddTicks( 1 ) ), true );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstStartBefore ), false );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstStartAfter ), true );
			Assert.AreEqual( dstStart.Subtract( dstStartBefore ), new TimeSpan( 2, 0, 0 ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstStartBefore, dstStart, timeZone ), new TimeSpan( 1, 0, 0 ) );
			Assert.AreEqual( dstStartAfter.Subtract( dstStart ), new TimeSpan( 2, 0, 0 ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstStart, dstStartAfter, timeZone ), new TimeSpan( 2, 0, 0 ) );
			Assert.AreEqual( dstStartAfter.Subtract( dstStartBefore ), new TimeSpan( 4, 0, 0 ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstStartBefore, dstStartAfter, timeZone ), new TimeSpan( 3, 0, 0 ) );

			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstEnd.AddTicks( -1 ) ), true );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstEnd ), false );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstEnd.AddTicks( 1 ) ), false );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstEndBefore ), true );
			Assert.AreEqual( timeZone.IsDaylightSavingTime( dstEndAfter ), false );
			Assert.AreEqual( dstEnd.Subtract( dstEndBefore ), new TimeSpan( 2, 0, 0 ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstEndBefore, dstEnd, timeZone ), new TimeSpan( 3, 0, 0 ) );
			Assert.AreEqual( dstEndAfter.Subtract( dstEnd ), new TimeSpan( 2, 0, 0 ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstEnd, dstEndAfter, timeZone ), new TimeSpan( 2, 0, 0 ) );
			Assert.AreEqual( dstEndAfter.Subtract( dstEndBefore ), new TimeSpan( 4, 0, 0 ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstEndBefore, dstEndAfter, timeZone ), new TimeSpan( 5, 0, 0 ) );

			Assert.AreEqual( Duration.GetDaylightDuration( dstStartBefore, dstEndAfter, timeZone ), dstEndAfter.Subtract( dstStartBefore ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstStartAfter, dstEndBefore, timeZone ), dstEndBefore.Subtract( dstStartAfter ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstStartBefore, dstEndBefore, timeZone ), dstEndBefore.Subtract( dstStartBefore ).Subtract( new TimeSpan( 1, 0, 0 ) ) );
			Assert.AreEqual( Duration.GetDaylightDuration( dstStartAfter, dstEndAfter, timeZone ), dstEndAfter.Subtract( dstStartAfter ).Add( new TimeSpan( 1, 0, 0 ) ) );
		} // DaylightDurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void UnsafeDaylightDurationTest()
		{
			DateTime unsafeMoment = new DateTime( 2013,10, 27, 2, 30, 0 );
			DateTime unsafeBefore = unsafeMoment.AddHours( -2 );
			DateTime unsafeAfter = unsafeMoment.AddHours( 2 );

			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById( "Central Europe Standard Time" );

			var utcOffset = new DateTimeOffset(unsafeMoment, TimeSpan.Zero);
			DateTimeOffset safeMoment = new DateTimeOffset(unsafeMoment, timeZone.GetUtcOffset(utcOffset));


			Assert.AreEqual( timeZone.IsDaylightSavingTime( unsafeMoment ), false );
			Assert.AreEqual( Duration.GetDaylightDuration( unsafeBefore, unsafeMoment, timeZone ), new TimeSpan( 1, 0, 0 ) );
			Assert.AreEqual( Duration.GetDaylightDuration( unsafeMoment, unsafeAfter, timeZone ), new TimeSpan( 1, 0, 0 ) );
		} // UnsafeDaylightDurationTest

	} // class DurationTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
