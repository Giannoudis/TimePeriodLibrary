// -- FILE ------------------------------------------------------------------
// name       : TimePeriodChain.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimePeriodChain : ITimePeriodChain
	{

		// ----------------------------------------------------------------------
		public TimePeriodChain()
		{
		} // TimePeriodChain

		// ----------------------------------------------------------------------
		public TimePeriodChain( IEnumerable<ITimePeriod> timePeriods )
		{
			if ( timePeriods == null )
			{
				throw new ArgumentNullException( "timePeriods" );
			}
			AddAll( timePeriods );
		} // TimePeriodChain

		// ----------------------------------------------------------------------
		public bool IsReadOnly
		{
			get { return false; }
		} // IsReadOnly

		// ----------------------------------------------------------------------
		public ITimePeriod First
		{
			get { return periods.Count > 0 ? periods[ 0 ] : null; }
		} // First

		// ----------------------------------------------------------------------
		public ITimePeriod Last
		{
			get { return periods.Count > 0 ? periods[ periods.Count - 1 ] : null; }
		} // Last

		// ----------------------------------------------------------------------
		public int Count
		{
			get { return periods.Count; }
		} // Count

		// ----------------------------------------------------------------------
		public ITimePeriod this[ int index ]
		{
			get { return periods[ index ]; }
			set
			{
				RemoveAt( index );
				Insert( index, value );
			}
		} // this[]

		// ----------------------------------------------------------------------
		public bool IsAnytime
		{
			get { return !HasStart && !HasEnd; }
		} // IsAnytime

		// ----------------------------------------------------------------------
		public bool IsMoment
		{
			get { return Count != 0 && First.Start.Equals( Last.End ); }
		} // IsMoment

		// ----------------------------------------------------------------------
		public bool HasStart
		{
			get { return Start != TimeSpec.MinPeriodDate; }
		} // HasStart

		// ----------------------------------------------------------------------
		public DateTime Start
		{
			get { return Count > 0 ? First.Start : TimeSpec.MinPeriodDate; }
			set
			{
				if ( Count == 0 )
				{
					return;
				}
				Move( value - Start );
			}
		} // Start

		// ----------------------------------------------------------------------
		public bool HasEnd
		{
			get { return End != TimeSpec.MaxPeriodDate; }
		} // HasEnd

		// ----------------------------------------------------------------------
		public DateTime End
		{
			get { return Count > 0 ? Last.End : TimeSpec.MaxPeriodDate; }
			set
			{
				if ( Count == 0 )
				{
					return;
				}
				Move( value - End );
			}
		} // End

		// ----------------------------------------------------------------------
		public TimeSpan Duration
		{
			get { return End - Start; }
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
			throw new InvalidOperationException();
		} // Setup

		// ----------------------------------------------------------------------
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		} // IEnumerable.GetEnumerator

		// ----------------------------------------------------------------------
		public IEnumerator<ITimePeriod> GetEnumerator()
		{
			return periods.GetEnumerator();
		} // GetEnumerator

		// ----------------------------------------------------------------------
		public virtual void Move( TimeSpan delta )
		{
			if ( delta == TimeSpan.Zero )
			{
				return;
			}

			foreach ( ITimePeriod timePeriod in periods )
			{
				DateTime start = timePeriod.Start + delta;
				timePeriod.Setup( start, start.Add( timePeriod.Duration ) );
			}
		} // Move

		// ----------------------------------------------------------------------
		public virtual void Add( ITimePeriod item )
		{
			if ( item == null )
			{
				throw new ArgumentNullException( "item" );
			}
			CheckReadOnlyItem( item );

			ITimePeriod last = Last;
			if ( last != null )
			{
				CheckSpaceAfter( last.End, item.Duration );
				item.Setup( last.End, last.End.Add( item.Duration ) );
			}

			periods.Add( item );
		} // Add

		// ----------------------------------------------------------------------
		public bool ContainsPeriod( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			foreach ( ITimePeriod period in periods )
			{
				if ( period.IsSamePeriod( test ) )
				{
					return true;
				}
			}
			return false;
		} // ContainsPeriod

		// ----------------------------------------------------------------------
		public void AddAll( IEnumerable<ITimePeriod> timePeriods )
		{
			if ( timePeriods == null )
			{
				throw new ArgumentNullException( "timePeriods" );
			}

			foreach ( ITimePeriod period in timePeriods )
			{
				Add( period );
			}
		} // AddAll

		// ----------------------------------------------------------------------
		public virtual void Insert( int index, ITimePeriod period )
		{
			if ( index < 0 || index > Count )
			{
				throw new ArgumentOutOfRangeException( "index" );
			}
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			CheckReadOnlyItem( period );

			TimeSpan itemDuration = period.Duration;

			ITimePeriod previous = null;
			ITimePeriod next = null;
			if ( Count > 0 )
			{
				if ( index > 0 )
				{
					previous = this[ index - 1 ];
					CheckSpaceAfter( End, itemDuration );
				}
				if ( index < Count - 1 )
				{
					next = this[ index ];
					CheckSpaceBefore( Start, itemDuration );
				}
			}

			periods.Insert( index, period );

			// adjust time periods after the inserted item
			if ( previous != null )
			{
				period.Setup( previous.End, previous.End + period.Duration );
				for ( int i = index + 1; i < Count; i++ )
				{
					DateTime previousStart = this[ i ].Start.Add( itemDuration );
					this[ i ].Setup( previousStart, previousStart.Add( this[ i ].Duration ) );
				}
			}

			// adjust time periods before the inserted item
			if ( next == null )
			{
				return;
			}
			DateTime nextStart = next.Start.Subtract( itemDuration );
			period.Setup( nextStart, nextStart.Add( period.Duration ) );
			for ( int i = 0; i < index - 1; i++ )
			{
				nextStart = this[ i ].Start.Subtract( itemDuration );
				this[ i ].Setup( nextStart, nextStart.Add( this[ i ].Duration ) );
			}
		} // Insert

		// ----------------------------------------------------------------------
		public virtual bool Contains( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			return periods.Contains( period );
		} // Contains

		// ----------------------------------------------------------------------
		public virtual int IndexOf( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			return periods.IndexOf( period );
		} // IndexOf

		// ----------------------------------------------------------------------
		public virtual void CopyTo( ITimePeriod[] array, int arrayIndex )
		{
			if ( array == null )
			{
				throw new ArgumentNullException( "array" );
			}
			periods.CopyTo( array, arrayIndex );
		} // CopyTo

		// ----------------------------------------------------------------------
		public virtual void Clear()
		{
			periods.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public virtual bool Remove( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}

			TimeSpan itemDuration = period.Duration;
			int index = IndexOf( period );

			ITimePeriod next = null;
			if ( itemDuration > TimeSpan.Zero && Count > 1 && index > 0 && index < Count - 1 ) // between
			{
				next = this[ index ];
			}

			bool removed = periods.Remove( period );
			if ( removed && next != null ) // fill the gap
			{
				for ( int i = index; i < Count; i++ )
				{
					DateTime start = this[ i ].Start.Subtract( itemDuration );
					this[ i ].Setup( start, start.Add( this[ i ].Duration ) );
				}
			}

			return removed;
		} // Remove

		// ----------------------------------------------------------------------
		public virtual void RemoveAt( int index )
		{
			Remove( this[ index ] );
		} // RemoveAt

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
		public virtual bool IntersectsWith( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.IntersectsWith( this, test );
		} // IntersectsWith

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
		public string GetDescription( ITimeFormatter formatter = null )
		{
			return Format( formatter ?? TimeFormatter.Instance );
		} // GetDescription

		// ----------------------------------------------------------------------
		protected virtual string Format( ITimeFormatter formatter )
		{
			return formatter.GetCollectionPeriod( Count, Start, End, Duration );
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
			return HasSameData( obj as TimePeriodChain );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( IList<ITimePeriod> comp )
		{
			if ( Count != comp.Count )
			{
				return false;
			}

			for ( int i = 0; i < Count; i++ )
			{
				if ( !this[ i ].Equals( comp[ i ] ) )
				{
					return false;
				}
			}

			return true;
		} // HasSameData

		// ----------------------------------------------------------------------
		public sealed override int GetHashCode()
		{
			return HashTool.AddHashCode( GetType().GetHashCode(), ComputeHashCode() );
		} // GetHashCode

		// ----------------------------------------------------------------------
		protected virtual int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( periods );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		protected void CheckSpaceBefore( DateTime moment, TimeSpan duration )
		{
			bool hasSpace = moment != TimeSpec.MinPeriodDate;
			if ( hasSpace )
			{
				TimeSpan remaining = moment - TimeSpec.MinPeriodDate;
				hasSpace = duration <= remaining;
			}

			if ( !hasSpace )
			{
				throw new InvalidOperationException( "duration " + duration + " out of range " );
			}
		} // CheckSpaceBefore

		// ----------------------------------------------------------------------
		protected void CheckSpaceAfter( DateTime moment, TimeSpan duration )
		{
			bool hasSpace = moment != TimeSpec.MaxPeriodDate;
			if ( hasSpace )
			{
				TimeSpan remaining = TimeSpec.MaxPeriodDate - moment;
				hasSpace = duration <= remaining;
			}

			if ( !hasSpace )
			{
				throw new InvalidOperationException( "duration " + duration + " out of range" );
			}
		} // CheckSpaceAfter

		// ----------------------------------------------------------------------
		protected void CheckReadOnlyItem( ITimePeriod timePeriod )
		{
			if ( timePeriod.IsReadOnly )
			{
				throw new NotSupportedException( "TimePeriod is read-only" );
			}
		} // CheckReadOnlyItem

		// ----------------------------------------------------------------------
		// members
		private readonly List<ITimePeriod> periods = new List<ITimePeriod>();

	} // class TimePeriodChain

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
