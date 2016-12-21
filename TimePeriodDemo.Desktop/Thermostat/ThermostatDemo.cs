// -- FILE ------------------------------------------------------------------
// name       : ThermostatDemo.cs
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
	public class ThermostatDemo
	{

		// ----------------------------------------------------------------------
		public ThermostatDemo()
		{
			calculator = new ScheduleCalculator<TimeRange>( GetScheduleWeek() );
			SetupHolidays( calculator.Holidays );
		} // ThermostatDemo

		// ----------------------------------------------------------------------
		public void DemoOn()
		{
			Console.WriteLine( "Monday" );
			DemoOn( new DateTime( 2011, 3, 28, 0, 0, 0 ) );
			DemoOn( new DateTime( 2011, 3, 28, 6, 30, 0 ) );
			DemoOn( new DateTime( 2011, 3, 28, 8, 30, 0 ) );
			DemoOn( new DateTime( 2011, 3, 28, 15, 0, 0 ) );
			DemoOn( new DateTime( 2011, 3, 28, 22, 30, 0 ) );

			Console.WriteLine( "Wednesday (holiday)" );
			DemoOn( new DateTime( 2011, 3, 30, 0, 0, 0 ) );

			Console.WriteLine( "Friday" );
			DemoOn( new DateTime( 2011, 4, 1, 0, 0, 0 ) );
			DemoOn( new DateTime( 2011, 4, 1, 6, 30, 0 ) );
			DemoOn( new DateTime( 2011, 4, 1, 8, 30, 0 ) );
			DemoOn( new DateTime( 2011, 4, 1, 15, 0, 0 ) );
			DemoOn( new DateTime( 2011, 4, 1, 22, 30, 0 ) );

			Console.WriteLine( "Saturday" );
			DemoOn( new DateTime( 2011, 4, 2, 0, 0, 0 ) );
			DemoOn( new DateTime( 2011, 4, 2, 6, 30, 0 ) );
			DemoOn( new DateTime( 2011, 4, 2, 8, 30, 0 ) );

			Console.WriteLine( "Sunday" );
			DemoOn( new DateTime( 2011, 4, 3, 0, 0, 0 ) );
			DemoOn( new DateTime( 2011, 4, 3, 6, 30, 0 ) );
			DemoOn( new DateTime( 2011, 4, 3, 8, 30, 0 ) );
		} // DemoOn

		// ----------------------------------------------------------------------
		public void DemoOff()
		{
			Console.WriteLine( "Monday" );
			DemoOff( new DateTime( 2011, 3, 28, 00, 00, 0 ) );
			DemoOff( new DateTime( 2011, 3, 28, 06, 30, 0 ) );
			DemoOff( new DateTime( 2011, 3, 28, 08, 30, 0 ) );
			DemoOff( new DateTime( 2011, 3, 28, 15, 00, 0 ) );
			DemoOff( new DateTime( 2011, 3, 28, 23, 00, 0 ) );

			Console.WriteLine( "Wednesday (holiday)" );
			DemoOff( new DateTime( 2011, 3, 30, 0, 0, 0 ) );

			Console.WriteLine( "Friday" );
			DemoOff( new DateTime( 2011, 4, 1, 00, 00, 0 ) );
			DemoOff( new DateTime( 2011, 4, 1, 06, 30, 0 ) );
			DemoOff( new DateTime( 2011, 4, 1, 08, 30, 0 ) );
			DemoOff( new DateTime( 2011, 4, 1, 15, 00, 0 ) );
			DemoOff( new DateTime( 2011, 4, 1, 23, 00, 0 ) );

			Console.WriteLine( "Saturday" );
			DemoOff( new DateTime( 2011, 4, 2, 00, 00, 0 ) );
			DemoOff( new DateTime( 2011, 4, 2, 06, 30, 0 ) );
			DemoOff( new DateTime( 2011, 4, 2, 23, 00, 0 ) );

			Console.WriteLine( "Sunday" );
			DemoOff( new DateTime( 2011, 4, 3, 00, 00, 0 ) );
			DemoOff( new DateTime( 2011, 4, 3, 06, 30, 0 ) );
			DemoOff( new DateTime( 2011, 4, 3, 23, 00, 0 ) );
		} // DemoOff

		// ----------------------------------------------------------------------
		private void DemoOn( DateTime moment )
		{
			Console.WriteLine( "Next 'On' from {0}: {1}", moment, GetNextOnDate( moment ) );
		} // Demo

		// ----------------------------------------------------------------------
		private void DemoOff( DateTime moment )
		{
			Console.WriteLine( "Next 'Off' from {0}: {1}", moment, GetNextOffDate( moment ) );
		} // DemoOff

		// ----------------------------------------------------------------------
		public void Start()
		{
			On( DateTime.Now );
		} // Start

		// ----------------------------------------------------------------------
		public void On( DateTime moment )
		{
			DateTime nextOnDate = GetNextOnDate( moment );
			Console.WriteLine( "starting timer 'On' from {0} to {1}", moment, nextOnDate );
		} // On

		// ----------------------------------------------------------------------
		public void Off( DateTime moment )
		{
			DateTime nextOnDate = GetNextOffDate( moment );
			Console.WriteLine( "starting timer 'Off' from {0} to {1}", moment, nextOnDate );
		} // Off

		// ----------------------------------------------------------------------
		public DateTime GetNextOnDate()
		{
			return GetNextOnDate( DateTime.Now );
		} // GetNextOnDate

		// ----------------------------------------------------------------------
		public DateTime GetNextOnDate( DateTime moment )
		{
			return calculator.CalculateNextStateChange( moment, WorkingState.On );
		} // GetNextOnDate

		// ----------------------------------------------------------------------
		public DateTime GetNextOffDate()
		{
			return GetNextOffDate( DateTime.Now );
		} // GetNextOffDate

		// ----------------------------------------------------------------------
		public DateTime GetNextOffDate( DateTime moment )
		{
			return calculator.CalculateNextStateChange( moment, WorkingState.Off );
		} // GetNextOffDate

		// ----------------------------------------------------------------------
		private static ScheduleWeek<TimeRange> GetScheduleWeek()
		{
			ScheduleWeek<TimeRange> week = new ScheduleWeek<TimeRange>();

			DateTime now = DateTime.Now; // used as time container

			week[ DayOfWeek.Monday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 6, 30 ), TimeTrim.Hour( now, 8, 30 ) ) );
			week[ DayOfWeek.Monday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 15 ), TimeTrim.Hour( now, 22, 30 ) ) );

			week[ DayOfWeek.Tuesday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 6, 30 ), TimeTrim.Hour( now, 8, 30 ) ) );
			week[ DayOfWeek.Monday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 15 ), TimeTrim.Hour( now, 22, 30 ) ) );

			week[ DayOfWeek.Wednesday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 6, 30 ), TimeTrim.Hour( now, 8, 30 ) ) );
			week[ DayOfWeek.Wednesday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 12 ), TimeTrim.Hour( now, 22, 30 ) ) );

			week[ DayOfWeek.Thursday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 6, 30 ), TimeTrim.Hour( now, 8, 30 ) ) );
			week[ DayOfWeek.Thursday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 15 ), TimeTrim.Hour( now, 22, 30 ) ) );

			week[ DayOfWeek.Friday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 6, 30 ), TimeTrim.Hour( now, 8, 30 ) ) );
			week[ DayOfWeek.Friday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 15 ), TimeTrim.Hour( now, 22, 30 ) ) );

			week[ DayOfWeek.Saturday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 7 ), TimeTrim.Hour( now, 22, 30 ) ) );

			week[ DayOfWeek.Sunday ].WorkingTimes.Add( new TimeRange( TimeTrim.Hour( now, 7 ), TimeTrim.Hour( now, 22, 30 ) ) );

			return week;
		} // GetScheduleWeek

		// ----------------------------------------------------------------------
		private static void SetupHolidays( ICollection<ITimePeriod> holidays )
		{
			holidays.Add( new TimeRange( new DateTime( 2011, 3, 30 ), new DateTime( 2011, 3, 31 ) ) );

			// more holidays please :)
		} // SetupHolidays

		// ----------------------------------------------------------------------
		// members
		private readonly ScheduleCalculator<TimeRange> calculator;

	} // class ThermostatDemo

} // namespace Itenso.TimePeriodDemo.Thermostat
// -- EOF -------------------------------------------------------------------