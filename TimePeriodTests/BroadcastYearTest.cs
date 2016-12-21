// -- FILE ------------------------------------------------------------------
// name       : BroadcastYearTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.09.30
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
	public sealed class BroadcastYearTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void SpecificMomentsTest()
		{
			Assert.AreEqual( 2011, new BroadcastYear( new DateTime( 2011, 12, 25 ) ).Year );
			Assert.AreEqual( 2012, new BroadcastYear( new DateTime( 2011, 12, 26 ) ).Year );

			Assert.AreEqual( 2012, new BroadcastYear( new DateTime( 2012, 12, 30 ) ).Year );
			Assert.AreEqual( 2013, new BroadcastYear( new DateTime( 2012, 12, 31 ) ).Year );

			Assert.AreEqual( 2013, new BroadcastYear( new DateTime( 2013, 12, 29 ) ).Year );
			Assert.AreEqual( 2014, new BroadcastYear( new DateTime( 2013, 12, 30 ) ).Year );

			Assert.AreEqual( 2014, new BroadcastYear( new DateTime( 2014, 12, 28 ) ).Year );
			Assert.AreEqual( 2015, new BroadcastYear( new DateTime( 2014, 12, 29 ) ).Year );
		} // SpecificMomentsTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearRangeTest()
		{
			Assert.AreEqual( new DateTime( 2010, 12, 27 ), new BroadcastYear( 2011 ).Start.Date );
			Assert.AreEqual( new DateTime( 2011, 12, 25 ), new BroadcastYear( 2011 ).End.Date );

			Assert.AreEqual( new DateTime( 2011, 12, 26 ), new BroadcastYear( 2012 ).Start.Date );
			Assert.AreEqual( new DateTime( 2012, 12, 30 ), new BroadcastYear( 2012 ).End.Date );

			Assert.AreEqual( new DateTime( 2012, 12, 31 ), new BroadcastYear( 2013 ).Start.Date );
			Assert.AreEqual( new DateTime( 2013, 12, 29 ), new BroadcastYear( 2013 ).End.Date );

			Assert.AreEqual( new DateTime( 2013, 12, 30 ), new BroadcastYear( 2014 ).Start.Date );
			Assert.AreEqual( new DateTime( 2014, 12, 28 ), new BroadcastYear( 2014 ).End.Date );
		} // YearRangeTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearMonthsTest()
		{
			Assert.AreEqual( 12, new BroadcastYear( 2011 ).GetMonths().Count );
			Assert.AreEqual( new DateTime( 2010, 12, 27 ), new BroadcastYear( 2011 ).GetMonths()[ 0 ].Start.Date );
			Assert.AreEqual( new DateTime( 2011, 12, 25 ), new BroadcastYear( 2011 ).GetMonths()[ 11 ].End.Date );

			Assert.AreEqual( 12, new BroadcastYear( 2012 ).GetMonths().Count );
			Assert.AreEqual( new DateTime( 2011, 12, 26 ), new BroadcastYear( 2012 ).GetMonths()[ 0 ].Start.Date );
			Assert.AreEqual( new DateTime( 2012, 12, 30 ), new BroadcastYear( 2012 ).GetMonths()[ 11 ].End.Date );

			Assert.AreEqual( 12, new BroadcastYear( 2013 ).GetMonths().Count );
			Assert.AreEqual( new DateTime( 2012, 12, 31 ), new BroadcastYear( 2013 ).GetMonths()[ 0 ].Start.Date );
			Assert.AreEqual( new DateTime( 2013, 12, 29 ), new BroadcastYear( 2013 ).GetMonths()[ 11 ].End.Date );
			
			Assert.AreEqual( 12, new BroadcastYear( 2014 ).GetMonths().Count );
			Assert.AreEqual( new DateTime( 2013, 12, 30 ), new BroadcastYear( 2014 ).GetMonths()[ 0 ].Start.Date );
			Assert.AreEqual( new DateTime( 2014, 12, 28 ), new BroadcastYear( 2014 ).GetMonths()[ 11 ].End.Date );
		} // YearMonthsTest

		// ----------------------------------------------------------------------
		[Test]
		public void YearWeeksTest()
		{
			Assert.AreEqual( 52, new BroadcastYear( 2011 ).GetWeeks().Count );
			Assert.AreEqual( 53, new BroadcastYear( 2012 ).GetWeeks().Count );
			Assert.AreEqual( 52, new BroadcastYear( 2013 ).GetWeeks().Count );
			Assert.AreEqual( 52, new BroadcastYear( 2014 ).GetWeeks().Count );
		} // YearWeeksTest

	} // class BroadcastYearTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
