// -- FILE ------------------------------------------------------------------
// name       : TimeRangePeriodRelationTestData.cs
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
    public class TimeRangePeriodRelationTestData
    {

        // ----------------------------------------------------------------------
        public TimeRangePeriodRelationTestData(DateTime start, DateTime end, TimeSpan offset)
        {
            if (offset <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            Reference = new TimeRange(start, end, true);

            var beforeEnd = start.Subtract(offset);
            var beforeStart = beforeEnd.Subtract(Reference.Duration);
            var insideStart = start.Add(offset);
            var insideEnd = end.Subtract(offset);
            var afterStart = end.Add(offset);
            var afterEnd = afterStart.Add(Reference.Duration);

            After = new TimeRange(beforeStart, beforeEnd, true);
            StartTouching = new TimeRange(beforeStart, start, true);
            StartInside = new TimeRange(beforeStart, insideStart, true);
            InsideStartTouching = new TimeRange(start, afterStart, true);
            EnclosingStartTouching = new TimeRange(start, insideEnd, true);
            Enclosing = new TimeRange(insideStart, insideEnd, true);
            EnclosingEndTouching = new TimeRange(insideStart, end, true);
            ExactMatch = new TimeRange(start, end, true);
            Inside = new TimeRange(beforeStart, afterEnd, true);
            InsideEndTouching = new TimeRange(beforeStart, end, true);
            EndInside = new TimeRange(insideEnd, afterEnd, true);
            EndTouching = new TimeRange(end, afterEnd, true);
            Before = new TimeRange(afterStart, afterEnd, true);

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
        } // TimeRangePeriodRelationTestData

        // ----------------------------------------------------------------------
        public ICollection<ITimePeriod> AllPeriods => allPeriods; // AllPeriods

        // ----------------------------------------------------------------------
        public ITimeRange Reference { get; }

        // ----------------------------------------------------------------------
        public ITimeRange Before { get; }

        // ----------------------------------------------------------------------
        public ITimeRange StartTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeRange StartInside { get; }

        // ----------------------------------------------------------------------
        public ITimeRange InsideStartTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeRange EnclosingStartTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeRange Inside { get; }

        // ----------------------------------------------------------------------
        public ITimeRange EnclosingEndTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeRange ExactMatch { get; }

        // ----------------------------------------------------------------------
        public ITimeRange Enclosing { get; }

        // ----------------------------------------------------------------------
        public ITimeRange InsideEndTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeRange EndInside { get; }

        // ----------------------------------------------------------------------
        public ITimeRange EndTouching { get; }

        // ----------------------------------------------------------------------
        public ITimeRange After { get; }

        // ----------------------------------------------------------------------
        // members
        private readonly List<ITimePeriod> allPeriods = new();

    } // class TimeRangePeriodRelationTestData

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
