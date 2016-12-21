// -- FILE ------------------------------------------------------------------
// name       : TimeLineMomentCollectionTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.26
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
	public sealed class TimeLineMomentCollectionTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void HasOverlapsTest()
		{
			DateTime start1 = ClockProxy.Clock.Now.Date;
			DateTime end1 = start1.AddDays( 1 );
			DateTime start2 = end1;
			DateTime end2 = start2.AddDays( 1 );

			TimePeriodCollection periods = new TimePeriodCollection();
			TimeLine<TimeRange> timeLineMoments = new TimeLine<TimeRange>( periods, null, null );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start1, start1 ) );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );

			periods.Add( new TimeRange( start1, end1 ) );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start1, end1 ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start1.AddHours( 1 ), end1.AddHours( 1 ) ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start1.AddHours( -1 ), end1.AddHours( -1 ) ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start1.AddHours( -1 ), end1.AddHours( +1 ) ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start1.AddHours( +1 ), end1.AddHours( -1 ) ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start2, end2 ) );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Add( new TimeRange( start2, end2.AddHours( 1 ) ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );

			periods.Add( new TimeRange( start2, end2.AddHours( -1 ) ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );
			periods.RemoveAt( periods.Count - 1 );

			periods.Add( new TimeRange( start1, end2 ) );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );

			periods.RemoveAt( 0 );
			Assert.AreEqual( true, timeLineMoments.HasOverlaps() );

			periods.RemoveAt( 0 );
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );

			periods.Clear();
			Assert.AreEqual( false, timeLineMoments.HasOverlaps() );
		} // HasOverlapsTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasGapsTest()
		{
			DateTime start1 = ClockProxy.Clock.Now.Date;
			DateTime end1 = start1.AddDays( 1 );
			DateTime start2 = end1;
			DateTime end2 = start2.AddDays( 1 );
			DateTime start3 = end2;
			DateTime end3 = start3.AddDays( 1 );
			DateTime start4 = end3;
			DateTime end4 = start4.AddDays( 1 );

			TimePeriodCollection periods = new TimePeriodCollection();
			TimeLine<TimeRange> timeLineMoments = new TimeLine<TimeRange>( periods, null, null );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.Add( new TimeRange( start1, start1 ) );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );
			periods.RemoveAt( periods.Count - 1 );

			periods.Add( new TimeRange( start1, end1 ) );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.Add( new TimeRange( start1, end1 ) );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );
			periods.RemoveAt( periods.Count - 1 );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.Add( new TimeRange( start2, end2 ) );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.Add( new TimeRange( start1, end2 ) );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );
			periods.RemoveAt( periods.Count - 1 );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.Add( new TimeRange( start3, end3 ) );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.Add( new TimeRange( start4, end4 ) );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.RemoveAt( 2 ); // start/end 3
			Assert.AreEqual( true, timeLineMoments.HasGaps() );

			periods.RemoveAt( 1 ); // start/end 1
			Assert.AreEqual( true, timeLineMoments.HasGaps() );

			periods.RemoveAt( periods.Count - 1 ); // start/end 4
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.RemoveAt( 0 );
			Assert.AreEqual( false, timeLineMoments.HasGaps() );

			periods.Clear();
			Assert.AreEqual( false, timeLineMoments.HasGaps() );
		} // HasGapsTest

	} // class TimeLineMomentCollectionTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
