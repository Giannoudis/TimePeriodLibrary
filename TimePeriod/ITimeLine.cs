// -- FILE ------------------------------------------------------------------
// name       : ITimeLine.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.31
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface ITimeLine
	{

		// ----------------------------------------------------------------------
		ITimePeriodContainer Periods { get; }

		// ----------------------------------------------------------------------
		ITimePeriod Limits { get; }

		// ----------------------------------------------------------------------
		ITimePeriodMapper PeriodMapper { get; }

		// ----------------------------------------------------------------------
		bool HasOverlaps();

		// ----------------------------------------------------------------------
		bool HasGaps();

		// ----------------------------------------------------------------------
		ITimePeriodCollection CombinePeriods();

		// ----------------------------------------------------------------------
		ITimePeriodCollection IntersectPeriods( bool combinePeriods );

		// ----------------------------------------------------------------------
		ITimePeriodCollection CalculateGaps();

	} // interface ITimeLine

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
