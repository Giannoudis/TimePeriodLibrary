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
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class TimePeriodChainTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultConstructorTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.AreEqual( timePeriods.Count, 0 );
			Assert.IsFalse( timePeriods.HasStart );
			Assert.IsFalse( timePeriods.HasEnd );
			Assert.IsFalse( timePeriods.IsReadOnly );
		} // DefaultConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyConstructorTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );
			Assert.AreEqual( timePeriods.Count, schoolDay.Count );
			Assert.IsTrue( timePeriods.HasStart );
			Assert.IsTrue( timePeriods.HasEnd );
			Assert.IsFalse( timePeriods.IsReadOnly );

			Assert.AreEqual( timePeriods.Start, schoolDay.Start );
		} // CopyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			Assert.AreEqual( schoolDay.First, schoolDay.Lesson1 );
		} // FirstTest

		// ----------------------------------------------------------------------
		[Test]
		public void LastTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			Assert.AreEqual( schoolDay.Last, schoolDay.Lesson4 );
		} // LastTest

		// ----------------------------------------------------------------------
		[Test]
		public void CountTest()
		{
			Assert.AreEqual( new TimePeriodChain().Count, 0 );
			Assert.AreEqual( new SchoolDay().Count, 7 );
		} // CountTest

		// ----------------------------------------------------------------------
		[Test]
		public void ItemIndexTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			Assert.AreEqual( schoolDay[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( schoolDay[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( schoolDay[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( schoolDay[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( schoolDay[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( schoolDay[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( schoolDay[ 6 ], schoolDay.Lesson4 );
		} // ItemIndexTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsAnytimeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.IsTrue( timePeriods.IsAnytime );

			timePeriods.Add( new TimeBlock( TimeSpec.MinPeriodDate, now ) );
			Assert.IsFalse( timePeriods.IsAnytime );

			timePeriods.Add( new TimeBlock( now, TimeSpec.MaxPeriodDate ) );
			Assert.IsTrue( timePeriods.IsAnytime );
		} // IsAnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsMomentTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.IsFalse( timePeriods.IsMoment );

			timePeriods.Add( new TimeBlock( now ) );
			Assert.AreEqual( timePeriods.Count, 1 );
			Assert.IsTrue( timePeriods.HasStart );
			Assert.IsTrue( timePeriods.HasEnd );
			Assert.IsTrue( timePeriods.IsMoment );

			timePeriods.Add( new TimeBlock( now ) );
			Assert.AreEqual( timePeriods.Count, 2 );
			Assert.IsTrue( timePeriods.HasStart );
			Assert.IsTrue( timePeriods.HasEnd );
			Assert.IsTrue( timePeriods.IsMoment );
		} // IsMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasStartTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
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
			TimePeriodChain timePeriods = new TimePeriodChain();
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
			Assert.AreEqual( schoolDay.Start, now );

			schoolDay.Start = now.AddHours( 0 );
			Assert.AreEqual( schoolDay.Start, now );
			schoolDay.Start = now.AddHours( 1 );
			Assert.AreEqual( schoolDay.Start, now.AddHours( 1 ) );
			schoolDay.Start = now.AddHours( -1 );
			Assert.AreEqual( schoolDay.Start, now.AddHours( -1 ) );
		} // StartMoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimePeriodChain timePeriods = new TimePeriodChain();
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
			TimePeriodChain timePeriods = new TimePeriodChain();
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

			DateTime end = schoolDay.End;
			schoolDay.End = end.AddHours( 0 );
			Assert.AreEqual( schoolDay.End, end );
			schoolDay.End = end.AddHours( 1 );
			Assert.AreEqual( schoolDay.End, end.AddHours( 1 ) );
			schoolDay.End = end.AddHours( -1 );
			Assert.AreEqual( schoolDay.End, end.AddHours( -1 ) );
		} // EndMoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.AreEqual( timePeriods.Duration, TimeSpec.MaxPeriodDuration );

			TimeSpan duration = Duration.Hour;
			timePeriods.Add( new TimeBlock( ClockProxy.Clock.Now, duration ) );
			Assert.AreEqual( timePeriods.Duration, duration );
		} // DurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void MoveTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			DateTime startDate = schoolDay.Start;
			DateTime endDate = schoolDay.End;
			TimeSpan startDuration = schoolDay.Duration;

			TimeSpan duration = Duration.Hour;
			schoolDay.Move( duration );

			Assert.AreEqual( schoolDay.Start, startDate.Add( duration ) );
			Assert.AreEqual( schoolDay.End, endDate.Add( duration ) );
			Assert.AreEqual( schoolDay.Duration, startDuration );
		} // MoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			int count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ShortBreak shortBreak = new ShortBreak( schoolDay.Start );
			schoolDay.Add( shortBreak );
			Assert.AreEqual( schoolDay.Count, count + 1 );
			Assert.AreEqual( schoolDay.Last, shortBreak );
			Assert.AreEqual( schoolDay.End, end.Add( shortBreak.Duration ) );
			Assert.AreEqual( shortBreak.Start, end );
			Assert.AreEqual( shortBreak.End, schoolDay.End );
			Assert.AreEqual( shortBreak.Duration, ShortBreak.ShortBreakDuration );
		} // AddTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddTimeRangeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );

			timePeriods.Add( timeRange );
			Assert.AreEqual( timePeriods.Last, timeRange );
		} // AddTimeRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void ContainsPeriodTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );
			Assert.IsFalse( timePeriods.Contains( timeRange ) );
			Assert.IsTrue( timePeriods.ContainsPeriod( timeRange ) );
		} // ContainsPeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddAllTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			int count = schoolDay.Count;
			TimeSpan duration = schoolDay.Duration;
			DateTime start = schoolDay.Start;

			schoolDay.AddAll( new SchoolDay() );
			Assert.AreEqual( schoolDay.Count, count + count );
			Assert.AreEqual( schoolDay.Start, start );
			Assert.AreEqual( schoolDay.Duration, duration + duration );
		} // AddAllTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void AddReadOnlyTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			timePeriods.Add( new TimeRange( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, true ) );
		} // AddReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( NotSupportedException ) )]
		public void InsertReadOnlyTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			timePeriods.Insert( 0, new TimeRange( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, true ) );
		} // InsertReadOnlyTest

		// ----------------------------------------------------------------------
		[Test]
		public void InsertTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			// first
			int count = schoolDay.Count;
			DateTime start = schoolDay.Start;
			Lesson lesson1 = new Lesson( schoolDay.Start );
			schoolDay.Insert( 0, lesson1 );
			Assert.AreEqual( schoolDay.Count, count + 1 );
			Assert.AreEqual( schoolDay[ 0 ], lesson1 );
			Assert.AreEqual( schoolDay.First, lesson1 );
			Assert.AreEqual( schoolDay.Start, start.Subtract( lesson1.Duration ) );
			Assert.AreEqual( lesson1.Start, schoolDay.Start );
			Assert.AreEqual( lesson1.End, start );
			Assert.AreEqual( lesson1.Duration, Lesson.LessonDuration );

			// inside
			count = schoolDay.Count;
			start = schoolDay.Start;
			ShortBreak shortBreak1 = new ShortBreak( schoolDay.Start );
			schoolDay.Insert( 1, shortBreak1 );
			Assert.AreEqual( schoolDay.Count, count + 1 );
			Assert.AreEqual( schoolDay[ 1 ], shortBreak1 );
			Assert.AreEqual( schoolDay.First, lesson1 );
			Assert.AreEqual( schoolDay.Start, start );
			Assert.AreEqual( shortBreak1.Start, schoolDay.Start.Add( lesson1.Duration ) );
			Assert.AreEqual( shortBreak1.Duration, ShortBreak.ShortBreakDuration );

			// last
			count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ShortBreak shortBreak2 = new ShortBreak( schoolDay.Start );
			schoolDay.Insert( schoolDay.Count, shortBreak2 );
			Assert.AreEqual( schoolDay.Count, count + 1 );
			Assert.AreEqual( schoolDay[ count ], shortBreak2 );
			Assert.AreEqual( schoolDay.Last, shortBreak2 );
			Assert.AreEqual( schoolDay.End, shortBreak2.End );
			Assert.AreEqual( shortBreak2.Start, end );
			Assert.AreEqual( shortBreak2.Duration, ShortBreak.ShortBreakDuration );
		} // InsertTest

		// ----------------------------------------------------------------------
		[Test]
		public void InsertTimeRangeTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( now );
			TimePeriodChain timePeriods = new TimePeriodChain( schoolDay );

			TimeRange timeRange = new TimeRange( schoolDay.Lesson1.Start, schoolDay.Lesson1.End );

			timePeriods.Add( timeRange );
			Assert.AreEqual( timePeriods.Last, timeRange );
		} // InsertTimeRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void ContainsTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			Assert.IsFalse( schoolDay.Contains( new TimeRange() ) );
			Assert.IsFalse( schoolDay.Contains( new TimeBlock() ) );

			Assert.IsTrue( schoolDay.Contains( schoolDay.Lesson1 ) );
			Assert.IsTrue( schoolDay.Contains( schoolDay.Break1 ) );
			Assert.IsTrue( schoolDay.Contains( schoolDay.Lesson2 ) );
			Assert.IsTrue( schoolDay.Contains( schoolDay.Break2 ) );
			Assert.IsTrue( schoolDay.Contains( schoolDay.Lesson3 ) );
			Assert.IsTrue( schoolDay.Contains( schoolDay.Break3 ) );
			Assert.IsTrue( schoolDay.Contains( schoolDay.Lesson4 ) );

			schoolDay.Remove( schoolDay.Lesson1 );
			Assert.IsFalse( schoolDay.Contains( schoolDay.Lesson1 ) );
		} // ContainsTest

		// ----------------------------------------------------------------------
		[Test]
		public void IndexOfTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			Assert.AreEqual( schoolDay.IndexOf( new TimeRange() ), -1 );
			Assert.AreEqual( schoolDay.IndexOf( new TimeBlock() ), -1 );

			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Lesson1 ), 0 );
			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Break1 ), 1 );
			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Lesson2 ), 2 );
			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Break2 ), 3 );
			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Lesson3 ), 4 );
			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Break3 ), 5 );
			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Lesson4 ), 6 );

			schoolDay.Remove( schoolDay.Lesson1 );
			Assert.AreEqual( schoolDay.IndexOf( schoolDay.Lesson1 ), -1 );
		} // IndexOfTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyToTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			ITimePeriod[] array1 = new ITimePeriod[ 0 ];
			timePeriods.CopyTo( array1, 0 );

			SchoolDay schoolDay = new SchoolDay();
			ITimePeriod[] array2 = new ITimePeriod[ schoolDay.Count ];
			schoolDay.CopyTo( array2, 0 );
			Assert.AreEqual( array2[ 0 ], schoolDay.Lesson1 );
			Assert.AreEqual( array2[ 1 ], schoolDay.Break1 );
			Assert.AreEqual( array2[ 2 ], schoolDay.Lesson2 );
			Assert.AreEqual( array2[ 3 ], schoolDay.Break2 );
			Assert.AreEqual( array2[ 4 ], schoolDay.Lesson3 );
			Assert.AreEqual( array2[ 5 ], schoolDay.Break3 );
			Assert.AreEqual( array2[ 6 ], schoolDay.Lesson4 );

			ITimePeriod[] array3 = new ITimePeriod[ schoolDay.Count + 3 ];
			schoolDay.CopyTo( array3, 3 );
			Assert.AreEqual( array3[ 3 ], schoolDay.Lesson1 );
			Assert.AreEqual( array3[ 4 ], schoolDay.Break1 );
			Assert.AreEqual( array3[ 5 ], schoolDay.Lesson2 );
			Assert.AreEqual( array3[ 6 ], schoolDay.Break2 );
			Assert.AreEqual( array3[ 7 ], schoolDay.Lesson3 );
			Assert.AreEqual( array3[ 8 ], schoolDay.Break3 );
			Assert.AreEqual( array3[ 9 ], schoolDay.Lesson4 );
		} // CopyToTest

		// ----------------------------------------------------------------------
		[Test]
		public void ClearTest()
		{
			TimePeriodChain timePeriods = new TimePeriodChain();
			Assert.AreEqual( timePeriods.Count, 0 );
			timePeriods.Clear();
			Assert.AreEqual( timePeriods.Count, 0 );

			SchoolDay schoolDay = new SchoolDay();
			Assert.AreEqual( schoolDay.Count, 7 );
			schoolDay.Clear();
			Assert.AreEqual( schoolDay.Count, 0 );
		} // ClearTest

		// ----------------------------------------------------------------------
		[Test]
		public void RemoveTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			// first
			int count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ITimePeriod removeItem = schoolDay.First;
			TimeSpan duration = schoolDay.Duration;
			schoolDay.Remove( removeItem );
			Assert.AreEqual( schoolDay.Count, count - 1 );
			Assert.AreNotEqual( schoolDay.First, removeItem );
			Assert.AreEqual( schoolDay.End, end );
			Assert.AreEqual( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// inside
			count = schoolDay.Count;
			duration = schoolDay.Duration;
			DateTime start = schoolDay.Start;
			ITimePeriod first = schoolDay.First;
			ITimePeriod last = schoolDay.Last;
			removeItem = schoolDay[ 1 ];
			schoolDay.Remove( removeItem );
			Assert.AreEqual( schoolDay.Count, count - 1 );
			Assert.AreNotEqual( schoolDay[ 1 ], removeItem );
			Assert.AreEqual( schoolDay.First, first );
			Assert.AreEqual( schoolDay.Start, start );
			Assert.AreEqual( schoolDay.Last, last );
			Assert.AreEqual( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// last
			count = schoolDay.Count;
			start = schoolDay.Start;
			duration = schoolDay.Duration;
			removeItem = schoolDay.Last;
			schoolDay.Remove( removeItem );
			Assert.AreEqual( schoolDay.Count, count - 1 );
			Assert.AreNotEqual( schoolDay.Last, removeItem );
			Assert.AreEqual( schoolDay.Start, start );
			Assert.AreEqual( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );
		} // RemoveTest

		// ----------------------------------------------------------------------
		[Test]
		public void RemoveAtTest()
		{
			SchoolDay schoolDay = new SchoolDay();

			// first
			int count = schoolDay.Count;
			DateTime end = schoolDay.End;
			ITimePeriod removeItem = schoolDay[ 0 ];
			TimeSpan duration = schoolDay.Duration;
			schoolDay.RemoveAt( 0 );
			Assert.AreEqual( schoolDay.Count, count - 1 );
			Assert.AreNotEqual( schoolDay[ 0 ], removeItem );
			Assert.AreEqual( schoolDay.End, end );
			Assert.AreEqual( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// inside
			count = schoolDay.Count;
			duration = schoolDay.Duration;
			DateTime start = schoolDay.Start;
			ITimePeriod first = schoolDay.First;
			ITimePeriod last = schoolDay.Last;
			removeItem = schoolDay[ 1 ];
			schoolDay.RemoveAt( 1 );
			Assert.AreEqual( schoolDay.Count, count - 1 );
			Assert.AreNotEqual( schoolDay[ 1 ], removeItem );
			Assert.AreEqual( schoolDay.First, first );
			Assert.AreEqual( schoolDay.Start, start );
			Assert.AreEqual( schoolDay.Last, last );
			Assert.AreEqual( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );

			// last
			count = schoolDay.Count;
			start = schoolDay.Start;
			duration = schoolDay.Duration;
			removeItem = schoolDay[ schoolDay.Count - 1 ];
			schoolDay.RemoveAt( schoolDay.Count - 1 );
			Assert.AreEqual( schoolDay.Count, count - 1 );
			Assert.AreNotEqual( schoolDay[ schoolDay.Count - 1 ], removeItem );
			Assert.AreEqual( schoolDay.Start, start );
			Assert.AreEqual( schoolDay.Duration, duration.Subtract( removeItem.Duration ) );
		} // RemoveAtTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsSamePeriodTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeRange manualRange = new TimeRange( schoolDay.Start, schoolDay.End );

			Assert.IsTrue( schoolDay.IsSamePeriod( schoolDay ) );
			Assert.IsTrue( schoolDay.IsSamePeriod( manualRange ) );
			Assert.IsTrue( manualRange.IsSamePeriod( schoolDay ) );

			Assert.IsFalse( schoolDay.IsSamePeriod( TimeBlock.Anytime ) );
			Assert.IsFalse( manualRange.IsSamePeriod( TimeBlock.Anytime ) );

			schoolDay.RemoveAt( 0 );
			Assert.IsFalse( schoolDay.IsSamePeriod( manualRange ) );
			Assert.IsFalse( manualRange.IsSamePeriod( schoolDay ) );
		} // IsSamePeriodTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasInsideTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.IsFalse( schoolDay.HasInside( testData.Before ) );
			Assert.IsFalse( schoolDay.HasInside( testData.StartTouching ) );
			Assert.IsFalse( schoolDay.HasInside( testData.StartInside ) );
			Assert.IsFalse( schoolDay.HasInside( testData.InsideStartTouching ) );
			Assert.IsTrue( schoolDay.HasInside( testData.EnclosingStartTouching ) );
			Assert.IsTrue( schoolDay.HasInside( testData.Enclosing ) );
			Assert.IsTrue( schoolDay.HasInside( testData.ExactMatch ) );
			Assert.IsTrue( schoolDay.HasInside( testData.EnclosingEndTouching ) );
			Assert.IsFalse( schoolDay.HasInside( testData.Inside ) );
			Assert.IsFalse( schoolDay.HasInside( testData.InsideEndTouching ) );
			Assert.IsFalse( schoolDay.HasInside( testData.EndInside ) );
			Assert.IsFalse( schoolDay.HasInside( testData.EndTouching ) );
			Assert.IsFalse( schoolDay.HasInside( testData.After ) );
		} // HasInsideTest

		// ----------------------------------------------------------------------
		[Test]
		public void IntersectsWithTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.IsFalse( schoolDay.IntersectsWith( testData.Before ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.StartTouching ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.StartInside ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.InsideStartTouching ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.EnclosingStartTouching ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.Enclosing ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.EnclosingEndTouching ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.ExactMatch ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.Inside ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.InsideEndTouching ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.EndInside ) );
			Assert.IsTrue( schoolDay.IntersectsWith( testData.EndTouching ) );
			Assert.IsFalse( schoolDay.IntersectsWith( testData.After ) );
		} // IntersectsWithTest

		// ----------------------------------------------------------------------
		[Test]
		public void OverlapsWithTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.IsFalse( schoolDay.OverlapsWith( testData.Before ) );
			Assert.IsFalse( schoolDay.OverlapsWith( testData.StartTouching ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.StartInside ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.InsideStartTouching ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.EnclosingStartTouching ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.Enclosing ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.EnclosingEndTouching ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.ExactMatch ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.Inside ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.InsideEndTouching ) );
			Assert.IsTrue( schoolDay.OverlapsWith( testData.EndInside ) );
			Assert.IsFalse( schoolDay.OverlapsWith( testData.EndTouching ) );
			Assert.IsFalse( schoolDay.OverlapsWith( testData.After ) );
		} // OverlapsWithTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetRelationTest()
		{
			SchoolDay schoolDay = new SchoolDay();
			TimeSpan offset = Duration.Second;
			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( schoolDay.Start, schoolDay.End, offset );

			Assert.AreEqual( schoolDay.GetRelation( testData.Before ), PeriodRelation.Before );
			Assert.AreEqual( schoolDay.GetRelation( testData.StartTouching ), PeriodRelation.StartTouching );
			Assert.AreEqual( schoolDay.GetRelation( testData.StartInside ), PeriodRelation.StartInside );
			Assert.AreEqual( schoolDay.GetRelation( testData.InsideStartTouching ), PeriodRelation.InsideStartTouching );
			Assert.AreEqual( schoolDay.GetRelation( testData.EnclosingStartTouching ), PeriodRelation.EnclosingStartTouching );
			Assert.AreEqual( schoolDay.GetRelation( testData.Enclosing ), PeriodRelation.Enclosing );
			Assert.AreEqual( schoolDay.GetRelation( testData.EnclosingEndTouching ), PeriodRelation.EnclosingEndTouching );
			Assert.AreEqual( schoolDay.GetRelation( testData.ExactMatch ), PeriodRelation.ExactMatch );
			Assert.AreEqual( schoolDay.GetRelation( testData.Inside ), PeriodRelation.Inside );
			Assert.AreEqual( schoolDay.GetRelation( testData.InsideEndTouching ), PeriodRelation.InsideEndTouching );
			Assert.AreEqual( schoolDay.GetRelation( testData.EndInside ), PeriodRelation.EndInside );
			Assert.AreEqual( schoolDay.GetRelation( testData.EndTouching ), PeriodRelation.EndTouching );
			Assert.AreEqual( schoolDay.GetRelation( testData.After ), PeriodRelation.After );
		} // GetRelationTest

	} // class TimePeriodChainTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
