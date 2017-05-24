// -- FILE ------------------------------------------------------------------
// name       : SampleAttribute.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.24
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	[AttributeUsage( AttributeTargets.Method )]
	internal class SampleAttribute : Attribute
	{

		// ----------------------------------------------------------------------
		public SampleAttribute( string group )
		{
			Group = group;
		} // SampleAttribute

		// ----------------------------------------------------------------------
		public string Group { get; set; }

	} // class SampleAttribute

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
