// -- FILE ------------------------------------------------------------------
// name       : TimePeriodComparerTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.23
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
	public sealed class TimePeriodComparerTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void StartComparerTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange left = new TimeRange( now, now.AddDays( 1 ) );
			TimeRange right = new TimeRange( now.AddDays( 1 ), now.AddDays( 2 ) );

			// base
			Assert.AreEqual( 0, left.CompareTo( left, TimePeriodStartComparer.Comparer ) );
			Assert.AreEqual( -1, left.CompareTo( right, TimePeriodStartComparer.Comparer ) );
			Assert.AreEqual( 1, right.CompareTo( left, TimePeriodStartComparer.Comparer ) );

			// revers
			Assert.AreEqual( 0, left.CompareTo( left, TimePeriodStartComparer.ReverseComparer ) );
			Assert.AreEqual( 1, left.CompareTo( right, TimePeriodStartComparer.ReverseComparer ) );
			Assert.AreEqual( -1, right.CompareTo( left, TimePeriodStartComparer.ReverseComparer ) );
		} // StartComparerTest

		// ----------------------------------------------------------------------
		[Test]
		public void EndComparerTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange left = new TimeRange( now, now.AddDays( 1 ) );
			TimeRange right = new TimeRange( now.AddDays( 1 ), now.AddDays( 2 ) );

			// base
			Assert.AreEqual( 0, left.CompareTo( left, TimePeriodEndComparer.Comparer ) );
			Assert.AreEqual( -1, left.CompareTo( right, TimePeriodEndComparer.Comparer ) );
			Assert.AreEqual( 1, right.CompareTo( left, TimePeriodEndComparer.Comparer ) );

			// revers
			Assert.AreEqual( 0, left.CompareTo( left, TimePeriodEndComparer.ReverseComparer ) );
			Assert.AreEqual( 1, left.CompareTo( right, TimePeriodEndComparer.ReverseComparer ) );
			Assert.AreEqual(- 1, right.CompareTo( left, TimePeriodEndComparer.ReverseComparer ) );
		} // EndComparerTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationComparerTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeRange left = new TimeRange( now, now.AddDays( 1 ) );
			TimeRange right = new TimeRange( now, now.AddDays( 2 ) );

			// base
			Assert.AreEqual( 0, left.CompareTo( left, TimePeriodDurationComparer.Comparer ) );
			Assert.AreEqual( -1, left.CompareTo( right, TimePeriodDurationComparer.Comparer ) );
			Assert.AreEqual( 1, right.CompareTo( left, TimePeriodDurationComparer.Comparer ) );

			// revers
			Assert.AreEqual( 0, left.CompareTo( left, TimePeriodDurationComparer.ReverseComparer ) );
			Assert.AreEqual( 1, left.CompareTo( right, TimePeriodDurationComparer.ReverseComparer ) );
			Assert.AreEqual( -1, right.CompareTo( left, TimePeriodDurationComparer.ReverseComparer ) );
		} // DurationComparerTest

	} // class TimePeriodComparerTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
