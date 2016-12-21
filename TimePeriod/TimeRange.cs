// -- FILE ------------------------------------------------------------------
// name       : TimeRange.cs
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
	public class TimeRange : ITimeRange
	{

		// ----------------------------------------------------------------------
		public static readonly TimeRange Anytime = new TimeRange( true );

		// ----------------------------------------------------------------------
		public TimeRange() :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate )
		{
		} // TimeRange

		// ----------------------------------------------------------------------
		internal TimeRange( bool isReadOnly = false ) :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, isReadOnly )
		{
		} // TimeRange

		// ----------------------------------------------------------------------
		public TimeRange( DateTime moment, bool isReadOnly = false ) :
			this( moment, moment, isReadOnly )
		{
		} // TimeRange

		// ----------------------------------------------------------------------
		public TimeRange( DateTime start, DateTime end, bool isReadOnly = false )
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
			this.isReadOnly = isReadOnly;
		} // TimeRange

		// ----------------------------------------------------------------------
		public TimeRange( DateTime start, TimeSpan duration, bool isReadOnly = false )
		{
			if ( duration >= TimeSpan.Zero )
			{
				this.start = start;
				end = start.Add( duration );
			}
			else
			{
				this.start = start.Add( duration );
				end = start;
			}
			this.isReadOnly = isReadOnly;
		} // TimeRange

		// ----------------------------------------------------------------------
		public TimeRange( ITimePeriod copy )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			start = copy.Start;
			end = copy.End;
			isReadOnly = copy.IsReadOnly;
		} // TimeRange

		// ----------------------------------------------------------------------
		protected TimeRange( ITimePeriod copy, bool isReadOnly )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			start = copy.Start;
			end = copy.End;
			this.isReadOnly = isReadOnly;
		} // TimeRange

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
				if ( value > end )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				start = value;
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
				if ( value < start )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				end = value;
			}
		} // End

		// ----------------------------------------------------------------------
		public TimeSpan Duration
		{
			get { return end.Subtract( start ); }
			set
			{
				CheckModification();
				end = start.Add( value );
			}
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
		} // Setup

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
		public ITimeRange Copy()
		{
			return Copy( TimeSpan.Zero );
		} // Copy

		// ----------------------------------------------------------------------
		public virtual ITimeRange Copy( TimeSpan offset )
		{
			return new TimeRange( start.Add( offset ), end.Add( offset ), IsReadOnly );
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
		public virtual void ExpandStartTo( DateTime moment )
		{
			CheckModification();
			if ( start > moment )
			{
				start = moment;
			}
		} // ExpandStartTo

		// ----------------------------------------------------------------------
		public virtual void ExpandEndTo( DateTime moment )
		{
			CheckModification();
			if ( end < moment )
			{
				end = moment;
			}
		} // ExpandEndTo

		// ----------------------------------------------------------------------
		public void ExpandTo( DateTime moment )
		{
			ExpandStartTo( moment );
			ExpandEndTo( moment );
		} // ExpandTo

		// ----------------------------------------------------------------------
		public void ExpandTo( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			ExpandStartTo( period.Start );
			ExpandEndTo( period.End );
		} // ExpandTo

		// ----------------------------------------------------------------------
		public virtual void ShrinkStartTo( DateTime moment )
		{
			CheckModification();
			if ( HasInside( moment ) && start < moment )
			{
				start = moment;
			}
		} // ShrinkStartTo

		// ----------------------------------------------------------------------
		public virtual void ShrinkEndTo( DateTime moment )
		{
			CheckModification();
			if ( HasInside( moment ) && end > moment )
			{
				end = moment;
			}
		} // ShrinkEndTo

		// ----------------------------------------------------------------------
		public void ShrinkTo( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			ShrinkStartTo( period.Start );
			ShrinkEndTo( period.End );
		} // ShrinkTo

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
		public virtual ITimeRange GetIntersection( ITimePeriod period )
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
			return new TimeRange(
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
			return formatter.GetPeriod( start, end, Duration );
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
			return HasSameData( obj as TimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( TimeRange comp )
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
			return HashTool.ComputeHashCode( isReadOnly, start, end );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		protected void CheckModification()
		{
			if ( IsReadOnly )
			{
				throw new NotSupportedException( "TimeRange is read-only" );
			}
		} // CheckModification

		// ----------------------------------------------------------------------
		// members
		private readonly bool isReadOnly;
		private DateTime start;
		private DateTime end;

	} // class TimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
