// -- FILE ------------------------------------------------------------------
// name       : ITimeRange.cs
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
	public interface ITimeRange : ITimePeriod
	{

		// ----------------------------------------------------------------------
		new DateTime Start { get; set; }

		// ----------------------------------------------------------------------
		new DateTime End { get; set; }

		// ----------------------------------------------------------------------
		new TimeSpan Duration { get; set; }

		// ----------------------------------------------------------------------
		void Move( TimeSpan offset );

		// ----------------------------------------------------------------------
		void ExpandStartTo( DateTime moment );

		// ----------------------------------------------------------------------
		void ExpandEndTo( DateTime moment );

		// ----------------------------------------------------------------------
		void ExpandTo( DateTime moment );

		// ----------------------------------------------------------------------
		void ExpandTo( ITimePeriod period );

		// ----------------------------------------------------------------------
		void ShrinkStartTo( DateTime moment );

		// ----------------------------------------------------------------------
		void ShrinkEndTo( DateTime moment );

		// ----------------------------------------------------------------------
		void ShrinkTo( ITimePeriod period );

		// ----------------------------------------------------------------------
		ITimeRange Copy( TimeSpan offset );

		// ----------------------------------------------------------------------
		ITimeRange GetIntersection( ITimePeriod period );

	} // interface ITimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
