// -- FILE ------------------------------------------------------------------
// name       : Strings.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using System.Resources;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	internal sealed class Strings
	{

		#region Year

		// ----------------------------------------------------------------------
		public static string SystemYearName( int year )
		{
			return Format( inst.GetString( "SystemYearName" ), year );
		} // SystemYearName

		// ----------------------------------------------------------------------
		public static string CalendarYearName( int year )
		{
			return Format( inst.GetString( "CalendarYearName" ), year );
		} // CalendarYearName
		
		// ----------------------------------------------------------------------
		public static string FiscalYearName( int year )
		{
			return Format( inst.GetString( "FiscalYearName" ), year );
		} // FiscalYearName
		
		// ----------------------------------------------------------------------
		public static string SchoolYearName( int year )
		{
			return Format( inst.GetString( "SchoolYearName" ), year );
		} // SchoolYearName

		#endregion

		#region Halfyear

		// ----------------------------------------------------------------------
		public static string SystemHalfyearName( YearHalfyear yearHalfyear )
		{
			return Format( inst.GetString( "SystemHalfyearName" ), (int)yearHalfyear );
		} // SystemHalfyearName

		// ----------------------------------------------------------------------
		public static string CalendarHalfyearName( YearHalfyear yearHalfyear )
		{
			return Format( inst.GetString( "CalendarHalfyearName" ), (int)yearHalfyear );
		} // CalendarHalfyearName
		
		// ----------------------------------------------------------------------
		public static string FiscalHalfyearName( YearHalfyear yearHalfyear )
		{
			return Format( inst.GetString( "FiscalHalfyearName" ), (int)yearHalfyear );
		} // FiscalHalfyearName
		
		// ----------------------------------------------------------------------
		public static string SchoolHalfyearName( YearHalfyear yearHalfyear )
		{
			return Format( inst.GetString( "SchoolHalfyearName" ), (int)yearHalfyear );
		} // SchoolHalfyearName

		// ----------------------------------------------------------------------
		public static string SystemHalfyearOfYearName( YearHalfyear yearHalfyear, int year )
		{
			return Format( inst.GetString( "SystemHalfyearOfYearName" ), (int)yearHalfyear, year );
		} // SystemHalfyearOfYearName

		// ----------------------------------------------------------------------
		public static string CalendarHalfyearOfYearName( YearHalfyear yearHalfyear, int year )
		{
			return Format( inst.GetString( "CalendarHalfyearOfYearName" ), (int)yearHalfyear, year );
		} // CalendarHalfyearOfYearName
		
		// ----------------------------------------------------------------------
		public static string FiscalHalfyearOfYearName( YearHalfyear yearHalfyear, int year )
		{
			return Format( inst.GetString( "FiscalHalfyearOfYearName" ), (int)yearHalfyear, year );
		} // FiscalHalfyearOfYearName
		
		// ----------------------------------------------------------------------
		public static string SchoolHalfyearOfYearName( YearHalfyear yearHalfyear, int year )
		{
			return Format( inst.GetString( "SchoolHalfyearOfYearName" ), (int)yearHalfyear, year );
		} // SchoolHalfyearOfYearName

		#endregion

		#region Quarter

		// ----------------------------------------------------------------------
		public static string SystemQuarterName( YearQuarter yearQuarter )
		{
			return Format( inst.GetString( "SystemQuarterName" ), (int)yearQuarter );
		} // SystemQuarterName

		// ----------------------------------------------------------------------
		public static string CalendarQuarterName( YearQuarter yearQuarter )
		{
			return Format( inst.GetString( "CalendarQuarterName" ), (int)yearQuarter );
		} // CalendarQuarterName
		
		// ----------------------------------------------------------------------
		public static string FiscalQuarterName( YearQuarter yearQuarter )
		{
			return Format( inst.GetString( "FiscalQuarterName" ), (int)yearQuarter );
		} // FiscalQuarterName
		
		// ----------------------------------------------------------------------
		public static string SchoolQuarterName( YearQuarter yearQuarter )
		{
			return Format( inst.GetString( "SchoolQuarterName" ), (int)yearQuarter );
		} // SchoolQuarterName

		// ----------------------------------------------------------------------
		public static string SystemQuarterOfYearName( YearQuarter yearQuarter, int year )
		{
			return Format( inst.GetString( "SystemQuarterOfYearName" ), (int)yearQuarter, year );
		} // SystemQuarterOfYearName

		// ----------------------------------------------------------------------
		public static string CalendarQuarterOfYearName( YearQuarter yearQuarter, int year )
		{
			return Format( inst.GetString( "CalendarQuarterOfYearName" ), (int)yearQuarter, year );
		} // CalendarQuarterOfYearName
		
		// ----------------------------------------------------------------------
		public static string FiscalQuarterOfYearName( YearQuarter yearQuarter, int year )
		{
			return Format( inst.GetString( "FiscalQuarterOfYearName" ), (int)yearQuarter, year );
		} // FiscalQuarterOfYearName
		
		// ----------------------------------------------------------------------
		public static string SchoolQuarterOfYearName( YearQuarter yearQuarter, int year )
		{
			return Format( inst.GetString( "SchoolQuarterOfYearName" ), (int)yearQuarter, year );
		} // SchoolQuarterOfYearName

		#endregion

		#region Month

		// ----------------------------------------------------------------------
		public static string MonthOfYearName( string monthName, string yearName )
		{
			return Format( inst.GetString( "MonthOfYearName" ), monthName, yearName );
		} // MonthOfYearName

		#endregion

		#region Wek

		// ----------------------------------------------------------------------
		public static string WeekOfYearName( int weekOfYear, string yearName )
		{
			return Format( inst.GetString( "WeekOfYearName" ), weekOfYear, yearName );
		} // WeekOfYearName

		#endregion

		#region Time Formatter

		// ----------------------------------------------------------------------
		public static string TimeSpanYears
		{
			 get { return inst.GetString( "TimeSpanYears" ); }
		} // TimeSpanYears

		// ----------------------------------------------------------------------
		public static string TimeSpanYear
		{
			get { return inst.GetString( "TimeSpanYear" ); }
		} // TimeSpanYear
		
		// ----------------------------------------------------------------------
		public static string TimeSpanMonths
		{
			 get { return inst.GetString( "TimeSpanMonths" ); }
		} // TimeSpanMonths

		// ----------------------------------------------------------------------
		public static string TimeSpanMonth
		{
			get { return inst.GetString( "TimeSpanMonth" ); }
		} // TimeSpanMonth
		
		// ----------------------------------------------------------------------
		public static string TimeSpanWeeks
		{
			 get { return inst.GetString( "TimeSpanWeeks" ); }
		} // TimeSpanWeeks

		// ----------------------------------------------------------------------
		public static string TimeSpanWeek
		{
			get { return inst.GetString( "TimeSpanWeek" ); }
		} // TimeSpanWeek

		// ----------------------------------------------------------------------
		public static string TimeSpanDays
		{
			 get { return inst.GetString( "TimeSpanDays" ); }
		} // TimeSpanDays

		// ----------------------------------------------------------------------
		public static string TimeSpanDay
		{
			get { return inst.GetString( "TimeSpanDay" ); }
		} // TimeSpanDay

		// ----------------------------------------------------------------------
		public static string TimeSpanHours
		{
			get { return inst.GetString( "TimeSpanHours" ); }
		} // TimeSpanHours

		// ----------------------------------------------------------------------
		public static string TimeSpanHour
		{
			get { return inst.GetString( "TimeSpanHour" ); }
		} // TimeSpanHour

		// ----------------------------------------------------------------------
		public static string TimeSpanMinutes
		{
			get { return inst.GetString( "TimeSpanMinutes" ); }
		} // TimeSpanMinutes

		// ----------------------------------------------------------------------
		public static string TimeSpanMinute
		{
			get { return inst.GetString( "TimeSpanMinute" ); }
		} // TimeSpanMinute

		// ----------------------------------------------------------------------
		public static string TimeSpanSeconds
		{
			get { return inst.GetString( "TimeSpanSeconds" ); }
		} // TimeSpanSeconds

		// ----------------------------------------------------------------------
		public static string TimeSpanSecond
		{
			get { return inst.GetString( "TimeSpanSecond" ); }
		} // TimeSpanSecond

		#endregion

		// ----------------------------------------------------------------------
		private static string Format( string format, params object[] args )
		{
			return string.Format( CultureInfo.InvariantCulture, format, args );
		} // Format

		// ----------------------------------------------------------------------
		private static ResourceManager NewInst( Type singletonType )
		{
			if ( singletonType == null )
			{
				throw new ArgumentNullException( "singletonType" );
			}
			if ( singletonType.FullName == null )
			{
				throw new InvalidOperationException();
			}
			return new ResourceManager( singletonType.FullName, singletonType.Assembly );
		} // NewInst

		// ----------------------------------------------------------------------
		// members
		private static readonly ResourceManager inst = NewInst( typeof( Strings ) );

	} // class Strings

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
