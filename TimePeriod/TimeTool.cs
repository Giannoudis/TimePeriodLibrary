// -- FILE ------------------------------------------------------------------
// name       : TimeTool.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public static class TimeTool
	{

		#region Date and Time

		// ----------------------------------------------------------------------
		public static DateTime GetDate( DateTime dateTime )
		{
			return dateTime.Date;
		} // GetDate

		// ----------------------------------------------------------------------
		public static DateTime SetDate( DateTime from, DateTime to )
		{
			return SetDate( from, to.Year, to.Month, to.Day );
		} // SetDate

		// ----------------------------------------------------------------------
		public static DateTime SetDate( DateTime from, int year, int month = 1, int day = 1 )
		{
			return new DateTime( year, month, day, from.Hour, from.Minute, from.Second, from.Millisecond );
		} // SetDate

		// ----------------------------------------------------------------------
		public static bool HasTimeOfDay( DateTime dateTime )
		{
			return dateTime.TimeOfDay > TimeSpan.Zero;
		} // HasTimeOfDay

		// ----------------------------------------------------------------------
		public static DateTime SetTimeOfDay( DateTime from, DateTime to )
		{
			return SetTimeOfDay( from, to.Hour, to.Minute, to.Second, to.Millisecond );
		} // SetTimeOfDay

		// ----------------------------------------------------------------------
		public static DateTime SetTimeOfDay( DateTime from, int hour = 0, int minute = 0, int second = 0, int millisecond = 0 )
		{
			return new DateTime( from.Year, from.Month, from.Day, hour, minute, second, millisecond );
		} // SetTimeOfDay

		#endregion

		#region Year

		// ----------------------------------------------------------------------
		public static int GetYearOf( YearMonth yearBaseMonth, DateTime moment )
		{
			return GetYearOf( yearBaseMonth, moment.Year, moment.Month );
		} // GetYearOf

		// ----------------------------------------------------------------------
		public static int GetYearOf( YearMonth yearBaseMonth, int year, int month )
		{
			return month >= (int)yearBaseMonth ? year : year - 1;
		} // GetYearOf

		#endregion

		#region Halfyear

		// ----------------------------------------------------------------------
		public static void NextHalfyear( YearHalfyear startHalfyear, out int year, out YearHalfyear halfyear )
		{
			AddHalfyear( startHalfyear, 1, out year, out halfyear );
		} // NextHalfyear

		// ----------------------------------------------------------------------
		public static void PreviousHalfyear( YearHalfyear startHalfyear, out int year, out YearHalfyear halfyear )
		{
			AddHalfyear( startHalfyear, -1, out year, out halfyear );
		} // PreviousHalfyear

		// ----------------------------------------------------------------------
		public static void AddHalfyear( YearHalfyear startHalfyear, int count, out int year, out YearHalfyear halfyear )
		{
			AddHalfyear( 0, startHalfyear, count, out year, out halfyear );
		} // AddHalfyear

		// ----------------------------------------------------------------------
		public static void AddHalfyear( int startYear, YearHalfyear startHalfyear, int count, out int year, out YearHalfyear halfyear )
		{
			int offsetYear = ( Math.Abs( count ) / TimeSpec.HalfyearsPerYear ) + 1;
			int startHalfyearCount = ( ( startYear + offsetYear ) * TimeSpec.HalfyearsPerYear ) + ( (int)startHalfyear - 1 );
			int targetHalfyearCount = startHalfyearCount + count;

			year = ( targetHalfyearCount / TimeSpec.HalfyearsPerYear ) - offsetYear;
			halfyear = (YearHalfyear)( ( targetHalfyearCount % TimeSpec.HalfyearsPerYear ) + 1 );
		} // AddHalfyear

		// ----------------------------------------------------------------------
		public static YearHalfyear GetHalfyearOfMonth( YearMonth yearMonth )
		{
			return GetHalfyearOfMonth( TimeSpec.CalendarYearStartMonth, yearMonth );
		} // GetHalfyearOfMonth

		// ----------------------------------------------------------------------
		public static YearHalfyear GetHalfyearOfMonth( YearMonth yearBaseMonth, YearMonth yearMonth )
		{
			int yearMonthIndex = (int)yearMonth - 1;
			int yearStartMonthIndex = (int)yearBaseMonth - 1;
			if ( yearMonthIndex < yearStartMonthIndex )
			{
				yearMonthIndex += TimeSpec.MonthsPerYear;
			}
			int deltaMonths = yearMonthIndex - yearStartMonthIndex;
			return (YearHalfyear)( ( deltaMonths / TimeSpec.MonthsPerHalfyear ) + 1 );
		} // GetHalfyearOfMonth

		// ----------------------------------------------------------------------
		public static YearMonth[] GetMonthsOfHalfyear( YearHalfyear yearHalfyear )
		{
			switch ( yearHalfyear )
			{
				case YearHalfyear.First:
					return TimeSpec.FirstHalfyearMonths;
				case YearHalfyear.Second:
					return TimeSpec.SecondHalfyearMonths;
			}
			throw new InvalidOperationException( "invalid year halfyear " + yearHalfyear );
		} // GetMonthsOfHalfyear

		#endregion

		#region Quarter

		// ----------------------------------------------------------------------
		public static void NextQuarter( YearQuarter startQuarter, out int year, out YearQuarter quarter )
		{
			AddQuarter( startQuarter, 1, out year, out quarter );
		} // NextQuarter

		// ----------------------------------------------------------------------
		public static void PreviousQuarter( YearQuarter startQuarter, out int year, out YearQuarter quarter )
		{
			AddQuarter( startQuarter, -1, out year, out quarter );
		} // PreviousQuarter

		// ----------------------------------------------------------------------
		public static void AddQuarter( YearQuarter startQuarter, int count, out int year, out YearQuarter quarter )
		{
			AddQuarter( 0, startQuarter, count, out year, out quarter );
		} // AddQuarter

		// ----------------------------------------------------------------------
		public static void AddQuarter( int startYear, YearQuarter startQuarter, int count, out int year, out YearQuarter quarter )
		{
			int offsetYear = ( Math.Abs( count ) / TimeSpec.QuartersPerYear ) + 1;
			int startQuarterCount = ( ( startYear + offsetYear ) * TimeSpec.QuartersPerYear ) + ( (int)startQuarter - 1 );
			int targetQuarterCount = startQuarterCount + count;

			year = ( targetQuarterCount / TimeSpec.QuartersPerYear ) - offsetYear;
			quarter = (YearQuarter)( ( targetQuarterCount % TimeSpec.QuartersPerYear ) + 1 );
		} // AddQuarter

		// ----------------------------------------------------------------------
		public static YearQuarter GetQuarterOfMonth( YearMonth yearMonth )
		{
			return GetQuarterOfMonth( TimeSpec.CalendarYearStartMonth, yearMonth );
		} // GetQuarterOfMonth

		// ----------------------------------------------------------------------
		public static YearQuarter GetQuarterOfMonth( YearMonth yearBaseMonth, YearMonth yearMonth )
		{
			int yearMonthIndex = (int)yearMonth - 1;
			int yearStartMonthIndex = (int)yearBaseMonth - 1;
			if ( yearMonthIndex < yearStartMonthIndex )
			{
				yearMonthIndex += TimeSpec.MonthsPerYear;
			}
			int deltaMonths = yearMonthIndex - yearStartMonthIndex;
			return (YearQuarter)( ( deltaMonths / TimeSpec.MonthsPerQuarter ) + 1 );
		} // GetQuarterOfMonth

		// ----------------------------------------------------------------------
		public static YearMonth[] GetMonthsOfQuarter( YearQuarter yearQuarter )
		{
			switch ( yearQuarter )
			{
				case YearQuarter.First:
					return TimeSpec.FirstQuarterMonths;
				case YearQuarter.Second:
					return TimeSpec.SecondQuarterMonths;
				case YearQuarter.Third:
					return TimeSpec.ThirdQuarterMonths;
				case YearQuarter.Fourth:
					return TimeSpec.FourthQuarterMonths;
			}
			throw new InvalidOperationException( "invalid year quarter " + yearQuarter );
		} // GetMonthsOfQuarter

		#endregion

		#region Month

		// ----------------------------------------------------------------------
		public static void NextMonth( YearMonth startMonth, out int year, out YearMonth month )
		{
			AddMonth( startMonth, 1, out year, out month );
		} // NextMonth

		// ----------------------------------------------------------------------
		public static void PreviousMonth( YearMonth startMonth, out int year, out YearMonth month )
		{
			AddMonth( startMonth, -1, out year, out month );
		} // PreviousMonth

		// ----------------------------------------------------------------------
		public static void AddMonth( YearMonth startMonth, int count, out int year, out YearMonth month )
		{
			AddMonth( 0, startMonth, count, out year, out month );
		} // AddMonth

		// ----------------------------------------------------------------------
		public static void AddMonth( int startYear, YearMonth startMonth, int count, out int year, out YearMonth month )
		{
			int offsetYear = ( Math.Abs( count ) / TimeSpec.MonthsPerYear ) + 1;
			int startMonthCount = ( ( startYear + offsetYear ) * TimeSpec.MonthsPerYear ) + ( (int)startMonth - 1 );
			int targetMonthCount = startMonthCount + count;

			year = ( targetMonthCount / TimeSpec.MonthsPerYear ) - offsetYear;
			month = (YearMonth)( ( targetMonthCount % TimeSpec.MonthsPerYear ) + 1 );
		} // AddMonth

		// ----------------------------------------------------------------------
		public static int GetDaysInMonth( int year, int month )
		{
			DateTime firstDay = new DateTime( year, month, 1 );
			return firstDay.AddMonths( 1 ).AddDays( -1 ).Day;
		} // GetDaysInMonth

		#endregion

		#region Week

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfWeek( DateTime time, DayOfWeek firstDayOfWeek )
		{
			DateTime currentDay = new DateTime( time.Year, time.Month, time.Day );
			while ( currentDay.DayOfWeek != firstDayOfWeek )
			{
				currentDay = currentDay.AddDays( -1 );
			}
			return currentDay;
		} // GetStartOfWeek

		// ----------------------------------------------------------------------
		public static void GetWeekOfYear( DateTime moment, CultureInfo culture, YearWeekType yearWeekType,
			out int year, out int weekOfYear )
		{
			GetWeekOfYear( moment, culture, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek, yearWeekType,
				out year, out weekOfYear );
		} // GetWeekOfYear

		// ----------------------------------------------------------------------
		public static void GetWeekOfYear( DateTime moment, CultureInfo culture,
			CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek, YearWeekType yearWeekType, out int year, out int weekOfYear )
		{
			if ( culture == null )
			{
				throw new ArgumentNullException( "culture" );
			}

			if ( yearWeekType == YearWeekType.Iso8601 && weekRule == CalendarWeekRule.FirstFourDayWeek )
			{
				// see http://blogs.msdn.com/b/shawnste/archive/2006/01/24/517178.aspx
				DayOfWeek day = culture.Calendar.GetDayOfWeek( moment );
				if ( day >= firstDayOfWeek && (int)day <= (int)( firstDayOfWeek + 2 ) % 7 )
				{
					moment = moment.AddDays( 3 );
				}
			}

			weekOfYear = culture.Calendar.GetWeekOfYear( moment, weekRule, firstDayOfWeek );
			year = moment.Year;
			if ( weekOfYear >= 52 && moment.Month < 12 )
			{
				year--;
			}
		} // GetWeekOfYear

		// ----------------------------------------------------------------------
		public static int GetWeeksOfYear( int year, CultureInfo culture, YearWeekType yearWeekType )
		{
			return GetWeeksOfYear( year, culture, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek, yearWeekType );
		} // GetWeeksOfYear

		// ----------------------------------------------------------------------
		public static int GetWeeksOfYear( int year, CultureInfo culture,
			CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek, YearWeekType yearWeekType )
		{
			if ( culture == null )
			{
				throw new ArgumentNullException( "culture" );
			}

			int currentYear;
			int currentWeek;
			DateTime currentDay = new DateTime( year, 12, 31 );
			GetWeekOfYear( currentDay, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek );
			while ( currentYear != year )
			{
				currentDay = currentDay.AddDays( -1 );
				GetWeekOfYear( currentDay, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek );
			}
			return currentWeek;
		} // GetWeeksOfYear

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfYearWeek( int year, int weekOfYear, CultureInfo culture, YearWeekType yearWeekType )
		{
			if ( culture == null )
			{
				throw new ArgumentNullException( "culture" );
			}
			return GetStartOfYearWeek( year, weekOfYear, culture,
				culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek, yearWeekType );
		} // GetStartOfYearWeek

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfYearWeek( int year, int weekOfYear, CultureInfo culture,
			CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek, YearWeekType yearWeekType )
		{
			if ( culture == null )
			{
				throw new ArgumentNullException( "culture" );
			}
			if ( weekOfYear < 1 )
			{
				throw new ArgumentOutOfRangeException( "weekOfYear" );
			}

			DateTime dateTime = new DateTime( year, 1, 1 ).AddDays( weekOfYear * TimeSpec.DaysPerWeek );
			int currentYear;
			int currentWeek;
			GetWeekOfYear( dateTime, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek );


			// end date of week
			while ( currentWeek != weekOfYear )
			{
				dateTime = dateTime.AddDays( -1 );
				GetWeekOfYear( dateTime, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek );
			}

			// end of previous week
			while ( currentWeek == weekOfYear )
			{
				dateTime = dateTime.AddDays( -1 );
				GetWeekOfYear( dateTime, culture, weekRule, firstDayOfWeek, yearWeekType, out currentYear, out currentWeek );
			}

			return dateTime.AddDays( 1 );
		} // GetStartOfYearWeek

		#endregion

		#region Day

		// ----------------------------------------------------------------------
		public static DateTime DayStart( DateTime dateTime )
		{
			return dateTime.Date;
		} // DayStart

		// ----------------------------------------------------------------------
		public static DayOfWeek NextDay( DayOfWeek day )
		{
			return AddDays( day, 1 );
		} // NextMonth

		// ----------------------------------------------------------------------
		public static DayOfWeek PreviousDay( DayOfWeek day )
		{
			return AddDays( day, -1 );
		} // PreviousDay

		// ----------------------------------------------------------------------
		public static DayOfWeek AddDays( DayOfWeek day, int days )
		{
			if ( days == 0 )
			{
				return day;
			}
			int weeks = ( Math.Abs( days ) / TimeSpec.DaysPerWeek ) + 1;

			int offset = weeks * TimeSpec.DaysPerWeek + (int)day;
			int targetOffset = offset + days;
			return (DayOfWeek)( targetOffset % TimeSpec.DaysPerWeek );
		} // AddMonths

		#endregion

	} // class TimeTool

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
