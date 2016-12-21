// -- FILE ------------------------------------------------------------------
// name       : TimeGapCalculatorTest.cs
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
	public sealed class TimeGapCalculatorTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void NoPeriodsTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			ITimePeriodCollection gaps = gapCalculator.GetGaps( new TimePeriodCollection(), limits );
			Assert.AreEqual( gaps.Count, 1 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( limits ) );
		} // NoPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodEqualsLimitsTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( limits );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 0 );
		} // PeriodEqualsLimitsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodLargerThanLimitsTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 2, 1 ), new DateTime( 2011, 4, 1 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 0 );
		} // PeriodLargerThanLimitsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodOutsideLimitsTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 2, 1 ), new DateTime( 2011, 2, 15 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 4, 1 ), new DateTime( 2011, 4, 15 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 1 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( limits ) );
		} // PeriodOutsideLimitsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodOutsideTouchingLimitsTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 31 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 2, 1 ), new DateTime( 2011, 3, 5 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 4, 15 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 1 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // PeriodOutsideTouchingLimitsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SimleGapsTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 2 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) ) ) );
			Assert.IsTrue( gaps[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // SimleGapsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodTouchingLimitsStartTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 1 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) ) ) );
		} // PeriodTouchingLimitsStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodTouchingLimitsEndTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 1 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 10 ) ) ) );
		} // PeriodTouchingLimitsEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void MomentPeriodTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 20 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 10 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 1 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( limits ) );
		} // MomentPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void TouchingPeriodsTest()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 4, 1 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 00, 00, 0 ), new DateTime( 2011, 3, 30, 08, 30, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 08, 30, 0 ), new DateTime( 2011, 3, 30, 12, 00, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 10, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 2 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 3, 30 ) ) ) );
			Assert.IsTrue( gaps[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 31 ), new DateTime( 2011, 4, 01 ) ) ) );
		} // TouchingPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void OverlappingPeriods1Test()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 4, 1 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 00, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 00, 00, 0 ), new DateTime( 2011, 3, 30, 12, 00, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 12, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 2 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 3, 30 ) ) ) );
			Assert.IsTrue( gaps[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 31 ), new DateTime( 2011, 4, 01 ) ) ) );
		} // OverlappingPeriods1Test

		// ----------------------------------------------------------------------
		[Test]
		public void OverlappingPeriods2Test()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 4, 1 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 00, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 00, 00, 0 ), new DateTime( 2011, 3, 30, 06, 30, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 08, 30, 0 ), new DateTime( 2011, 3, 30, 12, 00, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 22, 30, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 2 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 3, 30 ) ) ) );
			Assert.IsTrue( gaps[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 31 ), new DateTime( 2011, 4, 01 ) ) ) );
		} // OverlappingPeriods2Test

		// ----------------------------------------------------------------------
		[Test]
		public void OverlappingPeriods3Test()
		{
			TimeRange limits = new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 4, 1 ) );
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 00, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) ) );
			excludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 30, 00, 00, 0 ), new DateTime( 2011, 3, 31, 00, 00, 0 ) ) );

			ITimePeriodCollection gaps = gapCalculator.GetGaps( excludePeriods, limits );
			Assert.AreEqual( gaps.Count, 2 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 29 ), new DateTime( 2011, 3, 30 ) ) ) );
			Assert.IsTrue( gaps[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 31 ), new DateTime( 2011, 4, 01 ) ) ) );
		} // OverlappingPeriods3Test

		// ----------------------------------------------------------------------
		[Test]
		public void GetGapTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection excludePeriods = new TimePeriodCollection();
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			excludePeriods.AddAll( schoolDay );

			Assert.AreEqual( gapCalculator.GetGaps( excludePeriods ).Count, 0 );
			Assert.AreEqual( gapCalculator.GetGaps( excludePeriods, schoolDay ).Count, 0 );

			excludePeriods.Clear();
			excludePeriods.Add( schoolDay.Lesson1 );
			excludePeriods.Add( schoolDay.Lesson2 );
			excludePeriods.Add( schoolDay.Lesson3 );
			excludePeriods.Add( schoolDay.Lesson4 );

			ITimePeriodCollection gaps2 = gapCalculator.GetGaps( excludePeriods );
			Assert.AreEqual( gaps2.Count, 3 );
			Assert.IsTrue( gaps2[ 0 ].IsSamePeriod( schoolDay.Break1 ) );
			Assert.IsTrue( gaps2[ 1 ].IsSamePeriod( schoolDay.Break2 ) );
			Assert.IsTrue( gaps2[ 2 ].IsSamePeriod( schoolDay.Break3 ) );

			TimeRange testRange3 = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson4.End );
			ITimePeriodCollection gaps3 = gapCalculator.GetGaps( excludePeriods, testRange3 );
			Assert.AreEqual( gaps3.Count, 3 );
			Assert.IsTrue( gaps3[ 0 ].IsSamePeriod( schoolDay.Break1 ) );
			Assert.IsTrue( gaps3[ 1 ].IsSamePeriod( schoolDay.Break2 ) );
			Assert.IsTrue( gaps3[ 2 ].IsSamePeriod( schoolDay.Break3 ) );

			TimeRange testRange4 = new TimeRange( schoolDay.Start.AddHours( -1 ), schoolDay.End.AddHours( 1 ) );
			ITimePeriodCollection gaps4 = gapCalculator.GetGaps( excludePeriods, testRange4 );
			Assert.AreEqual( gaps4.Count, 5 );
			Assert.IsTrue( gaps4[ 0 ].IsSamePeriod( new TimeRange( testRange4.Start, schoolDay.Start ) ) );
			Assert.IsTrue( gaps4[ 1 ].IsSamePeriod( schoolDay.Break1 ) );
			Assert.IsTrue( gaps4[ 2 ].IsSamePeriod( schoolDay.Break2 ) );
			Assert.IsTrue( gaps4[ 3 ].IsSamePeriod( schoolDay.Break3 ) );
			Assert.IsTrue( gaps4[ 4 ].IsSamePeriod( new TimeRange( testRange4.End, testRange3.End ) ) );

			excludePeriods.Clear();
			excludePeriods.Add( schoolDay.Lesson1 );
			ITimePeriodCollection gaps8 = gapCalculator.GetGaps( excludePeriods, schoolDay.Lesson1 );
			Assert.AreEqual( gaps8.Count, 0 );

			TimeRange testRange9 = new TimeRange( schoolDay.Lesson1.Start.Subtract( new TimeSpan( 1 ) ), schoolDay.Lesson1.End.Add( new TimeSpan( 1 ) ) );
			ITimePeriodCollection gaps9 = gapCalculator.GetGaps( excludePeriods, testRange9 );
			Assert.AreEqual( gaps9.Count, 2 );
			Assert.AreEqual( gaps9[ 0 ].Duration, new TimeSpan( 1 ) );
			Assert.AreEqual( gaps9[ 1 ].Duration, new TimeSpan( 1 ) );
		} // GetGapsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CalendarGetGapTest()
		{
			// simmulation of some reservations
			TimePeriodCollection periods = new TimePeriodCollection();
			periods.Add( new Days( 2011, 3, 7, 2 ) );
			periods.Add( new Days( 2011, 3, 16, 2 ) );

			// the overall search range
			CalendarTimeRange limits = new CalendarTimeRange( new DateTime( 2011, 3, 4 ), new DateTime( 2011, 3, 21 ) );
			Days days = new Days( limits.Start, limits.Duration.Days + 1 );
			ITimePeriodCollection dayList = days.GetDays();
			foreach ( Day day in dayList )
			{
				if ( !limits.HasInside( day ) )
				{
					continue; // outside of the search scope
				}
				if ( day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday )
				{
					periods.Add( day ); // // exclude weekend day
				}
			}

			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>( new TimeCalendar() );
			ITimePeriodCollection gaps = gapCalculator.GetGaps( periods, limits );

			Assert.AreEqual( gaps.Count, 4 );
			Assert.IsTrue( gaps[ 0 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 4 ), Duration.Days( 1 ) ) ) );
			Assert.IsTrue( gaps[ 1 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 9 ), Duration.Days( 3 ) ) ) );
			Assert.IsTrue( gaps[ 2 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 14 ), Duration.Days( 2 ) ) ) );
			Assert.IsTrue( gaps[ 3 ].IsSamePeriod( new TimeRange( new DateTime( 2011, 3, 18 ), Duration.Days( 1 ) ) ) );
		} // CalendarGetGapTest

	} // class TimeGapCalculatorTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
