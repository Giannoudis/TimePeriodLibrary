// -- FILE ------------------------------------------------------------------
// name       : Time.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.10
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public struct Time : IComparable, IComparable<Time>, IEquatable<Time>
	{

		// ----------------------------------------------------------------------
		public Time( DateTime dateTime )
		{
			duration = dateTime.TimeOfDay;
		} // Time

		// ----------------------------------------------------------------------
		public Time( TimeSpan duration ) :
			this( Math.Abs( duration.Hours ), Math.Abs( duration.Minutes ),
						Math.Abs( duration.Seconds ), Math.Abs( duration.Milliseconds ) )
		{
		} // Time

		// ----------------------------------------------------------------------
		public Time( int hour = 0, int minute = 0, int second = 0, int millisecond = 0 )
		{
			if ( hour < 0 || hour > TimeSpec.HoursPerDay )
			{
				throw new ArgumentOutOfRangeException( "hour" );
			}
			if ( hour == TimeSpec.HoursPerDay )
			{
				if ( minute > 0 )
				{
					throw new ArgumentOutOfRangeException( "minute" );
				}
				if ( second > 0 )
				{
					throw new ArgumentOutOfRangeException( "second" );
				}
				if ( millisecond > 0 )
				{
					throw new ArgumentOutOfRangeException( "millisecond" );
				}
			}
			if ( minute < 0 || minute >= TimeSpec.MinutesPerHour )
			{
				throw new ArgumentOutOfRangeException( "minute" );
			}
			if ( second < 0 || second >= TimeSpec.SecondsPerMinute )
			{
				throw new ArgumentOutOfRangeException( "second" );
			}
			if ( millisecond < 0 || millisecond >= TimeSpec.MillisecondsPerSecond )
			{
				throw new ArgumentOutOfRangeException( "millisecond" );
			}

			duration = new TimeSpan( 0, hour, minute, second, millisecond );
		} // Time

		// ----------------------------------------------------------------------
		public int Hour
		{
			get { return duration.Hours; }
		} // Hour

		// ----------------------------------------------------------------------
		public int Minute
		{
			get { return duration.Minutes; }
		} // Minute

		// ----------------------------------------------------------------------
		public int Second
		{
			get { return duration.Seconds; }
		} // Second

		// ----------------------------------------------------------------------
		public int Millisecond
		{
			get { return duration.Milliseconds; }
		} // Millisecond

		// ----------------------------------------------------------------------
		public TimeSpan Duration
		{
			get { return duration; }
		} // Duration

		// ----------------------------------------------------------------------
		public bool IsZero
		{
			get { return duration.Equals( TimeSpan.Zero ); }
		} // IsZero

		// ----------------------------------------------------------------------
		public bool IsFullDay
		{
			get { return (int)duration.TotalHours == TimeSpec.HoursPerDay; }
		} // IsFullDay

		// ----------------------------------------------------------------------
		public bool IsFullDayOrZero
		{
			get { return IsFullDay || IsZero; }
		} // IsFullDayOrZero

		// ----------------------------------------------------------------------
		public long Ticks
		{
			get { return duration.Ticks; }
		} // Ticks

		// ----------------------------------------------------------------------
		public double TotalHours
		{
			get { return duration.TotalHours; }
		} // TotalHours

		// ----------------------------------------------------------------------
		public double TotalMinutes
		{
			get { return duration.TotalMinutes; }
		} // TotalMinutes

		// ----------------------------------------------------------------------
		public double TotalSeconds
		{
			get { return duration.TotalSeconds; }
		} // TotalSeconds

		// ----------------------------------------------------------------------
		public double TotalMilliseconds
		{
			get { return duration.TotalMilliseconds; }
		} // TotalMilliseconds

		// ----------------------------------------------------------------------
		public int CompareTo( Time other )
		{
			return duration.CompareTo( other.duration );
		} // CompareTo

		// ----------------------------------------------------------------------
		public int CompareTo( object obj )
		{
			return duration.CompareTo( ((Time)obj).duration );
		} // CompareTo

		// ----------------------------------------------------------------------
		public bool Equals( Time other )
		{
			return duration.Equals( other.duration );
		} // Equals

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return ( (int)TotalHours ).ToString( "00" ) + ":" + Minute.ToString( "00" ) +
				":" + Second.ToString( "00" ) + "." + Millisecond.ToString( "000" );
		} // ToString

		// ----------------------------------------------------------------------
		public override bool Equals( object obj )
		{
			if ( obj == null || GetType() != obj.GetType() )
			{
				return false;
			}

			return Equals( (Time)obj );
		} // Equals

		// ----------------------------------------------------------------------
		public override int GetHashCode()
		{
			return HashTool.ComputeHashCode( GetType().GetHashCode(), duration );
		} // GetHashCode

		// ----------------------------------------------------------------------
		public static TimeSpan operator -( Time time1, Time time2 )
		{
			return ( time1 - time2.duration ).duration;
		} // operator -

		// ----------------------------------------------------------------------
		public static Time operator -( Time time, TimeSpan duration )
		{
			if ( Equals( duration, TimeSpan.Zero ) )
			{
				return time;
			}
			DateTime day = duration > TimeSpan.Zero ? DateTime.MaxValue.Date : DateTime.MinValue.Date;
			return new Time( time.ToDateTime( day ).Subtract( duration ) );
		} // operator -

		// ----------------------------------------------------------------------
		public static TimeSpan operator +( Time time1, Time time2 )
		{
			return ( time1 + time2.duration ).duration;
		} // operator +

		// ----------------------------------------------------------------------
		public static Time operator +( Time time, TimeSpan duration )
		{
			if ( Equals( duration, TimeSpan.Zero ) )
			{
				return time;
			}
			DateTime day = duration > TimeSpan.Zero ? DateTime.MinValue : DateTime.MaxValue;
			return new Time( time.ToDateTime( day ).Add( duration ) );
		} // operator +

		// ----------------------------------------------------------------------
		public static bool operator <( Time time1, Time time2 )
		{
			return time1.duration < time2.duration;
		} // operator <

		// ----------------------------------------------------------------------
		public static bool operator <=( Time time1, Time time2 )
		{
			return time1.duration <= time2.duration;
		} // operator <=

		// ----------------------------------------------------------------------
		public static bool operator ==( Time left, Time right )
		{
			return Equals( left, right );
		} // operator ==

		// ----------------------------------------------------------------------
		public static bool operator !=( Time left, Time right )
		{
			return !Equals( left, right );
		} // operator !=

		// ----------------------------------------------------------------------
		public static bool operator >( Time time1, Time time2 )
		{
			return time1.duration > time2.duration;
		} // operator >

		// ----------------------------------------------------------------------
		public static bool operator >=( Time time1, Time time2 )
		{
			return time1.duration >= time2.duration;
		} // operator >=

		// ----------------------------------------------------------------------
		public DateTime ToDateTime( Date date )
		{
			return ToDateTime( date.DateTime );
		} // ToDateTime

		// ----------------------------------------------------------------------
		public DateTime ToDateTime( DateTime dateTime )
		{
			return ToDateTime( dateTime, this );
		} // ToDateTime

		// ----------------------------------------------------------------------
		public static DateTime ToDateTime( Date date, Time time )
		{
			return ToDateTime( date.DateTime, time );
		} // ToDateTime

		// ----------------------------------------------------------------------
		public static DateTime ToDateTime( DateTime dateTime, Time time )
		{
			return dateTime.Date.Add( time.Duration );
		} // ToDateTime

		// ----------------------------------------------------------------------
		// members
		private readonly TimeSpan duration;

	} // struct Time

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
