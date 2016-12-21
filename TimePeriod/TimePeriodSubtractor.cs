// -- FILE ------------------------------------------------------------------
// name       : TimePeriodSubtractor.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.09.30
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimePeriodSubtractor<T> where T : ITimePeriod, new()
	{

		// ----------------------------------------------------------------------
		public TimePeriodSubtractor() :
			this( null )
		{
		} // TimePeriodSubtractor

		// ----------------------------------------------------------------------
		public TimePeriodSubtractor( ITimePeriodMapper periodMapper )
		{
			this.periodMapper = periodMapper;
			timePeriodCombiner = new TimePeriodCombiner<T>( periodMapper );
			timeGapCalculator = new TimeGapCalculator<T>( periodMapper );
			timePeriodIntersector = new TimePeriodIntersector<T>( periodMapper );
		} // TimePeriodSubtractor

		// ----------------------------------------------------------------------
		public ITimePeriodMapper PeriodMapper
		{
			get { return periodMapper; }
		} // PeriodMapper

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection SubtractPeriods( ITimePeriodContainer sourcePeriods, ITimePeriodCollection subtractingPeriods,
			bool combinePeriods = true )
		{
			if ( sourcePeriods == null )
			{
				throw new ArgumentNullException( "sourcePeriods" );
			}
			if ( subtractingPeriods == null )
			{
				throw new ArgumentNullException( "subtractingPeriods" );
			}

			if ( sourcePeriods.Count == 0 )
			{
				return new TimePeriodCollection();
			}

			if ( subtractingPeriods.Count == 0 && !combinePeriods )
			{
				return new TimePeriodCollection( sourcePeriods );
			}

			// combined source periods
			sourcePeriods = timePeriodCombiner.CombinePeriods( sourcePeriods );

			// combined subtracting periods
			if ( subtractingPeriods.Count == 0 )
			{
				return new TimePeriodCollection( sourcePeriods );
			}
			subtractingPeriods = timePeriodCombiner.CombinePeriods( subtractingPeriods );

			// invert subtracting periods
			sourcePeriods.AddAll( timeGapCalculator.GetGaps( subtractingPeriods, new TimeRange( sourcePeriods.Start, sourcePeriods.End ) ) );

			return timePeriodIntersector.IntersectPeriods( sourcePeriods );
		} // SubtractPeriods

		// ----------------------------------------------------------------------
		// members
		private readonly ITimePeriodMapper periodMapper;
		private readonly TimePeriodCombiner<T> timePeriodCombiner;
		private readonly TimeGapCalculator<T> timeGapCalculator;
		private readonly TimePeriodIntersector<T> timePeriodIntersector;

	} // class TimePeriodSubtractor

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
