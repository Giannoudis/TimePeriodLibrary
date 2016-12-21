// -- FILE ------------------------------------------------------------------
// name       : ITimeBlock.cs
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
	public interface ITimeBlock : ITimePeriod
	{

		// ----------------------------------------------------------------------
		new DateTime Start { get; set; }

		// ----------------------------------------------------------------------
		new DateTime End { get; set; }

		// ----------------------------------------------------------------------
		new TimeSpan Duration { get; set; }

		// ----------------------------------------------------------------------
		void Setup( DateTime newStart, TimeSpan newDuration );

		// ----------------------------------------------------------------------
		void Move( TimeSpan delta );

		// ----------------------------------------------------------------------
		void DurationFromStart( TimeSpan newDuration );

		// ----------------------------------------------------------------------
		void DurationFromEnd( TimeSpan newDuration );

		// ----------------------------------------------------------------------
		ITimeBlock Copy( TimeSpan delta );

		// ----------------------------------------------------------------------
		ITimeBlock GetPreviousPeriod( TimeSpan offset );

		// ----------------------------------------------------------------------
		ITimeBlock GetNextPeriod( TimeSpan offset );

		// ----------------------------------------------------------------------
		ITimeBlock GetIntersection( ITimePeriod period );

	} // class ITimeBlock

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
