// -- FILE ------------------------------------------------------------------
// name       : TimePeriodDurationComparer.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.23
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public sealed class TimePeriodDurationComparer : ITimePeriodComparer
	{

		// ----------------------------------------------------------------------
		public static ITimePeriodComparer Comparer = new TimePeriodDurationComparer();
		public static ITimePeriodComparer ReverseComparer = new TimePeriodReversComparer( new TimePeriodDurationComparer() );

		// ----------------------------------------------------------------------
		public int Compare( ITimePeriod left, ITimePeriod right )
		{
			return left.Duration.CompareTo( right.Duration );
		} // Compare

	} // class TimePeriodDurationComparer

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
