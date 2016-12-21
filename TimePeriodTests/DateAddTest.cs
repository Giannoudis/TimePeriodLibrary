// -- FILE ------------------------------------------------------------------
// name       : DateAddTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.22
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
	public sealed class DateAddTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void NoPeriodsTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			DateAdd dateAdd = new DateAdd();
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 1, 0, 0, 0 ) ), test.Add( new TimeSpan( 1, 0, 0, 0 ) ) );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( -1, 0, 0, 0 ) ), test.Add( new TimeSpan( -1, 0, 0, 0 ) ) );
			Assert.AreEqual( dateAdd.Subtract( test, new TimeSpan( 1, 0, 0, 0 ) ), test.Subtract( new TimeSpan( 1, 0, 0, 0 ) ) );
			Assert.AreEqual( dateAdd.Subtract( test, new TimeSpan( -1, 0, 0, 0 ) ), test.Subtract( new TimeSpan( -1, 0, 0, 0 ) ) );
		} // NoPeriodsTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodLimitsAddTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 20 ), new DateTime( 2011, 4, 25 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( 2011, 4, 30 ), DateTime.MaxValue );
			DateAdd dateAdd = new DateAdd();
			dateAdd.ExcludePeriods.Add( timeRange1 );
			dateAdd.ExcludePeriods.Add( timeRange2 );

			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 8, 0, 0, 0 ) ), timeRange1.End );
			Assert.IsNull( dateAdd.Add( test, new TimeSpan( 20, 0, 0, 0 ) ) );
		} // PeriodLimitsAddTest

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodLimitsSubtractTest()
		{
			DateTime test = new DateTime( 2011, 4, 30 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 20 ), new DateTime( 2011, 4, 25 ) );
			TimeRange timeRange2 = new TimeRange( DateTime.MinValue, new DateTime( 2011, 4, 10 ) );
			DateAdd dateAdd = new DateAdd();
			dateAdd.ExcludePeriods.Add( timeRange1 );
			dateAdd.ExcludePeriods.Add( timeRange2 );

			Assert.AreEqual( dateAdd.Subtract( test, new TimeSpan( 5, 0, 0, 0 ) ), timeRange1.Start );
			Assert.IsNull( dateAdd.Subtract( test, new TimeSpan( 20, 0, 0, 0 ) ) );
		} // PeriodLimitsSubtractTest

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeOutsideMaxTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange = new TimeRange( new DateTime( 2011, 4, 20 ), DateTime.MaxValue );
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( timeRange );

			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), new DateTime( 2011, 4, 20 ) );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 1, 0, 0, 0 ) ), new DateTime( 2011, 4, 21 ) );

			Assert.IsNull( dateAdd.Subtract( test, TimeSpan.Zero ) );
			Assert.IsNull( dateAdd.Subtract( test, new TimeSpan( 1, 0, 0, 0 ) ) );
		} // IncludeOutsideMaxTest

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeOutsideMinTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange = new TimeRange( DateTime.MinValue, new DateTime( 2011, 4, 10 ) );
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( timeRange );

			Assert.IsNull( dateAdd.Add( test, TimeSpan.Zero ) );
			Assert.IsNull( dateAdd.Add( test, new TimeSpan( 1, 0, 0, 0 ) ) );

			Assert.AreEqual( dateAdd.Subtract( test, TimeSpan.Zero ), new DateTime( 2011, 4, 10 ) );
			Assert.AreEqual( dateAdd.Subtract( test, new TimeSpan( 1, 0, 0, 0 ) ), new DateTime( 2011, 4, 9 ) );
		} // IncludeOutsideMinTest

		// ----------------------------------------------------------------------
		[Test]
		public void AllExcludedTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange = new TimeRange( new DateTime( 2011, 4, 10 ), new DateTime( 2011, 4, 20 ) );
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( timeRange );
			dateAdd.ExcludePeriods.Add( timeRange );

			Assert.IsNull( dateAdd.Add( test, TimeSpan.Zero ) );
		} // AllExcludedTest

		// ----------------------------------------------------------------------
		[Test]
		public void AllExcluded2Test()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 10 ), new DateTime( 2011, 4, 20 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( 2011, 4, 10 ), new DateTime( 2011, 4, 15 ) );
			TimeRange timeRange3 = new TimeRange( new DateTime( 2011, 4, 15 ), new DateTime( 2011, 4, 20 ) );
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( timeRange1 );
			dateAdd.ExcludePeriods.Add( timeRange2 );
			dateAdd.ExcludePeriods.Add( timeRange3 );

			Assert.IsNull( dateAdd.Add( test, TimeSpan.Zero ) );
		} // AllExcluded2Test

		// ----------------------------------------------------------------------
		[Test]
		public void AllExcluded3Test()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 4, 10 ), new DateTime( 2011, 4, 20 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 4, 15 ), new DateTime( 2011, 4, 20 ) ) );

			Assert.IsNull( dateAdd.Add( test, new TimeSpan( 3, 0, 0, 0 ) ) );
		} // AllExcluded3Test

		// ----------------------------------------------------------------------
		[Test]
		public void PeriodMomentTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange = new TimeRange( test, test );
			DateAdd dateAdd = new DateAdd();
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			dateAdd.IncludePeriods.Add( timeRange );
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			dateAdd.ExcludePeriods.Add( timeRange );
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			dateAdd.IncludePeriods.Clear();
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			dateAdd.ExcludePeriods.Clear();
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
		} // PeriodMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange = new TimeRange( new DateTime( 2011, 4, 1 ), DateTime.MaxValue );
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( timeRange );

			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 1, 0, 0, 0 ) ), test.Add( new TimeSpan( 1, 0, 0, 0 ) ) );
		} // IncludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeSplitTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 1 ), new DateTime( 2011, 4, 15 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( 2011, 4, 20 ), new DateTime( 2011, 4, 24 ) );
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( timeRange1 );
			dateAdd.IncludePeriods.Add( timeRange2 );

			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 1, 0, 0, 0 ) ), test.Add( new TimeSpan( 1, 0, 0, 0 ) ) );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 5, 0, 0, 0 ) ), timeRange2.Start.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
			Assert.IsNull( dateAdd.Add( test, new TimeSpan( 15, 0, 0, 0 ) ) );
		} // IncludeSplitTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExcludeTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange = new TimeRange( new DateTime( 2011, 4, 15 ), new DateTime( 2011, 4, 20 ) );
			DateAdd dateAdd = new DateAdd();
			dateAdd.ExcludePeriods.Add( timeRange );

			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 2, 0, 0, 0 ) ), test.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 3, 0, 0, 0 ) ), timeRange.End );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 3, 0, 0, 0, 1 ) ), timeRange.End.Add( new TimeSpan( 0, 0, 0, 0, 1 ) ) );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 5, 0, 0, 0 ) ), timeRange.End.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
		} // ExcludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void ExcludeSplitTest()
		{
			DateTime test = new DateTime( 2011, 4, 12 );

			TimeRange timeRange1 = new TimeRange( new DateTime( 2011, 4, 15 ), new DateTime( 2011, 4, 20 ) );
			TimeRange timeRange2 = new TimeRange( new DateTime( 2011, 4, 22 ), new DateTime( 2011, 4, 25 ) );
			DateAdd dateAdd = new DateAdd();
			dateAdd.ExcludePeriods.Add( timeRange1 );
			dateAdd.ExcludePeriods.Add( timeRange2 );

			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 2, 0, 0, 0 ) ), test.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 3, 0, 0, 0 ) ), timeRange1.End );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 4, 0, 0, 0 ) ), timeRange1.End.Add( new TimeSpan( 1, 0, 0, 0 ) ) );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 5, 0, 0, 0 ) ), timeRange2.End );
			Assert.AreEqual( dateAdd.Add( test, new TimeSpan( 7, 0, 0, 0 ) ), timeRange2.End.Add( new TimeSpan( 2, 0, 0, 0 ) ) );
		} // ExcludeSplitTest

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeEqualsExcludeTest()
		{
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 10 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 10 ) ) );

			DateTime test = new DateTime( 2011, 3, 5 );
			Assert.IsNull( dateAdd.Add( test, TimeSpan.Zero ) );
			Assert.IsNull( dateAdd.Add( test, new TimeSpan( 1 ) ) );
			Assert.IsNull( dateAdd.Add( test, new TimeSpan( -1 ) ) );
			Assert.IsNull( dateAdd.Subtract( test, TimeSpan.Zero ) );
			Assert.IsNull( dateAdd.Subtract( test, new TimeSpan( 1 ) ) );
			Assert.IsNull( dateAdd.Subtract( test, new TimeSpan( -1 ) ) );
		} // IncludeEqualsExcludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeExcludeTest()
		{
			DateAdd dateAdd = new DateAdd();

			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 17 ), new DateTime( 2011, 4, 20 ) ) );

			// setup some periods to exclude
			dateAdd.ExcludePeriods.Add( new TimeRange(
				new DateTime( 2011, 3, 22 ), new DateTime( 2011, 3, 25 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange(
				new DateTime( 2011, 4, 1 ), new DateTime( 2011, 4, 7 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange(
				new DateTime( 2011, 4, 15 ), new DateTime( 2011, 4, 16 ) ) );

			// positive
			DateTime periodStart = new DateTime( 2011, 3, 19 );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Hours( 1 ) ), new DateTime( 2011, 3, 19, 1, 0, 0 ) );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 4 ) ), new DateTime( 2011, 3, 26, 0, 0, 0 ) );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 17 ) ), new DateTime( 2011, 4, 14 ) );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 20 ) ), new DateTime( 2011, 4, 18 ) );
			Assert.IsNull( dateAdd.Add( periodStart, Duration.Days( 22 ) ) );

			// negative
			DateTime periodEnd = new DateTime( 2011, 4, 18 );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Hours( -1 ) ), new DateTime( 2011, 4, 17, 23, 0, 0 ) );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -4 ) ), new DateTime( 2011, 4, 13 ) );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -17 ) ), new DateTime( 2011, 3, 22 ) );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -20 ) ), new DateTime( 2011, 3, 19 ) );
			Assert.IsNull( dateAdd.Add( periodEnd, Duration.Days( -22 ) ) );
		} // IncludeExcludeTest

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeExclude2Test()
		{
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 1 ), new DateTime( 2011, 3, 5 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 10 ) ) );
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) ) );
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 20 ), new DateTime( 2011, 3, 25 ) ) );

			DateTime periodStart = new DateTime( 2011, 3, 1 );
			DateTime periodEnd = new DateTime( 2011, 3, 25 );

			// add from start
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 1 ) ), new DateTime( 2011, 3, 2 ) );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 4 ) ), new DateTime( 2011, 3, 10 ) );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 5 ) ), new DateTime( 2011, 3, 11 ) );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 9 ) ), new DateTime( 2011, 3, 20 ) );
			Assert.AreEqual( dateAdd.Add( periodStart, Duration.Days( 10 ) ), new DateTime( 2011, 3, 21 ) );
			Assert.IsNull( dateAdd.Add( periodStart, Duration.Days( 15 ) ) );
			// add from end
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -1 ) ), new DateTime( 2011, 3, 24 ) );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -5 ) ), new DateTime( 2011, 3, 15 ) );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -6 ) ), new DateTime( 2011, 3, 14 ) );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -10 ) ), new DateTime( 2011, 3, 5 ) );
			Assert.AreEqual( dateAdd.Add( periodEnd, Duration.Days( -11 ) ), new DateTime( 2011, 3, 4 ) );
			Assert.IsNull( dateAdd.Add( periodEnd, Duration.Days( -15 ) ) );

			// subtract form end
			Assert.AreEqual( dateAdd.Subtract( periodEnd, Duration.Days( 1 ) ), new DateTime( 2011, 3, 24 ) );
			Assert.AreEqual( dateAdd.Subtract( periodEnd, Duration.Days( 5 ) ), new DateTime( 2011, 3, 15 ) );
			Assert.AreEqual( dateAdd.Subtract( periodEnd, Duration.Days( 6 ) ), new DateTime( 2011, 3, 14 ) );
			Assert.AreEqual( dateAdd.Subtract( periodEnd, Duration.Days( 10 ) ), new DateTime( 2011, 3, 5 ) );
			Assert.AreEqual( dateAdd.Subtract( periodEnd, Duration.Days( 11 ) ), new DateTime( 2011, 3, 4 ) );
			Assert.IsNull( dateAdd.Subtract( periodStart, Duration.Days( 15 ) ) );
			// subtract form start
			Assert.AreEqual( dateAdd.Subtract( periodStart, Duration.Days( -1 ) ), new DateTime( 2011, 3, 2 ) );
			Assert.AreEqual( dateAdd.Subtract( periodStart, Duration.Days( -4 ) ), new DateTime( 2011, 3, 10 ) );
			Assert.AreEqual( dateAdd.Subtract( periodStart, Duration.Days( -5 ) ), new DateTime( 2011, 3, 11 ) );
			Assert.AreEqual( dateAdd.Subtract( periodStart, Duration.Days( -9 ) ), new DateTime( 2011, 3, 20 ) );
			Assert.AreEqual( dateAdd.Subtract( periodStart, Duration.Days( -10 ) ), new DateTime( 2011, 3, 21 ) );
			Assert.IsNull( dateAdd.Subtract( periodStart, Duration.Days( -15 ) ) );
		} // IncludeExclude2Test

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeExclude3Test()
		{
			DateAdd dateAdd = new DateAdd();
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 10 ) ) );
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) ) );

			DateTime test = new DateTime( 2011, 3, 10 );
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), test );
			Assert.AreEqual( dateAdd.Add( test, Duration.Days( 1 ) ), new DateTime( 2011, 3, 11 ) );
			Assert.IsNull( dateAdd.Add( test, Duration.Days( 5 ) ) );
		} // IncludeExclude3Test

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeExclude4Test()
		{
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 15 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 20 ) ) );

			DateTime test = new DateTime( 2011, 3, 10 );
			Assert.IsNull( dateAdd.Add( test, TimeSpan.Zero ) );
			Assert.IsNull( dateAdd.Add( test, Duration.Days( 1 ) ) );
			Assert.IsNull( dateAdd.Add( test, Duration.Days( 5 ) ) );
		} // IncludeExclude4Test

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeExclude5Test()
		{
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 15 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 15 ), new DateTime( 2011, 3, 30 ) ) );

			DateTime test = new DateTime( 2011, 3, 10 );
			Assert.IsNull( dateAdd.Add( test, TimeSpan.Zero ) );
			Assert.IsNull( dateAdd.Add( test, new TimeSpan( 1 ) ) );
			Assert.IsNull( dateAdd.Add( test, new TimeSpan( -1 ) ) );
			Assert.IsNull( dateAdd.Subtract( test, TimeSpan.Zero ) );
			Assert.IsNull( dateAdd.Subtract( test, new TimeSpan( 1 ) ) );
			Assert.IsNull( dateAdd.Subtract( test, new TimeSpan( -1 ) ) );
		} // IncludeExclude5Test

		// ----------------------------------------------------------------------
		[Test]
		public void IncludeExclude6Test()
		{
			DateAdd dateAdd = new DateAdd();
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 10 ), new DateTime( 2011, 3, 20 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 5 ), new DateTime( 2011, 3, 12 ) ) );
			dateAdd.ExcludePeriods.Add( new TimeRange( new DateTime( 2011, 3, 18 ), new DateTime( 2011, 3, 30 ) ) );

			DateTime test = new DateTime( 2011, 3, 10 );
			Assert.AreEqual( dateAdd.Add( test, TimeSpan.Zero ), new DateTime( 2011, 3, 12 ) );
			Assert.AreEqual( dateAdd.Add( test, Duration.Days( 1 ) ), new DateTime( 2011, 3, 13 ) );
		} // IncludeExclude6Test

		// ----------------------------------------------------------------------
		[Test]
		public void ExcludePeriodTest()
		{
			DateAdd dateAdd = new DateAdd();

			// include periods
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2013, 1, 1, 8, 0, 0 ), new DateTime( 2013, 1, 1, 12, 0, 0 ) ) );
			dateAdd.IncludePeriods.Add( new TimeRange( new DateTime( 2013, 1, 1, 13, 0, 0 ), new DateTime( 2013, 1, 1, 17, 0, 0 ) ) );

			// exclude interval
			TimeInterval interval = new TimeInterval( TimeSpec.MinPeriodDate, new DateTime( 2013, 1, 1, 14, 0, 0 ),
																							 IntervalEdge.Closed, IntervalEdge.Open );
			dateAdd.ExcludePeriods.Add( interval );
			DateTime? result = dateAdd.Add( dateAdd.IncludePeriods.Start, TimeSpan.FromHours( 2 ), SeekBoundaryMode.Fill );
			Assert.IsNotNull( result );
			Assert.AreEqual( result, new DateTime( 2013, 1, 1, 16, 0, 0 ).AddTicks( -1 ) );

			// exclude time range
			dateAdd.ExcludePeriods.Clear();
			dateAdd.ExcludePeriods.Add( new TimeRange( interval.StartInterval, interval.EndInterval ) );
			result = dateAdd.Add( dateAdd.IncludePeriods.Start, TimeSpan.FromHours( 2 ), SeekBoundaryMode.Fill );
			Assert.IsNotNull( result );
			Assert.AreEqual( result, new DateTime( 2013, 1, 1, 16, 0, 0 ) );
		} // ExcludePeriodTest

	} // class DateAddTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
