// -- FILE ------------------------------------------------------------------
// name       : TimeCalendarTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	
	public sealed class TimeCalendarTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void StartOffsetTest()
		{
			Assert.Equal( new TimeCalendar().StartOffset, TimeCalendar.DefaultStartOffset );

			TimeSpan offset = Duration.Second;
			TimeCalendar timeCalendar = new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = offset,
				EndOffset = TimeSpan.Zero
			} );
			Assert.Equal( timeCalendar.StartOffset, offset );
		} // StartOffsetTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void EndOffsetTest()
		{
			Assert.Equal( new TimeCalendar().EndOffset, TimeCalendar.DefaultEndOffset );

			TimeSpan offset = Duration.Second.Negate();
			TimeCalendar timeCalendar = new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = TimeSpan.Zero,
				EndOffset = offset
			} );
			Assert.Equal( timeCalendar.EndOffset, offset );
		} // EndOffsetTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void InvalidOffset1Test()
		{
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TimeCalendar( new TimeCalendarConfig
			    {
				    StartOffset = Duration.Second.Negate(),
				    EndOffset = TimeSpan.Zero
			    } )));
		} // InvalidOffset1Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void InvalidOffset2Test()
		{
            Assert.NotNull(Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TimeCalendar( new TimeCalendarConfig
			    {
				    StartOffset = TimeSpan.Zero,
				    EndOffset = Duration.Second
			    } )));
		} // InvalidOffset2Test

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void CultureTest()
		{
			Assert.Equal( new TimeCalendar().Culture, CultureInfo.CurrentCulture );
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).Culture, culture );
			}
		} // CultureTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void FirstDayOfWeekTest()
		{
			Assert.Equal( new TimeCalendar().FirstDayOfWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek );
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).FirstDayOfWeek, culture.DateTimeFormat.FirstDayOfWeek );
			}
		} // FirstDayOfWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void MapStartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapStart( now ), now );
				Assert.Equal( TimeCalendar.New( culture, offset, TimeSpan.Zero ).MapStart( now ), now.Add( offset ) );

				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapStart( testDate ), testDate );
				Assert.Equal( TimeCalendar.New( culture, offset, TimeSpan.Zero ).MapStart( testDate ), testDate.Add( offset ) );
			}
		} // MapStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void UnmapStartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapStart( now ), now );
				Assert.Equal( TimeCalendar.New( culture, offset, TimeSpan.Zero ).UnmapStart( now ), now.Subtract( offset ) );

				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapStart( testDate ), testDate );
				Assert.Equal( TimeCalendar.New( culture, offset, TimeSpan.Zero ).UnmapStart( testDate ), testDate.Subtract( offset ) );
			}
		} // UnmapStartTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void MapEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second.Negate();
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapEnd( now ), now );
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, offset ).MapEnd( now ), now.Add( offset ) );

				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapEnd( testDate ), testDate );
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, offset ).MapEnd( testDate ), testDate.Add( offset ) );
			}
		} // MapEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void UnmapEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second.Negate();
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapEnd( now ), now );
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, offset ).UnmapEnd( now ), now.Subtract( offset ) );

				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapEnd( testDate ), testDate );
				Assert.Equal( TimeCalendar.New( culture, TimeSpan.Zero, offset ).UnmapEnd( testDate ), testDate.Subtract( offset ) );
			}
		} // UnmapEndTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetYearTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetYear( now ), culture.Calendar.GetYear( now ) );
				Assert.Equal( TimeCalendar.New( culture ).GetYear( testDate ), culture.Calendar.GetYear( testDate ) );
			}
		} // GetYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetMonth( now ), culture.Calendar.GetMonth( now ) );
				Assert.Equal( TimeCalendar.New( culture ).GetMonth( testDate ), culture.Calendar.GetMonth( testDate ) );
			}
		} // GetMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetHourTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetHour( now ), culture.Calendar.GetHour( now ) );
				Assert.Equal( TimeCalendar.New( culture ).GetHour( testDate ), culture.Calendar.GetHour( testDate ) );
			}
		} // GetHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetMinuteTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetMinute( now ), culture.Calendar.GetMinute( now ) );
				Assert.Equal( TimeCalendar.New( culture ).GetMinute( testDate ), culture.Calendar.GetMinute( testDate ) );
			}
		} // GetMinuteTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetDayOfMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetDayOfMonth( now ), culture.Calendar.GetDayOfMonth( now ) );
				Assert.Equal( TimeCalendar.New( culture ).GetDayOfMonth( testDate ), culture.Calendar.GetDayOfMonth( testDate ) );
			}
		} // GetDayOfMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetDayOfWeekTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetDayOfWeek( now ), culture.Calendar.GetDayOfWeek( now ) );
				Assert.Equal( TimeCalendar.New( culture ).GetDayOfWeek( testDate ), culture.Calendar.GetDayOfWeek( testDate ) );
			}
		} // GetDayOfWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetDaysInMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetDaysInMonth( now.Year, now.Month ), culture.Calendar.GetDaysInMonth( now.Year, now.Month ) );
				Assert.Equal( TimeCalendar.New( culture ).GetDaysInMonth( testDate.Year, testDate.Month ), culture.Calendar.GetDaysInMonth( testDate.Year, testDate.Month ) );
			}
		} // GetDaysInMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetMonthNameTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetMonthName( now.Month ), culture.DateTimeFormat.GetMonthName( now.Month ) );
				Assert.Equal( TimeCalendar.New( culture ).GetMonthName( testDate.Month ), culture.DateTimeFormat.GetMonthName( testDate.Month ) );
			}
		} // GetMonthNameTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetDayNameTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.Equal( TimeCalendar.New( culture ).GetDayName( now.DayOfWeek ), culture.DateTimeFormat.GetDayName( now.DayOfWeek ) );
				Assert.Equal( TimeCalendar.New( culture ).GetDayName( testDate.DayOfWeek ), culture.DateTimeFormat.GetDayName( testDate.DayOfWeek ) );
			}
		} // GetDayNameTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetWeekOfYearTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				foreach ( CalendarWeekRule weekRule in Enum.GetValues( typeof( CalendarWeekRule ) ) )
				{
					culture.DateTimeFormat.CalendarWeekRule = weekRule;

					int year;
					int weekOfYear;

					// calendar week
					TimeCalendar timeCalendar = TimeCalendar.New( culture );
					TimeTool.GetWeekOfYear( now, culture, weekRule, culture.DateTimeFormat.FirstDayOfWeek, YearWeekType.Calendar,
																	out year, out weekOfYear );
					Assert.Equal( timeCalendar.GetWeekOfYear( now ), weekOfYear );

					// iso 8601 calendar week
					TimeCalendar timeCalendarIso8601 = TimeCalendar.New( culture, YearMonth.January, YearWeekType.Iso8601 );
					TimeTool.GetWeekOfYear( now, culture, weekRule, culture.DateTimeFormat.FirstDayOfWeek, YearWeekType.Iso8601,
																	out year, out weekOfYear );
					Assert.Equal( timeCalendarIso8601.GetWeekOfYear( now ), weekOfYear );
				}
			}
		} // GetWeekOfYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeCalendar")]
        [Fact]
		public void GetStartOfWeekTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				foreach ( CalendarWeekRule weekRule in Enum.GetValues( typeof( CalendarWeekRule ) ) )
				{
					culture.DateTimeFormat.CalendarWeekRule = weekRule;

					int year;
					int weekOfYear;

					// calendar week
					TimeTool.GetWeekOfYear( now, culture, YearWeekType.Calendar, out year, out weekOfYear );
					DateTime weekStartCalendar = TimeTool.GetStartOfYearWeek( year, weekOfYear, culture, YearWeekType.Calendar );
					TimeCalendar timeCalendar = TimeCalendar.New( culture );
					Assert.Equal( timeCalendar.GetStartOfYearWeek( year, weekOfYear ), weekStartCalendar );

					// iso 8601 calendar week
					TimeTool.GetWeekOfYear( now, culture, YearWeekType.Iso8601, out year, out weekOfYear );
					DateTime weekStartCalendarIso8601 = TimeTool.GetStartOfYearWeek( year, weekOfYear, culture, YearWeekType.Iso8601 );
					TimeCalendar timeCalendarIso8601 = TimeCalendar.New( culture, YearMonth.January, YearWeekType.Iso8601 );
					Assert.Equal( timeCalendarIso8601.GetStartOfYearWeek( year, weekOfYear ), weekStartCalendarIso8601 );
				}
			}
		} // GetStartOfWeekTest

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime testDate = new DateTime( 2010, 3, 18, 14, 9, 34, 234 );

	} // class TimeCalendarTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
