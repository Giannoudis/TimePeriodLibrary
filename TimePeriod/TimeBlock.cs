// -- FILE ------------------------------------------------------------------
// name       : TimeBlock.cs
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
	public class TimeBlock : ITimeBlock
	{

		// ----------------------------------------------------------------------
		public static readonly TimeBlock Anytime = new TimeBlock( true );

		// ----------------------------------------------------------------------
		public TimeBlock() :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate )
		{
		} // TimeBlock

		// ----------------------------------------------------------------------
		internal TimeBlock( bool isReadOnly = false ) :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, isReadOnly )
		{
		} // TimeBlock

		// ----------------------------------------------------------------------
		public TimeBlock( DateTime moment, bool isReadOnly = false ) :
			this( moment, TimeSpec.MinPeriodDuration, isReadOnly )
		{
		} // TimeBlock

		// ----------------------------------------------------------------------
		public TimeBlock( DateTime start, DateTime end, bool isReadOnly = false )
		{
			if ( start <= end )
			{
				this.start = start;
				this.end = end;
			}
			else
			{
				this.end = start;
				this.start = end;
			}
			duration = this.end - this.start;
			this.isReadOnly = isReadOnly;
		} // TimeBlock

		// ----------------------------------------------------------------------
		public TimeBlock( DateTime start, TimeSpan duration, bool isReadOnly = false )
		{
			if ( duration < TimeSpec.MinPeriodDuration )
			{
				throw new ArgumentOutOfRangeException( "duration" );
			}
			this.start = start;
			this.duration = duration;
			end = start.Add( duration );
			this.isReadOnly = isReadOnly;
		} // TimeBlock

		// ----------------------------------------------------------------------
		public TimeBlock( TimeSpan duration, DateTime end, bool isReadOnly = false )
		{
			if ( duration < TimeSpec.MinPeriodDuration )
			{
				throw new ArgumentOutOfRangeException( "duration" );
			}
			this.end = end;
			this.duration = duration;
			start = end.Subtract( duration );
			this.isReadOnly = isReadOnly;
		} // TimeBlock

		// ----------------------------------------------------------------------
		public TimeBlock( ITimePeriod copy )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			start = copy.Start;
			end = copy.End;
			duration = copy.Duration;
			isReadOnly = copy.IsReadOnly;
		} // TimeBlock

		// ----------------------------------------------------------------------
		protected TimeBlock( ITimePeriod copy, bool isReadOnly )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			start = copy.Start;
			end = copy.End;
			duration = copy.Duration;
			this.isReadOnly = isReadOnly;
		} // TimeBlock

		// ----------------------------------------------------------------------
		public bool IsReadOnly
		{
			get { return isReadOnly; }
		} // IsReadOnly

		// ----------------------------------------------------------------------
		public bool IsAnytime
		{
			get { return !HasStart && !HasEnd; }
		} // IsAnytime

		// ----------------------------------------------------------------------
		public bool IsMoment
		{
			get { return start.Equals( end ); }
		} // IsMoment

		// ----------------------------------------------------------------------
		public bool HasStart
		{
			get { return start != TimeSpec.MinPeriodDate; }
		} // HasStart

		// ----------------------------------------------------------------------
		public DateTime Start
		{
			get { return start; }
			set
			{
				CheckModification();
				start = value;
				end = start.Add( duration );
			}
		} // Start

		// ----------------------------------------------------------------------
		public bool HasEnd
		{
			get { return end != TimeSpec.MaxPeriodDate; }
		} // HasEnd

		// ----------------------------------------------------------------------
		public DateTime End
		{
			get { return end; }
			set
			{
				CheckModification();
				end = value;
				start = end.Subtract( duration );
			}
		} // End

		// ----------------------------------------------------------------------
		public TimeSpan Duration
		{
			get { return duration; }
			set { DurationFromStart( value ); }
		} // Duration

		// ----------------------------------------------------------------------
		public string DurationDescription
		{
			get { return TimeFormatter.Instance.GetDuration( Duration, DurationFormatType.Detailed ); }
		} // DurationDescription

		// ----------------------------------------------------------------------
		public virtual TimeSpan GetDuration( IDurationProvider provider )
		{
			if ( provider == null )
			{
				throw new ArgumentNullException( "provider" );
			}
			return provider.GetDuration( Start, End );
		} // GetDuration

		// ----------------------------------------------------------------------
		public virtual void Setup( DateTime newStart, DateTime newEnd )
		{
			CheckModification();
			if ( newStart <= newEnd )
			{
				start = newStart;
				end = newEnd;
			}
			else
			{
				end = newStart;
				start = newEnd;
			}
			duration = end - start;
		} // Setup

		// ----------------------------------------------------------------------
		public virtual void Setup( DateTime newStart, TimeSpan newDuration )
		{
			CheckModification();
			if ( newDuration < TimeSpec.MinPeriodDuration )
			{
				throw new ArgumentOutOfRangeException( "newDuration" );
			}
			start = newStart;
			duration = newDuration;
			end = start.Add( duration );
		} // Setup

		// ----------------------------------------------------------------------
		public ITimeBlock Copy()
		{
			return Copy( TimeSpan.Zero );
		} // Copy

		// ----------------------------------------------------------------------
		public virtual ITimeBlock Copy( TimeSpan offset )
		{
			return new TimeBlock( start.Add( offset ), end.Add( offset ), IsReadOnly );
		} // Copy

		// ----------------------------------------------------------------------
		public virtual void Move( TimeSpan offset )
		{
			CheckModification();
			if ( offset == TimeSpan.Zero )
			{
				return;
			}
			start = start.Add( offset );
			end = end.Add( offset );
		} // Move

		// ----------------------------------------------------------------------
		public ITimeBlock GetPreviousPeriod()
		{
			return GetPreviousPeriod( TimeSpan.Zero );
		} // GetPreviousPeriod

		// ----------------------------------------------------------------------
		public virtual ITimeBlock GetPreviousPeriod( TimeSpan offset )
		{
			return new TimeBlock( Duration, Start.Add( offset ), IsReadOnly );
		} // GetPreviousPeriod

		// ----------------------------------------------------------------------
		public ITimeBlock GetNextPeriod()
		{
			return GetNextPeriod( TimeSpan.Zero );
		} // GetNextPeriod

		// ----------------------------------------------------------------------
		public virtual ITimeBlock GetNextPeriod( TimeSpan offset )
		{
			return new TimeBlock( End.Add( offset ), Duration, IsReadOnly );
		} // GetNextPeriod

		// ----------------------------------------------------------------------
		public virtual void DurationFromStart( TimeSpan newDuration )
		{
			if ( newDuration < TimeSpec.MinPeriodDuration )
			{
				throw new ArgumentOutOfRangeException( "newDuration" );
			}
			CheckModification();

			duration = newDuration;
			end = start.Add( newDuration );
		} // DurationFromStart

		// ----------------------------------------------------------------------
		public virtual void DurationFromEnd( TimeSpan newDuration )
		{
			if ( newDuration < TimeSpec.MinPeriodDuration )
			{
				throw new ArgumentOutOfRangeException( "newDuration" );
			}
			CheckModification();

			duration = newDuration;
			start = end.Subtract( newDuration );
		} // DurationFromEnd

		// ----------------------------------------------------------------------
		public virtual bool IsSamePeriod( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return start == test.Start && end == test.End;
		} // IsSamePeriod

		// ----------------------------------------------------------------------
		public virtual bool HasInside( DateTime test )
		{
			return TimePeriodCalc.HasInside( this, test );
		} // HasInside

		// ----------------------------------------------------------------------
		public virtual bool HasInside( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.HasInside( this, test );
		} // HasInside

		// ----------------------------------------------------------------------
		public virtual bool IntersectsWith( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.IntersectsWith( this, test );
		} // IntersectsWith

		// ----------------------------------------------------------------------
		public virtual ITimeBlock GetIntersection( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			if ( !IntersectsWith( period ) )
			{
				return null;
			}
			DateTime periodStart = period.Start;
			DateTime periodEnd = period.End;
			return new TimeBlock(
				periodStart > start ? periodStart : start,
				periodEnd < end ? periodEnd : end,
				IsReadOnly );
		} // GetIntersection

		// ----------------------------------------------------------------------
		public virtual bool OverlapsWith( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.OverlapsWith( this, test );
		} // OverlapsWith

		// ----------------------------------------------------------------------
		public virtual PeriodRelation GetRelation( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.GetRelation( this, test );
		} // GetRelation

		// ----------------------------------------------------------------------
		public virtual int CompareTo( ITimePeriod other, ITimePeriodComparer comparer )
		{
			if ( other == null )
			{
				throw new ArgumentNullException( "other" );
			}
			if ( comparer == null )
			{
				throw new ArgumentNullException( "comparer" );
			}
			return comparer.Compare( this, other );
		} // CompareTo

		// ----------------------------------------------------------------------
		public virtual void Reset()
		{
			CheckModification();
			start = TimeSpec.MinPeriodDate;
			duration = TimeSpec.MaxPeriodDuration;
			end = TimeSpec.MaxPeriodDate;
		} // Reset

		// ----------------------------------------------------------------------
		public string GetDescription( ITimeFormatter formatter = null )
		{
			return Format( formatter ?? TimeFormatter.Instance );
		} // GetDescription

		// ----------------------------------------------------------------------
		protected virtual string Format( ITimeFormatter formatter )
		{
			return formatter.GetPeriod( start, end, duration );
		} // Format

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return GetDescription();
		} // ToString

		// ----------------------------------------------------------------------
		public sealed override bool Equals( object obj )
		{
			if ( obj == this )
			{
				return true;
			}
			if ( obj == null || GetType() != obj.GetType() )
			{
				return false;
			}
			return IsEqual( obj );
		} // Equals

		// ----------------------------------------------------------------------
		protected virtual bool IsEqual( object obj )
		{
			return HasSameData( obj as TimeBlock );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( TimeBlock comp )
		{
			return start == comp.start && end == comp.end && isReadOnly == comp.isReadOnly;
		} // HasSameData

		// ----------------------------------------------------------------------
		public sealed override int GetHashCode()
		{
			return HashTool.AddHashCode( GetType().GetHashCode(), ComputeHashCode() );
		} // GetHashCode

		// ----------------------------------------------------------------------
		protected virtual int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( isReadOnly, start, end, duration );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		protected void CheckModification()
		{
			if ( IsReadOnly )
			{
				throw new NotSupportedException( "TimeBlock is read-only" );
			}
		} // CheckModification

		// ----------------------------------------------------------------------
		// members
		private readonly bool isReadOnly;
		private DateTime start;
		private TimeSpan duration;
		private DateTime end;  // cache

	} // class TimeBlock

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
