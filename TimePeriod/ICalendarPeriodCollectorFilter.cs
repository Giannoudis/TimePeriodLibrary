// -- FILE ------------------------------------------------------------------
// name       : ICalendarPeriodCollectorFilter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface ICalendarPeriodCollectorFilter : ICalendarVisitorFilter
	{

		// ----------------------------------------------------------------------
		IList<MonthRange> CollectingMonths { get; }

		// ----------------------------------------------------------------------
		IList<DayRange> CollectingDays { get; }

		// ----------------------------------------------------------------------
		IList<HourRange> CollectingHours { get; }

	} // interface ICalendarPeriodCollectorFilter

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
