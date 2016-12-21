// -- FILE ------------------------------------------------------------------
// name       : ITimePeriodMapper.cs
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
	public interface ITimePeriodMapper
	{

		// ----------------------------------------------------------------------
		DateTime MapStart( DateTime moment );

		// ----------------------------------------------------------------------
		DateTime MapEnd( DateTime moment );

		// ----------------------------------------------------------------------
		DateTime UnmapStart( DateTime moment );

		// ----------------------------------------------------------------------
		DateTime UnmapEnd( DateTime moment );

	} // interface ITimePeriodMapper

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
