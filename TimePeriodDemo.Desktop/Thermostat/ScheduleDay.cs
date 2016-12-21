// -- FILE ------------------------------------------------------------------
// name       : ScheduleDay.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo.Thermostat
{

	// ------------------------------------------------------------------------
	public class ScheduleDay<T> where T : class, ITimePeriod, new()
	{

		// ----------------------------------------------------------------------
		public ScheduleDay( DayOfWeek dayOfWeek )
		{
			this.dayOfWeek = dayOfWeek;
		} // ScheduleDay

		// ----------------------------------------------------------------------
		public ITimePeriodCollection WorkingTimes
		{
			get { return workingTimes; }
		} // WorkingTimes

		// ----------------------------------------------------------------------
		public DayOfWeek DayOfWeek
		{
			get { return dayOfWeek; }
		} // DayOfWeek

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetWorkingPeriods( DateTime moment )
		{
			TimePeriodCollection workingPeriods = new TimePeriodCollection();
			foreach ( ITimePeriod workingTime in workingTimes )
			{
				DateTime start = TimeTool.SetTimeOfDay( moment, workingTime.Start );
				DateTime end = TimeTool.SetTimeOfDay( moment, workingTime.End );
				T item = new T();
				item.Setup( start, end );
				workingPeriods.Add( item );
			}
			return workingPeriods;
		} // GetWorkingPeriods

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetExcludePeriods( DateTime moment )
		{
			ITimePeriodCollection workingPeriods = GetWorkingPeriods( moment );
			if ( workingPeriods.Count == 0 )
			{
				return new TimePeriodCollection();
			}

			DateTime dayStart = TimeTrim.Hour( moment );
			DateTime dayEnd = dayStart.Add( new TimeSpan( 1, 0, 0, 0 ) );
			T day = new T();
			day.Setup( dayStart, dayEnd );

			return new TimeGapCalculator<T>().GetGaps( workingPeriods, day );
		} // GetExcludePeriods

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return dayOfWeek + ": " + workingTimes;
		} // ToString

		// ----------------------------------------------------------------------
		// members
		private readonly ITimePeriodCollection workingTimes = new TimePeriodCollection();
		private readonly DayOfWeek dayOfWeek;

	} // class ScheduleDay

} // namespace Itenso.TimePeriodDemo.Thermostat
// -- EOF -------------------------------------------------------------------
