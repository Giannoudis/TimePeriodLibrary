// -- FILE ------------------------------------------------------------------
// name       : TimeRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.12
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class ComparableTimeRange : TimeRange, IComparable
	{

		// ----------------------------------------------------------------------
		public ComparableTimeRange() :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate )
		{
		} // ComparableTimeRange

		// ----------------------------------------------------------------------
		public ComparableTimeRange( DateTime start, DateTime end, bool isReadOnly = false ) :
			base( start, end, isReadOnly )
		{
		} // ComparableTimeRange

		// ----------------------------------------------------------------------
		public ComparableTimeRange( DateTime start, TimeSpan duration, bool isReadOnly = false ) :
			base( start, duration, isReadOnly )
		{
		} // ComparableTimeRange

		// ----------------------------------------------------------------------
		public ComparableTimeRange( ITimePeriod copy ) :
			base( copy )
		{
		} // ComparableTimeRange

		// ----------------------------------------------------------------------
		public int CompareTo( object obj )
		{
			if ( obj == null )
			{
				throw new ArgumentNullException( "obj" );
			}

			//return CompareDuration( obj as ITimePeriod );
			return CompareStart( obj as ITimePeriod );
		} // CompareTo

		// ----------------------------------------------------------------------
		public int CompareStart( ITimePeriod compare )
		{
			if ( compare == null )
			{
				throw new ArgumentNullException( "compare" );
			}
			return Start.CompareTo( compare.Start );
		} // CompareStart

		// ----------------------------------------------------------------------
		public int CompareDuration( ITimePeriod compare )
		{
			if ( compare == null )
			{
				throw new ArgumentNullException( "compare" );
			}
			return Duration.CompareTo( compare.Duration );
		} // CompareDuration

	} // class ComparableTimeRange

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
