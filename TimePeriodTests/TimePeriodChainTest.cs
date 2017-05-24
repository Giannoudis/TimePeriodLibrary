// -- FILE ------------------------------------------------------------------
// name       : TimePeriodChainTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class TimePeriodChainTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void DefaultConstructorTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.Equal(0, timePeriods.Count);
			Assert.False( timePeriods.HasStart );
			Assert.False( timePeriods.HasEnd );
			Assert.False( timePeriods.IsReadOnly );
		} // DefaultConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void CopyConstructorTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );
			Assert.Equal( timePeriods.Count, schoolDay.Count );
			Assert.True( timePeriods.HasStart );
			Assert.True( timePeriods.HasEnd );
			Assert.False( timePeriods.IsReadOnly );

			Assert.Equal( timePeriods.Start, schoolDay.Start );
		} // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void FirstTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			Assert.Equal( schoolDay.First, schoolDay.Lesson1 );
		} // FirstTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void LastTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			Assert.Equal( schoolDay.Last, schoolDay.Lesson4 );
		} // LastTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void CountTest()
		{
			Assert.Equal(0, new TimePeriodChain().Count);
			Assert.Equal(7, new SchoolDay().Count);
		} // CountTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void ItemIndexTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			Assert.Equal( schoolDay[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( schoolDay[ 1 ], schoolDay.Break1 );
			Assert.Equal( schoolDay[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( schoolDay[ 3 ], schoolDay.Break2 );
			Assert.Equal( schoolDay[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( schoolDay[ 5 ], schoolDay.Break3 );
			Assert.Equal( schoolDay[ 6 ], schoolDay.Lesson4 );
		} // ItemIndexTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void IsAnytimeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.True( timePeriods.IsAnytime );

			timePeriods.Add( new TimeBlock( TimeSpec.MinPeriodDate, now ) );
			Assert.False( timePeriods.IsAnytime );

			timePeriods.Add( new TimeBlock( now, TimeSpec.MaxPeriodDate ) );
			Assert.True( timePeriods.IsAnytime );
		} // IsAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void IsMomentTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.False( timePeriods.IsMoment );

			timePeriods.Add( new TimeBlock( now ) );
			Assert.Equal(1, timePeriods.Count);
			Assert.True( timePeriods.HasStart );
			Assert.True( timePeriods.HasEnd );
			Assert.True( timePeriods.IsMoment );

			timePeriods.Add( new TimeBlock( now ) );
			Assert.Equal(2, timePeriods.Count);
			Assert.True( timePeriods.HasStart );
			Assert.True( timePeriods.HasEnd );
			Assert.True( timePeriods.IsMoment );
		} // IsMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void HasStartTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.False( timePeriods.HasStart );

			timePeriods.Add( new TimeBlock( TimeSpec.MinPeriodDate, Duration.Hour ) );
			Assert.False( timePeriods.HasStart );

			timePeriods.Clear();
			timePeriods.Add( new TimeBlock( ClockProxy.Clock.Now, Duration.Hour ) );
			Assert.True( timePeriods.HasStart );
		} // HasStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void StartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.Equal( timePeriods.Start, TimeSpec.MinPeriodDate );

			timePeriods.Add( new TimeBlock( now, Duration.Hour ) );
			Assert.Equal( timePeriods.Start, now );

			timePeriods.Clear();
			Assert.Equal( timePeriods.Start, TimeSpec.MinPeriodDate );
		} // StartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void StartMoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			Assert.Equal( schoolDay.Start, now );

			schoolDay.Start = now.AddHours( 0 );
			Assert.Equal( schoolDay.Start, now );
			schoolDay.Start = now.AddHours( 1 );
			Assert.Equal( schoolDay.Start, now.AddHours( 1 ) );
			schoolDay.Start = now.AddHours( -1 );
			Assert.Equal( schoolDay.Start, now.AddHours( -1 ) );
		} // StartMoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void HasEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.False( timePeriods.HasEnd );

			timePeriods.Add( new TimeBlock( Duration.Hour, TimeSpec.MaxPeriodDate ) );
			Assert.False( timePeriods.HasEnd );

			timePeriods.Clear();
			timePeriods.Add( new TimeBlock( now, Duration.Hour ) );
			Assert.True( timePeriods.HasEnd );
		} // HasEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void EndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.Equal( timePeriods.End, TimeSpec.MaxPeriodDate );

			timePeriods.Add( new TimeBlock( Duration.Hour, now ) );
			Assert.Equal( timePeriods.End, now );

			timePeriods.Clear();
			Assert.Equal( timePeriods.End, TimeSpec.MaxPeriodDate );
		} // EndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void EndMoveTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );

			DateTime end = schoolDay.End;
			schoolDay.End = end.AddHours( 0 );
			Assert.Equal( schoolDay.End, end );
			schoolDay.End = end.AddHours( 1 );
			Assert.Equal( schoolDay.End, end.AddHours( 1 ) );
			schoolDay.End = end.AddHours( -1 );
			Assert.Equal( schoolDay.End, end.AddHours( -1 ) );
		} // EndMoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void DurationTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.Equal( timePeriods.Duration, TimeSpec.MaxPeriodDuration );

			TimeSpan duration = Duration.Hour;
			timePeriods.Add( new TimeBlock( ClockProxy.Clock.Now, duration ) );
			Assert.Equal( timePeriods.Duration, duration );
		} // DurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void MoveTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			DateTime startDate = schoolDay.Start;
			DateTime endDate = schoolDay.End;
			TimeSpan startDuration = schoolDay.Duration;

			TimeSpan duration = Duration.Hour;
			schoolDay.Move( duration );

			Assert.Equal( schoolDay.Start, startDate.Add( duration ) );
			Assert.Equal( schoolDay.End, endDate.Add( duration ) );
			Assert.Equal( schoolDay.Duration, startDuration );
		} // MoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void AddTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			int count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ShortBreak shortBreak = new ShortBreak( schoolDay.Start );
			schoolDay.Add( shortBreak );
			Assert.Equal( schoolDay.Count, count + 1 );
			Assert.Equal( schoolDay.Last, shortBreak );
			Assert.Equal( schoolDay.End, end.Add( shortBreak.Duration ) );
			Assert.Equal( shortBreak.Start, end );
			Assert.Equal( shortBreak.End, schoolDay.End );
			Assert.Equal( shortBreak.Duration, ShortBreak.ShortBreakDuration );
		} // AddTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void AddTimeRangeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );

			timePeriods.Add( timeRange );
			Assert.Equal( timePeriods.Last, timeRange );
		} // AddTimeRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void ContainsPeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );
			Assert.False( timePeriods.Contains( timeRange ) );
			Assert.True( timePeriods.ContainsPeriod( timeRange ) );
		} // ContainsPeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void AddAllTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			int count = schoolDay.Count;
			TimeSpan duration = schoolDay.Duration;
			DateTime start = schoolDay.Start;

			schoolDay.AddAll( new SchoolDay() );
			Assert.Equal( schoolDay.Count, count + count );
			Assert.Equal( schoolDay.Start, start );
			Assert.Equal( schoolDay.Duration, duration + duration );
		} // AddAllTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void AddReadOnlyTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
            Assert.NotNull(Assert.Throws<NotSupportedException>( () =>
            timePeriods.Add( new TimeRange( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, true ) ) ) );
		} // AddReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void InsertReadOnlyTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
            Assert.NotNull(Assert.Throws<NotSupportedException>(() =>
                timePeriods.Insert( 0, new TimeRange( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, true ) ) ) );
		} // InsertReadOnlyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void InsertTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			// first
			int count = schoolDay.Count;
			DateTime start = schoolDay.Start;
			Lesson lesson1 = new Lesson( schoolDay.Start );
			schoolDay.Insert( 0, lesson1 );
			Assert.Equal( schoolDay.Count, count + 1 );
			Assert.Equal( schoolDay[ 0 ], lesson1 );
			Assert.Equal( schoolDay.First, lesson1 );
			Assert.Equal( schoolDay.Start, start.Subtract( lesson1.Duration ) );
			Assert.Equal( lesson1.Start, schoolDay.Start );
			Assert.Equal( lesson1.End, start );
			Assert.Equal( lesson1.Duration, Lesson.LessonDuration );

			// inside
			count = schoolDay.Count;
			start = schoolDay.Start;
			ShortBreak shortBreak1 = new ShortBreak( schoolDay.Start );
			schoolDay.Insert( 1, shortBreak1 );
			Assert.Equal( schoolDay.Count, count + 1 );
			Assert.Equal( schoolDay[ 1 ], shortBreak1 );
			Assert.Equal( schoolDay.First, lesson1 );
			Assert.Equal( schoolDay.Start, start );
			Assert.Equal( shortBreak1.Start, schoolDay.Start.Add( lesson1.Duration ) );
			Assert.Equal( shortBreak1.Duration, ShortBreak.ShortBreakDuration );

			// last
			count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ShortBreak shortBreak2 = new ShortBreak( schoolDay.Start );
			schoolDay.Insert( schoolDay.Count, shortBreak2 );
			Assert.Equal( schoolDay.Count, count + 1 );
			Assert.Equal( schoolDay[ count ], shortBreak2 );
			Assert.Equal( schoolDay.Last, shortBreak2 );
			Assert.Equal( schoolDay.End, shortBreak2.End );
			Assert.Equal( shortBreak2.Start, end );
			Assert.Equal( shortBreak2.Duration, ShortBreak.ShortBreakDuration );
		} // InsertTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void InsertTimeRangeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );

			timePeriods.Add( timeRange );
			Assert.Equal( timePeriods.Last, timeRange );
		} // InsertTimeRangeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void ContainsTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			Assert.False( schoolDay.Contains( new TimeRange() ) );
			Assert.False( schoolDay.Contains( new TimeBlock() ) );

			Assert.True( schoolDay.Contains( schoolDay.Lesson1 ) );
			Assert.True( schoolDay.Contains( schoolDay.Break1 ) );
			Assert.True( schoolDay.Contains( schoolDay.Lesson2 ) );
			Assert.True( schoolDay.Contains( schoolDay.Break2 ) );
			Assert.True( schoolDay.Contains( schoolDay.Lesson3 ) );
			Assert.True( schoolDay.Contains( schoolDay.Break3 ) );
			Assert.True( schoolDay.Contains( schoolDay.Lesson4 ) );

			schoolDay.Remove( schoolDay.Lesson1 );
			Assert.False( schoolDay.Contains( schoolDay.Lesson1 ) );
		} // ContainsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void IndexOfTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			Assert.Equal( schoolDay.IndexOf( new TimeRange() ), -1 );
			Assert.Equal( schoolDay.IndexOf( new TimeBlock() ), -1 );

			Assert.Equal(0, schoolDay.IndexOf( schoolDay.Lesson1 ));
			Assert.Equal(1, schoolDay.IndexOf( schoolDay.Break1 ));
			Assert.Equal(2, schoolDay.IndexOf( schoolDay.Lesson2 ));
			Assert.Equal(3, schoolDay.IndexOf( schoolDay.Break2 ));
			Assert.Equal(4, schoolDay.IndexOf( schoolDay.Lesson3 ));
			Assert.Equal(5, schoolDay.IndexOf( schoolDay.Break3 ));
			Assert.Equal(6, schoolDay.IndexOf( schoolDay.Lesson4 ));

			schoolDay.Remove( schoolDay.Lesson1 );
			Assert.Equal( schoolDay.IndexOf( schoolDay.Lesson1 ), -1 );
		} // IndexOfTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void CopyToTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			ITimePeriod[] array1 = new ITimePeriod[ 0 ];
			timePeriods.CopyTo( array1, 0 );

			SchoolDay schoolDay = new SchoolDay();
			ITimePeriod[] array2 = new ITimePeriod[ schoolDay.Count ];
			schoolDay.CopyTo( array2, 0 );
			Assert.Equal( array2[ 0 ], schoolDay.Lesson1 );
			Assert.Equal( array2[ 1 ], schoolDay.Break1 );
			Assert.Equal( array2[ 2 ], schoolDay.Lesson2 );
			Assert.Equal( array2[ 3 ], schoolDay.Break2 );
			Assert.Equal( array2[ 4 ], schoolDay.Lesson3 );
			Assert.Equal( array2[ 5 ], schoolDay.Break3 );
			Assert.Equal( array2[ 6 ], schoolDay.Lesson4 );

			ITimePeriod[] array3 = new ITimePeriod[ schoolDay.Count + 3 ];
			schoolDay.CopyTo( array3, 3 );
			Assert.Equal( array3[ 3 ], schoolDay.Lesson1 );
			Assert.Equal( array3[ 4 ], schoolDay.Break1 );
			Assert.Equal( array3[ 5 ], schoolDay.Lesson2 );
			Assert.Equal( array3[ 6 ], schoolDay.Break2 );
			Assert.Equal( array3[ 7 ], schoolDay.Lesson3 );
			Assert.Equal( array3[ 8 ], schoolDay.Break3 );
			Assert.Equal( array3[ 9 ], schoolDay.Lesson4 );
		} // CopyToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void ClearTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.Equal(0, timePeriods.Count);
			timePeriods.Clear();
			Assert.Equal(0, timePeriods.Count);

			SchoolDay schoolDay = new SchoolDay();
			Assert.Equal(7, schoolDay.Count);
			schoolDay.Clear();
			Assert.Equal(0, schoolDay.Count);
		} // ClearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void RemoveTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			// first
			int count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ITimePeriod removeItem = schoolDay.First;
			TimeSpan duration = schoolDay.Duration;
			schoolDay.Remove( removeItem );
			Assert.Equal( schoolDay.Count, count - 1 );
			Assert.NotEqual( schoolDay.First, removeItem );
			Assert.Equal( schoolDay.End, end );
			Assert.Equal( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// inside
			count = schoolDay.Count;
			duration = schoolDay.Duration;
			DateTime start = schoolDay.Start;
			ITimePeriod first = schoolDay.First;
			ITimePeriod last = schoolDay.Last;
			removeItem = schoolDay[ 1 ];
			schoolDay.Remove( removeItem );
			Assert.Equal( schoolDay.Count, count - 1 );
			Assert.NotEqual( schoolDay[ 1 ], removeItem );
			Assert.Equal( schoolDay.First, first );
			Assert.Equal( schoolDay.Start, start );
			Assert.Equal( schoolDay.Last, last );
			Assert.Equal( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// last
			count = schoolDay.Count;
			start = schoolDay.Start;
			duration = schoolDay.Duration;
			removeItem = schoolDay.Last;
			schoolDay.Remove( removeItem );
			Assert.Equal( schoolDay.Count, count - 1 );
			Assert.NotEqual( schoolDay.Last, removeItem );
			Assert.Equal( schoolDay.Start, start );
			Assert.Equal( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );
		} // RemoveTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void RemoveAtTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			// first
			int count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ITimePeriod removeItem = schoolDay[ 0 ];
			TimeSpan duration = schoolDay.Duration;
			schoolDay.RemoveAt( 0 );
			Assert.Equal( schoolDay.Count, count - 1 );
			Assert.NotEqual( schoolDay[ 0 ], removeItem );
			Assert.Equal( schoolDay.End, end );
			Assert.Equal( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// inside
			count = schoolDay.Count;
			duration = schoolDay.Duration;
			DateTime start = schoolDay.Start;
			ITimePeriod first = schoolDay.First;
			ITimePeriod last = schoolDay.Last;
			removeItem = schoolDay[ 1 ];
			schoolDay.RemoveAt( 1 );
			Assert.Equal( schoolDay.Count, count - 1 );
			Assert.NotEqual( schoolDay[ 1 ], removeItem );
			Assert.Equal( schoolDay.First, first );
			Assert.Equal( schoolDay.Start, start );
			Assert.Equal( schoolDay.Last, last );
			Assert.Equal( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// last
			count = schoolDay.Count;
			start = schoolDay.Start;
			duration = schoolDay.Duration;
			removeItem = schoolDay[ schoolDay.Count - 1 ];
			schoolDay.RemoveAt( schoolDay.Count - 1 );
			Assert.Equal( schoolDay.Count, count - 1 );
			Assert.NotEqual( schoolDay[ schoolDay.Count - 1 ], removeItem );
			Assert.Equal( schoolDay.Start, start );
			Assert.Equal( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );
		} // RemoveAtTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void IsSamePeriodTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeRange manualRange = new TimeRange( schoolDay.Start, schoolDay.End );

			Assert.True( schoolDay.IsSamePeriod( schoolDay ) );
			Assert.True( schoolDay.IsSamePeriod( manualRange ) );
			Assert.True( manualRange.IsSamePeriod( schoolDay ) );

			Assert.False( schoolDay.IsSamePeriod( TimeBlock.Anytime ) );
			Assert.False( manualRange.IsSamePeriod( TimeBlock.Anytime ) );

			schoolDay.RemoveAt( 0 );
			Assert.False( schoolDay.IsSamePeriod( manualRange ) );
			Assert.False( manualRange.IsSamePeriod( schoolDay ) );
		} // IsSamePeriodTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void HasInsideTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.False( schoolDay.HasInside( testData.Before ) );
			Assert.False( schoolDay.HasInside( testData.StartTouching ) );
			Assert.False( schoolDay.HasInside( testData.StartInside ) );
			Assert.False( schoolDay.HasInside( testData.InsideStartTouching ) );
			Assert.True( schoolDay.HasInside( testData.EnclosingStartTouching ) );
			Assert.True( schoolDay.HasInside( testData.Enclosing ) );
			Assert.True( schoolDay.HasInside( testData.ExactMatch ) );
			Assert.True( schoolDay.HasInside( testData.EnclosingEndTouching ) );
			Assert.False( schoolDay.HasInside( testData.Inside ) );
			Assert.False( schoolDay.HasInside( testData.InsideEndTouching ) );
			Assert.False( schoolDay.HasInside( testData.EndInside ) );
			Assert.False( schoolDay.HasInside( testData.EndTouching ) );
			Assert.False( schoolDay.HasInside( testData.After ) );
		} // HasInsideTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void IntersectsWithTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.False( schoolDay.IntersectsWith( testData.Before ) );
			Assert.True( schoolDay.IntersectsWith( testData.StartTouching ) );
			Assert.True( schoolDay.IntersectsWith( testData.StartInside ) );
			Assert.True( schoolDay.IntersectsWith( testData.InsideStartTouching ) );
			Assert.True( schoolDay.IntersectsWith( testData.EnclosingStartTouching ) );
			Assert.True( schoolDay.IntersectsWith( testData.Enclosing ) );
			Assert.True( schoolDay.IntersectsWith( testData.EnclosingEndTouching ) );
			Assert.True( schoolDay.IntersectsWith( testData.ExactMatch ) );
			Assert.True( schoolDay.IntersectsWith( testData.Inside ) );
			Assert.True( schoolDay.IntersectsWith( testData.InsideEndTouching ) );
			Assert.True( schoolDay.IntersectsWith( testData.EndInside ) );
			Assert.True( schoolDay.IntersectsWith( testData.EndTouching ) );
			Assert.False( schoolDay.IntersectsWith( testData.After ) );
		} // IntersectsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void OverlapsWithTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.False( schoolDay.OverlapsWith( testData.Before ) );
			Assert.False( schoolDay.OverlapsWith( testData.StartTouching ) );
			Assert.True( schoolDay.OverlapsWith( testData.StartInside ) );
			Assert.True( schoolDay.OverlapsWith( testData.InsideStartTouching ) );
			Assert.True( schoolDay.OverlapsWith( testData.EnclosingStartTouching ) );
			Assert.True( schoolDay.OverlapsWith( testData.Enclosing ) );
			Assert.True( schoolDay.OverlapsWith( testData.EnclosingEndTouching ) );
			Assert.True( schoolDay.OverlapsWith( testData.ExactMatch ) );
			Assert.True( schoolDay.OverlapsWith( testData.Inside ) );
			Assert.True( schoolDay.OverlapsWith( testData.InsideEndTouching ) );
			Assert.True( schoolDay.OverlapsWith( testData.EndInside ) );
			Assert.False( schoolDay.OverlapsWith( testData.EndTouching ) );
			Assert.False( schoolDay.OverlapsWith( testData.After ) );
		} // OverlapsWithTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimePeriodChain")]
        [Fact]
		public void GetRelationTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.Equal(PeriodRelation.Before, schoolDay.GetRelation( testData.Before ));
			Assert.Equal(PeriodRelation.StartTouching, schoolDay.GetRelation( testData.StartTouching ));
			Assert.Equal(PeriodRelation.StartInside, schoolDay.GetRelation( testData.StartInside ));
			Assert.Equal(PeriodRelation.InsideStartTouching, schoolDay.GetRelation( testData.InsideStartTouching ));
			Assert.Equal(PeriodRelation.EnclosingStartTouching, schoolDay.GetRelation( testData.EnclosingStartTouching ));
			Assert.Equal(PeriodRelation.Enclosing, schoolDay.GetRelation( testData.Enclosing ));
			Assert.Equal(PeriodRelation.EnclosingEndTouching, schoolDay.GetRelation( testData.EnclosingEndTouching ));
			Assert.Equal(PeriodRelation.ExactMatch, schoolDay.GetRelation( testData.ExactMatch ));
			Assert.Equal(PeriodRelation.Inside, schoolDay.GetRelation( testData.Inside ));
			Assert.Equal(PeriodRelation.InsideEndTouching, schoolDay.GetRelation( testData.InsideEndTouching ));
			Assert.Equal(PeriodRelation.EndInside, schoolDay.GetRelation( testData.EndInside ));
			Assert.Equal(PeriodRelation.EndTouching, schoolDay.GetRelation( testData.EndTouching ));
			Assert.Equal(PeriodRelation.After, schoolDay.GetRelation( testData.After ));
		} // GetRelationTest

	} // class TimePeriodChainTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
