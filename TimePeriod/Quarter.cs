// -- FILE ------------------------------------------------------------------
// name       : Quarter.cs
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
	public sealed class Quarter : QuarterTimeRange
	{

		// ----------------------------------------------------------------------
		public Quarter() :
			this( new TimeCalendar() )
		{
		} // Quarter

		// ----------------------------------------------------------------------
		public Quarter( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Quarter

		// ----------------------------------------------------------------------
		public Quarter( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Quarter

		// ----------------------------------------------------------------------
		public Quarter( DateTime moment, ITimeCalendar calendar ) :
			this( TimeTool.GetYearOf( calendar.YearBaseMonth, calendar.GetYear( moment ), calendar.GetMonth( moment ) ),
				TimeTool.GetQuarterOfMonth( calendar.YearBaseMonth, (YearMonth)moment.Month ), calendar )
		{
		} // Quarter

		// ----------------------------------------------------------------------
		public Quarter( int baseYear, YearQuarter yearQuarter ) :
			this( baseYear, yearQuarter, new TimeCalendar() )
		{
		} // Quarter

		// ----------------------------------------------------------------------
		public Quarter( int baseYear, YearQuarter yearQuarter, ITimeCalendar calendar ) :
			base( baseYear, yearQuarter, 1, calendar )
		{
		} // Quarter

		// ----------------------------------------------------------------------
		public int Year
		{
			get
			{
				int year;
				YearMonth month;
				int monthCount = ( (int)StartQuarter - 1 ) * TimeSpec.MonthsPerQuarter;
				TimeTool.AddMonth( BaseYear, Calendar.YearBaseMonth, monthCount, out year, out month );
				return Calendar.GetYear( year, (int)month );
			}
		} // Year

		// ----------------------------------------------------------------------
		public YearMonth StartMonth
		{
			get
			{
				int year;
				YearMonth month;
				int monthCount = ( (int)StartQuarter - 1 ) * TimeSpec.MonthsPerQuarter;
				TimeTool.AddMonth( BaseYear, Calendar.YearBaseMonth, monthCount, out year, out month );
				return month;
			}
		} // YearHalfyear

		// ----------------------------------------------------------------------
		public YearQuarter YearQuarter
		{
			get { return StartQuarter; }
		} // YearQuarter

		// ----------------------------------------------------------------------
		public string QuarterName
		{
			get { return StartQuarterName; }
		} // QuarterName

		// ----------------------------------------------------------------------
		public string QuarterOfYearName
		{
			get { return StartQuarterOfYearName; }
		} // QuarterOfYearName

		// ----------------------------------------------------------------------
		public bool IsCalendarQuarter
		{
			get { return ( (int)YearBaseMonth - 1 ) % TimeSpec.MonthsPerQuarter == 0; }
		} // IsCalendarQuarter

		// ----------------------------------------------------------------------
		public bool MultipleCalendarYears
		{
			get
			{
				if ( IsCalendarQuarter )
				{
					return false;
				}
				int startYear;
				YearMonth month;
				int monthCount = ( (int)StartQuarter - 1 ) * TimeSpec.MonthsPerQuarter;
				TimeTool.AddMonth( BaseYear, YearBaseMonth, monthCount, out startYear, out month );

				int endYear;
				monthCount += TimeSpec.MonthsPerQuarter;
				TimeTool.AddMonth( BaseYear, YearBaseMonth, monthCount, out endYear, out month );
				return startYear != endYear;
			}
		} // IsCalendarHalfyear

		// ----------------------------------------------------------------------
		public Quarter GetPreviousQuarter()
		{
			return AddQuarters( -1 );
		} // GetPreviousQuarter

		// ----------------------------------------------------------------------
		public Quarter GetNextQuarter()
		{
			return AddQuarters( 1 );
		} // GetNextQuarter

		// ----------------------------------------------------------------------
		public Quarter AddQuarters( int count )
		{
			int year;
			YearQuarter quarter;
			TimeTool.AddQuarter( BaseYear, StartQuarter, count, out year, out quarter );
			return new Quarter( year, quarter, Calendar );
		} // AddQuarters

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( QuarterOfYearName,
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Quarter

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
