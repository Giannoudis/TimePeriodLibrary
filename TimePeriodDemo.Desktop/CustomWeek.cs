// -- FILE ------------------------------------------------------------------
// name       : CustomWeek.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class CustomWeek : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		public CustomWeek() :
			this( new TimeCalendar() )
		{
		} // CustomWeek

		// ----------------------------------------------------------------------
		public CustomWeek( ITimeCalendar calendar ) :
			this( DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek, calendar )
		{
		} // CustomWeek

		// ----------------------------------------------------------------------
		public CustomWeek( DayOfWeek firstDayOfWeek ) :
			this( ClockProxy.Clock.Now, firstDayOfWeek, new TimeCalendar() )
		{
		} // CustomWeek

		// ----------------------------------------------------------------------
		public CustomWeek( DayOfWeek firstDayOfWeek, ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, firstDayOfWeek, calendar )
		{
		} // CustomWeek

		// ----------------------------------------------------------------------
		public CustomWeek( DateTime moment, DayOfWeek firstDayOfWeek ) :
			this( moment, firstDayOfWeek, new TimeCalendar() )
		{
		} // CustomWeek

		// ----------------------------------------------------------------------
		public CustomWeek( DateTime moment, DayOfWeek firstDayOfWeek, ITimeCalendar calendar ) :
			base( GetPeriodOf( moment, firstDayOfWeek ), calendar )
		{
		} // CustomWeek

		// ----------------------------------------------------------------------
		public DayOfWeek FirstDayOfWeek
		{
			get { return Start.DayOfWeek; }
		} // FirstDayOfWeek

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetDays()
		{
			TimePeriodCollection days = new TimePeriodCollection();
			DateTime startDate = new DateTime( Start.Year, Start.Month, Start.Day );
			for ( int day = 0; day < TimeSpec.DaysPerWeek; day++ )
			{
				days.Add( new Day( startDate.AddDays( day ), Calendar ) );
			}
			return days;
		} // GetDays

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( DateTime moment, DayOfWeek firstDayOfWeek )
		{
			DateTime start = TimeTool.GetStartOfWeek( moment, firstDayOfWeek );
			DateTime end = start.AddDays( TimeSpec.DaysPerWeek );
			return new TimeRange( start, end );
		} // GetPeriodOf

	} // class CustomWeek

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
