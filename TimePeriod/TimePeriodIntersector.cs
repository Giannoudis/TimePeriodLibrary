// -- FILE ------------------------------------------------------------------
// name       : TimePeriodIntersector.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.22
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimePeriodIntersector<T> where T : ITimePeriod, new()
	{

		// ----------------------------------------------------------------------
		public TimePeriodIntersector() :
			this( null )
		{
		} // TimePeriodIntersector

		// ----------------------------------------------------------------------
		public TimePeriodIntersector( ITimePeriodMapper periodMapper )
		{
			this.periodMapper = periodMapper;
		} // TimePeriodIntersector

		// ----------------------------------------------------------------------
		public ITimePeriodMapper PeriodMapper
		{
			get { return periodMapper; }
		} // PeriodMapper

		// ----------------------------------------------------------------------
		public virtual ITimePeriodCollection IntersectPeriods( ITimePeriodContainer periods, bool combinePeriods = true )
		{
			if ( periods == null )
			{
				throw new ArgumentNullException( "periods" );
			}
			TimeLine<T> timeLine = new TimeLine<T>( periods, periodMapper );
			return timeLine.IntersectPeriods( combinePeriods );
		} // IntersectPeriods

		// ----------------------------------------------------------------------
		// members
		private readonly ITimePeriodMapper periodMapper;

	} // class TimePeriodIntersector

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
