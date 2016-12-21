// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCollectionTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
#if (!PCL)
using System.ComponentModel;
#endif
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class TimePeriodCollectionTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		public TimePeriodCollectionTest()
		{
			startTestData = ClockProxy.Clock.Now;
			endTestData = startTestData.Add( durationTesData );
			timeRangeTestData = new TimeRangePeriodRelationTestData( startTestData, endTestData, offsetTestData );
		} // TimePeriodCollectionTest

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultConstructorTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.Count, 0 );
			Assert.IsFalse( timePeriods.HasStart );
			Assert.IsFalse( timePeriods.HasEnd );
			Assert.IsFalse( timePeriods.IsReadOnly );
		} // DefaultConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyConstructorTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection( timeRangeTestData.AllPeriods );
			Assert.AreEqual( timePeriods.Count, timeRangeTestData.AllPeriods.Count );
			Assert.IsTrue( timePeriods.HasStart );
			Assert.IsTrue( timePeriods.HasEnd );
			Assert.IsFalse( timePeriods.IsReadOnly );
		} // CopyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void CountTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.Count, 0 );

			timePeriods.AddAll( timeRangeTestData.AllPeriods );
			Assert.AreEqual( timePeriods.Count, timeRangeTestData.AllPeriods.Count );

			timePeriods.Clear();
			Assert.AreEqual( timePeriods.Count, 0 );
		} // CountTest

		// ----------------------------------------------------------------------
		[Test]
		public void ItemIndexTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			SchoolDay schoolDay = new SchoolDay();
			timePeriods.AddAll( schoolDay );

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson4 );
		} // ItemIndexTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsAnytimeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.IsTrue( timePeriods.IsAnytime );

			timePeriods.Add( TimeRange.Anytime );
			Assert.IsTrue( timePeriods.IsAnytime );

			timePeriods.Clear();
			Assert.IsTrue( timePeriods.IsAnytime );

			timePeriods.Add( new TimeRange( TimeSpec.MinPeriodDate, now ) );
			Assert.IsFalse( timePeriods.IsAnytime );

			timePeriods.Add( new TimeRange( now, TimeSpec.MaxPeriodDate ) );
			Assert.IsTrue( timePeriods.IsAnytime );

			timePeriods.Clear();
			Assert.IsTrue( timePeriods.IsAnytime );
		} // IsAnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsMomentTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.IsFalse( timePeriods.IsMoment );

			timePeriods.Add( TimeRange.Anytime );
			Assert.IsFalse( timePeriods.IsMoment );

			timePeriods.Clear();
			Assert.IsFalse( timePeriods.IsMoment );

			timePeriods.Add( new TimeRange( now ) );
			Assert.IsTrue( timePeriods.IsMoment );

			timePeriods.Add( new TimeRange( now ) );
			Assert.IsTrue( timePeriods.IsMoment );

			timePeriods.Clear();
			Assert.IsTrue( timePeriods.IsAnytime );
		} // IsMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasStartTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.IsFalse( timePeriods.HasStart );

			timePeriods.Add( new TimeBlock( TimeSpec.MinPeriodDate, Duration.Hour ) );
			Assert.IsFalse( timePeriods.HasStart );

			timePeriods.Clear();
			timePeriods.Add( new TimeBlock( ClockProxy.Clock.Now, Duration.Hour ) );
			Assert.IsTrue( timePeriods.HasStart );
		} // HasStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.Start, TimeSpec.MinPeriodDate );

			timePeriods.Add( new TimeBlock( now, Duration.Hour ) );
			Assert.AreEqual( timePeriods.Start, now );

			timePeriods.Clear();
			Assert.AreEqual( timePeriods.Start, TimeSpec.MinPeriodDate );
		} // StartTest

		// ----------------------------------------------------------------------
		[Test]
		public void StartMoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			timePeriods.Start = now.AddHours( 0 );
			Assert.AreEqual( timePeriods.Start, now );
			timePeriods.Start = now.AddHours( 1 );
			Assert.AreEqual( timePeriods.Start, now.AddHours( 1 ) );
			timePeriods.Start = now.AddHours( -1 );
			Assert.AreEqual( timePeriods.Start, now.AddHours( -1 ) );
		} // StartMoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void SortByTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			// start
			timePeriods.Add( schoolDay.Lesson4 );
			timePeriods.Add( schoolDay.Break3 );
			timePeriods.Add( schoolDay.Lesson3 );
			timePeriods.Add( schoolDay.Break2 );
			timePeriods.Add( schoolDay.Lesson2 );
			timePeriods.Add( schoolDay.Break1 );
			timePeriods.Add( schoolDay.Lesson1 );

			timePeriods.SortBy( TimePeriodStartComparer.Comparer );

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson4 );

			timePeriods.SortReverseBy( TimePeriodStartComparer.Comparer );

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson1 );

			// end
			timePeriods.Clear();
			timePeriods.AddAll( schoolDay );

			timePeriods.SortReverseBy( TimePeriodEndComparer.Comparer );

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson1 );

			timePeriods.SortBy( TimePeriodEndComparer.Comparer );

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson4 );

			// duration
			timePeriods.Clear();
			TimeSpan oneHour = new TimeSpan( 1, 0, 0 );
			TimeSpan twoHours = new TimeSpan( 2, 0, 0 );
			TimeSpan threeHours = new TimeSpan( 3, 0, 0 );
			TimeSpan fourHours = new TimeSpan( 4, 0, 0 );
			timePeriods.Add( new TimeRange( now, oneHour ) );
			timePeriods.Add( new TimeRange( now, twoHours ) );
			timePeriods.Add( new TimeRange( now, threeHours ) );
			timePeriods.Add( new TimeRange( now, fourHours ) );

			timePeriods.SortReverseBy( TimePeriodDurationComparer.Comparer );

			Assert.AreEqual( fourHours, timePeriods[ 0 ].Duration );
			Assert.AreEqual( threeHours, timePeriods[ 1 ].Duration );
			Assert.AreEqual( twoHours, timePeriods[ 2 ].Duration );
			Assert.AreEqual( oneHour, timePeriods[ 3 ].Duration );

			timePeriods.SortBy( TimePeriodDurationComparer.Comparer );

			Assert.AreEqual( oneHour, timePeriods[ 0 ].Duration );
			Assert.AreEqual( twoHours, timePeriods[ 1 ].Duration );
			Assert.AreEqual( threeHours, timePeriods[ 2 ].Duration );
			Assert.AreEqual( fourHours, timePeriods[ 3 ].Duration );
		} // SortByTest

		// ----------------------------------------------------------------------
		[Test]
		public void SortByStartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			timePeriods.Add( schoolDay.Lesson4 );
			timePeriods.Add( schoolDay.Break3 );
			timePeriods.Add( schoolDay.Lesson3 );
			timePeriods.Add( schoolDay.Break2 );
			timePeriods.Add( schoolDay.Lesson2 );
			timePeriods.Add( schoolDay.Break1 );
			timePeriods.Add( schoolDay.Lesson1 );

			timePeriods.SortByStart( ListSortDirection.Descending );

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson1 );

			timePeriods.SortByStart();

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson4 );
		} // SortByStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void SortByEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			timePeriods.AddAll( schoolDay );

			timePeriods.SortByEnd( ListSortDirection.Descending );

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson1 );

			timePeriods.SortByEnd();

			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ 6 ], schoolDay.Lesson4 );
		} // SortByEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void SortByDurationTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			TimeSpan oneHour = new TimeSpan( 1, 0, 0 );
			TimeSpan twoHours = new TimeSpan( 2, 0, 0 );
			TimeSpan threeHours = new TimeSpan( 3, 0, 0 );
			TimeSpan fourHours = new TimeSpan( 4, 0, 0 );
			timePeriods.Add( new TimeRange( now, oneHour ) );
			timePeriods.Add( new TimeRange( now, twoHours ) );
			timePeriods.Add( new TimeRange( now, threeHours ) );
			timePeriods.Add( new TimeRange( now, fourHours ) );

			timePeriods.SortByDuration( ListSortDirection.Descending );

			Assert.AreEqual( fourHours, timePeriods[ 0 ].Duration );
			Assert.AreEqual( threeHours, timePeriods[ 1 ].Duration );
			Assert.AreEqual( twoHours, timePeriods[ 2 ].Duration );
			Assert.AreEqual( oneHour, timePeriods[ 3 ].Duration );

			timePeriods.SortByDuration();

			Assert.AreEqual( oneHour, timePeriods[ 0 ].Duration );
			Assert.AreEqual( twoHours, timePeriods[ 1 ].Duration );
			Assert.AreEqual( threeHours, timePeriods[ 2 ].Duration );
			Assert.AreEqual( fourHours, timePeriods[ 3 ].Duration );
		} // SortByDurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void InsidePeriodsTimePeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange timeRange1 = new TimeRange( new DateTime( now.Year, now.Month, 8 ), new DateTime( now.Year, now.Month, 18 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( now.Year, now.Month, 10 ), new DateTime( now.Year, now.Month, 11 ) );
			TimeRange timeRange3 = new TimeRange( new DateTime( now.Year, now.Month, 13 ), new DateTime( now.Year, now.Month, 15 ) );
			TimeRange timeRange4 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 13 ) );
			TimeRange timeRange5 = new TimeRange( new DateTime( now.Year, now.Month, 15 ), new DateTime( now.Year, now.Month, 17 ) );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( timeRange1 );
			timePeriods.Add( timeRange2 );
			timePeriods.Add( timeRange3 );
			timePeriods.Add( timeRange4 );
			timePeriods.Add( timeRange5 );

			Assert.AreEqual( timePeriods.InsidePeriods( timeRange1 ).Count, 5 );
			Assert.AreEqual( timePeriods.InsidePeriods( timeRange2 ).Count, 1 );
			Assert.AreEqual( timePeriods.InsidePeriods( timeRange3 ).Count, 1 );
			Assert.AreEqual( timePeriods.InsidePeriods( timeRange4 ).Count, 2 );
			Assert.AreEqual( timePeriods.InsidePeriods( timeRange5 ).Count, 1 );

			ITimeRange test1 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ).Negate() );
			ITimePeriodCollection insidePeriods1 = timePeriods.InsidePeriods( test1 );
			Assert.AreEqual( insidePeriods1.Count, 0 );

			ITimeRange test2 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ) );
			ITimePeriodCollection insidePeriods2 = timePeriods.InsidePeriods( test2 );
			Assert.AreEqual( insidePeriods2.Count, 0 );

			TimeRange test3 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 11 ) );
			ITimePeriodCollection insidePeriods3 = timePeriods.InsidePeriods( test3 );
			Assert.AreEqual( insidePeriods3.Count, 1 );

			TimeRange test4 = new TimeRange( new DateTime( now.Year, now.Month, 14 ), new DateTime( now.Year, now.Month, 17 ) );
			ITimePeriodCollection insidePeriods4 = timePeriods.InsidePeriods( test4 );
			Assert.AreEqual( insidePeriods4.Count, 1 );
		} // InsidePeriodsTimePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void OverlapPeriodsTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange timeRange1 = new TimeRange( new DateTime( now.Year, now.Month, 8 ), new DateTime( now.Year, now.Month, 18 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( now.Year, now.Month, 10 ), new DateTime( now.Year, now.Month, 11 ) );
			TimeRange timeRange3 = new TimeRange( new DateTime( now.Year, now.Month, 13 ), new DateTime( now.Year, now.Month, 15 ) );
			TimeRange timeRange4 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 13 ) );
			TimeRange timeRange5 = new TimeRange( new DateTime( now.Year, now.Month, 15 ), new DateTime( now.Year, now.Month, 17 ) );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( timeRange1 );
			timePeriods.Add( timeRange2 );
			timePeriods.Add( timeRange3 );
			timePeriods.Add( timeRange4 );
			timePeriods.Add( timeRange5 );

			Assert.AreEqual( timePeriods.OverlapPeriods( timeRange1 ).Count, 5 );
			Assert.AreEqual( timePeriods.OverlapPeriods( timeRange2 ).Count, 3 );
			Assert.AreEqual( timePeriods.OverlapPeriods( timeRange3 ).Count, 2 );
			Assert.AreEqual( timePeriods.OverlapPeriods( timeRange4 ).Count, 3 );
			Assert.AreEqual( timePeriods.OverlapPeriods( timeRange5 ).Count, 2 );

			ITimeRange test1 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ).Negate() );
			ITimePeriodCollection insidePeriods1 = timePeriods.OverlapPeriods( test1 );
			Assert.AreEqual( insidePeriods1.Count, 0 );

			ITimeRange test2 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ) );
			ITimePeriodCollection insidePeriods2 = timePeriods.OverlapPeriods( test2 );
			Assert.AreEqual( insidePeriods2.Count, 0 );

			TimeRange test3 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 11 ) );
			ITimePeriodCollection insidePeriods3 = timePeriods.OverlapPeriods( test3 );
			Assert.AreEqual( insidePeriods3.Count, 3 );

			TimeRange test4 = new TimeRange( new DateTime( now.Year, now.Month, 14 ), new DateTime( now.Year, now.Month, 17 ) );
			ITimePeriodCollection insidePeriods4 = timePeriods.OverlapPeriods( test4 );
			Assert.AreEqual( insidePeriods4.Count, 3 );
		} // OverlapPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectionPeriodsDateTimeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange timeRange1 = new TimeRange( new DateTime( now.Year, now.Month, 8 ), new DateTime( now.Year, now.Month, 18 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( now.Year, now.Month, 10 ), new DateTime( now.Year, now.Month, 11 ) );
			TimeRange timeRange3 = new TimeRange( new DateTime( now.Year, now.Month, 13 ), new DateTime( now.Year, now.Month, 15 ) );
			TimeRange timeRange4 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 14 ) );
			TimeRange timeRange5 = new TimeRange( new DateTime( now.Year, now.Month, 16 ), new DateTime( now.Year, now.Month, 17 ) );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( timeRange1 );
			timePeriods.Add( timeRange2 );
			timePeriods.Add( timeRange3 );
			timePeriods.Add( timeRange4 );
			timePeriods.Add( timeRange5 );

			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange1.Start ).Count, 1 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange1.End ).Count, 1 );

			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange2.Start ).Count, 3 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange2.End ).Count, 3 );

			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange3.Start ).Count, 3 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange3.End ).Count, 2 );

			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange4.Start ).Count, 2 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange4.End ).Count, 3 );

			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange5.Start ).Count, 2 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange5.End ).Count, 2 );

			DateTime test1 = timeRange1.Start.AddMilliseconds( -1 );
			ITimePeriodCollection insidePeriods1 = timePeriods.IntersectionPeriods( test1 );
			Assert.AreEqual( insidePeriods1.Count, 0 );

			DateTime test2 = timeRange1.End.AddMilliseconds( 1 );
			ITimePeriodCollection insidePeriods2 = timePeriods.IntersectionPeriods( test2 );
			Assert.AreEqual( insidePeriods2.Count, 0 );

			DateTime test3 = new DateTime( now.Year, now.Month, 12 );
			ITimePeriodCollection insidePeriods3 = timePeriods.IntersectionPeriods( test3 );
			Assert.AreEqual( insidePeriods3.Count, 2 );

			DateTime test4 = new DateTime( now.Year, now.Month, 14 );
			ITimePeriodCollection insidePeriods4 = timePeriods.IntersectionPeriods( test4 );
			Assert.AreEqual( insidePeriods4.Count, 3 );
		} // IntersectionPeriodsDateTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void RelationPeriodsTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange timeRange1 = new TimeRange( new DateTime( now.Year, now.Month, 8 ), new DateTime( now.Year, now.Month, 18 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( now.Year, now.Month, 10 ), new DateTime( now.Year, now.Month, 11 ) );
			TimeRange timeRange3 = new TimeRange( new DateTime( now.Year, now.Month, 13 ), new DateTime( now.Year, now.Month, 15 ) );
			TimeRange timeRange4 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 14 ) );
			TimeRange timeRange5 = new TimeRange( new DateTime( now.Year, now.Month, 16 ), new DateTime( now.Year, now.Month, 17 ) );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( timeRange1 );
			timePeriods.Add( timeRange2 );
			timePeriods.Add( timeRange3 );
			timePeriods.Add( timeRange4 );
			timePeriods.Add( timeRange5 );

			Assert.AreEqual( timePeriods.RelationPeriods( timeRange1, PeriodRelation.ExactMatch ).Count, 1 );
			Assert.AreEqual( timePeriods.RelationPeriods( timeRange2, PeriodRelation.ExactMatch ).Count, 1 );
			Assert.AreEqual( timePeriods.RelationPeriods( timeRange3, PeriodRelation.ExactMatch ).Count, 1 );
			Assert.AreEqual( timePeriods.RelationPeriods( timeRange4, PeriodRelation.ExactMatch ).Count, 1 );
			Assert.AreEqual( timePeriods.RelationPeriods( timeRange5, PeriodRelation.ExactMatch ).Count, 1 );

			// all
			Assert.AreEqual( timePeriods.RelationPeriods( new TimeRange( new DateTime( now.Year, now.Month, 7 ), new DateTime( now.Year, now.Month, 19 ) ), PeriodRelation.Enclosing ).Count, 5 );

			// timerange3
			Assert.AreEqual( timePeriods.RelationPeriods( new TimeRange( new DateTime( now.Year, now.Month, 11 ), new DateTime( now.Year, now.Month, 16 ) ), PeriodRelation.Enclosing ).Count, 1 );
		} // RelationPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectionPeriodsTimePeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange timeRange1 = new TimeRange( new DateTime( now.Year, now.Month, 8 ), new DateTime( now.Year, now.Month, 18 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( now.Year, now.Month, 10 ), new DateTime( now.Year, now.Month, 11 ) );
			TimeRange timeRange3 = new TimeRange( new DateTime( now.Year, now.Month, 13 ), new DateTime( now.Year, now.Month, 15 ) );
			TimeRange timeRange4 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 13 ) );
			TimeRange timeRange5 = new TimeRange( new DateTime( now.Year, now.Month, 15 ), new DateTime( now.Year, now.Month, 17 ) );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( timeRange1 );
			timePeriods.Add( timeRange2 );
			timePeriods.Add( timeRange3 );
			timePeriods.Add( timeRange4 );
			timePeriods.Add( timeRange5 );

			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange1 ).Count, 5 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange2 ).Count, 3 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange3 ).Count, 4 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange4 ).Count, 4 );
			Assert.AreEqual( timePeriods.IntersectionPeriods( timeRange5 ).Count, 3 );

			ITimeRange test1 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ).Negate() );
			ITimePeriodCollection insidePeriods1 = timePeriods.IntersectionPeriods( test1 );
			Assert.AreEqual( insidePeriods1.Count, 0 );

			ITimeRange test2 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ) );
			ITimePeriodCollection insidePeriods2 = timePeriods.IntersectionPeriods( test2 );
			Assert.AreEqual( insidePeriods2.Count, 0 );

			TimeRange test3 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 11 ) );
			ITimePeriodCollection insidePeriods3 = timePeriods.IntersectionPeriods( test3 );
			Assert.AreEqual( insidePeriods3.Count, 3 );

			TimeRange test4 = new TimeRange( new DateTime( now.Year, now.Month, 14 ), new DateTime( now.Year, now.Month, 17 ) );
			ITimePeriodCollection insidePeriods4 = timePeriods.IntersectionPeriods( test4 );
			Assert.AreEqual( insidePeriods4.Count, 3 );
		} // IntersectionPeriodsTimePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.IsFalse( timePeriods.HasEnd );

			timePeriods.Add( new TimeBlock( Duration.Hour, TimeSpec.MaxPeriodDate ) );
			Assert.IsFalse( timePeriods.HasEnd );

			timePeriods.Clear();
			timePeriods.Add( new TimeBlock( now, Duration.Hour ) );
			Assert.IsTrue( timePeriods.HasEnd );
		} // HasEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void EndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.End, TimeSpec.MaxPeriodDate );

			timePeriods.Add( new TimeBlock( Duration.Hour, now ) );
			Assert.AreEqual( timePeriods.End, now );

			timePeriods.Clear();
			Assert.AreEqual( timePeriods.End, TimeSpec.MaxPeriodDate );
		} // EndTest

		// ----------------------------------------------------------------------
		[Test]
		public void EndMoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			DateTime end = schoolDay.End;
			timePeriods.End = end.AddHours( 0 );
			Assert.AreEqual( timePeriods.End, end );
			timePeriods.End = end.AddHours( 1 );
			Assert.AreEqual( timePeriods.End, end.AddHours( 1 ) );
			timePeriods.End = end.AddHours( -1 );
			Assert.AreEqual( timePeriods.End, end.AddHours( -1 ) );
		} // EndMoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.Duration, TimeSpec.MaxPeriodDuration );

			TimeSpan duration = Duration.Hour;
			timePeriods.Add( new TimeBlock( ClockProxy.Clock.Now, duration ) );
			Assert.AreEqual( timePeriods.Duration, duration );
		} // DurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void TotalDurationTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange timeRange1 = new TimeRange( new DateTime( now.Year, now.Month, 8 ), new DateTime( now.Year, now.Month, 18 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( now.Year, now.Month, 10 ), new DateTime( now.Year, now.Month, 11 ) );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.TotalDuration, TimeSpan.Zero );

			timePeriods.Add( timeRange1 );
			Assert.AreEqual( timePeriods.TotalDuration, timeRange1.End.Subtract( timeRange1.Start ) );
			Assert.AreEqual( timePeriods.TotalDuration, timeRange1.Duration );

			timePeriods.Add( timeRange2 );
			Assert.AreEqual( timePeriods.TotalDuration, timeRange1.End.Subtract( timeRange1.Start ).
				Add( timeRange2.End.Subtract( timeRange2.Start ) ) );
			Assert.AreEqual( timePeriods.TotalDuration, timeRange1.Duration.Add( timeRange2.Duration ) );
		} // TotalDurationTest

		/*
		// ----------------------------------------------------------------------
		[Test]
		public void TotalDaylightDurationTest()
		{
			DateTime dstStart = new DateTime( 2014, 3, 30, 2, 0, 0 ).AddHours( 1 );
			DateTime dstEnd = new DateTime( 2014, 10, 26, 3, 0, 0 ).AddHours( -1 );
			TimeRange timeRange1 = new TimeRange( dstStart.AddHours( -2 ), dstStart.AddHours( 2 ) );
			TimeRange timeRange2 = new TimeRange( dstEnd.AddHours( -2 ), dstEnd.AddHours( 2 ) );

			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById( "Central Europe Standard Time" );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.GetTotalDaylightDuration( timeZone ), TimeSpan.Zero );

			timePeriods.Add( timeRange1 );
			Assert.AreEqual( timePeriods.GetTotalDaylightDuration( timeZone ), timeRange1.GetDaylightDuration( timeZone ) );

			timePeriods.Add( timeRange2 );
			Assert.AreEqual( timePeriods.GetTotalDaylightDuration( timeZone ), timeRange1.GetDaylightDuration( timeZone ).
				Add( timeRange2.GetDaylightDuration( timeZone ) ) );
		} // TotalDaylightDurationTest
		*/

		// ----------------------------------------------------------------------
		[Test]
		public void MoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			DateTime startDate = schoolDay.Start;
			DateTime endDate = schoolDay.End;
			TimeSpan startDuration = timePeriods.Duration;

			TimeSpan duration = Duration.Hour;
			timePeriods.Move( duration );

			Assert.AreEqual( timePeriods.Start, startDate.Add( duration ) );
			Assert.AreEqual( timePeriods.End, endDate.Add( duration ) );
			Assert.AreEqual( timePeriods.Duration, startDuration );
		} // MoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.Count, 0 );

			timePeriods.Add( new TimeRange() );
			Assert.AreEqual( timePeriods.Count, 1 );

			timePeriods.Add( new TimeRange() );
			Assert.AreEqual( timePeriods.Count, 2 );

			timePeriods.Clear();
			Assert.AreEqual( timePeriods.Count, 0 );
		} // AddTest

		// ----------------------------------------------------------------------
		[Test]
		public void ContainsPeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );
			Assert.IsFalse( timePeriods.Contains( timeRange ) );
			Assert.IsTrue( timePeriods.ContainsPeriod( timeRange ) );

			timePeriods.Add( timeRange );
			Assert.IsTrue( timePeriods.Contains( timeRange ) );
			Assert.IsTrue( timePeriods.ContainsPeriod( timeRange ) );
		} // ContainsPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddAllTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.AreEqual( timePeriods.Count, 0 );

			timePeriods.AddAll( schoolDay );
			Assert.AreEqual( timePeriods.Count, schoolDay.Count );

			timePeriods.Clear();
			Assert.AreEqual( timePeriods.Count, 0 );
		} // AddAllTest

		// ----------------------------------------------------------------------
		[Test]
		public void InsertTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.Count, 0 );

			timePeriods.Add( schoolDay.Lesson1 );
			Assert.AreEqual( timePeriods.Count, 1 );
			timePeriods.Add( schoolDay.Lesson3 );
			Assert.AreEqual( timePeriods.Count, 2 );
			timePeriods.Add( schoolDay.Lesson4 );
			Assert.AreEqual( timePeriods.Count, 3 );

			// between
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Lesson3 );
			timePeriods.Insert( 1, schoolDay.Lesson2 );
			Assert.AreEqual( timePeriods[ 1 ], schoolDay.Lesson2 );

			// first
			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson1 );
			timePeriods.Insert( 0, schoolDay.Break1 );
			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Break1 );

			// last
			Assert.AreEqual( timePeriods[ timePeriods.Count - 1 ], schoolDay.Lesson4 );
			timePeriods.Insert( timePeriods.Count, schoolDay.Break3 );
			Assert.AreEqual( timePeriods[ timePeriods.Count - 1 ], schoolDay.Break3 );
		} // InsertTest

		// ----------------------------------------------------------------------
		[Test]
		public void ContainsTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.IsFalse( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Add( schoolDay.Lesson1 );
			Assert.IsTrue( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Remove( schoolDay.Lesson1 );
			Assert.IsFalse( timePeriods.Contains( schoolDay.Lesson1 ) );
		} // ContainsTest

		// ----------------------------------------------------------------------
		[Test]
		public void IndexOfTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.AreEqual( timePeriods.IndexOf( new TimeRange() ), -1 );
			Assert.AreEqual( timePeriods.IndexOf( new TimeBlock() ), -1 );

			timePeriods.AddAll( schoolDay );

			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Lesson1 ), 0 );
			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Break1 ), 1 );
			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Lesson2 ), 2 );
			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Break2 ), 3 );
			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Lesson3 ), 4 );
			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Break3 ), 5 );
			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Lesson4 ), 6 );

			timePeriods.Remove( schoolDay.Lesson1 );
			Assert.AreEqual( timePeriods.IndexOf( schoolDay.Lesson1 ), -1 );
		} // IndexOfTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyToTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			ITimePeriod[] array = new ITimePeriod[ schoolDay.Count ];
			timePeriods.CopyTo( array, 0 );
			Assert.AreEqual( array[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( array[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( array[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( array[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( array[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( array[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( array[ 6 ], schoolDay.Lesson4 );
		} // CopyToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ClearTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.AreEqual( timePeriods.Count, 0 );
			timePeriods.Clear();
			Assert.AreEqual( timePeriods.Count, 0 );

			timePeriods.AddAll( new SchoolDay() );
			Assert.AreEqual( timePeriods.Count, 7 );
			timePeriods.Clear();
			Assert.AreEqual( timePeriods.Count, 0 );
		} // ClearTest

		// ----------------------------------------------------------------------
		[Test]
		public void RemoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.IsFalse( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Add( schoolDay.Lesson1 );
			Assert.IsTrue( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Remove( schoolDay.Lesson1 );
			Assert.IsFalse( timePeriods.Contains( schoolDay.Lesson1 ) );
		} // RemoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void RemoveAtTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			// inside
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Lesson2 );
			timePeriods.RemoveAt( 2 );
			Assert.AreEqual( timePeriods[ 2 ], schoolDay.Break2 );

			// first
			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Lesson1 );
			timePeriods.RemoveAt( 0 );
			Assert.AreEqual( timePeriods[ 0 ], schoolDay.Break1 );

			// last
			Assert.AreEqual( timePeriods[ timePeriods.Count - 1 ], schoolDay.Lesson4 );
			timePeriods.RemoveAt( timePeriods.Count - 1 );
			Assert.AreEqual( timePeriods[ timePeriods.Count - 1 ], schoolDay.Break3 );
		} // RemoveAtTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSamePeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			Assert.IsTrue( timePeriods.IsSamePeriod( timePeriods ) );
			Assert.IsTrue( timePeriods.IsSamePeriod( schoolDay ) );

			Assert.IsTrue( schoolDay.IsSamePeriod( schoolDay ) );
			Assert.IsTrue( schoolDay.IsSamePeriod( timePeriods ) );

			Assert.IsFalse( timePeriods.IsSamePeriod( TimeBlock.Anytime ) );
			Assert.IsFalse( schoolDay.IsSamePeriod( TimeBlock.Anytime ) );

			timePeriods.RemoveAt( 0 );
			Assert.IsFalse( timePeriods.IsSamePeriod( schoolDay ) );
		} // IsSamePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsideTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.IsFalse( timePeriods.HasInside( testData.Before ) );
			Assert.IsFalse( timePeriods.HasInside( testData.StartTouching ) );
			Assert.IsFalse( timePeriods.HasInside( testData.StartInside ) );
			Assert.IsFalse( timePeriods.HasInside( testData.InsideStartTouching ) );
			Assert.IsTrue( timePeriods.HasInside( testData.EnclosingStartTouching ) );
			Assert.IsTrue( timePeriods.HasInside( testData.Enclosing ) );
			Assert.IsTrue( timePeriods.HasInside( testData.EnclosingEndTouching ) );
			Assert.IsTrue( timePeriods.HasInside( testData.ExactMatch ) );
			Assert.IsFalse( timePeriods.HasInside( testData.Inside ) );
			Assert.IsFalse( timePeriods.HasInside( testData.InsideEndTouching ) );
			Assert.IsFalse( timePeriods.HasInside( testData.EndInside ) );
			Assert.IsFalse( timePeriods.HasInside( testData.EndTouching ) );
			Assert.IsFalse( timePeriods.HasInside( testData.After ) );
		} // HasInsideTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectsWithTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.IsFalse( timePeriods.IntersectsWith( testData.Before ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.StartTouching ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.StartInside ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.InsideStartTouching ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.EnclosingStartTouching ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.Enclosing ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.EnclosingEndTouching ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.ExactMatch ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.Inside ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.InsideEndTouching ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.EndInside ) );
			Assert.IsTrue( timePeriods.IntersectsWith( testData.EndTouching ) );
			Assert.IsFalse( timePeriods.IntersectsWith( testData.After ) );
		} // IntersectsWithTest

		// ----------------------------------------------------------------------
		[Test]
		public void OverlapsWithTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.IsFalse( timePeriods.OverlapsWith( testData.Before ) );
			Assert.IsFalse( timePeriods.OverlapsWith( testData.StartTouching ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.StartInside ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.InsideStartTouching ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.EnclosingStartTouching ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.Enclosing ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.EnclosingEndTouching ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.ExactMatch ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.Inside ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.InsideEndTouching ) );
			Assert.IsTrue( timePeriods.OverlapsWith( testData.EndInside ) );
			Assert.IsFalse( timePeriods.OverlapsWith( testData.EndTouching ) );
			Assert.IsFalse( timePeriods.OverlapsWith( testData.After ) );
		} // OverlapsWithTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetRelationTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.AreEqual( timePeriods.GetRelation( testData.Before ), PeriodRelation.Before );
			Assert.AreEqual( timePeriods.GetRelation( testData.StartTouching ), PeriodRelation.StartTouching );
			Assert.AreEqual( timePeriods.GetRelation( testData.StartInside ), PeriodRelation.StartInside );
			Assert.AreEqual( timePeriods.GetRelation( testData.InsideStartTouching ), PeriodRelation.InsideStartTouching );
			Assert.AreEqual( timePeriods.GetRelation( testData.EnclosingStartTouching ), PeriodRelation.EnclosingStartTouching );
			Assert.AreEqual( timePeriods.GetRelation( testData.Enclosing ), PeriodRelation.Enclosing );
			Assert.AreEqual( timePeriods.GetRelation( testData.EnclosingEndTouching ), PeriodRelation.EnclosingEndTouching );
			Assert.AreEqual( timePeriods.GetRelation( testData.ExactMatch ), PeriodRelation.ExactMatch );
			Assert.AreEqual( timePeriods.GetRelation( testData.Inside ), PeriodRelation.Inside );
			Assert.AreEqual( timePeriods.GetRelation( testData.InsideEndTouching ), PeriodRelation.InsideEndTouching );
			Assert.AreEqual( timePeriods.GetRelation( testData.EndInside ), PeriodRelation.EndInside );
			Assert.AreEqual( timePeriods.GetRelation( testData.EndTouching ), PeriodRelation.EndTouching );
			Assert.AreEqual( timePeriods.GetRelation( testData.After ), PeriodRelation.After );
		} // GetRelationTest

		// ----------------------------------------------------------------------
		// members
		private readonly TimeSpan durationTesData = Duration.Hour;
		private readonly DateTime startTestData;
		private readonly DateTime endTestData;
		private readonly TimeSpan offsetTestData = Duration.Millisecond;
		private readonly TimeRangePeriodRelationTestData timeRangeTestData;

	} // class TimePeriodCollectionTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
