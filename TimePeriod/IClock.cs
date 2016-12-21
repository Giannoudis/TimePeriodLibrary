// -- FILE ------------------------------------------------------------------
// name       : IClock.cs
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
	// see http://stackoverflow.com/questions/43711/whats-a-good-way-to-overwrite-datetime-now-during-testing
	public interface IClock
	{

		// ----------------------------------------------------------------------
		DateTime Now { get; }

	} // interface IClock

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
