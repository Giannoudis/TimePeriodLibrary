// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCollection.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
#if (!PCL)
using System.ComponentModel;
#endif

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimePeriodCollection : ITimePeriodCollection
	{

		// ----------------------------------------------------------------------
		public TimePeriodCollection()
		{
		} // TimePeriodCollection

		// ----------------------------------------------------------------------
		public TimePeriodCollection( IEnumerable<ITimePeriod> timePeriods ) :
			this()
		{
			if ( timePeriods == null )
			{
				throw new ArgumentNullException( "timePeriods" );
			}
			AddAll( timePeriods );
		} // TimePeriodCollection

		// ----------------------------------------------------------------------
		public bool IsReadOnly
		{
			get { return false; }
		} // IsReadOnly

		// ----------------------------------------------------------------------
		public int Count
		{
			get { return periods.Count; }
		} // Count

		// ----------------------------------------------------------------------
		public ITimePeriod this[ int index ]
		{
			get { return periods[ index ]; }
			set { periods[ index ] = value; }
		} // this[]

		// ----------------------------------------------------------------------
		public bool IsAnytime
		{
			get { return !HasStart && !HasEnd; }
		} // IsAnytime

		// ----------------------------------------------------------------------
		public bool IsMoment
		{
			get { return Duration == TimeSpan.Zero; }
		} // IsMoment

		// ----------------------------------------------------------------------
		public bool HasStart
		{
			get { return Start != TimeSpec.MinPeriodDate; }
		} // HasStart

		// ----------------------------------------------------------------------
		public DateTime Start
		{
			get
			{
				DateTime? start = GetStart();
				return start.HasValue ? start.Value : TimeSpec.MinPeriodDate;
			}
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
			get
			{
				DateTime? end = GetEnd();
				return end.HasValue ? end.Value : TimeSpec.MaxPeriodDate;
			}
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
			get
			{
				TimeSpan? duration = GetDuration();
				return duration.HasValue ? duration.Value : TimeSpec.MaxPeriodDuration;
			}
		} // Duration

		// ----------------------------------------------------------------------
		public virtual TimeSpan TotalDuration
		{
			get
			{
				TimeSpan duration = TimeSpan.Zero;
				foreach ( ITimePeriod timePeriod in periods )
				{
					duration = duration.Add( timePeriod.Duration );
				}
				return duration;
			}
		} // GetTotalDuration

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
		public virtual TimeSpan GetTotalDuration( IDurationProvider provider )
		{
			if ( provider == null )
			{
				throw new ArgumentNullException( "provider" );
			}

			TimeSpan duration = TimeSpan.Zero;
			foreach ( ITimePeriod timePeriod in periods )
			{
				duration = duration.Add( timePeriod.GetDuration( provider ) );
			}
			return duration;
		} // GetTotalDuration

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
		public virtual void SortBy( ITimePeriodComparer comparer )
		{
			if ( comparer == null )
			{
				throw new ArgumentNullException( "comparer" );
			}
			periods.Sort( comparer );
		} // SortBy

		// ----------------------------------------------------------------------
		public virtual void SortReverseBy( ITimePeriodComparer comparer )
		{
			if ( comparer == null )
			{
				throw new ArgumentNullException( "comparer" );
			}
			SortBy( new TimePeriodReversComparer( comparer ) );
		} // SortReverseBy

		// ----------------------------------------------------------------------
		public virtual void SortByStart( ListSortDirection sortDirection = ListSortDirection.Ascending )
		{
			switch ( sortDirection )
			{
				case ListSortDirection.Ascending:
					SortBy( TimePeriodStartComparer.Comparer );
					break;
				case ListSortDirection.Descending:
					SortBy( TimePeriodStartComparer.ReverseComparer );
					break;
				default:
					throw new ArgumentOutOfRangeException( "sortDirection" );
			}
		} // SortByStart

		// ----------------------------------------------------------------------
		public virtual void SortByEnd( ListSortDirection sortDirection = ListSortDirection.Ascending )
		{
			switch ( sortDirection )
			{
				case ListSortDirection.Ascending:
					SortBy( TimePeriodEndComparer.Comparer );
					break;
				case ListSortDirection.Descending:
					SortBy( TimePeriodEndComparer.ReverseComparer );
					break;
				default:
					throw new ArgumentOutOfRangeException( "sortDirection" );
			}
		} // SortByEnd

		// ----------------------------------------------------------------------
		public virtual void SortByDuration( ListSortDirection sortDirection = ListSortDirection.Ascending )
		{
			switch ( sortDirection )
			{
				case ListSortDirection.Ascending:
					SortBy( TimePeriodDurationComparer.Comparer );
					break;
				case ListSortDirection.Descending:
					SortBy( TimePeriodDurationComparer.ReverseComparer );
					break;
				default:
					throw new ArgumentOutOfRangeException( "sortDirection" );
			}
		} // SortByDuration

		// ----------------------------------------------------------------------
		public virtual bool HasInsidePeriods( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			foreach ( ITimePeriod period in periods )
			{
				if ( test.HasInside( period ) )
				{
					return true;
				}
			}

			return false;
		} // HasInsidePeriods

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection InsidePeriods( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			TimePeriodCollection insidePeriods = new TimePeriodCollection();

			foreach ( ITimePeriod period in periods )
			{
				if ( test.HasInside( period ) )
				{
					insidePeriods.Add( period );
				}
			}

			return insidePeriods;
		} // InsidePeriods

		// ----------------------------------------------------------------------
		public virtual bool HasOverlaps()
		{
			bool hasOverlaps = false;
			if ( Count == 2 )
			{
				hasOverlaps = this[ 0 ].OverlapsWith( this[ 1 ] );
			}
			else if ( Count > 2 )
			{
				hasOverlaps = new TimeLineMomentCollection( this ).HasOverlaps();
			}

			return hasOverlaps;
		} // HasOverlaps

		// ----------------------------------------------------------------------
		public virtual bool HasGaps()
		{
			bool hasGaps = false;
			if ( Count > 1 )
			{
				hasGaps = new TimeLineMomentCollection( this ).HasGaps();
			}

			return hasGaps;
		} // HasGaps

		// ----------------------------------------------------------------------
		public virtual bool HasOverlapPeriods( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			foreach ( ITimePeriod period in periods )
			{
				if ( test.OverlapsWith( period ) )
				{
					return true;
				}
			}

			return false;
		} // HasOverlapPeriods

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection OverlapPeriods( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			TimePeriodCollection overlapPeriods = new TimePeriodCollection();

			foreach ( ITimePeriod period in periods )
			{
				if ( test.OverlapsWith( period ) )
				{
					overlapPeriods.Add( period );
				}
			}

			return overlapPeriods;
		} // OverlapPeriods

		// ----------------------------------------------------------------------
		public virtual bool HasIntersectionPeriods( DateTime test )
		{
			foreach ( ITimePeriod period in periods )
			{
				if ( period.HasInside( test ) )
				{
					return true;
				}
			}

			return false;
		} // HasIntersectionPeriods

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection IntersectionPeriods( DateTime test )
		{
			TimePeriodCollection intersectionPeriods = new TimePeriodCollection();

			foreach ( ITimePeriod period in periods )
			{
				if ( period.HasInside( test ) )
				{
					intersectionPeriods.Add( period );
				}
			}

			return intersectionPeriods;
		} // IntersectionPeriods

		// ----------------------------------------------------------------------
		public virtual bool HasIntersectionPeriods( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			foreach ( ITimePeriod period in periods )
			{
				if ( period.IntersectsWith( test ) )
				{
					return true;
				}
			}

			return false;
		} // HasIntersectionPeriods

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection IntersectionPeriods( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			TimePeriodCollection intersectionPeriods = new TimePeriodCollection();

			foreach ( ITimePeriod period in periods )
			{
				if ( test.IntersectsWith( period ) )
				{
					intersectionPeriods.Add( period );
				}
			}

			return intersectionPeriods;
		} // IntersectionPeriods

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection RelationPeriods( ITimePeriod test, PeriodRelation relation )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}

			TimePeriodCollection relationPeriods = new TimePeriodCollection();

			foreach ( ITimePeriod period in periods )
			{
				if ( test.GetRelation( period ) == relation )
				{
					relationPeriods.Add( period );
				}
			}

			return relationPeriods;
		} // RelationPeriods

		// ----------------------------------------------------------------------
		public virtual void Add( ITimePeriod item )
		{
			if ( item == null )
			{
				throw new ArgumentNullException( "item" );
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
		public virtual void Insert( int index, ITimePeriod item )
		{
			if ( index < 0 || index > Count )
			{
				throw new ArgumentOutOfRangeException( "index" );
			}
			if ( item == null )
			{
				throw new ArgumentNullException( "item" );
			}
			periods.Insert( index, item );
		} // Insert

		// ----------------------------------------------------------------------
		public virtual bool Contains( ITimePeriod item )
		{
			if ( item == null )
			{
				throw new ArgumentNullException( "item" );
			}
			return periods.Contains( item );
		} // Contains

		// ----------------------------------------------------------------------
		public virtual int IndexOf( ITimePeriod item )
		{
			if ( item == null )
			{
				throw new ArgumentNullException( "item" );
			}
			return periods.IndexOf( item );
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
		public virtual bool Remove( ITimePeriod item )
		{
			if ( item == null )
			{
				throw new ArgumentNullException( "item" );
			}
			return periods.Remove( item );
		} // Remove

		// ----------------------------------------------------------------------
		public virtual void RemoveAt( int index )
		{
			periods.RemoveAt( index );
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
			return HasSameData( obj as TimePeriodCollection );
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
		protected virtual DateTime? GetStart()
		{
			if ( Count == 0 )
			{
				return null;
			}

			DateTime start = TimeSpec.MaxPeriodDate;

			foreach ( ITimePeriod timePeriod in periods )
			{
				if ( timePeriod.Start < start )
				{
					start = timePeriod.Start;
				}
			}

			return start;
		} // GetStart

		// ----------------------------------------------------------------------
		protected virtual DateTime? GetEnd()
		{
			if ( Count == 0 )
			{
				return null;
			}

			DateTime end = TimeSpec.MinPeriodDate;

			foreach ( ITimePeriod timePeriod in periods )
			{
				if ( timePeriod.End > end )
				{
					end = timePeriod.End;
				}
			}

			return end;
		} // GetEnd

		// ----------------------------------------------------------------------
		protected virtual void GetStartEnd( out DateTime? start, out DateTime? end )
		{
			if ( Count == 0 )
			{
				start = null;
				end = null;
				return;
			}

			start = TimeSpec.MaxPeriodDate;
			end = TimeSpec.MinPeriodDate;

			foreach ( ITimePeriod timePeriod in periods )
			{
				if ( timePeriod.Start < start )
				{
					start = timePeriod.Start;
				}
				if ( timePeriod.End > end )
				{
					end = timePeriod.End;
				}
			}
		} // GetStartEnd

		// ----------------------------------------------------------------------
		protected virtual TimeSpan? GetDuration()
		{
			DateTime? start;
			DateTime? end;

			GetStartEnd( out start, out end );

			if ( start == null || end == null )
			{
				return null;
			}

			return end.Value - start.Value;
		} // GetDuration

		// ----------------------------------------------------------------------
		// members
		private readonly List<ITimePeriod> periods = new List<ITimePeriod>();

	} // class TimePeriodCollection

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
