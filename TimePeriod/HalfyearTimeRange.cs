// -- FILE ------------------------------------------------------------------
// name       : HalfyearTimeRange.cs
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
	public abstract class HalfyearTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected HalfyearTimeRange( int startYear, YearHalfyear startHalfyear, int halfyearCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar, startYear, startHalfyear, halfyearCount ), calendar )
		{
			this.startYear = startYear;
			this.startHalfyear = startHalfyear;
			this.halfyearCount = halfyearCount;
			TimeTool.AddHalfyear( startYear, startHalfyear, halfyearCount - 1, out endYear, out endHalfyear );
		} // HalfyearTimeRange

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
		public YearHalfyear StartHalfyear
		{
			get { return startHalfyear; }
		} // StartHalfyear

		// ----------------------------------------------------------------------
		public YearHalfyear EndHalfyear
		{
			get { return endHalfyear; }
		} // EndHalfyear

		// ----------------------------------------------------------------------
		public int HalfyearCount
		{
			get { return halfyearCount; }
		} // HalfyearCount

		// ----------------------------------------------------------------------
		public string StartHalfyearName
		{
			get { return Calendar.GetHalfyearName( StartHalfyear ); }
		} // StartHalfyearName

		// ----------------------------------------------------------------------
		public string StartHalfyearOfYearName
		{
			get { return Calendar.GetHalfyearOfYearName( StartYear, StartHalfyear ); }
		} // StartHalfyearOfYearName

		// ----------------------------------------------------------------------
		public string EndHalfyearName
		{
			get { return Calendar.GetHalfyearName( EndHalfyear ); }
		} // EndHalfyearName

		// ----------------------------------------------------------------------
		public string EndHalfyearOfYearName
		{
			get { return Calendar.GetHalfyearOfYearName( EndYear, EndHalfyear ); }
		} // EndHalfyearOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetQuarters()
		{
			TimePeriodCollection quarters = new TimePeriodCollection();
			YearQuarter startQuarter = StartHalfyear == YearHalfyear.First ? YearQuarter.First : YearQuarter.Third;
			for ( int i = 0; i < halfyearCount; i++ )
			{
				for ( int quarter = 0; quarter < TimeSpec.QuartersPerHalfyear; quarter++ )
				{
					int year;
					YearQuarter yearQuarter;
					TimeTool.AddQuarter( startYear, startQuarter, ( i * TimeSpec.QuartersPerHalfyear ) + quarter, out year, out yearQuarter );
					quarters.Add( new Quarter( year, yearQuarter, Calendar ) );
				}
			}
			return quarters;
		} // GetQuarters

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			for ( int i = 0; i < halfyearCount; i++ )
			{
				for ( int month = 0; month < TimeSpec.MonthsPerHalfyear; month++ )
				{
					int year;
					YearMonth yearMonth;
					TimeTool.AddMonth( startYear, YearBaseMonth, ( i * TimeSpec.MonthsPerHalfyear ) + month, out year, out yearMonth );
					months.Add( new Month( year, yearMonth, Calendar ) );
				}
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as HalfyearTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( HalfyearTimeRange comp )
		{
			return
				startYear == comp.startYear &&
				startHalfyear == comp.startHalfyear &&
				halfyearCount == comp.halfyearCount &&
				endYear == comp.endYear &&
				endHalfyear == comp.endHalfyear;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startYear, startHalfyear, halfyearCount, endYear, endHalfyear );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static DateTime GetStartOfHalfyear( ITimeCalendar calendar, int year, YearHalfyear halfyear )
		{
			DateTime startOfHalfyear;

			switch ( calendar.YearType )
			{
				case YearType.FiscalYear:
					startOfHalfyear = FiscalCalendarTool.GetStartOfHalfyear( year, halfyear,
						calendar.YearBaseMonth, calendar.FiscalFirstDayOfYear, calendar.FiscalYearAlignment );
					break;
				default:
					DateTime yearStart = new DateTime( year, (int)calendar.YearBaseMonth, 1 );
					startOfHalfyear = yearStart.AddMonths( ( (int)halfyear - 1 ) * TimeSpec.MonthsPerHalfyear );
					break;
			}
			return startOfHalfyear;
		} // GetStartOfHalfyear

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( ITimeCalendar calendar, int startYear, YearHalfyear startHalfyear, int halfyearCount )
		{
			if ( halfyearCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "halfyearCount" );
			}

			DateTime start = GetStartOfHalfyear( calendar, startYear, startHalfyear );
			int endYear;
			YearHalfyear endHalfyear;
			TimeTool.AddHalfyear( startYear, startHalfyear, halfyearCount, out endYear, out endHalfyear );
			DateTime end = GetStartOfHalfyear( calendar, endYear, endHalfyear );

			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int startYear;
		private readonly YearHalfyear startHalfyear;
		private readonly int halfyearCount;
		private readonly int endYear; // cache
		private readonly YearHalfyear endHalfyear; // cache

	} // class HalfyearTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
