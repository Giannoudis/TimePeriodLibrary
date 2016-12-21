// -- FILE ------------------------------------------------------------------
// name       : TimeSpecTest.cs
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
	public sealed class TimeSpecTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void WeeksPerTimeSpecTest()
		{
			// relations
			Assert.AreEqual( TimeSpec.MonthsPerYear, 12 );
			Assert.AreEqual( TimeSpec.HalfyearsPerYear, 2 );
			Assert.AreEqual( TimeSpec.QuartersPerYear, 4 );
			Assert.AreEqual( TimeSpec.QuartersPerHalfyear, TimeSpec.QuartersPerYear / TimeSpec.HalfyearsPerYear );
			Assert.AreEqual( TimeSpec.MaxWeeksPerYear, 53 );
			Assert.AreEqual( TimeSpec.MonthsPerHalfyear, TimeSpec.MonthsPerYear / TimeSpec.HalfyearsPerYear );
			Assert.AreEqual( TimeSpec.MonthsPerQuarter, TimeSpec.MonthsPerYear / TimeSpec.QuartersPerYear );
			Assert.AreEqual( TimeSpec.MaxDaysPerMonth, 31 );
			Assert.AreEqual( TimeSpec.DaysPerWeek, 7 );
			Assert.AreEqual( TimeSpec.HoursPerDay, 24 );
			Assert.AreEqual( TimeSpec.MinutesPerHour, 60 );
			Assert.AreEqual( TimeSpec.SecondsPerMinute, 60 );
			Assert.AreEqual( TimeSpec.MillisecondsPerSecond, 1000 );

			// halfyear
			Assert.AreEqual( TimeSpec.FirstHalfyearMonths.Length, TimeSpec.MonthsPerHalfyear );
			Assert.AreEqual( TimeSpec.FirstHalfyearMonths[ 0 ], YearMonth.January );
			Assert.AreEqual( TimeSpec.FirstHalfyearMonths[ 1 ], YearMonth.February );
			Assert.AreEqual( TimeSpec.FirstHalfyearMonths[ 2 ], YearMonth.March );
			Assert.AreEqual( TimeSpec.FirstHalfyearMonths[ 3 ], YearMonth.April );
			Assert.AreEqual( TimeSpec.FirstHalfyearMonths[ 4 ], YearMonth.May );
			Assert.AreEqual( TimeSpec.FirstHalfyearMonths[ 5 ], YearMonth.June );

			Assert.AreEqual( TimeSpec.SecondHalfyearMonths.Length, TimeSpec.MonthsPerHalfyear );
			Assert.AreEqual( TimeSpec.SecondHalfyearMonths[ 0 ], YearMonth.July );
			Assert.AreEqual( TimeSpec.SecondHalfyearMonths[ 1 ], YearMonth.August );
			Assert.AreEqual( TimeSpec.SecondHalfyearMonths[ 2 ], YearMonth.September );
			Assert.AreEqual( TimeSpec.SecondHalfyearMonths[ 3 ], YearMonth.October );
			Assert.AreEqual( TimeSpec.SecondHalfyearMonths[ 4 ], YearMonth.November );
			Assert.AreEqual( TimeSpec.SecondHalfyearMonths[ 5 ], YearMonth.December );

			// quarter
			Assert.AreEqual( TimeSpec.FirstQuarterMonthIndex, 1 );
			Assert.AreEqual( TimeSpec.SecondQuarterMonthIndex,  TimeSpec.FirstQuarterMonthIndex +  TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( TimeSpec.ThirdQuarterMonthIndex,  TimeSpec.SecondQuarterMonthIndex +  TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( TimeSpec.FourthQuarterMonthIndex,  TimeSpec.ThirdQuarterMonthIndex +  TimeSpec.MonthsPerQuarter );

			Assert.AreEqual( TimeSpec.FirstQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( TimeSpec.FirstQuarterMonths[ 0 ], YearMonth.January );
			Assert.AreEqual( TimeSpec.FirstQuarterMonths[ 1 ], YearMonth.February );
			Assert.AreEqual( TimeSpec.FirstQuarterMonths[ 2 ], YearMonth.March );

			Assert.AreEqual( TimeSpec.SecondQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( TimeSpec.SecondQuarterMonths[ 0 ], YearMonth.April );
			Assert.AreEqual( TimeSpec.SecondQuarterMonths[ 1 ], YearMonth.May );
			Assert.AreEqual( TimeSpec.SecondQuarterMonths[ 2 ], YearMonth.June );

			Assert.AreEqual( TimeSpec.ThirdQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( TimeSpec.ThirdQuarterMonths[ 0 ], YearMonth.July );
			Assert.AreEqual( TimeSpec.ThirdQuarterMonths[ 1 ], YearMonth.August );
			Assert.AreEqual( TimeSpec.ThirdQuarterMonths[ 2 ], YearMonth.September );

			Assert.AreEqual( TimeSpec.FourthQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.AreEqual( TimeSpec.FourthQuarterMonths[ 0 ], YearMonth.October );
			Assert.AreEqual( TimeSpec.FourthQuarterMonths[ 1 ], YearMonth.November );
			Assert.AreEqual( TimeSpec.FourthQuarterMonths[ 2 ], YearMonth.December );

			// duration
			Assert.AreEqual( TimeSpec.NoDuration, TimeSpan.Zero );
			Assert.AreEqual( TimeSpec.MinPositiveDuration, new TimeSpan( 1 ) );
			Assert.AreEqual( TimeSpec.MinNegativeDuration, new TimeSpan( -1 ) );

			// period
			Assert.AreEqual( TimeSpec.MinPeriodDate, DateTime.MinValue );
			Assert.AreEqual( TimeSpec.MaxPeriodDate, DateTime.MaxValue );
			Assert.AreEqual( TimeSpec.MinPeriodDuration, TimeSpan.Zero );
			Assert.AreEqual( TimeSpec.MaxPeriodDuration, TimeSpec.MaxPeriodDate - TimeSpec.MinPeriodDate );

		} // WeeksPerTimeSpecTest

	} // class TimeSpecTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
