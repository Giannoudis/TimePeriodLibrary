// -- FILE ------------------------------------------------------------------
// name       : CalendarPeriodCollectorFilter.cs
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
	public class CalendarPeriodCollectorFilter : CalendarVisitorFilter, ICalendarPeriodCollectorFilter
	{

		// ----------------------------------------------------------------------
		public override void Clear()
		{
			base.Clear();
			collectingMonths.Clear();
			collectingDays.Clear();
			collectingHours.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public IList<MonthRange> CollectingMonths
		{
			get { return collectingMonths; }
		} // CollectingMonths

		// ----------------------------------------------------------------------
		public IList<DayRange> CollectingDays
		{
			get { return collectingDays; }
		} // CollectingDays

		// ----------------------------------------------------------------------
		public IList<HourRange> CollectingHours
		{
			get { return collectingHours; }
		} // CollectingHours

		// ----------------------------------------------------------------------
		public IList<DayHourRange> CollectingDayHours
		{
			get { return collectingDayHours; }
		} // CollectingDayHours

		// ----------------------------------------------------------------------
		// members
		private readonly List<MonthRange> collectingMonths = new List<MonthRange>();
		private readonly List<DayRange> collectingDays = new List<DayRange>();
		private readonly List<HourRange> collectingHours = new List<HourRange>();
		private readonly List<DayHourRange> collectingDayHours = new List<DayHourRange>();

	} // class CalendarPeriodCollectorFilter

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
