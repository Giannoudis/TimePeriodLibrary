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
using System.Threading;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class TimeCalendarTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void StartOffsetTest()
		{
			Assert.AreEqual( new TimeCalendar().StartOffset, TimeCalendar.DefaultStartOffset );

			TimeSpan offset = Duration.Second;
			TimeCalendar timeCalendar = new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = offset,
				EndOffset = TimeSpan.Zero
			} );
			Assert.AreEqual( timeCalendar.StartOffset, offset );
		} // StartOffsetTest

		// ----------------------------------------------------------------------
		[Test]
		public void EndOffsetTest()
		{
			Assert.AreEqual( new TimeCalendar().EndOffset, TimeCalendar.DefaultEndOffset );

			TimeSpan offset = Duration.Second.Negate();
			TimeCalendar timeCalendar = new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = TimeSpan.Zero,
				EndOffset = offset
			} );
			Assert.AreEqual( timeCalendar.EndOffset, offset );
		} // EndOffsetTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void InvalidOffset1Test()
		{
			new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = Duration.Second.Negate(),
				EndOffset = TimeSpan.Zero
			} );
		} // InvalidOffset1Test

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void InvalidOffset2Test()
		{
			new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = TimeSpan.Zero,
				EndOffset = Duration.Second
			} );
		} // InvalidOffset2Test

		// ----------------------------------------------------------------------
		[Test]
		public void CultureTest()
		{
			Assert.AreEqual( new TimeCalendar().Culture, Thread.CurrentThread.CurrentCulture );
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).Culture, culture );
			}
		} // CultureTest

		// ----------------------------------------------------------------------
		[Test]
		public void FirstDayOfWeekTest()
		{
			Assert.AreEqual( new TimeCalendar().FirstDayOfWeek, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek );
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).FirstDayOfWeek, culture.DateTimeFormat.FirstDayOfWeek );
			}
		} // FirstDayOfWeekTest

		// ----------------------------------------------------------------------
		[Test]
		public void MapStartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapStart( now ), now );
				Assert.AreEqual( TimeCalendar.New( culture, offset, TimeSpan.Zero ).MapStart( now ), now.Add( offset ) );

				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapStart( testDate ), testDate );
				Assert.AreEqual( TimeCalendar.New( culture, offset, TimeSpan.Zero ).MapStart( testDate ), testDate.Add( offset ) );
			}
		} // MapStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void UnmapStartTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapStart( now ), now );
				Assert.AreEqual( TimeCalendar.New( culture, offset, TimeSpan.Zero ).UnmapStart( now ), now.Subtract( offset ) );

				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapStart( testDate ), testDate );
				Assert.AreEqual( TimeCalendar.New( culture, offset, TimeSpan.Zero ).UnmapStart( testDate ), testDate.Subtract( offset ) );
			}
		} // UnmapStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void MapEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second.Negate();
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapEnd( now ), now );
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, offset ).MapEnd( now ), now.Add( offset ) );

				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).MapEnd( testDate ), testDate );
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, offset ).MapEnd( testDate ), testDate.Add( offset ) );
			}
		} // MapEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void UnmapEndTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeSpan offset = Duration.Second.Negate();
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapEnd( now ), now );
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, offset ).UnmapEnd( now ), now.Subtract( offset ) );

				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, TimeSpan.Zero ).UnmapEnd( testDate ), testDate );
				Assert.AreEqual( TimeCalendar.New( culture, TimeSpan.Zero, offset ).UnmapEnd( testDate ), testDate.Subtract( offset ) );
			}
		} // UnmapEndTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetYearTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetYear( now ), culture.Calendar.GetYear( now ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetYear( testDate ), culture.Calendar.GetYear( testDate ) );
			}
		} // GetYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetMonth( now ), culture.Calendar.GetMonth( now ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetMonth( testDate ), culture.Calendar.GetMonth( testDate ) );
			}
		} // GetMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetHourTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetHour( now ), culture.Calendar.GetHour( now ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetHour( testDate ), culture.Calendar.GetHour( testDate ) );
			}
		} // GetHourTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMinuteTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetMinute( now ), culture.Calendar.GetMinute( now ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetMinute( testDate ), culture.Calendar.GetMinute( testDate ) );
			}
		} // GetMinuteTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDayOfMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetDayOfMonth( now ), culture.Calendar.GetDayOfMonth( now ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetDayOfMonth( testDate ), culture.Calendar.GetDayOfMonth( testDate ) );
			}
		} // GetDayOfMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDayOfWeekTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetDayOfWeek( now ), culture.Calendar.GetDayOfWeek( now ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetDayOfWeek( testDate ), culture.Calendar.GetDayOfWeek( testDate ) );
			}
		} // GetDayOfWeekTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDaysInMonthTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetDaysInMonth( now.Year, now.Month ), culture.Calendar.GetDaysInMonth( now.Year, now.Month ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetDaysInMonth( testDate.Year, testDate.Month ), culture.Calendar.GetDaysInMonth( testDate.Year, testDate.Month ) );
			}
		} // GetDaysInMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthNameTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetMonthName( now.Month ), culture.DateTimeFormat.GetMonthName( now.Month ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetMonthName( testDate.Month ), culture.DateTimeFormat.GetMonthName( testDate.Month ) );
			}
		} // GetMonthNameTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetDayNameTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				Assert.AreEqual( TimeCalendar.New( culture ).GetDayName( now.DayOfWeek ), culture.DateTimeFormat.GetDayName( now.DayOfWeek ) );
				Assert.AreEqual( TimeCalendar.New( culture ).GetDayName( testDate.DayOfWeek ), culture.DateTimeFormat.GetDayName( testDate.DayOfWeek ) );
			}
		} // GetDayNameTest

		// ----------------------------------------------------------------------
		[Test]
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
					Assert.AreEqual( timeCalendar.GetWeekOfYear( now ), weekOfYear );

					// iso 8601 calendar week
					TimeCalendar timeCalendarIso8601 = TimeCalendar.New( culture, YearMonth.January, YearWeekType.Iso8601 );
					TimeTool.GetWeekOfYear( now, culture, weekRule, culture.DateTimeFormat.FirstDayOfWeek, YearWeekType.Iso8601,
																	out year, out weekOfYear );
					Assert.AreEqual( timeCalendarIso8601.GetWeekOfYear( now ), weekOfYear );
				}
			}
		} // GetWeekOfYearTest

		// ----------------------------------------------------------------------
		[Test]
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
					Assert.AreEqual( timeCalendar.GetStartOfYearWeek( year, weekOfYear ), weekStartCalendar );

					// iso 8601 calendar week
					TimeTool.GetWeekOfYear( now, culture, YearWeekType.Iso8601, out year, out weekOfYear );
					DateTime weekStartCalendarIso8601 = TimeTool.GetStartOfYearWeek( year, weekOfYear, culture, YearWeekType.Iso8601 );
					TimeCalendar timeCalendarIso8601 = TimeCalendar.New( culture, YearMonth.January, YearWeekType.Iso8601 );
					Assert.AreEqual( timeCalendarIso8601.GetStartOfYearWeek( year, weekOfYear ), weekStartCalendarIso8601 );
				}
			}
		} // GetStartOfWeekTest

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime testDate = new DateTime( 2010, 3, 18, 14, 9, 34, 234 );

	} // class TimeCalendarTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
