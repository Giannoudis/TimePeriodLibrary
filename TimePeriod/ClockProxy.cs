// -- FILE ------------------------------------------------------------------
// name       : ClockProxy.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public static class ClockProxy
	{

		// ----------------------------------------------------------------------
		public static IClock Clock
		{
			get
			{
				if ( clock == null )
				{
					lock ( mutex )
					{
						if ( clock == null )
						{
							clock = new SystemClock();
						}
					}
				}
				return clock;
			}
			set
			{
				lock ( mutex )
				{
					clock = value;
				}
			}
		} // Clock

		// ----------------------------------------------------------------------
		// members
		private static readonly object mutex = new object();
		private static volatile IClock clock;

	} // class ClockProxy

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
