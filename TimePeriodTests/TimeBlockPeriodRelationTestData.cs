// -- FILE ------------------------------------------------------------------
// name       : TimeBlockPeriodRelationTestData.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------
    public class TimeBlockPeriodRelationTestData
    {

        // ----------------------------------------------------------------------
        public TimeBlockPeriodRelationTestData(DateTime start, TimeSpan duration, TimeSpan offset)
        {
            if (offset <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            var end = start.Add(duration);
            Reference = new TimeBlock(start, duration, true);

            var beforeEnd = start.Subtract(offset);
            var beforeStart = beforeEnd.Subtract(Reference.Duration);
            var insideStart = start.Add(offset);
            var insideEnd = end.Subtract(offset);
            var afterStart = end.Add(offset);
            var afterEnd = afterStart.Add(Reference.Duration);

            After = new TimeBlock(beforeStart, beforeEnd, true);
            StartTouching = new TimeBlock(beforeStart, start, true);
            StartInside = new TimeBlock(beforeStart, insideStart, true);
            InsideStartTouching = new TimeBlock(start, afterStart, true);
            EnclosingStartTouching = new TimeBlock(start, insideEnd, true);
            Enclosing = new TimeBlock(insideStart, insideEnd, true);
            EnclosingEndTouching = new TimeBlock(insideStart, end, true);
            ExactMatch = new TimeBlock(start, end, true);
            Inside = new TimeBlock(beforeStart, afterEnd, true);
            InsideEndTouching = new TimeBlock(beforeStart, end, true);
            EndInside = new TimeBlock(insideEnd, afterEnd, true);
            EndTouching = new TimeBlock(end, afterEnd, true);
            Before = new TimeBlock(afterStart, afterEnd, true);

            allPeriods.Add(Reference);
            allPeriods.Add(After);
            allPeriods.Add(StartTouching);
            allPeriods.Add(StartInside);
            allPeriods.Add(InsideStartTouching);
            allPeriods.Add(EnclosingStartTouching);
            allPeriods.Add(Enclosing);
            allPeriods.Add(EnclosingEndTouching);
            allPeriods.Add(ExactMatch);
            allPeriods.Add(Inside);
            allPeriods.Add(InsideEndTouching);
            allPeriods.Add(EndInside);
            allPeriods.Add(EndTouching);
            allPeriods.Add(Before);
        } // TimeBlockPeriodRelationTestData

        // ----------------------------------------------------------------------
        public ICollection<ITimePeriod> AllPeriods => allPeriods; // AllPeriods

        // ----------------------------------------------------------------------
        public ITimeBlock Reference { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock Before { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock StartTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock StartInside { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock InsideStartTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock EnclosingStartTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock Inside { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock EnclosingEndTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock ExactMatch { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock Enclosing { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock InsideEndTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock EndInside { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock EndTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeBlock After { get; }

        // ----------------------------------------------------------------------
        // members
        private readonly List<ITimePeriod> allPeriods = new();

    } // class TimeBlockPeriodRelationTestData

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
