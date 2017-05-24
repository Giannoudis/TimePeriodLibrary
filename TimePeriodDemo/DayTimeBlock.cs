// -- FILE ------------------------------------------------------------------
// name       : DayTimeBlock.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.09.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class DayTimeBlock : TimeBlock
	{

		// ----------------------------------------------------------------------
		public DayTimeBlock( int dayCount ) :
			this( DateTime.Now, dayCount )
		{
		} // DayTimeBlock

		// ----------------------------------------------------------------------
		public DayTimeBlock( DateTime start, int dayCount ) :
			base( start.Date, start.AddDays( dayCount ) )
		{
		} // DayTimeBlock

		// ----------------------------------------------------------------------
		public DayTimeBlock( int startYear, int startMonth, int startDay, int dayCount ) :
			this( new DateTime( startYear, startMonth, startDay ), dayCount )
		{
		} // DayTimeBlock

		// ----------------------------------------------------------------------
		public override void Setup( DateTime newStart, DateTime newEnd )
		{
			base.Setup( newStart.Date, newEnd.Date );
		} // Setup

		// ----------------------------------------------------------------------
		public override void Setup( DateTime newStart, TimeSpan newDuration )
		{
			base.Setup( newStart.Date, newDuration );
		} // Setup

	} // DayTimeBlock

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
