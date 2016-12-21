// -- FILE ------------------------------------------------------------------
// name       : TimeGapCalculator.cs
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
	public class TimeGapCalculator<T> where T : ITimePeriod, new()
	{

		// ----------------------------------------------------------------------
		public TimeGapCalculator() :
			this( null )
		{
		} // TimeGapCalculator

		// ----------------------------------------------------------------------
		public TimeGapCalculator( ITimePeriodMapper periodMapper )
		{
			this.periodMapper = periodMapper;
		} // TimeGapCalculator

		// ----------------------------------------------------------------------
		public ITimePeriodMapper PeriodMapper
		{
			get { return periodMapper; }
		} // PeriodMapper

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection GetGaps( ITimePeriodContainer periods, ITimePeriod limits = null ) 
		{
			if ( periods == null )
			{
				throw new ArgumentNullException( "periods" );
			}
			TimeLine<T> timeLine = new TimeLine<T>( periods, limits, periodMapper );
			return timeLine.CalculateGaps();
		} // GetGaps

		// ----------------------------------------------------------------------
		// members
		private readonly ITimePeriodMapper periodMapper;

	} // class TimeGapCalculator

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
