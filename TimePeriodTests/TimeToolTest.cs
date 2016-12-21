// -- FILE ------------------------------------------------------------------
// name       : TimeToolTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	[TestFixture]
	public sealed class TimeToolTest : TestUnitBase
	{

		#region Date and Time

		// ----------------------------------------------------------------------
		[Test]
		public void GetDateTest()
		{
			Assert.AreEqual( TimeTool.GetDate( testDate ).Year, testDate.Year );
			Assert.AreEqual( TimeTool.GetDate( testDate ).Month, testDate.Month );
			Assert.AreEqual( TimeTool.GetDate( testDate ).Day, testDate.Day );
			Assert.AreEqual( TimeTool.GetDate( testDate ).Hour, 0 );
			Assert.AreEqual( TimeTool.GetDate( testDate ).Minute, 0 );
			Assert.AreEqual( TimeTool.GetDate( testDate ).Second, 0 );
			Assert.AreEqual( TimeTool.GetDate( testDate ).Millisecond, 0 );
		} // GetDateTest

		// ----------------------------------------------------------------------
		[Test]
		public void SetDateTest()
		{
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate ).Year, testDiffDate.Year );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate ).Month, testDiffDate.Month );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate ).Day, testDiffDate.Day );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate ).Hour, testDate.Hour );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate ).Minute, testDate.Minute );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate ).Second, testDate.Second );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate ).Millisecond, testDate.Millisecond );

			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day ).Year, testDiffDate.Year );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day ).Month, testDiffDate.Month );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day ).Day, testDiffDate.Day );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day ).Hour, testDate.Hour );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day ).Minute, testDate.Minute );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day ).Second, testDate.Second );
			Assert.AreEqual( TimeTool.SetDate( testDate, testDiffDate.Year, testDiffDate.Month, testDiffDate.Day ).Millisecond, testDate.Millisecond );
		} // SetDateTest

		// ----------------------------------------------------------------------
		[Test]
		public void HasTimeOfDayTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			Assert.IsFalse( TimeTool.HasTimeOfDay( new DateTime( now.Year, now.Month, now.Day, 0, 0, 0, 0 ) ) );
			Assert.IsTrue( TimeTool.HasTimeOfDay( new DateTime( now.Year, now.Month, now.Day, 1, 0, 0, 0 ) ) );
			Assert.IsTrue( TimeTool.HasTimeOfDay( new DateTime( now.Year, now.Month, now.Day, 0, 1, 0, 0 ) ) );
			Assert.IsTrue( TimeTool.HasTimeOfDay( new DateTime( now.Year, now.Month, now.Day, 0, 0, 1, 0 ) ) );
			Assert.IsTrue( TimeTool.HasTimeOfDay( new DateTime( now.Year, now.Month, now.Day, 0, 0, 0, 1 ) ) );
		} // HasTimeOfDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void SetTimeOfDayTest()
		{
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate ).Year, testDate.Year );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate ).Month, testDate.Month );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate ).Day, testDate.Day );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate ).Hour, testDiffDate.Hour );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate ).Minute, testDiffDate.Minute );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate ).Second, testDiffDate.Second );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate ).Millisecond, testDiffDate.Millisecond );

			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond ).Year, testDate.Year );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond ).Month, testDate.Month );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond ).Day, testDate.Day );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond ).Hour, testDiffDate.Hour );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond ).Minute, testDiffDate.Minute );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond ).Second, testDiffDate.Second );
			Assert.AreEqual( TimeTool.SetTimeOfDay( testDate, testDiffDate.Hour, testDiffDate.Minute, testDiffDate.Second, testDiffDate.Millisecond ).Millisecond, testDiffDate.Millisecond );
		} // SetTimeOfDayTest

		#endregion

		#region Year

		// ----------------------------------------------------------------------
		[Test]
		public void GetYearOfTest()
		{
			Assert.AreEqual( TimeTool.GetYearOf( YearMonth.January, new DateTime( 2000, 1, 1 ) ), 2000 );
			Assert.AreEqual( TimeTool.GetYearOf( YearMonth.April, new DateTime( 2000, 4, 1 ) ), 2000 );
			Assert.AreEqual( TimeTool.GetYearOf( YearMonth.April, new DateTime( 2001, 3, 31 ) ), 2000 );
			Assert.AreEqual( TimeTool.GetYearOf( YearMonth.April, new DateTime( 2000, 3, 31 ) ), 1999 );
		} // GetYearOfTest

		#endregion

		#region Halfyear

		// ----------------------------------------------------------------------
		[Test]
		public void NextHalfyearTest()
		{
			int year;
			YearHalfyear halfyear;

			TimeTool.NextHalfyear( YearHalfyear.First, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );

			TimeTool.NextHalfyear( YearHalfyear.Second, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );
		} // NextHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void PreviousHalfyearTest()
		{
			int year;
			YearHalfyear halfyear;

			TimeTool.PreviousHalfyear( YearHalfyear.First, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );

			TimeTool.PreviousHalfyear( YearHalfyear.Second, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );
		} // PreviousHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddHalfyearTest()
		{
			int year;
			YearHalfyear halfyear;

			TimeTool.AddHalfyear( YearHalfyear.First, 1, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );
			TimeTool.AddHalfyear( YearHalfyear.First, -1, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );
			TimeTool.AddHalfyear( YearHalfyear.Second, 1, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );
			TimeTool.AddHalfyear( YearHalfyear.Second, -1, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );

			TimeTool.AddHalfyear( YearHalfyear.First, 2, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );
			TimeTool.AddHalfyear( YearHalfyear.First, -2, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );
			TimeTool.AddHalfyear( YearHalfyear.Second, 2, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );
			TimeTool.AddHalfyear( YearHalfyear.Second, -2, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );

			TimeTool.AddHalfyear( YearHalfyear.First, 5, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );
			TimeTool.AddHalfyear( YearHalfyear.First, -5, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.Second );
			TimeTool.AddHalfyear( YearHalfyear.Second, 5, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );
			TimeTool.AddHalfyear( YearHalfyear.Second, -5, out year, out halfyear );
			Assert.AreEqual( halfyear, YearHalfyear.First );

			TimeTool.AddHalfyear( 2008, YearHalfyear.First, 1, out year, out halfyear );
			Assert.AreEqual( year, 2008 );
			Assert.AreEqual( halfyear, YearHalfyear.Second );
			TimeTool.AddHalfyear( 2008, YearHalfyear.Second, 1, out year, out halfyear );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( halfyear, YearHalfyear.First );

			TimeTool.AddHalfyear( 2008, YearHalfyear.First, 2, out year, out halfyear );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( halfyear, YearHalfyear.First );
			TimeTool.AddHalfyear( 2008, YearHalfyear.Second, 2, out year, out halfyear );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( halfyear, YearHalfyear.Second );

			TimeTool.AddHalfyear( 2008, YearHalfyear.First, 3, out year, out halfyear );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( halfyear, YearHalfyear.Second );
			TimeTool.AddHalfyear( 2008, YearHalfyear.Second, 3, out year, out halfyear );
			Assert.AreEqual( year, 2010 );
			Assert.AreEqual( halfyear, YearHalfyear.First );
		} // AddHalfyearTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetCalendarHalfyearOfMonthTest()
		{
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.January ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.February ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.March ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.April ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.May ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.June ), YearHalfyear.First );

			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.July ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.August ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.September ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.November ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.December ), YearHalfyear.Second );
		} // GetCalendarHalfyearOfMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetHalfyearOfMonthTest()
		{
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.October ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.November ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.December ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.January ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.February ), YearHalfyear.First );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.March ), YearHalfyear.First );

			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.April ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.May ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.June ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.July ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.August ), YearHalfyear.Second );
			Assert.AreEqual( TimeTool.GetHalfyearOfMonth( YearMonth.October, YearMonth.September ), YearHalfyear.Second );
		} // GetHalfyearOfMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthsOfHalfyearTest()
		{
			Assert.AreEqual( TimeTool.GetMonthsOfHalfyear( YearHalfyear.First ), TimeSpec.FirstHalfyearMonths );
			Assert.AreEqual( TimeTool.GetMonthsOfHalfyear( YearHalfyear.Second ), TimeSpec.SecondHalfyearMonths );
		} // GetMonthsOfQuarterTest

		#endregion

		#region Quarter

		// ----------------------------------------------------------------------
		[Test]
		public void NextQarterTest()
		{
			int year;
			YearQuarter quarter;

			TimeTool.NextQuarter( YearQuarter.First, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.NextQuarter( YearQuarter.Second, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.NextQuarter( YearQuarter.Third, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.NextQuarter( YearQuarter.Fourth, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
		} // NextQarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void PreviousQuarterTest()
		{
			int year;
			YearQuarter quarter;

			TimeTool.PreviousQuarter( YearQuarter.First, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.PreviousQuarter( YearQuarter.Second, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.PreviousQuarter( YearQuarter.Third, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.PreviousQuarter( YearQuarter.Fourth, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
		} // PreviousQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddQuarterTest()
		{
			int year;
			YearQuarter quarter;

			TimeTool.AddQuarter( YearQuarter.First, 1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( YearQuarter.Second, 1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( YearQuarter.Third, 1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( YearQuarter.Fourth, 1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );

			TimeTool.AddQuarter( YearQuarter.First, -1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( YearQuarter.Second, -1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.AddQuarter( YearQuarter.Third, -1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( YearQuarter.Fourth, -1, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );

			TimeTool.AddQuarter( YearQuarter.First, 2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( YearQuarter.Second, 2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( YearQuarter.Third, 2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.AddQuarter( YearQuarter.Fourth, 2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );

			TimeTool.AddQuarter( YearQuarter.First, -2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( YearQuarter.Second, -2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( YearQuarter.Third, -2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.AddQuarter( YearQuarter.Fourth, -2, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );

			TimeTool.AddQuarter( YearQuarter.First, 3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( YearQuarter.Second, 3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.AddQuarter( YearQuarter.Third, 3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( YearQuarter.Fourth, 3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );

			TimeTool.AddQuarter( YearQuarter.First, -3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( YearQuarter.Second, -3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( YearQuarter.Third, -3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( YearQuarter.Fourth, -3, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );

			TimeTool.AddQuarter( YearQuarter.First, 4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.AddQuarter( YearQuarter.Second, 4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( YearQuarter.Third, 4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( YearQuarter.Fourth, 4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );

			TimeTool.AddQuarter( YearQuarter.First, -4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.AddQuarter( YearQuarter.Second, -4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( YearQuarter.Third, -4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( YearQuarter.Fourth, -4, out year, out quarter );
			Assert.AreEqual( quarter, YearQuarter.Fourth );

			TimeTool.AddQuarter( 2008, YearQuarter.First, 1, out year, out quarter );
			Assert.AreEqual( year, 2008 );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( 2008, YearQuarter.Second, 1, out year, out quarter );
			Assert.AreEqual( year, 2008 );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( 2008, YearQuarter.Third, 1, out year, out quarter );
			Assert.AreEqual( year, 2008 );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( 2008, YearQuarter.Fourth, 1, out year, out quarter );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( quarter, YearQuarter.First );

			TimeTool.AddQuarter( 2008, YearQuarter.First, 2, out year, out quarter );
			Assert.AreEqual( year, 2008 );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( 2008, YearQuarter.Second, 2, out year, out quarter );
			Assert.AreEqual( year, 2008 );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( 2008, YearQuarter.Third, 2, out year, out quarter );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( quarter, YearQuarter.First );
			TimeTool.AddQuarter( 2008, YearQuarter.Fourth, 2, out year, out quarter );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( quarter, YearQuarter.Second );

			TimeTool.AddQuarter( 2008, YearQuarter.First, 5, out year, out quarter );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( quarter, YearQuarter.Second );
			TimeTool.AddQuarter( 2008, YearQuarter.Second, 5, out year, out quarter );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( quarter, YearQuarter.Third );
			TimeTool.AddQuarter( 2008, YearQuarter.Third, 5, out year, out quarter );
			Assert.AreEqual( year, 2009 );
			Assert.AreEqual( quarter, YearQuarter.Fourth );
			TimeTool.AddQuarter( 2008, YearQuarter.Fourth, 5, out year, out quarter );
			Assert.AreEqual( year, 2010 );
			Assert.AreEqual( quarter, YearQuarter.First );
		} // AddQuarterTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetCalendarQuarterOfMonthTest()
		{
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.January ), YearQuarter.First );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.February ), YearQuarter.First );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.March ), YearQuarter.First );

			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.April ), YearQuarter.Second );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.May ), YearQuarter.Second );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.June ), YearQuarter.Second );

			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.July ), YearQuarter.Third );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.August ), YearQuarter.Third );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.September ), YearQuarter.Third );

			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October ), YearQuarter.Fourth );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.November ), YearQuarter.Fourth );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.December ), YearQuarter.Fourth );
		} // GetCalendarQuarterOfMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetQuarterOfMonthTest()
		{
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.October ), YearQuarter.First );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.November ), YearQuarter.First );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.December ), YearQuarter.First );

			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.January ), YearQuarter.Second );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.February ), YearQuarter.Second );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.March ), YearQuarter.Second );

			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.April ), YearQuarter.Third );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.May ), YearQuarter.Third );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.June ), YearQuarter.Third );

			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.July ), YearQuarter.Fourth );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.August ), YearQuarter.Fourth );
			Assert.AreEqual( TimeTool.GetQuarterOfMonth( YearMonth.October, YearMonth.September ), YearQuarter.Fourth );
		} // GetQuarterOfMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void GetMonthsOfQuarterTest()
		{
			Assert.AreEqual( TimeTool.GetMonthsOfQuarter( YearQuarter.First ), TimeSpec.FirstQuarterMonths );
			Assert.AreEqual( TimeTool.GetMonthsOfQuarter( YearQuarter.Second ), TimeSpec.SecondQuarterMonths );
			Assert.AreEqual( TimeTool.GetMonthsOfQuarter( YearQuarter.Third ), TimeSpec.ThirdQuarterMonths );
			Assert.AreEqual( TimeTool.GetMonthsOfQuarter( YearQuarter.Fourth ), TimeSpec.FourthQuarterMonths );
		} // GetMonthsOfQuarterTest

		#endregion

		#region Month

		// ----------------------------------------------------------------------
		[Test]
		public void NextMonthTest()
		{
			int year;
			YearMonth month;

			TimeTool.NextMonth( YearMonth.January, out year, out month );
			Assert.AreEqual( month, YearMonth.February );
			TimeTool.NextMonth( YearMonth.February, out year, out month );
			Assert.AreEqual( month, YearMonth.March );
			TimeTool.NextMonth( YearMonth.March, out year, out month );
			Assert.AreEqual( month, YearMonth.April );
			TimeTool.NextMonth( YearMonth.April, out year, out month );
			Assert.AreEqual( month, YearMonth.May );
			TimeTool.NextMonth( YearMonth.May, out year, out month );
			Assert.AreEqual( month, YearMonth.June );
			TimeTool.NextMonth( YearMonth.June, out year, out month );
			Assert.AreEqual( month, YearMonth.July );
			TimeTool.NextMonth( YearMonth.July, out year, out month );
			Assert.AreEqual( month, YearMonth.August );
			TimeTool.NextMonth( YearMonth.August, out year, out month );
			Assert.AreEqual( month, YearMonth.September );
			TimeTool.NextMonth( YearMonth.September, out year, out month );
			Assert.AreEqual( month, YearMonth.October );
			TimeTool.NextMonth( YearMonth.October, out year, out month );
			Assert.AreEqual( month, YearMonth.November );
			TimeTool.NextMonth( YearMonth.November, out year, out month );
			Assert.AreEqual( month, YearMonth.December );
			TimeTool.NextMonth( YearMonth.December, out year, out month );
			Assert.AreEqual( month, YearMonth.January );
		} // NextMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void PreviousMonthTest()
		{
			int year;
			YearMonth month;

			TimeTool.PreviousMonth( YearMonth.January, out year, out month );
			Assert.AreEqual( month, YearMonth.December );
			TimeTool.PreviousMonth( YearMonth.February, out year, out month );
			Assert.AreEqual( month, YearMonth.January );
			TimeTool.PreviousMonth( YearMonth.March, out year, out month );
			Assert.AreEqual( month, YearMonth.February );
			TimeTool.PreviousMonth( YearMonth.April, out year, out month );
			Assert.AreEqual( month, YearMonth.March );
			TimeTool.PreviousMonth( YearMonth.May, out year, out month );
			Assert.AreEqual( month, YearMonth.April );
			TimeTool.PreviousMonth( YearMonth.June, out year, out month );
			Assert.AreEqual( month, YearMonth.May );
			TimeTool.PreviousMonth( YearMonth.July, out year, out month );
			Assert.AreEqual( month, YearMonth.June );
			TimeTool.PreviousMonth( YearMonth.August, out year, out month );
			Assert.AreEqual( month, YearMonth.July );
			TimeTool.PreviousMonth( YearMonth.September, out year, out month );
			Assert.AreEqual( month, YearMonth.August );
			TimeTool.PreviousMonth( YearMonth.October, out year, out month );
			Assert.AreEqual( month, YearMonth.September );
			TimeTool.PreviousMonth( YearMonth.November, out year, out month );
			Assert.AreEqual( month, YearMonth.October );
			TimeTool.PreviousMonth( YearMonth.December, out year, out month );
			Assert.AreEqual( month, YearMonth.November );
		} // PreviousMonthTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddMonthsTest()
		{
			int year;
			YearMonth month;

			TimeTool.AddMonth( YearMonth.January, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.February );
			TimeTool.AddMonth( YearMonth.February, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.March );
			TimeTool.AddMonth( YearMonth.March, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.April );
			TimeTool.AddMonth( YearMonth.April, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.May );
			TimeTool.AddMonth( YearMonth.May, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.June );
			TimeTool.AddMonth( YearMonth.June, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.July );
			TimeTool.AddMonth( YearMonth.July, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.August );
			TimeTool.AddMonth( YearMonth.August, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.September );
			TimeTool.AddMonth( YearMonth.September, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.October );
			TimeTool.AddMonth( YearMonth.October, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.November );
			TimeTool.AddMonth( YearMonth.November, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.December );
			TimeTool.AddMonth( YearMonth.December, 1, out year, out month );
			Assert.AreEqual( month, YearMonth.January );

			TimeTool.AddMonth( YearMonth.January, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.December );
			TimeTool.AddMonth( YearMonth.February, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.January );
			TimeTool.AddMonth( YearMonth.March, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.February );
			TimeTool.AddMonth( YearMonth.April, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.March );
			TimeTool.AddMonth( YearMonth.May, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.April );
			TimeTool.AddMonth( YearMonth.June, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.May );
			TimeTool.AddMonth( YearMonth.July, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.June );
			TimeTool.AddMonth( YearMonth.August, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.July );
			TimeTool.AddMonth( YearMonth.September, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.August );
			TimeTool.AddMonth( YearMonth.October, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.September );
			TimeTool.AddMonth( YearMonth.November, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.October );
			TimeTool.AddMonth( YearMonth.December, -1, out year, out month );
			Assert.AreEqual( month, YearMonth.November );

			for ( int i = -36; i <= 36; i += 36 )
			{

				TimeTool.AddMonth( YearMonth.January, i, out year, out month );
				Assert.AreEqual( month, YearMonth.January );
				TimeTool.AddMonth( YearMonth.February, i, out year, out month );
				Assert.AreEqual( month, YearMonth.February );
				TimeTool.AddMonth( YearMonth.March, i, out year, out month );
				Assert.AreEqual( month, YearMonth.March );
				TimeTool.AddMonth( YearMonth.April, i, out year, out month );
				Assert.AreEqual( month, YearMonth.April );
				TimeTool.AddMonth( YearMonth.May, i, out year, out month );
				Assert.AreEqual( month, YearMonth.May );
				TimeTool.AddMonth( YearMonth.June, i, out year, out month );
				Assert.AreEqual( month, YearMonth.June );
				TimeTool.AddMonth( YearMonth.July, i, out year, out month );
				Assert.AreEqual( month, YearMonth.July );
				TimeTool.AddMonth( YearMonth.August, i, out year, out month );
				Assert.AreEqual( month, YearMonth.August );
				TimeTool.AddMonth( YearMonth.September, i, out year, out month );
				Assert.AreEqual( month, YearMonth.September );
				TimeTool.AddMonth( YearMonth.October, i, out year, out month );
				Assert.AreEqual( month, YearMonth.October );
				TimeTool.AddMonth( YearMonth.November, i, out year, out month );
				Assert.AreEqual( month, YearMonth.November );
				TimeTool.AddMonth( YearMonth.December, i, out year, out month );
				Assert.AreEqual( month, YearMonth.December );
			}

			for ( int i = 1; i < ( 3 * TimeSpec.MonthsPerYear ); i++ )
			{
				TimeTool.AddMonth( 2008, (YearMonth)i, 1, out year, out month );
				Assert.AreEqual( year, 2008 + ( i / 12 ) );
				Assert.AreEqual( month, (YearMonth)( ( i % TimeSpec.MonthsPerYear ) + 1 ) );
			}
		} // AddMonthsTest

		#endregion

		#region Week

		// ----------------------------------------------------------------------
		[Test]
		public void WeeekOfYearCalendarTest()
		{
			DateTime moment = new DateTime( 2007, 12, 31 );
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				int calendarWeekOfYear = culture.Calendar.GetWeekOfYear(
					moment,
					culture.DateTimeFormat.CalendarWeekRule,
					culture.DateTimeFormat.FirstDayOfWeek );
				int year;
				int weekOfYear;
				TimeTool.GetWeekOfYear( moment, culture, YearWeekType.Calendar, out year, out weekOfYear );
				Assert.AreEqual( weekOfYear, calendarWeekOfYear );
			}
		} // WeeekOfYearCalendarTest

		// ----------------------------------------------------------------------
		[Test]
		public void WeeekOfYearIsoTest()
		{
			DateTime moment = new DateTime( 2007, 12, 31 );
			CultureTestData cultures = new CultureTestData();
			foreach ( CultureInfo culture in cultures )
			{
				if ( culture.DateTimeFormat.CalendarWeekRule != CalendarWeekRule.FirstFourDayWeek ||
						 culture.DateTimeFormat.FirstDayOfWeek != DayOfWeek.Monday )
				{
					continue;
				}
				int year;
				int weekOfYear;
				TimeTool.GetWeekOfYear( moment, culture, YearWeekType.Iso8601, out year, out weekOfYear );
				Assert.AreEqual( weekOfYear, 1 );
			}
		} // WeeekOfYearIsoTest

		#endregion

		#region Day

		// ----------------------------------------------------------------------
		[Test]
		public void DayStartTest()
		{
			Assert.AreEqual( TimeTool.DayStart( testDate ).Year, testDate.Year );
			Assert.AreEqual( TimeTool.DayStart( testDate ).Month, testDate.Month );
			Assert.AreEqual( TimeTool.DayStart( testDate ).Day, testDate.Day );
			Assert.AreEqual( TimeTool.DayStart( testDate ).Hour, 0 );
			Assert.AreEqual( TimeTool.DayStart( testDate ).Minute, 0 );
			Assert.AreEqual( TimeTool.DayStart( testDate ).Second, 0 );
			Assert.AreEqual( TimeTool.DayStart( testDate ).Millisecond, 0 );
		} // DayStartTest

		// ----------------------------------------------------------------------
		[Test]
		public void NextDayTest()
		{
			Assert.AreEqual( TimeTool.NextDay( DayOfWeek.Monday ), DayOfWeek.Tuesday );
			Assert.AreEqual( TimeTool.NextDay( DayOfWeek.Tuesday ), DayOfWeek.Wednesday );
			Assert.AreEqual( TimeTool.NextDay( DayOfWeek.Wednesday ), DayOfWeek.Thursday );
			Assert.AreEqual( TimeTool.NextDay( DayOfWeek.Thursday ), DayOfWeek.Friday );
			Assert.AreEqual( TimeTool.NextDay( DayOfWeek.Friday ), DayOfWeek.Saturday );
			Assert.AreEqual( TimeTool.NextDay( DayOfWeek.Saturday ), DayOfWeek.Sunday );
			Assert.AreEqual( TimeTool.NextDay( DayOfWeek.Sunday ), DayOfWeek.Monday );
		} // NextDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void PreviousDayTest()
		{
			Assert.AreEqual( TimeTool.PreviousDay( DayOfWeek.Monday ), DayOfWeek.Sunday );
			Assert.AreEqual( TimeTool.PreviousDay( DayOfWeek.Tuesday ), DayOfWeek.Monday );
			Assert.AreEqual( TimeTool.PreviousDay( DayOfWeek.Wednesday ), DayOfWeek.Tuesday );
			Assert.AreEqual( TimeTool.PreviousDay( DayOfWeek.Thursday ), DayOfWeek.Wednesday );
			Assert.AreEqual( TimeTool.PreviousDay( DayOfWeek.Friday ), DayOfWeek.Thursday );
			Assert.AreEqual( TimeTool.PreviousDay( DayOfWeek.Saturday ), DayOfWeek.Friday );
			Assert.AreEqual( TimeTool.PreviousDay( DayOfWeek.Sunday ), DayOfWeek.Saturday );
		} // PreviousDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void AddDaysTest()
		{
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Sunday, 1 ), DayOfWeek.Monday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Monday, 1 ), DayOfWeek.Tuesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Tuesday, 1 ), DayOfWeek.Wednesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Wednesday, 1 ), DayOfWeek.Thursday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Thursday, 1 ), DayOfWeek.Friday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Friday, 1 ), DayOfWeek.Saturday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Saturday, 1 ), DayOfWeek.Sunday );

			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Monday, -1 ), DayOfWeek.Sunday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Tuesday, -1 ), DayOfWeek.Monday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Wednesday, -1 ), DayOfWeek.Tuesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Thursday, -1 ), DayOfWeek.Wednesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Friday, -1 ), DayOfWeek.Thursday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Saturday, -1 ), DayOfWeek.Friday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Sunday, -1 ), DayOfWeek.Saturday );

			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Sunday, 14 ), DayOfWeek.Sunday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Monday, 14 ), DayOfWeek.Monday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Tuesday, 14 ), DayOfWeek.Tuesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Wednesday, 14 ), DayOfWeek.Wednesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Thursday, 14 ), DayOfWeek.Thursday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Friday, 14 ), DayOfWeek.Friday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Saturday, 14 ), DayOfWeek.Saturday );

			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Sunday, -14 ), DayOfWeek.Sunday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Monday, -14 ), DayOfWeek.Monday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Tuesday, -14 ), DayOfWeek.Tuesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Wednesday, -14 ), DayOfWeek.Wednesday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Thursday, -14 ), DayOfWeek.Thursday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Friday, -14 ), DayOfWeek.Friday );
			Assert.AreEqual( TimeTool.AddDays( DayOfWeek.Saturday, -14 ), DayOfWeek.Saturday );
		} // AddDaysTest

		#endregion

		// ----------------------------------------------------------------------
		// members
		private DateTime testDate = new DateTime( 2000, 10, 2, 13, 45, 53, 673 );
		private DateTime testDiffDate = new DateTime( 2002, 9, 3, 7, 14, 22, 234 );

	} // class TimeToolTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
