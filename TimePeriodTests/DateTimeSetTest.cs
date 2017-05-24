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
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class DateTimeSetTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void DefaultConstructorTest()
		{
			DateTimeSet dateTimeSet = new DateTimeSet();

			Assert.Equal(0, dateTimeSet.Count);
			Assert.Null(dateTimeSet.Min);
			Assert.Null(dateTimeSet.Max);
			Assert.Null(dateTimeSet.Duration);
			Assert.True( dateTimeSet.IsEmpty );
			Assert.False( dateTimeSet.IsMoment );
			Assert.False( dateTimeSet.IsAnytime );
		} // DefaultConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
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

			Assert.Equal( dateTimeSet.Count, moments.Count );
			Assert.Equal( dateTimeSet.Min, previousCalendarYear );
			Assert.Equal( dateTimeSet.Max, nextCalendarYear );
			Assert.Equal( dateTimeSet.Duration, nextCalendarYear - previousCalendarYear );
			Assert.False( dateTimeSet.IsEmpty );
			Assert.False( dateTimeSet.IsMoment );
			Assert.False( dateTimeSet.IsAnytime );
		} // CopyConstructorTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void ItemTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			dateTimeSet.Add( nextCalendarYear );
			dateTimeSet.Add( currentCalendarYear );
			dateTimeSet.Add( previousCalendarYear );

			Assert.Equal( dateTimeSet[ 0 ], previousCalendarYear );
			Assert.Equal( dateTimeSet[ 1 ], currentCalendarYear );
			Assert.Equal( dateTimeSet[ 2 ], nextCalendarYear );
		} // ItemTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void MinTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Null(dateTimeSet.Min);

			dateTimeSet.Add( currentCalendarYear );
			Assert.Equal( dateTimeSet.Min, currentCalendarYear );

			dateTimeSet.Add( previousCalendarYear );
			Assert.Equal( dateTimeSet.Min, previousCalendarYear );

			dateTimeSet.Clear();
			Assert.Null(dateTimeSet.Min);
		} // MinTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void MaxTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Null(dateTimeSet.Max);

			dateTimeSet.Add( currentCalendarYear );
			Assert.Equal( dateTimeSet.Max, currentCalendarYear );

			dateTimeSet.Add( nextCalendarYear );
			Assert.Equal( dateTimeSet.Max, nextCalendarYear );

			dateTimeSet.Clear();
			Assert.Null(dateTimeSet.Max);
		} // MaxTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void DurationTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Null(dateTimeSet.Duration);

			dateTimeSet.Add( currentCalendarYear );
			Assert.Equal( dateTimeSet.Duration, TimeSpan.Zero );

			dateTimeSet.Add( nextCalendarYear );
			Assert.Equal( dateTimeSet.Duration, nextCalendarYear - currentCalendarYear );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.Equal( dateTimeSet.Duration, TimeSpan.Zero );

			dateTimeSet.Clear();
			Assert.Null(dateTimeSet.Duration);
		} // DurationTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void IsEmptyTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.True( dateTimeSet.IsEmpty );

			dateTimeSet.Add( currentCalendarYear );
			Assert.False( dateTimeSet.IsEmpty );

			dateTimeSet.Add( nextCalendarYear );
			Assert.False( dateTimeSet.IsEmpty );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.False( dateTimeSet.IsEmpty );

			dateTimeSet.Clear();
			Assert.True( dateTimeSet.IsEmpty );
		} // IsEmptyTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void IsMomentTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.False( dateTimeSet.IsMoment );

			dateTimeSet.Add( currentCalendarYear );
			Assert.True( dateTimeSet.IsMoment );

			dateTimeSet.Add( nextCalendarYear );
			Assert.False( dateTimeSet.IsMoment );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.True( dateTimeSet.IsMoment );

			dateTimeSet.Clear();
			Assert.False( dateTimeSet.IsMoment );
		} // IsMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void IsAnytimeTest()
		{
			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.False( dateTimeSet.IsAnytime );

			dateTimeSet.Add( DateTime.MinValue );
			Assert.False( dateTimeSet.IsAnytime );

			dateTimeSet.Add( DateTime.MaxValue );
			Assert.True( dateTimeSet.IsAnytime );

			dateTimeSet.Remove( DateTime.MinValue );
			Assert.False( dateTimeSet.IsAnytime );

			dateTimeSet.Clear();
			Assert.False( dateTimeSet.IsAnytime );
		} // IsAnytimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void CountTest()
		{
			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Equal(0, dateTimeSet.Count);

			dateTimeSet.Add( DateTime.MinValue );
			Assert.Equal(1, dateTimeSet.Count);

			dateTimeSet.Add( DateTime.MinValue );
			Assert.Equal(1, dateTimeSet.Count);

			dateTimeSet.Add( DateTime.MaxValue );
			Assert.Equal(2, dateTimeSet.Count);

			dateTimeSet.Add( DateTime.MaxValue );
			Assert.Equal(2, dateTimeSet.Count);

			dateTimeSet.Remove( DateTime.MinValue );
			Assert.Equal(1, dateTimeSet.Count);

			dateTimeSet.Clear();
			Assert.Equal(0, dateTimeSet.Count);
		} // CountTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void IndexOfTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();

			Assert.Equal( dateTimeSet.IndexOf( currentCalendarYear ), -1 );
			Assert.Equal( dateTimeSet.IndexOf( previousCalendarYear ), -1 );
			Assert.Equal( dateTimeSet.IndexOf( nextCalendarYear ), -1 );

			dateTimeSet.Add( previousCalendarYear );
			dateTimeSet.Add( nextCalendarYear );
			dateTimeSet.Add( currentCalendarYear );

			Assert.Equal(0, dateTimeSet.IndexOf( previousCalendarYear ));
			Assert.Equal(1, dateTimeSet.IndexOf( currentCalendarYear ));
			Assert.Equal(2, dateTimeSet.IndexOf( nextCalendarYear ));
		} // IndexOfTest


        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void FindPreviousTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Null(dateTimeSet.FindPrevious( now ));

			dateTimeSet.Add( currentCalendarYear );
			Assert.Equal( dateTimeSet.FindPrevious( now ), now == currentCalendarYear ? (DateTime?)null : currentCalendarYear );

			dateTimeSet.Add( previousCalendarYear );
			Assert.Equal( dateTimeSet.FindPrevious( now ), now == currentCalendarYear ? previousCalendarYear : currentCalendarYear );
			Assert.Equal( dateTimeSet.FindPrevious( currentCalendarYear ), previousCalendarYear );

			dateTimeSet.Remove( currentCalendarYear );
			Assert.Equal( dateTimeSet.FindPrevious( now ), previousCalendarYear );

			dateTimeSet.Remove( previousCalendarYear );
			Assert.Null(dateTimeSet.FindPrevious( now ));
		} // FindPreviousTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void FindNextTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Null(dateTimeSet.FindNext( now ));

			dateTimeSet.Add( currentCalendarYear );
			Assert.Null(dateTimeSet.FindNext( now ));

			dateTimeSet.Add( nextCalendarYear );
			Assert.Equal( dateTimeSet.FindNext( now ), nextCalendarYear );
			Assert.Equal( dateTimeSet.FindNext( currentCalendarYear ), nextCalendarYear );

			dateTimeSet.Remove( currentCalendarYear );
			Assert.Equal( dateTimeSet.FindNext( now ), nextCalendarYear );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.Null(dateTimeSet.FindNext( now ));
		} // FindNextTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void AddTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Equal(0, dateTimeSet.Count);

			dateTimeSet.Add( previousCalendarYear );
			Assert.Equal(1, dateTimeSet.Count);
			Assert.Equal( dateTimeSet.Min, previousCalendarYear );
			Assert.Equal( dateTimeSet.Max, previousCalendarYear );

			dateTimeSet.Add( nextCalendarYear );
			Assert.Equal(2, dateTimeSet.Count);
			Assert.Equal( dateTimeSet.Min, previousCalendarYear );
			Assert.Equal( dateTimeSet.Max, nextCalendarYear );

			dateTimeSet.Add( currentCalendarYear );
			Assert.Equal(3, dateTimeSet.Count);
			Assert.Equal( dateTimeSet.Min, previousCalendarYear );
			Assert.Equal( dateTimeSet.Max, nextCalendarYear );

			dateTimeSet.Add( previousCalendarYear );
			Assert.Equal(3, dateTimeSet.Count);
			Assert.False( dateTimeSet.Add( previousCalendarYear ) );
			dateTimeSet.Add( currentCalendarYear );
			Assert.Equal(3, dateTimeSet.Count);
			Assert.False( dateTimeSet.Add( currentCalendarYear ) );
			dateTimeSet.Add( nextCalendarYear );
			Assert.Equal(3, dateTimeSet.Count);
			Assert.False( dateTimeSet.Add( nextCalendarYear ) );
		} // AddTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
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

			Assert.Equal(0, dateTimeSet.Count);
			Assert.Null(dateTimeSet.Min);
			Assert.Null(dateTimeSet.Max);
			Assert.Null(dateTimeSet.Duration);
			Assert.True( dateTimeSet.IsEmpty );
			Assert.False( dateTimeSet.IsMoment );
			Assert.False( dateTimeSet.IsAnytime );

			dateTimeSet.AddAll( moments );

			Assert.Equal( dateTimeSet.Count, moments.Count );
			Assert.Equal( dateTimeSet.Min, previousCalendarYear );
			Assert.Equal( dateTimeSet.Max, nextCalendarYear );
			Assert.Equal( dateTimeSet.Duration, nextCalendarYear - previousCalendarYear );
			Assert.False( dateTimeSet.IsEmpty );
			Assert.False( dateTimeSet.IsMoment );
			Assert.False( dateTimeSet.IsAnytime );
		} // AddAllTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
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
			Assert.Equal(4, durations1.Count);
			Assert.Equal( durations1[ 0 ], previousCalendarMonth - previousCalendarYear );
			Assert.Equal( durations1[ 1 ], currentCalendarYear - previousCalendarMonth );
			Assert.Equal( durations1[ 2 ], nextCalendarMonth - currentCalendarYear );
			Assert.Equal( durations1[ 3 ], nextCalendarYear - nextCalendarMonth );

			IList<TimeSpan> durations2 = dateTimeSet.GetDurations( 1, 2 );
			Assert.Equal(2, durations2.Count);
			Assert.Equal( durations2[ 0 ], currentCalendarYear - previousCalendarMonth );
			Assert.Equal( durations2[ 1 ], nextCalendarMonth - currentCalendarYear );

			IList<TimeSpan> durations3 = dateTimeSet.GetDurations( 2, dateTimeSet.Count );
			Assert.Equal(2, durations3.Count);
			Assert.Equal( durations3[ 0 ], nextCalendarMonth - currentCalendarYear );
			Assert.Equal( durations3[ 1 ], nextCalendarYear - nextCalendarMonth );
		} // GetDurationsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void ClearTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.Equal(0, dateTimeSet.Count);
			dateTimeSet.Add( previousCalendarYear );
			Assert.Equal(1, dateTimeSet.Count);
			dateTimeSet.Add( nextCalendarYear );
			Assert.Equal(2, dateTimeSet.Count);
			dateTimeSet.Add( currentCalendarYear );
			Assert.Equal(3, dateTimeSet.Count);

			dateTimeSet.Clear();
			Assert.Equal(0, dateTimeSet.Count);
		} // ClearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void ContainsTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();
			Assert.False( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.False( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.False( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Add( previousCalendarYear );
			Assert.False( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.True( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.False( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Add( nextCalendarYear );
			Assert.False( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.True( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.True( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Add( currentCalendarYear );
			Assert.True( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.True( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.True( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Remove( nextCalendarYear );
			Assert.True( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.True( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.False( dateTimeSet.Contains( nextCalendarYear ) );

			dateTimeSet.Clear();
			Assert.False( dateTimeSet.Contains( currentCalendarYear ) );
			Assert.False( dateTimeSet.Contains( previousCalendarYear ) );
			Assert.False( dateTimeSet.Contains( nextCalendarYear ) );
		} // ContainsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
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
			Assert.Equal( array[ 0 ], previousCalendarYear );
			Assert.Equal( array[ 1 ], currentCalendarYear );
			Assert.Equal( array[ 2 ], nextCalendarYear );
		} // CopyToTest

        // ----------------------------------------------------------------------
        [Trait("Category", "DateTimeSet")]
        [Fact]
		public void RemoveTest()
		{
			DateTime currentCalendarYear = Now.CalendarYear;
			DateTime previousCalendarYear = currentCalendarYear.AddYears( -1 );
			DateTime nextCalendarYear = currentCalendarYear.AddYears( 1 );

			DateTimeSet dateTimeSet = new DateTimeSet();

			Assert.False( dateTimeSet.Contains( previousCalendarYear ) );

			dateTimeSet.Add( previousCalendarYear );
			Assert.True( dateTimeSet.Contains( previousCalendarYear ) );

			dateTimeSet.Remove( previousCalendarYear );
			Assert.False( dateTimeSet.Contains( previousCalendarYear ) );

			Assert.False( dateTimeSet.Contains( nextCalendarYear ) );
			dateTimeSet.Add( nextCalendarYear );
			Assert.True( dateTimeSet.Contains( nextCalendarYear ) );
			dateTimeSet.Remove( previousCalendarYear );
			Assert.True( dateTimeSet.Contains( nextCalendarYear ) );
		} // RemoveTest

	} // class DateTimeSetTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
