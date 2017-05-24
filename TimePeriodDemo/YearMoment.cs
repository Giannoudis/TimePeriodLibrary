// -- FILE ------------------------------------------------------------------
// name       : YearMoment.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.10.17
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class YearMoment
	{

		// ----------------------------------------------------------------------
		public YearMoment() :
			this( DateTime.Now )
		{
		} // YearMoment

		// ----------------------------------------------------------------------
		public YearMoment( DateTime moment ) :
			this( moment.Month, moment.Day )
		{
		} // YearMoment

		// ----------------------------------------------------------------------
		public YearMoment( int month, int day )
		{
			Month = month;
			Day = day;
		} // YearMoment

		// ----------------------------------------------------------------------
		public int Month
		{
			get { return month; }
			set
			{
				if ( value < 1 || value > TimeSpec.MonthsPerYear )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				month = value;
			}
		} // Month

		// ----------------------------------------------------------------------
		public int Day
		{
			get { return day; }
			set
			{
				if ( value < 1 || value > TimeSpec.MaxDaysPerMonth )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				day = value;
			}
		} // Day

		// ----------------------------------------------------------------------
		// members
		private int month;
		private int day;

	} // YearMoment

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
