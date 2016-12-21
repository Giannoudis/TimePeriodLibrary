// -- FILE ------------------------------------------------------------------
// name       : CalendarPeriodCollectorContext.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class CalendarPeriodCollectorContext : ICalendarVisitorContext
	{

		// ----------------------------------------------------------------------
		public enum CollectType
		{
			Year,
			Month,
			Day,
			Hour,
		} // enum CollectType

		// ----------------------------------------------------------------------
		public CollectType Scope { get; set; }

	} // class CalendarPeriodCollectorContext

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
