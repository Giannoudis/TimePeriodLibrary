// -- FILE ------------------------------------------------------------------
// name       : CommunitySamples.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.04.06
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class IdTimeRange : TimeRange
	{

		// ----------------------------------------------------------------------
		public IdTimeRange( int id, DateTime start, DateTime end ) :
			base( start, end )
		{
			Id = id;
		} // IdTimeRange		

		// ----------------------------------------------------------------------
		public int Id { get; private set; }

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return Id + ": " + base.ToString();
		} // ToString

	} // IdTimeRange

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
