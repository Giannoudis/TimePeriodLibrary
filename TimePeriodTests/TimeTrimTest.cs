// -- FILE ------------------------------------------------------------------
// name       : TimeTrimTest.cs
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
	public sealed class TimeTrimTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void MonthTest()
		{
			Assert.AreEqual( TimeTrim.Month( testDate ), new DateTime( testDate.Year, 1, 1 ) );
			Assert.AreEqual( TimeTrim.Month( testDate, 6 ), new DateTime( testDate.Year, 6, 1 ) );
			Assert.AreEqual( TimeTrim.Month( testDate, 6, 5 ), new DateTime( testDate.Year, 6, 5, 0, 0, 0 ) );
			Assert.AreEqual( TimeTrim.Month( testDate, 6, 5, 4 ), new DateTime( testDate.Year, 6, 5, 4, 0, 0 ) );
			Assert.AreEqual( TimeTrim.Month( testDate, 6, 5, 4, 23 ), new DateTime( testDate.Year, 6, 5, 4, 23, 0 ) );
			Assert.AreEqual( TimeTrim.Month( testDate, 6, 5, 4, 23, 55 ), new DateTime( testDate.Year, 6, 5, 4, 23, 55 ) );
			Assert.AreEqual( TimeTrim.Month( testDate, 6, 5, 4, 23, 55, 128 ), new DateTime( testDate.Year, 6, 5, 4, 23, 55, 128 ) );
		} // MonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void DayTest()
		{
			Assert.AreEqual( TimeTrim.Day( testDate ), new DateTime( testDate.Year, testDate.Month, 1 ) );
			Assert.AreEqual( TimeTrim.Day( testDate, 5 ), new DateTime( testDate.Year, testDate.Month, 5, 0, 0, 0 ) );
			Assert.AreEqual( TimeTrim.Day( testDate, 5, 4 ), new DateTime( testDate.Year, testDate.Month, 5, 4, 0, 0 ) );
			Assert.AreEqual( TimeTrim.Day( testDate, 5, 4, 23 ), new DateTime( testDate.Year, testDate.Month, 5, 4, 23, 0 ) );
			Assert.AreEqual( TimeTrim.Day( testDate, 5, 4, 23, 55 ), new DateTime( testDate.Year, testDate.Month, 5, 4, 23, 55 ) );
			Assert.AreEqual( TimeTrim.Day( testDate, 5, 4, 23, 55, 128 ), new DateTime( testDate.Year, testDate.Month, 5, 4, 23, 55, 128 ) );
		} // DayTest

		// ----------------------------------------------------------------------
		[Test]
		public void HourTest()
		{
			Assert.AreEqual( TimeTrim.Hour( testDate ), new DateTime( testDate.Year, testDate.Month, testDate.Day ) );
			Assert.AreEqual( TimeTrim.Hour( testDate, 4 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, 4, 0, 0 ) );
			Assert.AreEqual( TimeTrim.Hour( testDate, 4, 23 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, 4, 23, 0 ) );
			Assert.AreEqual( TimeTrim.Hour( testDate, 4, 23, 55 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, 4, 23, 55 ) );
			Assert.AreEqual( TimeTrim.Hour( testDate, 4, 23, 55, 128 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, 4, 23, 55, 128 ) );
		} // HourTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinuteTest()
		{
			Assert.AreEqual( TimeTrim.Minute( testDate ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, 0, 0 ) );
			Assert.AreEqual( TimeTrim.Minute( testDate, 23 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, 23, 0 ) );
			Assert.AreEqual( TimeTrim.Minute( testDate, 23, 55 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, 23, 55 ) );
			Assert.AreEqual( TimeTrim.Minute( testDate, 23, 55, 128 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, 23, 55, 128 ) );
		} // MinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void SecondTest()
		{
			Assert.AreEqual( TimeTrim.Second( testDate ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, testDate.Minute, 0 ) );
			Assert.AreEqual( TimeTrim.Second( testDate, 55 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, testDate.Minute, 55 ) );
			Assert.AreEqual( TimeTrim.Second( testDate, 55, 128 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, testDate.Minute, 55, 128 ) );
		} // SecondTest

		// ----------------------------------------------------------------------
		[Test]
		public void MillisecondTest()
		{
			Assert.AreEqual( TimeTrim.Millisecond( testDate ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, testDate.Minute, testDate.Second ) );
			Assert.AreEqual( TimeTrim.Millisecond( testDate, 128 ), new DateTime( testDate.Year, testDate.Month, testDate.Day, testDate.Hour, testDate.Minute, testDate.Second, 128 ) );
		} // MillisecondTest

		// ----------------------------------------------------------------------
		// members
		private DateTime testDate = new DateTime( 2000, 10, 2, 13, 45, 53, 673 );

	} // class TimeTrimTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
