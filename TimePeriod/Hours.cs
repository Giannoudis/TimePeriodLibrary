// -- FILE ------------------------------------------------------------------
// name       : Hours.cs
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
	public sealed class Hours : HourTimeRange
	{

		// ----------------------------------------------------------------------
		public Hours( DateTime moment, int count ) :
			this( moment, count, new TimeCalendar() )
		{
		} // Hours

		// ----------------------------------------------------------------------
		public Hours( DateTime moment, int count, ITimeCalendar calendar ) :
			this( calendar.GetYear( moment ), calendar.GetMonth( moment ),
				calendar.GetDayOfMonth( moment ), calendar.GetHour( moment ), count, calendar )
		{
		} // Hours

		// ----------------------------------------------------------------------
		public Hours( int startYear, int startMonth, int startDay, int hour, int hourCount ) :
			this( startYear, startMonth, startDay, hour, hourCount, new TimeCalendar() )
		{
		} // Hours

		// ----------------------------------------------------------------------
		public Hours( int startYear, int startMonth, int startDay, int hour, int hourCount, ITimeCalendar calendar ) :
			base( startYear, startMonth, startDay, hour, hourCount, calendar )
		{
		} // Hours

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetHours()
		{
			TimePeriodCollection hours = new TimePeriodCollection();
			DateTime startHour = new DateTime( StartYear, StartMonth, StartDay, StartHour, 0, 0 );
			for ( int i = 0; i < HourCount; i++ )
			{
				hours.Add( new Hour( startHour.AddHours( i ), Calendar ) );
			}
			return hours;
		} // GetHours

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( formatter.GetShortDate( Start ), formatter.GetShortDate( End ),
				formatter.GetShortTime( Start ), formatter.GetShortTime( End ), Duration );
		} // Format

	} // class Hours

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
