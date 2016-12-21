// -- FILE ------------------------------------------------------------------
// name       : MinutesTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class MinutesTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SingleMinutesTest()
		{
			const int startYear = 2004;
			const int startMonth = 2;
			const int startDay = 22;
			const int startHour = 17;
			const int startMinute = 42;
			Minutes minutes = new Minutes( startYear, startMonth, startDay,  startHour, startMinute, 1 );

			Assert.AreEqual( minutes.MinuteCount, 1 );
			Assert.AreEqual( minutes.StartYear, startYear );
			Assert.AreEqual( minutes.StartMonth, startMonth );
			Assert.AreEqual( minutes.StartDay, startDay );
			Assert.AreEqual( minutes.StartHour, startHour );
			Assert.AreEqual( minutes.StartMinute, startMinute );
			Assert.AreEqual( minutes.EndYear, 2004 );
			Assert.AreEqual( minutes.EndMonth, 2 );
			Assert.AreEqual( minutes.EndDay, startDay );
			Assert.AreEqual( minutes.EndHour, 17 );
			Assert.AreEqual( minutes.EndMinute, startMinute + 1 );
			Assert.AreEqual( minutes.GetMinutes().Count, 1 );
			Assert.IsTrue( minutes.GetMinutes()[ 0 ].IsSamePeriod( new Minute( 2004, 2, 22, 17, 42 ) ) );
		} // SingleMinutesTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarMinutesTest()
		{
			const int startYear = 2004;
			const int startMonth = 2;
			const int startDay = 22;
			const int startHour = 23;
			const int startMinute = 59;
			const int minuteCount = 4;
			Minutes minutes = new Minutes( startYear, startMonth, startDay, startHour, startMinute, minuteCount );

			Assert.AreEqual( minutes.MinuteCount, minuteCount );
			Assert.AreEqual( minutes.StartYear, startYear );
			Assert.AreEqual( minutes.StartMonth, startMonth );
			Assert.AreEqual( minutes.StartDay, startDay );
			Assert.AreEqual( minutes.StartHour, startHour );
			Assert.AreEqual( minutes.StartMinute, startMinute );
			Assert.AreEqual( minutes.EndYear, 2004 );
			Assert.AreEqual( minutes.EndMonth, 2 );
			Assert.AreEqual( minutes.EndDay, 23 );
			Assert.AreEqual( minutes.EndHour, 0 );
			Assert.AreEqual( minutes.EndMinute, 3 );
			Assert.AreEqual( minutes.GetMinutes().Count, minuteCount );
			Assert.IsTrue( minutes.GetMinutes()[ 0 ].IsSamePeriod( new Minute( 2004, 2, 22, 23, 59 ) ) );
			Assert.IsTrue( minutes.GetMinutes()[ 1 ].IsSamePeriod( new Minute( 2004, 2, 23, 0, 0 ) ) );
			Assert.IsTrue( minutes.GetMinutes()[ 2 ].IsSamePeriod( new Minute( 2004, 2, 23, 0, 1 ) ) );
			Assert.IsTrue( minutes.GetMinutes()[ 3 ].IsSamePeriod( new Minute( 2004, 2, 23, 0, 2 ) ) );
		} // CalendarMinutesTest

	} // class MinutesTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
