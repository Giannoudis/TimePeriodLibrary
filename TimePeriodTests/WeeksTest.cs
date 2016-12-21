// -- FILE ------------------------------------------------------------------
// name       : WeeksTest.cs
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
	public sealed class WeeksTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SingleWeeksTest()
		{
			const int startYear = 2004;
			const int startWeek = 22;
			Weeks weeks = new Weeks( startYear, startWeek, 1 );

			Assert.AreEqual( weeks.Year, startYear );
			Assert.AreEqual( weeks.WeekCount, 1 );
			Assert.AreEqual( weeks.StartWeek, startWeek );
			Assert.AreEqual( weeks.EndWeek, startWeek );
			Assert.AreEqual( weeks.GetWeeks().Count, 1 );
			Assert.IsTrue( weeks.GetWeeks()[ 0 ].IsSamePeriod( new Week( 2004, 22 ) ) );
		} // SingleWeeksTest

		// ----------------------------------------------------------------------
		[Test]
		public void MultiWeekTest()
		{
			const int startYear = 2004;
			const int startWeek = 22;
			const int weekCount = 4;
			Weeks weeks = new Weeks( startYear, startWeek, weekCount );

			Assert.AreEqual( weeks.Year, startYear );
			Assert.AreEqual( weeks.WeekCount, weekCount );
			Assert.AreEqual( weeks.StartWeek, startWeek );
			Assert.AreEqual( weeks.EndWeek, startWeek + weekCount - 1 );
			Assert.AreEqual( weeks.GetWeeks().Count, weekCount );
			Assert.IsTrue( weeks.GetWeeks()[ 0 ].IsSamePeriod( new Week( 2004, 22 ) ) );
		} // MultiWeekTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarWeeksTest()
		{
			const int startYear = 2004;
			const int startWeek = 22;
			const int weekCount = 5;
			Weeks weeks = new Weeks( startYear, startWeek, weekCount );

			Assert.AreEqual( weeks.Year, startYear );
			Assert.AreEqual( weeks.WeekCount, weekCount );
			Assert.AreEqual( weeks.StartWeek, startWeek );
			Assert.AreEqual( weeks.EndWeek, startWeek + weekCount - 1 );
			Assert.AreEqual( weeks.GetWeeks().Count, weekCount );
			Assert.IsTrue( weeks.GetWeeks()[ 0 ].IsSamePeriod( new Week( 2004, 22 ) ) );
			Assert.IsTrue( weeks.GetWeeks()[ 1 ].IsSamePeriod( new Week( 2004, 23 ) ) );
			Assert.IsTrue( weeks.GetWeeks()[ 2 ].IsSamePeriod( new Week( 2004, 24 ) ) );
			Assert.IsTrue( weeks.GetWeeks()[ 3 ].IsSamePeriod( new Week( 2004, 25 ) ) );
			Assert.IsTrue( weeks.GetWeeks()[ 4 ].IsSamePeriod( new Week( 2004, 26 ) ) );
		} // CalendarWeeksTest

	} // class WeeksTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
