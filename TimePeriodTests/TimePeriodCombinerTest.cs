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
using Xunit;
using System.Collections.Generic;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class TimePeriodCombinerTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void NoPeriodsTest()
		{
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection() );
			Assert.Equal(0, periods.Count);
		} // NoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void SinglePeriodAnytimeTest()
		{
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { TimeRange.Anytime } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( TimeRange.Anytime ) );
		} // SinglePeriodAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void SinglePeriodTest()
		{
			TimeRange period = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( period ) );
		} // SinglePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void MomentTest()
		{
			TimeRange period = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 1 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( period ) );
		} // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void TwoPeriodsTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.Equal(2, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( period1 ) );
			Assert.True( periods[ 1 ].IsSamePeriod( period2 ) );
		} // TwoPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void TwoPeriodsTouchingTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsTouchingTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void TwoPeriodsOverlap1Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsOverlap1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void TwoPeriodsOverlap2Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsOverlap2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void TwoPeriodsInside1Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 15 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsInside1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void TwoPeriodsInside2Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TwoPeriodsInside2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void Pattern1Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void Pattern2Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 15 ) );

			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 22 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 22 ), new DateTime( 2011, 3, 28 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 25 ), new DateTime( 2011, 3, 30 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.Equal(2, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 15 ) ) ) );
			Assert.True( periods[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 30 ) ) ) );
		} // Pattern2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void Pattern3Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 20 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern3Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
		public void Pattern4Test()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 25 ) );
			TimeRange period5 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
			ITimePeriodCollection periods = periodCombiner.CombinePeriods( new TimePeriodCollection { period1, period2, period3, period4, period5 } );
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern4Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
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
			Assert.Equal(1, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 25 ) ) ) );
		} // Pattern5Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
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
			Assert.Equal(3, periods.Count);
			Assert.True( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 10 ) ) ) );
			Assert.True( periods[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 24 ) ) ) );
			Assert.True( periods[ 2 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 26 ), new DateTime( 2011, 3, 30 ) ) ) );
		} // Pattern6Test


        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCombiner")]
        [Fact]
        public void CombinePeriodsIsssue9()
        {
            // Combine Periods
            List<TimeRange> timeRanges = new List<TimeRange>() {
                new TimeRange() { Start = new DateTime(2016, 01, 01), End = new DateTime(2016, 01, 01) },
                new TimeRange() { Start = new DateTime(2016, 12, 31), End = new DateTime(2016, 12, 31) }
            };

            TimePeriodCollection periods = new TimePeriodCollection(timeRanges);
            TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
            ITimePeriodCollection combinedPeriods = periodCombiner.CombinePeriods(periods);
            Assert.Equal(0, combinedPeriods.Count);
        }

    } // class TimePeriodCombinerTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
