// -- FILE ------------------------------------------------------------------
// name       : TimeCalendarConfig.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public struct TimeCalendarConfig
	{

		// ----------------------------------------------------------------------
		public CultureInfo Culture { get; set; }

		// ----------------------------------------------------------------------
		public YearType? YearType { get; set; }

		// ----------------------------------------------------------------------
		public TimeSpan? StartOffset { get; set; }

		// ----------------------------------------------------------------------
		public TimeSpan? EndOffset { get; set; }

		// ----------------------------------------------------------------------
		public YearMonth? YearBaseMonth { get; set; }

		// ----------------------------------------------------------------------
		public YearMonth? FiscalYearBaseMonth { get; set; }

		// ----------------------------------------------------------------------
		public DayOfWeek? FiscalFirstDayOfYear { get; set; }

		// ----------------------------------------------------------------------
		public FiscalYearAlignment? FiscalYearAlignment { get; set; }

		// ----------------------------------------------------------------------
		public FiscalQuarterGrouping? FiscalQuarterGrouping { get; set; }

		// ----------------------------------------------------------------------
		public YearWeekType? YearWeekType { get; set; }

		// ----------------------------------------------------------------------
		public CalendarNameType? DayNameType { get; set; }

		// ----------------------------------------------------------------------
		public CalendarNameType? MonthNameType { get; set; }

	} // struct TimeCalendarConfig

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
