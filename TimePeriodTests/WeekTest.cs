// -- FILE ------------------------------------------------------------------
// name       : WeekTest.cs
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
	
	public sealed class WeekTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void DefaultCalendarTest()
		{
			const int startWeek = 1;
			int currentYear = ClockProxy.Clock.Now.Year;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				foreach ( YearWeekType yearWeekType in Enum.GetValues( typeof( YearWeekType ) ) )
				{
					int weeksOfYear = TimeTool.GetWeeksOfYear( currentYear, culture, yearWeekType );
					for ( int weekOfYear = startWeek; weekOfYear < weeksOfYear; weekOfYear++ )
					{
						Week week = new Week( currentYear, weekOfYear, TimeCalendar.New( culture, YearMonth.January, yearWeekType ) );
						Assert.Equal( week.Year, currentYear );

						DateTime weekStart = TimeTool.GetStartOfYearWeek( currentYear, weekOfYear, culture, yearWeekType );
						DateTime weekEnd = weekStart.AddDays( TimeSpec.DaysPerWeek );
						Assert.Equal( week.Start, weekStart.Add( week.Calendar.StartOffset ) );
						Assert.Equal( week.End, weekEnd.Add( week.Calendar.EndOffset ) );
					}
				}
			}
		} // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void EnAuCultureTest()
		{
			CultureInfo cultureInfo = new CultureInfo( "en-AU" );
			//	cultureInfo.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			TimeCalendar calendar = new TimeCalendar( new TimeCalendarConfig { Culture = cultureInfo } );
			Week week = new Week( new DateTime( 2011, 4, 1, 9, 0, 0 ), calendar );
			Assert.Equal( week.Start, new DateTime( 2011, 3, 28 ) );
		} // EnAuCultureTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void DanishUsCultureTest()
		{
			CultureInfo danishCulture = new CultureInfo( "da-DK" );
			Week danishWeek = new Week( 2011, 36, new TimeCalendar( new TimeCalendarConfig { Culture = danishCulture } ) );
			Assert.Equal( danishWeek.Start.Date, new DateTime( 2011, 9, 5 ) );
			Assert.Equal( danishWeek.End.Date, new DateTime( 2011, 9, 11 ) );

			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			Week usWeek = new Week( 2011, 36, new TimeCalendar( new TimeCalendarConfig { Culture = usCulture } ) );
			Assert.Equal( usWeek.Start.Date, new DateTime( 2011, 9, 4 ) );
			Assert.Equal( usWeek.End.Date, new DateTime( 2011, 9, 10 ) );
		} // DanishUsCultureTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void Iso8601WithMondayAsStartWeekTest()
		{
			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			usCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

			TimeCalendar isoCalendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearWeekType = YearWeekType.Iso8601,
						Culture = usCulture
					} );

			Week weekFrom20131229 = new Week( new DateTime( 2013, 12, 29 ), isoCalendar );
			Assert.Equal( weekFrom20131229.Start.Date, new DateTime( 2013, 12, 23 ) );
			Assert.Equal( weekFrom20131229.End.Date, new DateTime( 2013, 12, 29 ) );
			Assert.Equal(52, weekFrom20131229.WeekOfYear);

			Week weekFrom20131230 = new Week( new DateTime( 2013, 12, 30 ), isoCalendar );
			Assert.Equal( weekFrom20131230.Start.Date, new DateTime( 2013, 12, 30 ) );
			Assert.Equal( weekFrom20131230.End.Date, new DateTime( 2014, 1, 5 ) );
			Assert.Equal(1, weekFrom20131230.WeekOfYear);

			Week weekFrom20140105 = new Week( new DateTime( 2014, 1, 5 ), isoCalendar );
			Assert.Equal( weekFrom20140105.Start.Date, new DateTime( 2013, 12, 30 ) );
			Assert.Equal( weekFrom20140105.End.Date, new DateTime( 2014, 1, 5 ) );
			Assert.Equal(1, weekFrom20140105.WeekOfYear);

			Week weekFrom20140106 = new Week( new DateTime( 2014, 1, 6 ), isoCalendar );
			Assert.Equal( weekFrom20140106.Start.Date, new DateTime( 2014, 1, 6 ) );
			Assert.Equal( weekFrom20140106.End.Date, new DateTime( 2014, 1, 12 ) );
			Assert.Equal(2, weekFrom20140106.WeekOfYear);
		} // Iso8601WithMondayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void Iso8601WithTuesdayAsStartWeekTest()
		{
			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			usCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Tuesday;

			TimeCalendar isoCalendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearWeekType = YearWeekType.Iso8601,
						Culture = usCulture
					} );

			Week weekFrom20131230 = new Week( new DateTime( 2013, 12, 30 ), isoCalendar );
			Assert.Equal( weekFrom20131230.Start.Date, new DateTime( 2013, 12, 24 ) );
			Assert.Equal( weekFrom20131230.End.Date, new DateTime( 2013, 12, 30 ) );
			Assert.Equal(52, weekFrom20131230.WeekOfYear);

			Week weekFrom20131231 = new Week( new DateTime( 2013, 12, 31 ), isoCalendar );
			Assert.Equal( weekFrom20131231.Start.Date, new DateTime( 2013, 12, 31 ) );
			Assert.Equal( weekFrom20131231.End.Date, new DateTime( 2014, 1, 6 ) );
			Assert.Equal(1, weekFrom20131231.WeekOfYear);

			Week weekFrom20140106 = new Week( new DateTime( 2014, 1, 6 ), isoCalendar );
			Assert.Equal( weekFrom20140106.Start.Date, new DateTime( 2013, 12, 31 ) );
			Assert.Equal( weekFrom20140106.End.Date, new DateTime( 2014, 1, 6 ) );
			Assert.Equal(1, weekFrom20140106.WeekOfYear);

			Week weekFrom20140107 = new Week( new DateTime( 2014, 1, 7 ), isoCalendar );
			Assert.Equal( weekFrom20140107.Start.Date, new DateTime( 2014, 1, 7 ) );
			Assert.Equal( weekFrom20140107.End.Date, new DateTime( 2014, 1, 13 ) );
			Assert.Equal(2, weekFrom20140107.WeekOfYear);
		} // Iso8601WithTuesdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void Iso8601WithWednesdayAsStartWeekTest()
		{
			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			usCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Wednesday;

			TimeCalendar isoCalendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearWeekType = YearWeekType.Iso8601,
						Culture = usCulture
					} );

			Week weekFrom20131231 = new Week( new DateTime( 2013, 12, 31 ), isoCalendar );
			Assert.Equal( weekFrom20131231.Start.Date, new DateTime( 2013, 12, 25 ) );
			Assert.Equal( weekFrom20131231.End.Date, new DateTime( 2013, 12, 31 ) );
			Assert.Equal(52, weekFrom20131231.WeekOfYear);

			Week weekFrom20140101 = new Week( new DateTime( 2014, 1, 1 ), isoCalendar );
			Assert.Equal( weekFrom20140101.Start.Date, new DateTime( 2014, 1, 1 ) );
			Assert.Equal( weekFrom20140101.End.Date, new DateTime( 2014, 1, 7 ) );
			Assert.Equal(1, weekFrom20140101.WeekOfYear);

			Week weekFrom20140107 = new Week( new DateTime( 2014, 1, 7 ), isoCalendar );
			Assert.Equal( weekFrom20140107.Start.Date, new DateTime( 2014, 1, 1 ) );
			Assert.Equal( weekFrom20140107.End.Date, new DateTime( 2014, 1, 7 ) );
			Assert.Equal(1, weekFrom20140107.WeekOfYear);

			Week weekFrom20140108 = new Week( new DateTime( 2014, 1, 8 ), isoCalendar );
			Assert.Equal( weekFrom20140108.Start.Date, new DateTime( 2014, 1, 8 ) );
			Assert.Equal( weekFrom20140108.End.Date, new DateTime( 2014, 1, 14 ) );
			Assert.Equal(2, weekFrom20140108.WeekOfYear);
		} // Iso8601WithWednesdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void Iso8601WithThursdayAsStartWeekTest()
		{
			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			usCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Thursday;

			TimeCalendar isoCalendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearWeekType = YearWeekType.Iso8601,
						Culture = usCulture
					} );

			Week weekFrom20140101 = new Week( new DateTime( 2014, 1, 1 ), isoCalendar );
			Assert.Equal( weekFrom20140101.Start.Date, new DateTime( 2013, 12, 26 ) );
			Assert.Equal( weekFrom20140101.End.Date, new DateTime( 2014, 1, 1 ) );
			Assert.Equal(52, weekFrom20140101.WeekOfYear);

			Week weekFrom20140102 = new Week( new DateTime( 2014, 1, 2 ), isoCalendar );
			Assert.Equal( weekFrom20140102.Start.Date, new DateTime( 2014, 1, 2 ) );
			Assert.Equal( weekFrom20140102.End.Date, new DateTime( 2014, 1, 8 ) );
			Assert.Equal(1, weekFrom20140102.WeekOfYear);

			Week weekFrom20140108 = new Week( new DateTime( 2014, 1, 8 ), isoCalendar );
			Assert.Equal( weekFrom20140108.Start.Date, new DateTime( 2014, 1, 2 ) );
			Assert.Equal( weekFrom20140108.End.Date, new DateTime( 2014, 1, 8 ) );
			Assert.Equal(1, weekFrom20140108.WeekOfYear);

			Week weekFrom20140109 = new Week( new DateTime( 2014, 1, 9 ), isoCalendar );
			Assert.Equal( weekFrom20140109.Start.Date, new DateTime( 2014, 1, 9 ) );
			Assert.Equal( weekFrom20140109.End.Date, new DateTime( 2014, 1, 15 ) );
			Assert.Equal(2, weekFrom20140109.WeekOfYear);
		} // Iso8601WithThursdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void Iso8601WithFridayAsStartWeekTest()
		{
			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			usCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Friday;

			TimeCalendar isoCalendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearWeekType = YearWeekType.Iso8601,
						Culture = usCulture
					} );

			Week weekFrom20140102 = new Week( new DateTime( 2014, 1, 2 ), isoCalendar );
			Assert.Equal( weekFrom20140102.Start.Date, new DateTime( 2013, 12, 27 ) );
			Assert.Equal( weekFrom20140102.End.Date, new DateTime( 2014, 1, 2 ) );
			Assert.Equal(52, weekFrom20140102.WeekOfYear);

			Week weekFrom20140103 = new Week( new DateTime( 2014, 1, 3 ), isoCalendar );
			Assert.Equal( weekFrom20140103.Start.Date, new DateTime( 2014, 1, 3 ) );
			Assert.Equal( weekFrom20140103.End.Date, new DateTime( 2014, 1, 9 ) );
			Assert.Equal(1, weekFrom20140103.WeekOfYear);

			Week weekFrom20140109 = new Week( new DateTime( 2014, 1, 9 ), isoCalendar );
			Assert.Equal( weekFrom20140109.Start.Date, new DateTime( 2014, 1, 3 ) );
			Assert.Equal( weekFrom20140109.End.Date, new DateTime( 2014, 1, 9 ) );
			Assert.Equal(1, weekFrom20140109.WeekOfYear);

			Week weekFrom20140110 = new Week( new DateTime( 2014, 1, 10 ), isoCalendar );
			Assert.Equal( weekFrom20140110.Start.Date, new DateTime( 2014, 1, 10 ) );
			Assert.Equal( weekFrom20140110.End.Date, new DateTime( 2014, 1, 16 ) );
			Assert.Equal(2, weekFrom20140110.WeekOfYear);
		} // Iso8601WithFridayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void Iso8601WithSaturdayAsStartWeekTest()
		{
			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			usCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday;

			TimeCalendar isoCalendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearWeekType = YearWeekType.Iso8601,
						Culture = usCulture
					} );

			Week weekFrom20140103 = new Week( new DateTime( 2014, 1, 3 ), isoCalendar );
			Assert.Equal( weekFrom20140103.Start.Date, new DateTime( 2013, 12, 28 ) );
			Assert.Equal( weekFrom20140103.End.Date, new DateTime( 2014, 1, 3 ) );
			Assert.Equal(53, weekFrom20140103.WeekOfYear);

			Week weekFrom20140104 = new Week( new DateTime( 2014, 1, 4 ), isoCalendar );
			Assert.Equal( weekFrom20140104.Start.Date, new DateTime( 2014, 1, 4 ) );
			Assert.Equal( weekFrom20140104.End.Date, new DateTime( 2014, 1, 10 ) );
			Assert.Equal(1, weekFrom20140104.WeekOfYear);

			Week weekFrom20140110 = new Week( new DateTime( 2014, 1, 10 ), isoCalendar );
			Assert.Equal( weekFrom20140110.Start.Date, new DateTime( 2014, 1, 4 ) );
			Assert.Equal( weekFrom20140110.End.Date, new DateTime( 2014, 1, 10 ) );
			Assert.Equal(1, weekFrom20140110.WeekOfYear);

			Week weekFrom20140111 = new Week( new DateTime( 2014, 1, 11 ), isoCalendar );
			Assert.Equal( weekFrom20140111.Start.Date, new DateTime( 2014, 1, 11 ) );
			Assert.Equal( weekFrom20140111.End.Date, new DateTime( 2014, 1, 17 ) );
			Assert.Equal(2, weekFrom20140111.WeekOfYear);
		} // Iso8601WithSaturdayAsStartWeekTest

        // ----------------------------------------------------------------------
        [Trait("Category", "Week")]
        [Fact]
		public void Iso8601WithSundayAsStartWeekTest()
		{
			CultureInfo usCulture = new CultureInfo( "en-US" );
			usCulture.DateTimeFormat.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
			usCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;

			TimeCalendar isoCalendar = new TimeCalendar(
					new TimeCalendarConfig
					{
						YearWeekType = YearWeekType.Iso8601,
						Culture = usCulture
					} );

			Week weekFrom20131228 = new Week( new DateTime( 2013, 12, 28 ), isoCalendar );
			Assert.Equal( weekFrom20131228.Start.Date, new DateTime( 2013, 12, 22 ) );
			Assert.Equal( weekFrom20131228.End.Date, new DateTime( 2013, 12, 28 ) );
			Assert.Equal(52, weekFrom20131228.WeekOfYear);

			Week weekFrom20131229 = new Week( new DateTime( 2013, 12, 29 ), isoCalendar );
			Assert.Equal( weekFrom20131229.Start.Date, new DateTime( 2013, 12, 29 ) );
			Assert.Equal( weekFrom20131229.End.Date, new DateTime( 2014, 1, 4 ) );
			Assert.Equal(1, weekFrom20131229.WeekOfYear);

			Week weekFrom20140104 = new Week( new DateTime( 2014, 1, 4 ), isoCalendar );
			Assert.Equal( weekFrom20140104.Start.Date, new DateTime( 2013, 12, 29 ) );
			Assert.Equal( weekFrom20140104.End.Date, new DateTime( 2014, 1, 4 ) );
			Assert.Equal(1, weekFrom20140104.WeekOfYear);

			Week weekFrom20140105 = new Week( new DateTime( 2014, 1, 5 ), isoCalendar );
			Assert.Equal( weekFrom20140105.Start.Date, new DateTime( 2014, 1, 5 ) );
			Assert.Equal( weekFrom20140105.End.Date, new DateTime( 2014, 1, 11 ) );
			Assert.Equal(2, weekFrom20140105.WeekOfYear);
		} // Iso8601WithSundayAsStartWeekTest

	} // class WeekTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
