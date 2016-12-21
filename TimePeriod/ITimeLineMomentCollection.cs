// -- FILE ------------------------------------------------------------------
// name       : ITimeLineMomentCollection.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.31
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface ITimeLineMomentCollection : IEnumerable<ITimeLineMoment>
	{

		// ----------------------------------------------------------------------
		int Count { get; }

		// ----------------------------------------------------------------------
		bool IsEmpty { get; }

		// ----------------------------------------------------------------------
		ITimeLineMoment Min { get; }

		// ----------------------------------------------------------------------
		ITimeLineMoment Max { get; }

		// ----------------------------------------------------------------------
		ITimeLineMoment this[ int index ] { get; }

		// ----------------------------------------------------------------------
		ITimeLineMoment this[ DateTime moment ] { get; }

		// ----------------------------------------------------------------------
		void Add( ITimePeriod period );

		// ----------------------------------------------------------------------
		void AddAll( IEnumerable<ITimePeriod> periods );

		// ----------------------------------------------------------------------
		void Remove( ITimePeriod period );

		// ----------------------------------------------------------------------
		ITimeLineMoment Find( DateTime moment );

		// ----------------------------------------------------------------------
		bool Contains( DateTime moment );

		// ----------------------------------------------------------------------
		bool HasOverlaps();

		// ----------------------------------------------------------------------
		bool HasGaps();

	} // interface ITimeLineMomentCollection

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
