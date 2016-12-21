// -- FILE ------------------------------------------------------------------
// name       : HoursTest.cs
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
	public sealed class HoursTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SingleHoursTest()
		{
			const int startYear = 2004;
			const int startMonth = 2;
			const int startDay = 22;
			const int startHour = 17;
			Hours hours = new Hours( startYear, startMonth, startDay, startHour, 1 );

			Assert.AreEqual( hours.HourCount, 1 );
			Assert.AreEqual( hours.StartYear, startYear );
			Assert.AreEqual( hours.StartMonth, startMonth );
			Assert.AreEqual( hours.StartDay, startDay );
			Assert.AreEqual( hours.StartHour, startHour );
			Assert.AreEqual( hours.EndYear, 2004 );
			Assert.AreEqual( hours.EndMonth, 2 );
			Assert.AreEqual( hours.EndDay, startDay );
			Assert.AreEqual( hours.EndHour, startHour + 1 );
			Assert.AreEqual( hours.GetHours().Count, 1 );
			Assert.IsTrue( hours.GetHours()[ 0 ].IsSamePeriod( new Hour( 2004, 2, 22, 17 ) ) );
		} // SingleHoursTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarHoursTest()
		{
			const int startYear = 2004;
			const int startMonth = 2;
			const int startDay = 11;
			const int startHour = 22;
			const int hourCount = 4;
			Hours hours = new Hours( startYear, startMonth, startDay, startHour, hourCount );

			Assert.AreEqual( hours.HourCount, hourCount );
			Assert.AreEqual( hours.StartYear, startYear );
			Assert.AreEqual( hours.StartMonth, startMonth );
			Assert.AreEqual( hours.StartDay, startDay );
			Assert.AreEqual( hours.StartHour, startHour );
			Assert.AreEqual( hours.EndYear, 2004 );
			Assert.AreEqual( hours.EndMonth, 2 );
			Assert.AreEqual( hours.EndDay, startDay + 1 );
			Assert.AreEqual( hours.EndHour, 2 );
			Assert.AreEqual( hours.GetHours().Count, hourCount );
			Assert.IsTrue( hours.GetHours()[ 0 ].IsSamePeriod( new Hour( 2004, 2, 11, 22 ) ) );
			Assert.IsTrue( hours.GetHours()[ 1 ].IsSamePeriod( new Hour( 2004, 2, 11, 23 ) ) );
			Assert.IsTrue( hours.GetHours()[ 2 ].IsSamePeriod( new Hour( 2004, 2, 12, 0 ) ) );
			Assert.IsTrue( hours.GetHours()[ 3 ].IsSamePeriod( new Hour( 2004, 2, 12, 1 ) ) );
		} // CalendarHoursTest

	} // class HoursTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
