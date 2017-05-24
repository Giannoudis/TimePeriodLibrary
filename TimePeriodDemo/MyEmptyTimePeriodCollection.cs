// -- FILE ------------------------------------------------------------------
// name       : MyEmptyTimePeriodCollection.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.10.20
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class MyEmptyTimePeriodCollection : TimePeriodCollection
	{

		// ----------------------------------------------------------------------
		protected override TimeSpan? GetDuration()
		{
			return base.GetDuration() ?? TimeSpan.Zero;
		} // GetDuration

	} // class MyEmptyTimePeriodCollection

} // namespace Itenso.TimePeriodDemo.Player
// -- EOF -------------------------------------------------------------------
