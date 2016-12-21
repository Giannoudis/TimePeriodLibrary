// -- FILE ------------------------------------------------------------------
// name       : Minute.cs
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
	public sealed class Minute : MinuteTimeRange
	{

		// ----------------------------------------------------------------------
		public Minute() :
			this( new TimeCalendar() )
		{
		} // Minute

		// ----------------------------------------------------------------------
		public Minute( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Minute

		// ----------------------------------------------------------------------
		public Minute( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Minute

		// ----------------------------------------------------------------------
		public Minute( DateTime moment, ITimeCalendar calendar ) :
			this( calendar.GetYear( moment ), calendar.GetMonth( moment ), calendar.GetDayOfMonth( moment ),
			calendar.GetHour( moment ), calendar.GetMinute( moment ), calendar )
		{
		} // Minute

		// ----------------------------------------------------------------------
		public Minute( int year, int month, int day, int hour, int minute ) :
			this( year, month, day, hour, minute, new TimeCalendar() )
		{
		} // Minute

		// ----------------------------------------------------------------------
		public Minute( int year, int month, int day, int hour, int minute, ITimeCalendar calendar ) :
			base( year, month, day, hour, minute, 1, calendar )
		{
		} // Minute

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
		public int Day
		{
			get { return StartDay; }
		} // Day

		// ----------------------------------------------------------------------
		public int Hour
		{
			get { return StartHour; }
		} // Hour

		// ----------------------------------------------------------------------
		public int MinuteValue
		{
			get { return StartMinute; }
		} // MinuteValue

		// ----------------------------------------------------------------------
		public Minute GetPreviousMinute()
		{
			return AddMinutes( -1 );
		} // GetPreviousMinute

		// ----------------------------------------------------------------------
		public Minute GetNextMinute()
		{
			return AddMinutes( 1 );
		} // GetNextMinute

		// ----------------------------------------------------------------------
		public Minute AddMinutes( int minutes )
		{
			DateTime startMinute = new DateTime( StartYear, StartMonth, StartDay, StartHour, StartMinute, 0 );
			return new Minute( startMinute.AddMinutes( minutes ), Calendar );
		} // AddMinutes

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( formatter.GetShortDate( Start ),
				formatter.GetShortTime( Start ), formatter.GetShortTime( End ), Duration );
		} // Format

	} // class Minute

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
