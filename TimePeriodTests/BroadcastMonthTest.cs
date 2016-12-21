// -- FILE ------------------------------------------------------------------
// name       : BroadcastMonthTest.cs
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
	public sealed class BroadcastMonthTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SpecificMomentsTest()
		{
			Assert.AreEqual( 2013, new BroadcastMonth( new DateTime( 2013, 12, 29 ) ).Year );
			Assert.AreEqual( YearMonth.December, new BroadcastMonth( new DateTime( 2013, 12, 29 ) ).Month );

			Assert.AreEqual( 2014, new BroadcastMonth( new DateTime( 2013, 12, 30 ) ).Year );
			Assert.AreEqual( YearMonth.January, new BroadcastMonth( new DateTime( 2013, 12, 30 ) ).Month );

			Assert.AreEqual( 2014, new BroadcastMonth( new DateTime( 2014, 12, 28 ) ).Year );
			Assert.AreEqual( YearMonth.December, new BroadcastMonth( new DateTime( 2014, 12, 28 ) ).Month );

			Assert.AreEqual( 2015, new BroadcastMonth( new DateTime( 2014, 12, 29 ) ).Year );
			Assert.AreEqual( YearMonth.January, new BroadcastMonth( new DateTime( 2014, 12, 29 ) ).Month );
		} // SpecificMomentsTest

		// ----------------------------------------------------------------------
		[Test]
		public void MonthDaysTest()
		{
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.January ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.February ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.March ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.April ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.May ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.June ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.July ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.August ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.September ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.October ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.November ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.December ).GetDays().Count );

			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.January ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.February ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.March ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.April ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.May ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.June ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.July ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.August ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.September ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.October ).GetDays().Count );
			Assert.AreEqual( 4 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2013, YearMonth.November ).GetDays().Count );
			Assert.AreEqual( 5 * TimeSpec.DaysPerWeek, new BroadcastMonth( 2012, YearMonth.December ).GetDays().Count );
		} // MonthDaysTest

		// ----------------------------------------------------------------------
		[Test]
		public void MonthWeeksTest()
		{
			Assert.AreEqual( 5, new BroadcastMonth( 2012, YearMonth.January ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2012, YearMonth.February ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2012, YearMonth.March ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2012, YearMonth.April ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2012, YearMonth.May ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2012, YearMonth.June ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2012, YearMonth.July ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2012, YearMonth.August ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2012, YearMonth.September ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2012, YearMonth.October ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2012, YearMonth.November ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2012, YearMonth.December ).GetWeeks().Count );

			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.January ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.February ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2013, YearMonth.March ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.April ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.May ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2013, YearMonth.June ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.July ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.August ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2013, YearMonth.September ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.October ).GetWeeks().Count );
			Assert.AreEqual( 4, new BroadcastMonth( 2013, YearMonth.November ).GetWeeks().Count );
			Assert.AreEqual( 5, new BroadcastMonth( 2012, YearMonth.December ).GetWeeks().Count );
		} // MonthWeeksTest

	} // class BroadcastMonthTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
