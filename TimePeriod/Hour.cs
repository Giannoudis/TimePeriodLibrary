// -- FILE ------------------------------------------------------------------
// name       : Hour.cs
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
	public sealed class Hour : HourTimeRange
	{

		// ----------------------------------------------------------------------
		public Hour() :
			this( new TimeCalendar() )
		{
		} // Hour

		// ----------------------------------------------------------------------
		public Hour( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Hour

		// ----------------------------------------------------------------------
		public Hour( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Hour

		// ----------------------------------------------------------------------
		public Hour( DateTime moment, ITimeCalendar calendar ) :
			this( calendar.GetYear( moment ), calendar.GetMonth( moment ),
				calendar.GetDayOfMonth( moment ), calendar.GetHour( moment ), calendar )
		{
		} // Hour

		// ----------------------------------------------------------------------
		public Hour( int year, int month, int day, int hour ) :
			this( year, month, day, hour, new TimeCalendar() )
		{
		} // Hour

		// ----------------------------------------------------------------------
		public Hour( int year, int month, int day, int hour, ITimeCalendar calendar ) :
			base( year, month, day, hour, 1, calendar )
		{
		} // Hour

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
		public int HourValue
		{
			get { return StartHour; }
		} // HourValue

		// ----------------------------------------------------------------------
		public Hour GetPreviousHour()
		{
			return AddHours( -1 );
		} // GetPreviousHour

		// ----------------------------------------------------------------------
		public Hour GetNextHour()
		{
			return AddHours( 1 );
		} // GetNextHour

		// ----------------------------------------------------------------------
		public Hour AddHours( int hours )
		{
			DateTime startHour = new DateTime( StartYear, StartMonth, StartDay, StartHour, 0, 0 );
			return new Hour( startHour.AddHours( hours ), Calendar );
		} // AddHours

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( formatter.GetShortDate( Start ),
				formatter.GetShortTime( Start ), formatter.GetShortTime( End ), Duration );
		} // Format

	} // class Hour

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
