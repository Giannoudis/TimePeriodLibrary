// -- FILE ------------------------------------------------------------------
// name       : TimeFormatterTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.20
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class TimeFormatterTest : TestUnitBase
	{

#if (!NETCOREAPP1_1)
        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
		public void GetShortDateTest()
		{
			DateTime moment = new DateTime( 2013, 12, 15, 17, 34, 54, 900 );

			Assert.Equal( moment.ToShortDateString(), moment.ToString("d") );
			Assert.Equal( moment.ToShortTimeString(), moment.ToString( "t" ) );
			Assert.Equal( moment.ToLongTimeString(), moment.ToString( "T" ) );
		} // GetShortDateTest
#endif

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
		public void SameDayTest()
		{
			DateTime start = new DateTime( 2013, 12, 15, 17, 34, 54, 900 );
			DateTime end = new DateTime( 2013, 12, 15, 17, 35, 0, 0 );
			TimeSpan duration = new TimeSpan( 3, 4, 5 );

			TimeFormatter formatter = new TimeFormatter();
			string interval = formatter.GetInterval(
				start,
				end,
				IntervalEdge.Open,
				IntervalEdge.Open,
				duration );

			string expectedInterval = string.Concat(
				"(",
				formatter.GetDateTime( start ),
				formatter.StartEndSeparator,
				formatter.GetLongTime( end ),
				")",
				formatter.DurationSeparator,
				formatter.GetDuration( duration ) );
			Assert.Equal( interval, expectedInterval );
		} // SameDayTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
		public void DifferentDayWithTimeTest()
		{
			DateTime start = new DateTime( 2013, 12, 15, 17, 34, 54, 900 );
			DateTime end = new DateTime( 2013, 12, 16, 17, 35, 0, 0 );
			TimeSpan duration = new TimeSpan( 3, 4, 5 );

			TimeFormatter formatter = new TimeFormatter();
			string interval = formatter.GetInterval(
				start,
				end,
				IntervalEdge.Open,
				IntervalEdge.Open,
				duration );

			string expectedInterval = string.Concat(
				"(",
				formatter.GetDateTime( start ),
				formatter.StartEndSeparator,
				formatter.GetDateTime( end ),
				")",
				formatter.DurationSeparator,
				formatter.GetDuration( duration ) );
			Assert.Equal( interval, expectedInterval );
		} // DifferentDayWithTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeFormatter")]
        [Fact]
		public void DifferentDayWithoutTimeTest()
		{
			DateTime start = new DateTime( 2013, 12, 15 );
			DateTime end = new DateTime( 2013, 12, 16 );
			TimeSpan duration = new TimeSpan( 3, 4, 5 );

			TimeFormatter formatter = new TimeFormatter();
			string interval = formatter.GetInterval(
				start,
				end,
				IntervalEdge.Open,
				IntervalEdge.Open,
				duration );

			string expectedInterval = string.Concat(
				"(",
				formatter.GetShortDate( start ),
				formatter.StartEndSeparator,
				formatter.GetShortDate( end ),
				")",
				formatter.DurationSeparator,
				formatter.GetDuration( duration ) );
			Assert.Equal( interval, expectedInterval );
		} // DifferentDayWithoutTimeTest

	} // class TimeCompareTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
