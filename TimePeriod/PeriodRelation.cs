// -- FILE ------------------------------------------------------------------
// name       : PeriodRelation.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public enum PeriodRelation
	{
		After,
		StartTouching,
		StartInside,
		InsideStartTouching,
		EnclosingStartTouching,
		Enclosing,
		EnclosingEndTouching,
		ExactMatch,
		Inside,
		InsideEndTouching,
		EndInside,
		EndTouching,
		Before,
	} // enum PeriodRelation

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
