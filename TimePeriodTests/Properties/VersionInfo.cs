// -- FILE ------------------------------------------------------------------
// name       : VersionInfo.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Reflection;

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("2.0.0.0")]

// ReSharper disable CheckNamespace
namespace Itenso.TimePeriodTests
// ReSharper restore CheckNamespace
{

	// ------------------------------------------------------------------------
	public sealed class VersionInfo
	{

		/// <value>Provides easy access to the assemblies version as a string.</value>
		public static readonly string AssemblyVersion = typeof( VersionInfo ).Assembly.GetName().Version.ToString();

	} // class VersionInfo

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
