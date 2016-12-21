// -- FILE ------------------------------------------------------------------
// name       : TimeLineMomentCollection.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.31
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
	public class TimeLineMomentCollection : ITimeLineMomentCollection
	{

		// ----------------------------------------------------------------------
		public TimeLineMomentCollection()
		{
		} // TimeLineMomentCollection

		// ----------------------------------------------------------------------
		public TimeLineMomentCollection( IEnumerable<ITimePeriod> periods )
		{
			AddAll( periods );
		} // TimeLineMomentCollection

		// ----------------------------------------------------------------------
		public int Count
		{
			get { return timeLineMoments.Count; }
		} // Count

		// ----------------------------------------------------------------------
		public bool IsEmpty
		{
			get { return Count == 0; }
		} // IsEmpty

		// ----------------------------------------------------------------------
		public ITimeLineMoment Min
		{
			get { return !IsEmpty ? timeLineMoments[ 0 ] : null; }
		} // Min

		// ----------------------------------------------------------------------
		public ITimeLineMoment Max
		{
			get { return !IsEmpty ? timeLineMoments[ Count - 1 ] : null; }
		} // Max

		// ----------------------------------------------------------------------
		public ITimeLineMoment this[ int index ]
		{
			get { return timeLineMoments[ index ]; }
		} // this[]

		// ----------------------------------------------------------------------
		public ITimeLineMoment this[ DateTime moment ]
		{
			get { return timeLineMomentLookup[ moment ]; }
		} // this[]

		// ----------------------------------------------------------------------
		protected IList<ITimeLineMoment> Moments
		{
			get { return timeLineMoments; }
		} // Moments

		// ----------------------------------------------------------------------
		public void Clear()
		{
			timeLineMoments.Clear();
			timeLineMomentLookup.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public void Add( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			AddStart( period.Start );
			AddEnd( period.End );
			Sort();
		} // Add

		// ----------------------------------------------------------------------
		public void AddAll( IEnumerable<ITimePeriod> periods )
		{
			if ( periods == null )
			{
				throw new ArgumentNullException( "periods" );
			}

			foreach ( ITimePeriod period in periods )
			{
				AddStart( period.Start );
				AddEnd( period.End );
			}
			Sort();
		} // AddAll

		// ----------------------------------------------------------------------
		public void Remove( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}

			RemoveStart( period.Start );
			RemoveEnd( period.End );
			Sort();
		} // Remove

		// ----------------------------------------------------------------------
		public ITimeLineMoment Find( DateTime moment )
		{
			ITimeLineMoment timeLineMoment = null;
			if ( Count > 0 )
			{
				timeLineMomentLookup.TryGetValue( moment, out timeLineMoment );
			}
			return timeLineMoment;
		} // Find

		// ----------------------------------------------------------------------
		public bool Contains( DateTime moment )
		{
			return timeLineMomentLookup.ContainsKey( moment );
		} // Contains

		// ----------------------------------------------------------------------
		public bool HasOverlaps()
		{
			bool hasOverlaps = false;

			if ( Count > 1 )
			{
				bool start = true;
				foreach ( ITimeLineMoment timeLineMoment in timeLineMoments )
				{
					int startCount = timeLineMoment.StartCount;
					int endCount = timeLineMoment.EndCount;
					if ( start )
					{
						if ( startCount != 1 || endCount > 1 )
						{
							hasOverlaps = true;
							break;
						}
					}
					else // end
					{
						if ( startCount > 1 || endCount != 1 )
						{
							hasOverlaps = true;
							break;
						}
					}
					start = ( endCount - startCount ) > 0;
				}
			}

			return hasOverlaps;
		} // HasOverlaps

		// ----------------------------------------------------------------------
		public bool HasGaps()
		{
			bool hasGaps = false;

			if ( Count > 1 )
			{
				int momentCount = 0;
				for ( int index = 0; index < timeLineMoments.Count; index++ )
				{
					ITimeLineMoment timeLineMoment = timeLineMoments[ index ];
					momentCount += timeLineMoment.StartCount;
					momentCount -= timeLineMoment.EndCount;
					if ( momentCount == 0 && index > 0 && index < timeLineMoments.Count - 1 )
					{
						hasGaps = true;
						break;
					}
				}
			}

			return hasGaps;
		} // HasGaps

		// ----------------------------------------------------------------------
		public IEnumerator<ITimeLineMoment> GetEnumerator()
		{
			return timeLineMoments.GetEnumerator();
		} // GetEnumerator

		// ----------------------------------------------------------------------
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		} // IEnumerable.GetEnumerator

		// ----------------------------------------------------------------------
		protected virtual void AddStart( DateTime moment )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				timeLineMoment = new TimeLineMoment( moment );
				timeLineMoments.Add( timeLineMoment );
				timeLineMomentLookup.Add( moment, timeLineMoment );
			}
			timeLineMoment.AddStart();
		} // AddStart

		// ----------------------------------------------------------------------
		protected virtual void AddEnd( DateTime moment )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				timeLineMoment = new TimeLineMoment( moment );
				timeLineMoments.Add( timeLineMoment );
				timeLineMomentLookup.Add( moment, timeLineMoment );
			}
			timeLineMoment.AddEnd();
		} // AddEnd

		// ----------------------------------------------------------------------
		protected virtual void RemoveStart( DateTime moment )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				throw new InvalidOperationException();
			}

			timeLineMoment.RemoveStart();
			if ( timeLineMoment.IsEmpty )
			{
				timeLineMoments.Remove( timeLineMoment );
				timeLineMomentLookup.Remove( moment );
			}
		} // RemoveStart

		// ----------------------------------------------------------------------
		protected virtual void RemoveEnd( DateTime moment )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				throw new InvalidOperationException();
			}

			timeLineMoment.RemoveEnd();
			if ( timeLineMoment.IsEmpty )
			{
				timeLineMoments.Remove( timeLineMoment );
				timeLineMomentLookup.Remove( moment );
			}
		} // RemoveEnd

		// ----------------------------------------------------------------------
		protected virtual void Sort()
		{
			timeLineMoments.Sort( ( left, right ) => left.Moment.CompareTo( right.Moment ) );
		} // Sort

		// ----------------------------------------------------------------------
		// members
		private readonly List<ITimeLineMoment> timeLineMoments = new List<ITimeLineMoment>();
		private readonly Dictionary<DateTime, ITimeLineMoment> timeLineMomentLookup = new Dictionary<DateTime, ITimeLineMoment>();

	} // class TimeLineMomentCollection

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
