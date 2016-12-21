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
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class TimeFormatterTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void GetShortDateTest()
		{
			DateTime moment = new DateTime( 2013, 12, 15, 17, 34, 54, 900 );

			Assert.AreEqual( moment.ToShortDateString(), moment.ToString("d") );
			Assert.AreEqual( moment.ToShortTimeString(), moment.ToString( "t" ) );
			Assert.AreEqual( moment.ToLongTimeString(), moment.ToString( "T" ) );
		} // GetShortDateTest

		// ----------------------------------------------------------------------
		[Test]
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
			Assert.AreEqual( interval, expectedInterval );
		} // SameDayTest

		// ----------------------------------------------------------------------
		[Test]
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
			Assert.AreEqual( interval, expectedInterval );
		} // DifferentDayWithTimeTest

		// ----------------------------------------------------------------------
		[Test]
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
			Assert.AreEqual( interval, expectedInterval );
		} // DifferentDayWithoutTimeTest

	} // class TimeCompareTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
