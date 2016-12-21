// -- FILE ------------------------------------------------------------------
// name       : TimeLineMomentCollection.Desktop.cs
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
			get { return !IsEmpty ? this[ 0 ] : null; }
		} // Min

		// ----------------------------------------------------------------------
		public ITimeLineMoment Max
		{
			get { return !IsEmpty ? this[ Count - 1 ] : null; }
		} // Max

		// ----------------------------------------------------------------------
		public ITimeLineMoment this[ int index ]
		{
			get { return timeLineMoments.Values[ index ]; }
		} // this[]

		// ----------------------------------------------------------------------
		public ITimeLineMoment this[ DateTime moment ]
		{
			get { return timeLineMoments[ moment ]; }
		} // this[]

		// ----------------------------------------------------------------------
		protected IList<ITimeLineMoment> Moments
		{
			get { return timeLineMoments.Values; }
		} // Moments

		// ----------------------------------------------------------------------
		public void Clear()
		{
			timeLineMoments.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public void Add( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			AddStart( period.Start, period );
			AddEnd( period.End, period );
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
				AddStart( period.Start, period );
				AddEnd( period.End, period );
			}
		} // AddAll

		// ----------------------------------------------------------------------
		public void Remove( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}

			RemoveStart( period.Start, period );
			RemoveEnd( period.End, period );
		} // Remove

		// ----------------------------------------------------------------------
		public ITimeLineMoment Find( DateTime moment )
		{
			ITimeLineMoment timeLineMoment;
			timeLineMoments.TryGetValue( moment, out timeLineMoment );
			return timeLineMoment;
		} // Find

		// ----------------------------------------------------------------------
		public bool Contains( DateTime moment )
		{
			return Find( moment ) != null;
		} // Contains

		// ----------------------------------------------------------------------
		public bool HasOverlaps()
		{
			bool hasOverlap = false;
			if ( Count > 1 )
			{
				bool start = true;
				foreach ( ITimeLineMoment timeLineMoment in timeLineMoments.Values )
				{
					if ( start )
					{
						if ( timeLineMoment.StartCount != 1 || timeLineMoment.EndCount > 1 )
						{
							hasOverlap = true;
							break;
						}
					}
					else // end
					{
						if ( timeLineMoment.StartCount > 1 || timeLineMoment.EndCount != 1 )
						{
							hasOverlap = true;
							break;
						}
					}
					start = ( timeLineMoment.EndCount - timeLineMoment.StartCount ) > 0;
				}
			}

			return hasOverlap;
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
					ITimeLineMoment timeLineMoment = this[ index ];
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
			return timeLineMoments.Values.GetEnumerator();
		} // GetEnumerator

		// ----------------------------------------------------------------------
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		} // IEnumerable.GetEnumerator

		// ----------------------------------------------------------------------
		protected virtual void AddStart( DateTime moment, ITimePeriod period )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				timeLineMoment = new TimeLineMoment( moment );
				timeLineMoments.Add( moment, timeLineMoment );
			}
			timeLineMoment.AddStart();
		} // AddStart

		// ----------------------------------------------------------------------
		protected virtual void AddEnd( DateTime moment, ITimePeriod period )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				timeLineMoment = new TimeLineMoment( moment );
				timeLineMoments.Add( moment, timeLineMoment );
			}
			timeLineMoment.AddEnd();
		} // AddEnd

		// ----------------------------------------------------------------------
		protected virtual void RemoveStart( DateTime moment, ITimePeriod period )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				throw new InvalidOperationException();
			}

			timeLineMoment.RemoveStart();
			if ( timeLineMoment.IsEmpty)
			{
				timeLineMoments.Remove( moment );
			}
		} // RemoveStart

		// ----------------------------------------------------------------------
		protected virtual void RemoveEnd( DateTime moment, ITimePeriod period )
		{
			ITimeLineMoment timeLineMoment = Find( moment );
			if ( timeLineMoment == null )
			{
				throw new InvalidOperationException();
			}

			timeLineMoment.RemoveEnd();
			if ( timeLineMoment.IsEmpty )
			{
				timeLineMoments.Remove( moment );
			}
		} // RemoveEnd

		// ----------------------------------------------------------------------
		// members
		private readonly SortedList<DateTime, ITimeLineMoment> timeLineMoments = new SortedList<DateTime, ITimeLineMoment>();

	} // class TimeLineMomentCollection

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
