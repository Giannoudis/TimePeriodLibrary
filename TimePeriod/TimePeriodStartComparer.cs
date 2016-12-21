// -- FILE ------------------------------------------------------------------
// name       : TimePeriodStartComparer.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.23
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public sealed class TimePeriodStartComparer : ITimePeriodComparer
	{

		// ----------------------------------------------------------------------
		public static ITimePeriodComparer Comparer = new TimePeriodStartComparer();
		public static ITimePeriodComparer ReverseComparer = new TimePeriodReversComparer( new TimePeriodStartComparer() );

		// ----------------------------------------------------------------------
		public int Compare( ITimePeriod left, ITimePeriod right )
		{
			return left.Start.CompareTo( right.Start );
		} // Compare

	} // class TimePeriodStartComparer

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
