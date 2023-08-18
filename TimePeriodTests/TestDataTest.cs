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
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class TestDataTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "TestData")]
        [Fact]
        public void TimeRangePeriodRelationTestDataTest()
        {
            var now = ClockProxy.Clock.Now;
            var duration = Duration.Hour;
            var start = now;
            var end = start.Add(duration);
            var offset = Duration.Minute;

            var testData = new TimeRangePeriodRelationTestData(start, end, offset);
            Assert.Equal(testData.Reference, new TimeRange(start, end, true));
            Assert.Equal(PeriodRelation.Before, testData.Reference.GetRelation(testData.Before));
            Assert.Equal(PeriodRelation.StartTouching, testData.Reference.GetRelation(testData.StartTouching));
            Assert.Equal(PeriodRelation.StartInside, testData.Reference.GetRelation(testData.StartInside));
            Assert.Equal(PeriodRelation.InsideStartTouching, testData.Reference.GetRelation(testData.InsideStartTouching));
            Assert.Equal(PeriodRelation.EnclosingStartTouching, testData.Reference.GetRelation(testData.EnclosingStartTouching));
            Assert.Equal(PeriodRelation.Inside, testData.Reference.GetRelation(testData.Inside));
            Assert.Equal(PeriodRelation.EnclosingEndTouching, testData.Reference.GetRelation(testData.EnclosingEndTouching));
            Assert.Equal(PeriodRelation.ExactMatch, testData.Reference.GetRelation(testData.ExactMatch));
            Assert.Equal(PeriodRelation.Enclosing, testData.Reference.GetRelation(testData.Enclosing));
            Assert.Equal(PeriodRelation.InsideEndTouching, testData.Reference.GetRelation(testData.InsideEndTouching));
            Assert.Equal(PeriodRelation.EndInside, testData.Reference.GetRelation(testData.EndInside));
            Assert.Equal(PeriodRelation.EndTouching, testData.Reference.GetRelation(testData.EndTouching));
            Assert.Equal(PeriodRelation.After, testData.Reference.GetRelation(testData.After));
        } // TimeRangePeriodRelationTestDataTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TestData")]
        [Fact]
        public void TimeBlockPeriodRelationTestDataTest()
        {
            var now = ClockProxy.Clock.Now;
            var duration = Duration.Hour;
            var start = now;
            var end = start.Add(duration);
            var offset = Duration.Minute;

            var testData = new TimeBlockPeriodRelationTestData(start, duration, offset);
            Assert.Equal(testData.Reference, new TimeBlock(start, end, true));
            Assert.Equal(PeriodRelation.Before, testData.Reference.GetRelation(testData.Before));
            Assert.Equal(PeriodRelation.StartTouching, testData.Reference.GetRelation(testData.StartTouching));
            Assert.Equal(PeriodRelation.StartInside, testData.Reference.GetRelation(testData.StartInside));
            Assert.Equal(PeriodRelation.InsideStartTouching, testData.Reference.GetRelation(testData.InsideStartTouching));
            Assert.Equal(PeriodRelation.EnclosingStartTouching, testData.Reference.GetRelation(testData.EnclosingStartTouching));
            Assert.Equal(PeriodRelation.Inside, testData.Reference.GetRelation(testData.Inside));
            Assert.Equal(PeriodRelation.EnclosingEndTouching, testData.Reference.GetRelation(testData.EnclosingEndTouching));
            Assert.Equal(PeriodRelation.ExactMatch, testData.Reference.GetRelation(testData.ExactMatch));
            Assert.Equal(PeriodRelation.Enclosing, testData.Reference.GetRelation(testData.Enclosing));
            Assert.Equal(PeriodRelation.InsideEndTouching, testData.Reference.GetRelation(testData.InsideEndTouching));
            Assert.Equal(PeriodRelation.EndInside, testData.Reference.GetRelation(testData.EndInside));
            Assert.Equal(PeriodRelation.EndTouching, testData.Reference.GetRelation(testData.EndTouching));
            Assert.Equal(PeriodRelation.After, testData.Reference.GetRelation(testData.After));
        } // TimeBlockPeriodRelationTestDataTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TestData")]
        [Fact]
        public void SchoolDayLessonsTest()
        {
            var lessonDuration = Duration.Minutes(50);
            var shortBreakDuration = Duration.Minutes(5);
            var largeBreakDuration = Duration.Minutes(15);
            var now = ClockProxy.Clock.Now;
            var todaySchoolStart = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);

            var lesson1 = new TimeBlock(todaySchoolStart, lessonDuration);
            var break1 = new TimeBlock(lesson1.End, shortBreakDuration);
            var lesson2 = new TimeBlock(break1.End, lessonDuration);
            var break2 = new TimeBlock(lesson2.End, largeBreakDuration);
            var lesson3 = new TimeBlock(break2.End, lessonDuration);
            var break3 = new TimeBlock(lesson3.End, shortBreakDuration);
            var lesson4 = new TimeBlock(break3.End, lessonDuration);

            Assert.Equal(lesson4.End,
                todaySchoolStart.Add(lessonDuration).
                Add(shortBreakDuration).
                Add(lessonDuration).
                Add(largeBreakDuration).
                Add(lessonDuration).
                Add(shortBreakDuration).
                Add(lessonDuration));
        } // SchoolDayLessonsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TestData")]
        [Fact]
        public void SchoolDayTest()
        {
            var start = ClockProxy.Clock.Now;
            var schoolDay = new SchoolDay(start);

            Assert.Equal(7, schoolDay.Count);
            Assert.Equal(schoolDay.First, schoolDay.Lesson1);
            Assert.Equal(schoolDay.Last, schoolDay.Lesson4);

            Assert.Equal(schoolDay.Lesson1.Duration, Lesson.LessonDuration);
            Assert.Equal(schoolDay.Lesson1.Start, start);

            Assert.Equal(schoolDay.Break1.Duration, ShortBreak.ShortBreakDuration);
            Assert.Equal(schoolDay.Break1.Start, schoolDay.Lesson1.End);

            Assert.Equal(schoolDay.Lesson2.Duration, Lesson.LessonDuration);
            Assert.Equal(schoolDay.Lesson2.Start, schoolDay.Break1.End);

            Assert.Equal(schoolDay.Break2.Duration, LargeBreak.LargeBreakDuration);
            Assert.Equal(schoolDay.Break2.Start, schoolDay.Lesson2.End);

            Assert.Equal(schoolDay.Lesson3.Duration, Lesson.LessonDuration);
            Assert.Equal(schoolDay.Lesson3.Start, schoolDay.Break2.End);

            Assert.Equal(schoolDay.Break3.Duration, ShortBreak.ShortBreakDuration);
            Assert.Equal(schoolDay.Break3.Start, schoolDay.Lesson3.End);

            Assert.Equal(schoolDay.Lesson4.Duration, Lesson.LessonDuration);
            Assert.Equal(schoolDay.Lesson4.Start, schoolDay.Break3.End);

            Assert.Equal(schoolDay.Start, schoolDay.Lesson1.Start);
            Assert.Equal(schoolDay.End, schoolDay.Lesson4.End);
        } // SchoolDayTest

    } // class TestDataTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
