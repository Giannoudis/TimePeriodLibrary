// -- FILE ------------------------------------------------------------------
// name       : DayRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public struct DayRange
	{

		// ----------------------------------------------------------------------
		public DayRange( int day ) :
			this( day, day )
		{
		} // DayRange

		// ----------------------------------------------------------------------
		public DayRange( int min, int max )
		{
			if ( min < 1 || min > TimeSpec.MaxDaysPerMonth )
			{
				throw new ArgumentOutOfRangeException( "min" );
			}
			if ( max < min || max > TimeSpec.MaxDaysPerMonth )
			{
				throw new ArgumentOutOfRangeException( "max" );
			}
			this.min = min;
			this.max = max;
		} // DayRange

		// ----------------------------------------------------------------------
		public int Min
		{
			get { return min; }
		} // Min

		// ----------------------------------------------------------------------
		public int Max
		{
			get { return max; }
		} // Max

		// ----------------------------------------------------------------------
		public bool IsSingleDay
		{
			get { return min == max; }
		} // IsSingleDay

		// ----------------------------------------------------------------------
		public bool HasInside( int test )
		{
			return test >= min && test <= max;
		} // HasInside

		// ----------------------------------------------------------------------
		// members
		private readonly int min;
		private readonly int max;

	} // struct DayRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
