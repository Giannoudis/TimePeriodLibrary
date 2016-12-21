// -- FILE ------------------------------------------------------------------
// name       : DateTimeSetTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class DateTimeSetTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void DefaultConstructorTest()
		{
			DateTimeSet dateTimeSet = new DateTimeSet();

			Assert.AreEqual( dateTimeSet.Count, 0 );
			Assert.AreEqual( dateTimeSet.Min, null );
			Assert.AreEqual( dateTimeSet.Max, null );
			Assert.AreEqual( dateTimeSet.Duration, null );
			Assert.IsTrue( dateTimeSet.IsEmpty );
			Assert.IsFalse( dateTimeSet.IsMoment );
			Assert.IsFalse( dateTimeSet.IsAnytime );
		} // DefaultConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyConstructorTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			List<DateTime> moments = new List<DateTime>();
			moments.Add( nextCalendarYear );
			moments.Add( previousCalendarYear );
			moments.Add( currentCalendarYear );

			DateTimeSet dateTimeSet = new DateTimeSet( moments );

			Assert.AreEqual( dateTimeSet.Count, moments.Count );
			Assert.AreEqual( dateTimeSet.Min, previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Max, nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Duration, nextCalendarYear - previousCalendarYear );
			Assert.IsFalse( dateTimeSet.IsEmpty );
			Assert.IsFalse( dateTimeSet.IsMoment );
			Assert.IsFalse( dateTimeSet.IsAnytime );
		} // CopyConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void ItemTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			dateTimeSet.Add( nextCalendarYear );
			dateTimeSet.Add( currentCalendarYear );
			dateTimeSet.Add( previousCalendarYear );

			Assert.AreEqual( dateTimeSet[ 0 ], previousCalendarYear );
			Assert.AreEqual( dateTimeSet[ 1 ], currentCalendarYear );
			Assert.AreEqual( dateTimeSet[ 2 ], nextCalendarYear );
		} // ItemTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.Min, null );

			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.Min, currentCalendarYear );

			dateTimeSet.Add( previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Min, previousCalendarYear );

			dateTimeSet.Clear();
			Assert.AreEqual( dateTimeSet.Min, null );
		} // MinTest

		// ----------------------------------------------------------------------
		[Test]
		public void MaxTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.Max, null );

			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.Max, currentCalendarYear );

			dateTimeSet.Add( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Max, nextCalendarYear );

			dateTimeSet.Clear();
			Assert.AreEqual( dateTimeSet.Max, null );
		} // MaxTest

		// ----------------------------------------------------------------------
		[Test]
		public void DurationTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.Duration, null );

			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.Duration, TimeSpan.Zero );

			dateTimeSet.Add( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Duration, nextCalendarYear - currentCalendarYear );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Duration, TimeSpan.Zero );

			dateTimeSet.Clear();
			Assert.AreEqual( dateTimeSet.Duration, null );
		} // DurationTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsEmptyTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.IsTrue( dateTimeSet.IsEmpty );

			dateTimeSet.Add( currentCalendarYear );
			Assert.IsFalse( dateTimeSet.IsEmpty );

			dateTimeSet.Add( nextCalendarYear );
			Assert.IsFalse( dateTimeSet.IsEmpty );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.IsFalse( dateTimeSet.IsEmpty );

			dateTimeSet.Clear();
			Assert.IsTrue( dateTimeSet.IsEmpty );
		} // IsEmptyTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsMomentTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.IsFalse( dateTimeSet.IsMoment );

			dateTimeSet.Add( currentCalendarYear );
			Assert.IsTrue( dateTimeSet.IsMoment );

			dateTimeSet.Add( nextCalendarYear );
			Assert.IsFalse( dateTimeSet.IsMoment );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.IsTrue( dateTimeSet.IsMoment );

			dateTimeSet.Clear();
			Assert.IsFalse( dateTimeSet.IsMoment );
		} // IsMomentTest

		// ----------------------------------------------------------------------
		[Test]
		public void IsAnytimeTest()
		{
			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.IsFalse( dateTimeSet.IsAnytime );

			dateTimeSet.Add( DateTime.MinValue );
			Assert.IsFalse( dateTimeSet.IsAnytime );

			dateTimeSet.Add( DateTime.MaxValue );
			Assert.IsTrue( dateTimeSet.IsAnytime );

			dateTimeSet.Remove( DateTime.MinValue );
			Assert.IsFalse( dateTimeSet.IsAnytime );

			dateTimeSet.Clear();
			Assert.IsFalse( dateTimeSet.IsAnytime );
		} // IsAnytimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void CountTest()
		{
			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.Count, 0 );

			dateTimeSet.Add( DateTime.MinValue );
			Assert.AreEqual( dateTimeSet.Count, 1 );

			dateTimeSet.Add( DateTime.MinValue );
			Assert.AreEqual( dateTimeSet.Count, 1 );

			dateTimeSet.Add( DateTime.MaxValue );
			Assert.AreEqual( dateTimeSet.Count, 2 );

			dateTimeSet.Add( DateTime.MaxValue );
			Assert.AreEqual( dateTimeSet.Count, 2 );

			dateTimeSet.Remove( DateTime.MinValue );
			Assert.AreEqual( dateTimeSet.Count, 1 );

			dateTimeSet.Clear();
			Assert.AreEqual( dateTimeSet.Count, 0 );
		} // CountTest

		// ----------------------------------------------------------------------
		[Test]
		public void IndexOfTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();

			Assert.AreEqual( dateTimeSet.IndexOf( currentCalendarYear ), -1 );
			Assert.AreEqual( dateTimeSet.IndexOf( previousCalendarYear ), -1 );
			Assert.AreEqual( dateTimeSet.IndexOf( nextCalendarYear ), -1 );

			dateTimeSet.Add( previousCalendarYear );
			dateTimeSet.Add( nextCalendarYear );
			dateTimeSet.Add( currentCalendarYear );

			Assert.AreEqual( dateTimeSet.IndexOf( previousCalendarYear ), 0 );
			Assert.AreEqual( dateTimeSet.IndexOf( currentCalendarYear ), 1 );
			Assert.AreEqual( dateTimeSet.IndexOf( nextCalendarYear ), 2 );
		} // IndexOfTest


		// ----------------------------------------------------------------------
		[Test]
		public void FindPreviousTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.FindPrevious( now ), null );

			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.FindPrevious( now ), now == currentCalendarYear ? (DateTime?)null : currentCalendarYear );

			dateTimeSet.Add( previousCalendarYear );
			Assert.AreEqual( dateTimeSet.FindPrevious( now ), now == currentCalendarYear ? previousCalendarYear : currentCalendarYear );
			Assert.AreEqual( dateTimeSet.FindPrevious( currentCalendarYear ), previousCalendarYear );

			dateTimeSet.Remove( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.FindPrevious( now ), previousCalendarYear );

			dateTimeSet.Remove( previousCalendarYear );
			Assert.AreEqual( dateTimeSet.FindPrevious( now ), null );
		} // FindPreviousTest

		// ----------------------------------------------------------------------
		[Test]
		public void FindNextTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.FindNext( now ), null );

			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.FindNext( now ), null );

			dateTimeSet.Add( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.FindNext( now ), nextCalendarYear );
			Assert.AreEqual( dateTimeSet.FindNext( currentCalendarYear ), nextCalendarYear );

			dateTimeSet.Remove( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.FindNext( now ), nextCalendarYear );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.FindNext( now ), null );
		} // FindNextTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.Count, 0 );

			dateTimeSet.Add( previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 1 );
			Assert.AreEqual( dateTimeSet.Min, previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Max, previousCalendarYear );

			dateTimeSet.Add( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 2 );
			Assert.AreEqual( dateTimeSet.Min, previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Max, nextCalendarYear );

			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 3 );
			Assert.AreEqual( dateTimeSet.Min, previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Max, nextCalendarYear );

			dateTimeSet.Add( previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 3 );
			Assert.IsFalse( dateTimeSet.Add( previousCalendarYear ) );
			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 3 );
			Assert.IsFalse( dateTimeSet.Add( currentCalendarYear ) );
			dateTimeSet.Add( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 3 );
			Assert.IsFalse( dateTimeSet.Add( nextCalendarYear ) );
		} // AddTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddAllTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			List<DateTime> moments = new List<DateTime>();
			moments.Add( nextCalendarYear );
			moments.Add( currentCalendarYear );
			moments.Add( previousCalendarYear );

			DateTimeSet dateTimeSet = new DateTimeSet();

			Assert.AreEqual( dateTimeSet.Count, 0 );
			Assert.AreEqual( dateTimeSet.Min, null );
			Assert.AreEqual( dateTimeSet.Max, null );
			Assert.AreEqual( dateTimeSet.Duration, null );
			Assert.IsTrue( dateTimeSet.IsEmpty );
			Assert.IsFalse( dateTimeSet.IsMoment );
			Assert.IsFalse( dateTimeSet.IsAnytime );

			dateTimeSet.AddAll( moments );

			Assert.AreEqual( dateTimeSet.Count, moments.Count );
			Assert.AreEqual( dateTimeSet.Min, previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Max, nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Duration, nextCalendarYear - previousCalendarYear );
			Assert.IsFalse( dateTimeSet.IsEmpty );
			Assert.IsFalse( dateTimeSet.IsMoment );
			Assert.IsFalse( dateTimeSet.IsAnytime );
		} // AddAllTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDurationsTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarMonth = currentCalendarYear.AddMonths( -1 );
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarMonth = currentCalendarYear.AddMonths( 1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			dateTimeSet.Add( currentCalendarYear );
			dateTimeSet.Add( nextCalendarYear );
			dateTimeSet.Add( nextCalendarMonth );
			dateTimeSet.Add( previousCalendarYear );
			dateTimeSet.Add( previousCalendarMonth );

			IList<TimeSpan> durations1 = dateTimeSet.GetDurations( 0, dateTimeSet.Count );
			Assert.AreEqual( durations1.Count, 4 );
			Assert.AreEqual( durations1[ 0 ], previousCalendarMonth - previousCalendarYear );
			Assert.AreEqual( durations1[ 1 ], currentCalendarYear - previousCalendarMonth );
			Assert.AreEqual( durations1[ 2 ], nextCalendarMonth - currentCalendarYear );
			Assert.AreEqual( durations1[ 3 ], nextCalendarYear - nextCalendarMonth );

			IList<TimeSpan> durations2 = dateTimeSet.GetDurations( 1, 2 );
			Assert.AreEqual( durations2.Count, 2 );
			Assert.AreEqual( durations2[ 0 ], currentCalendarYear - previousCalendarMonth );
			Assert.AreEqual( durations2[ 1 ], nextCalendarMonth - currentCalendarYear );

			IList<TimeSpan> durations3 = dateTimeSet.GetDurations( 2, dateTimeSet.Count );
			Assert.AreEqual( durations3.Count, 2 );
			Assert.AreEqual( durations3[ 0 ], nextCalendarMonth - currentCalendarYear );
			Assert.AreEqual( durations3[ 1 ], nextCalendarYear - nextCalendarMonth );
		} // GetDurationsTest

		// ----------------------------------------------------------------------
		[Test]
		public void ClearTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.AreEqual( dateTimeSet.Count, 0 );
			dateTimeSet.Add( previousCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 1 );
			dateTimeSet.Add( nextCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 2 );
			dateTimeSet.Add( currentCalendarYear );
			Assert.AreEqual( dateTimeSet.Count, 3 );

			dateTimeSet.Clear();
			Assert.AreEqual( dateTimeSet.Count, 0 );
		} // ClearTest

		// ----------------------------------------------------------------------
		[Test]
		public void ContainsTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.IsFalse( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.IsFalse( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.IsFalse( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Add( previousCalendarYear );
			Assert.IsFalse( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.IsTrue( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.IsFalse( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Add( nextCalendarYear );
			Assert.IsFalse( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.IsTrue( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.IsTrue( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Add( currentCalendarYear );
			Assert.IsTrue( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.IsTrue( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.IsTrue( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.IsTrue( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.IsTrue( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.IsFalse( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Clear();
			Assert.IsFalse( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.IsFalse( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.IsFalse( dateTimeSet.Contains( nextCalendarYear ) );
		} // ContainsTest

		// ----------------------------------------------------------------------
		[Test]
		public void CopyToTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			dateTimeSet.Add( nextCalendarYear );
			dateTimeSet.Add( currentCalendarYear );
			dateTimeSet.Add( previousCalendarYear );

			DateTime[] array = new DateTime[ 3 ];
			dateTimeSet.CopyTo( array, 0 );
			Assert.AreEqual( array[ 0 ], previousCalendarYear );
			Assert.AreEqual( array[ 1 ], currentCalendarYear );
			Assert.AreEqual( array[ 2 ], nextCalendarYear );
		} // CopyToTest

		// ----------------------------------------------------------------------
		[Test]
		public void RemoveTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();

			Assert.IsFalse( dateTimeSet.Contains( previousCalendarYear ) );

			dateTimeSet.Add( previousCalendarYear );
			Assert.IsTrue( dateTimeSet.Contains( previousCalendarYear ) );

			dateTimeSet.Remove( previousCalendarYear );
			Assert.IsFalse( dateTimeSet.Contains( previousCalendarYear ) );

			Assert.IsFalse( dateTimeSet.Contains( nextCalendarYear ) );
			dateTimeSet.Add( nextCalendarYear );
			Assert.IsTrue( dateTimeSet.Contains( nextCalendarYear ) );
			dateTimeSet.Remove( previousCalendarYear );
			Assert.IsTrue( dateTimeSet.Contains( nextCalendarYear ) );
		} // RemoveTest

	} // class DateTimeSetTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
