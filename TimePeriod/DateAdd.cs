// -- FILE ------------------------------------------------------------------
// name       : DateAdd.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.19
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class DateAdd
	{

		// ----------------------------------------------------------------------
		public ITimePeriodCollection IncludePeriods
		{
			get { return includePeriods; }
		} // IncludePeriods

		// ----------------------------------------------------------------------
		public ITimePeriodCollection ExcludePeriods
		{
			get { return excludePeriods; }
		} // ExcludePeriods

		// ----------------------------------------------------------------------
		public virtual DateTime? Subtract( DateTime start, TimeSpan offset, SeekBoundaryMode seekBoundaryMode = SeekBoundaryMode.Next )
		{
			if ( includePeriods.Count == 0 && excludePeriods.Count == 0 )
			{
				return start.Subtract( offset );
			}

			TimeSpan? remaining;
			return offset < TimeSpan.Zero ?
				CalculateEnd( start, offset.Negate(), SeekDirection.Forward, seekBoundaryMode, out remaining ) :
				CalculateEnd( start, offset, SeekDirection.Backward, seekBoundaryMode, out remaining );
		} // Subtract

		// ----------------------------------------------------------------------
		public virtual DateTime? Add( DateTime start, TimeSpan offset, SeekBoundaryMode seekBoundaryMode = SeekBoundaryMode.Next )
		{
			if ( includePeriods.Count == 0 && excludePeriods.Count == 0 )
			{
				return start.Add( offset );
			}

			TimeSpan? remaining;
			return offset < TimeSpan.Zero ?
				CalculateEnd( start, offset.Negate(), SeekDirection.Backward, seekBoundaryMode, out remaining ) :
				CalculateEnd( start, offset, SeekDirection.Forward, seekBoundaryMode, out remaining );
		} // Add

		// ----------------------------------------------------------------------
		protected DateTime? CalculateEnd( DateTime start, TimeSpan offset,
			SeekDirection seekDirection, SeekBoundaryMode seekBoundaryMode, out TimeSpan? remaining )
		{
			if ( offset < TimeSpan.Zero )
			{
				throw new InvalidOperationException( "time span must be positive" );
			}

			remaining = offset;

			// search periods
			TimePeriodCollection searchPeriods = new TimePeriodCollection( includePeriods );

			// no search periods specified: search anytime
			if ( searchPeriods.Count == 0 )
			{
				searchPeriods.Add( TimeRange.Anytime );
			}

			// available periods
			ITimePeriodCollection availablePeriods = new TimePeriodCollection();

			// no exclude periods specified: use all search periods
			if ( excludePeriods.Count == 0 )
			{
				availablePeriods.AddAll( searchPeriods );
			}
			else // remove exclude periods
			{
				TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();
				foreach ( ITimePeriod searchPeriod in searchPeriods )
				{

					// no overlaps: use the entire search range
					if ( !excludePeriods.HasOverlapPeriods( searchPeriod ) )
					{
						availablePeriods.Add( searchPeriod );
					}
					else // add gaps of search period using the exclude periods
					{
						availablePeriods.AddAll( gapCalculator.GetGaps( excludePeriods, searchPeriod ) );
					}
				}
			}

			// no periods available
			if ( availablePeriods.Count == 0 )
			{
				return null;
			}

			// combine the available periods, ensure no overlapping
			// used for FindNextPeriod/FindPreviousPeriod
			if ( availablePeriods.Count > 1 )
			{
				TimePeriodCombiner<TimeRange> periodCombiner = new TimePeriodCombiner<TimeRange>();
				availablePeriods = periodCombiner.CombinePeriods( availablePeriods );
			}

			// find the starting search period
			ITimePeriod startPeriod = null;
			DateTime seekMoment = start;
			switch ( seekDirection )
			{
				case SeekDirection.Forward:
					startPeriod = FindNextPeriod( start, availablePeriods, out seekMoment );
					break;
				case SeekDirection.Backward:
					startPeriod = FindPreviousPeriod( start, availablePeriods, out seekMoment );
					break;
			}

			// no starting period available
			if ( startPeriod == null )
			{
				return null;
			}

			// no offset: use the search staring position 
			// maybe moved to the next available search period
			if ( offset == TimeSpan.Zero )
			{
				return seekMoment;
			}

			// setup destination search
			switch ( seekDirection )
			{
				case SeekDirection.Forward:
					for ( int i = availablePeriods.IndexOf( startPeriod ); i < availablePeriods.Count; i++ )
					{
						ITimePeriod gap = availablePeriods[ i ];
						TimeSpan gapRemining = gap.End - seekMoment;

						bool isTargetPeriod = false;
						switch ( seekBoundaryMode )
						{
							case SeekBoundaryMode.Fill:
								isTargetPeriod = gapRemining >= remaining;
								break;
							case SeekBoundaryMode.Next:
								isTargetPeriod = gapRemining > remaining;
								break;
						}

						if ( isTargetPeriod )
						{
							DateTime end = seekMoment + remaining.Value;
							remaining = null;
							return end;
						}

						remaining = remaining - gapRemining;
						if ( i == availablePeriods.Count - 1 )
						{
							return null;
						}
						seekMoment = availablePeriods[ i + 1 ].Start; // next period
					}
					break;
				case SeekDirection.Backward:
					for ( int i = availablePeriods.IndexOf( startPeriod ); i >= 0; i-- )
					{
						ITimePeriod gap = availablePeriods[ i ];
						TimeSpan gapRemining = seekMoment - gap.Start;

						bool isTargetPeriod = false;
						switch ( seekBoundaryMode )
						{
							case SeekBoundaryMode.Fill:
								isTargetPeriod = gapRemining >= remaining;
								break;
							case SeekBoundaryMode.Next:
								isTargetPeriod = gapRemining > remaining;
								break;
						}

						if ( isTargetPeriod )
						{
							DateTime end = seekMoment - remaining.Value;
							remaining = null;
							return end;
						}
						remaining = remaining - gapRemining;
						if ( i == 0 )
						{
							return null;
						}
						seekMoment = availablePeriods[ i - 1 ].End; // previous period
					}
					break;
			}

			return null;
		} // CalculateEnd

		// ----------------------------------------------------------------------
		// assumes no no overlapping periods in parameter periods
		private static ITimePeriod FindNextPeriod( DateTime start, IEnumerable<ITimePeriod> periods, out DateTime moment )
		{
			ITimePeriod nearestPeriod = null;
			TimeSpan difference = TimeSpan.MaxValue;
			moment = start;
			foreach ( ITimePeriod period in periods )
			{
				// inside period
				if ( period.HasInside( start ) )
				{
					nearestPeriod = period;
					moment = start;
					break;
				}

				// period out of range
				if ( period.End < start )
				{
					continue;
				}

				// not the nearest
				TimeSpan periodToMoment = period.Start - start;
				if ( periodToMoment >= difference )
				{
					continue;
				}

				difference = periodToMoment;
				nearestPeriod = period;
				moment = nearestPeriod.Start;
			}

			return nearestPeriod;
		} // FindNextPeriod

		// ----------------------------------------------------------------------
		// assumes no no overlapping periods in parameter periods
		private static ITimePeriod FindPreviousPeriod( DateTime start, IEnumerable<ITimePeriod> periods, out DateTime moment )
		{
			ITimePeriod nearestPeriod = null;
			TimeSpan difference = TimeSpan.MaxValue;
			moment = start;
			foreach ( ITimePeriod period in periods )
			{
				// inside period
				if ( period.HasInside( start ) )
				{
					nearestPeriod = period;
					moment = start;
					break;
				}

				// period out of range
				if ( period.Start > start )
				{
					continue;
				}

				// not the nearest
				TimeSpan periodToMoment = start - period.End;
				if ( periodToMoment >= difference )
				{
					continue;
				}

				difference = periodToMoment;
				nearestPeriod = period;
				moment = nearestPeriod.End;
			}

			return nearestPeriod;
		} // FindPreviousPeriod

		// ----------------------------------------------------------------------
		// members
		private readonly TimePeriodCollection includePeriods = new TimePeriodCollection();
		private readonly TimePeriodCollection excludePeriods = new TimePeriodCollection();

	} // class DateAdd

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
