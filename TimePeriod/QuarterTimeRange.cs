// -- FILE ------------------------------------------------------------------
// name       : QuarterTimeRange.cs
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
	public abstract class QuarterTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected QuarterTimeRange( int startYear, YearQuarter startQuarter, int quarterCount ) :
			this( startYear, startQuarter, quarterCount, new TimeCalendar() )
		{
		} // QuarterTimeRange

		// ----------------------------------------------------------------------
		protected QuarterTimeRange( int startYear, YearQuarter startQuarter, int quarterCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar, startYear, startQuarter, quarterCount ), calendar )
		{
			this.startYear = startYear;
			this.startQuarter = startQuarter;
			this.quarterCount = quarterCount;
			TimeTool.AddQuarter( startYear, startQuarter, quarterCount - 1, out endYear, out endQuarter );
		} // QuarterTimeRange

		// ----------------------------------------------------------------------
		public override int BaseYear
		{
			get { return startYear; }
		} // BaseYear

		// ----------------------------------------------------------------------
		public int StartYear
		{
			get { return Calendar.GetYear( startYear, (int)Calendar.YearBaseMonth ); }
		} // StartYear

		// ----------------------------------------------------------------------
		public int EndYear
		{
			get { return Calendar.GetYear( endYear, (int)Calendar.YearBaseMonth ); }
		} // EndYear

		// ----------------------------------------------------------------------
		public YearQuarter StartQuarter
		{
			get { return startQuarter; }
		} // StartQuarter

		// ----------------------------------------------------------------------
		public YearQuarter EndQuarter
		{
			get { return endQuarter; }
		} // EndQuarter

		// ----------------------------------------------------------------------
		public int QuarterCount
		{
			get { return quarterCount; }
		} // QuarterCount

		// ----------------------------------------------------------------------
		public string StartQuarterName
		{
			get { return Calendar.GetQuarterName( StartQuarter ); }
		} // StartQuarterName

		// ----------------------------------------------------------------------
		public string StartQuarterOfYearName
		{
			get { return Calendar.GetQuarterOfYearName( StartYear, StartQuarter ); }
		} // StartQuarterOfYearName

		// ----------------------------------------------------------------------
		public string EndQuarterName
		{
			get { return Calendar.GetQuarterName( EndQuarter ); }
		} // EndQuarterName

		// ----------------------------------------------------------------------
		public string EndQuarterOfYearName
		{
			get { return Calendar.GetQuarterOfYearName( EndYear, EndQuarter ); }
		} // EndQuarterOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			for ( int i = 0; i < quarterCount; i++ )
			{
				for ( int month = 0; month < TimeSpec.MonthsPerQuarter; month++ )
				{
					int year;
					YearMonth yearMonth;
					TimeTool.AddMonth( startYear, YearBaseMonth, ( i * TimeSpec.MonthsPerQuarter ) + month, out year, out yearMonth );
					months.Add( new Month( year, yearMonth, Calendar ) );
				}
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as QuarterTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( QuarterTimeRange comp )
		{
			return
				startYear == comp.startYear &&
				startQuarter == comp.startQuarter &&
				quarterCount == comp.quarterCount &&
				endYear == comp.endYear &&
				endQuarter == comp.endQuarter;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startYear, startQuarter, quarterCount, endYear, endQuarter );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static DateTime GetStartOfQuarter( ITimeCalendar calendar, int year, YearQuarter quarter )
		{
			DateTime startOfQuarter;

			switch ( calendar.YearType )
			{
				case YearType.FiscalYear:
					startOfQuarter = FiscalCalendarTool.GetStartOfQuarter( year, quarter,
						calendar.YearBaseMonth, calendar.FiscalFirstDayOfYear, calendar.FiscalYearAlignment );
					break;
				default:
					DateTime yearStart = new DateTime( year, (int)calendar.YearBaseMonth, 1 );
					startOfQuarter = yearStart.AddMonths( ( (int)quarter - 1 ) * TimeSpec.MonthsPerQuarter );
					break;
			}

			return startOfQuarter;
		} // GetStartOfQuarter

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( ITimeCalendar calendar, int startYear, YearQuarter startQuarter, int quarterCount )
		{
			if ( quarterCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "quarterCount" );
			}

			DateTime start = GetStartOfQuarter( calendar, startYear, startQuarter );
			int endYear;
			YearQuarter endQuarter;
			TimeTool.AddQuarter( startYear, startQuarter, quarterCount, out endYear, out endQuarter );
			DateTime end = GetStartOfQuarter( calendar, endYear, endQuarter );

			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int startYear;
		private readonly YearQuarter startQuarter;
		private readonly int quarterCount;
		private readonly int endYear; // cache
		private readonly YearQuarter endQuarter; // cache

	} // class QuarterTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
