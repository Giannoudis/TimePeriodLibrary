// -- FILE ------------------------------------------------------------------
// name       : IDateTimeSet.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface IDateTimeSet : ICollection<DateTime>
	{

		// ----------------------------------------------------------------------
		DateTime this[ int index ] { get; }

		// ----------------------------------------------------------------------
		DateTime? Min { get; }

		// ----------------------------------------------------------------------
		DateTime? Max { get; }

		// ----------------------------------------------------------------------
		TimeSpan? Duration { get; }

		// ----------------------------------------------------------------------
		bool IsEmpty { get; }

		// ----------------------------------------------------------------------
		bool IsMoment { get; }

		// ----------------------------------------------------------------------
		bool IsAnytime { get; }

		// ----------------------------------------------------------------------
		int IndexOf( DateTime moment );
		
		// ----------------------------------------------------------------------
		new bool Add( DateTime moment );

		// ----------------------------------------------------------------------
		void AddAll( IEnumerable<DateTime> moments );

		// ----------------------------------------------------------------------
		IList<TimeSpan> GetDurations( int startIndex, int count );

		// ----------------------------------------------------------------------
		DateTime? FindPrevious( DateTime moment );

		// ----------------------------------------------------------------------
		DateTime? FindNext( DateTime moment );

	} // class IDateTimeSet

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
