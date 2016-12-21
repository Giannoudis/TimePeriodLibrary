// -- FILE ------------------------------------------------------------------
// name       : DurationProvider.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.11.03
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class DurationProvider : IDurationProvider
	{

		// ----------------------------------------------------------------------
		public virtual TimeSpan GetDuration( DateTime start, DateTime end )
		{
			return end.Subtract( start );
		} // GetDuration

	} // class DurationProvider

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
