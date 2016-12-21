// -- FILE ------------------------------------------------------------------
// name       : HourRangeTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.08.26
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class HourRangeTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorHourTest()
		{
			HourRange hourRange = new HourRange( 1 );

			Assert.AreEqual( hourRange.Start, new Time( 1 ) );
			Assert.AreEqual( hourRange.End, new Time( 1 ) );
		} // ConstructorHourTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTimeTest()
		{
			HourRange hourRange = new HourRange( new Time( 1, 30 ), new Time( 2, 45 ) );

			Assert.AreEqual( hourRange.Start, new Time( 1, 30 ) );
			Assert.AreEqual( hourRange.End, new Time( 2, 45 ) );
		} // ConstructorTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTimeSortTest()
		{
			HourRange hourRange = new HourRange( new Time( 2, 45 ), new Time( 1, 30 ) );

			Assert.AreEqual( hourRange.Start, new Time( 1, 30 ) );
			Assert.AreEqual( hourRange.End, new Time( 2, 45 ) );
		} // ConstructorTimeSortTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsMomentTest()
		{
			HourRange hourRange = new HourRange( new Time( 2, 45, 33, 876 ), new Time( 2, 45, 33, 876 ) );

			Assert.IsTrue( hourRange.IsMoment );
		} // IsMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsNotMomentTest()
		{
			Assert.IsTrue( new HourRange( new Time(), new Time() ).IsMoment );
			Assert.IsTrue( new HourRange( new Time( 24 ), new Time( 24 ) ).IsMoment );
			Assert.IsTrue( new HourRange( new Time( 0, 24 ), new Time( 0, 24 ) ).IsMoment );
			Assert.IsFalse( new HourRange( new Time( 2, 45, 33, 876 ), new Time( 2, 45, 33, 877 ) ).IsMoment );
		} // IsNotMomentTest

	} // class HourRangeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
