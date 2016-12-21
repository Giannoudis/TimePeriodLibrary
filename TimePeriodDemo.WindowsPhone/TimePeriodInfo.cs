// -- FILE ------------------------------------------------------------------
// name       : TimePeriodInfo.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections;
using System.Text;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class TimePeriodInfo
	{

		// ----------------------------------------------------------------------
		public void Clear()
		{
			sb.Remove( 0, sb.Length );
		} // Clear

		// ----------------------------------------------------------------------
		public void AddSection( string name )
		{
			sb.Append( new string( '-', 5 ) );
			sb.Append( " " );
			sb.Append( name );
			sb.Append( " " );
			sb.Append( new string( '-', 5 ) );
			sb.Append( Environment.NewLine );
		} // AddSection

		// ----------------------------------------------------------------------
		public void AddItem( string label, object value )
		{
			sb.Append( label );
			sb.Append( ": " );
			sb.Append( value );
			sb.Append( Environment.NewLine );
		} // AddItem

		// ----------------------------------------------------------------------
		public void AddSubitems( string label, IEnumerable values )
		{
			AddSection( label );
			foreach ( object value in values )
			{
				sb.Append( value );
				sb.Append( Environment.NewLine );
			}
		} // AddSubitems

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return sb.ToString();
		} // ToString

		// ----------------------------------------------------------------------
		// members
		private readonly StringBuilder sb = new StringBuilder();

	} // class TimePeriodInfo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
