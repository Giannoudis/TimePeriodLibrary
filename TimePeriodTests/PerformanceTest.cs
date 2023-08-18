// -- FILE ------------------------------------------------------------------
// name       : PerformanceTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2014.03.30
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2014 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------
    public class PerformanceTest
    {

        // ----------------------------------------------------------------------
        public void GapCalculator2(int count)
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 4, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0))
            };

            Console.Write("GapCalculator 2 ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                gapCalculator.GetGaps(excludePeriods, limits);
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // GapCalculator2

        // ----------------------------------------------------------------------
        public void GapCalculator4(int count)
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 4, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange( new DateTime( 2011, 4, 01, 00, 00, 0 ), new DateTime( 2011, 4, 12, 00, 00, 0 ) ),
                new TimeRange( new DateTime( 2011, 4, 12, 00, 00, 0 ), new DateTime( 2011, 4, 18, 00, 00, 0 ) )
            };

            Console.Write("GapCalculator 4 ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                gapCalculator.GetGaps(excludePeriods, limits);
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // GapCalculator4

        // ----------------------------------------------------------------------
        public void GapCalculator8(int count)
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 6, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection
            {
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)),
                new TimeRange( new DateTime( 2011, 4, 01, 00, 00, 0 ), new DateTime( 2011, 4, 12, 00, 00, 0 ) ),
                new TimeRange( new DateTime( 2011, 4, 12, 00, 00, 0 ), new DateTime( 2011, 4, 18, 00, 00, 0 ) ),
                new TimeRange( new DateTime( 2011, 4, 29, 00, 00, 0 ), new DateTime( 2011, 4, 30, 00, 00, 0 ) ),
                new TimeRange( new DateTime( 2011, 4, 29, 00, 00, 0 ), new DateTime( 2011, 4, 30, 00, 00, 0 ) ),
                new TimeRange( new DateTime( 2011, 5, 01, 00, 00, 0 ), new DateTime( 2011, 5, 12, 00, 00, 0 ) ),
                new TimeRange( new DateTime( 2011, 5, 12, 00, 00, 0 ), new DateTime( 2011, 5, 18, 00, 00, 0 ) )
            };

            Console.Write("GapCalculator 8 ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                gapCalculator.GetGaps(excludePeriods, limits);
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // GapCalculator8

        // ----------------------------------------------------------------------
        public void GapCalculator16(int count)
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 6, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection();
            for (var i = 0; i < 2; i++)
            {
                excludePeriods.Add(new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 01, 00, 00, 0), new DateTime(2011, 4, 12, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 12, 00, 00, 0), new DateTime(2011, 4, 18, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 29, 00, 00, 0), new DateTime(2011, 4, 30, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 29, 00, 00, 0), new DateTime(2011, 4, 30, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 5, 01, 00, 00, 0), new DateTime(2011, 5, 12, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 5, 12, 00, 00, 0), new DateTime(2011, 5, 18, 00, 00, 0)));
            }

            Console.Write("GapCalculator 16 ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                gapCalculator.GetGaps(excludePeriods, limits);
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // GapCalculator16

        // ----------------------------------------------------------------------
        public void GapCalculator32(int count)
        {
            var limits = new TimeRange(new DateTime(2011, 3, 29), new DateTime(2011, 6, 1));
            var gapCalculator = new TimeGapCalculator<TimeRange>();

            var excludePeriods = new TimePeriodCollection();
            for (var i = 0; i < 4; i++)
            {
                excludePeriods.Add(new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 3, 30, 00, 00, 0), new DateTime(2011, 3, 31, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 01, 00, 00, 0), new DateTime(2011, 4, 12, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 12, 00, 00, 0), new DateTime(2011, 4, 18, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 29, 00, 00, 0), new DateTime(2011, 4, 30, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 4, 29, 00, 00, 0), new DateTime(2011, 4, 30, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 5, 01, 00, 00, 0), new DateTime(2011, 5, 12, 00, 00, 0)));
                excludePeriods.Add(new TimeRange(new DateTime(2011, 5, 12, 00, 00, 0), new DateTime(2011, 5, 18, 00, 00, 0)));
            }

            Console.Write("GapCalculator 32 ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                gapCalculator.GetGaps(excludePeriods, limits);
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // GapCalculator32

        // ----------------------------------------------------------------------
        public void Combiner5(int count)
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period3 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 15));
            var period4 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 20));
            var period5 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 25));

            var periodCombiner = new TimePeriodCombiner<TimeRange>();

            Console.Write("Combiner 5 ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                periodCombiner.CombinePeriods(new TimePeriodCollection { period1, period2, period3, period4, period5 });
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // Combiner5

        // ----------------------------------------------------------------------
        public void Intersector4(int count)
        {
            var period1 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 10));
            var period2 = new TimeRange(new DateTime(2011, 3, 01), new DateTime(2011, 3, 05));
            var period3 = new TimeRange(new DateTime(2011, 3, 04), new DateTime(2011, 3, 06));
            var period4 = new TimeRange(new DateTime(2011, 3, 05), new DateTime(2011, 3, 10));

            var periodIntersector = new TimePeriodIntersector<TimeRange>();

            Console.Write("Intersector 4 ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                periodIntersector.IntersectPeriods(new TimePeriodCollection { period1, period2, period3, period4 }, false);
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // 	Intersector4

        // ----------------------------------------------------------------------
        public void TimeGapCalculator(int count)
        {
            // simulation of some reservations
            var reservations = new TimePeriodCollection
            {
                new Days(2011, 3, 7, 2),
                new Days(2011, 3, 16, 2)
            };

            // the overall search range
            var searchLimits = new CalendarTimeRange(new DateTime(2011, 3, 4), new DateTime(2011, 3, 21));

            // search the largest free time block
            Console.Write("TimeGapCalculator ({0:#,0}): ", count);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                FindLargestFreeTimeBlock(reservations, searchLimits);
            }
            stopwatch.Stop();
            Console.WriteLine(" {0} ms", stopwatch.ElapsedMilliseconds);
        } // TimeGapCalculator

        // ----------------------------------------------------------------------
        private ICalendarTimeRange FindLargestFreeTimeBlock(IEnumerable<ITimePeriod> reservations,
            ITimePeriod searchLimits = null, bool excludeWeekends = true)
        {
            var bookedPeriods = new TimePeriodCollection(reservations);

            if (searchLimits == null)
            {
                searchLimits = bookedPeriods; // use boundary of reservations
            }

            if (excludeWeekends)
            {
                var currentWeek = new Week(searchLimits.Start);
                var lastWeek = new Week(searchLimits.End);
                do
                {
                    var days = currentWeek.GetDays();
                    foreach (Day day in days)
                    {
                        if (!searchLimits.HasInside(day))
                        {
                            continue; // outside of the search scope
                        }
                        if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                        {
                            bookedPeriods.Add(day); // // exclude weekend day
                        }
                    }
                    currentWeek = currentWeek.GetNextWeek();
                } while (currentWeek.Start < lastWeek.Start);
            }

            // calculate the gaps using the time calendar as period mapper
            var gapCalculator = new TimeGapCalculator<TimeRange>(new TimeCalendar());
            var freeTimes = gapCalculator.GetGaps(bookedPeriods, searchLimits);
            if (freeTimes.Count == 0)
            {
                return null;
            }

            freeTimes.SortByDuration(); // move the largest gap to the start
            return new CalendarTimeRange(freeTimes[0]);
        } // FindLargestFreeTimeBlock

    } // class PerformanceTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
