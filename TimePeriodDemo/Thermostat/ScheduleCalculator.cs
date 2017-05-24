// -- FILE ------------------------------------------------------------------
// name       : ScheduleCalculator.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo.Thermostat
{

	// ------------------------------------------------------------------------
	public class ScheduleCalculator<T> where T : class, ITimePeriod, new()
	{

		// ----------------------------------------------------------------------
		public ScheduleCalculator( ScheduleWeek<T> week )
		{
			if ( week == null )
			{
				throw new ArgumentNullException( "week" );
			}
			this.week = week;
		} // ScheduleCalculator

		// ----------------------------------------------------------------------
		public ITimePeriodCollection Holidays
		{
			get { return holidays; }
		} // Holidays

		// ----------------------------------------------------------------------
		public ScheduleWeek<T> Week
		{
			get { return week; }
		} // Week

		// ----------------------------------------------------------------------
		public DateTime CalculateNextStateChange( DateTime moment, WorkingState state )
		{
			DateTime? nextDate = null;

			while ( nextDate.HasValue == false )
			{
				ITimePeriod weekPeriod = week.GetWeekPeriod( moment );
				ITimePeriodCollection weekExcludePeriods = GetWeekExcludePeriods( moment );

				// setup moment for the next search
				ITimePeriod nextExcludePeriod = FindNextPeriod( weekExcludePeriods, moment );
				if ( nextExcludePeriod == null )
				{
					moment = weekPeriod.End;
					continue;
				}
				moment = nextExcludePeriod.End;

				// search next availabe date by adding zero
				DateAdd dateAdd = new DateAdd();
				dateAdd.IncludePeriods.Add( weekPeriod );
				dateAdd.ExcludePeriods.AddAll( weekExcludePeriods );

				// calculate the next avialable time
				nextDate = dateAdd.Add( moment, TimeSpan.Zero );
				if ( !nextDate.HasValue ) // not found in this week
				{
					moment = weekPeriod.End.AddTicks( 1 );
					continue;
				}

				switch ( state )
				{
					case WorkingState.On:
						break;
					case WorkingState.Off:
						ITimePeriod nextPeriod = FindSuccessorPeriod( weekExcludePeriods, nextDate.Value );
						if ( nextPeriod == null ) // search in next week
						{
							nextDate = null;
							moment = weekPeriod.End.AddTicks( 1 );
						}
						else
						{
							nextDate = nextPeriod.Start;
						}
						break;
				}
			}

			return nextDate.Value;
		} // CalculateNextStateChange

		// ----------------------------------------------------------------------
		private ITimePeriodCollection GetWeekExcludePeriods( DateTime moment )
		{
			ITimePeriodCollection weekExcludePeriods = new TimePeriodCollection();
			weekExcludePeriods.AddAll( holidays );

			Week calendarWeek = new Week( moment );
			foreach ( Day calendarDay in calendarWeek.GetDays() )
			{
				ScheduleDay<T> day = week[ calendarDay.DayOfWeek ];
				weekExcludePeriods.AddAll( day.GetExcludePeriods( calendarDay.Start ) );
			}

			return new TimePeriodCombiner<T>().CombinePeriods( weekExcludePeriods );
		} // GetWeekExcludePeriods

		// ----------------------------------------------------------------------
		private static ITimePeriod FindNextPeriod( IEnumerable<ITimePeriod> periods, DateTime moment )
		{
			foreach ( ITimePeriod period in periods )
			{
				if ( period.HasInside( moment ) || period.Start >= moment )
				{
					return period;
				}
			}
			return null;
		} // FindNextPeriod

		// ----------------------------------------------------------------------
		private static ITimePeriod FindSuccessorPeriod( IList<ITimePeriod> periods, DateTime endOfPrevious )
		{
			ITimePeriod current = FindPeriodByEnd( periods, endOfPrevious );
			if ( current == null )
			{
				return null;
			}
			int index = periods.IndexOf( current );
			if ( index == periods.Count - 1 )
			{
				return null;
			}
			return periods[ index + 1 ];
		} // FindSuccessorPeriod

		// ----------------------------------------------------------------------
		private static ITimePeriod FindPeriodByEnd( IEnumerable<ITimePeriod> periods, DateTime end )
		{
			foreach ( ITimePeriod period in periods )
			{
				if ( period.End.Equals( end ) )
				{
					return period;
				}
			}
			return null;
		} // FindPeriodByEnd

		// ----------------------------------------------------------------------
		// members
		private readonly TimePeriodCollection holidays = new TimePeriodCollection();
		private readonly ScheduleWeek<T> week;

	} // class ScheduleCalculator

} // namespace Itenso.TimePeriodDemo.Thermostat
// -- EOF -------------------------------------------------------------------
