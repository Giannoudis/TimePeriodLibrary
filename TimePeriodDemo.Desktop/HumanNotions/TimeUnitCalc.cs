// -- FILE ------------------------------------------------------------------
// name       : TimeUnitCalc.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.05.23
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo.HumanNotions
{

	// ------------------------------------------------------------------------
	public static class TimeUnitCalc
	{

		// ----------------------------------------------------------------------
		public static DateTime CalcTimeMoment( TimeUnit offsetUnit, long offsetCount = 1, ITimeCalendar calendar = null )
		{
			return CalcTimeMoment( DateTime.Now, offsetUnit, offsetCount, calendar );
		} // CalcTimeMoment

		// ----------------------------------------------------------------------
		public static DateTime CalcTimeMoment( DateTime baseMoment, TimeUnit offsetUnit, long offsetCount = 1, ITimeCalendar calendar = null )
		{
			switch ( offsetUnit )
			{
				case TimeUnit.Tick:
					return baseMoment.AddTicks( offsetCount );
				case TimeUnit.Millisecond:
					DateTime offsetMillisecond = baseMoment.AddSeconds( offsetCount );
					return TimeTrim.Millisecond( offsetMillisecond, offsetMillisecond.Millisecond );
				case TimeUnit.Second:
					DateTime offsetSecond = baseMoment.AddSeconds( offsetCount );
					return TimeTrim.Second( offsetSecond, offsetSecond.Second );
				case TimeUnit.Minute:
					return new Minute(baseMoment, calendar).AddMinutes( ToInt( offsetCount ) ).Start;
				case TimeUnit.Hour:
					return new Hour( baseMoment, calendar ).AddHours( ToInt( offsetCount ) ).Start;
				case TimeUnit.Day:
					return new Day( baseMoment, calendar ).AddDays( ToInt( offsetCount ) ).Start;
				case TimeUnit.Week:
					return new Week( baseMoment, calendar ).AddWeeks( ToInt( offsetCount ) ).Start;
				case TimeUnit.Month:
					return new Month( baseMoment, calendar ).AddMonths( ToInt( offsetCount ) ).Start;
				case TimeUnit.Quarter:
					return new Quarter( baseMoment, calendar ).AddQuarters( ToInt( offsetCount ) ).Start;
				case TimeUnit.Halfyear:
					return new Halfyear( baseMoment, calendar ).AddHalfyears( ToInt( offsetCount ) ).Start;
				case TimeUnit.Year:
					return new Year( baseMoment, calendar ).AddYears( ToInt( offsetCount ) ).Start;
				default:
					throw new InvalidOperationException();
			}
		} // CalcTimeMoment

		// ----------------------------------------------------------------------
		public static ITimePeriod CalcTimePeriod( TimeUnit periodUnit, long periodDuration = 1,
			TimeUnit? offsetUnit = null, long offsetCount = 1, ITimeCalendar calendar = null )
		{
			return CalcTimePeriod( DateTime.Now, periodUnit, periodDuration, offsetUnit, offsetCount, calendar );
		} // CalcTimePeriod

		// ----------------------------------------------------------------------
		public static ITimePeriod CalcTimePeriod( DateTime baseMoment, TimeUnit periodUnit, long periodDuration = 1,
			TimeUnit? offsetUnit = null, long offsetCount = 1, ITimeCalendar calendar = null )
		{
			if ( !offsetUnit.HasValue )
			{
				offsetUnit = periodUnit;
			}
			DateTime start = CalcTimeMoment( baseMoment, offsetUnit.Value, offsetCount, calendar );
			switch ( periodUnit )
			{
				case TimeUnit.Tick:
					return new TimeRange( start, start.AddTicks( periodDuration ) );
				case TimeUnit.Millisecond:
					return new TimeRange( start, start.AddMilliseconds( periodDuration ) );
				case TimeUnit.Second:
					return new TimeRange( start, start.AddSeconds( periodDuration ) );
				case TimeUnit.Minute:
					return periodDuration == 1 ? new Minute( start, calendar ) as ITimePeriod : new Minutes( start, ToInt( periodDuration ), calendar );
				case TimeUnit.Hour:
					return periodDuration == 1 ? new Hour( start, calendar ) as ITimePeriod : new Hours( start, ToInt( periodDuration ), calendar );
				case TimeUnit.Day:
					return periodDuration == 1 ? new Day( start, calendar ) as ITimePeriod : new Days( start, ToInt( periodDuration ), calendar );
				case TimeUnit.Week:
					return periodDuration == 1 ? new Week( start, calendar ) as ITimePeriod : new Weeks( start, ToInt( periodDuration ), calendar );
				case TimeUnit.Month:
					YearMonth startMonth = calendar != null ? calendar.YearBaseMonth : TimeSpec.CalendarYearStartMonth;
					return periodDuration == 1 ? new Month( start, calendar ) as ITimePeriod : new Months( start, startMonth, ToInt( periodDuration ), calendar );
				case TimeUnit.Quarter:
					return periodDuration == 1 ? new Quarter( start, calendar ) as ITimePeriod : new Quarters( start, YearQuarter.First, ToInt( periodDuration ), calendar );
				case TimeUnit.Halfyear:
					return periodDuration == 1 ? new Halfyear( start, calendar ) as ITimePeriod : new Halfyears( start, YearHalfyear.First, ToInt( periodDuration ), calendar );
				case TimeUnit.Year:
					return periodDuration == 1 ? new Year( start, calendar ) as ITimePeriod : new Years( start, ToInt( periodDuration ), calendar );
				default:
					throw new InvalidOperationException();
			}
		} // CalcTimeMoment

		// ----------------------------------------------------------------------
		private static int ToInt( long value )
		{
			if ( value < int.MinValue || value > int.MaxValue )
			{
				throw new ArgumentOutOfRangeException( "value" );
			}
			return (int)value;
		} // ToInt

	} // class TimeUnitCalc

} // namespace Itenso.TimePeriodDemo.HumanNotions
// -- EOF -------------------------------------------------------------------
