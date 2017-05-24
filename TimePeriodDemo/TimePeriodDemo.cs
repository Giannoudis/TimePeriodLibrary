// -- FILE ------------------------------------------------------------------
// name       : TimePeriodDemo.cs
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
	public class TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		protected static void Write( string text )
		{
			ConsoleTool.Write( text );
		} // Write

		// ----------------------------------------------------------------------
		protected static void Write( string format, params object[] args )
		{
			ConsoleTool.Write( format, args );
		} // Write

		// ----------------------------------------------------------------------
		protected static void WriteLine( string text )
		{
			ConsoleTool.WriteLine( text );
		} // WriteLine

		// ----------------------------------------------------------------------
		protected static void WriteLine( string format, params object[] args )
		{
			ConsoleTool.WriteLine( format, args );
		} // WriteLine

		// ----------------------------------------------------------------------
		protected static void WriteLine()
		{
			ConsoleTool.WriteLine();
		} // WriteLine

		// ----------------------------------------------------------------------
		protected static void WriteIndentLine( string text )
		{
			ConsoleTool.WriteIndentLine( text );
		} // WriteIndentLine

		// ----------------------------------------------------------------------
		protected static void WriteIndentLine( string format, params object[] args )
		{
			ConsoleTool.WriteIndentLine( format, args );
		} // WriteIndentLine

		// ----------------------------------------------------------------------
		protected static string Format( DateTime dateTime, bool compact = true )
		{
			return ConsoleTool.Format( dateTime, compact );
		} // Format

	} // class TimePeriodDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
