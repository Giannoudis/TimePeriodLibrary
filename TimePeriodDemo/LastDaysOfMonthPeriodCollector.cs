// -- FILE ------------------------------------------------------------------
// name       : LastDaysOfMonthPeriodCollector.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class LastDaysOfMonthPeriodCollector : CalendarPeriodCollector
	{

		// ------------------------------------------------------------------------
		public LastDaysOfMonthPeriodCollector( CalendarPeriodCollectorFilter filter, ITimePeriod limits ) :
			base( filter, limits )
		{
		} // LastDaysOfMonthPeriodCollector

		// ----------------------------------------------------------------------
		protected override bool OnVisitDay( Day day, CalendarPeriodCollectorContext context )
		{
			// allow only the last day of the month
			if ( day.Month == day.AddDays( TimeSpec.DaysPerWeek ).Month )
			{
				return false;
			}

			return base.OnVisitDay( day, context );
		} // OnVisitDay

	} // class LastDaysOfMonthPeriodCollector

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
