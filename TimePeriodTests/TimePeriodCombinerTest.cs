// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCombinerTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.29
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
	public sealed class TimePeriodCombinerTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void NoPeriodsTest()
		{
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection() );
			Assert.AreEqual( periods.Count, 0 );
		} // NoPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SinglePeriodAnytimeTest()
		{
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { TimeRange.Anytime } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( TimeRange.Anytime ) );
		} // SinglePeriodAnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void SinglePeriodTest()
		{
			TimeRange period = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( period ) );
		} // SinglePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentTest()
		{
			TimeRange period = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 1 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( period ) );
		} // MomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void TwoPeriodsTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.AreEqual( periods.Count, 2 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( period1 ) );
			Assert.IsTrue( periods[ 1 ].IsSamePeriod( period2 ) );
		} // TwoPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void TwoPeriodsTouchingTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsTouchingTest

		// ----------------------------------------------------------------------
		[Test]
		public void TwoPeriodsOverlap1Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsOverlap1Test

		// ----------------------------------------------------------------------
		[Test]
		public void TwoPeriodsOverlap2Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsOverlap2Test

		// ----------------------------------------------------------------------
		[Test]
		public void TwoPeriodsInside1Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 15 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsInside1Test

		// ----------------------------------------------------------------------
		[Test]
		public void TwoPeriodsInside2Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsInside2Test

		// ----------------------------------------------------------------------
		[Test]
		public void Pattern1Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern1Test

		// ----------------------------------------------------------------------
		[Test]
		public void Pattern2Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 15 ) );

			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 22 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 22 ), new DateTime( 2011, 3, 28 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 25 ), new DateTime( 2011, 3, 30 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.AreEqual( periods.Count, 2 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 15 ) ) ) );
			Assert.IsTrue( periods[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 30 ) ) ) );
		} // Pattern2Test

		// ----------------------------------------------------------------------
		[Test]
		public void Pattern3Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 20 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern3Test

		// ----------------------------------------------------------------------
		[Test]
		public void Pattern4Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern4Test

		// ----------------------------------------------------------------------
		[Test]
		public void Pattern5Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period6 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5, period6 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern5Test

		// ----------------------------------------------------------------------
		[Test]
		public void Pattern6Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 04 ), new DateTime( 2011, 3, 08 ) );

			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 18 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 18 ), new DateTime( 2011, 3, 22 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 24 ) );

			TimeRange period6 = new TimeRange( new DateTime( 2011, 3, 26 ), new DateTime( 2011, 3, 30 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5, period6 } );
			Assert.AreEqual( periods.Count, 3 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 10 ) ) ) );
			Assert.IsTrue( periods[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 24 ) ) ) );
			Assert.IsTrue( periods[ 2 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 26 ), new DateTime( 2011, 3, 30 ) ) ) );
		} // Pattern6Test

	} // class TimePeriodCombinerTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
