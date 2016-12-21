// -- FILE ------------------------------------------------------------------
// name       : Day.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public sealed class Day : DayTimeRange
	{

		// ----------------------------------------------------------------------
		public Day() :
			this( new TimeCalendar() )
		{
		} // Day

		// ----------------------------------------------------------------------
		public Day( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Day

		// ----------------------------------------------------------------------
		public Day( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Day

		// ----------------------------------------------------------------------
		public Day( DateTime moment, ITimeCalendar calendar ) :
			this( calendar.GetYear( moment ), calendar.GetMonth( moment ), calendar.GetDayOfMonth( moment ), calendar )
		{
		} // Day

		// ----------------------------------------------------------------------
		public Day( int year, int month ) :
			this( year, month, new TimeCalendar() )
		{
		} // Day

		// ----------------------------------------------------------------------
		public Day( int year, int month, ITimeCalendar calendar ) :
			this( year, month, 1, calendar )
		{
		} // Day

		// ----------------------------------------------------------------------
		public Day( int year, int month, int day ) :
			this( year, month, day, new TimeCalendar() )
		{
		} // Day

		// ----------------------------------------------------------------------
		public Day( int year, int month, int day, ITimeCalendar calendar ) :
			base( year, month, day, 1, calendar )
		{
		} // Day

		// ----------------------------------------------------------------------
		public int Year
		{
			get { return StartYear; }
		} // Year

		// ----------------------------------------------------------------------
		public int Month
		{
			get { return StartMonth; }
		} // Month

		// ----------------------------------------------------------------------
		public int DayValue
		{
			get { return StartDay; }
		} // DayValue

		// ----------------------------------------------------------------------
		public DayOfWeek DayOfWeek
		{
			get { return StartDayOfWeek; }
		} // DayOfWeek

		// ----------------------------------------------------------------------
		public string DayName
		{
			get { return StartDayName; }
		} // DayName

		// ----------------------------------------------------------------------
		public Day GetPreviousDay()
		{
			return AddDays( -1 );
		} // GetPreviousDay

		// ----------------------------------------------------------------------
		public Day GetNextDay()
		{
			return AddDays( 1 );
		} // GetNextDay

		// ----------------------------------------------------------------------
		public Day AddDays( int days )
		{
			DateTime startDay = new DateTime( StartYear, StartMonth, StartDay );
			return new Day( startDay.AddDays( days ), Calendar );
		} // AddDays

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( DayName, 
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Day

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
