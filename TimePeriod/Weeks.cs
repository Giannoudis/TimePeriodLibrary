// -- FILE ------------------------------------------------------------------
// name       : Weeks.cs
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
	public sealed class Weeks : WeekTimeRange
	{

		// ----------------------------------------------------------------------
		public Weeks( DateTime moment, int count ) :
			this( moment,  count, new TimeCalendar() )
		{
		} // Weeks

		// ----------------------------------------------------------------------
		public Weeks( DateTime moment, int count, ITimeCalendar calendar ) :
			base( moment, count, calendar )
		{
		} // Weeks

		// ----------------------------------------------------------------------
		public Weeks( int year, int startWeek, int count ) :
			this( year, startWeek, count, new TimeCalendar() )
		{
		} // Weeks

		// ----------------------------------------------------------------------
		public Weeks( int year, int startWeek, int count, ITimeCalendar calendar ) :
			base( year, startWeek, count, calendar )
		{
		} // Weeks

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetWeeks()
		{
			TimePeriodCollection weeks = new TimePeriodCollection();
			for ( int i = 0; i < WeekCount; i++ )
			{
				weeks.Add( new Week( Year, StartWeek + i, Calendar ) );
			}
			return weeks;
		} // GetWeeks

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( StartWeekOfYearName, EndWeekOfYearName, 
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Weeks

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
