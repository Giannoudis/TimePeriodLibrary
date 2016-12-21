// -- FILE ------------------------------------------------------------------
// name       : TestDataTest.cs
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
	public sealed class TestDataTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void TimeRangePeriodRelationTestDataTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan duration = Duration.Hour;
			DateTime start = now;
			DateTime end = start.Add( duration );
			TimeSpan offset = Duration.Minute;

			TimeRangePeriodRelationTestData testData = new TimeRangePeriodRelationTestData( start, end, offset );
			Assert.AreEqual( testData.Reference, new TimeRange( start, end, true ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Before ), PeriodRelation.Before );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartTouching ), PeriodRelation.StartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartInside ), PeriodRelation.StartInside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideStartTouching ), PeriodRelation.InsideStartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingStartTouching ), PeriodRelation.EnclosingStartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Inside ), PeriodRelation.Inside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingEndTouching ), PeriodRelation.EnclosingEndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.ExactMatch ), PeriodRelation.ExactMatch );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Enclosing ), PeriodRelation.Enclosing );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideEndTouching ), PeriodRelation.InsideEndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndInside ), PeriodRelation.EndInside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndTouching ), PeriodRelation.EndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.After ), PeriodRelation.After );
		} // TimeRangePeriodRelationTestDataTest

		// ----------------------------------------------------------------------
		[Test]
		public void TimeBlockPeriodRelationTestDataTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan duration = Duration.Hour;
			DateTime start = now;
			DateTime end = start.Add( duration );
			TimeSpan offset = Duration.Minute;

			TimeBlockPeriodRelationTestData testData = new TimeBlockPeriodRelationTestData( start, duration, offset );
			Assert.AreEqual( testData.Reference, new TimeBlock( start, end, true ) );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Before ), PeriodRelation.Before );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartTouching ), PeriodRelation.StartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.StartInside ), PeriodRelation.StartInside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideStartTouching ), PeriodRelation.InsideStartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingStartTouching ), PeriodRelation.EnclosingStartTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Inside ), PeriodRelation.Inside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EnclosingEndTouching ), PeriodRelation.EnclosingEndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.ExactMatch ), PeriodRelation.ExactMatch );
			Assert.AreEqual( testData.Reference.GetRelation( testData.Enclosing ), PeriodRelation.Enclosing );
			Assert.AreEqual( testData.Reference.GetRelation( testData.InsideEndTouching ), PeriodRelation.InsideEndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndInside ), PeriodRelation.EndInside );
			Assert.AreEqual( testData.Reference.GetRelation( testData.EndTouching ), PeriodRelation.EndTouching );
			Assert.AreEqual( testData.Reference.GetRelation( testData.After ), PeriodRelation.After );
		} // TimeBlockPeriodRelationTestDataTest

		// ----------------------------------------------------------------------
		[Test]
		public void SchoolDayLessonsTest()
		{
			TimeSpan lessonDuration = Duration.Minutes( 50 );
			TimeSpan shortBreakDuration = Duration.Minutes( 5 );
			TimeSpan largeBreakDuration = Duration.Minutes( 15 );
			DateTime now = ClockProxy.Clock.Now;
			DateTime todaySchoolStart = new DateTime( now.Year, now.Month, now.Day, 8, 0, 0 );

			TimeBlock lesson1 = new TimeBlock( todaySchoolStart, lessonDuration );
			TimeBlock break1 = new TimeBlock( lesson1.End, shortBreakDuration );
			TimeBlock lesson2 = new TimeBlock( break1.End, lessonDuration );
			TimeBlock break2 = new TimeBlock( lesson2.End, largeBreakDuration );
			TimeBlock lesson3 = new TimeBlock( break2.End, lessonDuration );
			TimeBlock break3 = new TimeBlock( lesson3.End, shortBreakDuration );
			TimeBlock lesson4 = new TimeBlock( break3.End, lessonDuration );

			Assert.AreEqual( lesson4.End,
				todaySchoolStart.Add( lessonDuration ).
				Add( shortBreakDuration ).
				Add( lessonDuration ).
				Add( largeBreakDuration ).
				Add( lessonDuration ).
				Add( shortBreakDuration ).
				Add( lessonDuration ) );
		} // SchoolDayLessonsTest

		// ----------------------------------------------------------------------
		[Test]
		public void SchoolDayTest()
		{
			DateTime start = ClockProxy.Clock.Now;
			SchoolDay schoolDay = new SchoolDay( start );

			Assert.AreEqual( schoolDay.Count, 7 );
			Assert.AreEqual( schoolDay.First, schoolDay.Lesson1 );
			Assert.AreEqual( schoolDay.Last, schoolDay.Lesson4 );

			Assert.AreEqual( schoolDay.Lesson1.Duration, Lesson.LessonDuration );
			Assert.AreEqual( schoolDay.Lesson1.Start, start );

			Assert.AreEqual( schoolDay.Break1.Duration, ShortBreak.ShortBreakDuration );
			Assert.AreEqual( schoolDay.Break1.Start, schoolDay.Lesson1.End );

			Assert.AreEqual( schoolDay.Lesson2.Duration, Lesson.LessonDuration );
			Assert.AreEqual( schoolDay.Lesson2.Start, schoolDay.Break1.End );

			Assert.AreEqual( schoolDay.Break2.Duration, LargeBreak.LargeBreakDuration );
			Assert.AreEqual( schoolDay.Break2.Start, schoolDay.Lesson2.End );

			Assert.AreEqual( schoolDay.Lesson3.Duration, Lesson.LessonDuration );
			Assert.AreEqual( schoolDay.Lesson3.Start, schoolDay.Break2.End );

			Assert.AreEqual( schoolDay.Break3.Duration, ShortBreak.ShortBreakDuration );
			Assert.AreEqual( schoolDay.Break3.Start, schoolDay.Lesson3.End );

			Assert.AreEqual( schoolDay.Lesson4.Duration, Lesson.LessonDuration );
			Assert.AreEqual( schoolDay.Lesson4.Start, schoolDay.Break3.End );

			Assert.AreEqual( schoolDay.Start, schoolDay.Lesson1.Start );
			Assert.AreEqual( schoolDay.End, schoolDay.Lesson4.End );
		} // SchoolDayTest

	} // class TestDataTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
