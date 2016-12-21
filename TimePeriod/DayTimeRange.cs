// -- FILE ------------------------------------------------------------------
// name       : DayTimeRange.cs
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
	public abstract class DayTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected DayTimeRange( int startYear, int startMonth, int startDay, int dayCount ) :
			this( startYear, startMonth, startDay, dayCount, new TimeCalendar() )
		{
		} // DayTimeRange

		// ----------------------------------------------------------------------
		protected DayTimeRange( int startYear, int startMonth, int startDay, int dayCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( startYear, startMonth, startDay, dayCount ), calendar )
		{
			this.startDay = new DateTime( startYear, startMonth, startDay );
			this.dayCount = dayCount;
			endDay = calendar.MapEnd( this.startDay.AddDays( dayCount ) );
		} // DayTimeRange

		// ----------------------------------------------------------------------
		public int StartYear
		{
			get { return startDay.Year; }
		} // StartYear

		// ----------------------------------------------------------------------
		public int StartMonth
		{
			get { return startDay.Month; }
		} // StartMonth

		// ----------------------------------------------------------------------
		public int StartDay
		{
			get { return startDay.Day; }
		} // StartDay

		// ----------------------------------------------------------------------
		public int EndYear
		{
			get { return endDay.Year; }
		} // EndYear

		// ----------------------------------------------------------------------
		public int EndMonth
		{
			get { return endDay.Month; }
		} // EndMonth

		// ----------------------------------------------------------------------
		public int EndDay
		{
			get { return endDay.Day; }
		} // EndDay

		// ----------------------------------------------------------------------
		public int DayCount
		{
			get { return dayCount; }
		} // DayCount

		// ----------------------------------------------------------------------
		public DayOfWeek StartDayOfWeek
		{
			get { return Calendar.GetDayOfWeek( startDay ); }
		} // StartDayOfWeek

		// ----------------------------------------------------------------------
		public string StartDayName
		{
			get { return Calendar.GetDayName( StartDayOfWeek ); }
		} // StartDayName

		// ----------------------------------------------------------------------
		public DayOfWeek EndDayOfWeek
		{
			get { return Calendar.GetDayOfWeek( endDay ); }
		} // EndDayOfWeek

		// ----------------------------------------------------------------------
		public string EndDayName
		{
			get { return Calendar.GetDayName( EndDayOfWeek ); }
		} // EndDayName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetHours()
		{
			TimePeriodCollection hours = new TimePeriodCollection();
			DateTime startDate = startDay;
			for ( int day = 0; day < dayCount; day++ )
			{
				DateTime curDay = startDate.AddDays( day );
				for ( int hour = 0; hour < TimeSpec.HoursPerDay; hour++ )
				{
					hours.Add( new Hour( curDay.AddHours( hour ), Calendar ) );
				}
			}
			return hours;
		} // GetHours

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as DayTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( DayTimeRange comp )
		{
			return 
				startDay == comp.startDay && 
				dayCount == comp.dayCount && 
				endDay == comp.endDay;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startDay, dayCount, endDay );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, int month, int day, int dayCount )
		{
			if ( dayCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "dayCount" );
			}

			DateTime start = new DateTime( year, month, day );
			DateTime end = start.AddDays( dayCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime startDay;
		private readonly int dayCount;
		private readonly DateTime endDay; // cache

	} // class DayTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
