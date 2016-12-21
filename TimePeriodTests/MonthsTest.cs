// -- FILE ------------------------------------------------------------------
// name       : MonthsTest.cs
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
	public sealed class MonthsTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SingleMonthsTest()
		{
			const int startYear = 2004;
			const YearMonth startMonth = YearMonth.June;
			Months months = new Months( startYear, startMonth, 1 );

			Assert.AreEqual( months.MonthCount, 1 );
			Assert.AreEqual( months.StartMonth, startMonth );
			Assert.AreEqual( months.StartYear, startYear );
			Assert.AreEqual( months.EndYear, startYear );
			Assert.AreEqual( months.EndMonth, YearMonth.June );
			Assert.AreEqual( months.GetMonths().Count, 1 );
			Assert.IsTrue( months.GetMonths()[ 0 ].IsSamePeriod( new Month( 2004, YearMonth.June ) ) );
		} // SingleMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarMonthsTest()
		{
			const int startYear = 2004;
			const YearMonth startMonth = YearMonth.November;
			const int monthCount = 5;
			Months months = new Months( startYear, startMonth, monthCount );

			Assert.AreEqual( months.MonthCount, monthCount );
			Assert.AreEqual( months.StartMonth, startMonth );
			Assert.AreEqual( months.StartYear, startYear );
			Assert.AreEqual( months.EndYear, 2005 );
			Assert.AreEqual( months.EndMonth, YearMonth.March );
			Assert.AreEqual( months.GetMonths().Count, monthCount );
			Assert.IsTrue( months.GetMonths()[ 0 ].IsSamePeriod( new Month( 2004, YearMonth.November ) ) );
			Assert.IsTrue( months.GetMonths()[ 1 ].IsSamePeriod( new Month( 2004, YearMonth.December ) ) );
			Assert.IsTrue( months.GetMonths()[ 2 ].IsSamePeriod( new Month( 2005, YearMonth.January ) ) );
			Assert.IsTrue( months.GetMonths()[ 3 ].IsSamePeriod( new Month( 2005, YearMonth.February ) ) );
			Assert.IsTrue( months.GetMonths()[ 4 ].IsSamePeriod( new Month( 2005, YearMonth.March ) ) );
		} // CalendarMonthsTest

	} // class MonthsTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
