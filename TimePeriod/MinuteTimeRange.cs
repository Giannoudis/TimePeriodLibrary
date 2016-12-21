// -- FILE ------------------------------------------------------------------
// name       : MinuteTimeRange.cs
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
	public abstract class MinuteTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected MinuteTimeRange( int startYear, int startMonth, int startDay, int startHour, int startMinute, int minuteCount ) :
			this( startYear, startMonth, startDay, startHour, startMinute, minuteCount, new TimeCalendar() )
		{
		} // MinuteTimeRange

		// ----------------------------------------------------------------------
		protected MinuteTimeRange( int startYear, int startMonth, int startDay, int startHour, int startMinute, int minuteCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( startYear, startMonth, startDay, startHour, startMinute, minuteCount ), calendar )
		{
			this.startMinute = new DateTime( startYear, startMonth, startDay, startHour, startMinute, 0 );
			this.minuteCount = minuteCount;
			endMinute = this.startMinute.AddMinutes( minuteCount );
		} // MinuteTimeRange

		// ----------------------------------------------------------------------
		public int StartYear
		{
			get { return startMinute.Year; }
		} // StartYear

		// ----------------------------------------------------------------------
		public int StartMonth
		{
			get { return startMinute.Month; }
		} // StartMonth

		// ----------------------------------------------------------------------
		public int StartDay
		{
			get { return startMinute.Day; }
		} // StartDay

		// ----------------------------------------------------------------------
		public int StartHour
		{
			get { return startMinute.Hour; }
		} // StartHour

		// ----------------------------------------------------------------------
		public int StartMinute
		{
			get { return startMinute.Minute; }
		} // StartMinute

		// ----------------------------------------------------------------------
		public int EndYear
		{
			get { return endMinute.Year; }
		} // EndYear

		// ----------------------------------------------------------------------
		public int EndMonth
		{
			get { return endMinute.Month; }
		} // EndMonth

		// ----------------------------------------------------------------------
		public int EndDay
		{
			get { return endMinute.Day; }
		} // EndDay

		// ----------------------------------------------------------------------
		public int EndHour
		{
			get { return endMinute.Hour; }
		} // EndHour

		// ----------------------------------------------------------------------
		public int EndMinute
		{
			get { return endMinute.Minute; }
		} // EndMinute

		// ----------------------------------------------------------------------
		public int MinuteCount
		{
			get { return minuteCount; }
		} // MinuteCount

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as MinuteTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( MinuteTimeRange comp )
		{
			return startMinute == comp.startMinute && minuteCount == comp.minuteCount && endMinute == comp.endMinute;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startMinute, minuteCount, endMinute );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, int month, int day, int hour, int minute, int minuteCount )
		{
			if ( minuteCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "minuteCount" );
			}

			DateTime start = new DateTime( year, month, day, hour, minute, 0 );
			DateTime end = start.AddMinutes( minuteCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime startMinute;
		private readonly int minuteCount;
		private readonly DateTime endMinute; // cache

	} // class MinuteTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
