// -- FILE ------------------------------------------------------------------
// name       : IDurationProvider.cs
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
	public interface IDurationProvider
	{

		// ----------------------------------------------------------------------
		TimeSpan GetDuration( DateTime start, DateTime end );

	} // interface IDurationProvider

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
