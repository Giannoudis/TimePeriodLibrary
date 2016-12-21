// -- FILE ------------------------------------------------------------------
// name       : TimePeriodEndComparer.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.23
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public sealed class TimePeriodEndComparer : ITimePeriodComparer
	{

		// ----------------------------------------------------------------------
		public static ITimePeriodComparer Comparer = new TimePeriodEndComparer();
		public static ITimePeriodComparer ReverseComparer = new TimePeriodReversComparer( new TimePeriodEndComparer() );

		// ----------------------------------------------------------------------
		public int Compare( ITimePeriod left, ITimePeriod right )
		{
			return left.End.CompareTo( right.End );
		} // Compare

	} // class TimePeriodEndComparer

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
