// -- FILE ------------------------------------------------------------------
// name       : TimeLinePeriodEvaluator.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.09.04
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public abstract class TimeLinePeriodEvaluator
	{

		// ----------------------------------------------------------------------
		protected TimeLinePeriodEvaluator( ITimePeriodContainer periods, ITimePeriodMapper periodMapper = null )
		{
			if ( periods == null )
			{
				throw new ArgumentNullException( "periods" );
			}

			this.periods = periods;
			this.periodMapper = periodMapper;
		} // TimeLinePeriodEvaluator

		// ----------------------------------------------------------------------
		public ITimePeriodContainer Periods
		{
			get { return periods; }
		} // Periods

		// ----------------------------------------------------------------------
		public ITimePeriodMapper PeriodMapper
		{
			get { return periodMapper; }
		} // PeriodMapper

		// ----------------------------------------------------------------------
		protected virtual bool IgnoreEmptyPeriods
		{
			get { return false; }
		} // IgnoreEmptyPeriods

		// ----------------------------------------------------------------------
		protected virtual void StartEvaluation()
		{
			if ( periods.Count > 0 )
			{
				TimeLineMomentCollection timeLineMoments = new TimeLineMomentCollection();
				timeLineMoments.AddAll( periods );
				if ( timeLineMoments.Count > 1 )
				{
					int periodCount = 0;
					for ( int i = 0; i < timeLineMoments.Count - 1; i++ )
					{
						ITimeLineMoment start = timeLineMoments[ i ];
						ITimeLineMoment end = timeLineMoments[ i + 1 ];

						if ( i == 0 )
						{
							periodCount += start.StartCount;
							periodCount -= start.EndCount;
						}

						ITimePeriod period = new TimeRange( MapPeriodStart( start.Moment ), MapPeriodEnd( end.Moment ) );
						if ( !( IgnoreEmptyPeriods && period.IsMoment ) )
						{
							if ( EvaluatePeriod( period, periodCount ) == false )
							{
								break;
							}
						}

						periodCount += end.StartCount;
						periodCount -= end.EndCount;
					}
				}
			}
		} // StartEvaluation

		// ----------------------------------------------------------------------
		protected abstract bool EvaluatePeriod( ITimePeriod period, int periodCount );

		// ----------------------------------------------------------------------
		private DateTime MapPeriodStart( DateTime start )
		{
			return periodMapper != null ? periodMapper.UnmapStart( start ) : start;
		} // MapPeriodStart

		// ----------------------------------------------------------------------
		private DateTime MapPeriodEnd( DateTime end )
		{
			return periodMapper != null ? periodMapper.UnmapEnd( end ) : end;
		} // MapPeriodEnd

		// ----------------------------------------------------------------------
		// members
		private readonly ITimePeriodContainer periods;
		private readonly ITimePeriodMapper periodMapper;

	} // class TimeLinePeriodEvaluator

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
