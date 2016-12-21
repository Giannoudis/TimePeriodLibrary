// -- FILE ------------------------------------------------------------------
// name       : ITimePeriodChain.cs
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
	public interface ITimePeriodChain : ITimePeriodContainer
	{

		// ----------------------------------------------------------------------
		new DateTime Start { get; set; }

		// ----------------------------------------------------------------------
		new DateTime End { get; set; }

		// ----------------------------------------------------------------------
		ITimePeriod First { get; }

		// ----------------------------------------------------------------------
		ITimePeriod Last { get; }

	} // interface ITimePeriodChain

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
