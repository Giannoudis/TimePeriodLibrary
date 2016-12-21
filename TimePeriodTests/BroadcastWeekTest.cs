// -- FILE ------------------------------------------------------------------
// name       : BroadcastWeekTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.09.30
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class BroadcastWeekTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SpecificMomentsTest()
		{
			Assert.AreEqual( 2013, new BroadcastWeek( new DateTime( 2013, 12, 29 ) ).Year );
			Assert.AreEqual( 52, new BroadcastWeek( new DateTime( 2013, 12, 29 ) ).Week );

			Assert.AreEqual( 2014, new BroadcastWeek( new DateTime( 2013, 12, 30 ) ).Year );
			Assert.AreEqual( 1, new BroadcastWeek( new DateTime( 2013, 12, 30 ) ).Week );

			Assert.AreEqual( 2014, new BroadcastWeek( new DateTime( 2014, 01, 05 ) ).Year );
			Assert.AreEqual( 1, new BroadcastWeek( new DateTime( 2014, 01, 05 ) ).Week );

			Assert.AreEqual( 2014, new BroadcastWeek( new DateTime( 2014, 01, 06 ) ).Year );
			Assert.AreEqual( 2, new BroadcastWeek( new DateTime( 2014, 01, 06 ) ).Week );
		} // SpecificMomentsTest

		// ----------------------------------------------------------------------
		[Test]
		public void WeekDaysTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			int weekCount = BroadcastCalendarTool.GetWeeksOfYear( currentYear );

			for ( int week = 1; week <= weekCount; week++ )
			{
				Assert.AreEqual( 7, new BroadcastWeek().GetDays().Count );
				Assert.AreEqual( DayOfWeek.Monday, new BroadcastWeek().GetDays()[ 0 ].Start.DayOfWeek );
			}
		} // WeekDaysTest

	} // class BroadcastWeekTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
