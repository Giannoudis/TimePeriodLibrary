// -- FILE ------------------------------------------------------------------
// name       : Year.cs
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
	public sealed class Year : YearTimeRange
	{

		// ----------------------------------------------------------------------
		public Year() :
			this( new TimeCalendar() )
		{
		} // Year

		// ----------------------------------------------------------------------
		public Year( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Year

		// ----------------------------------------------------------------------
		public Year( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Year

		// ----------------------------------------------------------------------
		public Year( DateTime moment, ITimeCalendar calendar ) :
			this( TimeTool.GetYearOf( calendar.YearBaseMonth, calendar.GetYear( moment ), calendar.GetMonth( moment ) ), calendar )
		{
		} // Year

		// ----------------------------------------------------------------------
		public Year( int year ) :
			this( year, new TimeCalendar() )
		{
		} // Year

		// ----------------------------------------------------------------------
		public Year( int year, ITimeCalendar calendar ) :
			base( year, 1, calendar )
		{
		} // Year

		// ----------------------------------------------------------------------
		public int YearValue
		{
			get { return Calendar.GetYear( BaseYear, (int)YearBaseMonth ); }
		} // YearValue

		// ----------------------------------------------------------------------
		public string YearName
		{
			get { return StartYearName; }
		} // YearName

		// ----------------------------------------------------------------------
		public bool IsCalendarYear
		{
			get { return YearBaseMonth == TimeSpec.CalendarYearStartMonth; }
		} // IsCalendarYearPeriod

		// ----------------------------------------------------------------------
		public Year GetPreviousYear()
		{
			return AddYears( -1 );
		} // GetPreviousYear

		// ----------------------------------------------------------------------
		public Year GetNextYear()
		{
			return AddYears( 1 );
		} // GetNextYear

		// ----------------------------------------------------------------------
		public Year AddYears( int count )
		{
			DateTime startDate = new DateTime( BaseYear, (int)YearBaseMonth, 1 );
			return new Year( startDate.AddYears( count ), Calendar );
		} // AddYears

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( YearName,
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Year

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
