// -- FILE ------------------------------------------------------------------
// name       : Week.cs
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
	public sealed class Week : WeekTimeRange
	{

		// ----------------------------------------------------------------------
		public Week() :
			this( new TimeCalendar() )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( DateTime moment, ITimeCalendar calendar ) :
			base( moment, 1, calendar )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( int year, int weekOfYear ) :
			this( year, weekOfYear, new TimeCalendar() )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( int year, int weekOfYear, ITimeCalendar calendar ) :
			base( year, weekOfYear, 1, calendar )
		{
		} // Week

		// ----------------------------------------------------------------------
		public int WeekOfYear
		{
			get { return StartWeek; }
		} // WeekOfYear

		// ----------------------------------------------------------------------
		public string WeekOfYearName
		{
			get { return StartWeekOfYearName; }
		} // WeekOfYearName

		// ----------------------------------------------------------------------
		public DateTime FirstDayOfWeek
		{
			get { return Start; }
		} // FirstDayOfWeek

		// ----------------------------------------------------------------------
		public DateTime LastDayOfWeek
		{
			get { return FirstDayOfWeek.AddDays( TimeSpec.DaysPerWeek - 1 ); }
		} // LastDayOfWeek

		// ----------------------------------------------------------------------
		public bool MultipleCalendarYears
		{
			get { return FirstDayOfWeek.Year != LastDayOfWeek.Year; }
		} // IsCalendarHalfyear

		// ----------------------------------------------------------------------
		public Week GetPreviousWeek()
		{
			return AddWeeks( -1 );
		} // GetPreviousWeek

		// ----------------------------------------------------------------------
		public Week GetNextWeek()
		{
			return AddWeeks( 1 );
		} // GetNextWeek

		// ----------------------------------------------------------------------
		public Week AddWeeks( int weeks )
		{
			DateTime startDate = TimeTool.GetStartOfYearWeek( Year, StartWeek, Calendar.Culture, Calendar.YearWeekType );
			return new Week( startDate.AddDays( weeks * TimeSpec.DaysPerWeek ), Calendar );
		} // AddWeeks

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( WeekOfYearName, 
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Week

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
