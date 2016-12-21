// -- FILE ------------------------------------------------------------------
// name       : SystemClock.cs
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
	public class SystemClock : IClock
	{

		// ----------------------------------------------------------------------
		internal SystemClock()
		{
		} // SystemClock

		// ----------------------------------------------------------------------
		public DateTime Now
		{
			get { return DateTime.Now; }
		} // Now

	} // class SystemClock

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
