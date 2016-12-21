// -- FILE ------------------------------------------------------------------
// name       : CalendarVisitorFilter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class CalendarVisitorFilter : ICalendarVisitorFilter
	{

		// ----------------------------------------------------------------------
		public virtual void Clear()
		{
			years.Clear();
			months.Clear();
			days.Clear();
			weekDays.Clear();
			hours.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public ITimePeriodCollection ExcludePeriods
		{
			get { return excludePeriods; }
		} // ExcludePeriods

		// ----------------------------------------------------------------------
		public IList<int> Years
		{
			get { return years; }
		} // Years

		// ----------------------------------------------------------------------
		public IList<YearMonth> Months
		{
			get { return months; }
		} // Months

		// ----------------------------------------------------------------------
		public IList<int> Days
		{
			get { return days; }
		} // Days

		// ----------------------------------------------------------------------
		public IList<DayOfWeek> WeekDays
		{
			get { return weekDays; }
		} // WeekDays

		// ----------------------------------------------------------------------
		public IList<int> Hours
		{
			get { return hours; }
		} // Hours

		// ----------------------------------------------------------------------
		public void AddWorkingWeekDays()
		{
			weekDays.Add( DayOfWeek.Monday );
			weekDays.Add( DayOfWeek.Tuesday );
			weekDays.Add( DayOfWeek.Wednesday );
			weekDays.Add( DayOfWeek.Thursday );
			weekDays.Add( DayOfWeek.Friday );
		} // AddWorkingWeekDays

		// ----------------------------------------------------------------------
		public void AddWeekendWeekDays()
		{
			weekDays.Add( DayOfWeek.Saturday );
			weekDays.Add( DayOfWeek.Sunday );
		} // AddWeekendWeekDays

		// ----------------------------------------------------------------------
		// members
		private readonly TimePeriodCollection excludePeriods = new TimePeriodCollection();
		private readonly List<int> years = new List<int>();
		private readonly List<YearMonth> months = new List<YearMonth>();
		private readonly List<int> days = new List<int>();
		private readonly List<DayOfWeek> weekDays = new List<DayOfWeek>();
		private readonly List<int> hours = new List<int>();

	} // class CalendarVisitorFilter

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
