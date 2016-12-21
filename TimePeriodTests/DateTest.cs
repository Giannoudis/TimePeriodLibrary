// -- FILE ------------------------------------------------------------------
// name       : DateTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.08.24
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
	public sealed class DateTest : TestUnitBase
	{

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorTest()
		{
			Date date = new Date( 2009, 7, 22 );

			Assert.AreEqual( date.Year, 2009 );
			Assert.AreEqual( date.Month, 7 );
			Assert.AreEqual( date.Day, 22 );
		} // ConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorYearTest()
		{
			Date date = new Date( 2009 );

			Assert.AreEqual( date.Year, 2009 );
			Assert.AreEqual( date.Month, 1 );
			Assert.AreEqual( date.Day, 1 );
		} // ConstructorYearTest

		// ----------------------------------------------------------------------
		[Test]
		public void ConstructorMonthTest()
		{
			Date date = new Date( 2009, 7 );

			Assert.AreEqual( date.Year, 2009 );
			Assert.AreEqual( date.Month, 7 );
			Assert.AreEqual( date.Day, 1 );
		} // ConstructorMonthTest


		// ----------------------------------------------------------------------
		[Test]
		public void DefaultConstructorTest()
		{
			const int year = 2009;
			Date date = new Date( year );

			Assert.AreEqual( date.Year, year );
			Assert.AreEqual( date.Month, 1 );
			Assert.AreEqual( date.Day, 1 );
		} // DefaultConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void DateTimeConstructorTest()
		{
			DateTime dateTime = new DateTime( 2009, 7, 22, 18, 23, 56, 344 );
			Date date = new Date( dateTime );

			Assert.AreEqual( date.Year, dateTime.Year );
			Assert.AreEqual( date.Month, dateTime.Month );
			Assert.AreEqual( date.Day, dateTime.Day );
		} // DateTimeConstructorTest

		// ----------------------------------------------------------------------
		[Test]
		public void MinValueTest()
		{
			new Date( DateTime.MinValue );
		} // MinValueTest

		// ----------------------------------------------------------------------
		[Test]
		public void MaxValueTest()
		{
			new Date( DateTime.MaxValue );
		} // MinValueTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinYearTest()
		{
			new Date( DateTime.MinValue.Year - 1 );
		} // MinYearTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxYearTest()
		{
			new Date( DateTime.MaxValue.Year + 1 );
		} // MaxYearTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinMonthTest()
		{
			new Date( DateTime.MinValue.Year, 0 );
		} // MinMonthTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxMonthTest()
		{
			new Date( DateTime.MaxValue.Year, TimeSpec.MonthsPerYear + 1 );
		} // MaxMonthTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MinDayTest()
		{
			new Date( DateTime.MinValue.Year, 1, 0 );
		} // MinDayTest

		// ----------------------------------------------------------------------
		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void MaxDayTest()
		{
			new Date( DateTime.MinValue.Year, 1, TimeSpec.MaxDaysPerMonth + 1 );
		} // MaxDayTest

		// ----------------------------------------------------------------------
		[Test]
		public void DateTimeTest1()
		{
			DateTime dateTime1 = new DateTime( 2009, 7, 22 );
			Date date1 = new Date( dateTime1 );
			Assert.AreEqual( date1.DateTime, dateTime1.Date );

			DateTime dateTime2 = new DateTime( 2009, 7, 22, 18, 23, 56, 344 );
			Date date2 = new Date( dateTime2 );
			Assert.AreEqual( date2.DateTime, dateTime2.Date );
		} // DateTimeTest1

		// ----------------------------------------------------------------------
		[Test]
		public void ToDateTimeTest2()
		{
			DateTime dateTime = new DateTime( 2009, 7, 22 );
			Date date = new Date( dateTime );

			Assert.AreEqual( date.DateTime, dateTime );
			Assert.AreEqual( date.ToDateTime( 1 ), new DateTime( dateTime.Year, dateTime.Month, dateTime.Day, 1, 0, 0, 0 ) );
			Assert.AreEqual( date.ToDateTime( 1, 1 ), new DateTime( dateTime.Year, dateTime.Month, dateTime.Day, 1, 1, 0, 0 ) );
			Assert.AreEqual( date.ToDateTime( 1, 1, 1 ), new DateTime( dateTime.Year, dateTime.Month, dateTime.Day, 1, 1, 1, 0 ) );
			Assert.AreEqual( date.ToDateTime( 1, 1, 1, 1 ), new DateTime( dateTime.Year, dateTime.Month, dateTime.Day, 1, 1, 1, 1 ) );
		} // ToDateTimeTest2

		// ----------------------------------------------------------------------
		[Test]
		public void GetDateTimeFromTimeTest()
		{
			DateTime dateTime = new DateTime( 2009, 7, 22 );
			Date date = new Date( dateTime );
			TimeSpan timeSpan = new TimeSpan( 0, 18, 23, 56, 344 );
			Time time = new Time( timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds );

			Assert.AreEqual( date.ToDateTime( time ), dateTime.Add( timeSpan ) );
		} // GetDateTimeFromTimeTest

		// ----------------------------------------------------------------------
		[Test]
		public void CompareToTest()
		{
			DateTime now = ClockProxy.Clock.Now.Date;
			Date date = new Date( now );
			Date dateBefore = new Date( now.AddDays( -1 ) );
			Date dateAfter = new Date( now.AddDays( 1 ) );

			Assert.AreEqual( 0, date.CompareTo( date ) );
			Assert.AreEqual( 1, date.CompareTo( dateBefore ) );
			Assert.AreEqual( -1, date.CompareTo( dateAfter ) );
		} // CompareToTest

		// ----------------------------------------------------------------------
		[Test]
		public void EquatableTest()
		{
			DateTime now = ClockProxy.Clock.Now.Date;
			Date date = new Date( now );
			Date dateBefore = new Date( now.AddDays( -1 ) );
			Date dateAfter = new Date( now.AddDays( 1 ) );

			Assert.AreEqual( true, date.Equals( date ) );
			Assert.AreEqual( false, date.Equals( dateBefore ) );
			Assert.AreEqual( false, date.Equals( dateAfter ) );
		} // EquatableTest

		// ----------------------------------------------------------------------
		[Test]
		public void OperatorTest()
		{
			DateTime now = ClockProxy.Clock.Now.Date;
			Date date = new Date( now );
			Date dateBefore = new Date( now.AddDays( -1 ) );
			Date dateAfter = new Date( now.AddDays( 1 ) );

			TimeSpan oneDay = new TimeSpan( 1, 0, 0, 0, 0 );
			TimeSpan halfDay = new TimeSpan( 0, 12, 30, 30, 500 );

			Assert.AreEqual( dateBefore, date - oneDay );
			Assert.AreEqual( dateBefore, date - halfDay );
			Assert.AreEqual( dateBefore, date - oneDay );
			Assert.AreEqual( oneDay, date - dateBefore );
			// no  operator +( Date, Date )
			Assert.AreEqual( dateAfter, date + oneDay );
			Assert.AreEqual( dateAfter, date + oneDay + halfDay );

			Assert.AreEqual( true, dateBefore < date );
			Assert.AreEqual( false, dateAfter <= date );

			Assert.AreEqual( true, dateBefore <= date );
			Assert.AreEqual( false, dateAfter <= date );

			Assert.AreEqual( false, dateBefore == date );
			Assert.AreEqual( false, dateAfter == date );

			Assert.AreEqual( true, dateBefore != date );
			Assert.AreEqual( true, dateAfter != date );

			Assert.AreEqual( false, dateBefore >= date );
			Assert.AreEqual( true, dateAfter >= date );

			Assert.AreEqual( false, dateBefore > date );
			Assert.AreEqual( true, dateAfter > date );
		} // OperatorTest

	} // class DateTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
