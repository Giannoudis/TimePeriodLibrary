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
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
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
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void DefaultConstructorTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal(0, timePeriods.Count);
			Assert.False( timePeriods.HasStart );
			Assert.False( timePeriods.HasEnd );
			Assert.False( timePeriods.IsReadOnly );
		} // DefaultConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void CopyConstructorTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection( timeRangeTestData.AllPeriods );
			Assert.Equal( timePeriods.Count, timeRangeTestData.AllPeriods.Count );
			Assert.True( timePeriods.HasStart );
			Assert.True( timePeriods.HasEnd );
			Assert.False( timePeriods.IsReadOnly );
		} // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void CountTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal(0, timePeriods.Count);

			timePeriods.AddAll( timeRangeTestData.AllPeriods );
			Assert.Equal( timePeriods.Count, timeRangeTestData.AllPeriods.Count );

			timePeriods.Clear();
			Assert.Equal(0, timePeriods.Count);
		} // CountTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void ItemIndexTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			SchoolDay schoolDay = new SchoolDay();
			timePeriods.AddAll( schoolDay );

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson4 );
		} // ItemIndexTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void IsAnytimeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.True( timePeriods.IsAnytime );

			timePeriods.Add( TimeRange.Anytime );
			Assert.True( timePeriods.IsAnytime );

			timePeriods.Clear();
			Assert.True( timePeriods.IsAnytime );

			timePeriods.Add( new TimeRange( TimeSpec.MinPeriodDate, now ) );
			Assert.False( timePeriods.IsAnytime );

			timePeriods.Add( new TimeRange( now, TimeSpec.MaxPeriodDate ) );
			Assert.True( timePeriods.IsAnytime );

			timePeriods.Clear();
			Assert.True( timePeriods.IsAnytime );
		} // IsAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void IsMomentTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.False( timePeriods.IsMoment );

			timePeriods.Add( TimeRange.Anytime );
			Assert.False( timePeriods.IsMoment );

			timePeriods.Clear();
			Assert.False( timePeriods.IsMoment );

			timePeriods.Add( new TimeRange( now ) );
			Assert.True( timePeriods.IsMoment );

			timePeriods.Add( new TimeRange( now ) );
			Assert.True( timePeriods.IsMoment );

			timePeriods.Clear();
			Assert.True( timePeriods.IsAnytime );
		} // IsMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void HasStartTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.False( timePeriods.HasStart );

			timePeriods.Add( new TimeBlock( TimeSpec.MinPeriodDate, Duration.Hour ) );
			Assert.False( timePeriods.HasStart );

			timePeriods.Clear();
			timePeriods.Add( new TimeBlock( ClockProxy.Clock.Now, Duration.Hour ) );
			Assert.True( timePeriods.HasStart );
		} // HasStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void StartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal( timePeriods.Start, TimeSpec.MinPeriodDate );

			timePeriods.Add( new TimeBlock( now, Duration.Hour ) );
			Assert.Equal( timePeriods.Start, now );

			timePeriods.Clear();
			Assert.Equal( timePeriods.Start, TimeSpec.MinPeriodDate );
		} // StartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void StartMoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			timePeriods.Start = now.AddHours( 0 );
			Assert.Equal( timePeriods.Start, now );
			timePeriods.Start = now.AddHours( 1 );
			Assert.Equal( timePeriods.Start, now.AddHours( 1 ) );
			timePeriods.Start = now.AddHours( -1 );
			Assert.Equal( timePeriods.Start, now.AddHours( -1 ) );
		} // StartMoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson4 );

			timePeriods.SortReverseBy( TimePeriodStartComparer.Comparer );

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson1 );

			// end
			timePeriods.Clear();
			timePeriods.AddAll( schoolDay );

			timePeriods.SortReverseBy( TimePeriodEndComparer.Comparer );

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson1 );

			timePeriods.SortBy( TimePeriodEndComparer.Comparer );

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson4 );

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

			Assert.Equal( fourHours, timePeriods[ 0 ].Duration );
			Assert.Equal( threeHours, timePeriods[ 1 ].Duration );
			Assert.Equal( twoHours, timePeriods[ 2 ].Duration );
			Assert.Equal( oneHour, timePeriods[ 3 ].Duration );

			timePeriods.SortBy( TimePeriodDurationComparer.Comparer );

			Assert.Equal( oneHour, timePeriods[ 0 ].Duration );
			Assert.Equal( twoHours, timePeriods[ 1 ].Duration );
			Assert.Equal( threeHours, timePeriods[ 2 ].Duration );
			Assert.Equal( fourHours, timePeriods[ 3 ].Duration );
		} // SortByTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson1 );

			timePeriods.SortByStart();

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson4 );
		} // SortByStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void SortByEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			timePeriods.AddAll( schoolDay );

			timePeriods.SortByEnd( ListSortDirection.Descending );

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson4 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson1 );

			timePeriods.SortByEnd();

			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Break1 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 3 ], schoolDay.Break2 );
			Assert.Equal( timePeriods[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( timePeriods[ 5 ], schoolDay.Break3 );
			Assert.Equal( timePeriods[ 6 ], schoolDay.Lesson4 );
		} // SortByEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal( fourHours, timePeriods[ 0 ].Duration );
			Assert.Equal( threeHours, timePeriods[ 1 ].Duration );
			Assert.Equal( twoHours, timePeriods[ 2 ].Duration );
			Assert.Equal( oneHour, timePeriods[ 3 ].Duration );

			timePeriods.SortByDuration();

			Assert.Equal( oneHour, timePeriods[ 0 ].Duration );
			Assert.Equal( twoHours, timePeriods[ 1 ].Duration );
			Assert.Equal( threeHours, timePeriods[ 2 ].Duration );
			Assert.Equal( fourHours, timePeriods[ 3 ].Duration );
		} // SortByDurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal(5, timePeriods.InsidePeriods( timeRange1 ).Count);
			Assert.Equal(1, timePeriods.InsidePeriods( timeRange2 ).Count);
			Assert.Equal(1, timePeriods.InsidePeriods( timeRange3 ).Count);
			Assert.Equal(2, timePeriods.InsidePeriods( timeRange4 ).Count);
			Assert.Equal(1, timePeriods.InsidePeriods( timeRange5 ).Count);

			ITimeRange test1 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ).Negate() );
			ITimePeriodCollection insidePeriods1 = timePeriods.InsidePeriods( test1 );
			Assert.Equal(0, insidePeriods1.Count);

			ITimeRange test2 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ) );
			ITimePeriodCollection insidePeriods2 = timePeriods.InsidePeriods( test2 );
			Assert.Equal(0, insidePeriods2.Count);

			TimeRange test3 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 11 ) );
			ITimePeriodCollection insidePeriods3 = timePeriods.InsidePeriods( test3 );
			Assert.Equal(1, insidePeriods3.Count);

			TimeRange test4 = new TimeRange( new DateTime( now.Year, now.Month, 14 ), new DateTime( now.Year, now.Month, 17 ) );
			ITimePeriodCollection insidePeriods4 = timePeriods.InsidePeriods( test4 );
			Assert.Equal(1, insidePeriods4.Count);
		} // InsidePeriodsTimePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal(5, timePeriods.OverlapPeriods( timeRange1 ).Count);
			Assert.Equal(3, timePeriods.OverlapPeriods( timeRange2 ).Count);
			Assert.Equal(2, timePeriods.OverlapPeriods( timeRange3 ).Count);
			Assert.Equal(3, timePeriods.OverlapPeriods( timeRange4 ).Count);
			Assert.Equal(2, timePeriods.OverlapPeriods( timeRange5 ).Count);

			ITimeRange test1 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ).Negate() );
			ITimePeriodCollection insidePeriods1 = timePeriods.OverlapPeriods( test1 );
			Assert.Equal(0, insidePeriods1.Count);

			ITimeRange test2 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ) );
			ITimePeriodCollection insidePeriods2 = timePeriods.OverlapPeriods( test2 );
			Assert.Equal(0, insidePeriods2.Count);

			TimeRange test3 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 11 ) );
			ITimePeriodCollection insidePeriods3 = timePeriods.OverlapPeriods( test3 );
			Assert.Equal(3, insidePeriods3.Count);

			TimeRange test4 = new TimeRange( new DateTime( now.Year, now.Month, 14 ), new DateTime( now.Year, now.Month, 17 ) );
			ITimePeriodCollection insidePeriods4 = timePeriods.OverlapPeriods( test4 );
			Assert.Equal(3, insidePeriods4.Count);
		} // OverlapPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal(1, timePeriods.IntersectionPeriods( timeRange1.Start ).Count);
			Assert.Equal(1, timePeriods.IntersectionPeriods( timeRange1.End ).Count);

			Assert.Equal(3, timePeriods.IntersectionPeriods( timeRange2.Start ).Count);
			Assert.Equal(3, timePeriods.IntersectionPeriods( timeRange2.End ).Count);

			Assert.Equal(3, timePeriods.IntersectionPeriods( timeRange3.Start ).Count);
			Assert.Equal(2, timePeriods.IntersectionPeriods( timeRange3.End ).Count);

			Assert.Equal(2, timePeriods.IntersectionPeriods( timeRange4.Start ).Count);
			Assert.Equal(3, timePeriods.IntersectionPeriods( timeRange4.End ).Count);

			Assert.Equal(2, timePeriods.IntersectionPeriods( timeRange5.Start ).Count);
			Assert.Equal(2, timePeriods.IntersectionPeriods( timeRange5.End ).Count);

			DateTime test1 = timeRange1.Start.AddMilliseconds( -1 );
			ITimePeriodCollection insidePeriods1 = timePeriods.IntersectionPeriods( test1 );
			Assert.Equal(0, insidePeriods1.Count);

			DateTime test2 = timeRange1.End.AddMilliseconds( 1 );
			ITimePeriodCollection insidePeriods2 = timePeriods.IntersectionPeriods( test2 );
			Assert.Equal(0, insidePeriods2.Count);

			DateTime test3 = new DateTime( now.Year, now.Month, 12 );
			ITimePeriodCollection insidePeriods3 = timePeriods.IntersectionPeriods( test3 );
			Assert.Equal(2, insidePeriods3.Count);

			DateTime test4 = new DateTime( now.Year, now.Month, 14 );
			ITimePeriodCollection insidePeriods4 = timePeriods.IntersectionPeriods( test4 );
			Assert.Equal(3, insidePeriods4.Count);
		} // IntersectionPeriodsDateTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal(1, timePeriods.RelationPeriods( timeRange1, PeriodRelation.ExactMatch ).Count);
			Assert.Equal(1, timePeriods.RelationPeriods( timeRange2, PeriodRelation.ExactMatch ).Count);
			Assert.Equal(1, timePeriods.RelationPeriods( timeRange3, PeriodRelation.ExactMatch ).Count);
			Assert.Equal(1, timePeriods.RelationPeriods( timeRange4, PeriodRelation.ExactMatch ).Count);
			Assert.Equal(1, timePeriods.RelationPeriods( timeRange5, PeriodRelation.ExactMatch ).Count);

			// all
			Assert.Equal(5, timePeriods.RelationPeriods( new TimeRange( new DateTime( now.Year, now.Month, 7 ), new DateTime( now.Year, now.Month, 19 ) ), PeriodRelation.Enclosing ).Count);

			// timerange3
			Assert.Equal(1, timePeriods.RelationPeriods( new TimeRange( new DateTime( now.Year, now.Month, 11 ), new DateTime( now.Year, now.Month, 16 ) ), PeriodRelation.Enclosing ).Count);
		} // RelationPeriodsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal(5, timePeriods.IntersectionPeriods( timeRange1 ).Count);
			Assert.Equal(3, timePeriods.IntersectionPeriods( timeRange2 ).Count);
			Assert.Equal(4, timePeriods.IntersectionPeriods( timeRange3 ).Count);
			Assert.Equal(4, timePeriods.IntersectionPeriods( timeRange4 ).Count);
			Assert.Equal(3, timePeriods.IntersectionPeriods( timeRange5 ).Count);

			ITimeRange test1 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ).Negate() );
			ITimePeriodCollection insidePeriods1 = timePeriods.IntersectionPeriods( test1 );
			Assert.Equal(0, insidePeriods1.Count);

			ITimeRange test2 = timeRange1.Copy( new TimeSpan( 100, 0, 0, 0 ) );
			ITimePeriodCollection insidePeriods2 = timePeriods.IntersectionPeriods( test2 );
			Assert.Equal(0, insidePeriods2.Count);

			TimeRange test3 = new TimeRange( new DateTime( now.Year, now.Month, 9 ), new DateTime( now.Year, now.Month, 11 ) );
			ITimePeriodCollection insidePeriods3 = timePeriods.IntersectionPeriods( test3 );
			Assert.Equal(3, insidePeriods3.Count);

			TimeRange test4 = new TimeRange( new DateTime( now.Year, now.Month, 14 ), new DateTime( now.Year, now.Month, 17 ) );
			ITimePeriodCollection insidePeriods4 = timePeriods.IntersectionPeriods( test4 );
			Assert.Equal(3, insidePeriods4.Count);
		} // IntersectionPeriodsTimePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void HasEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.False( timePeriods.HasEnd );

			timePeriods.Add( new TimeBlock( Duration.Hour, TimeSpec.MaxPeriodDate ) );
			Assert.False( timePeriods.HasEnd );

			timePeriods.Clear();
			timePeriods.Add( new TimeBlock( now, Duration.Hour ) );
			Assert.True( timePeriods.HasEnd );
		} // HasEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void EndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal( timePeriods.End, TimeSpec.MaxPeriodDate );

			timePeriods.Add( new TimeBlock( Duration.Hour, now ) );
			Assert.Equal( timePeriods.End, now );

			timePeriods.Clear();
			Assert.Equal( timePeriods.End, TimeSpec.MaxPeriodDate );
		} // EndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void EndMoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			DateTime end = schoolDay.End;
			timePeriods.End = end.AddHours( 0 );
			Assert.Equal( timePeriods.End, end );
			timePeriods.End = end.AddHours( 1 );
			Assert.Equal( timePeriods.End, end.AddHours( 1 ) );
			timePeriods.End = end.AddHours( -1 );
			Assert.Equal( timePeriods.End, end.AddHours( -1 ) );
		} // EndMoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void DurationTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal( timePeriods.Duration, TimeSpec.MaxPeriodDuration );

			TimeSpan duration = Duration.Hour;
			timePeriods.Add( new TimeBlock( ClockProxy.Clock.Now, duration ) );
			Assert.Equal( timePeriods.Duration, duration );
		} // DurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void TotalDurationTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange timeRange1 = new TimeRange( new DateTime( now.Year, now.Month, 8 ), new DateTime( now.Year, now.Month, 18 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( now.Year, now.Month, 10 ), new DateTime( now.Year, now.Month, 11 ) );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal( timePeriods.TotalDuration, TimeSpan.Zero );

			timePeriods.Add( timeRange1 );
			Assert.Equal( timePeriods.TotalDuration, timeRange1.End.Subtract( timeRange1.Start ) );
			Assert.Equal( timePeriods.TotalDuration, timeRange1.Duration );

			timePeriods.Add( timeRange2 );
			Assert.Equal( timePeriods.TotalDuration, timeRange1.End.Subtract( timeRange1.Start ).
				Add( timeRange2.End.Subtract( timeRange2.Start ) ) );
			Assert.Equal( timePeriods.TotalDuration, timeRange1.Duration.Add( timeRange2.Duration ) );
		} // TotalDurationTest

        /*
		// ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
		[Fact]
		public void TotalDaylightDurationTest()
		{
			DateTime dstStart = new DateTime( 2014, 3, 30, 2, 0, 0 ).AddHours( 1 );
			DateTime dstEnd = new DateTime( 2014, 10, 26, 3, 0, 0 ).AddHours( -1 );
			TimeRange timeRange1 = new TimeRange( dstStart.AddHours( -2 ), dstStart.AddHours( 2 ) );
			TimeRange timeRange2 = new TimeRange( dstEnd.AddHours( -2 ), dstEnd.AddHours( 2 ) );

			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById( "Central Europe Standard Time" );

			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal( timePeriods.GetTotalDaylightDuration( timeZone ), TimeSpan.Zero );

			timePeriods.Add( timeRange1 );
			Assert.Equal( timePeriods.GetTotalDaylightDuration( timeZone ), timeRange1.GetDaylightDuration( timeZone ) );

			timePeriods.Add( timeRange2 );
			Assert.Equal( timePeriods.GetTotalDaylightDuration( timeZone ), timeRange1.GetDaylightDuration( timeZone ).
				Add( timeRange2.GetDaylightDuration( timeZone ) ) );
		} // TotalDaylightDurationTest
		*/

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
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

			Assert.Equal( timePeriods.Start, startDate.Add( duration ) );
			Assert.Equal( timePeriods.End, endDate.Add( duration ) );
			Assert.Equal( timePeriods.Duration, startDuration );
		} // MoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void AddTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal(0, timePeriods.Count);

			timePeriods.Add( new TimeRange() );
			Assert.Equal(1, timePeriods.Count);

			timePeriods.Add( new TimeRange() );
			Assert.Equal(2, timePeriods.Count);

			timePeriods.Clear();
			Assert.Equal(0, timePeriods.Count);
		} // AddTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void ContainsPeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );
			Assert.False( timePeriods.Contains( timeRange ) );
			Assert.True( timePeriods.ContainsPeriod( timeRange ) );

			timePeriods.Add( timeRange );
			Assert.True( timePeriods.Contains( timeRange ) );
			Assert.True( timePeriods.ContainsPeriod( timeRange ) );
		} // ContainsPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void AddAllTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.Equal(0, timePeriods.Count);

			timePeriods.AddAll( schoolDay );
			Assert.Equal( timePeriods.Count, schoolDay.Count );

			timePeriods.Clear();
			Assert.Equal(0, timePeriods.Count);
		} // AddAllTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void InsertTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal(0, timePeriods.Count);

			timePeriods.Add( schoolDay.Lesson1 );
			Assert.Equal(1, timePeriods.Count);
			timePeriods.Add( schoolDay.Lesson3 );
			Assert.Equal(2, timePeriods.Count);
			timePeriods.Add( schoolDay.Lesson4 );
			Assert.Equal(3, timePeriods.Count);

			// between
			Assert.Equal( timePeriods[ 1 ], schoolDay.Lesson3 );
			timePeriods.Insert( 1, schoolDay.Lesson2 );
			Assert.Equal( timePeriods[ 1 ], schoolDay.Lesson2 );

			// first
			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson1 );
			timePeriods.Insert( 0, schoolDay.Break1 );
			Assert.Equal( timePeriods[ 0 ], schoolDay.Break1 );

			// last
			Assert.Equal( timePeriods[ timePeriods.Count - 1 ], schoolDay.Lesson4 );
			timePeriods.Insert( timePeriods.Count, schoolDay.Break3 );
			Assert.Equal( timePeriods[ timePeriods.Count - 1 ], schoolDay.Break3 );
		} // InsertTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void ContainsTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.False( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Add( schoolDay.Lesson1 );
			Assert.True( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Remove( schoolDay.Lesson1 );
			Assert.False( timePeriods.Contains( schoolDay.Lesson1 ) );
		} // ContainsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void IndexOfTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.Equal( timePeriods.IndexOf( new TimeRange() ), -1 );
			Assert.Equal( timePeriods.IndexOf( new TimeBlock() ), -1 );

			timePeriods.AddAll( schoolDay );

			Assert.Equal(0, timePeriods.IndexOf( schoolDay.Lesson1 ));
			Assert.Equal(1, timePeriods.IndexOf( schoolDay.Break1 ));
			Assert.Equal(2, timePeriods.IndexOf( schoolDay.Lesson2 ));
			Assert.Equal(3, timePeriods.IndexOf( schoolDay.Break2 ));
			Assert.Equal(4, timePeriods.IndexOf( schoolDay.Lesson3 ));
			Assert.Equal(5, timePeriods.IndexOf( schoolDay.Break3 ));
			Assert.Equal(6, timePeriods.IndexOf( schoolDay.Lesson4 ));

			timePeriods.Remove( schoolDay.Lesson1 );
			Assert.Equal( timePeriods.IndexOf( schoolDay.Lesson1 ), -1 );
		} // IndexOfTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void CopyToTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			ITimePeriod[] array = new ITimePeriod[ schoolDay.Count ];
			timePeriods.CopyTo( array, 0 );
			Assert.Equal( array[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( array[ 1 ], schoolDay.Break1 );
			Assert.Equal( array[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( array[ 3 ], schoolDay.Break2 );
			Assert.Equal( array[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( array[ 5 ], schoolDay.Break3 );
			Assert.Equal( array[ 6 ], schoolDay.Lesson4 );
		} // CopyToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void ClearTest()
		{
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			Assert.Equal(0, timePeriods.Count);
			timePeriods.Clear();
			Assert.Equal(0, timePeriods.Count);

			timePeriods.AddAll( new SchoolDay() );
			Assert.Equal(7, timePeriods.Count);
			timePeriods.Clear();
			Assert.Equal(0, timePeriods.Count);
		} // ClearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void RemoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection();

			Assert.False( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Add( schoolDay.Lesson1 );
			Assert.True( timePeriods.Contains( schoolDay.Lesson1 ) );
			timePeriods.Remove( schoolDay.Lesson1 );
			Assert.False( timePeriods.Contains( schoolDay.Lesson1 ) );
		} // RemoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void RemoveAtTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			// inside
			Assert.Equal( timePeriods[ 2 ], schoolDay.Lesson2 );
			timePeriods.RemoveAt( 2 );
			Assert.Equal( timePeriods[ 2 ], schoolDay.Break2 );

			// first
			Assert.Equal( timePeriods[ 0 ], schoolDay.Lesson1 );
			timePeriods.RemoveAt( 0 );
			Assert.Equal( timePeriods[ 0 ], schoolDay.Break1 );

			// last
			Assert.Equal( timePeriods[ timePeriods.Count - 1 ], schoolDay.Lesson4 );
			timePeriods.RemoveAt( timePeriods.Count - 1 );
			Assert.Equal( timePeriods[ timePeriods.Count - 1 ], schoolDay.Break3 );
		} // RemoveAtTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void IsSamePeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodCollection timePeriods = new TimePeriodCollection( schoolDay );

			Assert.True( timePeriods.IsSamePeriod( timePeriods ) );
			Assert.True( timePeriods.IsSamePeriod( schoolDay ) );

			Assert.True( schoolDay.IsSamePeriod( schoolDay ) );
			Assert.True( schoolDay.IsSamePeriod( timePeriods ) );

			Assert.False( timePeriods.IsSamePeriod( TimeBlock.Anytime ) );
			Assert.False( schoolDay.IsSamePeriod( TimeBlock.Anytime ) );

			timePeriods.RemoveAt( 0 );
			Assert.False( timePeriods.IsSamePeriod( schoolDay ) );
		} // IsSamePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void HasInsideTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.False( timePeriods.HasInside( testData.Before ) );
			Assert.False( timePeriods.HasInside( testData.StartTouching ) );
			Assert.False( timePeriods.HasInside( testData.StartInside ) );
			Assert.False( timePeriods.HasInside( testData.InsideStartTouching ) );
			Assert.True( timePeriods.HasInside( testData.EnclosingStartTouching ) );
			Assert.True( timePeriods.HasInside( testData.Enclosing ) );
			Assert.True( timePeriods.HasInside( testData.EnclosingEndTouching ) );
			Assert.True( timePeriods.HasInside( testData.ExactMatch ) );
			Assert.False( timePeriods.HasInside( testData.Inside ) );
			Assert.False( timePeriods.HasInside( testData.InsideEndTouching ) );
			Assert.False( timePeriods.HasInside( testData.EndInside ) );
			Assert.False( timePeriods.HasInside( testData.EndTouching ) );
			Assert.False( timePeriods.HasInside( testData.After ) );
		} // HasInsideTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void IntersectsWithTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.False( timePeriods.IntersectsWith( testData.Before ) );
			Assert.True( timePeriods.IntersectsWith( testData.StartTouching ) );
			Assert.True( timePeriods.IntersectsWith( testData.StartInside ) );
			Assert.True( timePeriods.IntersectsWith( testData.InsideStartTouching ) );
			Assert.True( timePeriods.IntersectsWith( testData.EnclosingStartTouching ) );
			Assert.True( timePeriods.IntersectsWith( testData.Enclosing ) );
			Assert.True( timePeriods.IntersectsWith( testData.EnclosingEndTouching ) );
			Assert.True( timePeriods.IntersectsWith( testData.ExactMatch ) );
			Assert.True( timePeriods.IntersectsWith( testData.Inside ) );
			Assert.True( timePeriods.IntersectsWith( testData.InsideEndTouching ) );
			Assert.True( timePeriods.IntersectsWith( testData.EndInside ) );
			Assert.True( timePeriods.IntersectsWith( testData.EndTouching ) );
			Assert.False( timePeriods.IntersectsWith( testData.After ) );
		} // IntersectsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void OverlapsWithTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.False( timePeriods.OverlapsWith( testData.Before ) );
			Assert.False( timePeriods.OverlapsWith( testData.StartTouching ) );
			Assert.True( timePeriods.OverlapsWith( testData.StartInside ) );
			Assert.True( timePeriods.OverlapsWith( testData.InsideStartTouching ) );
			Assert.True( timePeriods.OverlapsWith( testData.EnclosingStartTouching ) );
			Assert.True( timePeriods.OverlapsWith( testData.Enclosing ) );
			Assert.True( timePeriods.OverlapsWith( testData.EnclosingEndTouching ) );
			Assert.True( timePeriods.OverlapsWith( testData.ExactMatch ) );
			Assert.True( timePeriods.OverlapsWith( testData.Inside ) );
			Assert.True( timePeriods.OverlapsWith( testData.InsideEndTouching ) );
			Assert.True( timePeriods.OverlapsWith( testData.EndInside ) );
			Assert.False( timePeriods.OverlapsWith( testData.EndTouching ) );
			Assert.False( timePeriods.OverlapsWith( testData.After ) );
		} // OverlapsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodCollection")]
        [Fact]
		public void GetRelationTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( now, now.AddHours( 1 ), offset );
			TimePeriodCollection timePeriods = new TimePeriodCollection();
			timePeriods.Add( testData.Reference );

			Assert.Equal(PeriodRelation.Before, timePeriods.GetRelation( testData.Before ));
			Assert.Equal(PeriodRelation.StartTouching, timePeriods.GetRelation( testData.StartTouching ));
			Assert.Equal(PeriodRelation.StartInside, timePeriods.GetRelation( testData.StartInside ));
			Assert.Equal(PeriodRelation.InsideStartTouching, timePeriods.GetRelation( testData.InsideStartTouching ));
			Assert.Equal(PeriodRelation.EnclosingStartTouching, timePeriods.GetRelation( testData.EnclosingStartTouching ));
			Assert.Equal(PeriodRelation.Enclosing, timePeriods.GetRelation( testData.Enclosing ));
			Assert.Equal(PeriodRelation.EnclosingEndTouching, timePeriods.GetRelation( testData.EnclosingEndTouching ));
			Assert.Equal(PeriodRelation.ExactMatch, timePeriods.GetRelation( testData.ExactMatch ));
			Assert.Equal(PeriodRelation.Inside, timePeriods.GetRelation( testData.Inside ));
			Assert.Equal(PeriodRelation.InsideEndTouching, timePeriods.GetRelation( testData.InsideEndTouching ));
			Assert.Equal(PeriodRelation.EndInside, timePeriods.GetRelation( testData.EndInside ));
			Assert.Equal(PeriodRelation.EndTouching, timePeriods.GetRelation( testData.EndTouching ));
			Assert.Equal(PeriodRelation.After, timePeriods.GetRelation( testData.After ));
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
