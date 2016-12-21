// -- FILE ------------------------------------------------------------------
// name       : YearMomentCollection.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.10.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class YearMomentCollection : List<YearMoment>
	{

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetPeriodsOfYear( int year )
		{
			List<DateTime> timeLineMoments = new List<DateTime>();

			DateTime yearStartDay = new DateTime( year, 1, 1 );
			DateTime yearEndDay = yearStartDay.AddYears( 1 );

			timeLineMoments.Add( yearStartDay );
			foreach ( YearMoment yearMoment in this )
			{
				// add error handling in case of invalid date like month=2 and day=30
				timeLineMoments.Add( new DateTime( year, yearMoment.Month, yearMoment.Day ) );
			}
			timeLineMoments.Add( yearEndDay );

			timeLineMoments.Sort( ( left, right ) => left.Ticks.CompareTo( right.Ticks ) );

			TimePeriodCollection yearPeriods = new TimePeriodCollection();
			for ( int i = 0; i < timeLineMoments.Count - 1; i++ )
			{
				DateTime start = timeLineMoments[ i ];
				DateTime end = timeLineMoments[ i + 1 ];
				int dayCount = end.Subtract( start ).Days;
				yearPeriods.Add( new Days( start, dayCount ) );
			}

			return yearPeriods;
		} // GetPeriodsOfYear

	} // YearMomentCollection

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
