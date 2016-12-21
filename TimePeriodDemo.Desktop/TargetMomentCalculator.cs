// -- FILE ------------------------------------------------------------------
// name       : TargetMomentCalculator.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.09.04
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class TargetMomentCalculator : TimeLinePeriodEvaluator
	{

		// ----------------------------------------------------------------------
		public TargetMomentCalculator( ITimePeriodContainer periods, TimeSpan duration ) :
			base( periods )
		{
			if ( duration == null )
			{
				throw new ArgumentNullException( "duration" );
			}
			this.duration = duration;
		} // TargetMomentCalculator

		// ----------------------------------------------------------------------
		public TimeSpan Duration
		{
			get { return duration; }
		} // Duration

		// ----------------------------------------------------------------------
		public DateTime? GetTargetMoment()
		{
			targetMoment = null;
			remainingDuration = duration;
			StartEvaluation();
			return targetMoment;
		} // GetTargetMoment

		// ----------------------------------------------------------------------
		protected override bool EvaluatePeriod( ITimePeriod period, int periodCount )
		{
			TimeSpan allPeriodDuration = new TimeSpan( period.Duration.Ticks * periodCount );

			if ( allPeriodDuration >= remainingDuration )
			{
				targetMoment = period.Start.Add( new TimeSpan( remainingDuration.Ticks / periodCount ) );
				return false;
			}

			remainingDuration = remainingDuration.Subtract( allPeriodDuration );
			return true;
		} // EvaluatePeriod

		// ----------------------------------------------------------------------
		// members
		private readonly TimeSpan duration;
		private TimeSpan remainingDuration;
		private DateTime? targetMoment;

	} // TargetMomentCalculator

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
