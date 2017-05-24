// -- FILE ------------------------------------------------------------------
// name       : ScheduleWeek.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo.Thermostat
{

	// ------------------------------------------------------------------------
	public class ScheduleWeek<T>  where T : class, ITimePeriod, new()
	{

		// ----------------------------------------------------------------------
		public ScheduleWeek()
		{
			Setup();
		} // ScheduleWeek

		// ----------------------------------------------------------------------
		public ScheduleDay<T> this[ DayOfWeek dayOfWeek ]
		{
			get { return days[ dayOfWeek ]; }
		} // this[]

		// ----------------------------------------------------------------------
		public IEnumerable Days
		{
			get { return days.Values; }
		} // Days

		// ----------------------------------------------------------------------
		public ITimePeriod GetWeekPeriod( DateTime moment )
		{
			DateTime weekStart = new Week( moment ).Start;
			DateTime weekEnd = weekStart.AddDays( TimeSpec.DaysPerWeek );
		
			T week = new T();
			week.Setup( weekStart, weekEnd );
			return week;
		} // GetWeekPeriod

		// ----------------------------------------------------------------------
		private void Setup()
		{
			foreach ( DayOfWeek dayOfWeek in Enum.GetValues( typeof( DayOfWeek ) ) )
			{
				days.Add( dayOfWeek, new ScheduleDay<T>( dayOfWeek ) );
			}
		} // Setup

		// ----------------------------------------------------------------------
		// members
		private readonly Dictionary<DayOfWeek,ScheduleDay<T>> days = new Dictionary<DayOfWeek,ScheduleDay<T>>();

	} // class ScheduleWeek

} // namespace Itenso.TimePeriodDemo.Thermostat
// -- EOF -------------------------------------------------------------------
