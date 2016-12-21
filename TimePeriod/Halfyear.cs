// -- FILE ------------------------------------------------------------------
// name       : Halfyear.cs
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
	public sealed class Halfyear : HalfyearTimeRange
	{

		// ----------------------------------------------------------------------
		public Halfyear() :
			this( new TimeCalendar() )
		{
		} // Halfyear

		// ----------------------------------------------------------------------
		public Halfyear( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Halfyear

		// ----------------------------------------------------------------------
		public Halfyear( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Halfyear

		// ----------------------------------------------------------------------
		public Halfyear( DateTime moment, ITimeCalendar calendar ) :
			this( TimeTool.GetYearOf( calendar.YearBaseMonth, calendar.GetYear( moment ), calendar.GetMonth( moment ) ),
				TimeTool.GetHalfyearOfMonth( calendar.YearBaseMonth, (YearMonth)calendar.GetMonth( moment ) ), calendar )
		{
		} // Halfyear

		// ----------------------------------------------------------------------
		public Halfyear( int year, YearHalfyear yearHalfyear ) :
			this( year, yearHalfyear, new TimeCalendar() )
		{
		} // Halfyear

		// ----------------------------------------------------------------------
		public Halfyear( int year, YearHalfyear yearHalfyear, ITimeCalendar calendar ) :
			base( year, yearHalfyear, 1, calendar )
		{
		} // Halfyear

		// ----------------------------------------------------------------------
		public int Year
		{
			get { return StartYear; }
		} // Year

		// ----------------------------------------------------------------------
		public YearMonth StartMonth
		{
			get
			{
				int year;
				YearMonth month;
				int monthCount = ( (int)StartHalfyear - 1 ) * TimeSpec.MonthsPerHalfyear;
				TimeTool.AddMonth( BaseYear, Calendar.YearBaseMonth, monthCount, out year, out month );
				return month;
			}
		} // StartMonth

		// ----------------------------------------------------------------------
		public YearHalfyear YearHalfyear
		{
			get { return StartHalfyear; }
		} // YearHalfyear

		// ----------------------------------------------------------------------
		public string HalfyearName
		{
			get { return StartHalfyearName; }
		} // HalfyearName

		// ----------------------------------------------------------------------
		public string HalfyearOfYearName
		{
			get { return StartHalfyearOfYearName; }
		} // HalfyearOfYearName

		// ----------------------------------------------------------------------
		public bool IsCalendarHalfyear
		{
			get { return ( (int)YearBaseMonth - 1 ) % TimeSpec.MonthsPerHalfyear == 0; }
		} // IsCalendarHalfyear

		// ----------------------------------------------------------------------
		public bool MultipleCalendarYears
		{
			get
			{
				if ( IsCalendarHalfyear )
				{
					return false;
				}

				int startYear;
				YearMonth month;
				int monthCount = ( (int)StartHalfyear - 1 ) * TimeSpec.MonthsPerHalfyear;
				TimeTool.AddMonth( BaseYear, YearBaseMonth, monthCount, out startYear, out month );

				int endYear;
				monthCount += TimeSpec.MonthsPerHalfyear;
				TimeTool.AddMonth( BaseYear, YearBaseMonth, monthCount, out endYear, out month );
				return startYear != endYear;
			}
		} // MultipleCalendarYears

		// ----------------------------------------------------------------------
		public Halfyear GetPreviousHalfyear()
		{
			return AddHalfyears( -1 );
		} // GetPreviousHalfyear

		// ----------------------------------------------------------------------
		public Halfyear GetNextHalfyear()
		{
			return AddHalfyears( 1 );
		} // GetNextHalfyear

		// ----------------------------------------------------------------------
		public Halfyear AddHalfyears( int count )
		{
			int year;
			YearHalfyear halfyear;
			TimeTool.AddHalfyear( BaseYear, StartHalfyear, count, out year, out halfyear );
			return new Halfyear( year, halfyear, Calendar );
		} // AddHalfyears

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( HalfyearOfYearName,
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Halfyear

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
