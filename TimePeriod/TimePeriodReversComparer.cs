// -- FILE ------------------------------------------------------------------
// name       : TimePeriodReversComparer.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.26
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public sealed class TimePeriodReversComparer : ITimePeriodComparer
	{

		// ----------------------------------------------------------------------
		public TimePeriodReversComparer( ITimePeriodComparer baseComparer )
		{
			this.baseComparer = baseComparer;
		} // TimePeriodReversComparer

		// ----------------------------------------------------------------------
		public ITimePeriodComparer BaseComparer
		{
			get { return baseComparer; }
		} // BaseComparer

		// ----------------------------------------------------------------------
		public int Compare( ITimePeriod left, ITimePeriod right )
		{
			return -baseComparer.Compare( left, right );
		} // Compare

		// ----------------------------------------------------------------------
		// members
		private readonly ITimePeriodComparer baseComparer;

	} // class TimePeriodReversComparer

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
