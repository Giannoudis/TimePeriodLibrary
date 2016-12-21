// -- FILE ------------------------------------------------------------------
// name       : CalendarTimeRangeTest.cs
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
	public sealed class CalendarTimeRangeTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarTest()
		{
			TimeCalendar calendar = new TimeCalendar();
			CalendarTimeRange calendarTimeRange = new CalendarTimeRange( TimeRange.Anytime, calendar );
			Assert.AreEqual( calendarTimeRange.Calendar, calendar );
		} // CalendarTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void MomentTest()
		{
			DateTime testDate = new DateTime( 2000, 10, 1 );
			new CalendarTimeRange( testDate, testDate );
		} // CalendarTest

	} // class CalendarTimeRangeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
