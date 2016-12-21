// -- FILE ------------------------------------------------------------------
// name       : TimeIntervalComparer.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.11.23
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class TimeIntervalComparer : ITimePeriodComparer
	{

		// ----------------------------------------------------------------------
		public int Compare( ITimePeriod left, ITimePeriod right )
		{
			ITimeInterval leftInterval = left as ITimeInterval;
			ITimeInterval rightInterval = right as ITimeInterval;

			int compare;
			if ( leftInterval != null && rightInterval != null )
			{
				compare = leftInterval.StartInterval.CompareTo( leftInterval.EndInterval );
			}
			else
			{
				compare = left.CompareTo( right, TimePeriodStartComparer.Comparer ); // compare by start
				//compare = left.CompareTo( right, TimePeriodDurationComparer.Comparer ); // compare by duration
			}
			return compare;
		} // Compare

	} // class TimeIntervalComparer

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
