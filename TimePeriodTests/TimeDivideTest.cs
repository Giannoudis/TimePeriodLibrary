// -- FILE ------------------------------------------------------------------
// name       : WeekTest.cs
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

    public sealed class TimeDivideTest : TestUnitBase
    {

        [Fact]
        public void TimeDivide()
        {
            var timePeriod = new Month(2023, YearMonth.August);

            // half day
            int partCount = 62;
            var partSize = (timePeriod.Duration.Ticks / partCount) + 1;
            for (var i = 0; i < partCount; i++)
            {
                var start = timePeriod.Start.AddTicks(i * partSize);
                var end = start.AddTicks(partSize - 1);
                var timeRange = new TimeRange(start, end);
                Console.WriteLine($"Part {i + 1}: {timeRange}");
            }
        }
    }
}
