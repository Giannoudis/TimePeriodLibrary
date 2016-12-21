// -- FILE ------------------------------------------------------------------
// name       : TimeInterval.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.05.06
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimeInterval : ITimeInterval
	{

		// ----------------------------------------------------------------------
		public static readonly TimeInterval Anytime = new TimeInterval(
			TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, IntervalEdge.Closed, IntervalEdge.Closed, false, true );

		// ----------------------------------------------------------------------
		public TimeInterval() :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate )
		{
		} // TimeInterval

		// ----------------------------------------------------------------------
		public TimeInterval( DateTime moment,
			IntervalEdge startEdge = IntervalEdge.Closed, IntervalEdge endEdge = IntervalEdge.Closed,
			bool isIntervalEnabled = true, bool isReadOnly = false ) :
			this( moment, moment, startEdge, endEdge, isIntervalEnabled, isReadOnly )
		{
		} // TimeInterval

		// ----------------------------------------------------------------------
		public TimeInterval( DateTime startInterval, DateTime endInterval,
			IntervalEdge startEdge = IntervalEdge.Closed, IntervalEdge endEdge = IntervalEdge.Closed, 
			bool isIntervalEnabled = true, bool isReadOnly = false )
		{
			if ( startInterval <= endInterval )
			{
				this.startInterval = startInterval;
				this.endInterval = endInterval;
			}
			else
			{
				this.endInterval = startInterval;
				this.startInterval = endInterval;
			}

			this.startEdge = startEdge;
			this.endEdge = endEdge;

			this.isIntervalEnabled = isIntervalEnabled;
			this.isReadOnly = isReadOnly;
		} // TimeInterval

		// ----------------------------------------------------------------------
		public TimeInterval( ITimePeriod copy )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			ITimeInterval timeInterval = copy as ITimeInterval;
			if ( timeInterval != null )
			{
				startInterval = timeInterval.StartInterval;
				endInterval = timeInterval.EndInterval;
				startEdge = timeInterval.StartEdge;
				endEdge = timeInterval.EndEdge;
				isIntervalEnabled = timeInterval.IsIntervalEnabled;
			}
			else
			{
				startInterval = copy.Start;
				endInterval = copy.End;
			}

			isReadOnly = copy.IsReadOnly;
		} // TimeInterval

		// ----------------------------------------------------------------------
		protected TimeInterval( ITimePeriod copy, bool isReadOnly )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			ITimeInterval timeInterval = copy as ITimeInterval;
			if ( timeInterval != null )
			{
				startInterval = timeInterval.StartInterval;
				endInterval = timeInterval.EndInterval;
				startEdge = timeInterval.StartEdge;
				endEdge = timeInterval.EndEdge;
				isIntervalEnabled = timeInterval.IsIntervalEnabled;
			}
			else
			{
				startInterval = copy.Start;
				endInterval = copy.End;
			}
			this.isReadOnly = isReadOnly;
		} // TimeInterval

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
			get { return startInterval.Equals( endInterval ); }
		} // IsMoment

		// ----------------------------------------------------------------------
		public bool IsStartOpen
		{
			get { return startEdge == IntervalEdge.Open; }
		} // IsOpen

		// ----------------------------------------------------------------------
		public bool IsEndOpen
		{
			get { return endEdge == IntervalEdge.Open; }
		} // IsStartOpen

		// ----------------------------------------------------------------------
		public bool IsOpen
		{
			get { return IsStartOpen && IsEndOpen; }
		} // IsOpen

		// ----------------------------------------------------------------------
		public bool IsStartClosed
		{
			get { return startEdge == IntervalEdge.Closed; }
		} // IsStartClosed

	// ----------------------------------------------------------------------
		public bool IsEndClosed
		{
			get { return endEdge == IntervalEdge.Closed; }
		} // IsEndClosed

		// ----------------------------------------------------------------------
		public bool IsClosed
		{
			get { return IsStartClosed && IsEndClosed; }
		} // IsClosed

		// ----------------------------------------------------------------------
		public bool IsEmpty
		{
			get { return IsMoment && !IsClosed; }
		} // IsMoment

		// ----------------------------------------------------------------------
		public bool IsDegenerate
		{
			get { return IsMoment && IsClosed; }
		} // IsDegenerate

		// ----------------------------------------------------------------------
		public bool IsIntervalEnabled
		{
			get { return isIntervalEnabled; }
			set
			{
				CheckModification();
				isIntervalEnabled = value;
			}
		} // IsIntervalEnabled

		// ----------------------------------------------------------------------
		public bool HasStart
		{
			get { return !( startInterval == TimeSpec.MinPeriodDate && startEdge == IntervalEdge.Closed ); }
		} // HasStart

		// ----------------------------------------------------------------------
		public DateTime StartInterval
		{
			get { return startInterval; }
			set
			{
				CheckModification();
				if ( value > endInterval )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				startInterval = value;
			}
		} // StartInterval

		// ----------------------------------------------------------------------
		public DateTime Start
		{
			get
			{
				if ( isIntervalEnabled && startEdge == IntervalEdge.Open )
				{
					return startInterval.AddTicks( 1 );
				}
				return startInterval;
			}
		} // Start

		// ----------------------------------------------------------------------
		public IntervalEdge StartEdge
		{
			get { return startEdge; }
			set
			{
				CheckModification();
				startEdge = value;
			}
		} // StartEdge

		// ----------------------------------------------------------------------
		public bool HasEnd
		{
			get { return !( endInterval == TimeSpec.MaxPeriodDate && endEdge == IntervalEdge.Closed ); }
		} // HasEnd

		// ----------------------------------------------------------------------
		public DateTime EndInterval
		{
			get { return endInterval; }
			set
			{
				CheckModification();
				if ( value < startInterval )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				endInterval = value;
			}
		} // EndInterval

		// ----------------------------------------------------------------------
		public DateTime End
		{
			get
			{
				if ( isIntervalEnabled && endEdge == IntervalEdge.Open )
				{
					return endInterval.AddTicks( -1 );
				}
				return endInterval;
			}
		} // End

		// ----------------------------------------------------------------------
		public IntervalEdge EndEdge
		{
			get { return endEdge; }
			set
			{
				CheckModification();
				endEdge = value;
			}
		} // EndEdge

		// ----------------------------------------------------------------------
		public TimeSpan Duration
		{
			get { return endInterval.Subtract( startInterval ); }
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
		public virtual void Setup( DateTime newStartInterval, DateTime newEndInterval )
		{
			CheckModification();
			if ( newStartInterval <= newEndInterval )
			{
				startInterval = newStartInterval;
				endInterval = newEndInterval;
			}
			else
			{
				endInterval = newStartInterval;
				startInterval = newEndInterval;
			}
		} // Setup

		// ----------------------------------------------------------------------
		public virtual bool IsSamePeriod( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return Start == test.Start && End == test.End;
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
		public ITimeInterval Copy()
		{
			return Copy( TimeSpan.Zero );
		} // Copy

		// ----------------------------------------------------------------------
		public virtual ITimeInterval Copy( TimeSpan offset )
		{
			return new TimeInterval( 
				startInterval.Add( offset ), 
				endInterval.Add( offset ),
				startEdge, 
				endEdge,
				IsIntervalEnabled,
				IsReadOnly );
		} // Copy

		// ----------------------------------------------------------------------
		public virtual void Move( TimeSpan offset )
		{
			CheckModification();
			if ( offset == TimeSpan.Zero )
			{
				return;
			}
			startInterval = startInterval.Add( offset );
			endInterval = endInterval.Add( offset );
		} // Move

		// ----------------------------------------------------------------------
		public virtual void ExpandStartTo( DateTime moment )
		{
			CheckModification();
			if ( startInterval > moment )
			{
				startInterval = moment;
			}
		} // ExpandStartTo

		// ----------------------------------------------------------------------
		public virtual void ExpandEndTo( DateTime moment )
		{
			CheckModification();
			if ( endInterval < moment )
			{
				endInterval = moment;
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
			ITimeInterval timeInterval = period as ITimeInterval;
			if ( timeInterval != null )
			{
				ExpandStartTo( timeInterval.StartInterval );
				ExpandEndTo( timeInterval.EndInterval );
			}
			else
			{
				ExpandStartTo( period.Start );
				ExpandEndTo( period.End );
			}
		} // ExpandTo

		// ----------------------------------------------------------------------
		public virtual void ShrinkStartTo( DateTime moment )
		{
			CheckModification();
			if ( HasInside( moment ) && startInterval < moment )
			{
				startInterval = moment;
			}
		} // ShrinkStartTo

		// ----------------------------------------------------------------------
		public virtual void ShrinkEndTo( DateTime moment )
		{
			CheckModification();
			if ( HasInside( moment ) && endInterval > moment )
			{
				endInterval = moment;
			}
		} // ShrinkEndTo

		// ----------------------------------------------------------------------
		public void ShrinkTo( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			ITimeInterval timeInterval = period as ITimeInterval;
			if ( timeInterval != null )
			{
				ShrinkStartTo( timeInterval.StartInterval );
				ShrinkEndTo( timeInterval.EndInterval );
			}
			else
			{
				ShrinkStartTo( period.Start );
				ShrinkEndTo( period.End );
			}
			ShrinkStartTo( period.Start );
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
		public virtual ITimeInterval GetIntersection( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			if ( !IntersectsWith( period ) )
			{
				return null;
			}
			DateTime start = Start;
			DateTime end = End;
			DateTime periodStart = period.Start;
			DateTime periodEnd = period.End;
			return new TimeInterval(
				periodStart > start ? periodStart : start, 
				periodEnd < end ? periodEnd : end, 
				IntervalEdge.Closed,
				IntervalEdge.Closed,
				IsIntervalEnabled,
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
			isIntervalEnabled = true;
			startInterval = TimeSpec.MinPeriodDate;
			endInterval = TimeSpec.MaxPeriodDate;
			startEdge = IntervalEdge.Closed;
			endEdge = IntervalEdge.Closed;
		} // Reset

		// ----------------------------------------------------------------------
		public string GetDescription( ITimeFormatter formatter = null )
		{
			return Format( formatter ?? TimeFormatter.Instance );
		} // GetDescription

		// ----------------------------------------------------------------------
		protected virtual string Format( ITimeFormatter formatter )
		{
			return formatter.GetInterval( startInterval, endInterval, startEdge, endEdge, Duration );
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
			return HasSameData( obj as TimeInterval );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( TimeInterval comp )
		{
			return
				isReadOnly == comp.isReadOnly &&
				isIntervalEnabled == comp.isIntervalEnabled &&
				startInterval == comp.startInterval &&
				endInterval == comp.endInterval &&
				startEdge == comp.startEdge &&
				endEdge == comp.endEdge;
		} // HasSameData

		// ----------------------------------------------------------------------
		public sealed override int GetHashCode()
		{
			return HashTool.AddHashCode( GetType().GetHashCode(), ComputeHashCode() );
		} // GetHashCode

		// ----------------------------------------------------------------------
		protected virtual int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( 
				isReadOnly,
				isIntervalEnabled,
				startInterval, 
				endInterval, 
				startEdge, 
				endEdge );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		protected void CheckModification()
		{
			if ( IsReadOnly )
			{
				throw new NotSupportedException( "TimeInterval is read-only" );
			}
		} // CheckModification

		// ----------------------------------------------------------------------
		// members
		private readonly bool isReadOnly;
		private bool isIntervalEnabled = true;
		private DateTime startInterval;
		private DateTime endInterval;
		private IntervalEdge startEdge;
		private IntervalEdge endEdge;

	} // class TimeInterval

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
