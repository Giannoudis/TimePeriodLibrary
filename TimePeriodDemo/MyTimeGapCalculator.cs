// -- FILE ------------------------------------------------------------------
// name       : MyTimeGapCalculator.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class MyTimeGapCalculator
	{

		// ----------------------------------------------------------------------
		public IList<MyTimePeriod> GetGaps( IEnumerable<MyTimePeriod> periods )
		{
			TimeGapCalculator<TimeRange> gapCalculator = new TimeGapCalculator<TimeRange>();

			TimePeriodCollection calcPeriods = new TimePeriodCollection();
			foreach ( MyTimePeriod period in periods )
			{
				calcPeriods.Add( new TimeRange( period.Start, period.End ) );
			}

			List<MyTimePeriod> gaps = new List<MyTimePeriod>();
			if ( calcPeriods.Count == 0 )
			{
				return gaps;
			}

			ITimePeriodCollection calcCaps = gapCalculator.GetGaps( calcPeriods );
			foreach ( TimeRange calcCap in calcCaps )
			{
				gaps.Add( new MyTimePeriod { Start = calcCap.Start, End = calcCap.End } );
			}

			return gaps;
		} // GetGaps

	} // class MyTimeGapCalculator

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
