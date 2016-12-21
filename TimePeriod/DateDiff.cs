// -- FILE ------------------------------------------------------------------
// name       : DateDiff.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.19
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public sealed class DateDiff
	{

		// ----------------------------------------------------------------------
		public DateDiff( DateTime date ) :
			this( date, SafeCurrentInfo.Calendar, SafeCurrentInfo.FirstDayOfWeek )
		{
		} // DateDiff

		// ----------------------------------------------------------------------
		public DateDiff( DateTime date, Calendar calendar, DayOfWeek firstDayOfWeek,
			YearMonth yearBaseMonth = TimeSpec.CalendarYearStartMonth ) :
			this( date, ClockProxy.Clock.Now, calendar, firstDayOfWeek, yearBaseMonth )
		{
		} // DateDiff

		// ----------------------------------------------------------------------
		public DateDiff( DateTime date1, DateTime date2 ) :
			this( date1, date2, SafeCurrentInfo.Calendar, SafeCurrentInfo.FirstDayOfWeek )
		{
		} // DateDiff

		// ----------------------------------------------------------------------
		public DateDiff( DateTime date1, DateTime date2, Calendar calendar,
			DayOfWeek firstDayOfWeek, YearMonth yearBaseMonth = TimeSpec.CalendarYearStartMonth )
		{
			if ( calendar == null )
			{
				throw new ArgumentNullException( "calendar" );
			}
			this.calendar = calendar;
			this.yearBaseMonth = yearBaseMonth;
			this.firstDayOfWeek = firstDayOfWeek;
			this.date1 = date1;
			this.date2 = date2;
			difference = date2.Subtract( date1 );
		} // DateDiff

		// ----------------------------------------------------------------------
		public DateDiff( TimeSpan difference ) :
			this( ClockProxy.Clock.Now, difference, SafeCurrentInfo.Calendar, SafeCurrentInfo.FirstDayOfWeek )
		{
		} // DateDiff

		// ----------------------------------------------------------------------
		public DateDiff( TimeSpan difference, Calendar calendar,
				DayOfWeek firstDayOfWeek, YearMonth yearBaseMonth = TimeSpec.CalendarYearStartMonth ) :
			this( ClockProxy.Clock.Now, difference, calendar, firstDayOfWeek, yearBaseMonth )
		{
		} // DateDiff

		// ----------------------------------------------------------------------
		public DateDiff( DateTime date1, TimeSpan difference ) :
			this( date1, difference, SafeCurrentInfo.Calendar, SafeCurrentInfo.FirstDayOfWeek )
		{
		} // DateDiff

		// ----------------------------------------------------------------------
		public DateDiff( DateTime date1, TimeSpan difference, Calendar calendar,
				DayOfWeek firstDayOfWeek, YearMonth yearBaseMonth = TimeSpec.CalendarYearStartMonth )
		{
			if ( calendar == null )
			{
				throw new ArgumentNullException( "calendar" );
			}
			this.calendar = calendar;
			this.yearBaseMonth = yearBaseMonth;
			this.firstDayOfWeek = firstDayOfWeek;
			this.date1 = date1;
			date2 = date1.Add( difference );
			this.difference = difference;
		} // DateDiff

		public static DateTimeFormatInfo SafeCurrentInfo
		{
			get { return ( DateTimeFormatInfo.CurrentInfo ?? DateTimeFormatInfo.InvariantInfo ); }
		} // SafeCurrentInfo

		// ----------------------------------------------------------------------
		public Calendar Calendar
		{
			get { return calendar; }
		} // Calendar

		// ----------------------------------------------------------------------
		public YearMonth YearBaseMonth
		{
			get { return yearBaseMonth; }
		} // YearBaseMonth

		// ----------------------------------------------------------------------
		public DayOfWeek FirstDayOfWeek
		{
			get { return firstDayOfWeek; }
		} // FirstDayOfWeek

		// ----------------------------------------------------------------------
		public DateTime Date1
		{
			get { return date1; }
		} // Date1

		// ----------------------------------------------------------------------
		public DateTime Date2
		{
			get { return date2; }
		} // Date2

		// ----------------------------------------------------------------------
		public TimeSpan Difference
		{
			get { return difference; }
		} // Difference

		// ----------------------------------------------------------------------
		public bool IsEmpty
		{
			get { return difference == TimeSpan.Zero; }
		} // IsEmpty

		// ----------------------------------------------------------------------
		private int Year1
		{
			get { return calendar.GetYear( Date1 ); }
		} // Year1

		// ----------------------------------------------------------------------
		private int Year2
		{
			get { return calendar.GetYear( Date2 ); }
		} // Year2

		// ----------------------------------------------------------------------
		public int Years
		{
			get
			{
				if ( !years.HasValue )
				{
					years = CalcYears();
				}
				return years.Value;
			}
		} // Years

		// ----------------------------------------------------------------------
		public int ElapsedYears
		{
			get
			{
				if ( !elapsedYears.HasValue )
				{
					elapsedYears = Years;
				}
				return elapsedYears.Value;
			}
		} // ElapsedYears

		// ----------------------------------------------------------------------
		public int Quarters
		{
			get
			{
				if ( !quarters.HasValue )
				{
					quarters = CalcQuarters();
				}
				return quarters.Value;
			}
		} // Quarters

		// ----------------------------------------------------------------------
		private int Month1
		{
			get { return calendar.GetMonth( Date1 ); }
		} // Month1

		// ----------------------------------------------------------------------
		private int Month2
		{
			get { return calendar.GetMonth( Date2 ); }
		} // Month2

		// ----------------------------------------------------------------------
		public int Months
		{
			get
			{
				if ( !months.HasValue )
				{
					months = CalcMonths();
				}
				return months.Value;
			}
		} // Months

		// ----------------------------------------------------------------------
		public int ElapsedMonths
		{
			get
			{
				if ( !elapsedMonths.HasValue )
				{
					elapsedMonths = Months - ( ElapsedYears * TimeSpec.MonthsPerYear );
				}
				return elapsedMonths.Value;
			}
		} // ElapsedMonths

		// ----------------------------------------------------------------------
		public int Weeks
		{
			get
			{
				if ( !weeks.HasValue )
				{
					weeks = CalcWeeks();
				}
				return weeks.Value;
			}
		} // Weeks

		// ----------------------------------------------------------------------
		public int Days
		{
			get { return (int)Math.Round( Round( difference.TotalDays ) ); }
		} // Days

		// ----------------------------------------------------------------------
		public int Weekdays
		{
			get { return ( (int)Math.Round( Round( difference.TotalDays ) ) ) / TimeSpec.DaysPerWeek; }
		} // Weekdays

		// ----------------------------------------------------------------------
		public int ElapsedDays
		{
			get
			{
				if ( !elapsedDays.HasValue )
				{
					DateTime compareDate = date1.AddYears( ElapsedYears ).AddMonths( ElapsedMonths );
					elapsedDays = (int)date2.Subtract( compareDate ).TotalDays;
				}
				return elapsedDays.Value;
			}
		} // ElapsedDays

		// ----------------------------------------------------------------------
		public int Hours
		{
			get { return (int)Math.Round( Round( difference.TotalHours ) ); }
		} // Hours

		// ----------------------------------------------------------------------
		public int ElapsedHours
		{
			get
			{
				if ( !elapsedHours.HasValue )
				{
					DateTime compareDate = date1.AddYears( ElapsedYears ).AddMonths( ElapsedMonths ).AddDays( ElapsedDays );
					elapsedHours = (int)date2.Subtract( compareDate ).TotalHours;
				}
				return elapsedHours.Value;
			}
		} // ElapsedHours

		// ----------------------------------------------------------------------
		public int Minutes
		{
			get { return (int)Math.Round( Round( difference.TotalMinutes ) ); }
		} // Minutes

		// ----------------------------------------------------------------------
		public int ElapsedMinutes
		{
			get
			{
				if ( !elapsedMinutes.HasValue )
				{
					DateTime compareDate = date1.AddYears(
						ElapsedYears ).AddMonths( ElapsedMonths ).AddDays( ElapsedDays ).AddHours( ElapsedHours );
					elapsedMinutes = (int)date2.Subtract( compareDate ).TotalMinutes;
				}
				return elapsedMinutes.Value;
			}
		} // ElapsedMinutes

		// ----------------------------------------------------------------------
		public int Seconds
		{
			get { return (int)Math.Round( Round( difference.TotalSeconds ) ); }
		} // Seconds

		// ----------------------------------------------------------------------
		public int ElapsedSeconds
		{
			get
			{
				if ( !elapsedSeconds.HasValue )
				{
					DateTime compareDate = date1.AddYears(
						ElapsedYears ).AddMonths(
						ElapsedMonths ).AddDays(
						ElapsedDays ).AddHours(
						ElapsedHours ).AddMinutes(
						ElapsedMinutes );
					elapsedSeconds = (int)date2.Subtract( compareDate ).TotalSeconds;
				}
				return elapsedSeconds.Value;
			}
		} // ElapsedSeconds

		// ----------------------------------------------------------------------
		public string GetDescription( int precision = int.MaxValue, ITimeFormatter formatter = null )
		{
			if ( precision < 1 )
			{
				throw new ArgumentOutOfRangeException( "precision" );
			}

			formatter = formatter ?? TimeFormatter.Instance;

			int[] elapsedItems = new int[ 6 ];
			elapsedItems[ 0 ] = ElapsedYears;
			elapsedItems[ 1 ] = ElapsedMonths;
			elapsedItems[ 2 ] = ElapsedDays;
			elapsedItems[ 3 ] = ElapsedHours;
			elapsedItems[ 4 ] = ElapsedMinutes;
			elapsedItems[ 5 ] = ElapsedSeconds;

			if ( precision <= elapsedItems.Length - 1 )
			{
				for ( int i = precision; i < elapsedItems.Length; i++ )
				{
					elapsedItems[ i ] = 0;
				}
			}

			return formatter.GetDuration(
				elapsedItems[ 0 ],
				elapsedItems[ 1 ],
				elapsedItems[ 2 ],
				elapsedItems[ 3 ],
				elapsedItems[ 4 ],
				elapsedItems[ 5 ] );
		} // GetDescription

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return GetDescription();
		} // ToString

		// ----------------------------------------------------------------------
		public override bool Equals( object obj )
		{
			if ( obj == this )
			{
				return true;
			}
			if ( obj == null || GetType() != obj.GetType() )
			{
				return false;
			}

			DateDiff comp = (DateDiff)obj;
			return calendar == comp.calendar &&
				yearBaseMonth == comp.yearBaseMonth &&
				firstDayOfWeek == comp.firstDayOfWeek &&
				date1 == comp.date1 &&
				date2 == comp.date2 &&
				difference == comp.difference;
		} // Equals

		// ----------------------------------------------------------------------
		public override int GetHashCode()
		{
			return HashTool.ComputeHashCode( GetType().GetHashCode(),
				calendar,
				yearBaseMonth,
				firstDayOfWeek,
				date1,
				date2,
				difference );
		} // GetHashCode

		// ----------------------------------------------------------------------
		private static double Round( double number )
		{
			if ( number >= 0.0 )
			{
				return Math.Floor( number );
			}
			return -Math.Floor( -number );
		} // Round

		// ----------------------------------------------------------------------
		private int CalcYears()
		{
			if ( TimeCompare.IsSameMonth( date1, date2 ) )
			{
				return 0;
			}

			int compareDay = date2.Day;
			int compareDaysPerMonth = calendar.GetDaysInMonth( Year1, Month2 );
			if ( compareDay > compareDaysPerMonth )
			{
				compareDay = compareDaysPerMonth;
			}
			DateTime compareDate = new DateTime( Year1, Month2, compareDay,
				date2.Hour, date2.Minute, date2.Second, date2.Millisecond );
			if ( date2 > date1 )
			{
				if ( compareDate < date1 )
				{
					compareDate = compareDate.AddYears( 1 );
				}
			}
			else
			{
				if ( compareDate > date1 )
				{
					compareDate = compareDate.AddYears( -1 );
				}
			}
			return Year2 - calendar.GetYear( compareDate );
		} // CalcYears

		// ----------------------------------------------------------------------
		private int CalcQuarters()
		{
			if ( TimeCompare.IsSameMonth( date1, date2 ) )
			{
				return 0;
			}

			int year1 = TimeTool.GetYearOf( yearBaseMonth, Year1, Month1 );
			YearQuarter quarter1 = TimeTool.GetQuarterOfMonth( yearBaseMonth, (YearMonth)Month1 );

			int year2 = TimeTool.GetYearOf( yearBaseMonth, Year2, Month2 );
			YearQuarter quarter2 = TimeTool.GetQuarterOfMonth( yearBaseMonth, (YearMonth)Month2 );

			return
				( ( year2 * TimeSpec.QuartersPerYear ) + quarter2 ) -
				( ( year1 * TimeSpec.QuartersPerYear ) + quarter1 );
		} // CalcQuarters

		// ----------------------------------------------------------------------
		private int CalcMonths()
		{
			if ( TimeCompare.IsSameDay( date1, date2 ) )
			{
				return 0;
			}

			int compareDay = date2.Day;
			int compareDaysPerMonth = calendar.GetDaysInMonth( Year1, Month1 );
			if ( compareDay > compareDaysPerMonth )
			{
				compareDay = compareDaysPerMonth;
			}

			DateTime compareDate = new DateTime( Year1, Month1, compareDay,
				date2.Hour, date2.Minute, date2.Second, date2.Millisecond );
			if ( date2 > date1 )
			{
				if ( compareDate < date1 )
				{
					compareDate = compareDate.AddMonths( 1 );
				}
			}
			else
			{
				if ( compareDate > date1 )
				{
					compareDate = compareDate.AddMonths( -1 );
				}
			}
			return
			( ( Year2 * TimeSpec.MonthsPerYear ) + Month2 ) -
			( ( calendar.GetYear( compareDate ) * TimeSpec.MonthsPerYear ) + calendar.GetMonth( compareDate ) );
		} // CalcMonths

		// ----------------------------------------------------------------------
		private int CalcWeeks()
		{
			if ( TimeCompare.IsSameDay( date1, date2 ) )
			{
				return 0;
			}

			DateTime week1 = TimeTool.GetStartOfWeek( date1, firstDayOfWeek );
			DateTime week2 = TimeTool.GetStartOfWeek( date2, firstDayOfWeek );
			if ( week1.Equals( week2 ) )
			{
				return 0;
			}

			return (int)( week2.Subtract( week1 ).TotalDays / TimeSpec.DaysPerWeek );
		} // CalcWeeks

		// ----------------------------------------------------------------------
		// members
		private readonly Calendar calendar;
		private readonly YearMonth yearBaseMonth;
		private readonly DayOfWeek firstDayOfWeek;
		private readonly DateTime date1;
		private readonly DateTime date2;
		private readonly TimeSpan difference;
		// cached values
		private int? years;
		private int? quarters;
		private int? months;
		private int? weeks;
		private int? elapsedYears;
		private int? elapsedMonths;
		private int? elapsedDays;
		private int? elapsedHours;
		private int? elapsedMinutes;
		private int? elapsedSeconds;

	} // class DateDiff

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
