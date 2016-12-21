// -- FILE ------------------------------------------------------------------
// name       : DaysTest.cs
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
	public sealed class DaysTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SingleDaysTest()
		{
			const int startYear = 2004;
			const int startMonth = 2;
			const int startDay = 22;
			Days days = new Days( startYear, startMonth, startDay, 1 );

			Assert.AreEqual( days.DayCount, 1 );
			Assert.AreEqual( days.StartYear, startYear );
			Assert.AreEqual( days.StartMonth, startMonth );
			Assert.AreEqual( days.StartDay, startDay );
			Assert.AreEqual( days.EndYear, 2004 );
			Assert.AreEqual( days.EndMonth, 2 );
			Assert.AreEqual( days.EndDay, startDay );
			Assert.AreEqual( days.GetDays().Count, 1 );
			Assert.IsTrue( days.GetDays()[ 0 ].IsSamePeriod( new Day( 2004, 2, 22 ) ) );
		} // SingleDaysTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarDaysTest()
		{
			const int startYear = 2004;
			const int startMonth = 2;
			const int startDay = 27;
			const int dayCount = 5;
			Days days = new Days( startYear, startMonth, startDay, dayCount );

			Assert.AreEqual( days.DayCount, dayCount );
			Assert.AreEqual( days.StartYear, startYear );
			Assert.AreEqual( days.StartMonth, startMonth );
			Assert.AreEqual( days.StartDay, startDay );
			Assert.AreEqual( days.EndYear, 2004 );
			Assert.AreEqual( days.EndMonth, 3 );
			Assert.AreEqual( days.EndDay, 2 );
			Assert.AreEqual( days.GetDays().Count, dayCount );
			Assert.IsTrue( days.GetDays()[ 0 ].IsSamePeriod( new Day( 2004, 2, 27 ) ) );
			Assert.IsTrue( days.GetDays()[ 1 ].IsSamePeriod( new Day( 2004, 2, 28 ) ) );
			Assert.IsTrue( days.GetDays()[ 2 ].IsSamePeriod( new Day( 2004, 2, 29 ) ) );
			Assert.IsTrue( days.GetDays()[ 3 ].IsSamePeriod( new Day( 2004, 3, 1 ) ) );
			Assert.IsTrue( days.GetDays()[ 4 ].IsSamePeriod( new Day( 2004, 3, 2 ) ) );
		} // CalendarDaysTest

	} // class DaysTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
