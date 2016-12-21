// -- FILE ------------------------------------------------------------------
// name       : ICalendarVisitorFilter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.21
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface ICalendarVisitorFilter
	{

		// ----------------------------------------------------------------------
		ITimePeriodCollection ExcludePeriods { get; }

		// ----------------------------------------------------------------------
		IList<int> Years { get; }

		// ----------------------------------------------------------------------
		IList<YearMonth> Months { get; }

		// ----------------------------------------------------------------------
		IList<int> Days { get; }

		// ----------------------------------------------------------------------
		IList<DayOfWeek> WeekDays { get; }

		// ----------------------------------------------------------------------
		IList<int> Hours { get; }

		// ----------------------------------------------------------------------
		void AddWorkingWeekDays();

		// ----------------------------------------------------------------------
		void AddWeekendWeekDays();

		// ----------------------------------------------------------------------
		void Clear();

	} // interface ICalendarVisitorFilter

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
