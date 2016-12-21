// -- FILE ------------------------------------------------------------------
// name       : MonthRange.cs
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
	public struct MonthRange
	{

		// ----------------------------------------------------------------------
		public MonthRange( YearMonth month ) :
			this( month, month )
		{
		} // MonthRange

		// ----------------------------------------------------------------------
		public MonthRange( YearMonth min, YearMonth max )
		{
			if ( max < min )
			{
				throw new ArgumentOutOfRangeException( "max" );
			}
			this.min = min;
			this.max = max;
		} // MonthRange

		// ----------------------------------------------------------------------
		public YearMonth Min
		{
			get { return min; }
		} // Min

		// ----------------------------------------------------------------------
		public YearMonth Max
		{
			get { return max; }
		} // Max

		// ----------------------------------------------------------------------
		public bool IsSingleMonth
		{
			get { return min == max; }
		} // IsSingleMonth

		// ----------------------------------------------------------------------
		public bool HasInside( YearMonth test )
		{
			return test >= min && test <= max;
		} // HasInside

		// ----------------------------------------------------------------------
		// members
		private readonly YearMonth min;
		private readonly YearMonth max;

	} // struct MonthRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
