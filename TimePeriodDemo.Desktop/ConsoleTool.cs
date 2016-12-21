// -- FILE ------------------------------------------------------------------
// name       : ConsoleTool.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Text;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public static class ConsoleTool
	{

		// ----------------------------------------------------------------------
		public static void WriteSeparatorLine( char symbol = '-', int symbolCount = 80 )
		{
			Console.WriteLine( new string( symbol, symbolCount ) );
		} // Write

		// ----------------------------------------------------------------------
		public static void Write( string text )
		{
			Console.Write( text );
		} // Write

		// ----------------------------------------------------------------------
		public static void Write( string format, params object[] args )
		{
			Console.Write( format, args );
		} // Write

		// ----------------------------------------------------------------------
		public static void WriteLine( string text )
		{
			Console.WriteLine( text );
		} // WriteLine

		// ----------------------------------------------------------------------
		public static void WriteLine( string format, params object[] args )
		{
			Console.WriteLine( format, args );
		} // WriteLine

		// ----------------------------------------------------------------------
		public static void WriteLine()
		{
			Console.WriteLine();
		} // WriteLine

		// ----------------------------------------------------------------------
		public static void WriteIndentLine( string text )
		{
			Console.WriteLine( indentText + text );
		} // WriteIndentLine

		// ----------------------------------------------------------------------
		public static void WriteIndentLine( string format, params object[] args )
		{
			Console.WriteLine( indentText + format, args );
		} // WriteIndentLine

		// ----------------------------------------------------------------------
		public static string Format( DateTime dateTime, bool compact = true )
		{
			if ( compact && !TimeTool.HasTimeOfDay( dateTime ) )
			{
				return dateTime.ToShortDateString();
			}
			return dateTime.ToString();
		} // Format

		// ----------------------------------------------------------------------
		public static string QueryText( string prompt, string defaultValue )
		{
			Console.Write( prompt );
			StringBuilder sb = new StringBuilder();
			ConsoleKeyInfo key;
			do
			{
				key = Console.ReadKey();
				if ( key.Key == ConsoleKey.Escape )
				{
					return null;
				}
				if ( key.Key == ConsoleKey.Enter )
				{
					Console.WriteLine();
					return sb.Length == 0 ? defaultValue : sb.ToString();
				}
				sb.Append( key.KeyChar );
			} while ( true );
		} // QueryNumber

		// ----------------------------------------------------------------------
		public static int? QueryNumber( string prompt, int defaultValue, int minValue, int maxValue )
		{
			do
			{
				Console.Write( prompt );
				StringBuilder sb = new StringBuilder();
				ConsoleKeyInfo key;
				do
				{
					key = Console.ReadKey();
					if ( key.Key == ConsoleKey.Escape )
					{
						return null;
					}
					if ( key.Key == ConsoleKey.Enter )
					{
						Console.WriteLine();
						if ( sb.Length == 0 )
						{
							return defaultValue;
						}
						int parsedValue;
						try
						{
							parsedValue = int.Parse( sb.ToString() );
						}
						catch ( FormatException )
						{
							Console.WriteLine( "invalid number: " + sb );
							sb.Remove( 0, sb.Length );
							break;
						}
						catch ( OverflowException )
						{
							Console.WriteLine( "invalid number: " + sb );
							sb.Remove( 0, sb.Length );
							break;
						}
						if ( parsedValue < minValue || parsedValue > maxValue )
						{
							Console.WriteLine( "number out of range: " + sb );
							sb.Remove( 0, sb.Length );
							break;
						}
						return parsedValue;
					}
					sb.Append( key.KeyChar );

				} while ( true );

			} while ( true );
		} // QueryNumber

		// ----------------------------------------------------------------------
		private const string indentText = "  ";

	} // class ConsoleTool

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
