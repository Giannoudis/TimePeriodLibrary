// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCalc.cs
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
	internal static class TimePeriodCalc
	{

		// ----------------------------------------------------------------------
		public static bool HasInside( ITimePeriod period, DateTime test )
		{
			return test >= period.Start && test <= period.End;
		} // HasInside

		// ----------------------------------------------------------------------
		public static bool HasInside( ITimePeriod period, ITimePeriod test )
		{
			return HasInside( period, test.Start ) && HasInside( period, test.End );
		} // HasInside

		// ----------------------------------------------------------------------
		public static bool IntersectsWith( ITimePeriod period, ITimePeriod test )
		{
			return
				HasInside( period, test.Start ) ||
				HasInside( period, test.End ) ||
				( test.Start < period.Start && test.End > period.End );
		} // IntersectsWith

		// ----------------------------------------------------------------------
		public static bool OverlapsWith( ITimePeriod period, ITimePeriod test )
		{
			PeriodRelation relation = GetRelation( period, test );
			return
				relation != PeriodRelation.After &&
				relation != PeriodRelation.StartTouching &&
				relation != PeriodRelation.EndTouching &&
				relation != PeriodRelation.Before;
		} // OverlapsWith

		// ----------------------------------------------------------------------
		public static PeriodRelation GetRelation( ITimePeriod period, ITimePeriod test )
		{
			if ( test.End < period.Start )
			{
				return PeriodRelation.After;
			}
			if ( test.Start > period.End )
			{
				return PeriodRelation.Before;
			}
			if ( test.Start == period.Start && test.End == period.End )
			{
				return PeriodRelation.ExactMatch;
			}
			if ( test.End == period.Start )
			{
				return PeriodRelation.StartTouching;
			}
			if ( test.Start == period.End )
			{
				return PeriodRelation.EndTouching;
			}
			if ( HasInside( period, test ) )
			{
				if ( test.Start == period.Start )
				{
					return PeriodRelation.EnclosingStartTouching;
				}
				return test.End == period.End ? PeriodRelation.EnclosingEndTouching : PeriodRelation.Enclosing;
			}
			bool periodContainsMyStart = HasInside( test, period.Start );
			bool periodContainsMyEnd = HasInside( test, period.End );
			if ( periodContainsMyStart && periodContainsMyEnd )
			{
				if ( test.Start == period.Start )
				{
					return PeriodRelation.InsideStartTouching;
				}
				return test.End == period.End ? PeriodRelation.InsideEndTouching : PeriodRelation.Inside;
			}
			if ( periodContainsMyStart )
			{
				return PeriodRelation.StartInside;
			}
			if ( periodContainsMyEnd )
			{
				return PeriodRelation.EndInside;
			}
			throw new InvalidOperationException( "invalid period relation of '" + period + "' and '" + test + "'" );
		} // GetRelation

	} // class TimePeriodCalc

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
