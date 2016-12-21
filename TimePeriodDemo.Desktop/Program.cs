// -- FILE ------------------------------------------------------------------
// name       : Program.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class Program
	{

		// ----------------------------------------------------------------------
		private enum ProgramMode
		{
			CommunitySamples,
			ArticleSamples,
			CalendarDemo,
		} // enum ProgramMode

		// ----------------------------------------------------------------------
		static void Main( string[] args )
		{
			ProgramMode programMode = ProgramMode.CalendarDemo;

			if ( args != null && args.Length > 0 )
			{
				foreach ( string arg in args )
				{
					if ( "-community".Equals( arg ) || "-c".Equals( arg ) )
					{
						programMode = ProgramMode.CommunitySamples;
					}
					else if ( "-article".Equals( arg ) || "-a".Equals( arg ) )
					{
						programMode = ProgramMode.ArticleSamples;
					}
				}
			}

			switch ( programMode )
			{
				case ProgramMode.CommunitySamples:
					StartCommunitySamples();
					break;
				case ProgramMode.ArticleSamples:
					StartArticleSamples();
					break;
				case ProgramMode.CalendarDemo:
					StartCalendarDemo();
					return;
			}

			Console.WriteLine( "press any key to quit ..." );
			Console.ReadKey( true );
		} // Main

		// ----------------------------------------------------------------------
		private static void StartCommunitySamples()
		{
			SampleInvoker.Start( typeof( CommunitySamples ) );
		} // StartCommunitySamples

		// ----------------------------------------------------------------------
		private static void StartArticleSamples()
		{
			SampleInvoker.Start( typeof( ArticleSamples ) );
		} // StartArticleSamples

		// ----------------------------------------------------------------------
		private static void StartCalendarDemo()
		{
			ConsoleTool.WriteSeparatorLine();
			Console.WriteLine( "Itenso Time Period Demo" );

			TimePeriodDemoData demoData = new TimePeriodDemoData();

			// culture
			if ( demoData.QueryCulture() == false )
			{
				return;
			}

			string periodType = "y";
			while ( !string.IsNullOrEmpty( periodType ) )
			{
				try
				{
					if ( QueryPeriodData( ref periodType, demoData ) == false )
					{
						break;
					}
				}
				catch ( Exception e )
				{
					Console.WriteLine( "input error: " + e.Message );
				}
				if ( string.IsNullOrEmpty( periodType ) )
				{
					break;
				}
				ShowPeriodData( periodType, demoData );
			}
		} // Main

		// ----------------------------------------------------------------------
		private static bool QueryPeriodData( ref string periodType, TimePeriodDemoData demoData )
		{
			ConsoleTool.WriteSeparatorLine();
			do
			{
				string input = ConsoleTool.QueryText( "Period type (y/by/hy/q/m/bm/w/bw/d/h/min) [enter=" + periodType + "]: ", periodType );
				if ( string.IsNullOrEmpty( input ) )
				{
					return false;
				}

				switch ( input )
				{
					case "Y":
						Console.Clear();
						goto case "y";
					case "y":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearBaseMonth();
					case "BY":
						Console.Clear();
						goto case "by";
					case "by":
						periodType = input;
						return
							demoData.QueryYear();
					case "HY":
						Console.Clear();
						goto case "hy";
					case "hy":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearBaseMonth() &&
							demoData.QueryYearHalfyear();
					case "Q":
						Console.Clear();
						goto case "q";
					case "q":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearBaseMonth() &&
							demoData.QueryYearQuarter();
					case "M":
						Console.Clear();
						goto case "m";
					case "m":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearMonth();
					case "BM":
						Console.Clear();
						goto case "bm";
					case "bm":
						periodType = input;
						return
							demoData.QueryYear() &&
							demoData.QueryYearMonth();
					case "W":
						Console.Clear();
						goto case "w";
					case "w":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearWeekType() &&
							demoData.QueryWeek();
					case "BW":
						Console.Clear();
						goto case "bw";
					case "bw":
						periodType = input;
						return
							demoData.QueryYear() &&
							demoData.QueryWeek();
					case "D":
						Console.Clear();
						goto case "d";
					case "d":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearMonth() &&
							demoData.QueryDay();
					case "H":
						Console.Clear();
						goto case "h";
					case "h":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearMonth() &&
							demoData.QueryDay() &&
							demoData.QueryHour();
					case "Min":
						Console.Clear();
						goto case "min";
					case "min":
						periodType = input;
						return
							demoData.QueryPeriodCount() &&
							demoData.QueryYear() &&
							demoData.QueryYearMonth() &
							demoData.QueryDay() &&
							demoData.QueryHour() &&
							demoData.QueryMinute();
				}
			} while ( true );
		} // QueryPeriodData

		// ----------------------------------------------------------------------
		private static void ShowPeriodData( string periodType, TimePeriodDemoData demoData )
		{
			Console.WriteLine();
			Console.WriteLine( "Time Period Demo" );
			Console.WriteLine( "Start: {0}", demoData.SetupDate );

			switch ( periodType.ToLower() )
			{
				case "y":
					YearDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.CalendarConfig );
					break;
				case "by":
					BroadcastYearDemo.ShowAll( demoData.Year );
					break;
				case "hy":
					HalfyearDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.Halfyear, demoData.CalendarConfig );
					break;
				case "q":
					QuarterDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.Quarter, demoData.CalendarConfig );
					break;
				case "m":
					MonthDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.Month );
					break;
				case "bm":
					BroadcastMonthDemo.ShowAll( demoData.Year, demoData.Month );
					break;
				case "w":
					WeekDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.Week, demoData.CalendarConfig );
					break;
				case "bw":
					BroadcastWeekDemo.ShowAll( demoData.Year, demoData.Week );
					break;
				case "d":
					DayDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.Month, demoData.Day );
					break;
				case "h":
					HourDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.Month, demoData.Day, demoData.Hour );
					break;
				case "min":
					MinuteDemo.ShowAll( demoData.PeriodCount, demoData.Year, demoData.Month, demoData.Day, demoData.Hour, demoData.Minute );
					break;
			}
		} // StartCalendarDemo

	} // class Program

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
