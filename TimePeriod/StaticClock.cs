// -- FILE ------------------------------------------------------------------
// name       : StaticClock.cs
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
	public class StaticClock : IClock
	{

		// ----------------------------------------------------------------------
		public StaticClock( DateTime now )
		{
			this.now = now;
		} // StaticClock

		// ----------------------------------------------------------------------
		public DateTime Now
		{
			get { return now; }
		} // Now

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime now;

	} // class StaticClock

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
