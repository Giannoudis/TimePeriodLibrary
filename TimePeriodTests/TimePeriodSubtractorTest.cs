// -- FILE ------------------------------------------------------------------
// name       : TimePeriodSubtractorTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.01.29
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
	public sealed class TimePeriodSubtractorTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void NoPeriodsTest()
		{
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection(), new TimePeriodCollection() );
			Assert.AreEqual( periods.Count, 0 );
		} // NoPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SinglePeriodAnytimeTest()
		{
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { TimeRange.Anytime }, new TimePeriodCollection { TimeRange.Anytime } );
			Assert.AreEqual( periods.Count, 0 );
		} // SinglePeriodAnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 05 ) );
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { period1 }, new TimePeriodCollection { period2 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( period1 ) );
		} // MomentTest


		// ----------------------------------------------------------------------
		[Test]
		public void TouchingPeriodsTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 4, 01 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 4, 01 ) );
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { period1 }, new TimePeriodCollection { period2, period3 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // TouchingPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectionPeriodsTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 4, 01 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 25 ) );
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { period1 }, new TimePeriodCollection { period2, period3 } );
			Assert.AreEqual( periods.Count, 3 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) ) ) );
			Assert.IsTrue( periods[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) ) ) );
			Assert.IsTrue( periods[ 2 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 25 ), new DateTime( 2011, 4, 01 ) ) ) );
		} // IntersectionPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void NoSubtractionTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { period1, period3 }, new TimePeriodCollection { period2, period4 } );
			Assert.AreEqual( periods.Count, 2 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( period1 ) );
			Assert.IsTrue( periods[ 1 ].IsSamePeriod( period3 ) );
		} // NoSubtractionTest

		// ----------------------------------------------------------------------
		[Test]
		public void CombineTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { period1, period2 }, new TimePeriodCollection() );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( period1.Start, period2.End ) ) );
		} // CombineTest

		// ----------------------------------------------------------------------
		[Test]
		public void NotCombineTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { period1, period2 }, 
				new TimePeriodCollection(), false );
			Assert.AreEqual( periods.Count, 2 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( period1 ) );
			Assert.IsTrue( periods[ 1 ].IsSamePeriod( period2 ) );
		} // NotCombineTest

		// ----------------------------------------------------------------------
		[Test]
		public void NoSubtractionCombineTest()
		{
			TimeRange period1 = new TimeRange( new DateTime( 2011, 3, 01 ), new DateTime( 2011, 3, 05 ) );
			TimeRange period2 = new TimeRange( new DateTime( 2011, 3, 05 ), new DateTime( 2011, 3, 10 ) );
			TimeRange period3 = new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) );
			TimeRange period4 = new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) );
			TimePeriodSubtractor<TimeRange> periodSubtractor = new TimePeriodSubtractor<TimeRange>();
			ITimePeriodCollection periods = periodSubtractor.SubtractPeriods( new TimePeriodCollection { period1, period2 }, new TimePeriodCollection { period3, period4 } );
			Assert.AreEqual( periods.Count, 1 );
			Assert.IsTrue( periods[ 0 ].IsSamePeriod( new TimeRange( period1.Start, period2.End ) ) );
		} // NoSubtractionCombineTest

	} // class TimePeriodSubtractorTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
