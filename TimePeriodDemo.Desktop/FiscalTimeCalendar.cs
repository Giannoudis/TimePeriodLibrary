// -- FILE ------------------------------------------------------------------
// name       : FiscalTimeCalendar.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class FiscalTimeCalendar : TimeCalendar
	{

		// ----------------------------------------------------------------------
		public FiscalTimeCalendar()
			: base(
				new TimeCalendarConfig
				{
					YearBaseMonth = YearMonth.October,  //  October year base month
					YearWeekType = YearWeekType.Iso8601, // ISO 8601 week numbering
					YearType = YearType.FiscalYear// treat years as fiscal years
				} )
		{
		} // FiscalTimeCalendar

	} // class FiscalTimeCalendar

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
