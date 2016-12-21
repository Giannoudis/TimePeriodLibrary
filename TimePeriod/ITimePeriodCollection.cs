// -- FILE ------------------------------------------------------------------
// name       : ITimePeriodCollection.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
#if (!PCL)
using System.ComponentModel;
#endif

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface ITimePeriodCollection : ITimePeriodContainer
	{

		// ----------------------------------------------------------------------
		new DateTime Start { get; set; }

		// ----------------------------------------------------------------------
		new DateTime End { get; set; }

		// ----------------------------------------------------------------------
		TimeSpan TotalDuration { get; }

		// ----------------------------------------------------------------------
		TimeSpan GetTotalDuration( IDurationProvider provider );

		// ----------------------------------------------------------------------
		void SortBy( ITimePeriodComparer comparer );

		// ----------------------------------------------------------------------
		void SortReverseBy( ITimePeriodComparer comparer );

		// ----------------------------------------------------------------------
		void SortByStart( ListSortDirection sortDirection = ListSortDirection.Ascending );

		// ----------------------------------------------------------------------
		void SortByEnd( ListSortDirection sortDirection = ListSortDirection.Ascending );

		// ----------------------------------------------------------------------
		void SortByDuration( ListSortDirection sortDirection = ListSortDirection.Ascending );

		// ----------------------------------------------------------------------
		bool HasInsidePeriods( ITimePeriod test );

		// ----------------------------------------------------------------------
		bool HasOverlaps();

		// ----------------------------------------------------------------------
		bool HasGaps();

		// ----------------------------------------------------------------------
		bool HasOverlapPeriods( ITimePeriod test );

		// ----------------------------------------------------------------------
		bool HasIntersectionPeriods( DateTime test );

		// ----------------------------------------------------------------------
		bool HasIntersectionPeriods( ITimePeriod test );

		// ----------------------------------------------------------------------
		ITimePeriodCollection InsidePeriods( ITimePeriod test );

		// ----------------------------------------------------------------------
		ITimePeriodCollection OverlapPeriods( ITimePeriod test );

		// ----------------------------------------------------------------------
		ITimePeriodCollection IntersectionPeriods( DateTime test );

		// ----------------------------------------------------------------------
		ITimePeriodCollection IntersectionPeriods( ITimePeriod test );

		// ----------------------------------------------------------------------
		ITimePeriodCollection RelationPeriods( ITimePeriod test, PeriodRelation relation );

	} // interface ITimePeriodCollection

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
